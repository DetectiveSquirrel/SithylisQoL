using System;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud;
using PoeHUD.Models;
using PoeHUD.Poe.Components;
using Random_Features.Libs;
using SharpDX;
using SharpDX.Direct3D9;

namespace Random_Features
{
    partial class RandomFeatures
    {
        public void AreaTranitionsMenu()
        {
            Settings.AreaTransition.Value = ImGuiExtension.Checkbox("Shows Area Transition Names", Settings.AreaTransition.Value);
            Settings.AreaTransitionHideLocalTranition.Value = ImGuiExtension.Checkbox("Hide Same Area Transitions", Settings.AreaTransitionHideLocalTranition.Value);
            Settings.AreaTransitionHideInTownOrHideout.Value = ImGuiExtension.Checkbox("Hide In Town/Hideout", Settings.AreaTransitionHideInTownOrHideout.Value);
            Settings.AreaTransitionMaxLength.Value = ImGuiExtension.IntSlider("Text Wrap Length", Settings.AreaTransitionMaxLength);
            Settings.AreaTransitionLargeMapYOffset.Value = ImGuiExtension.IntSlider("large Map Text Y Offset", Settings.AreaTransitionLargeMapYOffset);
            Settings.AreaTransitionSizeSmall.Value = ImGuiExtension.IntSlider("Text Size On Small Map", Settings.AreaTransitionSizeSmall);
            Settings.AreaTransitionSize.Value = ImGuiExtension.IntSlider("Text Size", Settings.AreaTransitionSize);
            Settings.AreaTransitionColor = ImGuiExtension.ColorPicker("Text Color", Settings.AreaTransitionColor);
            Settings.AreaTransitionColorBackground = ImGuiExtension.ColorPicker("Text Color Background", Settings.AreaTransitionColorBackground);
        }

        private void AreaTranitions()
        {
            if (!Settings.AreaTransition) return;
            if (GameController.Game.IngameState.IngameUi.AtlasPanel.IsVisible || GameController.Game.IngameState.IngameUi.TreePanel.IsVisible)
                return;
            if (LocalPlayer.Area.IsHideout || LocalPlayer.Area.IsTown && Settings.AreaTransitionHideInTownOrHideout)
                return;
            foreach (var entity in GameController.Entities)
                try
                {
                    if (entity.HasComponent<AreaTransition>())
                    {
                        if (entity.GetComponent<AreaTransition>().TransitionType == AreaTransition.AreaTransitionType.Local && Settings.AreaTransitionHideLocalTranition) return;
                        if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
                        {
                            var TextInfo = new MinimapTextInfo
                            {
                                    Text = entity.GetComponent<AreaTransition>().TransitionType == AreaTransition.AreaTransitionType.Local ? LocalPlayer.Area.Name : entity.GetComponent<AreaTransition>().WorldArea.Name,
                                    FontSize = Settings.AreaTransitionSize,
                                    FontColor = Settings.AreaTransitionColor,
                                    FontBackgroundColor = Settings.AreaTransitionColorBackground,
                                    TextWrapLength = Settings.AreaTransitionMaxLength,
                                    TextOffsetY = Settings.AreaTransitionLargeMapYOffset
                            };
                            DrawToLargeMiniMapText(entity, TextInfo);
                        }

                        if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
                        {
                            var TextInfo = new MinimapTextInfo
                            {
                                    Text = entity.GetComponent<AreaTransition>().TransitionType == AreaTransition.AreaTransitionType.Local ? LocalPlayer.Area.Name : entity.GetComponent<AreaTransition>().WorldArea.Name,
                                    FontSize = Settings.AreaTransitionSizeSmall,
                                    FontColor = Settings.AreaTransitionColor,
                                    FontBackgroundColor = Settings.AreaTransitionColorBackground,
                                    TextWrapLength = Settings.AreaTransitionMaxLength,
                                    TextOffsetY = 0
                            };
                            DrawToSmallMiniMapText(entity, TextInfo);
                        }
                    }
                }
                catch
                {
                }
        }

        private void DrawToLargeMiniMapText(EntityWrapper entity, MinimapTextInfo info)
        {
            var camera = GameController.Game.IngameState.Camera;
            var mapWindow = GameController.Game.IngameState.IngameUi.Map;
            var mapRect = mapWindow.GetClientRect();
            var playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var posZ = GameController.Player.GetComponent<Render>().Z;
            var screenCenter = new Vector2(mapRect.Width / 2, mapRect.Height / 2).Translate(0, -20) + new Vector2(mapRect.X, mapRect.Y) + new Vector2(mapWindow.LargeMapShiftX, mapWindow.LargeMapShiftY);
            var diag = (float) Math.Sqrt(camera.Width * camera.Width + camera.Height * camera.Height);
            var k = camera.Width < 1024f ? 1120f : 1024f;
            var scale = k / camera.Height * camera.Width * 3f / 4f / mapWindow.LargeMapZoom;
            var iconZ = entity.GetComponent<Render>().Z;
            var point = screenCenter + MapIcon.DeltaInWorldToMinimapDelta(entity.GetComponent<Positioned>().GridPos - playerPos, diag, scale, (iconZ - posZ) / (9f / mapWindow.LargeMapZoom));
            point.Y += info.TextOffsetY;
            var size = Graphics.DrawText(WordWrap(info.Text, info.TextWrapLength), info.FontSize, point, info.FontColor, FontDrawFlags.Center);
            float maxWidth = 0;
            float maxheight = 0;
            point.Y += size.Height;
            maxheight += size.Height;
            maxWidth = Math.Max(maxWidth, size.Width);
            var background = new RectangleF(point.X - maxWidth / 2 - 3, point.Y - maxheight, maxWidth + 6, maxheight);
            Graphics.DrawBox(background, info.FontBackgroundColor);
        }

        private void DrawToSmallMiniMapText(EntityWrapper entity, MinimapTextInfo info)
        {
            var camera = GameController.Game.IngameState.Camera;
            var mapWindow = GameController.Game.IngameState.IngameUi.Map;
            var mapRect = mapWindow.SmallMinimap.GetClientRect();
            var playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var posZ = GameController.Player.GetComponent<Render>().Z;
            var screenCenter = new Vector2(mapRect.Width / 2, mapRect.Height / 2).Translate(0, -20) + new Vector2(mapRect.X, mapRect.Y) + new Vector2(mapWindow.SmallMinimapX, mapWindow.SmallMinimapY);
            var diag = (float) Math.Sqrt(camera.Width * camera.Width + camera.Height * camera.Height);
            var k = camera.Width < 1024f ? 1120f : 1024f;
            var scale = k / camera.Height * camera.Width * 3f / 4f / mapWindow.SmallMinimapZoom;
            var iconZ = entity.GetComponent<Render>().Z;
            var point = screenCenter + MapIcon.DeltaInWorldToMinimapDelta(entity.GetComponent<Positioned>().GridPos - playerPos, diag, scale, (iconZ - posZ) / (9f / mapWindow.SmallMinimapZoom));
            point.Y += info.TextOffsetY;
            var size = Graphics.DrawText(WordWrap(info.Text, info.TextWrapLength), info.FontSize, point, info.FontColor, FontDrawFlags.Center);
            float maxWidth = 0;
            float maxheight = 0;
            point.Y += size.Height;
            maxheight += size.Height;
            maxWidth = Math.Max(maxWidth, size.Width);
            var background = new RectangleF(point.X - maxWidth / 2 - 3, point.Y - maxheight, maxWidth + 6, maxheight);
            Graphics.DrawBox(background, info.FontBackgroundColor);
        }
    }

    public class MinimapTextInfo
    {
        public MinimapTextInfo() { }

        public MinimapTextInfo(string text, int fontSize, Color fontColor, Color fontBackgroundColor, int textWrapLength, int textOffsetY)
        {
            Text = text;
            FontSize = fontSize;
            FontColor = fontColor;
            FontBackgroundColor = fontBackgroundColor;
            TextWrapLength = textWrapLength;
            TextOffsetY = textOffsetY;
        }

        public string Text { get; set; }
        public int FontSize { get; set; }
        public Color FontColor { get; set; }
        public Color FontBackgroundColor { get; set; }
        public int TextWrapLength { get; set; }
        public int TextOffsetY { get; set; }
    }
}