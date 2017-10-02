﻿using PoeHUD.Hud.Settings;
using PoeHUD.Plugins;
using SharpDX;

namespace LabQoL
{
    public class LabQoLSettings : SettingsBase
    {
        public LabQoLSettings()
        {
            Enable = true;
            #region Labyrinth
            #region Chests
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
            #endregion
            #region DarkShrines
            Darkshrines = true;
            DarkshrinesOnFloor = true;
            DarkshrinesOnFloorSize = new RangeNode<int>(100, 1, 200);
            DarkshrinesOnMap = true;
            DarkshrinesIcon = new RangeNode<int>(25, 1, 50);
            DarkshrinesColor = new ColorBGRA(0, 128, 255, 255);
            #endregion
            #region Doors
            HiddenDoorway = true;
            HiddenDoorwayIcon = new RangeNode<int>(18, 1, 50);
            HiddenDoorwayColor = new ColorBGRA(119, 213, 108, 255);
            SecretPassage = true;
            SecretPassageIcon = new RangeNode<int>(18, 1, 50);
            SecretPassageColor = new ColorBGRA(0, 255, 255, 255);
            SmashableDoor = true;
            SmashableDoorColor = new ColorBGRA(251, 97, 4, 255);
            #endregion
            #region Traps
            Roombas = true;
            RoombasColor = new ColorBGRA(255, 255, 255, 255);
            Spinners = true;
            SpinnersColor = new ColorBGRA(255, 255, 255, 255);
            Saws = true;
            SawsColor = new ColorBGRA(255, 255, 255, 255);
            SecretSwitch = true;
            SecretSwitchColor = new ColorBGRA(0, 128, 255, 255);
            Delivery = true;
            DeliveryColor = new ColorBGRA(0, 255, 0, 255);
            PressurePlates = true;
            PressurePlatesColor = new ColorBGRA(255, 255, 255, 255);
            Arrows = true;
            ArrowColor = new ColorBGRA(255, 255, 255, 255);

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

            #endregion
            #region Lieutenant of Rage, Has Thorns
            LieutenantofRage = true;
            LieutenantofRageSize = new RangeNode<int>(100, 1, 200);
            #endregion
            #endregion
            #region Atziri
            Atziri = true;
            AtziriMirrorSize = new RangeNode<int>(100, 1, 200);
            #endregion
            #region Shrines
            NormalShrines = true;
            NormalShrineOnFloor = true;
            NormalShrineOnFloorSize = new RangeNode<int>(100, 1, 200);
            NormalShrineOnMap = true;
            NormalShrinesIcon = new RangeNode<int>(25, 1, 50);
            NormalShrinesColor = new ColorBGRA(0, 128, 255, 255);
            LesserShrines = true;
            LesserShrineOnFloor = true;
            LesserShrineOnFloorSize = new RangeNode<int>(100, 1, 200);
            LesserShrineOnMap = true;
            LesserShrinesIcon = new RangeNode<int>(25, 1, 50);
            LesserShrinesColor = new ColorBGRA(0, 128, 255, 255);
            #endregion
            #region Area Transitions
            AreaTransition = true;
            AreaTransitionIcon = new RangeNode<int>(18, 1, 50);
            AreaTransitionColor = new ColorBGRA(255, 150, 72, 255);
            #endregion
            #region Specter Bodies
            Specters = true;
            Tukohamas_Vanguard = true;
            WickerMan = true;
            Solar_Guard = true;
            #endregion
            #region Debug-ish
            Debug = false;
            DebugTextSize = new RangeNode<int>(12, 1, 50);
            #endregion
        }

        #region Labyrinth

        // 11
        [Menu("Labyrinth", 11)]
        public EmptyNode LabyrinthEmpty { get; set; }

        // 11-1
        [Menu("Chests", 111, 11)]
        public ToggleNode LabyrinthChest { get; set; }
        [Menu("Size", 156811, 111)]
        public RangeNode<int> LabyrinthChestSize { get; set; }


        [Menu("Normal", 4793, 111)]
        public EmptyNode dwfdfhdh { get; set; }
        [Menu("Trinkets", 47931, 4793)]
        public ColorNode TrinketChestColor { get; set; }
        [Menu("Treasure Key", 47932, 4793)]
        public ColorNode TreasureKeyChestColor { get; set; }
        [Menu("Specific Unique", 47933, 4793)]
        public ColorNode SpecificUniqueChestColor { get; set; }
        [Menu("Currency", 47934, 4793)]
        public ColorNode RewardCurrencyColor { get; set; }
        [Menu("Currency High", 47935, 4793)]
        public ColorNode RewardCurrencyQualityColor { get; set; }


        [Menu("Danger", 465745, 111)]
        public EmptyNode ertjertj { get; set; }
        [Menu("Danger Cur", 4657451, 465745)]
        public ColorNode RewardDangerCurrencyColor { get; set; }
        [Menu("Danger Cur Quality", 4657452, 465745)]
        public ColorNode RewardDangerCurrencyQualityColor { get; set; }
        [Menu("Danger Unique", 4657453, 465745)]
        public ColorNode RewardDangerUniqueColor { get; set; }
        [Menu("Danger Divination", 4657454, 465745)]
        public ColorNode RewardDangerDivinationColor { get; set; }
        [Menu("Danger Low Superior Gems", 4657455, 465745)]
        public ColorNode RewardDangerLowGemColor { get; set; }
        [Menu("Danger Corrupted Vaal", 4657456, 465745)]
        public ColorNode RewardDangerCorVaalColor { get; set; }
        [Menu("Danger Jewelery", 4657457, 465745)]
        public ColorNode RewardDangerJewelleryColor { get; set; }
        [Menu("Danger Generic", 4657458, 465745)]
        public ColorNode RewardDangerGenericColor { get; set; }


        [Menu("Silver", 32546, 111)]
        public EmptyNode yljhj { get; set; }
        [Menu("Silver Cur", 325461, 32546)]
        public ColorNode RewardSilverCurrencyColor { get; set; }
        [Menu("Silver Cur Quality", 325462, 32546)]
        public ColorNode RewardSilverCurrencyQualityColor { get; set; }
        [Menu("Silver Jewelry Unique", 325463, 32546)]
        public ColorNode RewardSilverJewelryUniqueColor { get; set; }
        [Menu("Silver Divination", 325464, 32546)]
        public ColorNode RewardSilverDivinationColor { get; set; }
        [Menu("Silver Unique 1", 325465, 32546)]
        public ColorNode RewardSilverUniqueOneColor { get; set; }
        [Menu("Silver Unique 2", 325466, 32546)]
        public ColorNode RewardSilverUniqueTwoColor { get; set; }
        [Menu("Silver Unique 3", 325467, 32546)]
        public ColorNode RewardSilverUniqueThreeColor { get; set; }
        [Menu("Silver Skill Gems", 325468, 32546)]
        public ColorNode RewardSilverSkillGemColor { get; set; }

        // 11-2
        [Menu("Dark Shrines", 112, 11)]
        public ToggleNode Darkshrines { get; set; }
        [Menu("Draw on Floor", 1121, 112)]
        public ToggleNode DarkshrinesOnFloor { get; set; }
        [Menu("Size", 11211, 1121)]
        public RangeNode<int> DarkshrinesOnFloorSize { get; set; }
        [Menu("Draw on Map", 1122, 112)]
        public ToggleNode DarkshrinesOnMap { get; set; }
        [Menu("Shrine Size", 11221, 1122)]
        public RangeNode<int> DarkshrinesIcon { get; set; }
        [Menu("Shrine Color", 1123, 112)]
        public ColorNode DarkshrinesColor { get; set; }

        // 11-3
        [Menu("Secret Switch", 113, 11)]
        public ToggleNode SecretSwitch { get; set; }
        [Menu("Color", 1131, 113)]
        public ColorNode SecretSwitchColor { get; set; }

        // 11-4
        [Menu("Gauntlet Delivery", 114, 11)]
        public ToggleNode Delivery { get; set; }
        [Menu("Color", 1141, 114)]
        public ColorNode DeliveryColor { get; set; }

        // 11-6
        [Menu("Hidden Doorway", 116, 11)]
        public ToggleNode HiddenDoorway { get; set; }
        [Menu("Size", 1161, 116)]
        public RangeNode<int> HiddenDoorwayIcon { get; set; }
        [Menu("Color", 1162, 116)]
        public ColorNode HiddenDoorwayColor { get; set; }


        [Menu("Secret Passage", 35476, 11)]
        public ToggleNode SecretPassage { get; set; }
        [Menu("Size", 354761, 35476)]
        public RangeNode<int> SecretPassageIcon { get; set; }
        [Menu("Color", 354762, 35476)]
        public ColorNode SecretPassageColor { get; set; }

        [Menu("Smashable Door", 118, 11)]
        public ToggleNode SmashableDoor { get; set; }
        [Menu("Color", 1181, 118)]
        public ColorNode SmashableDoorColor { get; set; }

        [Menu("Lieutenant With Thorns", 117, 11)]
        public ToggleNode LieutenantofRage { get; set; }
        [Menu("Size", 1171, 117)]
        public RangeNode<int> LieutenantofRageSize { get; set; }

        // 11-5
        [Menu("Traps", 115, 11)]
        public EmptyNode TrapsEmpty { get; set; }

        [Menu("Roombas", 1151, 115)]
        public ToggleNode Roombas { get; set; }
        [Menu("Color", 11511, 1151)]
        public ColorNode RoombasColor { get; set; }

        [Menu("Spinners", 1152, 115)]
        public ToggleNode Spinners { get; set; }
        [Menu("Color", 11521, 1152)]
        public ColorNode SpinnersColor { get; set; }

        [Menu("Saws", 1153, 115)]
        public ToggleNode Saws { get; set; }
        [Menu("Color", 11531, 1153)]
        public ColorNode SawsColor { get; set; }

        [Menu("Arrows", 1154, 115)]
        public ToggleNode Arrows { get; set; }
        [Menu("Color", 11541, 1154)]
        public ColorNode ArrowColor { get; set; }

        [Menu("Pressure Plates", 1155, 115)]
        public ToggleNode PressurePlates { get; set; }
        [Menu("Color", 11551, 1155)]
        public ColorNode PressurePlatesColor { get; set; }

        // Sentinels
        [Menu("Sentinels", 91357, 11)]
        public ToggleNode Sentinels { get; set; }

        [Menu("Slowing", 913578, 91357)]
        public ToggleNode UnendingLethargy { get; set; }
        [Menu("Color", 9135781, 913578)]
        public ColorNode UnendingLethargyColor { get; set; }

        [Menu("Remove Flask Chrages", 913571, 91357)]
        public ToggleNode EndlessDrought { get; set; }
        [Menu("Color", 9135711, 913571)]
        public ColorNode EndlessDroughtColor { get; set; }

        [Menu("Movement Damage", 913572, 91357)]
        public ToggleNode EndlessHazard { get; set; }
        [Menu("Color", 9135721, 913572)]
        public ColorNode EndlessHazardColor { get; set; }

        [Menu("Inc Damage Taken", 913573, 91357)]
        public ToggleNode EndlessPain { get; set; }
        [Menu("Color", 9135731, 913573)]
        public ColorNode EndlessPainColor { get; set; }

        [Menu("Bleeding", 913574, 91357)]
        public ToggleNode EndlessSting { get; set; }
        [Menu("Color", 9135741, 913574)]
        public ColorNode EndlessStingColor { get; set; }

        [Menu("Fire Nova", 913575, 91357)]
        public ToggleNode UnendingFire { get; set; }
        [Menu("Color", 9135751, 913575)]
        public ColorNode UnendingFireColor { get; set; }

        [Menu("Ice Nova", 913576, 91357)]
        public ToggleNode UnendingFrost { get; set; }
        [Menu("Color", 9135761, 913576)]
        public ColorNode UnendingFrostColor { get; set; }

        [Menu("Shock Nova", 913577, 91357)]
        public ToggleNode UnendingStorm { get; set; }
        [Menu("Color", 9135771, 913577)]
        public ColorNode UnendingStormColor { get; set; }
        #endregion
        #region Atziri
        [Menu("Atziri", 22)]
        public EmptyNode AtziriEmpty { get; set; }
        [Menu("Show Reflection", 221, 22)]
        public ToggleNode Atziri { get; set; }
        [Menu("Atziri Mirror Size", 2211, 221)]
        public RangeNode<int> AtziriMirrorSize { get; set; }
        #endregion
        #region Shrines
        [Menu("Shrines", 33)]
        public EmptyNode Shrines { get; set; }

        [Menu("Normal Shrines", 331, 33)]
        public ToggleNode NormalShrines { get; set; }
        [Menu("Draw on Floor", 3311, 331)]
        public ToggleNode NormalShrineOnFloor { get; set; }
        [Menu("Size", 33111, 3311)]
        public RangeNode<int> NormalShrineOnFloorSize { get; set; }
        [Menu("Draw on Map", 3312, 331)]
        public ToggleNode NormalShrineOnMap { get; set; }
        [Menu("Size", 33121, 3312)]
        public RangeNode<int> NormalShrinesIcon { get; set; }
        [Menu("Color", 3313, 331)]
        public ColorNode NormalShrinesColor { get; set; }

        [Menu("Lesser Shrines", 332, 33)]
        public ToggleNode LesserShrines { get; set; }
        [Menu("Draw on Floor", 3321, 332)]
        public ToggleNode LesserShrineOnFloor { get; set; }
        [Menu("Size", 33211, 3321)]
        public RangeNode<int> LesserShrineOnFloorSize { get; set; }
        [Menu("Draw on Map", 3322, 332)]
        public ToggleNode LesserShrineOnMap { get; set; }
        [Menu("Size", 33221, 3322)]
        public RangeNode<int> LesserShrinesIcon { get; set; }
        [Menu("Color", 3323, 332)]
        public ColorNode LesserShrinesColor { get; set; }
        #endregion
        #region Area Transitions
        [Menu("Area Transitions", 5468)]
        public ToggleNode AreaTransition { get; set; }
        [Menu("Size", 54681, 5468)]
        public RangeNode<int> AreaTransitionIcon { get; set; }
        [Menu("Color", 54682, 5468)]
        public ColorNode AreaTransitionColor { get; set; }
        #endregion
        #region Specter Bodies
        [Menu("Specter Bodies", 7001)]
        public ToggleNode Specters { get; set; }
        [Menu("Tukohama's Vanguard", 70011, 7001)]
        public ToggleNode Tukohamas_Vanguard { get; set; }
        [Menu("WickerMan", 70012, 7001)]
        public ToggleNode WickerMan { get; set; }
        [Menu("Solar Guard", 70013, 7001)]
        public ToggleNode Solar_Guard { get; set; }
        #endregion
        #region Debug-ish
        [Menu("Debug-ish", 99)]
        public ToggleNode Debug { get; set; }
        [Menu("Text Size", 991, 99)]
        public RangeNode<int> DebugTextSize { get; set; }
        #endregion
    }
}