using ImGuiNET;
using Random_Features.Libs;
using SharpDX;
using System.Collections.Generic;

namespace Random_Features
{
    partial class RandomFeatures
    {
        private Vector2 _clickWindowOffset;

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
            var windows = GameController.Game.IngameState.IngameUi;
            if (windows.TreePanel.IsVisible)
                return;
            if (windows.AtlasPanel.IsVisible)
                return;
            if (windows.OpenLeftPanel.IsVisible)
                return;
            if (windows.OpenRightPanel.IsVisible)
                return;
            var cursorPositionVector = Mouse.GetCursorPositionVector();
            var windowRectangle = GameController.Window.GetWindowRectangleReal();
            _clickWindowOffset = GameController.Window.GetWindowRectangle().TopLeft;
            cursorPositionVector -= _clickWindowOffset;

            // need to call use -170 in Z axis for player.pos.translate as this is whats used in poecore for hp bar.
            //i have no fucking clue why this is. If you do not follow this rule you will have a jumpy hp bar
            var playerToScreen = LocalPlayer.PlayerToScreen;
            Vector2 finalPointA;
            switch (Settings.WmcLineType)
            {
                case 0:
                    finalPointA = Vector2OffsetCalculations(new Vector2(playerToScreen.X, windowRectangle.Height / 2));
                    DrawLineToPosWithLength(finalPointA, cursorPositionVector, Settings.WmcLineLength, Settings.WmcLineSize, Settings.WmcLineColor);
                    break;
                case 1:
                    finalPointA = Vector2OffsetCalculations(new Vector2(playerToScreen.X, windowRectangle.Height / 2));
                    DrawLineToPos(finalPointA, cursorPositionVector, Settings.WmcLineSize, Settings.WmcLineColor);
                    break;
                case 2:
                    finalPointA = Vector2OffsetCalculations(new Vector2(playerToScreen.X, windowRectangle.Height / 2));
                    DrawLineToPosWithLength(finalPointA, cursorPositionVector, (int) windowRectangle.Width, Settings.WmcLineSize, Settings.WmcLineColor);
                    break;
            }
        }

        public Vector2 Vector2OffsetCalculations(Vector2 information)
        {
            var finalVectorCalculation = information;
            finalVectorCalculation.X = Settings.WmcPlayerOffsetXNegitive 
                                               ? finalVectorCalculation.X - Settings.WmcPlayerOffsetX 
                                               : finalVectorCalculation.X + Settings.WmcPlayerOffsetX;
            finalVectorCalculation.Y = Settings.WmcPlayerOffsetYNegitive 
                                               ? finalVectorCalculation.Y - Settings.WmcPlayerOffsetY 
                                               : finalVectorCalculation.Y + Settings.WmcPlayerOffsetY;
            return finalVectorCalculation;
        }

        public void DrawLineToPosWithLength(Vector2 pointA, Vector2 pointB, int lineLength, int lineSize, Color lineColor)
        {
            var direction = pointB - pointA;
            direction.Normalize();
            var pointC = pointA + direction * lineLength;
            Graphics.DrawLine(pointA, pointC, lineSize, lineColor);
        }

        public void DrawLineToPos(Vector2 pointA, Vector2 pointB, int lineSize, Color lineColor)
        {
            Graphics.DrawLine(pointA, pointB, lineSize, lineColor);
        }
    }
}