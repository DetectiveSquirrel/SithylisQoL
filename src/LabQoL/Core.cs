using System;
using System.Collections.Concurrent;
using ImGuiNET;
using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Models;
using PoeHUD.Plugins;
using Random_Features.Libs;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Numerics;
using System.Reflection;
using PoeHUD.Poe;
using PoeHUD.Poe.Elements;

namespace Random_Features
{
    public partial class RandomFeatures : BaseSettingsPlugin<RandomFeaturesSettings>
    {
        private const string AREA_MOD_WARNINGS = "Area Mod Warnings";
        private const string AREA_TRANSITIONS = "Area Transitions";
        private const string TRIALS = "Trials";
        private const string WHERES_MY_CURSOR = "Wheres My Cursor?";
        private const string FUCK_ROMAN_NUMERAS = "Fuck Roman Numerals";
        private const string RANDOM_FEATURES = "Random Features";
        private const string SKILL_GEM_LEVELING = "Skill Gem Leveling";

        //https://stackoverflow.com/questions/826777/how-to-have-an-auto-incrementing-version-number-visual-studio
        public Version version = Assembly.GetExecutingAssembly().GetName().Version;
        public string PluginVersion;
        public DateTime buildDate;
        public static int Selected;

        public static string[] SettingName =
        {
                RANDOM_FEATURES,
                FUCK_ROMAN_NUMERAS,
                WHERES_MY_CURSOR,
                TRIALS,
                AREA_TRANSITIONS,
                AREA_MOD_WARNINGS,
                SKILL_GEM_LEVELING
        };

        public static int idPop;
        private ConcurrentDictionary<long, EntityWrapper> _entityCollection;

        public string CustomImagePath;

        public string PoeHudImageLocation;

        public RandomFeatures() {
            PluginName = RANDOM_FEATURES;
        }

        private void AreaChange() { new Coroutine(ClearStoredEntities(), nameof(Random_Features), "Clear Stored Area Entities").Run(); }


        public override void Initialise()
        {
            buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            PluginVersion = $"{version}";
            _entityCollection = new ConcurrentDictionary<long, EntityWrapper>();

            storedAreaEntities = new List<StoredEntity>();
            CustomImagePath = PluginDirectory + @"\images\";
            PoeHudImageLocation = PluginDirectory + @"\..\..\textures\";
            GameController.Area.OnAreaChange += area => AreaChange();
            AreaModWarningsInit();
        }

        public string ReadElementText(Element element) { return element.AsObject<EntityLabel>().Text; }

        public override void EntityAdded(EntityWrapper entity) { _entityCollection[entity.Id] = entity; }

        public override void EntityRemoved(EntityWrapper entity) { _entityCollection.TryRemove(entity.Id, out _); }

        public override void Render()
        {
            base.Render();
            if (!Settings.Enable) return;
            UnsortedPlugin();
            FuckRomanNumerals();
            WheresMyCursor();
            AreaTranitions();
            //AreaModWarnings();
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
            ImGui.BulletText($"Last Updated: {buildDate}");
            idPop = 1;
            ImGui.PushStyleVar(StyleVar.ChildRounding, 5.0f);
            ImGuiExtension.ImGuiExtension_ColorTabs("LeftSettings", 50, SettingName, ref Selected, ref idPop);
            ImGuiNative.igGetContentRegionAvail(out var newcontentRegionArea);
            if (ImGui.BeginChild("RightSettings", new Vector2(newcontentRegionArea.X, newcontentRegionArea.Y), true, WindowFlags.Default))
                switch (SettingName[Selected])
                {
                    case RANDOM_FEATURES:
                        RandomFeaturesMenu(idPop, out var newInt);
                        idPop = newInt;
                        break;
                    case FUCK_ROMAN_NUMERAS:
                        FuckRomanNumeralsMenu();
                        break;
                    case WHERES_MY_CURSOR:
                        WheresMyCursorMenu();
                        break;
                    case TRIALS:
                        TrialMenu();
                        break;
                    case AREA_TRANSITIONS:
                        AreaTranitionsMenu();
                        break;
                    case AREA_MOD_WARNINGS:
                        AreaModWarningsMenu();
                        break;
                    case SKILL_GEM_LEVELING:
                        LevelSkillGemsMenu();
                        break;
                }
            ImGui.PopStyleVar();
            ImGui.EndChild();
        }
    }

    internal static class ExtensionMethod
    {
        public static bool InRange(this int current, int range1, int range2) => current >= range1 && current <= range2;
    }
}