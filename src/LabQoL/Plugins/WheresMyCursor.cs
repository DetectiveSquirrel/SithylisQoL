using ImGuiNET;
using Random_Features.Libs;
using SharpDX;
using System.Collections.Generic;

namespace Random_Features
{
    partial class RandomFeatures
    {
        private Vector2 _ClickWindowOffset;

        private void WheresMyCursorMenu()
        {
            Settings.WmcMain.Value = ImGuiExtension.Checkbox("Enable##FRNToggle", Settings.WmcMain);
            Settings.WmcLineType.Value = ImGuiExtension.ComboBox("Line Type?", Settings.WmcLineType.Value, new List<string>
            {
                    "Maximum Length To Cursor",
                    "Draw To The Mouse",
                    "Draw To The Edge Of The Screen"
            });
            Settings.WmcLineColor.Value = ImGuiExtension.ColorPicker("Line Color", Settings.WmcLineColor);
            Settings.WmcLineLength.Value = ImGuiExtension.IntSlider("Line Max Length", Settings.WmcLineLength);
            Settings.WmcLineSize.Value = ImGuiExtension.IntSlider("Line Thickness", Settings.WmcLineSize);
            ImGui.Spacing();
            Settings.WmcPlayerOffsetX.Value = ImGuiExtension.IntSlider("Player Offset X", Settings.WmcPlayerOffsetX.Value, -100, 100);
            Settings.WmcPlayerOffsetY.Value = ImGuiExtension.IntSlider("Player Offset Y", Settings.WmcPlayerOffsetY.Value, -100, 100);
        }

        private void WheresMyCursor()
        {
            if (!Settings.WmcMain) return;
            var Windows = GameController.Game.IngameState.IngameUi;
            if (Windows.TreePanel.IsVisible)
                return;
            if (Windows.AtlasPanel.IsVisible)
                return;
            if (Windows.OpenLeftPanel.IsVisible)
                return;
            if (Windows.OpenRightPanel.IsVisible)
                return;
            var CursorPositionVector = Mouse.GetCursorPositionVector();
            var WindowRectangle = GameController.Window.GetWindowRectangleReal();
            _ClickWindowOffset = GameController.Window.GetWindowRectangle().TopLeft;
            CursorPositionVector -= _ClickWindowOffset;

            // need to call use -170 in Z axis for player.pos.translate as this is whats used in poecore for hp bar.
            //i have no fucking clue why this is. If you do not follow this rule you will have a jumpy hp bar
            var PlayerToScreen = LocalPlayer.PlayerToScreen;
            Vector2 FinalPointA;
            switch (Settings.WmcLineType)
            {
                case 0:
                    FinalPointA = Vector2OffsetCalculations(new Vector2(PlayerToScreen.X, WindowRectangle.Height / 2));
                    DrawLineToPosWithLength(FinalPointA, CursorPositionVector, Settings.WmcLineLength, Settings.WmcLineSize, Settings.WmcLineColor);
                    break;
                case 1:
                    FinalPointA = Vector2OffsetCalculations(new Vector2(PlayerToScreen.X, WindowRectangle.Height / 2));
                    DrawLineToPos(FinalPointA, CursorPositionVector, Settings.WmcLineSize, Settings.WmcLineColor);
                    break;
                case 2:
                    FinalPointA = Vector2OffsetCalculations(new Vector2(PlayerToScreen.X, WindowRectangle.Height / 2));
                    DrawLineToPosWithLength(FinalPointA, CursorPositionVector, (int) WindowRectangle.Width, Settings.WmcLineSize, Settings.WmcLineColor);
                    break;
            }
        }

        public Vector2 Vector2OffsetCalculations(Vector2 Information)
        {
            var FinalVectorCalculation = Information;
            FinalVectorCalculation.X = Settings.WmcPlayerOffsetXNegitive 
                                               ? FinalVectorCalculation.X - Settings.WmcPlayerOffsetX 
                                               : FinalVectorCalculation.X + Settings.WmcPlayerOffsetX;
            FinalVectorCalculation.Y = Settings.WmcPlayerOffsetYNegitive 
                                               ? FinalVectorCalculation.Y - Settings.WmcPlayerOffsetY 
                                               : FinalVectorCalculation.Y + Settings.WmcPlayerOffsetY;
            return FinalVectorCalculation;
        }

        public void DrawLineToPosWithLength(Vector2 PointA, Vector2 PointB, int LineLength, int LineSize, Color LineColor)
        {
            var Direction = PointB - PointA;
            Direction.Normalize();
            var PointC = PointA + Direction * LineLength;
            Graphics.DrawLine(PointA, PointC, LineSize, LineColor);
        }

        public void DrawLineToPos(Vector2 PointA, Vector2 PointB, int LineSize, Color LineColor)
        {
            Graphics.DrawLine(PointA, PointB, LineSize, LineColor);
        }
    }
}