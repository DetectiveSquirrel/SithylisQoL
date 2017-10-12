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
using System.Threading;
using Map = PoeHUD.Poe.Elements.Map;

namespace LabQoL
{
    public class Sithylis_QoL : BaseSettingsPlugin<LabQoLSettings>
    {
        private HashSet<EntityWrapper> entities;
        public string CustomImagePath;
        public string PoeHUDImageLocation;

        public override void Initialise()
        {
            entities = new HashSet<EntityWrapper>();
            CustomImagePath = PluginDirectory + @"\images\";
            PoeHUDImageLocation = PluginDirectory + @"\..\..\textures\";
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
                DrawTextLabel(Settings.SmashableDoorColor.Value, "Smash Door", "Objects/LabyrinthSmashableDoor");

            if (Settings.SecretPassage)
                DrawTextLabelEquals(Settings.SecretPassageColor.Value, "Secret" + Environment.NewLine + "Passage", "Metadata/Terrain/Labyrinth/Objects/SecretPassage");

            if (Settings.Specters)
            {
                if (Settings.Tukohamas_Vanguard)
                    DrawTextLabelSpecter(Color.Yellow, "Tukohama's" + Environment.NewLine + "Vanguard", "Metadata/Monsters/KaomWarrior/KaomWarrior7", 120);
                if (Settings.WickerMan)
                    DrawTextLabelSpecter(Color.Yellow, "WickerMan", "Metadata/Monsters/WickerMan/WickerMan", 120);
                if (Settings.Solar_Guard)
                    DrawTextLabelSpecter(Color.Yellow, "Solar Guard", "Metadata/Monsters/HolyFireElemental/HolyFireElementalSolarisBeam", 120);
            }

            if (Settings.Sentinels)
            {
                if (Settings.UnendingLethargy)
                    DrawTextLabel(Settings.UnendingLethargyColor.Value, "Slowing", "LabyrinthPopUpTotemSlow");

                if (Settings.EndlessDrought)
                    DrawTextLabel(Settings.EndlessDroughtColor.Value, $"Flask Charge{Environment.NewLine}Removal", "LabyrinthPopUpTotemFlaskChargeNova");

                if (Settings.EndlessHazard)
                    DrawTextLabel(Settings.EndlessHazardColor.Value, "Movement Damage", "LabyrinthPopUpTotemDamageTakenOnMovementSkillUse");

                if (Settings.EndlessPain)
                    DrawTextLabel(Settings.EndlessPainColor.Value, "Inc Damage Taken", "LabyrinthPopUpTotemIncreasedDamageTaken");

                if (Settings.EndlessSting)
                    DrawTextLabel(Settings.EndlessStingColor.Value, "Bleeding", "LabyrinthPopUpTotemBleedNova");

                if (Settings.UnendingFire)
                    DrawTextLabel(Settings.UnendingFireColor.Value, "Fire Nova", "LabyrinthPopUpTotemMonsterFire");

                if (Settings.UnendingFrost)
                    DrawTextLabel(Settings.UnendingFrostColor.Value, "Ice Nova", "LabyrinthPopUpTotemMonsterIceNova");

                if (Settings.UnendingStorm)
                    DrawTextLabel(Settings.UnendingStormColor.Value, "Shock Nova", "LabyrinthPopUpTotemMonsterLightning");
            }

            // Debug-ish things
            if (Settings.Debug)
            {
                CountMonstersAroundMe(400);
                ShowAllPathObjects();

                Element buffBar = GameController.Game.IngameState.UIRoot.GetChildAtIndex(1).GetChildAtIndex(8);
                int i = 0;
                int i2 = 0;
                // Graphics.DrawFrame(buffBar.GetClientRect(), 2, Color.Gold);
                foreach (Element ui in buffBar.Children)
                {
                    foreach (Element ui2 in ui.Children)
                    {
                        if (ui2.IsVisible)
                        {
                            foreach (Element ui3 in ui2.Children)
                            {
                                if (ui3.IsVisible)
                                {
                                    Graphics.DrawFrame(ui3.GetClientRect(), 2, Color.Red);
                                    //Graphics.DrawText(ui3.vTable.ToString(), 15, ui3.GetClientRect().Center, FontDrawFlags.Center);
                                }
                                i2 += 15;
                            }
                            //Graphics.DrawFrame(ui2.GetClientRect(), 2, Color.Orange);
                            //Graphics.DrawText(i.ToString() + "-" + i2.ToString(), 15, ui.GetClientRect().Center, FontDrawFlags.Center);
                        }
                        //i2++;
                    }
                }
            }

            if (Settings.LimitPoeHudFPS && Settings.fps.Value >= 1)
            {
                Thread.Sleep(1000 / Settings.fps.Value);
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
                        Vector2 chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, +20), entity);
                        if (chestScreenCoords != new Vector2())
                        {
                            var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                            float maxWidth = 0;
                            float maxheight = 0;

                            var size = Graphics.DrawText("Distance[" + GetEntityDistance(entity).ToString() + "]", 16, iconRect, textColor, FontDrawFlags.Center);
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

        private void DrawTextLabelSpecter(ColorBGRA color, string text, string path, int zOffset = 0)
        {
            foreach (EntityWrapper entity in entities)
            {
                if (entity.Path.ToLower().Contains(path.ToLower()) && !entity.IsAlive)
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
                        Graphics.DrawPluginImage(CustomImagePath + "mirror.png", iconRect);
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
                            Graphics.DrawPluginImage(CustomImagePath + "shrines.png", iconRect, Settings.LesserShrinesColor);
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
                            Graphics.DrawPluginImage(CustomImagePath + "shrines.png", iconRect, Settings.NormalShrinesColor);
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
                            Graphics.DrawPluginImage(CustomImagePath + "shrines.png", iconRect, Settings.DarkshrinesColor);
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
                float size = icon.Size;
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
                Vector2 screenCenter = new Vector2(mapRect.Width / 2, mapRect.Height / 2).Translate(0, -20) + new Vector2(mapRect.X, mapRect.Y)
                    + new Vector2(mapWindow.LargeMapShiftX, mapWindow.LargeMapShiftY);
                var diag = (float)Math.Sqrt(camera.Width * camera.Width + camera.Height * camera.Height);
                float k = camera.Width < 1024f ? 1120f : 1024f;
                float scale = k / camera.Height * camera.Width * 3f / 4f / mapWindow.LargeMapZoom;

                float iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
                Vector2 point = screenCenter
                    + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, scale, (iconZ - posZ) / (9f / mapWindow.LargeMapZoom));

                HudTexture texture = icon.TextureIcon;
                float size = icon.Size * 2;//icon.SizeOfLargeIcon.GetValueOrDefault(icon.Size * 2);
                texture.DrawPluginImage(Graphics, new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size));
            }
        }

        private MapIcon GetMapIcon(EntityWrapper e)
        {
            if (Settings.LabyrinthChest && !e.GetComponent<Chest>().IsOpened)
            {
                #region Normal Chests
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTreasureKey"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.TreasureKeyChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTrinketChest"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.TrinketChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthSpecificUnique"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.SpecificUniqueChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0Currency"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0CurrencyQuality"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                #endregion
                #region Danger Chests
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrency"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardDangerCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrencyQuality"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardDangerCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerUnique"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardDangerUniqueColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerDivination"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardDangerDivinationColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerSkillGems"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardDangerLowGemColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCorruptedVaal"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardDangerCorVaalColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerJewelry"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardDangerJewelleryColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerGeneric"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardDangerGenericColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                #endregion
                #region Silver Chests
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrency"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardSilverCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrencyQuality"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardSilverCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverJewelryUnique"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardSilverJewelryUniqueColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverDivination"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardSilverDivinationColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique1"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardSilverUniqueOneColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique3"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardSilverUniqueTwoColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique2"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardSilverUniqueThreeColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverSkillGems"))
                    return new MapIcon(e, new HudTexture(PoeHUDImageLocation + "strongbox.png", Settings.RewardSilverSkillGemColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                #endregion
            }
            #region Shrines
            if (Settings.Darkshrines)
                if (Settings.DarkshrinesOnMap)
                    if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden"))
                        return new MapIcon(e, new HudTexture(CustomImagePath + "shrines.png", Settings.DarkshrinesColor), () => Settings.DarkshrinesOnMap, Settings.DarkshrinesIcon);

            if (Settings.NormalShrines)
                if (Settings.NormalShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/Shrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(e, new HudTexture(CustomImagePath + "shrines.png", Settings.NormalShrinesColor), () => Settings.NormalShrineOnMap, Settings.NormalShrinesIcon);

            if (Settings.LesserShrines)
                if (Settings.LesserShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/LesserShrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(e, new HudTexture(CustomImagePath + "shrines.png", Settings.LesserShrinesColor), () => Settings.LesserShrineOnMap, Settings.LesserShrinesIcon);
            #endregion
            #region Doorways
            if (Settings.HiddenDoorway)
                if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Short") || e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Long"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.HiddenDoorwayColor), () => Settings.HiddenDoorway, Settings.HiddenDoorwayIcon);

            if (Settings.SecretPassage)
                if (e.Path.Equals("Metadata/Terrain/Labyrinth/Objects/SecretPassage"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.SecretPassageColor), () => Settings.SecretPassage, Settings.SecretPassageIcon);

            if (Settings.AreaTransition)
                if (e.Path.Contains("Transition"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.AreaTransitionColor), () => true, Settings.AreaTransitionIcon);
            #endregion
            return null;
        }
    }
}