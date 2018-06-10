using System;
using ImGuiNET;
using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Models;
using PoeHUD.Plugins;
using Random_Features.Libs;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection;
using PoeHUD.Poe;
using PoeHUD.Poe.Elements;

namespace Random_Features
{
    public partial class RandomFeatures : BaseSettingsPlugin<RandomFeaturesSettings>
    {
        private const string Area_Mod_Warnings = "Area Mod Warnings";
        private const string Area_Transitions = "Area Transitions";
        private const string Trials = "Trials";
        private const string Wheres_My_Cursor = "Wheres My Cursor?";
        private const string Fuck_Roman_Numeras = "Fuck Roman Numerals";
        private const string Random_Features = "Random Features";
        private const string Skill_Gem_Leveling = "Skill Gem Leveling";

        //https://stackoverflow.com/questions/826777/how-to-have-an-auto-incrementing-version-number-visual-studio
        public Version Version = Assembly.GetExecutingAssembly().GetName().Version;
        public string PluginVersion;
        public DateTime BuildDate;
        public static int Selected;

        public static string[] SettingName =
        {
                Random_Features,
                Fuck_Roman_Numeras,
                Wheres_My_Cursor,
                Trials,
                Area_Transitions,
                Area_Mod_Warnings,
                Skill_Gem_Leveling
        };

        public static int IdPop;
        private HashSet<EntityWrapper> _EntityCollection;

        public string CustomImagePath;

        public string PoeHudImageLocation;

        public RandomFeatures() {
            PluginName = Random_Features;
        }

        private void AreaChange() { new Coroutine(ClearStoredEntities(), nameof(Random_Features), "Clear Stored Area Entities").Run(); }


        public override void Initialise()
        {
            BuildDate = new DateTime(2000, 1, 1).AddDays(Version.Build).AddSeconds(Version.Revision * 2);
            PluginVersion = $"{Version}";
            _EntityCollection = new HashSet<EntityWrapper>();
            CustomImagePath = PluginDirectory + @"\images\";
            PoeHudImageLocation = PluginDirectory + @"\..\..\textures\";
            GameController.Area.OnAreaChange += Area => AreaChange();
            AreaModWarningsInit();
        }

        public string ReadElementText(Element Element) { return Element.AsObject<EntityLabel>().Text; }

        public override void EntityAdded(EntityWrapper Entity) { _EntityCollection.Add(Entity); }

        public override void EntityRemoved(EntityWrapper Entity) { _EntityCollection.Remove(Entity); }

        public override void Render()
        {
            base.Render();
            if (!Settings.Enable) return;
            UnsortedPlugin();
            FuckRomanNumerals();
            WheresMyCursor();
            AreaTranitions();
            AreaModWarnings();
            LevelUpGems();

            //Element tradingWindow = GetPlayerTradingWindow();
            //if (tradingWindow == null || !tradingWindow.IsVisible)
            //{
            //    return;
            //}
            //var acceptButton = tradingWindow.GetChildAtIndex(5).GetChildAtIndex(0);
            //Graphics.DrawText(ReadElementText(acceptButton), 30, new SharpDX.Vector2(500, 500));
            //using (StreamWriter sw = File.AppendText("Button Log.txt"))
            //{
            //    sw.WriteLine(ReadElementText(acceptButton));
            //}



        }

        public Element GetPlayerTradingWindow()
        {
            try
            {
                // Player Trade Window
                return GameController.Game.IngameState.UIRoot.Children[1].Children[50].Children[3].Children[1].Children[0].Children[0];
            }
            catch
            {
                return null;
            }
        }

        public override void DrawSettingsMenu()
        {
            ImGui.BulletText($"v{PluginVersion}");
            ImGui.BulletText($"Last Updated: {BuildDate}");
            IdPop = 1;
            ImGui.PushStyleVar(StyleVar.ChildRounding, 5.0f);
            ImGuiExtension.ImGuiExtension_ColorTabs("LeftSettings", 50, SettingName, ref Selected, ref IdPop);
            ImGuiNative.igGetContentRegionAvail(out var NewcontentRegionArea);
            if (ImGui.BeginChild("RightSettings", new Vector2(NewcontentRegionArea.X, NewcontentRegionArea.Y), true, WindowFlags.Default))
                switch (SettingName[Selected])
                {
                    case Random_Features:
                        RandomFeaturesMenu(IdPop, out var NewInt);
                        IdPop = NewInt;
                        break;
                    case Fuck_Roman_Numeras:
                        FuckRomanNumeralsMenu();
                        break;
                    case Wheres_My_Cursor:
                        WheresMyCursorMenu();
                        break;
                    case Trials:
                        TrialMenu();
                        break;
                    case Area_Transitions:
                        AreaTranitionsMenu();
                        break;
                    case Area_Mod_Warnings:
                        AreaModWarningsMenu();
                        break;
                    case Skill_Gem_Leveling:
                        LevelSkillGemsMenu();
                        break;
                }
            ImGui.PopStyleVar();
            ImGui.EndChild();
        }
    }

    internal static class ExtensionMethod
    {
        public static bool InRange(this int Current, int Range1, int Range2) => Current >= Range1 && Current <= Range2;
    }
}