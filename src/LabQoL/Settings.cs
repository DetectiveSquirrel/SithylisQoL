using System;
using PoeHUD.Hud.Settings;
using PoeHUD.Plugins;
using SharpDX;
using SharpDX.Direct3D9;
using ImGuiVector2 = System.Numerics.Vector2;

namespace Random_Features
{
    public class RandomFeaturesSettings : SettingsBase
    {
        public RandomFeaturesSettings()
        {
            UnsortedPlugin = true;
            Enable = true;
            PluginTextSize = new RangeNode<int>(16, 1, 200);
            LabyrinthChest = true;
            LabyrinthChestSize = new RangeNode<int>(18, 1, 50);
            TreasureKeyChestColor = new ColorBGRA(0, 255, 0, 255);
            TrinketChestColor = new ColorBGRA(0, 97, 201, 255);
            SpecificUniqueChestColor = new ColorBGRA(175, 96, 37, 255);
            RewardCurrencyColor = new ColorBGRA(255, 0, 255, 255);
            RewardCurrencyQualityColor = new ColorBGRA(210, 210, 210, 255);
            RewardDangerCurrencyColor = new ColorBGRA(255, 0, 255, 255);
            RewardDangerCurrencyQualityColor = new ColorBGRA(210, 210, 210, 255);
            RewardDangerUniqueColor = new ColorBGRA(175, 96, 37, 255);
            RewardDangerDivinationColor = new ColorBGRA(250, 250, 0, 255);
            RewardDangerLowGemColor = new ColorBGRA(255, 255, 255, 255);
            RewardDangerCorVaalColor = new ColorBGRA(255, 0, 0, 255);
            RewardDangerJewelleryColor = new ColorBGRA(255, 255, 255, 255);
            RewardDangerGenericColor = new ColorBGRA(255, 255, 255, 255);
            RewardSilverCurrencyColor = new ColorBGRA(255, 0, 255, 255);
            RewardSilverCurrencyQualityColor = new ColorBGRA(210, 210, 210, 255);
            RewardSilverJewelryUniqueColor = new ColorBGRA(175, 96, 37, 255);
            RewardSilverDivinationColor = new ColorBGRA(250, 250, 0, 255);
            RewardSilverUniqueOneColor = new ColorBGRA(175, 96, 37, 255);
            RewardSilverUniqueTwoColor = new ColorBGRA(175, 96, 37, 255);
            RewardSilverUniqueThreeColor = new ColorBGRA(175, 96, 37, 255);
            RewardSilverSkillGemColor = new ColorBGRA(175, 96, 37, 255);
            Darkshrines = true;
            DarkshrinesOnFloor = true;
            DarkshrinesOnFloorSize = new RangeNode<int>(100, 1, 200);
            DarkshrinesOnMap = true;
            DarkshrinesIcon = new RangeNode<int>(25, 1, 200);
            DarkshrinesColor = new ColorBGRA(0, 128, 255, 255);
            HiddenDoorway = true;
            HiddenDoorwayIcon = new RangeNode<int>(18, 1, 50);
            HiddenDoorwayColor = new ColorBGRA(119, 213, 108, 255);
            SecretPassage = true;
            SecretPassageIcon = new RangeNode<int>(18, 1, 50);
            SecretPassageColor = new ColorBGRA(0, 255, 255, 255);
            SmashableDoor = true;
            SmashableDoorColor = new ColorBGRA(251, 97, 4, 255);
            Roombas = true;
            RoombasOnMap = true;
            RoombasOnMapColor = new ColorBGRA(255, 255, 255, 255);
            RoombasOnMapSize = new RangeNode<int>(25, 1, 200);
            RoombasColor = new ColorBGRA(255, 255, 255, 255);
            Spinners = true;
            SpinnersOnMap = true;
            SpinnersOnMapColor = new ColorBGRA(255, 255, 255, 255);
            SpinnersOnMapSize = new RangeNode<int>(25, 1, 200);
            SpinnersColor = new ColorBGRA(255, 255, 255, 255);
            Saws = true;
            SawsOnMap = true;
            SawsOnMapColor = new ColorBGRA(255, 255, 255, 255);
            SawsOnMapSize = new RangeNode<int>(25, 1, 200);
            SawsColor = new ColorBGRA(255, 255, 255, 255);
            SecretSwitch = true;
            SecretSwitchColor = new ColorBGRA(0, 128, 255, 255);
            Delivery = true;
            DeliveryColor = new ColorBGRA(0, 255, 0, 255);
            PressurePlates = true;
            PressurePlatesColor = new ColorBGRA(255, 255, 255, 255);
            Arrows = true;
            ArrowsColor = new ColorBGRA(255, 255, 255, 255);
            Sentinels = true;
            UnendingLethargy = true;
            UnendingLethargyColor = new ColorBGRA(255, 255, 255, 255);
            EndlessDrought = true;
            EndlessDroughtColor = new ColorBGRA(255, 255, 255, 255);
            EndlessHazard = true;
            EndlessHazardColor = new ColorBGRA(255, 255, 255, 255);
            EndlessPain = true;
            EndlessPainColor = new ColorBGRA(255, 255, 255, 255);
            EndlessSting = true;
            EndlessStingColor = new ColorBGRA(255, 255, 255, 255);
            UnendingFire = true;
            UnendingFireColor = new ColorBGRA(255, 255, 255, 255);
            UnendingFrost = true;
            UnendingFrostColor = new ColorBGRA(255, 255, 255, 255);
            UnendingStorm = true;
            UnendingStormColor = new ColorBGRA(255, 255, 255, 255);
            LieutenantofRage = true;
            LieutenantofRageSize = new RangeNode<int>(100, 1, 200);
            Atziri = true;
            AtziriMirrorSize = new RangeNode<int>(100, 1, 200);
            NormalShrines = true;
            NormalShrineOnFloor = true;
            NormalShrineOnFloorSize = new RangeNode<int>(100, 1, 200);
            NormalShrineOnMap = true;
            NormalShrinesIcon = new RangeNode<int>(25, 1, 200);
            NormalShrinesColor = new ColorBGRA(0, 128, 255, 255);
            LesserShrines = true;
            LesserShrineOnFloor = true;
            LesserShrineOnFloorSize = new RangeNode<int>(100, 1, 200);
            LesserShrineOnMap = true;
            LesserShrinesIcon = new RangeNode<int>(25, 1, 200);
            LesserShrinesColor = new ColorBGRA(0, 128, 255, 255);
            AreaTransition = true;
            AreaTransitionIcon = new RangeNode<int>(18, 1, 50);
            AreaTransitionColor = new ColorBGRA(255, 150, 72, 255);
            Specters = true;
            TukohamasVanguard = true;
            WickerMan = true;
            PockedLanternbearer = true;
            SolarGuard = true;
            AbyssCracks = true;
            AbyssSmallNodeSize = new RangeNode<int>(7, 1, 200);
            AbyssSmallNodeColor = new ColorBGRA(255, 140, 0, 255);
            AbyssLargeNodeSize = new RangeNode<int>(14, 1, 200);
            AbyssLargeNodeColor = new ColorBGRA(255, 100, 0, 255);
            AbysshoardChestToggleNode = true;
            AbysshoardChestSize = new RangeNode<int>(15, 1, 200);
            AbysshoardChestColor = new ColorBGRA(251, 177, 96, 255);
            VaultPiles = true;
            VaultPilesOnFloor = true;
            VaultPilesOnFloorSize = new RangeNode<int>(100, 1, 200);
            VaultPilesOnMap = true;
            VaultPilesIcon = new RangeNode<int>(25, 1, 200);
            AreaNamesOnMap = true;
            // Fuck Roman Numerals
            FrnMain = true;
            FrnBackgroundBoxExtraWidth = new RangeNode<int>(3, 0, 200);
            FrnBackgroundBoxWidth = new RangeNode<int>(24, 1, 200);
            FrnBackgroundBoxHeight = new RangeNode<int>(24, 1, 200);
            FrnNextBoxOffset = new RangeNode<int>(66, 1, 500);
            FrnFirstTierRowX = new RangeNode<int>(55, 1, 500);
            FrnFirstTierRowY = new RangeNode<int>(207, 1, 500);
            FrnSecondTierRowX = new RangeNode<int>(82, 1, 500);
            FrnSecondTierRowY = new RangeNode<int>(274, 1, 500);
            FrnFontSize = new RangeNode<int>(20, 1, 60);
            // Wheres My Cursor
            WmcMain = true;
            WmcLineType = new RangeNode<int>(1, 1, 3);
            WmcLineColor = new Color(255, 255, 255, 255);
            WmcLineLength = new RangeNode<int>(200, 1, 1000);
            WmcLineSize = new RangeNode<int>(1, 1, 10);
            WmcPlayerOffsetXNegitive = true;
            WmcPlayerOffsetX = new RangeNode<int>(0, 0, 200);
            WmcPlayerOffsetYNegitive = true;
            WmcPlayerOffsetY = new RangeNode<int>(0, 0, 200);
            ShowWindow = false;
            var centerPos = BasePlugin.API.GameController.Window.GetWindowRectangle().Center;
            LastSettingSize = new ImGuiVector2(620, 376);
            LastSettingPos = new ImGuiVector2(centerPos.X - LastSettingSize.X / 2, centerPos.Y - LastSettingSize.Y / 2);
        }

        public ToggleNode UnsortedPlugin { get; set; }
        public RangeNode<int> PluginTextSize { get; set; }
        public ToggleNode LabyrinthChest { get; set; }
        public RangeNode<int> LabyrinthChestSize { get; set; }
        public ColorNode TrinketChestColor { get; set; }
        public ColorNode TreasureKeyChestColor { get; set; }
        public ColorNode SpecificUniqueChestColor { get; set; }
        public ColorNode RewardCurrencyColor { get; set; }
        public ColorNode RewardCurrencyQualityColor { get; set; }
        public ColorNode RewardDangerCurrencyColor { get; set; }
        public ColorNode RewardDangerCurrencyQualityColor { get; set; }
        public ColorNode RewardDangerUniqueColor { get; set; }
        public ColorNode RewardDangerDivinationColor { get; set; }
        public ColorNode RewardDangerLowGemColor { get; set; }
        public ColorNode RewardDangerCorVaalColor { get; set; }
        public ColorNode RewardDangerJewelleryColor { get; set; }
        public ColorNode RewardDangerGenericColor { get; set; }
        public ColorNode RewardSilverCurrencyColor { get; set; }
        public ColorNode RewardSilverCurrencyQualityColor { get; set; }
        public ColorNode RewardSilverJewelryUniqueColor { get; set; }
        public ColorNode RewardSilverDivinationColor { get; set; }
        public ColorNode RewardSilverUniqueOneColor { get; set; }
        public ColorNode RewardSilverUniqueTwoColor { get; set; }
        public ColorNode RewardSilverUniqueThreeColor { get; set; }
        public ColorNode RewardSilverSkillGemColor { get; set; }
        public ToggleNode Darkshrines { get; set; }
        public ToggleNode DarkshrinesOnFloor { get; set; }
        public RangeNode<int> DarkshrinesOnFloorSize { get; set; }
        public ToggleNode DarkshrinesOnMap { get; set; }
        public RangeNode<int> DarkshrinesIcon { get; set; }
        public ColorNode DarkshrinesColor { get; set; }
        public ToggleNode SecretSwitch { get; set; }
        public ColorNode SecretSwitchColor { get; set; }
        public ToggleNode Delivery { get; set; }
        public ColorNode DeliveryColor { get; set; }
        public ToggleNode HiddenDoorway { get; set; }
        public RangeNode<int> HiddenDoorwayIcon { get; set; }
        public ColorNode HiddenDoorwayColor { get; set; }
        public ToggleNode SecretPassage { get; set; }
        public RangeNode<int> SecretPassageIcon { get; set; }
        public ColorNode SecretPassageColor { get; set; }
        public ToggleNode SmashableDoor { get; set; }
        public ColorNode SmashableDoorColor { get; set; }
        public ToggleNode LieutenantofRage { get; set; }
        public RangeNode<int> LieutenantofRageSize { get; set; }
        public ToggleNode Roombas { get; set; }
        public ToggleNode RoombasOnMap { get; set; }
        public RangeNode<int> RoombasOnMapSize { get; set; }
        public ColorNode RoombasOnMapColor { get; set; }
        public ColorNode RoombasColor { get; set; }
        public ToggleNode Spinners { get; set; }
        public ToggleNode SpinnersOnMap { get; set; }
        public RangeNode<int> SpinnersOnMapSize { get; set; }
        public ColorNode SpinnersOnMapColor { get; set; }
        public ColorNode SpinnersColor { get; set; }
        public ToggleNode Saws { get; set; }
        public ToggleNode SawsOnMap { get; set; }
        public RangeNode<int> SawsOnMapSize { get; set; }
        public ColorNode SawsOnMapColor { get; set; }
        public ColorNode SawsColor { get; set; }
        public ToggleNode Arrows { get; set; }
        public ColorNode ArrowsColor { get; set; }
        public ToggleNode PressurePlates { get; set; }
        public ColorNode PressurePlatesColor { get; set; }
        public ToggleNode Sentinels { get; set; }
        public ToggleNode UnendingLethargy { get; set; }
        public ColorNode UnendingLethargyColor { get; set; }
        public ToggleNode EndlessDrought { get; set; }
        public ColorNode EndlessDroughtColor { get; set; }
        public ToggleNode EndlessHazard { get; set; }
        public ColorNode EndlessHazardColor { get; set; }
        public ToggleNode EndlessPain { get; set; }
        public ColorNode EndlessPainColor { get; set; }
        public ToggleNode EndlessSting { get; set; }
        public ColorNode EndlessStingColor { get; set; }
        public ToggleNode UnendingFire { get; set; }
        public ColorNode UnendingFireColor { get; set; }
        public ToggleNode UnendingFrost { get; set; }
        public ColorNode UnendingFrostColor { get; set; }
        public ToggleNode UnendingStorm { get; set; }
        public ColorNode UnendingStormColor { get; set; }
        public ToggleNode Atziri { get; set; }
        public RangeNode<int> AtziriMirrorSize { get; set; }
        public ToggleNode NormalShrines { get; set; }
        public ToggleNode NormalShrineOnFloor { get; set; }
        public RangeNode<int> NormalShrineOnFloorSize { get; set; }
        public ToggleNode NormalShrineOnMap { get; set; }
        public RangeNode<int> NormalShrinesIcon { get; set; }
        public ColorNode NormalShrinesColor { get; set; }
        public ToggleNode LesserShrines { get; set; }
        public ToggleNode LesserShrineOnFloor { get; set; }
        public RangeNode<int> LesserShrineOnFloorSize { get; set; }
        public ToggleNode LesserShrineOnMap { get; set; }
        public RangeNode<int> LesserShrinesIcon { get; set; }
        public ColorNode LesserShrinesColor { get; set; }
        public ToggleNode AbyssCracks { get; set; }
        public RangeNode<int> AbyssSmallNodeSize { get; set; }
        public ColorNode AbyssSmallNodeColor { get; set; }
        public RangeNode<int> AbyssLargeNodeSize { get; set; }
        public ColorNode AbyssLargeNodeColor { get; set; }
        public ToggleNode AbysshoardChestToggleNode { get; set; }
        public RangeNode<int> AbysshoardChestSize { get; set; }
        public ColorNode AbysshoardChestColor { get; set; }
        public ToggleNode AreaTransition { get; set; }
        public RangeNode<int> AreaTransitionIcon { get; set; }
        public ColorNode AreaTransitionColor { get; set; }
        public ToggleNode Specters { get; set; }
        public ToggleNode TukohamasVanguard { get; set; }
        public ToggleNode WickerMan { get; set; }
        public ToggleNode PockedLanternbearer { get; set; }
        public ToggleNode SolarGuard { get; set; }
        public ToggleNode VaultPiles { get; set; }
        public ToggleNode VaultPilesOnFloor { get; set; }
        public RangeNode<int> VaultPilesOnFloorSize { get; set; }
        public ToggleNode VaultPilesOnMap { get; set; }
        public RangeNode<int> VaultPilesIcon { get; set; }


        public ToggleNode AreaNamesOnMap { get; set; }

        // Fuck Roman Numerals
        public ToggleNode FrnMain { get; set; }
        public RangeNode<int> FrnBackgroundBoxExtraWidth { get; set; }
        public RangeNode<int> FrnBackgroundBoxWidth { get; set; }
        public RangeNode<int> FrnBackgroundBoxHeight { get; set; }
        public RangeNode<int> FrnNextBoxOffset { get; set; }
        public RangeNode<int> FrnFirstTierRowX { get; set; }
        public RangeNode<int> FrnFirstTierRowY { get; set; }
        public RangeNode<int> FrnSecondTierRowX { get; set; }
        public RangeNode<int> FrnSecondTierRowY { get; set; }
        public RangeNode<int> FrnFontSize { get; set; }

        // Wheres My Cusrsor
        public ToggleNode WmcMain { get; set; }
        public RangeNode<int> WmcLineType { get; set; }
        public ColorNode WmcLineColor { get; set; }
        public RangeNode<int> WmcLineLength { get; set; }
        public RangeNode<int> WmcLineSize { get; set; }
        public ToggleNode WmcPlayerOffsetXNegitive { get; set; }
        public RangeNode<int> WmcPlayerOffsetX { get; set; }
        public ToggleNode WmcPlayerOffsetYNegitive { get; set; }
        public RangeNode<int> WmcPlayerOffsetY { get; set; }

        [Menu("Show ImGui Settings")]
        public ToggleNode ShowWindow { get; set; }

        public ImGuiVector2 LastSettingPos { get; set; }
        public ImGuiVector2 LastSettingSize { get; set; }

        [Menu("Version " + RandomFeatures.Version)]
        public EmptyNode VersionNode { get; set; }
    }
}