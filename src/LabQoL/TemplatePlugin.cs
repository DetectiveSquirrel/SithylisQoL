using PoeHUD.Controllers;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud;
using PoeHUD.Hud.UI;
using PoeHUD.Models;
using PoeHUD.Plugins;
using PoeHUD.Poe;
using PoeHUD.Poe.Components;
using PoeHUD.Poe.RemoteMemoryObjects;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.IO;
using Map = PoeHUD.Poe.Elements.Map;

namespace LabQoL
{
    public class Sithylis_QoL : BaseSettingsPlugin<LabQoLSettings>
    {
        private HashSet<EntityWrapper> entities;
        public string imagePath;

        public override void Initialise()
        {
            entities = new HashSet<EntityWrapper>();
            imagePath = PluginDirectory + @"\images\";
        }

        public override void Render()
        {
            base.Render();
            DrawToMap();
            DrawAtziriMirrorReflect();
            DrawLieutenantSkeletonThorns();
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
                DrawTextLabel(Settings.PressurePlatesColor.Value, "Pressure" + Environment.NewLine + "Plate", "pressureplate_reset");

            if (Settings.Arrows)
                DrawTextLabel(Settings.ArrowColor.Value, "Arrows", "Labyrintharrowtrap");

            if (Settings.SmashableDoor)
                DrawTextLabel(Settings.SmashableDoorColor.Value, "Smashable" + Environment.NewLine + "Door", "Objects/LabyrinthSmashableDoor");

            if (Settings.SecretPassage)
                DrawTextLabelEquals(Settings.SecretPassageColor.Value, "Secret" + Environment.NewLine + "Passage", "Metadata/Terrain/Labyrinth/Objects/SecretPassage");
            
            // Debug-ish things
            if (Settings.Debug)
            {
                CountMonstersAroundMe(400);
                ShowAllPathObjects();
            }

        }

        private int GetEntityDistance(EntityWrapper entity)
        {
            Positioned PlayerPosition = GameController.Player.GetComponent<Positioned>();
            Positioned MonsterPosition = entity.GetComponent<Positioned>();
            double distanceToEntity = Math.Sqrt(Math.Pow(PlayerPosition.X - MonsterPosition.X, 2) + Math.Pow(PlayerPosition.Y - MonsterPosition.Y, 2));

            return (int)distanceToEntity;
        }

        private void CountMonstersAroundMe(int distance)
        {
            Camera camera = GameController.Game.IngameState.Camera;
            int halfW = camera.Width / 2;
            Vector2 textPos = new Vector2(halfW, 40);

            Color textColor = Color.White;

            var MonsterCount = 0;

            foreach (EntityWrapper entity in entities)
            {
                if (entity.HasComponent<Monster>())
                {
                    if (entity.IsHostile && entity.IsAlive)
                    {
                        if (GetEntityDistance(entity) <= distance)
                        {
                            MonsterCount++;
                            textColor = Color.Green;
                        }
                        Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                        if (chestScreenCoords != new Vector2())
                        {
                            var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                            float maxWidth = 0;
                            float maxheight = 0;

                            var size = Graphics.DrawText(GetEntityDistance(entity).ToString(), 16, iconRect, textColor, FontDrawFlags.Center);
                            chestScreenCoords.Y += size.Height;
                            maxheight += size.Height;
                            maxWidth = Math.Max(maxWidth, size.Width);

                            var background = new RectangleF(chestScreenCoords.X - (maxWidth / 2) - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                            Graphics.DrawBox(background, Color.Black);
                        }
                    }
                }
                textColor = Color.White;
            }

            Graphics.DrawText("Hostile Monster Count Within [" + distance + "] Units: " + MonsterCount, 20, textPos, FontDrawFlags.Center);
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

        private void DrawTextLabelEquals(ColorBGRA color, string text, string path)
        {
            foreach (EntityWrapper entity in entities)
            {
                if (entity.Path.ToLower().Equals(path.ToLower()))
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
        private void DrawTextLabelEquals(ColorBGRA color, string text, string path, int zOffset)
        {
            foreach (EntityWrapper entity in entities)
            {
                if (entity.Path.ToLower().Equals(path.ToLower()))
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

        private void DrawLieutenantSkeletonThorns()
        {
            if (!Settings.LieutenantofRage)
                return;

            foreach (EntityWrapper entity in entities)
            {
                if (entity.Path.Contains("Metadata/Monsters/Labyrinth/LabyrinthLieutenantSkeletonMeleeIce") || entity.Path.Contains("Metadata/Monsters/Labyrinth/LabyrinthLieutenantSkeletonMeleeIce2"))
                {
                    Camera camera = GameController.Game.IngameState.Camera;
                    Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, -90), entity);
                    if (chestScreenCoords != new Vector2())
                    {
                        // create rect at chest location to draw icon
                        var iconRect = new RectangleF(chestScreenCoords.X - Settings.LieutenantofRageSize / 2, chestScreenCoords.Y - Settings.LieutenantofRageSize / 2, Settings.AtziriMirrorSize, Settings.LieutenantofRageSize);
                        Graphics.DrawPluginImage(imagePath + "mirror.png", iconRect);
                    }
                }
            }
        }

        private void DrawShrinesOnFloor()
        {
            foreach (EntityWrapper entity in entities)
            {
                if (Settings.LesserShrines && Settings.LesserShrineOnFloor)
                {
                    if (entity.GetComponent<Shrine>().IsAvailable && entity.Path.Contains("Metadata/Shrines/LesserShrine"))
                    {
                        Camera camera = GameController.Game.IngameState.Camera;
                        Vector2 Coords = camera.WorldToScreen(entity.Pos.Translate(0, 0, -90), entity);
                        if (Coords != new Vector2())
                        {
                            // create rect at chest location to draw icon
                            var iconRect = new RectangleF(Coords.X - Settings.LesserShrineOnFloorSize / 2, Coords.Y - Settings.LesserShrineOnFloorSize / 2, Settings.LesserShrineOnFloorSize, Settings.LesserShrineOnFloorSize);
                            Graphics.DrawPluginImage(imagePath + "shrines.png", iconRect, Settings.LesserShrinesColor);
                        }
                    }
                }

                if (Settings.NormalShrines && Settings.NormalShrineOnFloor)
                {
                    if (entity.GetComponent<Shrine>().IsAvailable && entity.Path.Contains("Metadata/Shrines/Shrine"))
                    {
                        Camera camera = GameController.Game.IngameState.Camera;
                        Vector2 Coords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                        if (Coords != new Vector2())
                        {
                            // create rect at chest location to draw icon
                            var iconRect = new RectangleF(Coords.X - Settings.NormalShrineOnFloorSize / 2, Coords.Y - Settings.NormalShrineOnFloorSize / 2, Settings.NormalShrineOnFloorSize, Settings.NormalShrineOnFloorSize);
                            Graphics.DrawPluginImage(imagePath + "shrines.png", iconRect, Settings.NormalShrinesColor);
                        }
                    }
                }

                if (Settings.Darkshrines && Settings.DarkshrinesOnFloor)
                {
                    if (entity.Path.Contains("Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden"))
                    {
                        Camera camera = GameController.Game.IngameState.Camera;
                        Vector2 Coords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                        if (Coords != new Vector2())
                        {
                            // create rect at chest location to draw icon
                            var iconRect = new RectangleF(Coords.X - Settings.DarkshrinesOnFloorSize / 2, Coords.Y - Settings.DarkshrinesOnFloorSize / 2, Settings.DarkshrinesOnFloorSize, Settings.DarkshrinesOnFloorSize);
                            Graphics.DrawPluginImage(imagePath + "shrines.png", iconRect, Settings.DarkshrinesColor);
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
                Vector2 point = mapCenter + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, SCALE, (iconZ - posZ) / 20);
                HudTexture texture = icon.TextureIcon;
                int size = icon.Size;
                var rect = new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size);
                bool isContain;
                mapRect.Contains(ref rect, out isContain);
                if (isContain)
                {
                    texture.DrawPluginImage(Graphics, rect);
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
                Vector2 point = screenCenter + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, scale, (iconZ - posZ) / 20);

                HudTexture texture = icon.TextureIcon;
                int size = icon.SizeOfLargeIcon.GetValueOrDefault(icon.Size * 2);
                texture.DrawPluginImage(Graphics, new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size));
            }
        }

        private MapIcon GetMapIcon(EntityWrapper e)
        {
            if (Settings.LabyrinthChest && !e.GetComponent<Chest>().IsOpened)
            {
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTreasureKey"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.TreasureKeyChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTrinketChest"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.TrinketChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthSpecificUnique"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.SpecificUniqueChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0Currency"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0CurrencyQuality"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrency"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardDangerCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrencyQuality"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardDangerCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerUnique"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardDangerUniqueColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerDivination"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardDangerDivinationColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrency"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardSilverCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrencyQuality"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardSilverCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverJewelryUnique"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardSilverJewelryUniqueColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverDivination"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardSilverDivinationColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique1"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardSilverUniqueOneColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique3"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardSilverUniqueTwoColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique2"))
                    return new MapIcon(e, new HudTexture("strongbox.png", Settings.RewardSilverUniqueThreeColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
            }

            if (Settings.Darkshrines)
                if (Settings.DarkshrinesOnMap)
                    if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden"))
                        return new MapIcon(e, new HudTexture(imagePath + "shrines.png", Settings.DarkshrinesColor), () => Settings.DarkshrinesOnMap, Settings.DarkshrinesIcon);

            if (Settings.NormalShrines)
                if (Settings.NormalShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/Shrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(e, new HudTexture(imagePath + "shrines.png", Settings.NormalShrinesColor), () => Settings.NormalShrineOnMap, Settings.NormalShrinesIcon);

            if (Settings.LesserShrines)
                if (Settings.LesserShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/LesserShrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(e, new HudTexture(imagePath + "shrines.png", Settings.LesserShrinesColor), () => Settings.LesserShrineOnMap, Settings.LesserShrinesIcon);

            if (Settings.HiddenDoorway)
                if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Short") || e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Long"))
                    return new MapIcon(e, new HudTexture(imagePath + "hidden_door.png", Settings.HiddenDoorwayColor), () => Settings.HiddenDoorway, Settings.HiddenDoorwayIcon);

            if (Settings.SecretPassage)
                if (e.Path.Equals("Metadata/Terrain/Labyrinth/Objects/SecretPassage"))
                    return new MapIcon(e, new HudTexture(imagePath + "hidden_door.png", Settings.SecretPassageColor), () => Settings.SecretPassage, Settings.SecretPassageIcon);

            if (e.Path.Contains("Transition"))
                return new MapIcon(e, new HudTexture(imagePath + "hidden_door.png", Color.Orange), () => true, Settings.SecretPassageIcon);

            return null;
        }
    }
}