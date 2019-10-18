using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Helpers;
using Random_Features.Libs;
using SharpDX;

namespace Random_Features
{
    partial class RandomFeatures
    {
        public static List<StoredEntity> storedAreaEntities = new List<StoredEntity>();

        public override void OnPluginDestroyForHotReload()
        {
            base.OnPluginDestroyForHotReload();
            storedAreaEntities = new List<StoredEntity>();
        }

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
            Settings._Debug.Value = ImGuiExtension.Checkbox("Debug", Settings._Debug);
        }

        public int CompletedTrial(string trialString)
        {
            var trial = GameController.Player.GetComponent<Player>().TrialStates;
            var returnState = 0;
            foreach (var trialState in trial)
            {
                if (trialState.TrialArea.Area.Name == trialString)
                    returnState = 2;
                if (trialState.TrialArea.Area.Name == trialString && trialState.IsCompleted)
                {
                    returnState = 1;
                    break;
                }
            }

            return returnState;
        }


        private void AreaTranitions()
        {
            if (!Settings.AreaTransition) return;
            if (GameController.Game.IngameState.IngameUi.AtlasPanel.IsVisible || GameController.Game.IngameState.IngameUi.TreePanel.IsVisible)
                return;
            if (GameController.Area.CurrentArea.IsHideout || GameController.Area.CurrentArea.IsTown && Settings.AreaTransitionHideInTownOrHideout)
                return;
            foreach (var entity in GameController.Entities.ToList())
            {
                if (entity is null) continue;
                try
                {
                    if (entity.HasComponent<AreaTransition>())
                    {
                        var positionedComp = entity.GetComponent<Positioned>();
                        if (entity.GetComponent<AreaTransition>().TransitionType == AreaTransitionType.Local && Settings.AreaTransitionHideLocalTranition) return;
                        var TextInfo = new MinimapTextInfo
                        {
                            Text = entity.GetComponent<AreaTransition>().TransitionType == AreaTransitionType.Local ? "Local Transition" : entity.GetComponent<AreaTransition>().WorldArea.Name,
                            FontSize = Settings.AreaTransitionSizeSmall,
                            FontColor = Settings.AreaTransitionColor,
                            FontBackgroundColor = Settings.AreaTransitionColorBackground,
                            TextWrapLength = Settings.AreaTransitionMaxLength
                        };
                        if (TextInfo.Text.Contains("NULL")) return;

                        if (entity.GetComponent<AreaTransition>().WorldArea.IsMapTrialArea)
                        {
                            if (CompletedTrial(TextInfo.Text) == 1)
                                TextInfo.Text = $"(✓) {TextInfo.Text}";
                            else if (CompletedTrial(TextInfo.Text) == 2)
                                TextInfo.Text = $"(✕) {TextInfo.Text}";
                        }

                        if (storedAreaEntities.Any(x => x.GridPos == positionedComp.GridPos))
                        {
                            var findIndex = storedAreaEntities.FindIndex(x => x.GridPos == positionedComp.GridPos);
                            storedAreaEntities[findIndex] = new StoredEntity(entity.GetComponent<Render>().Z, positionedComp.GridPos, entity.Id, TextInfo);
                        }
                        else
                        {
                            if (Settings._Debug)
                                LogMessage($"Random Features [AreaTransitions] Added Area | Area:{TextInfo.Text}", 3);
                            storedAreaEntities.Add(new StoredEntity(entity.GetComponent<Render>().Z, positionedComp.GridPos, entity.Id, TextInfo));
                        }
                    }
                }
                catch
                {
                }
            }

            foreach (var storedAreaEntity in storedAreaEntities)
            {
                if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
                    DrawToLargeMiniMapText(storedAreaEntity, storedAreaEntity.TextureInfo);
                if (GameController.Game.IngameState.IngameUi.Map.SmallMiniMap.IsVisible)
                    DrawToSmallMiniMapText(storedAreaEntity, storedAreaEntity.TextureInfo);
            }
        }

        private void DrawToLargeMiniMapText(StoredEntity entity, MinimapTextInfo info)
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
            var iconZ = entity.EntityZ;
            var point = screenCenter + MapIcon.DeltaInWorldToMinimapDelta(entity.GridPos - playerPos, diag, scale, (iconZ - posZ) / (9f / mapWindow.LargeMapZoom));
            point.Y += Settings.AreaTransitionLargeMapYOffset;
            var size = Graphics.DrawText(WordWrap(info.Text, info.TextWrapLength), point, info.FontColor, info.FontSize, FontAlign.Center);
            float maxWidth = 0;
            float maxheight = 0;
            //not sure about sizes below, need test
            point.Y += size.Y;
            maxheight += size.Y;
            maxWidth = Math.Max(maxWidth, size.X);
            var background = new RectangleF(point.X - maxWidth / 2 - 3, point.Y - maxheight, maxWidth + 6, maxheight);
            Graphics.DrawBox(background, info.FontBackgroundColor);
        }

        private void DrawToSmallMiniMapText(StoredEntity entity, MinimapTextInfo info)
        {
            var camera = GameController.Game.IngameState.Camera;
            var mapWindow = GameController.Game.IngameState.IngameUi.Map;
            var mapRect = mapWindow.SmallMiniMap.GetClientRect();
            var playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var posZ = GameController.Player.GetComponent<Render>().Z;
            var screenCenter = new Vector2(mapRect.Width / 2, mapRect.Height / 2 ).Translate(0, -20) + new Vector2(mapRect.X, mapRect.Y) + new Vector2(mapWindow.SmallMinMapX, mapWindow.SmallMinMapY);
            var diag = (float) Math.Sqrt(camera.Width * camera.Width + camera.Height * camera.Height);
            var k = camera.Width < 1024f ? 1120f : 1024f;
            var scale = k / camera.Height * camera.Width * 3f / 4f / mapWindow.SmallMinMapZoom;
            var iconZ = entity.EntityZ;
            var point = screenCenter + MapIcon.DeltaInWorldToMinimapDelta(entity.GridPos - playerPos, diag, scale, (iconZ - posZ) / (9f / mapWindow.SmallMinMapZoom));
            point.Y += info.TextOffsetY;
            if (!mapRect.Contains(point)) return;
            var size = Graphics.DrawText(WordWrap(info.Text, info.TextWrapLength), point, info.FontColor, info.FontSize, FontAlign.Center);
            float maxWidth = 0;
            float maxheight = 0;
            //not sure about sizes below, need test
            point.Y += size.Y;
            maxheight += size.Y;
            maxWidth = Math.Max(maxWidth, size.X);
            var background = new RectangleF(point.X - maxWidth / 2 - 3, point.Y - maxheight, maxWidth + 6, maxheight);
            Graphics.DrawBox(background, info.FontBackgroundColor);
        }
    }

    public class StoredEntity
    {
        public float EntityZ;
        public Vector2 GridPos;
        public long LongID;
        public MinimapTextInfo TextureInfo;

        public StoredEntity(float entityZ, Vector2 gridPos, long longID, MinimapTextInfo textureInfo)
        {
            EntityZ = entityZ;
            GridPos = gridPos;
            LongID = longID;
            TextureInfo = textureInfo;
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