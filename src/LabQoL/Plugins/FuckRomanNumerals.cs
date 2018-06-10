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
                    switch (i)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            color = Color.White;
                            text = (i+1).ToString();
                            break;
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            color = Color.Yellow;
                            text = (i + 1).ToString();
                            break;
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                            color = Color.Red;
                            text = (i + 1).ToString();
                            break;
                        case 16:
                            color = Color.Orange;
                            text = "U";
                            break;
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