using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Helpers;
using SharpDX;

namespace Random_Features.Libs
{
    public class LocalPlayer
    {
        public static Entity LocalPlayerEntity => GameState.pTheGame.IngameState.Data.LocalPlayer;    
        public static long          Experience => LocalPlayerEntity.GetComponent<Player>().XP;
        public static string        Name       => LocalPlayerEntity.GetComponent<Player>().PlayerName;
        public static int           Level      => LocalPlayerEntity.GetComponent<Player>().Level;
        public static Life          Health     => LocalPlayerEntity.GetComponent<Life>();
        public static Vector2 PlayerToScreen => GameState.pTheGame.IngameState.Camera.WorldToScreen(LocalPlayerEntity.Pos.Translate(0, 0, -170));

        public static object Entity { get; internal set; }

        public static bool HasBuff(string buffName) => LocalPlayerEntity.GetComponent<Life>().HasBuff(buffName);
    }
}