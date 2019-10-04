using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using ExileCore.PoEMemory;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.Elements;
using ExileCore.Shared.Nodes;
using ImGuiNET;
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
            Settings.LevelSkillGemsHotkey = ImGuiExtension.HotkeySelector("Level Up Skill Gems: " + Settings.LevelSkillGemsHotkey.Value.ToString(), Settings.LevelSkillGemsHotkey);
            ImGui.Separator();
            Settings.LevlSkillGemIsLeftClick.Value = ImGuiExtension.Checkbox("Left click to level Gems Up, Unticked if Right Click", Settings.LevlSkillGemIsLeftClick);
            ImGui.Separator();
            ImGui.Spacing();

            bool WeExistInRuleSet = false;
            var PlayerName = GameController.Player.GetComponent<Player>().PlayerName;

            if (Settings.SkillGemStopList == null)
            {
                Settings.SkillGemStopList.Add(new Person()
                {
                        Character = PlayerName,
                        Rules = new List<GemLevelRule>()
                });
            }
            if (Settings.SkillGemStopList.Any(t => t.Character == PlayerName))
            {
                WeExistInRuleSet = true;
            }

            if (!WeExistInRuleSet)
            {
                Settings.SkillGemStopList.Add(new Person
                {
                        Character = PlayerName,
                        Rules = new List<GemLevelRule>()
                });
            }

            //not sure about "ref" usage
            bool refBool = true;
            if (ImGui.CollapsingHeader($"Gem Leveling Rules For - {PlayerName}", ref refBool))
            {
                ImGui.Text("How Does It Work");
                ImGuiExtension.ToolTip("All gems are leveled up UNLESS the rules below catch the gem\n"
                                                   + "Example, you dont want CWDT to be leveled past 3\n"
                                                   + "Add \"Cast When Damage Taken Support\" with max level of 3\n"
                                                   + "When the gem is level 3 it will right click to hide that gem and it wont be leveled any further");

                var i = -1;
                for (int index = 0; index < Settings.SkillGemStopList.Count; index++)
                {
                    Person PersonCheck = Settings.SkillGemStopList[index];
                    if (PersonCheck.Character == GameController.Player.GetComponent<Player>().PlayerName)
                    {
                        i = index;
                    }
                }

                var RulesToRemove = new List<int>();
                
                ImGui.Separator();
                ImGui.Columns(3, "Columns", true);
                ImGui.SetColumnWidth(0, 30f);
                ImGui.Text("");
                ImGui.NextColumn();
                ImGui.Text("Full Gem Name");
                ImGui.NextColumn();
                ImGui.Text("Maximum Level");
                ImGui.NextColumn();
                if (Settings.SkillGemStopList[i].Rules.Count != 0)
                    ImGui.Separator();
                for (int j = 0; j < Settings.SkillGemStopList[i].Rules.Count; j++)
                {
                    if (ImGui.Button($"X##REMOVERULE{i}{j}"))
                    {
                        RulesToRemove.Add(j);
                    }

                    ImGui.NextColumn();
                    ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X);
                    Settings.SkillGemStopList[i].Rules[j].GemName =
                            ImGuiExtension.InputText($"##GN{i}{j}", Settings.SkillGemStopList[i].Rules[j].GemName, 35,
                                    ImGuiInputTextFlags.None);
                    ImGui.PopItemWidth();
                    ImGui.SameLine();
                    ImGui.NextColumn();
                    ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X);
                    Settings.SkillGemStopList[i].Rules[j].MaxLevel.Value =
                            ImGuiExtension.IntSlider($"##ML{i}{j}", Settings.SkillGemStopList[i].Rules[j].MaxLevel, 1, 20);
                    ImGui.NextColumn();
                    ImGui.PopItemWidth();
                }

                foreach (int i1 in RulesToRemove)
                {
                    Settings.SkillGemStopList[i].Rules.Remove(Settings.SkillGemStopList[i].Rules[i1]);
                }

                ImGui.Separator();
                ImGui.Columns(1, "", false);
                if (ImGui.Button($"Add New##AN{i}"))
                    Settings.SkillGemStopList[i]
                            .Rules.Add(new GemLevelRule
                             {
                                     GemName = "Cast when Damage Taken Support",
                                     MaxLevel = new RangeNode<int>(1, 1, 20)
                             });
            }
        }

        public void LevelUpGems()
        {
            if (!Keyboard.IsKeyDown((int) Settings.LevelSkillGemsHotkey.Value))
            {
                return;
            }

            if (!Settings.LevelSkillGems)
            {
                return;
            }

            if (GemClickTimer.ElapsedMilliseconds < 150) return;
            GemClickTimer.Restart();


            bool WeExistInRuleSet = false;
            var RuleSet = new List<GemLevelRule>();
            // Check if the profile exists
            foreach (Person PersonCheck in Settings.SkillGemStopList)
            {
                if (PersonCheck.Character == GameController.Player.GetComponent<Player>().PlayerName)
                {
                    WeExistInRuleSet = true;
                    RuleSet = PersonCheck.Rules;
                    break;
                }
            }

            if (!WeExistInRuleSet)
            {
                Settings.SkillGemStopList.Add(new Person
                {
                        Character = GameController.Player.GetComponent<Player>().PlayerName,
                        Rules = new List<GemLevelRule>()
                });
            }

            Element SkillGemLevelUps = GameController.Game.IngameState.UIRoot.GetChildAtIndex(1)
                                                     .GetChildAtIndex(4)
                                                     .GetChildAtIndex(1)
                                                     .GetChildAtIndex(0);
            if (SkillGemLevelUps == null || !SkillGemLevelUps.IsVisible) return;
            var MosuePos = Mouse.GetCursorPositionVector();
            foreach (Element element in SkillGemLevelUps.Children)
            {
                RectangleF skillGemButton = element.GetChildAtIndex(1).GetClientRect();
                string skillGemText = element.GetChildAtIndex(3).Text;
                if (element.GetChildAtIndex(2).IsVisibleLocal) continue;

                if (element.GetChildAtIndex(0).Tooltip.ChildCount == 0 && skillGemText?.ToLower() == "click to level up")
                {
                    var ToolTipHover = skillGemButton;
                    ToolTipHover.X += ToolTipHover.Width;
                    Mouse.SetCurosPosToCenterOfRec(ToolTipHover, GameController.Window.GetWindowRectangle());
                    //var WhileStart = DateTime.Now;
                    //while (element.GetChildAtIndex(0).Tooltip.ChildCount == 0 && WhileStart.AddSeconds(3) > DateTime.Now)
                    //{
                    //    Thread.Sleep(25);
                    //    Mouse.SetCurosPosToCenterOfRec(ToolTipHover, GameController.Window.GetWindowRectangle());
                    //}
                }
                //if (element.GetChildAtIndex(0).Tooltip.ChildCount != 0)
                //{
                    //SkillGemTooltipWrapper SkillGemToolTipInfo = new SkillGemTooltipWrapper(element.GetChildAtIndex(0).Tooltip);
                    if (skillGemText?.ToLower() == "click to level up")
                    {
                    //if (PassedGemRule(RuleSet, SkillGemToolTipInfo))
                    //{
                    Mouse.SetCurosPosToCenterOfRec(skillGemButton, GameController.Window.GetWindowRectangle());
                    Mouse.UniMouseClick(Settings.LevlSkillGemIsLeftClick, 5, 25);
                    //Mouse.SetCurosPosToCenterOfRec(GameController.Window.GetWindowRectangle(), GameController.Window.GetWindowRectangle());
                    GemClickTimer.Restart();
                            //break;
                        //}

                        //Mouse.SetCurosPosToCenterOfRec(skillGemButton, GameController.Window.GetWindowRectangle());
                        //Mouse.RightClick(5, 10);
                        ////Mouse.SetCurosPosToCenterOfRec(GameController.Window.GetWindowRectangle(), GameController.Window.GetWindowRectangle());
                        //GemClickTimer.Restart();
                        //break;
                    }
                //}
                Thread.Sleep(25);
            }

            //Mouse.SetCursorPos(MosuePos);
        }

        public bool PassedGemRule(List<GemLevelRule> rules, SkillGemTooltipWrapper gem)
        {
            //foreach (GemLevelRule rule in rules)
            //{
            //    if (!string.Equals(gem.Name, rule.GemName, StringComparison.CurrentCultureIgnoreCase)) continue;
            //    if (gem.Level >= rule.MaxLevel) return false;
            //}

            return true;
        }

        public void LevelSkillGems()
        {
                try
                {
                    LevelUpGems();
                }
                catch (Exception)
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
            public RangeNode<int> MaxLevel { get; set; }
        }

        public class Person
        {
            public string Character { get; set; }
            public List<GemLevelRule> Rules { get; set; }
        }
    }
}