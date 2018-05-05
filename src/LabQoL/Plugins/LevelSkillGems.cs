using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ImGuiNET;
using PoeHUD.Poe;
using PoeHUD.Poe.Elements;
using Random_Features.Libs;
using SharpDX;

namespace Random_Features
{
    partial class RandomFeatures
    {
        private readonly Stopwatch GemClickTimer = Stopwatch.StartNew();

        public void LevelSkillGemsMenu()
        {
            Settings.LevelSkillGems.Value = ImGuiExtension.Checkbox("Active Skill Gem Leveling Functions", Settings.LevelSkillGems.Value);
            ImGui.Spacing();
            Settings.LevelSkillGemsHotkey = PoeHUD.Hud.UI.ImGuiExtension.HotkeySelector("Level Up Skill Gems", Settings.LevelSkillGemsHotkey);
            ImGui.Separator();
            ImGui.Spacing();

            if (ImGui.CollapsingHeader("Gem Leveling Rules", "##DOntLevelIf", true, true))
            {
                ImGui.Columns(3, "Columns", true);
                ImGui.SetColumnWidth(0, 30f);
                ImGui.Text(""); ImGui.NextColumn();
                ImGui.Text("Full Gem Name"); ImGui.NextColumn();
                ImGui.Text("Maximum Level"); ImGui.NextColumn();
                ImGui.Separator();

                for (int i = 0; i < Settings.SkillGemStopList.Count; i++)
                {
                    GemLevelRule gemRule = Settings.SkillGemStopList[i];
                    if (ImGui.Button($"X##REMOVERULE{i}"))
                    {
                        Settings.SkillGemStopList.Remove(gemRule);
                    }
                    ImGui.NextColumn();
                    ImGui.PushItemWidth(ImGui.GetContentRegionAvailableWidth());
                    gemRule.GemName = PoeHUD.Hud.UI.ImGuiExtension.InputText($"##GN{i}", gemRule.GemName, 35, InputTextFlags.Default);
                    ImGui.PopItemWidth();
                    //ImGui.SameLine();
                    ImGui.NextColumn();
                    ImGui.PushItemWidth(ImGui.GetContentRegionAvailableWidth());
                    gemRule.MaxLevel = PoeHUD.Hud.UI.ImGuiExtension.IntSlider($"##ML{i}", gemRule.MaxLevel, 1, 20); ImGui.NextColumn();
                    ImGui.PopItemWidth();
                }

                ImGui.Separator();
                ImGui.Columns(1, "", false);

                if (ImGui.Button("Add New"))
                {
                    Settings.SkillGemStopList.Add(new GemLevelRule {GemName = "Cast when Damage Taken Support", MaxLevel = 1});
                }
            }

            Settings.SkillGemStopList = Settings.SkillGemStopList;
        }

        public void LevelUpGems()
        {
            if (!Keyboard.IsKeyDown((int) Settings.LevelSkillGemsHotkey.Value)) return;
            if (GemClickTimer.ElapsedMilliseconds < 150) return;
            GemClickTimer.Restart();
            Element SkillGemLevelUps = GameController.Game.IngameState.UIRoot.GetChildAtIndex(1)
                                                     .GetChildAtIndex(3)
                                                     .GetChildAtIndex(1)
                                                     .GetChildAtIndex(0);
            if (SkillGemLevelUps == null || !SkillGemLevelUps.IsVisible) return;
            var MosuePos = Mouse.GetCursorPositionVector();
            foreach (Element element in SkillGemLevelUps.Children)
            {
                RectangleF skillGemButton = element.GetChildAtIndex(1).GetClientRect();
                string skillGemText = element.GetChildAtIndex(3).AsObject<EntityLabel>().Text;
                if (element.GetChildAtIndex(2).IsVisibleLocal) continue;

                if (element.GetChildAtIndex(0).Tooltip.ChildCount == 0)
                {

                    var ToolTipHover = skillGemButton;
                    ToolTipHover.X += ToolTipHover.Width;
                    Mouse.SetCurosPosToCenterOfRec(ToolTipHover, GameController.Window.GetWindowRectangle());
                    while (element.GetChildAtIndex(0).Tooltip.ChildCount == 0)
                    {
                        Thread.Sleep(25);
                        Mouse.SetCurosPosToCenterOfRec(ToolTipHover, GameController.Window.GetWindowRectangle());
                    }
                }
                if (element.GetChildAtIndex(0).Tooltip.ChildCount != 0)
                {
                    SkillGemTooltipWrapper SkillGemToolTipInfo = new SkillGemTooltipWrapper(element.GetChildAtIndex(0).Tooltip);
                    if (skillGemText?.ToLower() == "click to level up" && PassedGemRule(SkillGemToolTipInfo))
                    {
                        Mouse.SetCurosPosToCenterOfRec(skillGemButton, GameController.Window.GetWindowRectangle());
                        Mouse.LeftClick(5, 10);
                        //Mouse.SetCurosPosToCenterOfRec(GameController.Window.GetWindowRectangle(), GameController.Window.GetWindowRectangle());
                        GemClickTimer.Restart();
                        break;
                    }
                }
                Thread.Sleep(25);
            }

            //Mouse.SetCursorPos(MosuePos);
        }

        public bool PassedGemRule(SkillGemTooltipWrapper gem)
        {
            foreach (GemLevelRule rule in Settings.SkillGemStopList)
            {
                if (!string.Equals(gem.Name, rule.GemName, StringComparison.CurrentCultureIgnoreCase)) continue;
                if (gem.Level >= rule.MaxLevel) return false;
            }

            return true;
        }

        public void LevelSkillGems()
        {
            if (!Settings.LevelSkillGems) return;
            try
            {
                LevelUpGems();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
            }
        }

        public class SkillGemTooltipWrapper
        {
            public SkillGemTooltipWrapper(Element SkillGemTooltip)
            {
                Name = SkillGemTooltip.GetChildAtIndex(0).GetChildAtIndex(3).AsObject<EntityLabel>().Text;
                string LevelText = SkillGemTooltip.GetChildAtIndex(1).GetChildAtIndex(1).AsObject<EntityLabel>().Text;
                Regex LevelStringExtraction = new Regex("\\{(.*?)\\}", RegexOptions.IgnoreCase);
                Match match = LevelStringExtraction.Match(LevelText);
                if (match.Success) Level = int.Parse(match.Groups[1].Value);
            }

            public string Name { get; set; }
            public int Level { get; set; }

            public override string ToString() => $"Name: {Name}, Level: {Level}";
        }

        public class GemLevelRule
        {
            public string GemName { get; set; }
            public int MaxLevel { get; set; }
        }
    }
}