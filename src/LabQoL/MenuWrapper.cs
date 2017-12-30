using System;
using PoeHUD.Hud.Menu;
using PoeHUD.Hud.Settings;

namespace SithylisQoL
{
    internal class MenuWrapper
    {
        public static MenuItem AddMenu<T>(string menuName, MenuItem parent, RangeNode<T> setting, string toolTip = "")
            where T : struct
        {
            var item = new Picker<T>(menuName, setting);
            if (toolTip != "")
                item.TooltipText = toolTip;

            parent.AddChild(item);

            return item;
        }

        public static MenuItem AddMenu(string text, MenuItem parent, string toolTip = "")
        {
            var item = MenuPlugin.AddChild(parent, text);
            if (toolTip != "")
                item.TooltipText = toolTip;

            return item;
        }

        public static MenuItem AddMenu(string text, MenuItem parent, HotkeyNode node, string toolTip = "")
        {
            var item = new HotkeyButton(text, node);
            if (toolTip != "")
                item.TooltipText = toolTip;

            parent.AddChild(item);
            return item;
        }

        public static MenuItem AddMenu(string text, MenuItem parent, ButtonNode node, string toolTip = "")
        {
            var item = new ButtonButton(text, node);
            if (toolTip != "")
                item.TooltipText = toolTip;

            parent.AddChild(item);
            return item;
        }

        public static ListButton AddMenu(string text, MenuItem parent, ListNode node, string toolTip = "")
        {
            var item = new ListButton(text, node);
            if (toolTip != "")
                item.TooltipText = toolTip;

            parent.AddChild(item);
            node.SettingsListButton = item;
            return item;
        }

        public static MenuItem AddMenu(string text, MenuItem parent, ToggleNode node, string toolTip = "",
            string key = null, Func<MenuItem, bool> hide = null)
        {
            var item = new ToggleButton(parent, text, node, key, hide);
            if (toolTip != "")
                item.TooltipText = toolTip;

            parent.AddChild(item);
            return item;
        }

        public static void AddMenu(FileNode path, MenuItem parent, string toolTip = "")
        {
            var item = new FileButton(path);
            if (toolTip != "")
                item.TooltipText = toolTip;

            parent.AddChild(item);
        }

        public static MenuItem AddMenu(string text, MenuItem parent, ColorNode node, string toolTip = "")
        {
            var item = new ColorButton(text, node);
            if (toolTip != "")
                item.TooltipText = toolTip;

            parent.AddChild(item);
            return item;
        }
    }
}