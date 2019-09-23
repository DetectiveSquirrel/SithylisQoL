using ExileCore;
using ExileCore.PoEMemory;
using ExileCore.PoEMemory.Elements;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared;
using ExileCore.Shared.AtlasHelper;
using ImGuiNET;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Random_Features.Libs;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Reflection;
using ImGuiVector2 = System.Numerics.Vector2;

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
        private const string FOSSIL_TIER_SETTINGS = "Gen Fossil Tiers";

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
                SKILL_GEM_LEVELING,
                FOSSIL_TIER_SETTINGS
        };

        public static int idPop;

        public override bool Initialise()
        {
            AtlasTextureInit();

            Name = RANDOM_FEATURES;

            Settings.centerPos = GameController.Window.GetWindowRectangle().Center;
            Settings.LastSettingSize = new ImGuiVector2(620, 376);
            Settings.LastSettingPos = new ImGuiVector2(Settings.centerPos.X - Settings.LastSettingSize.X / 2, Settings.centerPos.Y - Settings.LastSettingSize.Y / 2);

            Input.RegisterKey(Settings.LevelSkillGemsHotkey);

            buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            PluginVersion = $"{version}";

            storedAreaEntities = new List<StoredEntity>();
            GameController.Area.OnAreaChange += area => storedAreaEntities.Clear();

            AreaModWarningsInit();
            if (File.Exists($@"{DirectoryFullName}\Fossil_Tiers.json"))
            {
                var jsonFIle = File.ReadAllText($@"{DirectoryFullName}\Fossil_Tiers.json");
                FossilList = JsonConvert.DeserializeObject<FossilTiers>(jsonFIle, JsonSettings);
            }
            else
            {
                LogError("Error loading Fossil_Tiers.json, Please re download from Random Features github repository", 10);
            }

            return true;
        }

        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        public string ReadElementText(Element element) { return element.AsObject<EntityLabel>().Text; }

        public override void Render()
        {
            base.Render();

            if (!Settings.Enable || !GameController.InGame) return;
            // if (Settings._Debug)
                // LogMessage($"_entityCollection Size: {_entityCollection.Count}", 1);
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

        public override void DrawSettings()
        {
            ImGui.BulletText($"v{PluginVersion}");
            ImGui.BulletText($"Last Updated: {buildDate}");
            idPop = 1;
            ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, 5.0f);
            ImGuiExtension.ImGuiExtension_ColorTabs("LeftSettings", 50, SettingName, ref Selected, ref idPop);
            var newcontentRegionArea = new Vector2();
            newcontentRegionArea = ImGuiNative.igGetContentRegionAvail();
            if (ImGui.BeginChild("RightSettings", new Vector2(newcontentRegionArea.X, newcontentRegionArea.Y), true, ImGuiWindowFlags.None))
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
                    case FOSSIL_TIER_SETTINGS:
                        FielTierMenu();
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