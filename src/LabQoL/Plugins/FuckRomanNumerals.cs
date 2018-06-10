using System.Collections.Generic;
using ImGuiNET;
using PoeHUD.Models.Enums;
using PoeHUD.Poe;
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

                List<Element> MapElements = GameController.Game.IngameState.ServerData.StashPanel.VisibleStash.InventoryUiElement.Parent.Children;
                Element UiHover = GameController.Game.IngameState.UIHover;


                int Row = 0;
                int ElementId = 0;
                for (int I = 0; I < 17; I++)
                {
                    if (I == 9)
                    {
                        Row = 1;
                        ElementId = 0;
                    }

                    Element Element = MapElements[Row].Children[ElementId].Children[2];
                    ElementId++;
                    RectangleF BackgroundPos = RomanPosition(Element);
                    Vector2 TextPos = new Vector2(BackgroundPos.Center.X, BackgroundPos.Center.Y);
                    Color TextColor = Color.White;
                    Color BackgroundColor = Color.Black;
                    const float IntersectWiggleRoom = 7;
                    string Text = "";

                    // Whites (Tier 1-5)
                    if (I == 0 || I == 1 || I == 2 || I == 3 || I == 4)
                    {
                        TextColor = Color.White;
                        Text = (I + 1).ToString();
                    }
                    // Yellows (Tier 6-10)
                    else if (I == 5 || I == 6 || I == 7 || I == 8 || I == 9)
                    {
                        TextColor = Color.Yellow;
                        Text = (I + 1).ToString();
                    }
                    // Reds (Tier 11-16)
                    else if (I == 10 || I == 11 || I == 12 || I == 13 || I == 14 || I == 15)
                    {
                        TextColor = Color.Red;
                        Text = (I + 1).ToString();
                    }
                    // Uniques
                    else if (I == 16)
                    {
                        TextColor = Color.Orange;
                        Text = "U";
                    }

                    BackgroundPos.Inflate(-IntersectWiggleRoom, -IntersectWiggleRoom);
                    if (UiHover.Tooltip.GetClientRect().Intersects(BackgroundPos))
                        continue;
                    BackgroundPos.Inflate(+IntersectWiggleRoom, +IntersectWiggleRoom);

                    // Generate a nice font size depending on what our settings are
                    int FontStartSize = Settings.FrnFontSize;
                    double Percent = (double) Settings.FrnPercentOfBox / 100;
                    int TextSize = (int) (FontStartSize * Percent);

                    Graphics.DrawBox(BackgroundPos, BackgroundColor);
                    Graphics.DrawText(Text, TextSize, TextPos, TextColor, FontDrawFlags.VerticalCenter | FontDrawFlags.Center);
                }
            }
            catch
            {
                // ignored
            }
        }

        private RectangleF RomanPosition(Element Element)
        {
            float ElementWidth = Element.GetClientRect().Width;
            double Percent = (double) Settings.FrnPercentOfBox / 100;
            double Size = ElementWidth * Percent;

            return new RectangleF(Element.GetClientRect().Center.X - (int) Size / 2, Element.GetClientRect().Center.Y - (int) Size / 2, (int) Size, (int) Size);
        }
    }
}