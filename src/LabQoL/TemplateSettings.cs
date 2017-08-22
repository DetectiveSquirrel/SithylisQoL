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

            LabyrinthChest = true;
            LabyrinthChestIcon = new RangeNode<int>(18, 1, 28);
            LabyrinthChestColor = new ColorBGRA(0, 128, 255, 255);

            Darkshrines = true;
            DarkshrinesIcon = new RangeNode<int>(18, 1, 28);
            DarkshrinesColor = new ColorBGRA(0, 128, 255, 255);

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

            Atziri = true;
            AtziriMirrorSize = new RangeNode<int>(100, 1, 200);

            NormalShrines = true;
            NormalShrinesIcon = new RangeNode<int>(18, 1, 28);
            NormalShrinesColor = new ColorBGRA(0, 128, 255, 255);
            LesserShrines = true;
            LesserShrinesIcon = new RangeNode<int>(18, 1, 28);
            LesserShrinesColor = new ColorBGRA(0, 128, 255, 255);

            Debug = false;
            DebugTextSize = new RangeNode<int>(12, 1, 50);
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

        [Menu("Atziri", 22)]
        public EmptyNode AtziriEmpty { get; set; }
        [Menu("Show Reflection", 221, 22)]
        public ToggleNode Atziri { get; set; }
        [Menu("Atziri Mirror Size", 2211, 221)]
        public RangeNode<int> AtziriMirrorSize { get; set; }


        [Menu("Shrines", 33)]
        public EmptyNode Shrines { get; set; }

        [Menu("Normal Shrines", 331, 33)]
        public ToggleNode NormalShrines { get; set; }
        [Menu("Size", 3311, 331)]
        public RangeNode<int> NormalShrinesIcon { get; set; }
        [Menu("Color", 3312, 331)]
        public ColorNode NormalShrinesColor { get; set; }

        [Menu("Lesser Shrines", 332, 33)]
        public ToggleNode LesserShrines { get; set; }
        [Menu("Size", 3321, 332)]
        public RangeNode<int> LesserShrinesIcon { get; set; }
        [Menu("Color", 3322, 332)]
        public ColorNode LesserShrinesColor { get; set; }


        [Menu("Debug-ish", 99)]
        public ToggleNode Debug { get; set; }
        [Menu("Text Size", 991, 99)]
        public RangeNode<int> DebugTextSize { get; set; }
    }
}