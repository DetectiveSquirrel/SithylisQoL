using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud;
using PoeHUD.Models;
using PoeHUD.Plugins;
using PoeHUD.Poe.Components;
using SharpDX;
using SharpDX.Direct3D9;

namespace SithylisQoL
{
    public class SithylisQoL : BaseSettingsPlugin<LabQoLSettings>
    {
        // Hides unwanted garbage strings
        private static readonly List<string> DebugDoodads =
            new List<string> {"elavator", "elevator", "Hideoutdoodad", "worlditem"};

        private HashSet<EntityWrapper> _entityCollection;

        public string CustomImagePath;

        public string PoeHudImageLocation;

        public SithylisQoL()
        {
            PluginName = "Random Features";
        }

        public override void Initialise()
        {
            _entityCollection = new HashSet<EntityWrapper>();
            CustomImagePath = PluginDirectory + @"\images\";
            PoeHudImageLocation = PluginDirectory + @"\..\..\textures\";
        }

        public void DrawImageToWorld(ImageToWorldData information)
        {
            foreach (var entity in _entityCollection)
            foreach (var wantedPath in information.Path)
                if (entity.Path.Contains(wantedPath))
                {
                    var camera = GameController.Game.IngameState.Camera;

                    var chestScreenCoords = camera.WorldToScreen(
                        entity.Pos.Translate(0, 0, information.YOffset),
                        entity);

                    var iconRect = new RectangleF(
                        chestScreenCoords.X - information.ImageSize / 2,
                        chestScreenCoords.Y - information.ImageSize / 2,
                        information.ImageSize,
                        information.ImageSize);

                    if (chestScreenCoords == new Vector2()) continue;
                    // create rect at chest location to draw icon
                    if (information.HasColor)
                        Graphics.DrawPluginImage(information.ImagePath, iconRect, information.Color);

                    else Graphics.DrawPluginImage(information.ImagePath, iconRect);
                }
        }

        public void DrawImageToWorld_Chest(ImageToWorldData information)
        {
            foreach (var entity in _entityCollection)
            foreach (var wantedPath in information.Path)
                if (!entity.GetComponent<Chest>().IsOpened && entity.Path.Contains(wantedPath))
                {
                    var camera = GameController.Game.IngameState.Camera;

                    var chestScreenCoords = camera.WorldToScreen(
                        entity.Pos.Translate(0, 0, information.YOffset),
                        entity);

                    if (chestScreenCoords == new Vector2()) continue;

                    // create rect at chest location to draw icon
                    var iconRect = new RectangleF(
                        (chestScreenCoords.X - information.ImageSize) / 2,
                        (chestScreenCoords.Y - information.ImageSize) / 2,
                        information.ImageSize,
                        information.ImageSize);

                    if (information.HasColor)
                        Graphics.DrawPluginImage(information.ImagePath, iconRect, information.Color);

                    else Graphics.DrawPluginImage(information.ImagePath, iconRect);
                }
        }

        public void DrawImageToWorld_Shrine(ImageToWorldData information)
        {
            foreach (var entity in _entityCollection)
            foreach (var wantedPath in information.Path)
                if (entity.GetComponent<Shrine>().IsAvailable && entity.Path.Contains(wantedPath))
                {
                    var camera = GameController.Game.IngameState.Camera;

                    var chestScreenCoords = camera.WorldToScreen(
                        entity.Pos.Translate(0, 0, information.YOffset),
                        entity);

                    if (chestScreenCoords == new Vector2()) continue;

                    // create rect at chest location to draw icon
                    var iconRect = new RectangleF(
                        chestScreenCoords.X - information.ImageSize / 2,
                        chestScreenCoords.Y - information.ImageSize / 2,
                        information.ImageSize,
                        information.ImageSize);

                    if (information.HasColor)
                        Graphics.DrawPluginImage(information.ImagePath, iconRect, information.Color);
                    else Graphics.DrawPluginImage(information.ImagePath, iconRect);
                }
        }

        public override void EntityAdded(EntityWrapper entity)
        {
            _entityCollection.Add(entity);
        }

        public override void EntityRemoved(EntityWrapper entity)
        {
            _entityCollection.Remove(entity);
        }

        private void ToggleDebug()
        {
            Settings.Debug = !Settings.Debug;
        }

        public override void Render()
        {
            base.Render();

            // DrawAllPaths();

            if (Settings.DebugIshToggleButton.PressedOnce())
                ToggleDebug();

            // Graphics.DrawText(Class_Memory.ReadFloat(API.GameController.Game.IngameState.Camera.Address + 0x204).ToString(), 40, new Vector2(500, 500));
            if (!GameController.Game.IngameState.IngameUi.AtlasPanel.IsVisible &&
                !GameController.Game.IngameState.IngameUi.TreePanel.IsVisible)
            {
                RenderMapImages();
                RenderVaultPiles();
                RenderAtziriMirrorClone();
                RenderLieutenantSkeletonThorns();
                RenderShrines();
            }

            if (Settings.SecretSwitch)
                RenderTextLabel(Settings.SecretSwitchColor.Value, "Switch", "HiddenDoor_Switch");

            if (Settings.Spinners)
                RenderTextLabel(Settings.SpinnersColor.Value, "Spinner", "LabyrinthSpinner");

            if (Settings.Saws)
                RenderTextLabel(Settings.SawsColor.Value, "Saw Blade", "Labyrinthsawblade");

            if (Settings.Roombas)
                RenderTextLabel(Settings.RoombasColor.Value, "Roomba", "LabyrinthRoomba", -67);

            if (Settings.Roombas)
                RenderTextLabel(Settings.RoombasColor.Value, "Roomba", "LabyrinthflyingRoomba", -180);

            if (Settings.Delivery)
                RenderTextLabel(Settings.DeliveryColor.Value, "Deliver", "labyrinthtrapkeydelivery");

            if (Settings.PressurePlates)
                RenderTextLabel(
                    Settings.PressurePlatesColor.Value,
                    "Pressure" + Environment.NewLine + "Plate",
                    "pressureplate_reset");

            if (Settings.Arrows)
                RenderTextLabel(Settings.ArrowColor.Value, "Arrows", "Labyrintharrowtrap");

            if (Settings.SmashableDoor)
                RenderTextLabel(
                    Settings.SmashableDoorColor.Value,
                    "Smash Door",
                    "Objects/LabyrinthSmashableDoor");

            if (Settings.SecretPassage)
                DrawTextLabelEquals(
                    Settings.SecretPassageColor.Value,
                    "Secret" + Environment.NewLine + "Passage",
                    "Metadata/Terrain/Labyrinth/Objects/SecretPassage");

            if (Settings.Specters)
            {
                if (Settings.TukohamasVanguard)
                    DrawTextLabelSpecter(
                        Color.Yellow,
                        "Tukohama's" + Environment.NewLine + "Vanguard",
                        "Metadata/Monsters/KaomWarrior/KaomWarrior7",
                        120);
                if (Settings.WickerMan)
                    DrawTextLabelSpecter(Color.Yellow, "WickerMan", "Metadata/Monsters/WickerMan/WickerMan", 120);

                if (Settings.PockedLanternbearer)
                    DrawTextLabelSpecter(
                        Color.Yellow,
                        "Pocked " + Environment.NewLine + "Lanternbearer",
                        "Metadata/Monsters/Miner/MinerLantern",
                        120);
            }

            if (Settings.Sentinels)
            {
                if (Settings.UnendingLethargy)
                    RenderTextLabel(
                        Settings.UnendingLethargyColor.Value,
                        "Slowing",
                        "LabyrinthPopUpTotemSlow");

                if (Settings.EndlessDrought)
                    RenderTextLabel(
                        Settings.EndlessDroughtColor.Value,
                        $"Flask Charge{Environment.NewLine}Removal",
                        "LabyrinthPopUpTotemFlaskChargeNova");

                if (Settings.EndlessHazard)
                    RenderTextLabel(
                        Settings.EndlessHazardColor.Value,
                        "Movement Damage",
                        "LabyrinthPopUpTotemDamageTakenOnMovementSkillUse");

                if (Settings.EndlessPain)
                    RenderTextLabel(
                        Settings.EndlessPainColor.Value,
                        "Inc Damage Taken",
                        "LabyrinthPopUpTotemIncreasedDamageTaken");

                if (Settings.EndlessSting)
                    RenderTextLabel(
                        Settings.EndlessStingColor.Value,
                        "Bleeding",
                        "LabyrinthPopUpTotemBleedNova");

                if (Settings.UnendingFire)
                    RenderTextLabel(
                        Settings.UnendingFireColor.Value,
                        "Fire Nova",
                        "LabyrinthPopUpTotemMonsterFire");

                if (Settings.UnendingFrost)
                    RenderTextLabel(
                        Settings.UnendingFrostColor.Value,
                        "Ice Nova",
                        "LabyrinthPopUpTotemMonsterIceNova");

                if (Settings.UnendingStorm)
                    RenderTextLabel(
                        Settings.UnendingStormColor.Value,
                        "Shock Nova",
                        "LabyrinthPopUpTotemMonsterLightning");
            }

            // Debug-ish things
            if (Settings.Debug) ShowAllPathObjects();
        }

        private void DrawTextLabelEquals(ColorBGRA color, string text, string path)
        {
            foreach (var entity in _entityCollection)
                if (entity.Path.ToLower().Equals(path.ToLower()))
                {
                    var camera = GameController.Game.IngameState.Camera;
                    var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords == new Vector2()) continue;
                    var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                    float maxWidth = 0;
                    float maxheight = 0;

                    var size = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y += size.Height;
                    maxheight += size.Height;
                    maxWidth = Math.Max(maxWidth, size.Width);

                    Graphics.DrawBox(
                        new RectangleF(
                            chestScreenCoords.X - maxWidth / 2 - 3,
                            chestScreenCoords.Y - maxheight,
                            maxWidth + 6,
                            maxheight),
                        Color.Black);
                }
        }

        private void DrawTextLabelSpecter(ColorBGRA color, string text, string path, int zOffset = 0)
        {
            foreach (var entity in _entityCollection)
                if (entity.Path.ToLower().Contains(path.ToLower()) && !entity.IsAlive)
                {
                    var camera = GameController.Game.IngameState.Camera;
                    var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, zOffset), entity);
                    if (chestScreenCoords != new Vector2())
                    {
                        var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                        float maxWidth = 0;
                        float maxheight = 0;

                        var size = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                        chestScreenCoords.Y += size.Height;
                        maxheight += size.Height;
                        maxWidth = Math.Max(maxWidth, size.Width);

                        var background = new RectangleF(
                            chestScreenCoords.X - maxWidth / 2 - 3,
                            chestScreenCoords.Y - maxheight,
                            maxWidth + 6,
                            maxheight);
                        Graphics.DrawBox(background, Color.Black);
                    }
                }
        }

        private void DrawToLargeMiniMap(EntityWrapper entity)
        {
            var icon = GetMapIcon(entity);
            if (icon == null) return;
            var camera = GameController.Game.IngameState.Camera;
            var mapWindow = GameController.Game.IngameState.IngameUi.Map;
            var mapRect = mapWindow.GetClientRect();

            var playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var posZ = GameController.Player.GetComponent<Render>().Z;
            var screenCenter = new Vector2(mapRect.Width / 2, mapRect.Height / 2).Translate(0, -20)
                               + new Vector2(mapRect.X, mapRect.Y) + new Vector2(
                                   mapWindow.LargeMapShiftX,
                                   mapWindow.LargeMapShiftY);
            var diag = (float)Math.Sqrt(camera.Width * camera.Width + camera.Height * camera.Height);
            var k = camera.Width < 1024f ? 1120f : 1024f;
            var scale = k / camera.Height * camera.Width * 3f / 4f / mapWindow.LargeMapZoom;

            var iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
            var point = screenCenter + MapIcon.DeltaInWorldToMinimapDelta(
                            icon.WorldPosition - playerPos,
                            diag,
                            scale,
                            (iconZ - posZ) / (9f / mapWindow.LargeMapZoom));

            var texture = icon.TextureIcon;
            var size = icon.Size * 2; // icon.SizeOfLargeIcon.GetValueOrDefault(icon.Size * 2);
            texture.DrawPluginImage(
                Graphics,
                new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size));
        }

        private void DrawToSmallMiniMap(EntityWrapper entity)
        {
            var icon = GetMapIcon(entity);

            if (icon == null) return;
            var smallMinimap = GameController.Game.IngameState.IngameUi.Map.SmallMinimap;
            var playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var posZ = GameController.Player.GetComponent<Render>().Z;

            const float scale = 240f;
            var mapRect = smallMinimap.GetClientRect();
            var mapCenter =
                new Vector2(mapRect.X + mapRect.Width / 2, mapRect.Y + mapRect.Height / 2).Translate(0, 0);
            var diag = Math.Sqrt(mapRect.Width * mapRect.Width + mapRect.Height * mapRect.Height) / 2.0;

            var iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
            var point = mapCenter + MapIcon.DeltaInWorldToMinimapDelta(
                            icon.WorldPosition - playerPos,
                            diag,
                            scale,
                            (iconZ - posZ) / 20);

            var texture = icon.TextureIcon;
            var size = icon.Size;
            var rect = new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size);
            mapRect.Contains(ref rect, out var isContain);
            if (isContain) texture.DrawPluginImage(Graphics, rect);
        }

        private MapIcon GetMapIcon(EntityWrapper e)
        {
            if (Settings.Roombas && Settings.RoombasOnMap)
            {
                if (e.Path.Contains("LabyrinthFlyingRoomba") || e.Path.Contains("LabyrinthRoomba"))
                {
                    return new MapIcon(
                        e,
                        new HudTexture(CustomImagePath + "roomba.png", Settings.RoombasOnMapColor),
                        () => Settings.RoombasOnMap,
                        Settings.RoombasOnMapSize);
                }
            }

            if (Settings.Spinners && Settings.SpinnersOnMap)
            {
                if (e.Path.Contains("LabyrinthSpinner"))
                {
                    return new MapIcon(
                        e,
                        new HudTexture(CustomImagePath + "roomba.png", Settings.SpinnersOnMapColor),
                        () => Settings.SpinnersOnMap,
                        Settings.SpinnersOnMapSize);
                }
            }

            if (Settings.Saws && Settings.SawsOnMap)
            {
                if (e.Path.Contains("LabyrinthSawblade"))
                {
                    return new MapIcon(
                        e,
                        new HudTexture(CustomImagePath + "roomba.png", Settings.SawsOnMapColor),
                        () => Settings.SawsOnMap,
                        Settings.SawsOnMapSize);
                }
            }

            if (Settings.LabyrinthChest && !e.GetComponent<Chest>().IsOpened)
            {
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTreasureKey"))
                    return new MapIcon(
                        e,
                        new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TreasureKeyChestColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTrinketChest"))
                    return new MapIcon(
                        e,
                        new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TrinketChestColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthSpecificUnique"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.SpecificUniqueChestColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0Currency"))
                    return new MapIcon(
                        e,
                        new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardCurrencyColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0CurrencyQuality"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardCurrencyQualityColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrency"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardDangerCurrencyColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrencyQuality"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardDangerCurrencyQualityColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerUnique"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardDangerUniqueColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerDivination"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardDangerDivinationColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerSkillGems"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardDangerLowGemColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCorruptedVaal"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardDangerCorVaalColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerJewelry"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardDangerJewelleryColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerGeneric"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardDangerGenericColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrency"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardSilverCurrencyColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrencyQuality"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardSilverCurrencyQualityColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverJewelryUnique"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardSilverJewelryUniqueColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverDivination"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardSilverDivinationColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique1"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardSilverUniqueOneColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique3"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardSilverUniqueTwoColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique2"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardSilverUniqueThreeColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverSkillGems"))
                    return new MapIcon(
                        e,
                        new HudTexture(
                            PoeHudImageLocation + "strongbox.png",
                            Settings.RewardSilverSkillGemColor),
                        () => Settings.LabyrinthChest,
                        Settings.LabyrinthChestSize);
            }

            if (Settings.Darkshrines)
                if (Settings.DarkshrinesOnMap)
                    if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden"))
                        return new MapIcon(
                            e,
                            new HudTexture(CustomImagePath + "shrines.png", Settings.DarkshrinesColor),
                            () => Settings.DarkshrinesOnMap,
                            Settings.DarkshrinesIcon);

            if (Settings.NormalShrines)
                if (Settings.NormalShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/Shrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(
                                e,
                                new HudTexture(CustomImagePath + "shrines.png", Settings.NormalShrinesColor),
                                () => Settings.NormalShrineOnMap,
                                Settings.NormalShrinesIcon);

            if (Settings.LesserShrines)
                if (Settings.LesserShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/LesserShrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(
                                e,
                                new HudTexture(CustomImagePath + "shrines.png", Settings.LesserShrinesColor),
                                () => Settings.LesserShrineOnMap,
                                Settings.LesserShrinesIcon);

            if (Settings.HiddenDoorway)
                if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Short")
                    || e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Long"))
                    return new MapIcon(
                        e,
                        new HudTexture(CustomImagePath + "hidden_door.png", Settings.HiddenDoorwayColor),
                        () => Settings.HiddenDoorway,
                        Settings.HiddenDoorwayIcon);

            if (Settings.SecretPassage)
                if (e.Path.Equals("Metadata/Terrain/Labyrinth/Objects/SecretPassage"))
                    return new MapIcon(
                        e,
                        new HudTexture(CustomImagePath + "hidden_door.png", Settings.SecretPassageColor),
                        () => Settings.SecretPassage,
                        Settings.SecretPassageIcon);

            if (Settings.AreaTransition)
            {
                if (e.Path.Contains("Metadata/Chests/VaultTreasurePile"))
                    if (!e.GetComponent<Chest>().IsOpened)
                        return new MapIcon(
                            e,
                            new HudTexture(CustomImagePath + "Coin.png"),
                            () => true,
                            Settings.VaultPilesIcon);

                if (e.Path.Contains("Transition"))
                    return new MapIcon(
                        e,
                        new HudTexture(CustomImagePath + "hidden_door.png", Settings.AreaTransitionColor),
                        () => true,
                        Settings.AreaTransitionIcon);
            }

            if (Settings.AbyssCracks)
            {
                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssCrackSpawners/AbyssCrackSkeletonSpawner"))
                    return new MapIcon(
                        e,
                        new HudTexture(CustomImagePath + "abyss-crack.png", Settings.AbyssSmallNodeColor),
                        () => true,
                        Settings.AbyssSmallNodeSize);

                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeLarge") ||
                    e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmall") ||
                    e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmallMortarSpawned") ||
                    e.Path.Contains("Metadata/Chests/Abyss/AbyssFinal"))
                    return new MapIcon(
                        e,
                        new HudTexture(CustomImagePath + "abyss-node-small.png", Settings.AbyssLargeNodeColor),
                        () => true,
                        Settings.AbyssLargeNodeSize);
            }
            if (Settings.AbysshoardChestToggleNode)
            {
                if (e.Path.Contains("Metadata/Chests/AbyssChest") && !e.GetComponent<Chest>().IsOpened)
                    return new MapIcon(
                        e,
                        new HudTexture(CustomImagePath + "exclamationmark.png", Settings.AbysshoardChestColor),
                        () => Settings.AbysshoardChestToggleNode,
                        Settings.AbysshoardChestSize);
            }

            return null;
        }

        private void RenderAtziriMirrorClone()
        {
            if (!Settings.Atziri) return;

            var wantedEntity = new ImageToWorldData
            {
                Path = new[] {"Metadata/Monsters/Atziri/AtziriMirror"},
                ImagePath = $"{CustomImagePath}mirror.png",
                ImageSize = Settings.AtziriMirrorSize,
                YOffset = -90
            };

            DrawImageToWorld(wantedEntity);
        }

        private void RenderLieutenantSkeletonThorns()
        {
            if (!Settings.LieutenantofRage) return;

            var wantedEntity = new ImageToWorldData
            {
                Path = new[]
                {
                    "Metadata/Monsters/Labyrinth/LabyrinthLieutenantSkeletonMeleeIce",
                    "Metadata/Monsters/Labyrinth/LabyrinthLieutenantSkeletonMeleeIce2"
                },
                ImagePath = $"{CustomImagePath}mirror.png",
                ImageSize = Settings.LieutenantofRageSize,
                YOffset = -90
            };

            DrawImageToWorld(wantedEntity);
        }

        private void RenderMapImages()
        {
            if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
                foreach (var entity in _entityCollection) DrawToLargeMiniMap(entity);
            else if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
                foreach (var entity in _entityCollection) DrawToSmallMiniMap(entity);
        }

        private void RenderShrines()
        {
            var lesserShrine = new ImageToWorldData
            {
                Path = new[] {"Metadata/Shrines/LesserShrine"},
                ImagePath = $"{CustomImagePath}shrines.png",
                ImageSize = Settings.LesserShrineOnFloorSize,
                YOffset = -90,
                Color = Settings.LesserShrinesColor,
                HasColor = true
            };

            var normalShrine = new ImageToWorldData
            {
                Path = new[] {"Metadata/Shrines/Shrine"},
                ImagePath = $"{CustomImagePath}shrines.png",
                ImageSize = Settings.LesserShrineOnFloorSize,
                YOffset = -90,
                Color = Settings.NormalShrinesColor,
                HasColor = true
            };

            var darkShrine = new ImageToWorldData
            {
                Path =
                    new[]
                    {
                        "Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden"
                    },
                ImagePath = $"{CustomImagePath}shrines.png",
                ImageSize = Settings.DarkshrinesOnFloorSize,
                YOffset = -90,
                Color = Settings.DarkshrinesColor,
                HasColor = true
            };

            if (Settings.LesserShrines && Settings.LesserShrineOnFloor)
                DrawImageToWorld_Shrine(lesserShrine);

            if (Settings.NormalShrines && Settings.NormalShrineOnFloor)
                DrawImageToWorld_Shrine(normalShrine);

            if (Settings.Darkshrines && Settings.DarkshrinesOnFloor) DrawImageToWorld_Shrine(darkShrine);
        }

        private void RenderTextLabel(ColorBGRA color, string text, string path)
        {
            foreach (var entity in _entityCollection)
                if (entity.Path.ToLower().Contains(path.ToLower()))
                {
                    var camera = GameController.Game.IngameState.Camera;
                    var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords == new Vector2()) continue;
                    var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                    float maxWidth = 0;
                    float maxheight = 0;

                    var size = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y += size.Height;
                    maxheight += size.Height;
                    maxWidth = Math.Max(maxWidth, size.Width);

                    var background = new RectangleF(
                        chestScreenCoords.X - maxWidth / 2 - 3,
                        chestScreenCoords.Y - maxheight,
                        maxWidth + 6,
                        maxheight);

                    Graphics.DrawBox(background, Color.Black);
                }
        }

        private void RenderTextLabel(ColorBGRA color, string text, string path, int zOffset)
        {
            foreach (var entity in _entityCollection)
                if (entity.Path.ToLower().Contains(path.ToLower()))
                {
                    var camera = GameController.Game.IngameState.Camera;
                    var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, zOffset), entity);
                    if (chestScreenCoords == new Vector2()) continue;
                    var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                    float maxWidth = 0;
                    float maxheight = 0;

                    var size = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y += size.Height;
                    maxheight += size.Height;
                    maxWidth = Math.Max(maxWidth, size.Width);

                    var background = new RectangleF(
                        chestScreenCoords.X - maxWidth / 2 - 3,
                        chestScreenCoords.Y - maxheight,
                        maxWidth + 6,
                        maxheight);

                    Graphics.DrawBox(background, Color.Black);
                }
        }

        private void RenderVaultPiles()
        {
            if (!Settings.VaultPiles) return;

            var wantedEntity = new ImageToWorldData
            {
                Path = new[] {"Metadata/Chests/VaultTreasurePile"},
                ImagePath = $"{CustomImagePath}Coin.png",
                ImageSize = Settings.VaultPilesOnFloorSize
            };

            DrawImageToWorld_Chest(wantedEntity);
        }

        private void ShowAllPathObjects()
        {
            foreach (var entity in _entityCollection)
            {
                var textToShow = new List<string>();
                var show = true;

                foreach (var stringDebugDoodad in DebugDoodads)
                    if (entity.Path.ToLower().Contains(stringDebugDoodad.ToLower()))
                        show = false;

                if (show && Settings.DebugMonstersOnly)
                    if (!entity.HasComponent<Monster>() || !entity.IsAlive)
                        show = false;

                if (show)
                {
                    textToShow.Add(entity.Path);

                    textToShow.AddRange(entity.GetComponent<Life>().Buffs.Select(buff => buff.Name));

                    var camera = GameController.Game.IngameState.Camera;
                    var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords != new Vector2())

                    {
                        var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);

                        float maxWidth = 0;
                        float maxheight = 0;

                        var finalString = textToShow.Aggregate("",
                            (current, _string) => current + _string + Environment.NewLine);

                        var size = Graphics.DrawText(finalString, Settings.DebugTextSize, iconRect, Color.White,
                            FontDrawFlags.Center);

                        chestScreenCoords.Y += size.Height;
                        maxheight += size.Height;
                        maxWidth = Math.Max(maxWidth, size.Width);

                        var background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3,
                            chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);

                        Graphics.DrawBox(background, Color.Black);
                    }
                }
                show = false;
            }
        }

        public class ImageToWorldData
        {
            public Color Color;

            public bool HasColor;

            public string ImagePath;

            public int ImageSize = 10;

            public string[] Path = { };

            public int YOffset;
        }
    }
}