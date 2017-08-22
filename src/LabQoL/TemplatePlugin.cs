using PoeHUD.Controllers;
using PoeHUD.Hud;
using PoeHUD.Models;
using PoeHUD.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoeHUD.Hud.UI;
using PoeHUD.Poe.Components;
using SharpDX;
using Map = PoeHUD.Poe.Elements.Map;
using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Framework.Helpers;
using PoeHUD.Poe;
using SharpDX.Direct3D9;
using System.IO;

namespace LabQoL
{
    public class Sithylis_QoL : BaseSettingsPlugin<LabQoLSettings>
    {
        private HashSet<EntityWrapper> entities;

        public override void Initialise()
        {
            MoveAssets();
            entities = new HashSet<EntityWrapper>();
        }

        // Move image file every time just to make sure its there
        private void MoveAssets()
        {
            var File1 = $"{PluginDirectory}\\images\\shrines.png";
            var File1MoveTo = $"{PluginDirectory}\\..\\..\\textures\\shrines.png";

            var File2 = $"{PluginDirectory}\\images\\hidden_door.png";
            var File2MoveTo = $"{PluginDirectory}\\..\\..\\textures\\hidden_door.png";

            File.Copy(File1, File1MoveTo, true);
            File.Copy(File2, File2MoveTo, true);
        }

        public override void Render()
        {
            base.Render();
            DrawToMap();
            DrawAtziriMirrorReflect();
            DrawShrinesOnFloor();

            if (Settings.SecretSwitch)
                DrawTextLabel(Settings.SecretSwitchColor.Value, "Switch", "HiddenDoor_Switch");
            if (Settings.Spinners)
                DrawTextLabel(Settings.SpinnersColor.Value, "Spinner", "LabyrinthSpinner");
            if (Settings.Saws)
                DrawTextLabel(Settings.SawsColor.Value, "Saw Blade", "Labyrinthsawblade");
            if (Settings.Roombas)
                DrawTextLabel(Settings.RoombasColor.Value, "Roomba", "LabyrinthRoomba", -67);
            if (Settings.Roombas)
                DrawTextLabel(Settings.RoombasColor.Value, "Roomba", "LabyrinthflyingRoomba", -180);
            if (Settings.Delivery)
                DrawTextLabel(Settings.DeliveryColor.Value, "Deliver", "labyrinthtrapkeydelivery");
            if (Settings.PressurePlates)
                DrawTextLabel(Settings.PressurePlatesColor.Value, "Pressure Plate", "pressureplate_reset");
            if (Settings.Arrows)
                DrawTextLabel(Settings.ArrowColor.Value, "Arrows", "Labyrintharrowtrap");

            // Debug-ish, shows entity path
            if (Settings.Debug)
                ShowAllPathObjects();

        }

        // Hides unwanted garbage strings
        private static List<string> DebugDoodads = new List<string>
        {
            "elavator",
            "elevator",
            "Hideoutdoodad",
            "worlditem",
        };

        private void ShowAllPathObjects()
        {
            var isThere = false;
            foreach (EntityWrapper entity in entities)
            {
                for (var i = 0; i < DebugDoodads.Count; i++)
                {
                    if (entity.Path.ToLower().Contains(DebugDoodads[i].ToLower()))
                        isThere = true;
                }

                if (!isThere)
                {
                    Camera camera = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords != new Vector2())

                    {
                        var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                        float maxWidth = 0;
                        float maxheight = 0;

                        var size = Graphics.DrawText(entity.Path, Settings.DebugTextSize, iconRect, Color.White, FontDrawFlags.Center);
                        chestScreenCoords.Y += size.Height;
                        maxheight += size.Height;
                        maxWidth = Math.Max(maxWidth, size.Width);

                        var background = new RectangleF(chestScreenCoords.X - (maxWidth / 2) - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                        Graphics.DrawBox(background, Color.Black);
                    }
                }
                isThere = false;
            }
        }

        private void DrawTextLabel(ColorBGRA color, string text, string path)
        {
            foreach (EntityWrapper entity in entities)
            {
                if (entity.Path.ToLower().Contains(path.ToLower()))
                {
                    Camera camera = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords != new Vector2())

                    {
                        var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                        float maxWidth = 0;
                        float maxheight = 0;

                        var size = Graphics.DrawText(text, 16, iconRect, color, FontDrawFlags.Center);
                        chestScreenCoords.Y += size.Height;
                        maxheight += size.Height;
                        maxWidth = Math.Max(maxWidth, size.Width);

                        var background = new RectangleF(chestScreenCoords.X - (maxWidth / 2) - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                        Graphics.DrawBox(background, Color.Black);

                    }
                }
            }
        }
        private void DrawTextLabel(ColorBGRA color, string text, string path, int zOffset)
        {
            foreach (EntityWrapper entity in entities)
            {
                if (entity.Path.ToLower().Contains(path.ToLower()))
                {
                    Camera camera = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, zOffset), entity);
                    if (chestScreenCoords != new Vector2())

                    {
                        var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                        float maxWidth = 0;
                        float maxheight = 0;

                        var size = Graphics.DrawText(text, 16, iconRect, color, FontDrawFlags.Center);
                        chestScreenCoords.Y += size.Height;
                        maxheight += size.Height;
                        maxWidth = Math.Max(maxWidth, size.Width);

                        var background = new RectangleF(chestScreenCoords.X - (maxWidth / 2) - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);

                        Graphics.DrawBox(background, Color.Black);

                    }
                }
            }
        }

        private void DrawAtziriMirrorReflect()
        {
            if (!Settings.Atziri)
                return;

            foreach (EntityWrapper entity in entities)
            {
                if (entity.Path.Contains("Metadata/Monsters/Atziri/AtziriMirror"))
                {
                    Camera camera = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, -90), entity);
                    if (chestScreenCoords != new Vector2())
                    {
                        // create rect at chest location to draw icon
                        var iconRect = new RectangleF(chestScreenCoords.X - Settings.AtziriMirrorSize / 2, chestScreenCoords.Y - Settings.AtziriMirrorSize / 2, Settings.AtziriMirrorSize, Settings.AtziriMirrorSize);
                        Graphics.DrawPluginImage(PluginDirectory + @"\images\mirror.png", iconRect);
                    }
                }
            }
        }

        private void DrawShrinesOnFloor()
        {
            if (Settings.LesserShrines && Settings.LesserShrineOnFloor)
            {
                foreach (EntityWrapper entity in entities)
                {
                    if (entity.Path.Contains("Metadata/Shrines/LesserShrine"))
                    {
                        Camera camera = GameController.Game.IngameState.Camera;
                        Vector2 Coords = camera.WorldToScreen(entity.Pos.Translate(0, 0, -90), entity);
                        if (Coords != new Vector2())
                        {
                            // create rect at chest location to draw icon
                            var iconRect = new RectangleF(Coords.X - Settings.LesserShrineOnFloorSize / 2, Coords.Y - Settings.LesserShrineOnFloorSize / 2, Settings.LesserShrineOnFloorSize, Settings.LesserShrineOnFloorSize);
                            Graphics.DrawImage("shrines.png", iconRect, Settings.LesserShrinesColor);
                        }
                    }
                }
            }

            if (Settings.NormalShrines && Settings.NormalShrineOnFloor)
            {
                foreach (EntityWrapper entity in entities)
                {
                    if (entity.Path.Contains("Metadata/Shrines/Shrine"))
                    {
                        Camera camera = GameController.Game.IngameState.Camera;
                        Vector2 Coords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                        if (Coords != new Vector2())
                        {
                            // create rect at chest location to draw icon
                            var iconRect = new RectangleF(Coords.X - Settings.NormalShrineOnFloorSize / 2, Coords.Y - Settings.NormalShrineOnFloorSize / 2, Settings.NormalShrineOnFloorSize, Settings.NormalShrineOnFloorSize);
                            Graphics.DrawImage("shrines.png", iconRect, Settings.NormalShrinesColor);
                        }
                    }
                }
            }
        }

        private void DrawToMap()
        {
            if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
            {
                foreach (EntityWrapper entity in entities)
                {
                    DrawToLargeMiniMap(entity);
                }
            }
            else if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
            {
                foreach (EntityWrapper entity in entities)
                {
                    DrawToSmallMiniMap(entity);
                }
            }
        }

        public override void EntityAdded(EntityWrapper entity)
        {
            entities.Add(entity);
        }

        public override void EntityRemoved(EntityWrapper entity)
        {
            entities.Remove(entity);
        }

        private void DrawToSmallMiniMap(EntityWrapper entity)
        {
            MapIcon icon = GetMapIcon(entity);

            if (icon != null)
            {
                Element smallMinimap = GameController.Game.IngameState.IngameUi.Map.SmallMinimap;
                Vector2 playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
                float posZ = GameController.Player.GetComponent<Render>().Z;

                const float SCALE = 240f;
                RectangleF mapRect = smallMinimap.GetClientRect();
                var mapCenter = new Vector2(mapRect.X + mapRect.Width / 2, mapRect.Y + mapRect.Height / 2).Translate(0, 0);
                double diag = Math.Sqrt(mapRect.Width * mapRect.Width + mapRect.Height * mapRect.Height) / 2.0;
                float iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
                Vector2 point = mapCenter
                    + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, SCALE, (iconZ - posZ) / 20);

                HudTexture texture = icon.TextureIcon;
                int size = icon.Size;
                var rect = new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size);
                bool isContain;
                mapRect.Contains(ref rect, out isContain);
                if (isContain)
                {
                    texture.Draw(Graphics, rect);
                }
            }
        }

        private void DrawToLargeMiniMap(EntityWrapper entity)
        {
            Element smallMinimap = GameController.Game.IngameState.IngameUi.Map.SmallMinimap;
            MapIcon icon = GetMapIcon(entity);
            if (icon != null)
            {
                Camera camera = GameController.Game.IngameState.Camera;
                Map mapWindow = GameController.Game.IngameState.IngameUi.Map;
                RectangleF mapRect = mapWindow.GetClientRect();

                Vector2 playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
                float posZ = GameController.Player.GetComponent<Render>().Z;
                Vector2 screenCenter = new Vector2(mapRect.Width / 2, mapRect.Height / 2).Translate(0, -20) + new Vector2(mapRect.X, mapRect.Y) + new Vector2(mapWindow.ShiftX, mapWindow.ShiftY);
                var diag = (float)Math.Sqrt(camera.Width * camera.Width + camera.Height * camera.Height);
                float k = camera.Width < 1024f ? 1120f : 1024f;
                float scale = k / camera.Height * camera.Width * 3 / 4;

                float iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
                Vector2 point = screenCenter
                    + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, scale, (iconZ - posZ) / 20);

                HudTexture texture = icon.TextureIcon;
                int size = icon.SizeOfLargeIcon.GetValueOrDefault(icon.Size * 2);
                texture.Draw(Graphics, new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size));
            }
        }


        private static readonly List<string> LabyrinthRewrdChests = new List<string>
        {
            "Metadata/Chests/Labyrinth/LabyrinthTreasureKey",
            "Metadata/Chests/Labyrinth/LabyrinthSpecificUnique",
            "Metadata/Chests/Labyrinth/LabyrinthReward0Currency",
            "Metadata/Chests/Labyrinth/LabyrinthReward0CurrencyQuality",

            "Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrency",
            "Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrencyQuality",
            "Metadata/Chests/Labyrinth/LabyrinthRewardDangerUnique",
            "Metadata/Chests/Labyrinth/LabyrinthRewardDangerDivination",

            "Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrency",
            "Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrencyQuality",
            "Metadata/Chests/Labyrinth/LabyrinthRewardSilverJewelryUnique",
            "Metadata/Chests/Labyrinth/LabyrinthRewardSilverDivination",
            
            "Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique1",
            "Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique2",
            "Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique3",
        };

        private MapIcon GetMapIcon(EntityWrapper e)
        {
            if (LabyrinthRewrdChests.Contains(e.Path) && Settings.LabyrinthChest)
            {
                return new MapIcon(e, new HudTexture("strongbox.png", Settings.LabyrinthChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestIcon);
            }
            if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden") && Settings.Darkshrines)
            {
                return new MapIcon(e, new HudTexture("shrines.png", Settings.DarkshrinesColor), () => Settings.Darkshrines, Settings.DarkshrinesIcon);
            }
            if (e.Path.Contains("Metadata/Shrines/Shrine") && Settings.NormalShrines && Settings.NormalShrineOnMap)
            {
                return new MapIcon(e, new HudTexture("shrines.png", Settings.NormalShrinesColor), () => Settings.NormalShrineOnMap, Settings.NormalShrinesIcon);
            }
            if (e.Path.Contains("Metadata/Shrines/LesserShrine") && Settings.LesserShrines && Settings.LesserShrineOnMap)
            {
                return new MapIcon(e, new HudTexture("shrines.png", Settings.LesserShrinesColor), () => Settings.LesserShrineOnMap, Settings.LesserShrinesIcon);
            }
            if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Short") && Settings.HiddenDoorway)
            {
                return new MapIcon(e, new HudTexture("hidden_door.png", Settings.HiddenDoorwayColor), () => Settings.HiddenDoorway, Settings.HiddenDoorwayIcon);
            }
            if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Long") && Settings.HiddenDoorway)
            {
                return new MapIcon(e, new HudTexture("hidden_door.png", Settings.HiddenDoorwayColor), () => Settings.HiddenDoorway, Settings.HiddenDoorwayIcon);
            }
            return null;
        }
    }
}