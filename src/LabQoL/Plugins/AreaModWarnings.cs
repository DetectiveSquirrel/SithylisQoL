using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;
using ExileCore;
using ExileCore.Shared;
using ExileCore.Shared.Enums;
using ImGuiNET;
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
                Process.Start(DirectoryFullName + "\\How To Get Map Mods.txt");
            }
            if (ImGui.Button("Open: Area Mod List"))
            {
                Process.Start(DirectoryFullName + "\\Area Mods.txt");
            }
        }

        public void AreaModWarningsInit()
        {
            AreaModFile = $@"{DirectoryFullName}\AreaWarningMods.txt";

            // Make file if not present
            if (!File.Exists(AreaModFile))
            {
                File.Create(AreaModFile);
                Thread.Sleep(250);
            }

            AreaModWarningList = File.ReadAllLines(AreaModFile).ToList();
            UpdateAreaModList = new Coroutine(ReadAreaModFile(), this, "Update Area Mod Warning List");
            Core.ParallelRunner.Run(UpdateAreaModList);

        }
        private IEnumerator ReadAreaModFile()
        {
            AreaModWarningList = File.ReadAllLines(AreaModFile).ToList();
            yield return new WaitFunction(() => GameController.Game.IsLoading);
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
                Vector4 TextColor;

                unsafe
                {
                    var styleColorVec4 = ImGui.GetStyleColorVec4(ImGuiCol.Text);
                    var colorValue = new Vector4(styleColorVec4->X, styleColorVec4->Y, styleColorVec4->Z, styleColorVec4->W);
                    TextColor = colorValue;
                }

                var LongestText = ImGui.CalcTextSize(BadMods_Present.OrderByDescending(s => s.Length).First());
                var windowPadding = ImGui.GetStyle().WindowPadding;
                var ItemSpacing = ImGui.GetStyle().ItemSpacing;

                if (Settings.AreaModWarningOverrideColors)
                {
                    ImGui.PushStyleColor(ImGuiCol.WindowBg, ToImVector4(Settings.AreaModWarningBackground.ToVector4()));
                }

                ImGui.BeginPopupModal("RandomFeatures_BadAreaModWarning", ref refBool,
                    Settings.AreaModWarningLocked
                            ? ImGuiWindowFlags.NoCollapse
                            | ImGuiWindowFlags.NoScrollbar
                            | ImGuiWindowFlags.NoMove
                            | ImGuiWindowFlags.NoResize
                            | ImGuiWindowFlags.NoInputs
                            | ImGuiWindowFlags.NoBringToFrontOnFocus
                            | ImGuiWindowFlags.NoTitleBar
                            | ImGuiWindowFlags.NoFocusOnAppearing
                            : ImGuiWindowFlags.None | ImGuiWindowFlags.NoTitleBar);

                ImGui.SetWindowSize(new Vector2(
                        LongestText.X + windowPadding.X * 2,
                        LongestText.Y * (BadMods_Present.Count + 1) + windowPadding.Y * 2 + (ItemSpacing.Y * BadMods_Present.Count)
                ));

                if (Settings.AreaModWarningOverrideColors)
                {
                    ImGui.PopStyleColor();
                }

                ImGui.TextColored(Settings.AreaModWarningOverrideColors ? ToImVector4(Settings.AreaModWarningTitle.ToVector4()) : TextColor, "Mods Found");

                foreach (var BadMod in BadMods_Present)
                {
                    ImGui.TextColored(Settings.AreaModWarningOverrideColors ? ToImVector4(Settings.AreaModWarningBodyText.ToVector4()) : TextColor, BadMod);
                }

                ImGui.EndPopup();
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