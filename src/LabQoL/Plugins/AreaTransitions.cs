using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud;
using PoeHUD.Poe.Components;
using Random_Features.Libs;
using SharpDX;
using SharpDX.Direct3D9;

namespace Random_Features
{
    partial class RandomFeatures
    {
        public static List<StoredEntity> StoredAreaEntities = new List<StoredEntity>();

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
            Settings.Debug = ImGuiExtension.Checkbox("Debug", Settings.Debug);
        }

        public int CompletedTrial(string TrialString)
        {
            var Trial = GameController.Player.GetComponent<Player>().TrialStates;
            var ReturnState = 0;
            foreach (var TrialState in Trial)
            {
                if (TrialState.TrialArea.Area.Name == TrialString)
                    ReturnState = 2;
                if (TrialState.TrialArea.Area.Name == TrialString && TrialState.IsCompleted)
                {
                    ReturnState = 1;
                    break;
                }
            }

            return ReturnState;
        }

        private IEnumerator ClearStoredEntities()
        {
            StoredAreaEntities.Clear();
            yield return new WaitFunction(() => GameController.Game.IsGameLoading);
        }

        private void AreaTranitions()
        {
            if (!Settings.AreaTransition) return;
            if (GameController.Game.IngameState.IngameUi.AtlasPanel.IsVisible || GameController.Game.IngameState.IngameUi.TreePanel.IsVisible)
                return;
            if (LocalPlayer.Area.IsHideout || LocalPlayer.Area.IsTown && Settings.AreaTransitionHideInTownOrHideout)
                return;
            foreach (var Entity in GameController.Entities)
                try
                {
                    if (Entity.HasComponent<AreaTransition>())
                    {
                        var PositionedComp = Entity.GetComponent<Positioned>();
                        if (Entity.GetComponent<AreaTransition>().TransitionType == AreaTransition.AreaTransitionType.Local && Settings.AreaTransitionHideLocalTranition) return;
                        var TextInfo = new MinimapTextInfo
                        {
                                Text = Entity.GetComponent<AreaTransition>().TransitionType == AreaTransition.AreaTransitionType.Local ? "Local Transition" : Entity.GetComponent<AreaTransition>().WorldArea.Name,
                                FontSize = Settings.AreaTransitionSizeSmall,
                                FontColor = Settings.AreaTransitionColor,
                                FontBackgroundColor = Settings.AreaTransitionColorBackground,
                                TextWrapLength = Settings.AreaTransitionMaxLength
                        };
                        if (TextInfo.Text.Contains("NULL")) return;

                        if (Entity.GetComponent<AreaTransition>().WorldArea.IsMapTrialArea)
                        {
                            if (CompletedTrial(TextInfo.Text) == 1)
                                TextInfo.Text = $"(✓) {TextInfo.Text}";
                            else if (CompletedTrial(TextInfo.Text) == 2)
                                TextInfo.Text = $"(✕) {TextInfo.Text}";
                        }
                        if (StoredAreaEntities.Any(X => X.GridPos == PositionedComp.GridPos))
                        {
                            var FindIndex = StoredAreaEntities.FindIndex(X => X.GridPos == PositionedComp.GridPos);
                            StoredAreaEntities[FindIndex] = new StoredEntity(Entity.GetComponent<Render>().Z, PositionedComp.GridPos, Entity.LongId, TextInfo);
                        }
                        else
                        {
                            if (Settings.Debug)
                                LogMessage($"Random Features [AreaTransitions] Added Area | Area:{TextInfo.Text}", 3);
                            StoredAreaEntities.Add(new StoredEntity(Entity.GetComponent<Render>().Z, PositionedComp.GridPos, Entity.LongId, TextInfo));
                        }
                    }
                }
                catch
                {
                }

            foreach (var StoredAreaEntity in StoredAreaEntities)
            {
                if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
                    DrawToLargeMiniMapText(StoredAreaEntity, StoredAreaEntity.TextureInfo);
                if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
                    DrawToSmallMiniMapText(StoredAreaEntity, StoredAreaEntity.TextureInfo);
            }
        }

        private void DrawToLargeMiniMapText(StoredEntity Entity, MinimapTextInfo Info)
        {
            var Camera = GameController.Game.IngameState.Camera;
            var MapWindow = GameController.Game.IngameState.IngameUi.Map;
            var MapRect = MapWindow.GetClientRect();
            var PlayerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var PosZ = GameController.Player.GetComponent<Render>().Z;
            var ScreenCenter = new Vector2(MapRect.Width / 2, MapRect.Height / 2).Translate(0, -20) + new Vector2(MapRect.X, MapRect.Y) + new Vector2(MapWindow.LargeMapShiftX, MapWindow.LargeMapShiftY);
            var Diag = (float) Math.Sqrt(Camera.Width * Camera.Width + Camera.Height * Camera.Height);
            var K = Camera.Width < 1024f ? 1120f : 1024f;
            var Scale = K / Camera.Height * Camera.Width * 3f / 4f / MapWindow.LargeMapZoom;
            var IconZ = Entity.EntityZ;
            var Point = ScreenCenter + MapIcon.DeltaInWorldToMinimapDelta(Entity.GridPos - PlayerPos, Diag, Scale, (IconZ - PosZ) / (9f / MapWindow.LargeMapZoom));
            Point.Y += Settings.AreaTransitionLargeMapYOffset;
            var Size = Graphics.DrawText(WordWrap(Info.Text, Info.TextWrapLength), Info.FontSize, Point, Info.FontColor, FontDrawFlags.Center);
            float MaxWidth = 0;
            float Maxheight = 0;
            Point.Y += Size.Height;
            Maxheight += Size.Height;
            MaxWidth = Math.Max(MaxWidth, Size.Width);
            var Background = new RectangleF(Point.X - MaxWidth / 2 - 3, Point.Y - Maxheight, MaxWidth + 6, Maxheight);
            Graphics.DrawBox(Background, Info.FontBackgroundColor);
        }

        private void DrawToSmallMiniMapText(StoredEntity Entity, MinimapTextInfo Info)
        {
            var Camera = GameController.Game.IngameState.Camera;
            var MapWindow = GameController.Game.IngameState.IngameUi.Map;
            var MapRect = MapWindow.SmallMinimap.GetClientRect();
            var PlayerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var PosZ = GameController.Player.GetComponent<Render>().Z;
            var ScreenCenter = new Vector2(MapRect.Width / 2, MapRect.Height / 2).Translate(0, -20) + new Vector2(MapRect.X, MapRect.Y) + new Vector2(MapWindow.SmallMinimapX, MapWindow.SmallMinimapY);
            var Diag = (float) Math.Sqrt(Camera.Width * Camera.Width + Camera.Height * Camera.Height);
            var K = Camera.Width < 1024f ? 1120f : 1024f;
            var Scale = K / Camera.Height * Camera.Width * 3f / 4f / MapWindow.SmallMinimapZoom;
            var IconZ = Entity.EntityZ;
            var Point = ScreenCenter + MapIcon.DeltaInWorldToMinimapDelta(Entity.GridPos - PlayerPos, Diag, Scale, (IconZ - PosZ) / (9f / MapWindow.SmallMinimapZoom));
            Point.Y += Info.TextOffsetY;
            if (!MapRect.Contains(Point)) return;
            var Size = Graphics.DrawText(WordWrap(Info.Text, Info.TextWrapLength), Info.FontSize, Point, Info.FontColor, FontDrawFlags.Center);
            float MaxWidth = 0;
            float Maxheight = 0;
            Point.Y += Size.Height;
            Maxheight += Size.Height;
            MaxWidth = Math.Max(MaxWidth, Size.Width);
            var Background = new RectangleF(Point.X - MaxWidth / 2 - 3, Point.Y - Maxheight, MaxWidth + 6, Maxheight);
            Graphics.DrawBox(Background, Info.FontBackgroundColor);
        }
    }

    public class StoredEntity
    {
        public float EntityZ;
        public Vector2 GridPos;
        public long LongId;
        public MinimapTextInfo TextureInfo;

        public StoredEntity(float EntityZ, Vector2 GridPos, long LongId, MinimapTextInfo TextureInfo)
        {
            this.EntityZ = EntityZ;
            this.GridPos = GridPos;
            LongId = LongId;
            this.TextureInfo = TextureInfo;
        }
    }

    public class MinimapTextInfo
    {
        public MinimapTextInfo() { }

        public MinimapTextInfo(string Text, int FontSize, Color FontColor, Color FontBackgroundColor, int TextWrapLength, int TextOffsetY)
        {
            this.Text = Text;
            this.FontSize = FontSize;
            this.FontColor = FontColor;
            this.FontBackgroundColor = FontBackgroundColor;
            this.TextWrapLength = TextWrapLength;
            this.TextOffsetY = TextOffsetY;
        }

        public string Text { get; set; }
        public int FontSize { get; set; }
        public Color FontColor { get; set; }
        public Color FontBackgroundColor { get; set; }
        public int TextWrapLength { get; set; }
        public int TextOffsetY { get; set; }
    }
}