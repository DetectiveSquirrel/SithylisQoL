using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using ImGuiNET;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Models.Enums;
using Random_Features.Libs;
using Vector2 = System.Numerics.Vector2;

namespace Random_Features
{
    partial class RandomFeatures
    {
        public List<string> AreaModWarningList = new List<string>();
        public string AreaModFile { get; set; }
        public Coroutine UpdateAreaModList;

        public void AreaModWarningsMenu()
        {
            Settings.AreaModWarnings.Value = ImGuiExtension.Checkbox("Shows Area Mod Warnings", Settings.AreaModWarnings.Value);
            ImGui.Spacing();
            ImGui.Spacing();
            Settings.AreaModWarningLocked.Value = ImGuiExtension.Checkbox("Lock Mod Warning Box In Place", Settings.AreaModWarningLocked.Value);
            Settings.AreaModWarningOverrideColors.Value = ImGuiExtension.Checkbox("Override ImGui Colors", Settings.AreaModWarningOverrideColors.Value);
            Settings.AreaModWarningTitle = ImGuiExtension.ColorPicker("Title Color", Settings.AreaModWarningTitle);
            Settings.AreaModWarningBodyText = ImGuiExtension.ColorPicker("Bad Mod Colors", Settings.AreaModWarningBodyText);
            Settings.AreaModWarningBackground = ImGuiExtension.ColorPicker("Background", Settings.AreaModWarningBackground);

            if (ImGui.Button("Open: AreaWarningMods.txt"))
            {
                Process.Start(AreaModFile);
            }
            ImGui.SameLine();
            if (ImGui.Button("Open: How To Get Area Mod Strings"))
            {
                Process.Start(PluginDirectory + "\\How To Get Map Mods.txt");
            }
            if (ImGui.Button("Open: Area Mod List"))
            {
                Process.Start(PluginDirectory + "\\Area Mods.txt");
            }
        }

        public void AreaModWarningsInit()
        {
            AreaModFile = $@"{PluginDirectory}\AreaWarningMods.txt";

            // Make file if not present
            if (!File.Exists(AreaModFile))
            {
                File.Create(AreaModFile);
                Thread.Sleep(250);
            }

            AreaModWarningList = File.ReadAllLines(AreaModFile).ToList();
            UpdateAreaModList = new Coroutine(() =>
            {
                AreaModWarningList = File.ReadAllLines(AreaModFile).ToList();
                //LogMessage("Updated Area Mod List", 1);
            }, new WaitTime(1000), nameof(Random_Features), "Update Area Mod Warning List").Run();
        }

        public void AreaModWarnings()
        {
            if (!Settings.AreaModWarnings)
            {
                return;
            }

            try
            {
                DrawBadMods();
            }
            catch (Exception E)
            {
                //Console.WriteLine(e);
            }
        }

        public void DrawBadMods()
        {
            var AreaMods = GameController.Game.IngameState.Data.MapStats;
            var BadMods_Present = new List<string>();
            foreach (var AreaMod in AreaMods)
            {
                if (IsBadMod(AreaMod.Key))
                {
                    string ModString = "";
                    ModString = GetEnumDescription(AreaMod.Key).Replace("Map ", "");

                    if (ModString.Contains("+%"))
                    {
                        ModString = ModString.Replace("+%", $"+{AreaMod.Value}%");
                    }
                    else if (ModString.Contains(" X%"))
                    {
                        ModString = ModString.Replace(" X%", $" {AreaMod.Value}%");
                    }
                    else if (ModString.Contains(" X"))
                    {
                        ModString = ModString.Replace(" X", $" {AreaMod.Value}");
                    }
                    else if (ModString.Contains(" +"))
                    {
                        ModString = ModString.Replace(" +", $" +{AreaMod.Value}");
                    }
                    else if (ModString.Contains("%"))
                    {
                        ModString = ModString.Replace("%", $"{AreaMod.Value}%");
                    }

                    ModString = ModString.Replace("%", "%%");

                    BadMods_Present.Add("(" + AreaMod.Value + ") " + ModString);
                }
            }

            if (BadMods_Present.Count > 0)
            {
                var RefBool = true;
                float MenuOpacity = ImGui.GetStyle().GetColor(ColorTarget.ChildBg).W;
                var TextColor = ImGui.GetStyle().GetColor(ColorTarget.Text);
                var LongestText = ImGui.GetTextSize(BadMods_Present.OrderByDescending(S => S.Length).First());
                var WindowPadding = ImGui.GetStyle().WindowPadding;
                var ItemSpacing = ImGui.GetStyle().ItemSpacing;

                if (Settings.AreaModWarningOverrideColors)
                {
                    ImGui.PushStyleColor(ColorTarget.WindowBg, ToImVector4(Settings.AreaModWarningBackground.ToVector4()));
                    MenuOpacity = ImGui.GetStyle().GetColor(ColorTarget.WindowBg).W;
                }

                ImGui.BeginWindow("RandomFeatures_BadAreaModWarning", ref RefBool, new Vector2(200, 150), MenuOpacity, Settings.AreaModWarningLocked ? WindowFlags.NoCollapse | WindowFlags.NoScrollbar | WindowFlags.NoMove | WindowFlags.NoResize | WindowFlags.NoInputs | WindowFlags.NoBringToFrontOnFocus | WindowFlags.NoTitleBar | WindowFlags.NoFocusOnAppearing : WindowFlags.Default | WindowFlags.NoTitleBar | WindowFlags.ResizeFromAnySide);

                ImGui.SetWindowSize(new Vector2(
                        LongestText.X + WindowPadding.X * 2,
                        LongestText.Y * (BadMods_Present.Count + 1) + WindowPadding.Y * 2 + (ItemSpacing.Y * BadMods_Present.Count)
                ));
                if (Settings.AreaModWarningOverrideColors)
                {
                    ImGui.PopStyleColor();
                }

                ImGui.Text("Mods Found", Settings.AreaModWarningOverrideColors ? ToImVector4(Settings.AreaModWarningTitle.ToVector4()) : TextColor);
                foreach (var BadMod in BadMods_Present)
                {
                    ImGui.Text(BadMod, Settings.AreaModWarningOverrideColors ? ToImVector4(Settings.AreaModWarningBodyText.ToVector4()) : TextColor);
                }
                ImGui.EndWindow();
            }
        }

        public bool IsBadMod(GameStat AreaMod)
        {
            foreach (var BadMod in AreaModWarningList)
            {
                if (string.Equals(BadMod, AreaMod.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetEnumDescription(Enum Value)
        {
            var Fi = Value.GetType().GetField(Value.ToString());
            var Attributes = (DescriptionAttribute[]) Fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return Attributes.Length > 0 ? Attributes[0].Description : Value.ToString();
        }
    }
}