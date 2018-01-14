using PoeHUD.Framework.Helpers;
using PoeHUD.Models;
using PoeHUD.Plugins;
using PoeHUD.Poe.Components;
using SharpDX;

namespace Random_Features.Libs
{
    public class LocalPlayer
    {
        public static EntityWrapper Entity     => BasePlugin.API.GameController.Player;
        public static long          Experience => Entity.GetComponent<Player>().XP;
        public static string        Name       => Entity.GetComponent<Player>().PlayerName;
        public static int           Level      => Entity.GetComponent<Player>().Level;
        public static Life          Health     => Entity.GetComponent<Life>();
        public static AreaInstance  Area       => BasePlugin.API.GameController.Area.CurrentArea;
        public static int           AreaHash   => BasePlugin.API.GameController.Game.IngameState.Data.CurrentAreaHash;

        public static Vector2 PlayerToScreen => BasePlugin.API.GameController.Game.IngameState.Camera.WorldToScreen(Entity.Pos.Translate(0, 0, -170), Entity);

        public static bool HasBuff(string buffName) => Entity.GetComponent<Life>().HasBuff(buffName);
    }
}