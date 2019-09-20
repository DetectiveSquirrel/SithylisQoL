using System.Collections.Generic;
using ExileCore.PoEMemory;
using ExileCore.Shared.Enums;
using ImGuiNET;
using SharpDX;
using ImGuiExtension = Random_Features.Libs.ImGuiExtension;

namespace Random_Features
{
    partial class RandomFeatures
    {
        private void FuckRomanNumeralsMenu()
        {
            Settings.FrnMain.Value = ImGuiExtension.Checkbox("Enable##FRNToggle", Settings.FrnMain);
            ImGui.Spacing();
            Settings.FrnPercentOfBox.Value = ImGuiExtension.IntSlider("Size % of 'Map Tier' Button's Size", Settings.FrnPercentOfBox);
            Settings.FrnFontSize.Value = ImGuiExtension.IntSlider("Font Size (Somewhat awkward to make it always fit into the box)", Settings.FrnFontSize);
        }

        private void FuckRomanNumerals()
        {
            if (!Settings.FrnMain) return;
            DrawNumbersOverRomanNumerals();
        }

        public bool IsMapTabOpen()
        {
            return GameController.Game.IngameState.IngameUi.StashElement.IsVisible && GameController.Game.IngameState.IngameUi.StashElement.VisibleStash.InvType == InventoryType.MapStash;
            //return false;
        }

        private void DrawNumbersOverRomanNumerals()
        {
            try
            {
                if (!IsMapTabOpen()) return;

                IList<Element> mapElements = GameController.Game.IngameState.IngameUi.StashElement.VisibleStash.InventoryUIElement.Children;
                //List<Element> mapElements = new List<Element>();
                Element uiHover = GameController.Game.IngameState.UIHover;


                int row = 0;
                int elementID = 0;
                for (int i = 0; i < 17; i++)
                {
                    if (i == 9)
                    {
                        row = 1;
                        elementID = 0;
                    }

                    Element element = mapElements[row].Children[elementID].Children[2];
                    elementID++;
                    RectangleF backgroundPos = RomanPosition(element);
                    Vector2 textPos = new Vector2(backgroundPos.Center.X, backgroundPos.Center.Y);
                    Color textColor = Color.White;
                    Color backgroundColor = Color.Black;
                    float intersectWiggleRoom = 7;
                    string text = "";

                    // Whites (Tier 1-5)
                    if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4)
                    {
                        textColor = Color.White;
                        text = (i + 1).ToString();
                    }
                    // Yellows (Tier 6-10)
                    else if (i == 5 || i == 6 || i == 7 || i == 8 || i == 9)
                    {
                        textColor = Color.Yellow;
                        text = (i + 1).ToString();
                    }
                    // Reds (Tier 11-16)
                    else if (i == 10 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15)
                    {
                        textColor = Color.Red;
                        text = (i + 1).ToString();
                    }
                    // Uniques
                    else if (i == 16)
                    {
                        textColor = Color.Orange;
                        text = "U";
                    }

                    backgroundPos.Inflate(-intersectWiggleRoom, -intersectWiggleRoom);
                    if (uiHover.Tooltip.GetClientRect().Intersects(backgroundPos))
                    {
                        continue;
                    }
                    backgroundPos.Inflate(+intersectWiggleRoom, +intersectWiggleRoom);

                    // Generate a nice font size depending on what our settings are
                    int FontStartSize = Settings.FrnFontSize;
                    double Percent = (double) Settings.FrnPercentOfBox / 100;
                    int TextSize = (int) (FontStartSize * Percent);

                    Graphics.DrawBox(backgroundPos, backgroundColor);
                    Graphics.DrawText(text, textPos, textColor, TextSize, FontAlign.Center);
                }
            }
            catch
            {
                // ignored
            }
        }

        private RectangleF RomanPosition(Element element)
        {
            float elementWidth = element.GetClientRect().Width;
            double percent = (double) Settings.FrnPercentOfBox / 100;
            double size = elementWidth * percent;

            return new RectangleF(element.GetClientRect().Center.X - (int) size / 2, element.GetClientRect().Center.Y - (int) size / 2, (int) size, (int) size);
        }
    }
}