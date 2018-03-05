using ImGuiNET;
using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Models;
using PoeHUD.Plugins;
using Random_Features.Libs;
using System.Collections.Generic;
using System.Numerics;

namespace Random_Features
{
    public partial class RandomFeatures : BaseSettingsPlugin<RandomFeaturesSettings>
    {
        public const string Version = "1.1.2";
        public static int Selected;

        public static string[] SettingName =
        {
                "Random Features",
                "Fuck Roman Numerals",
                "Wheres My Cursor?",
                "Trials",
                "Area Transitions"
        };

        public static int idPop;
        private HashSet<EntityWrapper> _entityCollection;

        public string CustomImagePath;

        public string PoeHudImageLocation;

        public RandomFeatures() {
            PluginName = "Random Features";
        }

        private void AreaChange() { new Coroutine(ClearStoredEntities(), nameof(Random_Features), "Clear Stored Area Entities").Run(); }


        public override void Initialise()
        {
            _entityCollection = new HashSet<EntityWrapper>();
            CustomImagePath = PluginDirectory + @"\images\";
            PoeHudImageLocation = PluginDirectory + @"\..\..\textures\";
            GameController.Area.OnAreaChange += area => AreaChange();
        }

        public override void EntityAdded(EntityWrapper entity) { _entityCollection.Add(entity); }

        public override void EntityRemoved(EntityWrapper entity) { _entityCollection.Remove(entity); }

        public override void Render()
        {
            base.Render();
            if (!Settings.Enable) return;
            ImGuiMenu();
            UnsortedPlugin();
            FuckRomanNumerals();
            WheresMyCursor();
            AreaTranitions();
        }

        private void ImGuiMenu()
        {
            idPop = 1;
            if (!Settings.ShowWindow) return;
            var isOpened = Settings.ShowWindow.Value;
            ImGuiExtension.BeginWindow($"{PluginName} v{Version}", ref isOpened, Settings.LastSettingPos.X, Settings.LastSettingPos.Y, Settings.LastSettingSize.X, Settings.LastSettingSize.Y);
            Settings.ShowWindow.Value = isOpened;
            ImGui.PushStyleVar(StyleVar.ChildRounding, 5.0f);
            ImGuiExtension.ImGuiExtension_ColorTabs("LeftSettings", 50, SettingName, ref Selected, ref idPop);
            ImGuiNative.igGetContentRegionAvail(out var newcontentRegionArea);
            if (ImGui.BeginChild("RightSettings", new Vector2(newcontentRegionArea.X, newcontentRegionArea.Y), true, WindowFlags.Default))
                switch (SettingName[Selected])
                {
                    case "Random Features":
                        RandomFeaturesMenu(idPop, out var newInt);
                        idPop = newInt;
                        break;
                    case "Fuck Roman Numerals":
                        FuckRomanNumeralsMenu();
                        break;
                    case "Wheres My Cursor?":
                        WheresMyCursorMenu();
                        break;
                    case "Trials":
                        TrialMenu();
                        break;
                    case "Area Transitions":
                        AreaTranitionsMenu();
                        break;
                }
            ImGui.PopStyleVar();
            ImGui.EndChild();

            // Storing window Position and Size changed by the user
            if (ImGui.GetWindowHeight() > 21)
            {
                Settings.LastSettingPos = ImGui.GetWindowPosition();
                Settings.LastSettingSize = ImGui.GetWindowSize();
            }

            ImGui.EndWindow();
        }
    }

    internal static class ExtensionMethod
    {
        public static bool InRange(this int current, int range1, int range2) => current >= range1 && current <= range2;
    }
}