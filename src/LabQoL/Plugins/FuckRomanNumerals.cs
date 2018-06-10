using System;
using System.Collections.Generic;
using ImGuiNET;
using PoeHUD.Hud.UI;
using PoeHUD.Models.Enums;
using PoeHUD.Poe;
using SharpDX;
using SharpDX.Direct3D9;
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
            return GameController.Game.IngameState.ServerData.StashPanel.IsVisible && GameController.Game.IngameState.ServerData.StashPanel.VisibleStash.InvType == InventoryType.MapStash;
        }

        private void DrawNumbersOverRomanNumerals()
        {
            try
            {
                if (!IsMapTabOpen()) return;

                List<Element> mapElements = GameController.Game.IngameState.ServerData.StashPanel.VisibleStash.InventoryUiElement.Parent.Children;
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
                    RectangleF backgroundPos = RomanPosition(element);
                    Vector2 textPos = new Vector2(backgroundPos.Center.X, backgroundPos.Center.Y);
                    Color color = Color.White;


                    string text = "";

                    if (uiHover.Tooltip.GetClientRect().Intersects(backgroundPos))
                        continue;

                    // Whites
                    if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4)
                    {
                        color = Color.White;
                        text = i.ToString();
                    }
                    // Yellows
                    else if (i == 5 || i == 6 || i == 7 || i == 8 || i == 9)
                    {
                        color = Color.Yellow;
                        text = i.ToString();
                    }
                    // Reds
                    else if (i == 10 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15)
                    {
                        color = Color.Red;
                        text = i.ToString();
                    }
                    // Uniques
                    else if (i == 16)
                    {
                        color = Color.Orange;
                        text = "U";
                    }

                    // Generate a nice font size depending on what our settings are
                    int FontStartSize = Settings.FrnFontSize;
                    double Percent = (double) Settings.FrnPercentOfBox / 100;
                    int TextSize = (int) (FontStartSize * Percent);

                    Graphics.DrawBox(backgroundPos, Color.Black);
                    Graphics.DrawText(text, TextSize, textPos, color, FontDrawFlags.VerticalCenter | FontDrawFlags.Center);
                    elementID++;
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