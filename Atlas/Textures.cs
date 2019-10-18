using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.AtlasHelper;
using Random_Features.Libs;
using SharpDX;
using System.Linq;

namespace Random_Features
{
    partial class RandomFeatures
    {
        private AtlasTexture _iconAbberantFossil;
        private AtlasTexture _iconAbberantFossilT1;
        private AtlasTexture _iconAbberantFossilT2;
        private AtlasTexture _iconAbberantFossilT3;
        private AtlasTexture _iconAbyssCrack;
        private AtlasTexture _iconAbyssNodeSmall;
        private AtlasTexture _iconAdditionalSockets;
        private AtlasTexture _iconBombs;
        private AtlasTexture _iconChest;
        private AtlasTexture _iconChestJewelers;
        private AtlasTexture _iconCoin;
        private AtlasTexture _iconCorrupted;
        private AtlasTexture _iconCurrency;
        private AtlasTexture _iconDemonAltar;
        private AtlasTexture _iconDemonSmallBlood;
        private AtlasTexture _iconDivinationCard;
        private AtlasTexture _iconEnchant;
        private AtlasTexture _iconEssence;
        private AtlasTexture _iconExclamationMark;
        private AtlasTexture _iconFlare;
        private AtlasTexture _iconFragment;
        private AtlasTexture _iconGate;
        private AtlasTexture _iconHiddenDoor;
        private AtlasTexture _iconHighLevelGem;
        private AtlasTexture _iconLeagueChest;
        private AtlasTexture _iconMap;
        private AtlasTexture _iconMirror;
        private AtlasTexture _iconPaleCourtComplete;
        private AtlasTexture _iconResonatorT1;
        private AtlasTexture _iconResonatorT2;
        private AtlasTexture _iconResonatorT3;
        private AtlasTexture _iconRoomba;
        private AtlasTexture _iconShrines;
        private AtlasTexture _iconSilverCoin;
        private AtlasTexture _iconSixLink;
        private AtlasTexture _iconSpeedArmour;
        private AtlasTexture _iconStrongbox;
        private AtlasTexture _iconUniqueManaFlask;
        private AtlasTexture _iconUpgrade2x1A;
        private AtlasTexture _iconUpgrade2x2A;
        private AtlasTexture _iconWisDomCurrency;

        public void AtlasTextureInit()
        {
            _iconAbberantFossil = GetAtlasTexture("AbberantFossil");
            _iconAbberantFossilT1 = GetAtlasTexture("AbberantFossilT1");
            _iconAbberantFossilT2 = GetAtlasTexture("AbberantFossilT2");
            _iconAbberantFossilT3 = GetAtlasTexture("AbberantFossilT3");
            _iconAbyssCrack = GetAtlasTexture("AbyssCrack");
            _iconAbyssNodeSmall = GetAtlasTexture("AbyssNodeSmall");
            _iconAdditionalSockets = GetAtlasTexture("AdditionalSockets");
            _iconBombs = GetAtlasTexture("Bombs");
            _iconChest = GetAtlasTexture("Chest");
            _iconChestJewelers = GetAtlasTexture("ChestJewelers");
            _iconCoin = GetAtlasTexture("Coin");
            _iconCorrupted = GetAtlasTexture("Corrupted");
            _iconCurrency = GetAtlasTexture("Currency");
            _iconDemonAltar = GetAtlasTexture("DemonAltar");
            _iconDemonSmallBlood = GetAtlasTexture("DemonSmallBlood");
            _iconDivinationCard = GetAtlasTexture("DivinationCard");
            _iconEnchant = GetAtlasTexture("Enchant");
            _iconEssence = GetAtlasTexture("Essence");
            _iconExclamationMark = GetAtlasTexture("ExclamationMark");
            _iconFlare = GetAtlasTexture("Flare");
            _iconFragment = GetAtlasTexture("Fragment");
            _iconGate = GetAtlasTexture("Gate");
            _iconHiddenDoor = GetAtlasTexture("HiddenDoor");
            _iconHighLevelGem = GetAtlasTexture("HighLevelGem");
            _iconLeagueChest = GetAtlasTexture("LeagueChest");
            _iconMap = GetAtlasTexture("Map");
            _iconMirror = GetAtlasTexture("Mirror");
            _iconPaleCourtComplete = GetAtlasTexture("PaleCourtComplete");
            _iconResonatorT1 = GetAtlasTexture("ResonatorT1");
            _iconResonatorT2 = GetAtlasTexture("ResonatorT2");
            _iconResonatorT3 = GetAtlasTexture("ResonatorT3");
            _iconRoomba = GetAtlasTexture("Roomba");
            _iconShrines = GetAtlasTexture("Shrines");
            _iconSilverCoin = GetAtlasTexture("SilverCoin");
            _iconSixLink = GetAtlasTexture("SixLink");
            _iconSpeedArmour = GetAtlasTexture("SpeedArmour");
            _iconStrongbox = GetAtlasTexture("Strongbox");
            _iconUniqueManaFlask = GetAtlasTexture("UniqueManaFlask");
            _iconUpgrade2x1A = GetAtlasTexture("Upgrade2x1A");
            _iconUpgrade2x2A = GetAtlasTexture("Upgrade2x2A");
            _iconWisDomCurrency = GetAtlasTexture("WisDomCurrency");
        }

        private MapIcon GetMapIcon(Entity e)
        {
            if (Settings.RoyaleThings)
            {
                if (e.HasComponent<Chest>() && !e.GetComponent<Chest>().IsOpened)
                {
                    if (Settings.RoyaleHoardChests && e.Path.Contains("Metadata/Chests/RoyaleChestMidKitUnique"))
                        return new MapIcon(_iconStrongbox, Settings.RoyaleHoardColor, Settings.RoyaleHoardSize);

                    if (Settings.RoyaleTroveChests && e.Path.Contains("Metadata/Chests/RoyaleChestCurrency"))
                        return new MapIcon(_iconStrongbox, Settings.RoyaleTroveColor, Settings.RoyaleTroveSize);

                    if (Settings.RoyaleCacheChests && e.Path.Contains("Metadata/Chests/RoyaleChestMidKit"))
                        return new MapIcon(_iconStrongbox, Settings.RoyaleCacheColor, Settings.RoyaleCacheSize);

                    if (Settings.RoyaleSuppliesChests && e.Path.Contains("Metadata/Chests/RoyaleChestStarterKit"))
                        return new MapIcon(_iconStrongbox, Settings.RoyaleSuppliesColor, Settings.RoyaleSuppliesSize);

                    if (Settings.RoyaleExplosiveBarrelsChests &&
                        e.Path.Contains("Metadata/Chests/AtlasBarrelExplosive1"))
                        return new MapIcon(_iconChest, Settings.RoyaleExplosiveBarrelsColor, Settings.RoyaleExplosiveBarrelsSize);
                }
            }

            if (Settings.BeyondThings)
            {
                if (Settings.BeyondPortal && e.Path.Contains("Metadata/Monsters/BeyondDemons/BeyondPortal3"))
                    return new MapIcon(_iconDemonAltar, Settings.BeyondPortalColor, Settings.BeyondPortalSize * 1.5f);
                if (Settings.BeyondPortal && e.Path.Contains("Metadata/Monsters/BeyondDemons/BeyondPortal2"))
                    return new MapIcon(_iconDemonAltar, Settings.BeyondPortalColor, Settings.BeyondPortalSize);
                if (Settings.BeyondPortal && e.Path.Contains("Metadata/Monsters/BeyondDemons/BeyondPortal1"))
                    return new MapIcon(_iconDemonSmallBlood, Settings.BeyondPortalColor, Settings.BeyondPortalSize / 2);
            }

            if (Settings.Roombas && Settings.RoombasOnMap)
                if (e.Path.Contains("LabyrinthFlyingRoomba") || e.Path.Contains("LabyrinthRoomba"))
                    return new MapIcon(_iconRoomba, Settings.RoombasOnMapColor, Settings.RoombasOnMapSize);

            if (Settings.Roombas && Settings.RoombasOnMap)
                if (e.Path.Contains("HydraTurretProjectile"))
                    return new MapIcon(_iconRoomba, Settings.RoombasOnMapColor, Settings.RoombasOnMapSize);

            if (Settings.Spinners && Settings.SpinnersOnMap)
                if (e.Path.Contains("LabyrinthSpinner"))
                    return new MapIcon(_iconRoomba, Settings.SpinnersOnMapColor, Settings.SpinnersOnMapSize);

            if (Settings.Saws && Settings.SawsOnMap)
                if (e.Path.Contains("LabyrinthSawblade"))
                    return new MapIcon(_iconRoomba, Settings.SawsOnMapColor, Settings.SawsOnMapSize);

            if (Settings.LabyrinthChest && e.HasComponent<Chest>() && !e.GetComponent<Chest>().IsOpened)
            {
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTreasureKey"))
                    return new MapIcon(_iconStrongbox, Settings.TreasureKeyChestColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTrinketChest"))
                    return new MapIcon(_iconStrongbox, Settings.TrinketChestColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthSpecificUnique"))
                    return new MapIcon(_iconStrongbox, Settings.SpecificUniqueChestColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0Currency"))
                    return new MapIcon(_iconStrongbox, Settings.RewardCurrencyColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0CurrencyQuality"))
                    return new MapIcon(_iconStrongbox, Settings.RewardCurrencyQualityColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrency"))
                    return new MapIcon(_iconStrongbox, Settings.RewardDangerCurrencyColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrencyQuality"))
                    return new MapIcon(_iconStrongbox, Settings.RewardDangerCurrencyQualityColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerUnique"))
                    return new MapIcon(_iconStrongbox, Settings.RewardDangerUniqueColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerDivination"))
                    return new MapIcon(_iconStrongbox, Settings.RewardDangerDivinationColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerSkillGems"))
                    return new MapIcon(_iconStrongbox, Settings.RewardDangerLowGemColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCorruptedVaal"))
                    return new MapIcon(_iconStrongbox, Settings.RewardDangerCorVaalColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerJewelry"))
                    return new MapIcon(_iconStrongbox, Settings.RewardDangerJewelleryColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerGeneric"))
                    return new MapIcon(_iconStrongbox, Settings.RewardDangerGenericColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrency"))
                    return new MapIcon(_iconStrongbox, Settings.RewardSilverCurrencyColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrencyQuality"))
                    return new MapIcon(_iconStrongbox, Settings.RewardSilverCurrencyQualityColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverJewelryUnique"))
                    return new MapIcon(_iconStrongbox, Settings.RewardSilverJewelryUniqueColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverDivination"))
                    return new MapIcon(_iconStrongbox, Settings.RewardSilverDivinationColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique1"))
                    return new MapIcon(_iconStrongbox, Settings.RewardSilverUniqueOneColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique3"))
                    return new MapIcon(_iconStrongbox, Settings.RewardSilverUniqueTwoColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique2"))
                    return new MapIcon(_iconStrongbox, Settings.RewardSilverUniqueThreeColor, Settings.LabyrinthChestSize);

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverSkillGems"))
                    return new MapIcon(_iconStrongbox, Settings.RewardSilverSkillGemColor, Settings.LabyrinthChestSize);
            }

            if (Settings.Darkshrines)
                if (Settings.DarkshrinesOnMap)
                    if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden"))
                        return new MapIcon(_iconShrines, Settings.DarkshrinesColor, Settings.DarkshrinesIcon);

            if (Settings.NormalShrines)
                if (Settings.NormalShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/Shrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(_iconShrines, Settings.NormalShrinesColor, Settings.NormalShrinesIcon);

            if (Settings.LesserShrines)
                if (Settings.LesserShrineOnMap)
                    if (e.Path.Contains("Metadata/Shrines/LesserShrine"))
                        if (e.GetComponent<Shrine>().IsAvailable)
                            return new MapIcon(_iconShrines, Settings.LesserShrinesColor, Settings.LesserShrinesIcon);

            if (Settings.HiddenDoorway)
                if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Short")
                 || e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Long"))
                    return new MapIcon(_iconHiddenDoor, Settings.HiddenDoorwayColor, Settings.HiddenDoorwayIcon);

            if (Settings.SecretPassage)
                if (e.Path.Equals("Metadata/Terrain/Labyrinth/Objects/SecretPassage"))
                    return new MapIcon(_iconHiddenDoor, Settings.SecretPassageColor, Settings.SecretPassageIcon);

            if (Settings.VaultPilesOnMap)
                if (e.Path.Contains("Metadata/Chests/VaultTreasurePile"))
                    if (!e.GetComponent<Chest>().IsOpened)
                        return new MapIcon(_iconCoin, Settings.VaultPilesIcon);

            if (Settings.AbyssCracks)
            {
                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssCrackSpawners/AbyssCrackSkeletonSpawner"))
                    return new MapIcon(_iconAbyssCrack, Settings.AbyssSmallNodeColor, Settings.AbyssSmallNodeSize);

                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeLarge")
                 || e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmall")
                 || e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmallMortarSpawned")
                 || e.Path.Contains("Metadata/Chests/Abyss/AbyssFinal"))
                    return new MapIcon(_iconAbyssNodeSmall, Settings.AbyssLargeNodeColor, Settings.AbyssLargeNodeSize);
            }

            if (Settings.DelvePathWays)
                if (e.Path.EndsWith("Metadata/Terrain/Leagues/Delve/Objects/DelveLight"))
                    return new MapIcon(_iconAbyssCrack, Settings.DelvePathWaysNodeColor, Settings.DelvePathWaysNodeSize);

            if (Settings.DelveChests)
            {
                if (e.HasComponent<Chest>() && !e.GetComponent<Chest>().IsOpened)
                {
                    if (e.Path.StartsWith("Metadata/Chests/DelveChests/DelveChestGenericLeague") || e.Path.Contains("Unique") && e.Path.StartsWith("Metadata/Chests/DelveChests"))
                        return new MapIcon(_iconLeagueChest, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveMiningSuppliesDynamite"))
                        return new MapIcon(_iconBombs, Settings.DelveMiningSuppliesDynamiteChestColor, Settings.DelveMiningSuppliesDynamiteChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveMiningSuppliesFlares"))
                        return new MapIcon(_iconFlare, Settings.DelveMiningSuppliesFlaresChestColor, Settings.DelveMiningSuppliesFlaresChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("PathCurrency"))
                        return new MapIcon(_iconCurrency, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("DynamiteCurrency"))
                        return new MapIcon(_iconCurrency, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("AdditionalSockets"))
                        return new MapIcon(_iconAdditionalSockets, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("AtziriFragment"))
                        return new MapIcon(_iconFragment, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("PaleCourtFragment"))
                        return new MapIcon(_iconPaleCourtComplete, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Essence"))
                        return new MapIcon(_iconEssence, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("SilverCoin"))
                        return new MapIcon(_iconSilverCoin, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("WisdomScroll"))
                        return new MapIcon(_iconWisDomCurrency, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Divination"))
                        return new MapIcon(_iconDivinationCard, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveChestCurrency"))
                        return new MapIcon(_iconCurrency, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("DelveAzuriteVein") && !e.Path.Contains("Encounter"))
                        return new MapIcon(_iconStrongbox, Settings.DelveAzuriteVeinChestColor, Settings.DelveAzuriteVeinChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Resonator3") && e.Path.Contains("Resonator4") && e.Path.Contains("Resonator5"))
                        return new MapIcon(_iconResonatorT1, Settings.DelveResonatorChestColor, Settings.DelveResonatorChestSize * 0.7f);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Resonator2"))
                        return new MapIcon(_iconResonatorT2, Settings.DelveResonatorChestColor, Settings.DelveResonatorChestSize * 0.7f);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Resonator1"))
                        return new MapIcon(_iconResonatorT3, Settings.DelveResonatorChestColor, Settings.DelveResonatorChestSize * 0.7f);

                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveChestArmourMovementSpeed"))
                        return new MapIcon(_iconSpeedArmour, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveChestSpecialUniqueMana"))
                        return new MapIcon(_iconUniqueManaFlask, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize * 1.3f);

                    if (e.Path.Contains("RandomEnchant") && e.Path.StartsWith("Metadata/Chests/DelveChests"))
                        return new MapIcon(_iconEnchant, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("FossilChest") && e.Path.StartsWith("Metadata/Chests/DelveChests"))
                    {
                        if (FossilList.T1.Any(@string => e.Path.ToLower().Contains(@string.ToLower())))
                            return new MapIcon(_iconAbberantFossilT1, Settings.DelveFossilChestColor, Settings.DelveFossilChestSize);

                        if (FossilList.T2.Any(@string => e.Path.ToLower().Contains(@string.ToLower())))
                            return new MapIcon(_iconAbberantFossilT2, Settings.DelveFossilChestColor, Settings.DelveFossilChestSize);

                        if (FossilList.T3.Any(@string => e.Path.ToLower().Contains(@string.ToLower())))
                            return new MapIcon(_iconAbberantFossilT3, Settings.DelveFossilChestColor, Settings.DelveFossilChestSize);

                        return new MapIcon(_iconAbberantFossil, Settings.DelveFossilChestColor, Settings.DelveFossilChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Resonator"))
                        return new MapIcon(_iconResonatorT1, Settings.DelveResonatorChestColor, Settings.DelveResonatorChestSize * 0.7f);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Map"))
                        return new MapIcon(_iconMap, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Corrupted"))
                        return new MapIcon(_iconCorrupted, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("6Linked"))
                        return new MapIcon(_iconSixLink, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("GemHighLevel"))
                        return new MapIcon(_iconHighLevelGem, Settings.DelveCurrencyChestColor, Settings.DelveCurrencyChestSize);

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Trinkets"))
                        return new MapIcon(_iconChestJewelers, Color.Yellow, Settings.DelveCurrencyChestSize);

                    // catch missing delve chests
                    if (Settings.DelvePathwayChest)
                        if (e.Path.Contains("Metadata/Chests/DelveChests") && !e.Path.Contains("Encounter"))
                            return new MapIcon(_iconStrongbox, Settings.DelvePathwayChestColor, Settings.DelvePathwayChestSize);
                }

                if (e.Path.StartsWith("Metadata/Terrain/Leagues/Delve/Objects/DelveWall"))
                    switch (e.IsAlive)
                    {
                        case false:
                            return new MapIcon(_iconHiddenDoor, Settings.DelveWallColor, Settings.DelveWallSize);
                        case true:
                            return new MapIcon(_iconGate, Settings.DelveWallColor, Settings.DelveWallSize);
                    }
            }

            if (Settings.BlightPathWays)
                if (e.Path.EndsWith("Metadata/Terrain/Leagues/Blight/Objects/BlightPathway"))
                    return new MapIcon(_iconAbyssCrack, Settings.BlightPathWaysNodeColor, Settings.BlightPathWaysNodeSize);

            if (Settings.AbysshoardChestToggleNode)
                if (e.Path.Contains("Metadata/Chests/AbyssChest") && !e.GetComponent<Chest>().IsOpened)
                    return new MapIcon(_iconExclamationMark, Settings.AbysshoardChestColor, Settings.AbysshoardChestSize);

            if (Settings.Incursion)
            {
                if (Settings.ClosedIncursionDoor)
                {
                    if (e.Path.Contains("Metadata/Terrain/Leagues/Incursion/Objects/ClosedDoorPresent_Fuse"))
                        if (Settings.ClosedIncursionDoorOnMapColor != Color.White)
                            return new MapIcon(_iconGate, Settings.ClosedIncursionDoorOnMapColor, Settings.ClosedIncursionDoorOnMapSize);
                        else
                            return new MapIcon(_iconGate, Settings.ClosedIncursionDoorOnMapSize);

                    if (e.Path.Contains("Metadata/Terrain/Leagues/Incursion/Objects/TempleInternalDoor"))
                        if (Settings.OpenIncursionDoorOnMapColor != Color.White)
                            return new MapIcon(_iconHiddenDoor, Settings.OpenIncursionDoorOnMapColor, Settings.OpenIncursionDoorOnMapSize);
                        else
                            return new MapIcon(_iconHiddenDoor, Settings.OpenIncursionDoorOnMapSize);
                }

                if (Settings.IncursionBreachChest)
                    if (e.HasComponent<Chest>() && !e.GetComponent<Chest>().IsOpened)
                    {
                        if (e.Path.Contains("Metadata/Chests/IncursionChests/IncursionChestBreach"))
                            return new MapIcon(_iconStrongbox, Settings.IncursionBreachChestOnMapColor, Settings.IncursionBreachChestOnMapSize);
                        if (e.Path.Contains("Metadata/Chests/IncursionChestArmour"))
                            return new MapIcon(_iconStrongbox, Settings.IncursionChestArmorChestOnMapColor, Settings.IncursionChestArmorChestOnMapSize);
                    }
            }

            return null;
        }
    }
}
