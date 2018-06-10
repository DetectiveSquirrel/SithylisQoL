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
            catch (Exception)
            {
                //Console.WriteLine(e);
            }
        }

        public void DrawBadMods()
        {
            var AreaMods = GameController.Game.IngameState.Data.MapStats;
            var BadMods_Present = new List<string>();
            foreach (var areaMod in AreaMods)
            {
                if (IsBadMod(areaMod.Key))
                {
                    string modString = "";
                    modString = GetEnumDescription(areaMod.Key).Replace("Map ", "");

                    if (modString.Contains("+%"))
                    {
                        modString = modString.Replace("+%", $"+{areaMod.Value}%");
                    }
                    else if (modString.Contains(" X%"))
                    {
                        modString = modString.Replace(" X%", $" {areaMod.Value}%");
                    }
                    else if (modString.Contains(" X"))
                    {
                        modString = modString.Replace(" X", $" {areaMod.Value}");
                    }
                    else if (modString.Contains(" +"))
                    {
                        modString = modString.Replace(" +", $" +{areaMod.Value}");
                    }
                    else if (modString.Contains("%"))
                    {
                        modString = modString.Replace("%", $"{areaMod.Value}%");
                    }

                    modString = modString.Replace("%", "%%");

                    BadMods_Present.Add("(" + areaMod.Value + ") " + modString);
                }
            }

            if (BadMods_Present.Count > 0)
            {
                var refBool = true;
                float menuOpacity = ImGui.GetStyle().GetColor(ColorTarget.ChildBg).W;
                var TextColor = ImGui.GetStyle().GetColor(ColorTarget.Text);
                var LongestText = ImGui.GetTextSize(BadMods_Present.OrderByDescending(s => s.Length).First());
                var windowPadding = ImGui.GetStyle().WindowPadding;
                var ItemSpacing = ImGui.GetStyle().ItemSpacing;

                if (Settings.AreaModWarningOverrideColors)
                {
                    ImGui.PushStyleColor(ColorTarget.WindowBg, ToImVector4(Settings.AreaModWarningBackground.ToVector4()));
                    menuOpacity = ImGui.GetStyle().GetColor(ColorTarget.WindowBg).W;
                }

                ImGui.BeginWindow("RandomFeatures_BadAreaModWarning", ref refBool, new Vector2(200, 150), menuOpacity, Settings.AreaModWarningLocked ? WindowFlags.NoCollapse | WindowFlags.NoScrollbar | WindowFlags.NoMove | WindowFlags.NoResize | WindowFlags.NoInputs | WindowFlags.NoBringToFrontOnFocus | WindowFlags.NoTitleBar | WindowFlags.NoFocusOnAppearing : WindowFlags.Default | WindowFlags.NoTitleBar | WindowFlags.ResizeFromAnySide);

                ImGui.SetWindowSize(new Vector2(
                        LongestText.X + windowPadding.X * 2,
                        LongestText.Y * (BadMods_Present.Count + 1) + windowPadding.Y * 2 + (ItemSpacing.Y * BadMods_Present.Count)
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

        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}