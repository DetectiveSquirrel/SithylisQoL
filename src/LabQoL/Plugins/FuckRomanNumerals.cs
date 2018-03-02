using ImGuiNET;
using PoeHUD.Models.Enums;
using Random_Features.Libs;
using SharpDX;
using SharpDX.Direct3D9;

namespace Random_Features
{
    partial class RandomFeatures
    {
        private void FuckRomanNumeralsMenu()
        {
            Settings.FrnMain.Value = ImGuiExtension.Checkbox("Enable##FRNToggle", Settings.FrnMain);
            ImGui.Text("Box Options");
            Settings.FrnBackgroundBoxExtraWidth.Value = ImGuiExtension.IntSlider("Background Box Extra Width", Settings.FrnBackgroundBoxExtraWidth);
            Settings.FrnBackgroundBoxWidth.Value = ImGuiExtension.IntSlider("Background Box Width", Settings.FrnBackgroundBoxWidth);
            Settings.FrnBackgroundBoxHeight.Value = ImGuiExtension.IntSlider("Background Box Height", Settings.FrnBackgroundBoxHeight);
            Settings.FrnNextBoxOffset.Value = ImGuiExtension.IntSlider("Next Box Offset", Settings.FrnNextBoxOffset);
            ImGui.Spacing();
            ImGui.Text("First Row");
            Settings.FrnFirstTierRowX.Value = ImGuiExtension.IntSlider("First Tier Row X", Settings.FrnFirstTierRowX);
            Settings.FrnFirstTierRowY.Value = ImGuiExtension.IntSlider("First Tier Row Y", Settings.FrnFirstTierRowY);
            ImGui.Spacing();
            ImGui.Text("Second Row");
            Settings.FrnSecondTierRowX.Value = ImGuiExtension.IntSlider("Second Tier Row X", Settings.FrnSecondTierRowX);
            Settings.FrnSecondTierRowY.Value = ImGuiExtension.IntSlider("Second Tier Row Y", Settings.FrnSecondTierRowY);
            Settings.FrnFontSize.Value = ImGuiExtension.IntSlider("Font Size", Settings.FrnFontSize);
        }

        private void FuckRomanNumerals()
        {
            if (!Settings.FrnMain) return;
            DrawNumbersOverRomanNumerals();
        }

        public bool IsMapTabOpen() => GameController.Game.IngameState.ServerData.StashPanel.IsVisible && GameController.Game.IngameState.ServerData.StashPanel.VisibleStash.InvType == InventoryType.MapStash;

        private void DrawNumbersOverRomanNumerals()
        {
            try
            {
                if (!IsMapTabOpen()) return;
                const int topRowTierCount = 9;
                const int bottomRowTierCount = 7;
                var backgroundBoxExtraWidth = Settings.FrnBackgroundBoxExtraWidth;
                var backgroundBoxWidth = Settings.FrnBackgroundBoxWidth + Settings.FrnBackgroundBoxExtraWidth * 2;
                var backgroundBoxHeight = Settings.FrnBackgroundBoxHeight;
                var nextBoxOffset = Settings.FrnNextBoxOffset;
                var firstTierRowX = Settings.FrnFirstTierRowX;
                var firstTierRowY = Settings.FrnFirstTierRowY;
                var secondTierRowX = Settings.FrnSecondTierRowX;
                var secondTierRowY = Settings.FrnSecondTierRowY;
                var fontSize = Settings.FrnFontSize;
                var firstTabRow = new RectangleF(firstTierRowX - backgroundBoxExtraWidth, firstTierRowY, backgroundBoxWidth, backgroundBoxHeight);
                var firstTabText = new Vector2(firstTabRow.X + firstTabRow.Width / 2, firstTabRow.Y);
                var secondTabRow = new RectangleF(secondTierRowX - backgroundBoxExtraWidth, secondTierRowY, backgroundBoxWidth, backgroundBoxHeight);
                var secondTabText = new Vector2(secondTabRow.X + secondTabRow.Width / 2, secondTabRow.Y);
                for (var i = 0; i < topRowTierCount; i++)
                {
                    var newBox = new RectangleF(firstTabRow.X + i * nextBoxOffset, firstTabRow.Y, backgroundBoxWidth, backgroundBoxHeight);
                    var newText = new Vector2(firstTabText.X + i * nextBoxOffset, firstTabText.Y + 1);
                    Graphics.DrawBox(newBox, Color.Black);
                    Graphics.DrawText((i + 1).ToString(), fontSize, newText, i + 1 < 6 ? Color.White : Color.Yellow, FontDrawFlags.Center);
                }

                for (var i = 0; i < bottomRowTierCount; i++)
                {
                    var newBox = new RectangleF(secondTabRow.X + i * nextBoxOffset, secondTabRow.Y, backgroundBoxWidth, backgroundBoxHeight);
                    var newText = new Vector2(secondTabText.X + i * nextBoxOffset, secondTabText.Y + 1);
                    Graphics.DrawBox(newBox, Color.Black);
                    Graphics.DrawText((i + topRowTierCount + 1).ToString(), fontSize, newText, i + topRowTierCount + 1 < 11 ? Color.Yellow : Color.Red, FontDrawFlags.Center);
                }

                var uBox = new RectangleF(secondTabRow.X + bottomRowTierCount * nextBoxOffset, secondTabRow.Y, backgroundBoxWidth, backgroundBoxHeight);
                var uText = new Vector2(secondTabText.X + bottomRowTierCount * nextBoxOffset, secondTabText.Y + 1);
                Graphics.DrawBox(uBox, Color.Black);
                Graphics.DrawText("U", fontSize, uText, Color.Orange, FontDrawFlags.Center);
            }
            catch
            {
                // ignored
            }
        }
    }
}