using PoeHUD.Hud.Settings;
using PoeHUD.Plugins;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            LabyrinthChestIcon = new RangeNode<int>(18, 1, 50);
            LabyrinthChestColor = new ColorBGRA(0, 128, 255, 255);
            #endregion
            #region DarkShrines
            Darkshrines = true;
            DarkshrinesIcon = new RangeNode<int>(18, 1, 50);
            DarkshrinesColor = new ColorBGRA(0, 128, 255, 255);
            #endregion
            #region Traps
            Roombas = true;
            RoombasColor = new ColorBGRA(255, 255, 255, 255);
            Spinners = true;
            SpinnersColor = new ColorBGRA(255, 255, 255, 255);
            Saws = true;
            SawsColor = new ColorBGRA(255, 255, 255, 255);
            SecretSwitch = true;
            SecretSwitchColor = new ColorBGRA(255, 255, 255, 255);
            Delivery = true;
            DeliveryColor = new ColorBGRA(255, 255, 255, 255);
            PressurePlates = true;
            PressurePlatesColor = new ColorBGRA(255, 255, 255, 255);
            Arrows = true;
            ArrowColor = new ColorBGRA(255, 255, 255, 255);
            HiddenDoorway = true;
            HiddenDoorwayIcon = new RangeNode<int>(18, 1, 50);
            HiddenDoorwayColor = new ColorBGRA(255, 255, 255, 255);
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
            NormalShrinesIcon = new RangeNode<int>(18, 1, 50);
            NormalShrinesColor = new ColorBGRA(0, 128, 255, 255);
            LesserShrines = true;
            LesserShrineOnFloor = true;
            LesserShrineOnFloorSize = new RangeNode<int>(100, 1, 200);
            LesserShrineOnMap = true;
            LesserShrinesIcon = new RangeNode<int>(18, 1, 50);
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
        [Menu("Chest Size", 1111, 111)]
        public RangeNode<int> LabyrinthChestIcon { get; set; }
        [Menu("Chest Color", 1112, 111)]
        public ColorNode LabyrinthChestColor { get; set; }

        // 11-2
        [Menu("Dark Shrines", 112, 11)]
        public ToggleNode Darkshrines { get; set; }
        [Menu("Shrine Size", 1121, 112)]
        public RangeNode<int> DarkshrinesIcon { get; set; }
        [Menu("Shrine Color", 1122, 112)]
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