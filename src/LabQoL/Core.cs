using System;
using System.Collections.Generic;
using System.Linq;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud;
using PoeHUD.Hud.Menu;
using PoeHUD.Hud.Settings;
using PoeHUD.Models;
using PoeHUD.Models.Enums;
using PoeHUD.Plugins;
using PoeHUD.Poe;
using PoeHUD.Poe.Components;
using PoeHUD.Poe.RemoteMemoryObjects;
using Random_Features.Libs;
using SharpDX;
using SharpDX.Direct3D9;
using Map = PoeHUD.Poe.Elements.Map;

namespace Random_Features
{
    public class RandomFeatures : BaseSettingsPlugin<RandomFeaturesSettings>
    {
        // Hides unwanted garbage strings
        private static readonly List<string> DebugDoodads = new List<string> { "elavator", "elevator", "Hideoutdoodad", "worlditem" };

        private HashSet<EntityWrapper> _entityCollection;

        public string CustomImagePath;

        public string PoeHudImageLocation;

        public RandomFeatures() => PluginName = "Random Features";

        public override void Initialise()
        {
            _entityCollection   = new HashSet<EntityWrapper>();
            CustomImagePath     = PluginDirectory + @"\images\";
            PoeHudImageLocation = PluginDirectory + @"\..\..\textures\";
        }

        public override void InitialiseMenu(MenuItem mainMenu)
        {
            base.InitialiseMenu(mainMenu);
            {
                MenuItem rootPluginMenu = PluginSettingsRootMenu;
                MenuItem main           = MenuWrapper.AddMenu("Unsorted",                 rootPluginMenu, Settings.UnsortedPlugin);
                MenuWrapper.AddMenu("Text Size",                                          main,           Settings.PluginTextSize);
                MenuItem others     = MenuWrapper.AddMenu("Others",                       main,           "Other random features");
                MenuItem abyss      = MenuWrapper.AddMenu("Abyss",                        others,         "Abyss Section");
                MenuItem abyssCrack = MenuWrapper.AddMenu("AbyssCracks",                  abyss,          "Cracks in the floor can be seen before you activate the abyss");
                MenuWrapper.AddMenu("Small Node Size",                                    abyssCrack,     Settings.AbyssSmallNodeSize,  "Size of cracks on the minimap");
                MenuWrapper.AddMenu("Small Node Color",                                   abyssCrack,     Settings.AbyssSmallNodeColor, "Color of the cracks on the minimap");
                MenuWrapper.AddMenu("Large Node Size",                                    abyssCrack,     Settings.AbyssLargeNodeSize,  "Size of the sections connected to the cracks that spawn monsters on minimap");
                MenuWrapper.AddMenu("Large Node Color",                                   abyssCrack,     Settings.AbyssLargeNodeColor, "Color of the large cracks on the minimap");
                MenuItem abyssChests = MenuWrapper.AddMenu("Chests",                      abyss);
                MenuItem hoardChest  = MenuWrapper.AddMenu("Hoard",                       abyssChests,     Settings.AbysshoardChestToggleNode, "Hoard chests usually contain awsome loot");
                MenuWrapper.AddMenu("Size",                                               hoardChest,      Settings.AbysshoardChestSize,       "Size of icon on minimap");
                MenuWrapper.AddMenu("Color",                                              hoardChest,      Settings.AbysshoardChestColor,      "Color of icon on minimap");
                MenuItem labyrinth       = MenuWrapper.AddMenu("Labyrinth",               main,            "Labyrinth Section");
                MenuItem labyrinthChests = MenuWrapper.AddMenu("Chests",                  labyrinth,       "Labyrinth Chests");
                MenuWrapper.AddMenu("Size",                                               labyrinthChests, Settings.LabyrinthChestSize, "Size of icon on map");
                MenuItem normalLabChests = MenuWrapper.AddMenu("Normal",                  labyrinthChests, "These might be \"Puzzle\" chests I cant remember, sorry");
                MenuWrapper.AddMenu("Trinkets",                                           normalLabChests, Settings.TrinketChestColor,          "Chest contains a Trinket");
                MenuWrapper.AddMenu("Treasure Key",                                       normalLabChests, Settings.TreasureKeyChestColor,      "Chest contains a Treasure Key");
                MenuWrapper.AddMenu("Specific Unique",                                    normalLabChests, Settings.SpecificUniqueChestColor,   "Chest contains Unique items");
                MenuWrapper.AddMenu("Currency",                                           normalLabChests, Settings.RewardCurrencyColor,        "Chest contains Regular Currency");
                MenuWrapper.AddMenu("Quality Currency",                                   normalLabChests, Settings.RewardCurrencyQualityColor, "Chest contains Quality Currency");
                MenuItem dangerLabChests = MenuWrapper.AddMenu("Danger",                  labyrinthChests, "Requires running a \"Gauntlet\" to get to the chest");
                MenuWrapper.AddMenu("Currency",                                           dangerLabChests, Settings.RewardDangerCurrencyColor,        "Chest contains Regular Currency");
                MenuWrapper.AddMenu("Quality Currency",                                   dangerLabChests, Settings.RewardDangerCurrencyQualityColor, "Chest contains Quality Currency");
                MenuWrapper.AddMenu("Unique",                                             dangerLabChests, Settings.RewardDangerUniqueColor,          "Chest contains Unique items");
                MenuWrapper.AddMenu("Divination",                                         dangerLabChests, Settings.RewardDangerDivinationColor,      "Chest contains Divination Cards");
                MenuWrapper.AddMenu("Low Gems",                                           dangerLabChests, Settings.RewardDangerLowGemColor,          "Chest contains Low Quality Skill Gems");
                MenuWrapper.AddMenu("Corrupted Vaal",                                     dangerLabChests, Settings.RewardDangerCorVaalColor,         "Chest contains Corrupted/Fragment pieces");
                MenuWrapper.AddMenu("Jewelery",                                           dangerLabChests, Settings.RewardDangerJewelleryColor,       "Chest contains Generic Jewellery");
                MenuWrapper.AddMenu("Generic",                                            dangerLabChests, Settings.RewardDangerGenericColor,         "Chest contains Generic items");
                MenuItem silverLabChests = MenuWrapper.AddMenu("Silver",                  labyrinthChests, "Requires Silver keys to gain access to these chests");
                MenuWrapper.AddMenu("Currency",                                           silverLabChests, Settings.RewardSilverCurrencyColor,        "Chest contains Regular Currency");
                MenuWrapper.AddMenu("Quality Currency",                                   silverLabChests, Settings.RewardSilverCurrencyQualityColor, "Chest contains Quality Currency");
                MenuWrapper.AddMenu("Unique Jewellery",                                   silverLabChests, Settings.RewardSilverJewelryUniqueColor,   "Chest contains Generic Jewellery");
                MenuWrapper.AddMenu("Divination",                                         silverLabChests, Settings.RewardSilverDivinationColor,      "Chest contains Divination Cards");
                MenuWrapper.AddMenu("Unique 1",                                           silverLabChests, Settings.RewardSilverUniqueOneColor,       "Chest contains Unique items");
                MenuWrapper.AddMenu("Unique 2",                                           silverLabChests, Settings.RewardSilverUniqueTwoColor,       "Chest contains Unique items");
                MenuWrapper.AddMenu("Unique 3",                                           silverLabChests, Settings.RewardSilverUniqueThreeColor,     "Chest contains Unique items");
                MenuWrapper.AddMenu("Skill Gems",                                         silverLabChests, Settings.RewardSilverSkillGemColor,        "Chest contains Quality Skill Gems");
                MenuItem darkShrines       = MenuWrapper.AddMenu("Dark Shrines",          labyrinth);
                MenuItem darkShrineOnFloor = MenuWrapper.AddMenu("Draw on Floor",         darkShrines,       Settings.DarkshrinesOnFloor,     "Draw on the World Floor");
                MenuWrapper.AddMenu("Size",                                               darkShrineOnFloor, Settings.DarkshrinesOnFloorSize, "Size of icon on the World Floor");
                MenuItem darkShrineOnMap = MenuWrapper.AddMenu("Draw on Map",             darkShrines,       Settings.DarkshrinesOnMap,       "Draw on the Minimap");
                MenuWrapper.AddMenu("Size",                                               darkShrineOnMap,   Settings.DarkshrinesIcon,        "Size of Icon on the Minimap");
                MenuWrapper.AddMenu("Color",                                              darkShrineOnMap,   Settings.DarkshrinesColor,       "Color of Icon");
                MenuItem secretSwitch = MenuWrapper.AddMenu("Secret Switch",              labyrinth,         Settings.SecretSwitch,           "Switches that open secret doors");
                MenuWrapper.AddMenu("Color",                                              secretSwitch,      Settings.SecretSwitchColor,      "Color of text");
                MenuItem gauntletDelivery = MenuWrapper.AddMenu("Gauntlet Delivery",      labyrinth,         Settings.Delivery,               "Deliver Gauntlet to the end of the track");
                MenuWrapper.AddMenu("Color",                                              gauntletDelivery,  Settings.DeliveryColor,          "Color of text");
                MenuItem hidenDoorWay = MenuWrapper.AddMenu("Hidden Doorway",             labyrinth,         Settings.HiddenDoorway,          "Hidden Doorway Icon");
                MenuWrapper.AddMenu("Size",                                               hidenDoorWay,      Settings.HiddenDoorwayIcon,      "Size of Icon on Minimap");
                MenuWrapper.AddMenu("Color",                                              hidenDoorWay,      Settings.HiddenDoorwayColor,     "Color of icon on Minimap, suggest making it white");
                MenuItem secretPassage = MenuWrapper.AddMenu("Secret Passage",            labyrinth,         Settings.SecretPassage,          "Secret Passage to travel to another Area");
                MenuWrapper.AddMenu("Size",                                               secretPassage,     Settings.SecretPassageIcon,      "Size of Icon on Minimap");
                MenuWrapper.AddMenu("Color",                                              secretPassage,     Settings.SecretPassageColor,     "Color of icon on Minimap, suggest making it white");
                MenuItem smashableDoor = MenuWrapper.AddMenu("Smashable Door",            labyrinth,         Settings.SmashableDoor,          "Smashable doors on walls to gain access to blocked off areas");
                MenuWrapper.AddMenu("Color",                                              smashableDoor,     Settings.SmashableDoorColor,     "Color of text");
                MenuItem lieutenantOfRage = MenuWrapper.AddMenu("Lieutenant With Thorns", labyrinth,         Settings.LieutenantofRage,       "Ice Lieutenant in Izaro room sometimes casts a thorns/reflext aura\nThis will indicate that skeleton with a atziri mirror");
                MenuWrapper.AddMenu("Size",                                               lieutenantOfRage,  Settings.LieutenantofRageSize,   "Size of mirror over the Lieutenant");
                MenuItem traps   = MenuWrapper.AddMenu("Traps",                           labyrinth);
                MenuItem roombas = MenuWrapper.AddMenu("Roombas",                         traps,                 Settings.Roombas,               "Flying Roombas of Doom");
                MenuWrapper.AddMenu("Text Color",                                         roombas,               Settings.RoombasColor,          "Color of text on trap");
                MenuItem roombasOnMap = MenuWrapper.AddMenu("On Map",                     roombas,               Settings.RoombasOnMap,          "Draw Icon on minimap");
                MenuWrapper.AddMenu("Size",                                               roombasOnMap,          Settings.RoombasOnMapSize,      "Size of Icon on minimap");
                MenuWrapper.AddMenu("Color",                                              roombasOnMap,          Settings.RoombasOnMapColor,     "Color of Icon on minimap");
                MenuItem spinners = MenuWrapper.AddMenu("Spinners",                       traps,                 Settings.Spinners,              "Spinning Poles");
                MenuWrapper.AddMenu("Text Color",                                         spinners,              Settings.RoombasColor,          "Color of text on trap");
                MenuItem spinnersOnMap = MenuWrapper.AddMenu("On Map",                    spinners,              Settings.SpinnersOnMap,         "Draw Icon on minimap");
                MenuWrapper.AddMenu("Size",                                               spinnersOnMap,         Settings.SpinnersOnMapSize,     "Size of Icon on minimap");
                MenuWrapper.AddMenu("Color",                                              spinnersOnMap,         Settings.SpinnersOnMapColor,    "Color of Icon on minimap");
                MenuItem saws = MenuWrapper.AddMenu("Saws",                               traps,                 Settings.Spinners,              "Saw Blades");
                MenuWrapper.AddMenu("Text Color",                                         saws,                  Settings.RoombasColor,          "Color of text on trap");
                MenuItem sawsOnMap = MenuWrapper.AddMenu("On Map",                        saws,                  Settings.SawsOnMap,             "Draw Icon on minimap");
                MenuWrapper.AddMenu("Size",                                               sawsOnMap,             Settings.SawsOnMapSize,         "Size of Icon on minimap");
                MenuWrapper.AddMenu("Color",                                              sawsOnMap,             Settings.SawsOnMapColor,        "Color of Icon on minimap");
                MenuItem arrows = MenuWrapper.AddMenu("Arrows",                           traps,                 Settings.Arrows,                "Dart Traps hidden inside walls");
                MenuWrapper.AddMenu("Color",                                              arrows,                Settings.ArrowColor,            "Color of text on trap");
                MenuItem pressurePlates = MenuWrapper.AddMenu("Pressure Plates",          traps,                 Settings.PressurePlates,        "Pressure plates activate Arrow Traps");
                MenuWrapper.AddMenu("Color",                                              pressurePlates,        Settings.PressurePlatesColor,   "Color of text on trap");
                MenuItem sentinels             = MenuWrapper.AddMenu("Sentinels",         traps,                 Settings.Sentinels,             "Bad Curse like totems");
                MenuItem unendingLethargyColor = MenuWrapper.AddMenu("Unending Lethargy", sentinels,             Settings.UnendingLethargy,      "Applies Temporal Chains");
                MenuWrapper.AddMenu("Color",                                              unendingLethargyColor, Settings.UnendingLethargyColor, "Color of text on totem");
                MenuItem endlessDrought = MenuWrapper.AddMenu("Endless Drought",          sentinels,             Settings.EndlessDrought,        "Removes flask charges");
                MenuWrapper.AddMenu("Color",                                              endlessDrought,        Settings.EndlessDroughtColor,   "Color of text on totem");
                MenuItem endlessHazard = MenuWrapper.AddMenu("Endless Hazard",            sentinels,             Settings.EndlessHazard,         "Casts multiple circles that deal physical damage\nequal to 20% of life + 12% of ES (if ES is protecting life)\nwhen a movement skill is used\nThe damage can be mitigated, but not avoided");
                MenuWrapper.AddMenu("Color",                                              endlessHazard,         Settings.EndlessHazardColor,    "Color of text on totem");
                MenuItem endlessPain = MenuWrapper.AddMenu("Endless Pain",                sentinels,             Settings.EndlessPain,           "Casts an aura that apply 50% increased damage taken");
                MenuWrapper.AddMenu("Color",                                              endlessPain,           Settings.EndlessPainColor,      "Color of text on totem");
                MenuItem endlessSting = MenuWrapper.AddMenu("Endless Sting",              sentinels,             Settings.EndlessSting,          "Causes Bleeding");
                MenuWrapper.AddMenu("Color",                                              endlessSting,          Settings.EndlessStingColor,     "Color of text on totem");
                MenuItem unendingFire = MenuWrapper.AddMenu("Unending Fire",              sentinels,             Settings.UnendingFire,          "Casts Fire Nova");
                MenuWrapper.AddMenu("Color",                                              unendingFire,          Settings.UnendingFireColor,     "Color of text on totem");
                MenuItem unendingFrost = MenuWrapper.AddMenu("Unending Frost",            sentinels,             Settings.UnendingFrost,         "Casts Ice Nova");
                MenuWrapper.AddMenu("Color",                                              unendingFrost,         Settings.UnendingFrostColor,    "Color of text on totem");
                MenuItem unendingLethargy = MenuWrapper.AddMenu("Unending Storm",         sentinels,             Settings.UnendingLethargy,      "Casts Shock Nova");
                MenuWrapper.AddMenu("Color",                                              unendingLethargy,      Settings.UnendingStormColor,    "Color of text on totem");
                MenuItem atziri = MenuWrapper.AddMenu("Atziri",                           main,                  "Atziri Section");
                MenuWrapper.AddMenu("Show Reflection",                                    atziri,                Settings.Atziri,           "Show Atziri's mirror over the reflection clone\nHelps to quickly identify the bad clone");
                MenuWrapper.AddMenu("Size",                                               atziri,                Settings.AtziriMirrorSize, "Size of Icon over mirror clone");
                MenuItem shrines            = MenuWrapper.AddMenu("Shrines",              main,                  "Shrines in Wraeclast");
                MenuItem normalShrines      = MenuWrapper.AddMenu("Normal Shrines",       shrines,               Settings.NormalShrines,           "Normal Shrines");
                MenuItem normaShrineOnFloor = MenuWrapper.AddMenu("Draw on Floor",        normalShrines,         Settings.NormalShrineOnFloor,     "Draw icon on the world floor");
                MenuWrapper.AddMenu("Size",                                               normaShrineOnFloor,    Settings.NormalShrineOnFloorSize, "Size of the icon");
                MenuItem normalShrineOnMap = MenuWrapper.AddMenu("Draw on Map",           normalShrines,         Settings.NormalShrineOnMap,       "Draw icon on the minimap");
                MenuWrapper.AddMenu("Size",                                               normalShrineOnMap,     Settings.NormalShrinesIcon,       "Size of the icon");
                MenuWrapper.AddMenu("Color",                                              normalShrineOnMap,     Settings.NormalShrinesColor,      "Color of the icon");
                MenuItem lesserShrines       = MenuWrapper.AddMenu("Lesser Shrines",      shrines,               Settings.LesserShrines,           "Lesser shrines created by \"The Gull\" helmet");
                MenuItem lesserShrineOnFloor = MenuWrapper.AddMenu("Draw on Floor",       lesserShrines,         Settings.LesserShrineOnFloor,     "Draw the icon on the world floor");
                MenuWrapper.AddMenu("Size",                                               lesserShrineOnFloor,   Settings.LesserShrineOnFloorSize, "Size of the icon");
                MenuItem lesserShrineOnMap = MenuWrapper.AddMenu("Draw on Floor",         lesserShrines,         Settings.LesserShrineOnMap,       "Draw icon on the minimap");
                MenuWrapper.AddMenu("Size",                                               lesserShrineOnMap,     Settings.LesserShrinesIcon,       "Size of the icon");
                MenuWrapper.AddMenu("Color",                                              lesserShrineOnMap,     Settings.LesserShrinesColor,      "Color of the icon");
                MenuItem areaTransitions = MenuWrapper.AddMenu("Area Transitions",        main,                  Settings.AreaTransition,          "Shows area transitions on minimap\nCan show hidden ones that arnt currently shown on poes minimap");
                MenuWrapper.AddMenu("Size",                                               areaTransitions,       Settings.AreaTransitionIcon,      "Size on icon on minimap");
                MenuWrapper.AddMenu("Color",                                              areaTransitions,       Settings.AreaTransitionColor,     "Color of icon on minimap\nI would suggest making this White");
                MenuItem specters = MenuWrapper.AddMenu("Specter Bodies",                 main,                  Settings.Specters,                "Corpses onthe ground useful for spectres");
                MenuWrapper.AddMenu("Tukohama's Vanguard",                                specters,              Settings.TukohamasVanguard,       "Casts Scorching Ray Totems\nUsually used for bossing");
                MenuWrapper.AddMenu("WickerMan",                                          specters,              Settings.WickerMan,               "Use a Rightous Fire arua\nHigh life");
                MenuWrapper.AddMenu("Pocked Lanternbearer",                               specters,              Settings.PockedLanternbearer,     "Cant remember what i used these for, but i did");
                MenuWrapper.AddMenu("Solar Guard",                                        specters,              Settings.SolarGuard,              "Nice for projectiles summons\nusually used for high clearspeed");
                MenuItem vaultPiles        = MenuWrapper.AddMenu("Vault Gold Piles",      main,                  Settings.VaultPiles,              "Inside the Vault Map there are Gold Piles that contain AWSOME loot\nIdicated by a Perandus Coin icon");
                MenuItem vaultPilesOnFloor = MenuWrapper.AddMenu("Draw on Floor",         vaultPiles,            Settings.VaultPilesOnFloor,       "Draw icon on world floor");
                MenuWrapper.AddMenu("Size",                                               vaultPilesOnFloor,     Settings.VaultPilesOnFloor,       "Size of the icon on world floor");
                MenuItem vaultPilesOnMap = MenuWrapper.AddMenu("Draw on Map",             vaultPiles,            Settings.VaultPilesOnMap,         "Draw icon on the minimap");
                MenuWrapper.AddMenu("Size",                                               vaultPilesOnMap,       Settings.VaultPilesIcon,          "Size of the icon on the minimap");
                MenuItem debug = MenuWrapper.AddMenu("Debug-ish",                         main,                  "Random debug shit.");
                MenuWrapper.AddMenu("Toggle Button",                                      debug,                 Settings.DebugIshToggleButton, "Button to toggle debug On/Off");
                MenuWrapper.AddMenu("Monsters Only",                                      debug,                 Settings.DebugMonstersOnly,    "Show only entities with Monster component");
                MenuWrapper.AddMenu("Text Size",                                          debug,                 Settings.DebugTextSize,        "Size of text drawn over entities on world floor");
                // Fuck Roman Numerals
                MenuItem FrmMenu = MenuWrapper.AddMenu("Fuck Roman Numerals", rootPluginMenu, Settings.FrnMain, "Draws numbers over the roman numerals in the Map Tab\nThis has to be manualy configured im sorry");
                MenuWrapper.AddMenu("Background Box Extra Width",             FrmMenu,        Settings.FrnBackgroundBoxExtraWidth);
                MenuWrapper.AddMenu("Background Box Width",                   FrmMenu,        Settings.FrnBackgroundBoxWidth);
                MenuWrapper.AddMenu("Background Box Height",                  FrmMenu,        Settings.FrnBackgroundBoxHeight);
                MenuWrapper.AddMenu("Next Box Offset",                        FrmMenu,        Settings.FrnNextBoxOffset);
                MenuWrapper.AddMenu("First Tier Row X",                       FrmMenu,        Settings.FrnFirstTierRowX);
                MenuWrapper.AddMenu("First Tier RowY",                        FrmMenu,        Settings.FrnFirstTierRowY);
                MenuWrapper.AddMenu("Second Tier Row X",                      FrmMenu,        Settings.FrnSecondTierRowX);
                MenuWrapper.AddMenu("Second Tier Row Y",                      FrmMenu,        Settings.FrnSecondTierRowY);
                MenuWrapper.AddMenu("Font Size",                              FrmMenu,        Settings.FrnFontSize);
                // Wheres My Cursor
                MenuItem WmcMenu = MenuWrapper.AddMenu("Wheres My Cursor", rootPluginMenu, Settings.WmcMain,     "Draws a line from your character to where your mouse is");
                MenuWrapper.AddMenu("Line Type",                           WmcMenu,        Settings.WmcLineType, "1: Draw a set length no matter where the mouse is\n2: Draw to the mouse\n3: Draw to the edge of the screen");
                MenuWrapper.AddMenu("Line Color",                          WmcMenu,        Settings.WmcLineColor);
                MenuWrapper.AddMenu("Line Max Length",                     WmcMenu,        Settings.WmcLineLength);
                MenuWrapper.AddMenu("Line Size",                           WmcMenu,        Settings.WmcLineSize);
                MenuWrapper.AddMenu("Player Offset X Negitive",            WmcMenu,        Settings.WmcPlayerOffsetXNegitive);
                MenuWrapper.AddMenu("Player Offset X",                     WmcMenu,        Settings.WmcPlayerOffsetX);
                MenuWrapper.AddMenu("Player Offset Y Negitive",            WmcMenu,        Settings.WmcPlayerOffsetYNegitive);
                MenuWrapper.AddMenu("Player Offset Y",                     WmcMenu,        Settings.WmcPlayerOffsetY);
            }
        }

        public Vector2 Vector2OffsetCalculations(Vector2 information)
        {
            Vector2 finalVectorCalculation = information;
            finalVectorCalculation.X       = Settings.WmcPlayerOffsetXNegitive ? finalVectorCalculation.X - Settings.WmcPlayerOffsetX : finalVectorCalculation.X + Settings.WmcPlayerOffsetX;
            finalVectorCalculation.Y       = Settings.WmcPlayerOffsetYNegitive ? finalVectorCalculation.Y - Settings.WmcPlayerOffsetY : finalVectorCalculation.Y + Settings.WmcPlayerOffsetY;
            return finalVectorCalculation;
        }

        public void DrawLineToPosWithLength(Vector2 pointA, Vector2 pointB, int lineLength, int lineSize, Color lineColor)
        {
            Vector2 direction = pointB - pointA;
            direction.Normalize();
            Vector2 pointC = pointA + direction * lineLength;
            Graphics.DrawLine(pointA, pointC, lineSize, lineColor);
        }

        public void DrawLineToPos(Vector2 pointA, Vector2 pointB, int lineSize, Color lineColor) { Graphics.DrawLine(pointA, pointB, lineSize, lineColor); }

        public void DrawImageToWorld(ImageToWorldData information)
        {
            foreach (EntityWrapper entity in _entityCollection)
                foreach (string wantedPath in information.Path)
                    if (entity.Path.Contains(wantedPath))
                    {
                        Camera     camera            = GameController.Game.IngameState.Camera;
                        Vector2    chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, information.YOffset), entity);
                        RectangleF iconRect          = new RectangleF(chestScreenCoords.X - information.ImageSize / 2, chestScreenCoords.Y - information.ImageSize / 2, information.ImageSize, information.ImageSize);
                        if (chestScreenCoords == new Vector2()) continue;
                        // create rect at chest location to draw icon
                        if (information.HasColor)
                            Graphics.DrawPluginImage(information.ImagePath,  iconRect, information.Color);
                        else Graphics.DrawPluginImage(information.ImagePath, iconRect);
                    }
        }

        public void DrawImageToWorld_Chest(ImageToWorldData information)
        {
            foreach (EntityWrapper entity in _entityCollection)
                foreach (string wantedPath in information.Path)
                    if (!entity.GetComponent<Chest>().IsOpened && entity.Path.Contains(wantedPath))
                    {
                        Camera  camera            = GameController.Game.IngameState.Camera;
                        Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, information.YOffset), entity);
                        if (chestScreenCoords == new Vector2()) continue;

                        // create rect at chest location to draw icon
                        RectangleF iconRect = new RectangleF((chestScreenCoords.X - information.ImageSize) / 2, (chestScreenCoords.Y - information.ImageSize) / 2, information.ImageSize, information.ImageSize);
                        if (information.HasColor)
                            Graphics.DrawPluginImage(information.ImagePath,  iconRect, information.Color);
                        else Graphics.DrawPluginImage(information.ImagePath, iconRect);
                    }
        }

        public void DrawImageToWorld_Shrine(ImageToWorldData information)
        {
            foreach (EntityWrapper entity in _entityCollection)
                foreach (string wantedPath in information.Path)
                    if (entity.GetComponent<Shrine>().IsAvailable && entity.Path.Contains(wantedPath))
                    {
                        Camera  camera            = GameController.Game.IngameState.Camera;
                        Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, information.YOffset), entity);
                        if (chestScreenCoords == new Vector2()) continue;

                        // create rect at chest location to draw icon
                        RectangleF iconRect = new RectangleF(chestScreenCoords.X - information.ImageSize / 2, chestScreenCoords.Y - information.ImageSize / 2, information.ImageSize, information.ImageSize);
                        if (information.HasColor)
                            Graphics.DrawPluginImage(information.ImagePath,  iconRect, information.Color);
                        else Graphics.DrawPluginImage(information.ImagePath, iconRect);
                    }
        }

        public override void EntityAdded(EntityWrapper entity) { _entityCollection.Add(entity); }

        public override void EntityRemoved(EntityWrapper entity) { _entityCollection.Remove(entity); }

        private void ToggleDebug() { Settings.Debug = !Settings.Debug; }

        public override void Render()
        {
            base.Render();
            if (!Settings.Enable) return;
            UnsortedPlugin();
            FuckRomanNumerals();
            WheresMyCursor();
        }

        private void WheresMyCursor()
        {
            if (!Settings.WmcMain) return;
            IngameUIElements windows = GameController.Game.IngameState.IngameUi;
            if (windows.TreePanel.IsVisible)
                return;
            if (windows.AtlasPanel.IsVisible)
                return;
            if (windows.OpenLeftPanel.IsVisible)
                return;
            if (windows.OpenRightPanel.IsVisible)
                return;
            Vector2    cursorPositionVector = Mouse.GetCursorPositionVector();
            RectangleF windowRectangle      = GameController.Window.GetWindowRectangle();

            // need to call use -170 in Z axis for player.pos.translate as this is whats used in poecore for hp bar.
            //i have no fucking clue why this is. If you do not follow this rule you will have a jumpy hp bar
            Vector2 playerToScreen = LocalPlayer.PlayerToScreen;
            Vector2 finalPointA;
            switch (Settings.WmcLineType)
            {
                case 1:
                    finalPointA = Vector2OffsetCalculations(new Vector2(playerToScreen.X, windowRectangle.Height / 2));
                    DrawLineToPosWithLength(finalPointA, cursorPositionVector, Settings.WmcLineLength, Settings.WmcLineSize, Settings.WmcLineColor);
                    break;
                case 2:
                    finalPointA = Vector2OffsetCalculations(new Vector2(playerToScreen.X, windowRectangle.Height / 2));
                    DrawLineToPos(finalPointA, cursorPositionVector, Settings.WmcLineSize, Settings.WmcLineColor);
                    break;
                case 3:
                    finalPointA = Vector2OffsetCalculations(new Vector2(playerToScreen.X, windowRectangle.Height / 2));
                    DrawLineToPosWithLength(finalPointA, cursorPositionVector, (int) windowRectangle.Width, Settings.WmcLineSize, Settings.WmcLineColor);
                    break;
            }
        }

        private void FuckRomanNumerals()
        {
            if (!Settings.FrnMain) return;
            DrawNumbersOverRomanNumerals();
        }

        private void UnsortedPlugin()
        {
            if (Settings.DebugIshToggleButton.PressedOnce())
                ToggleDebug();

            // Graphics.DrawText(Class_Memory.ReadFloat(API.GameController.Game.IngameState.Camera.Address + 0x204).ToString(), 40, new Vector2(500, 500));
            if (!GameController.Game.IngameState.IngameUi.AtlasPanel.IsVisible && !GameController.Game.IngameState.IngameUi.TreePanel.IsVisible)
            {
                RenderMapImages();
                RenderVaultPiles();
                RenderAtziriMirrorClone();
                RenderLieutenantSkeletonThorns();
                RenderShrines();
            }

            if (Settings.SecretSwitch)
                RenderTextLabel(Settings.SecretSwitchColor.Value, "Switch", "HiddenDoor_Switch");
            if (Settings.Spinners)
                RenderTextLabel(Settings.SpinnersColor.Value, "Spinner", "LabyrinthSpinner");
            if (Settings.Saws)
                RenderTextLabel(Settings.SawsColor.Value, "Saw Blade", "Labyrinthsawblade");
            if (Settings.Roombas)
                RenderTextLabel(Settings.RoombasColor.Value, "Roomba", "LabyrinthRoomba", -67);
            if (Settings.Roombas)
                RenderTextLabel(Settings.RoombasColor.Value, "Roomba", "LabyrinthflyingRoomba", -180);
            if (Settings.Delivery)
                RenderTextLabel(Settings.DeliveryColor.Value, "Deliver", "labyrinthtrapkeydelivery");
            if (Settings.PressurePlates)
                RenderTextLabel(Settings.PressurePlatesColor.Value, "Pressure" + Environment.NewLine + "Plate", "pressureplate_reset");
            if (Settings.Arrows)
                RenderTextLabel(Settings.ArrowColor.Value, "Arrows", "Labyrintharrowtrap");
            if (Settings.SmashableDoor)
                RenderTextLabel(Settings.SmashableDoorColor.Value, "Smash Door", "Objects/LabyrinthSmashableDoor");
            if (Settings.SecretPassage)
                DrawTextLabelEquals(Settings.SecretPassageColor.Value, "Secret" + Environment.NewLine + "Passage", "Metadata/Terrain/Labyrinth/Objects/SecretPassage");
            if (Settings.Specters)
            {
                if (Settings.TukohamasVanguard)
                    DrawTextLabelSpecter(Color.Yellow, "Tukohama's" + Environment.NewLine + "Vanguard", "Metadata/Monsters/KaomWarrior/KaomWarrior7", 120);
                if (Settings.WickerMan)
                    DrawTextLabelSpecter(Color.Yellow, "WickerMan", "Metadata/Monsters/WickerMan/WickerMan", 120);
                if (Settings.PockedLanternbearer)
                    DrawTextLabelSpecter(Color.Yellow, "Pocked " + Environment.NewLine + "Lanternbearer", "Metadata/Monsters/Miner/MinerLantern", 120);
                if (Settings.SolarGuard)
                    DrawTextLabelSpecter(Color.Yellow, "Solar Guard", "Metadata/Monsters/HolyFireElemental/HolyFireElementalSolarisBeam", 120);
            }

            if (Settings.Sentinels)
            {
                if (Settings.UnendingLethargy)
                    RenderTextLabel(Settings.UnendingLethargyColor.Value, "Slowing", "LabyrinthPopUpTotemSlow");
                if (Settings.EndlessDrought)
                    RenderTextLabel(Settings.EndlessDroughtColor.Value, $"Flask Charge{Environment.NewLine}Removal", "LabyrinthPopUpTotemFlaskChargeNova");
                if (Settings.EndlessHazard)
                    RenderTextLabel(Settings.EndlessHazardColor.Value, "Movement Damage", "LabyrinthPopUpTotemDamageTakenOnMovementSkillUse");
                if (Settings.EndlessPain)
                    RenderTextLabel(Settings.EndlessPainColor.Value, "Inc Damage Taken", "LabyrinthPopUpTotemIncreasedDamageTaken");
                if (Settings.EndlessSting)
                    RenderTextLabel(Settings.EndlessStingColor.Value, "Bleeding", "LabyrinthPopUpTotemBleedNova");
                if (Settings.UnendingFire)
                    RenderTextLabel(Settings.UnendingFireColor.Value, "Fire Nova", "LabyrinthPopUpTotemMonsterFire");
                if (Settings.UnendingFrost)
                    RenderTextLabel(Settings.UnendingFrostColor.Value, "Ice Nova", "LabyrinthPopUpTotemMonsterIceNova");
                if (Settings.UnendingStorm)
                    RenderTextLabel(Settings.UnendingStormColor.Value, "Shock Nova", "LabyrinthPopUpTotemMonsterLightning");
            }

            // Debug-ish things
            if (Settings.Debug) ShowAllPathObjects();

            // DrawAllPaths();
        }

        private void DrawTextLabelEquals(ColorBGRA color, string text, string path)
        {
            foreach (EntityWrapper entity in _entityCollection)
                if (entity.Path.ToLower().Equals(path.ToLower()))
                {
                    Camera  camera            = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords == new Vector2()) continue;
                    Vector2 iconRect    = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);
                    float   maxWidth    = 0;
                    float   maxheight   = 0;
                    Size2   size        = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y += size.Height;
                    maxheight           += size.Height;
                    maxWidth            =  Math.Max(maxWidth, size.Width);
                    Graphics.DrawBox(new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight), Color.Black);
                }
        }

        private void DrawTextLabelSpecter(ColorBGRA color, string text, string path, int zOffset = 0)
        {
            foreach (EntityWrapper entity in _entityCollection)
                if (entity.Path.ToLower().Contains(path.ToLower()) && !entity.IsAlive)
                {
                    Camera  camera            = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, zOffset), entity);
                    if (chestScreenCoords == new Vector2()) continue;
                    Vector2 iconRect      = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);
                    float   maxWidth      = 0;
                    float   maxheight     = 0;
                    Size2   size          = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y   += size.Height;
                    maxheight             += size.Height;
                    maxWidth              =  Math.Max(maxWidth, size.Width);
                    RectangleF background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                    Graphics.DrawBox(background, Color.Black);
                }
        }

        private void DrawToLargeMiniMap(EntityWrapper entity)
        {
            MapIcon icon = GetMapIcon(entity);
            if (icon == null) return;
            Camera     camera       = GameController.Game.IngameState.Camera;
            Map        mapWindow    = GameController.Game.IngameState.IngameUi.Map;
            RectangleF mapRect      = mapWindow.GetClientRect();
            Vector2    playerPos    = GameController.Player.GetComponent<Positioned>().GridPos;
            float      posZ         = GameController.Player.GetComponent<Render>().Z;
            Vector2    screenCenter = new Vector2(mapRect.Width / 2, mapRect.Height / 2).Translate(0, -20) + new Vector2(mapRect.X, mapRect.Y) + new Vector2(mapWindow.LargeMapShiftX, mapWindow.LargeMapShiftY);
            float      diag         = (float) Math.Sqrt(camera.Width * camera.Width + camera.Height * camera.Height);
            float      k            = camera.Width < 1024f ? 1120f : 1024f;
            float      scale        = k / camera.Height * camera.Width * 3f / 4f / mapWindow.LargeMapZoom;
            float      iconZ        = icon.EntityWrapper.GetComponent<Render>().Z;
            Vector2    point        = screenCenter + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, scale, (iconZ - posZ) / (9f / mapWindow.LargeMapZoom));
            HudTexture texture      = icon.TextureIcon;
            float      size         = icon.Size * 2; // icon.SizeOfLargeIcon.GetValueOrDefault(icon.Size * 2);
            texture.DrawPluginImage(Graphics, new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size));
        }

        private void DrawToSmallMiniMap(EntityWrapper entity)
        {
            MapIcon icon = GetMapIcon(entity);
            if (icon == null) return;
            Element     smallMinimap = GameController.Game.IngameState.IngameUi.Map.SmallMinimap;
            Vector2     playerPos    = GameController.Player.GetComponent<Positioned>().GridPos;
            float       posZ         = GameController.Player.GetComponent<Render>().Z;
            const float scale        = 240f;
            RectangleF  mapRect      = smallMinimap.GetClientRect();
            Vector2     mapCenter    = new Vector2(mapRect.X + mapRect.Width / 2, mapRect.Y + mapRect.Height / 2).Translate(0, 0);
            double      diag         = Math.Sqrt(mapRect.Width * mapRect.Width + mapRect.Height * mapRect.Height) / 2.0;
            float       iconZ        = icon.EntityWrapper.GetComponent<Render>().Z;
            Vector2     point        = mapCenter + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, scale, (iconZ - posZ) / 20);
            HudTexture  texture      = icon.TextureIcon;
            float       size         = icon.Size;
            RectangleF  rect         = new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size);
            mapRect.Contains(ref rect, out bool isContain);
            if (isContain) texture.DrawPluginImage(Graphics, rect);
        }

        private MapIcon GetMapIcon(EntityWrapper e)
        {
            if (Settings.Roombas && Settings.RoombasOnMap)
                if (e.Path.Contains("LabyrinthFlyingRoomba") || e.Path.Contains("LabyrinthRoomba"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "roomba.png", Settings.RoombasOnMapColor), () => Settings.RoombasOnMap, Settings.RoombasOnMapSize);
            if (Settings.Spinners && Settings.SpinnersOnMap)
                if (e.Path.Contains("LabyrinthSpinner"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "roomba.png", Settings.SpinnersOnMapColor), () => Settings.SpinnersOnMap, Settings.SpinnersOnMapSize);
            if (Settings.Saws && Settings.SawsOnMap)
                if (e.Path.Contains("LabyrinthSawblade"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "roomba.png", Settings.SawsOnMapColor), () => Settings.SawsOnMap, Settings.SawsOnMapSize);
            if (Settings.LabyrinthChest && !e.GetComponent<Chest>().IsOpened)
            {
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTreasureKey"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TreasureKeyChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTrinketChest"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TrinketChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthSpecificUnique"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.SpecificUniqueChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0Currency"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0CurrencyQuality"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrency"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrencyQuality"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerUnique"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerUniqueColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerDivination"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerDivinationColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerSkillGems"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerLowGemColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCorruptedVaal"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCorVaalColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerJewelry"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerJewelleryColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerGeneric"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerGenericColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrency"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrencyQuality"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverJewelryUnique"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverJewelryUniqueColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverDivination"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverDivinationColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique1"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueOneColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique3"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueTwoColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique2"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueThreeColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverSkillGems"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverSkillGemColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
            }

            if (Settings.Darkshrines)
                if (Settings.DarkshrinesOnMap)
                    if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden"))
                        return new MapIcon(e, new HudTexture(CustomImagePath + "shrines.png", Settings.DarkshrinesColor), () => Settings.DarkshrinesOnMap, Settings.DarkshrinesIcon);
            if (Settings.NormalShrines)
                if (Settings.NormalShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/Shrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(e, new HudTexture(CustomImagePath + "shrines.png", Settings.NormalShrinesColor), () => Settings.NormalShrineOnMap, Settings.NormalShrinesIcon);
            if (Settings.LesserShrines)
                if (Settings.LesserShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/LesserShrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(e, new HudTexture(CustomImagePath + "shrines.png", Settings.LesserShrinesColor), () => Settings.LesserShrineOnMap, Settings.LesserShrinesIcon);
            if (Settings.HiddenDoorway)
                if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Short") || e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Long"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.HiddenDoorwayColor), () => Settings.HiddenDoorway, Settings.HiddenDoorwayIcon);
            if (Settings.SecretPassage)
                if (e.Path.Equals("Metadata/Terrain/Labyrinth/Objects/SecretPassage"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.SecretPassageColor), () => Settings.SecretPassage, Settings.SecretPassageIcon);
            if (Settings.AreaTransition)
            {
                if (e.Path.Contains("Metadata/Chests/VaultTreasurePile"))
                    if (!e.GetComponent<Chest>().IsOpened)
                        return new MapIcon(e, new HudTexture(CustomImagePath + "Coin.png"), () => true, Settings.VaultPilesIcon);
                if (e.Path.Contains("Transition"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.AreaTransitionColor), () => true, Settings.AreaTransitionIcon);
            }

            if (Settings.AbyssCracks)
            {
                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssCrackSpawners/AbyssCrackSkeletonSpawner"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "abyss-crack.png", Settings.AbyssSmallNodeColor), () => true, Settings.AbyssSmallNodeSize);
                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeLarge") || e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmall") || e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmallMortarSpawned") || e.Path.Contains("Metadata/Chests/Abyss/AbyssFinal"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "abyss-node-small.png", Settings.AbyssLargeNodeColor), () => true, Settings.AbyssLargeNodeSize);
            }

            if (Settings.AbysshoardChestToggleNode)
                if (e.Path.Contains("Metadata/Chests/AbyssChest") && !e.GetComponent<Chest>().IsOpened)
                    return new MapIcon(e, new HudTexture(CustomImagePath + "exclamationmark.png", Settings.AbysshoardChestColor), () => Settings.AbysshoardChestToggleNode, Settings.AbysshoardChestSize);
            return null;
        }

        private void RenderAtziriMirrorClone()
        {
            if (!Settings.Atziri) return;
            ImageToWorldData wantedEntity = new ImageToWorldData { Path = new[] { "Metadata/Monsters/Atziri/AtziriMirror" }, ImagePath = $"{CustomImagePath}mirror.png", ImageSize = Settings.AtziriMirrorSize, YOffset = -90 };
            DrawImageToWorld(wantedEntity);
        }

        private void RenderLieutenantSkeletonThorns()
        {
            if (!Settings.LieutenantofRage) return;
            ImageToWorldData wantedEntity = new ImageToWorldData { Path = new[] { "Metadata/Monsters/Labyrinth/LabyrinthLieutenantSkeletonMeleeIce", "Metadata/Monsters/Labyrinth/LabyrinthLieutenantSkeletonMeleeIce2" }, ImagePath = $"{CustomImagePath}mirror.png", ImageSize = Settings.LieutenantofRageSize, YOffset = -90 };
            DrawImageToWorld(wantedEntity);
        }

        private void RenderMapImages()
        {
            if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
                foreach (EntityWrapper entity in _entityCollection)
                    DrawToLargeMiniMap(entity);
            else if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
                foreach (EntityWrapper entity in _entityCollection)
                    DrawToSmallMiniMap(entity);
        }

        private void RenderShrines()
        {
            ImageToWorldData lesserShrine = new ImageToWorldData { Path = new[] { "Metadata/Shrines/LesserShrine" }, ImagePath                                = $"{CustomImagePath}shrines.png", ImageSize = Settings.LesserShrineOnFloorSize, YOffset = -90, Color = Settings.LesserShrinesColor, HasColor = true };
            ImageToWorldData normalShrine = new ImageToWorldData { Path = new[] { "Metadata/Shrines/Shrine" }, ImagePath                                      = $"{CustomImagePath}shrines.png", ImageSize = Settings.LesserShrineOnFloorSize, YOffset = -90, Color = Settings.NormalShrinesColor, HasColor = true };
            ImageToWorldData darkShrine   = new ImageToWorldData { Path = new[] { "Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden" }, ImagePath = $"{CustomImagePath}shrines.png", ImageSize = Settings.DarkshrinesOnFloorSize, YOffset  = -90, Color = Settings.DarkshrinesColor, HasColor   = true };
            if (Settings.LesserShrines && Settings.LesserShrineOnFloor)
                DrawImageToWorld_Shrine(lesserShrine);
            if (Settings.NormalShrines && Settings.NormalShrineOnFloor)
                DrawImageToWorld_Shrine(normalShrine);
            if (Settings.Darkshrines && Settings.DarkshrinesOnFloor) DrawImageToWorld_Shrine(darkShrine);
        }

        public bool IsMapTabOpen() => GameController.Game.IngameState.ServerData.StashPanel.IsVisible && GameController.Game.IngameState.ServerData.StashPanel.VisibleStash.InvType == InventoryType.MapStash;

        private void DrawNumbersOverRomanNumerals()
        {
            try
            {
                if (!IsMapTabOpen()) return;
                const int      topRowTierCount         = 9;
                const int      bottomRowTierCount      = 7;
                RangeNode<int> backgroundBoxExtraWidth = Settings.FrnBackgroundBoxExtraWidth;
                int            backgroundBoxWidth      = Settings.FrnBackgroundBoxWidth + Settings.FrnBackgroundBoxExtraWidth * 2;
                RangeNode<int> backgroundBoxHeight     = Settings.FrnBackgroundBoxHeight;
                RangeNode<int> nextBoxOffset           = Settings.FrnNextBoxOffset;
                RangeNode<int> firstTierRowX           = Settings.FrnFirstTierRowX;
                RangeNode<int> firstTierRowY           = Settings.FrnFirstTierRowY;
                RangeNode<int> secondTierRowX          = Settings.FrnSecondTierRowX;
                RangeNode<int> secondTierRowY          = Settings.FrnSecondTierRowY;
                RangeNode<int> fontSize                = Settings.FrnFontSize;
                RectangleF     firstTabRow             = new RectangleF(firstTierRowX - backgroundBoxExtraWidth, firstTierRowY, backgroundBoxWidth, backgroundBoxHeight);
                Vector2        firstTabText            = new Vector2(firstTabRow.X + firstTabRow.Width / 2, firstTabRow.Y);
                RectangleF     secondTabRow            = new RectangleF(secondTierRowX - backgroundBoxExtraWidth, secondTierRowY, backgroundBoxWidth, backgroundBoxHeight);
                Vector2        secondTabText           = new Vector2(secondTabRow.X + secondTabRow.Width / 2, secondTabRow.Y);
                for (int i = 0; i < topRowTierCount; i++)
                {
                    RectangleF newBox  = new RectangleF(firstTabRow.X + i * nextBoxOffset, firstTabRow.Y, backgroundBoxWidth, backgroundBoxHeight);
                    Vector2    newText = new Vector2(firstTabText.X + i * nextBoxOffset, firstTabText.Y + 1);
                    Graphics.DrawBox(newBox, Color.Black);
                    Graphics.DrawText((i + 1).ToString(), fontSize, newText, i + 1 < 6 ? Color.White : Color.Yellow, FontDrawFlags.Center);
                }

                for (int i = 0; i < bottomRowTierCount; i++)
                {
                    RectangleF newBox  = new RectangleF(secondTabRow.X + i * nextBoxOffset, secondTabRow.Y, backgroundBoxWidth, backgroundBoxHeight);
                    Vector2    newText = new Vector2(secondTabText.X + i * nextBoxOffset, secondTabText.Y + 1);
                    Graphics.DrawBox(newBox, Color.Black);
                    Graphics.DrawText((i + topRowTierCount + 1).ToString(), fontSize, newText, i + topRowTierCount + 1 < 11 ? Color.Yellow : Color.Red, FontDrawFlags.Center);
                }

                RectangleF uBox  = new RectangleF(secondTabRow.X + bottomRowTierCount * nextBoxOffset, secondTabRow.Y, backgroundBoxWidth, backgroundBoxHeight);
                Vector2    uText = new Vector2(secondTabText.X + bottomRowTierCount * nextBoxOffset, secondTabText.Y + 1);
                Graphics.DrawBox(uBox, Color.Black);
                Graphics.DrawText("U", fontSize, uText, Color.Orange, FontDrawFlags.Center);
            }
            catch
            {
                // ignored
            }
        }

        private void RenderTextLabel(ColorBGRA color, string text, string path)
        {
            foreach (EntityWrapper entity in _entityCollection)
                if (entity.Path.ToLower().Contains(path.ToLower()))
                {
                    Camera  camera            = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords == new Vector2()) continue;
                    Vector2 iconRect      = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);
                    float   maxWidth      = 0;
                    float   maxheight     = 0;
                    Size2   size          = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y   += size.Height;
                    maxheight             += size.Height;
                    maxWidth              =  Math.Max(maxWidth, size.Width);
                    RectangleF background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                    Graphics.DrawBox(background, Color.Black);
                }
        }

        private void RenderTextLabel(ColorBGRA color, string text, string path, int zOffset)
        {
            foreach (EntityWrapper entity in _entityCollection)
                if (entity.Path.ToLower().Contains(path.ToLower()))
                {
                    Camera  camera            = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, zOffset), entity);
                    if (chestScreenCoords == new Vector2()) continue;
                    Vector2 iconRect      = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);
                    float   maxWidth      = 0;
                    float   maxheight     = 0;
                    Size2   size          = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y   += size.Height;
                    maxheight             += size.Height;
                    maxWidth              =  Math.Max(maxWidth, size.Width);
                    RectangleF background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                    Graphics.DrawBox(background, Color.Black);
                }
        }

        private void RenderVaultPiles()
        {
            if (!Settings.VaultPiles) return;
            ImageToWorldData wantedEntity = new ImageToWorldData { Path = new[] { "Metadata/Chests/VaultTreasurePile" }, ImagePath = $"{CustomImagePath}Coin.png", ImageSize = Settings.VaultPilesOnFloorSize };
            DrawImageToWorld_Chest(wantedEntity);
        }

        private void ShowAllPathObjects()
        {
            foreach (EntityWrapper entity in _entityCollection)
            {
                List<string> textToShow = new List<string>();
                bool         show       = true;
                foreach (string stringDebugDoodad in DebugDoodads)
                    if (entity.Path.ToLower().Contains(stringDebugDoodad.ToLower()))
                        show = false;
                if (show && Settings.DebugMonstersOnly)
                    if (!entity.HasComponent<Monster>() || !entity.IsAlive)
                        show = false;
                if (show)
                {
                    textToShow.Add(entity.Path);
                    textToShow.AddRange(entity.GetComponent<Life>().Buffs.Select(buff => buff.Name));
                    Camera  camera            = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords != new Vector2())
                    {
                        Vector2 iconRect      = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);
                        float   maxWidth      = 0;
                        float   maxheight     = 0;
                        string  finalString   = textToShow.Aggregate("", (current, _string) => current + _string + Environment.NewLine);
                        Size2   size          = Graphics.DrawText(finalString, Settings.DebugTextSize, iconRect, Color.White, FontDrawFlags.Center);
                        chestScreenCoords.Y   += size.Height;
                        maxheight             += size.Height;
                        maxWidth              =  Math.Max(maxWidth, size.Width);
                        RectangleF background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                        Graphics.DrawBox(background, Color.Black);
                    }
                }
            }
        }

        public class ImageToWorldData
        {
            public Color Color;

            public bool HasColor;

            public string ImagePath;

            public int ImageSize = 10;

            public string[] Path = { };

            public int YOffset;
        }
    }
}