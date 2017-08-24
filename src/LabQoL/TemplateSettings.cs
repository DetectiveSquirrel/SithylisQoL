using PoeHUD.Hud.Settings;
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
            RewardSilverCurrencyColor = new ColorBGRA(255, 0, 255, 255);
            RewardSilverCurrencyQualityColor = new ColorBGRA(210, 210, 210, 255);
            RewardSilverJewelryUniqueColor = new ColorBGRA(175, 96, 37, 255);
            RewardSilverDivinationColor = new ColorBGRA(250, 250, 0, 255);
            RewardSilverUniqueOneColor = new ColorBGRA(175, 96, 37, 255);
            RewardSilverUniqueTwoColor = new ColorBGRA(175, 96, 37, 255);
            RewardSilverUniqueThreeColor = new ColorBGRA(175, 96, 37, 255);






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
        [Menu("Treasure Key", 9256, 111)]
        public ColorNode TreasureKeyChestColor { get; set; }
        [Menu("Trinkets", 4793, 111)]
        public ColorNode TrinketChestColor { get; set; }
        [Menu("Specific Unique", 48567, 111)]
        public ColorNode SpecificUniqueChestColor { get; set; }
        [Menu("Currency", 4385, 111)]
        public ColorNode RewardCurrencyColor { get; set; }
        [Menu("Currency Quality", 453, 111)]
        public ColorNode RewardCurrencyQualityColor { get; set; }
        [Menu("Danger Cur", 9987, 111)]
        public ColorNode RewardDangerCurrencyColor { get; set; }
        [Menu("Danger Cur Quality", 4536, 111)]
        public ColorNode RewardDangerCurrencyQualityColor { get; set; }
        [Menu("Danger Unique", 3654, 111)]
        public ColorNode RewardDangerUniqueColor { get; set; }
        [Menu("Danger Divination", 45337, 111)]
        public ColorNode RewardDangerDivinationColor { get; set; }
        [Menu("Silver Cur", 78963, 111)]
        public ColorNode RewardSilverCurrencyColor { get; set; }
        [Menu("Silver Cur Quality", 215487, 111)]
        public ColorNode RewardSilverCurrencyQualityColor { get; set; }
        [Menu("Silver Jewelry Unique", 5487, 111)]
        public ColorNode RewardSilverJewelryUniqueColor { get; set; }
        [Menu("Silver Divination", 4144, 111)]
        public ColorNode RewardSilverDivinationColor { get; set; }
        [Menu("Silver Unique 1", 401586, 111)]
        public ColorNode RewardSilverUniqueOneColor { get; set; }
        [Menu("Silver Unique 2", 43949, 111)]
        public ColorNode RewardSilverUniqueTwoColor { get; set; }
        [Menu("Silver Unique 3", 34877, 111)]
        public ColorNode RewardSilverUniqueThreeColor { get; set; }

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
        #region Debug-ish
        [Menu("Debug-ish", 99)]
        public ToggleNode Debug { get; set; }
        [Menu("Text Size", 991, 99)]
        public RangeNode<int> DebugTextSize { get; set; }
        #endregion
    }
}