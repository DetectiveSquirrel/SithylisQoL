using System;
using System.Collections.Generic;
using ImGuiNET;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud;
using PoeHUD.Models;
using PoeHUD.Models.Enums;
using PoeHUD.Poe;
using PoeHUD.Poe.Components;
using Random_Features.Libs;
using SharpDX;
using SharpDX.Direct3D9;
using ImVector2 = System.Numerics.Vector2;
using ImVector4 = System.Numerics.Vector4;
using Vector4 = SharpDX.Vector4;

namespace Random_Features
{
    partial class RandomFeatures
    {
        public void RandomFeaturesMenu(int IdIn, out int IdPop)
        {
            IdPop = IdIn;
            Settings.UnsortedPlugin.Value = ImGuiExtension.Checkbox("Enable##RFToggle", Settings.UnsortedPlugin);
            Settings.PluginTextSize.Value = ImGuiExtension.IntSlider("Plugin Text Size", Settings.PluginTextSize);
            if (ImGui.TreeNode("Abyss"))
            {
                if (ImGui.TreeNode("Cracks"))
                {
                    Settings.AbyssSmallNodeSize.Value = ImGuiExtension.IntSlider("Small Node Size", Settings.AbyssSmallNodeSize);
                    Settings.AbyssSmallNodeColor.Value = ImGuiExtension.ColorPicker("Small Node Color", Settings.AbyssSmallNodeColor);
                    ImGui.Spacing();
                    Settings.AbyssLargeNodeSize.Value = ImGuiExtension.IntSlider("Large Node Size", Settings.AbyssLargeNodeSize);
                    Settings.AbyssLargeNodeColor.Value = ImGuiExtension.ColorPicker("Large Node Color", Settings.AbyssLargeNodeColor);
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Hoard Chests"))
                {
                    ImGui.PushID("AbyssHoardChest");
                    Settings.AbysshoardChestToggleNode.Value = ImGuiExtension.Checkbox(Settings.AbysshoardChestToggleNode ? "Show" : "Hide",
                            Settings.AbysshoardChestToggleNode);
                    ImGui.PopID();
                    Settings.AbysshoardChestSize.Value = ImGuiExtension.IntSlider("Hoard Chest Size", Settings.AbysshoardChestSize);
                    Settings.AbysshoardChestColor.Value = ImGuiExtension.ColorPicker("Hoard Chest Color", Settings.AbysshoardChestColor);
                    ImGui.TreePop();
                }

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Labyrinth"))
            {
                if (ImGui.TreeNode("Chests"))
                {
                    Settings.LabyrinthChestSize.Value = ImGuiExtension.IntSlider("Labyrinth Chest Size", Settings.LabyrinthChestSize);
                    if (ImGui.TreeNode("Normal"))
                    {
                        Settings.TrinketChestColor.Value = ImGuiExtension.ColorPicker("Trinks", Settings.TrinketChestColor);
                        Settings.TreasureKeyChestColor.Value = ImGuiExtension.ColorPicker("Treasure Key", Settings.TreasureKeyChestColor);
                        Settings.SpecificUniqueChestColor.Value = ImGuiExtension.ColorPicker("Specific Unique", Settings.SpecificUniqueChestColor);
                        Settings.RewardCurrencyColor.Value = ImGuiExtension.ColorPicker("Currency", Settings.RewardCurrencyColor);
                        Settings.RewardCurrencyQualityColor.Value =
                                ImGuiExtension.ColorPicker("Quality Currency", Settings.RewardCurrencyQualityColor);
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Danger"))
                    {
                        ImGui.PushID(IdPop);
                        Settings.RewardDangerCurrencyColor.Value = ImGuiExtension.ColorPicker("Currency", Settings.RewardDangerCurrencyColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardDangerCurrencyQualityColor.Value =
                                ImGuiExtension.ColorPicker("Quality Currency", Settings.RewardDangerCurrencyQualityColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardDangerUniqueColor.Value = ImGuiExtension.ColorPicker("Unique", Settings.RewardDangerUniqueColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardDangerDivinationColor.Value = ImGuiExtension.ColorPicker("Divination", Settings.RewardDangerDivinationColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardDangerLowGemColor.Value = ImGuiExtension.ColorPicker("Low Gems", Settings.RewardDangerLowGemColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardDangerCorVaalColor.Value = ImGuiExtension.ColorPicker("Corrupted Vaal", Settings.RewardDangerCorVaalColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardDangerJewelleryColor.Value = ImGuiExtension.ColorPicker("Jewelery", Settings.RewardDangerJewelleryColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardDangerGenericColor.Value = ImGuiExtension.ColorPicker("Generic", Settings.RewardDangerGenericColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Silver"))
                    {
                        ImGui.PushID(IdPop);
                        Settings.RewardSilverCurrencyColor.Value = ImGuiExtension.ColorPicker("Currency", Settings.RewardSilverCurrencyColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardSilverCurrencyQualityColor.Value =
                                ImGuiExtension.ColorPicker("Quality Currency", Settings.RewardSilverCurrencyQualityColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardSilverJewelryUniqueColor.Value =
                                ImGuiExtension.ColorPicker("Unique Jewelery", Settings.RewardSilverJewelryUniqueColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardSilverDivinationColor.Value = ImGuiExtension.ColorPicker("Divination", Settings.RewardSilverDivinationColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardSilverUniqueOneColor.Value = ImGuiExtension.ColorPicker("Unique 1", Settings.RewardSilverUniqueOneColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardSilverUniqueTwoColor.Value = ImGuiExtension.ColorPicker("Unique 2", Settings.RewardSilverUniqueTwoColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardSilverUniqueThreeColor.Value = ImGuiExtension.ColorPicker("Unique 3", Settings.RewardSilverUniqueThreeColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RewardSilverSkillGemColor.Value = ImGuiExtension.ColorPicker("Skill Gems", Settings.RewardSilverSkillGemColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.TreePop();
                    }

                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Dark Shrines"))
                {
                    Settings.DarkshrinesOnFloor.Value = ImGuiExtension.Checkbox("Draw On The Floor", Settings.DarkshrinesOnFloor);
                    ImGui.PushID(IdPop);
                    Settings.DarkshrinesOnFloorSize.Value = ImGuiExtension.IntSlider("Size", Settings.DarkshrinesOnFloorSize);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.Spacing();
                    Settings.DarkshrinesOnMap.Value = ImGuiExtension.Checkbox("Draw On The Map", Settings.DarkshrinesOnMap);
                    ImGui.PushID(IdPop);
                    Settings.DarkshrinesIcon.Value = ImGuiExtension.IntSlider("Size", Settings.DarkshrinesIcon);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.PushID(IdPop);
                    Settings.DarkshrinesColor.Value = ImGuiExtension.ColorPicker("Color", Settings.DarkshrinesColor);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Rename Later - Misc Shit"))
                {
                    Settings.SecretSwitchColor.Value = ImGuiExtension.ColorPicker("##SecretSwitch", Settings.SecretSwitchColor);
                    ImGui.SameLine();
                    Settings.SecretSwitch.Value = ImGuiExtension.Checkbox("Secret Switch", Settings.SecretSwitch.Value);
                    Settings.DeliveryColor.Value = ImGuiExtension.ColorPicker("##GauntletDelivery", Settings.DeliveryColor);
                    ImGui.SameLine();
                    Settings.Delivery.Value = ImGuiExtension.Checkbox("Gauntlet Delivery", Settings.Delivery.Value);
                    Settings.SmashableDoorColor.Value = ImGuiExtension.ColorPicker("##SmashableDoor", Settings.SmashableDoorColor);
                    ImGui.SameLine();
                    Settings.SmashableDoor.Value = ImGuiExtension.Checkbox("Smashable Door", Settings.SmashableDoor.Value);
                    ImGui.Spacing();
                    Settings.HiddenDoorway.Value = ImGuiExtension.Checkbox("Hidden Doorway", Settings.HiddenDoorway);
                    ImGui.PushID(IdPop);
                    Settings.HiddenDoorwayIcon.Value = ImGuiExtension.IntSlider("Size", Settings.HiddenDoorwayIcon);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.PushID(IdPop);
                    Settings.HiddenDoorwayColor.Value = ImGuiExtension.ColorPicker("Color", Settings.HiddenDoorwayColor);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.Spacing();
                    Settings.SecretPassage.Value = ImGuiExtension.Checkbox("Secret Passage", Settings.SecretPassage);
                    ImGui.PushID(IdPop);
                    Settings.SecretPassageIcon.Value = ImGuiExtension.IntSlider("Size", Settings.SecretPassageIcon);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.PushID(IdPop);
                    Settings.SecretPassageColor.Value = ImGuiExtension.ColorPicker("Color", Settings.SecretPassageColor);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.Spacing();
                    Settings.LieutenantofRageSize.Value =
                            ImGuiExtension.IntSlider("##LieutenantofRageSize", "Size of icon", Settings.LieutenantofRageSize);
                    ImGui.SameLine();
                    Settings.LieutenantofRage.Value = ImGuiExtension.Checkbox("Lieutenant of Rage", Settings.LieutenantofRage.Value);
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Traps"))
                {
                    if (ImGui.TreeNode("Sentinels"))
                    {
                        Settings.UnendingLethargyColor.Value = ImGuiExtension.ColorPicker("##4tu3gv4", Settings.UnendingLethargyColor);
                        ImGui.SameLine();
                        Settings.UnendingLethargy.Value = ImGuiExtension.Checkbox("Undending Lethargy", Settings.UnendingLethargy.Value);
                        Settings.UnendingFireColor.Value = ImGuiExtension.ColorPicker("##tykrtbnb", Settings.UnendingFireColor);
                        ImGui.SameLine();
                        Settings.UnendingFire.Value = ImGuiExtension.Checkbox("Undending Fire", Settings.UnendingFire.Value);
                        Settings.UnendingFrostColor.Value = ImGuiExtension.ColorPicker("##gfljfghlk", Settings.UnendingFrostColor);
                        ImGui.SameLine();
                        Settings.UnendingFrost.Value = ImGuiExtension.Checkbox("Undending Frost", Settings.UnendingFrost.Value);
                        Settings.UnendingStormColor.Value = ImGuiExtension.ColorPicker("##fdgkdfg", Settings.UnendingStormColor);
                        ImGui.SameLine();
                        Settings.UnendingStorm.Value = ImGuiExtension.Checkbox("Undending Storm", Settings.UnendingStorm.Value);
                        Settings.EndlessDroughtColor.Value = ImGuiExtension.ColorPicker("##erturyhd", Settings.EndlessDroughtColor);
                        ImGui.SameLine();
                        Settings.EndlessDrought.Value = ImGuiExtension.Checkbox("Endless Drought", Settings.EndlessDrought.Value);
                        Settings.EndlessHazardColor.Value = ImGuiExtension.ColorPicker("##dsgjbngl", Settings.EndlessHazardColor);
                        ImGui.SameLine();
                        Settings.EndlessHazard.Value = ImGuiExtension.Checkbox("Endless Hazard", Settings.EndlessHazard.Value);
                        Settings.EndlessPainColor.Value = ImGuiExtension.ColorPicker("##fdgjn65", Settings.EndlessPainColor);
                        ImGui.SameLine();
                        Settings.EndlessPain.Value = ImGuiExtension.Checkbox("Endless Pain", Settings.EndlessPain.Value);
                        Settings.EndlessStingColor.Value = ImGuiExtension.ColorPicker("##fginkjre5", Settings.EndlessStingColor);
                        ImGui.SameLine();
                        Settings.EndlessSting.Value = ImGuiExtension.Checkbox("Endless Sting", Settings.EndlessSting.Value);
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Roombas"))
                    {
                        Settings.Roombas.Value = ImGuiExtension.Checkbox(Settings.Roombas.Value ? "Show" : "Hidden", Settings.Roombas);
                        ImGui.PushID(IdPop);
                        Settings.RoombasColor.Value = ImGuiExtension.ColorPicker("Text", Settings.RoombasColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.Spacing();
                        ImGui.PushID(IdPop);
                        Settings.RoombasOnMap.Value = ImGuiExtension.Checkbox("On Map", Settings.RoombasOnMap);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RoombasOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.RoombasOnMapSize);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.RoombasOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.RoombasOnMapColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Spinners"))
                    {
                        Settings.Spinners.Value = ImGuiExtension.Checkbox(Settings.Spinners.Value ? "Show" : "Hidden", Settings.Spinners);
                        ImGui.PushID(IdPop);
                        Settings.SpinnersColor.Value = ImGuiExtension.ColorPicker("Text", Settings.SpinnersColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.Spacing();
                        ImGui.PushID(IdPop);
                        Settings.SpinnersOnMap.Value = ImGuiExtension.Checkbox("On Map", Settings.SpinnersOnMap);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.SpinnersOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.SpinnersOnMapSize);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.SpinnersOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.SpinnersOnMapColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Saws"))
                    {
                        Settings.Saws.Value = ImGuiExtension.Checkbox(Settings.Saws.Value ? "Show" : "Hidden", Settings.Saws);
                        ImGui.PushID(IdPop);
                        Settings.SawsColor.Value = ImGuiExtension.ColorPicker("Text", Settings.SawsColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.Spacing();
                        ImGui.PushID(IdPop);
                        Settings.SawsOnMap.Value = ImGuiExtension.Checkbox("On Map", Settings.SawsOnMap);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.SawsOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.SawsOnMapSize);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.PushID(IdPop);
                        Settings.SawsOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.SawsOnMapColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Arrows"))
                    {
                        Settings.Arrows.Value = ImGuiExtension.Checkbox(Settings.Arrows.Value ? "Show" : "Hidden", Settings.Arrows);
                        ImGui.PushID(IdPop);
                        Settings.ArrowsColor.Value = ImGuiExtension.ColorPicker("Text", Settings.ArrowsColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Preassure Plates"))
                    {
                        Settings.PressurePlates.Value =
                                ImGuiExtension.Checkbox(Settings.PressurePlates.Value ? "Show" : "Hidden", Settings.PressurePlates);
                        ImGui.PushID(IdPop);
                        Settings.PressurePlatesColor.Value = ImGuiExtension.ColorPicker("Text", Settings.PressurePlatesColor);
                        ImGui.PopID();
                        IdPop++;
                        ImGui.TreePop();
                    }

                    ImGui.TreePop();
                }

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Atziri"))
            {
                Settings.AtziriMirrorSize.Value = ImGuiExtension.IntSlider("##324234", "Size", Settings.AtziriMirrorSize);
                ImGui.SameLine();
                Settings.Atziri.Value = ImGuiExtension.Checkbox("Show Clone", Settings.Atziri.Value);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Shrines"))
            {
                if (ImGui.TreeNode("Normal"))
                {
                    Settings.NormalShrines.Value = ImGuiExtension.Checkbox("Draw On The Floor", Settings.NormalShrines);
                    ImGui.PushID(IdPop);
                    Settings.NormalShrinesIcon.Value = ImGuiExtension.IntSlider("Size", Settings.NormalShrinesIcon);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.Spacing();
                    Settings.NormalShrineOnMap.Value = ImGuiExtension.Checkbox("Draw On The Map", Settings.NormalShrineOnMap);
                    ImGui.PushID(IdPop);
                    Settings.NormalShrineOnFloorSize.Value = ImGuiExtension.IntSlider("Size", Settings.NormalShrineOnFloorSize);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.PushID(IdPop);
                    Settings.NormalShrinesColor.Value = ImGuiExtension.ColorPicker("Color", Settings.NormalShrinesColor);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Lesser"))
                {
                    Settings.LesserShrines.Value = ImGuiExtension.Checkbox("Draw On The Floor", Settings.LesserShrines);
                    ImGui.PushID(IdPop);
                    Settings.LesserShrinesIcon.Value = ImGuiExtension.IntSlider("Size", Settings.LesserShrinesIcon);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.Spacing();
                    Settings.LesserShrineOnMap.Value = ImGuiExtension.Checkbox("Draw On The Map", Settings.LesserShrineOnMap);
                    ImGui.PushID(IdPop);
                    Settings.LesserShrineOnFloorSize.Value = ImGuiExtension.IntSlider("Size", Settings.LesserShrineOnFloorSize);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.PushID(IdPop);
                    Settings.LesserShrinesColor.Value = ImGuiExtension.ColorPicker("Color", Settings.LesserShrinesColor);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.TreePop();
                }

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Vault Gold Piles"))
            {
                Settings.VaultPilesOnFloor.Value = ImGuiExtension.Checkbox("Draw On The Floor", Settings.VaultPilesOnFloor);
                ImGui.PushID(IdPop);
                Settings.VaultPilesOnFloorSize.Value = ImGuiExtension.IntSlider("Size", Settings.VaultPilesOnFloorSize);
                ImGui.PopID();
                IdPop++;
                ImGui.Spacing();
                Settings.VaultPilesOnMap.Value = ImGuiExtension.Checkbox("Draw On The Map", Settings.VaultPilesOnMap);
                ImGui.PushID(IdPop);
                Settings.VaultPilesIcon.Value = ImGuiExtension.IntSlider("Size", Settings.VaultPilesIcon);
                ImGui.PopID();
                IdPop++;
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Specter Bodies"))
            {
                ImGui.PushID(IdPop);
                Settings.Specters.Value = ImGuiExtension.Checkbox("Show", Settings.Specters);
                ImGui.PopID();
                IdPop++;
                ImGui.Spacing();
                Settings.TukohamasVanguard.Value = ImGuiExtension.Checkbox("Tukohama's Vanguard", Settings.TukohamasVanguard);
                Settings.WickerMan.Value = ImGuiExtension.Checkbox("WickerMan", Settings.WickerMan);
                Settings.PockedLanternbearer.Value = ImGuiExtension.Checkbox("Pocked Lanternbearer", Settings.PockedLanternbearer);
                Settings.SolarGuard.Value = ImGuiExtension.Checkbox("Solar Guard", Settings.SolarGuard);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("BATTLE ROYALE"))
            {
                Settings.RoyaleThings.Value = ImGuiExtension.Checkbox($"Show##{IdPop}", Settings.RoyaleThings);
                IdPop++;
                ImGui.Spacing();
                Settings.RoyaleHoardChests.Value = ImGuiExtension.Checkbox($"Hoard Chests (1 Unique)##{IdPop}", Settings.RoyaleHoardChests);
                IdPop++;
                Settings.RoyaleHoardSize.Value = ImGuiExtension.IntSlider($"Size##{IdPop}", Settings.RoyaleHoardSize);
                IdPop++;
                Settings.RoyaleHoardColor.Value = ImGuiExtension.ColorPicker($"Color##{IdPop}", Settings.RoyaleHoardColor);
                IdPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyaleTroveChests.Value = ImGuiExtension.Checkbox($"Trove Chests (Currency)##{IdPop}", Settings.RoyaleTroveChests);
                IdPop++;
                Settings.RoyaleTroveSize.Value = ImGuiExtension.IntSlider($"Size##{IdPop}", Settings.RoyaleTroveSize);
                IdPop++;
                Settings.RoyaleTroveColor.Value = ImGuiExtension.ColorPicker($"Color##{IdPop}", Settings.RoyaleTroveColor);
                IdPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyaleCacheChests.Value = ImGuiExtension.Checkbox($"Cache Chests (Mid Kit Gear)##{IdPop}", Settings.RoyaleCacheChests);
                IdPop++;
                Settings.RoyaleCacheSize.Value = ImGuiExtension.IntSlider($"Size##{IdPop}", Settings.RoyaleCacheSize);
                IdPop++;
                Settings.RoyaleCacheColor.Value = ImGuiExtension.ColorPicker($"Color##{IdPop}", Settings.RoyaleCacheColor);
                IdPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyaleSuppliesChests.Value = ImGuiExtension.Checkbox($"Supplies Chests (Starter Kit Gear)##{IdPop}", Settings.RoyaleSuppliesChests);
                IdPop++;
                Settings.RoyaleSuppliesSize.Value = ImGuiExtension.IntSlider($"Size##{IdPop}", Settings.RoyaleSuppliesSize);
                IdPop++;
                Settings.RoyaleSuppliesColor.Value = ImGuiExtension.ColorPicker($"Color##{IdPop}", Settings.RoyaleSuppliesColor);
                IdPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyaleExplosiveBarrelsChests.Value = ImGuiExtension.Checkbox($"Explosive Barrels (They dont hurt mobs)##{IdPop}", Settings.RoyaleExplosiveBarrelsChests);
                IdPop++;
                Settings.RoyaleExplosiveBarrelsSize.Value = ImGuiExtension.IntSlider($"Size##{IdPop}", Settings.RoyaleExplosiveBarrelsSize);
                IdPop++;
                Settings.RoyaleExplosiveBarrelsColor.Value = ImGuiExtension.ColorPicker($"Color##{IdPop}", Settings.RoyaleExplosiveBarrelsColor);
                IdPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyalBeta.Value = ImGuiExtension.Checkbox($"World Position - Find Your Friends!##{IdPop}", Settings.RoyalBeta);
                IdPop++;
                Settings.RoyalLockedMudule.Value = ImGuiExtension.Checkbox($"locked Module##{IdPop}", Settings.RoyalLockedMudule);
                IdPop++;
                Settings.RoyalOverrideColors.Value = ImGuiExtension.Checkbox($"Override Module Colors##{IdPop}", Settings.RoyalOverrideColors);
                IdPop++;
                Settings.RoyalModuleBackground = ImGuiExtension.ColorPicker($"Module Background Color##{IdPop}", Settings.RoyalModuleBackground);

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Beyond Things"))
            {
                Settings.BeyondThings.Value = ImGuiExtension.Checkbox($"Show##{IdPop}", Settings.BeyondThings);
                IdPop++;
                ImGui.Spacing();
                Settings.BeyondPortal.Value = ImGuiExtension.Checkbox($"Beyond Portal2##{IdPop}", Settings.BeyondPortal);
                IdPop++;
                Settings.BeyondPortalSize.Value = ImGuiExtension.IntSlider($"Size##{IdPop}", Settings.BeyondPortalSize);
                IdPop++;
                Settings.BeyondPortalColor.Value = ImGuiExtension.ColorPicker($"Color##{IdPop}", Settings.BeyondPortalColor);
                IdPop++;

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Incursion DooDads"))
            {
                if (ImGui.TreeNode("Blocked Gateway"))
                {
                    Settings.ClosedIncursionDoor.Value = ImGuiExtension.Checkbox(Settings.ClosedIncursionDoor.Value ? "Show" : "Hidden", Settings.ClosedIncursionDoor);

                    ImGui.Spacing();
                    ImGui.PushID(IdPop);
                    Settings.ClosedIncursionDoorOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.ClosedIncursionDoorOnMapSize);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.PushID(IdPop);
                    Settings.ClosedIncursionDoorOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.ClosedIncursionDoorOnMapColor);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.TreePop();
                }
                if (ImGui.TreeNode("Breach Chest"))
                {
                    Settings.IncursionBreachChest.Value = ImGuiExtension.Checkbox(Settings.IncursionBreachChest.Value ? "Show" : "Hidden", Settings.IncursionBreachChest);

                    ImGui.Spacing();
                    ImGui.PushID(IdPop);
                    Settings.IncursionBreachChestOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.IncursionBreachChestOnMapSize);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.PushID(IdPop);
                    Settings.IncursionBreachChestOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.IncursionBreachChestOnMapColor);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.TreePop();
                }
                if (ImGui.TreeNode("Armour Chests"))
                {
                    Settings.IncursionChestArmorChest.Value = ImGuiExtension.Checkbox(Settings.IncursionChestArmorChest.Value ? "Show" : "Hidden", Settings.IncursionChestArmorChest);

                    ImGui.Spacing();
                    ImGui.PushID(IdPop);
                    Settings.IncursionChestArmorChestOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.IncursionChestArmorChestOnMapSize);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.PushID(IdPop);
                    Settings.IncursionChestArmorChestOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.IncursionChestArmorChestOnMapColor);
                    ImGui.PopID();
                    IdPop++;
                    ImGui.TreePop();
                }
                ImGui.TreePop();
            }
            if (ImGui.TreeNode("Monster Stats On Hover - This is strange and only shows the resistances once that resistance has been affected with + or -"))
            {
                Settings.MonsterHoverStats.Value = ImGuiExtension.Checkbox($"Show##{IdPop}", Settings.MonsterHoverStats);
                IdPop++;

                ImGui.TreePop();
            }

            Settings.Debug = ImGuiExtension.Checkbox("Debug", Settings.Debug);
        }
        private ImVector4 ToImVector4(Vector4 Vector) => new ImVector4(Vector.X, Vector.Y, Vector.Z, Vector.W);

        public void BETA_ROYALE()
        {
            if (!Settings.RoyalBeta)
            {
                return;
            }

            var RefBool = true;
            var MenuOpacity = ImGui.GetStyle().GetColor(ColorTarget.WindowBg).W;
            if (Settings.RoyalOverrideColors)
            {
                ImGui.PushStyleColor(ColorTarget.WindowBg, ToImVector4(Settings.RoyalModuleBackground.ToVector4()));
                MenuOpacity = ImGui.GetStyle().GetColor(ColorTarget.WindowBg).W;
            }

            ImGui.BeginWindow("ROYAL LOCATION", ref RefBool, new ImVector2(200, 150), MenuOpacity,
                    Settings.RoyalLockedMudule
                            ? WindowFlags.NoCollapse
                            | WindowFlags.NoScrollbar
                            | WindowFlags.NoMove
                            | WindowFlags.NoResize
                            | WindowFlags.NoInputs
                            | WindowFlags.NoBringToFrontOnFocus
                            | WindowFlags.NoTitleBar
                            | WindowFlags.NoFocusOnAppearing
                            : WindowFlags.Default | WindowFlags.NoTitleBar | WindowFlags.ResizeFromAnySide);
            if (Settings.RoyalOverrideColors)
            {
                ImGui.PopStyleColor();
            }

            var Location = LocalPlayer.Entity.GetComponent<Positioned>().WorldPos;
            Location = new Vector2((float) Math.Ceiling(Location.X), (float) Math.Ceiling(Location.Y));
            ImGui.Text("World Position");
            ImGui.BulletText($"X: {Location.X}");
            ImGui.BulletText($"Y: {Location.Y}");
            ImGui.EndWindow();
        }

        public bool IsTargeted(EntityWrapper Entity)
        {
        return Entity.GetComponent<Targetable>().Address != 0 && Memory.ReadBytes(Entity.GetComponent<Targetable>().Address + 0x30, 3)[2] == 1;
        }


        private string TryGetStat(GameStat Stat, EntityWrapper Entity)
        {
            return Entity.GetComponent<Stats>().StatDictionary.TryGetValue(Stat, out int StatInt) ? StatInt.ToString() : "?";
        }

        private void MonsterResistnaceOnHover()
        {
            if (!Settings.MonsterHoverStats) return;
            foreach (EntityWrapper Entity in _EntityCollection)
            {
                if (Entity.IsValid)
                    if (Entity.HasComponent<Monster>())
                        if (Entity.IsAlive)
                            if (IsTargeted(Entity))
                            {
                                var FireRes = TryGetStat(GameStat.FireDamageResistancePct, Entity);
                                var ColdRes = TryGetStat(GameStat.ColdDamageResistancePct, Entity);
                                var LightRes = TryGetStat(GameStat.LightningDamageResistancePct, Entity);
                                var ChaosRes = TryGetStat(GameStat.ChaosDamageResistancePct, Entity);
                                Element MonsterBox = MonsterTopName();
                                if (MonsterBox.Children[0].Width > 0)
                                {
                                    RectangleF Pos = MonsterBox.Children[0].GetClientRect();
                                    int TextSize = (int) Pos.Height;
                                    int NextTextSpace = 0;
                                    string NextText = $"{FireRes}";
                                    string String = NextText;
                                    Graphics.DrawText(NextText, TextSize, new Vector2(Pos.X + 10 + Pos.Width + NextTextSpace, Pos.Y), new Color(255, 85, 85, 255));
                                    NextTextSpace += Graphics.MeasureText(NextText, TextSize).Width;
                                    NextText = $" {ColdRes}";
                                    String += NextText;
                                    Graphics.DrawText(NextText, TextSize, new Vector2(Pos.X + 10 + Pos.Width + NextTextSpace, Pos.Y), new Color(77, 77, 255, 255));
                                    NextTextSpace += Graphics.MeasureText(NextText, TextSize).Width;
                                    NextText = $" {LightRes}";
                                    String += NextText;
                                    Graphics.DrawText(NextText, TextSize, new Vector2(Pos.X + 10 + Pos.Width + NextTextSpace, Pos.Y), new Color(253, 245, 75, 255));
                                    NextTextSpace += Graphics.MeasureText(NextText, TextSize).Width;
                                    NextText = $" {ChaosRes}";
                                    String += NextText;
                                    Graphics.DrawText(NextText, TextSize, new Vector2(Pos.X + 10 + Pos.Width + NextTextSpace, Pos.Y), new Color(255, 91, 179, 255));
                                    Graphics.DrawBox(
                                            new RectangleF(Pos.X + 10 + Pos.Width, Pos.Y, Graphics.MeasureText(String, TextSize).Width, Pos.Height),
                                            Color.Black);
                                }
                            }
            }
        }
        public Element MonsterTopName()
        {
            try
            {
                return GameController.Game.IngameState.UIRoot.Children[1]?.Children[10]?.Children[8];
            }
            catch (Exception E)
            {
                return null;
            }
        }

        private void UnsortedPlugin()
        {
            if (!GameController.Game.IngameState.IngameUi.AtlasPanel.IsVisible && !GameController.Game.IngameState.IngameUi.TreePanel.IsVisible)
            {
                RenderMapImages();
                RenderVaultPiles();
                RenderAtziriMirrorClone();
                RenderLieutenantSkeletonThorns();
                RenderShrines();
                BETA_ROYALE();
                MonsterResistnaceOnHover();
            }

            if (Settings.SecretSwitch)
            {
                RenderTextLabel(Settings.SecretSwitchColor.Value, "Switch", "HiddenDoor_Switch");
            }

            if (Settings.Spinners)
            {
                RenderTextLabel(Settings.SpinnersColor.Value, "Spinner", "LabyrinthSpinner");
            }

            if (Settings.Saws)
            {
                RenderTextLabel(Settings.SawsColor.Value, "Saw Blade", "Labyrinthsawblade");
            }

            if (Settings.Roombas)
            {
                RenderTextLabel(Settings.RoombasColor.Value, "Roomba", "LabyrinthRoomba", -67);
            }

            if (Settings.Roombas)
            {
                RenderTextLabel(Settings.RoombasColor.Value, "Roomba", "LabyrinthflyingRoomba", -180);
            }

            if (Settings.Delivery)
            {
                RenderTextLabel(Settings.DeliveryColor.Value, "Deliver", "labyrinthtrapkeydelivery");
            }

            if (Settings.PressurePlates)
            {
                RenderTextLabel(Settings.PressurePlatesColor.Value, "Pressure" + Environment.NewLine + "Plate", "pressureplate_reset");
            }

            if (Settings.Arrows)
            {
                RenderTextLabel(Settings.ArrowsColor.Value, "Arrows", "Labyrintharrowtrap");
            }

            if (Settings.SmashableDoor)
            {
                RenderTextLabel(Settings.SmashableDoorColor.Value, "Smash Door", "Objects/LabyrinthSmashableDoor");
            }

            if (Settings.SecretPassage)
            {
                DrawTextLabelEquals(Settings.SecretPassageColor.Value, "Secret" + Environment.NewLine + "Passage",
                        "Metadata/Terrain/Labyrinth/Objects/SecretPassage");
            }

            if (Settings.Specters)
            {
                if (Settings.TukohamasVanguard)
                {
                    DrawTextLabelSpecter(Color.Yellow, "Tukohama's" + Environment.NewLine + "Vanguard", "Metadata/Monsters/KaomWarrior/KaomWarrior7",
                            120);
                }

                if (Settings.WickerMan)
                {
                    DrawTextLabelSpecter(Color.Yellow, "WickerMan", "Metadata/Monsters/WickerMan/WickerMan", 120);
                }

                if (Settings.PockedLanternbearer)
                {
                    DrawTextLabelSpecter(Color.Yellow, "Pocked " + Environment.NewLine + "Lanternbearer", "Metadata/Monsters/Miner/MinerLantern",
                            120);
                }

                if (Settings.SolarGuard)
                {
                    DrawTextLabelSpecter(Color.Yellow, "Solar Guard", "Metadata/Monsters/HolyFireElemental/HolyFireElementalSolarisBeam", 120);
                }
            }

            if (Settings.Sentinels)
            {
                if (Settings.UnendingLethargy)
                {
                    RenderTextLabel(Settings.UnendingLethargyColor.Value, "Slowing", "LabyrinthPopUpTotemSlow");
                }

                if (Settings.EndlessDrought)
                {
                    RenderTextLabel(Settings.EndlessDroughtColor.Value, $"Flask Charge{Environment.NewLine}Removal",
                            "LabyrinthPopUpTotemFlaskChargeNova");
                }

                if (Settings.EndlessHazard)
                {
                    RenderTextLabel(Settings.EndlessHazardColor.Value, "Movement Damage", "LabyrinthPopUpTotemDamageTakenOnMovementSkillUse");
                }

                if (Settings.EndlessPain)
                {
                    RenderTextLabel(Settings.EndlessPainColor.Value, "Inc Damage Taken", "LabyrinthPopUpTotemIncreasedDamageTaken");
                }

                if (Settings.EndlessSting)
                {
                    RenderTextLabel(Settings.EndlessStingColor.Value, "Bleeding", "LabyrinthPopUpTotemBleedNova");
                }

                if (Settings.UnendingFire)
                {
                    RenderTextLabel(Settings.UnendingFireColor.Value, "Fire Nova", "LabyrinthPopUpTotemMonsterFire");
                }

                if (Settings.UnendingFrost)
                {
                    RenderTextLabel(Settings.UnendingFrostColor.Value, "Ice Nova", "LabyrinthPopUpTotemMonsterIceNova");
                }

                if (Settings.UnendingStorm)
                {
                    RenderTextLabel(Settings.UnendingStormColor.Value, "Shock Nova", "LabyrinthPopUpTotemMonsterLightning");
                }
            }
        }

        private void DrawTextLabelEquals(ColorBGRA Color, string Text, string Path)
        {
            foreach (var Entity in _EntityCollection)
            {
                if (Entity.Path.ToLower().Equals(Path.ToLower()))
                {
                    var Camera = GameController.Game.IngameState.Camera;
                    var ChestScreenCoords = Camera.WorldToScreen(Entity.Pos.Translate(0, 0, 0), Entity);
                    if (ChestScreenCoords == new Vector2())
                    {
                        continue;
                    }

                    var IconRect = new Vector2(ChestScreenCoords.X, ChestScreenCoords.Y);
                    float MaxWidth = 0;
                    float Maxheight = 0;
                    var Size = Graphics.DrawText(Text, Settings.PluginTextSize, IconRect, Color, FontDrawFlags.Center);
                    ChestScreenCoords.Y += Size.Height;
                    Maxheight += Size.Height;
                    MaxWidth = Math.Max(MaxWidth, Size.Width);
                    Graphics.DrawBox(new RectangleF(ChestScreenCoords.X - MaxWidth / 2 - 3, ChestScreenCoords.Y - Maxheight, MaxWidth + 6, Maxheight),
                            SharpDX.Color.Black);
                }
            }
        }

        private void DrawTextLabelSpecter(ColorBGRA Color, string Text, string Path, int ZOffset = 0)
        {
            foreach (var Entity in _EntityCollection)
            {
                if (Entity.Path.ToLower().Contains(Path.ToLower()) && !Entity.IsAlive)
                {
                    var Camera = GameController.Game.IngameState.Camera;
                    var ChestScreenCoords = Camera.WorldToScreen(Entity.Pos.Translate(0, 0, ZOffset), Entity);
                    if (ChestScreenCoords == new Vector2())
                    {
                        continue;
                    }

                    var IconRect = new Vector2(ChestScreenCoords.X, ChestScreenCoords.Y);
                    float MaxWidth = 0;
                    float Maxheight = 0;
                    var Size = Graphics.DrawText(Text, Settings.PluginTextSize, IconRect, Color, FontDrawFlags.Center);
                    ChestScreenCoords.Y += Size.Height;
                    Maxheight += Size.Height;
                    MaxWidth = Math.Max(MaxWidth, Size.Width);
                    var Background = new RectangleF(ChestScreenCoords.X - MaxWidth / 2 - 3, ChestScreenCoords.Y - Maxheight, MaxWidth + 6, Maxheight);
                    Graphics.DrawBox(Background, SharpDX.Color.Black);
                }
            }
        }

        public static string WordWrap(string Input, int MaxCharacters)
        {
            var Lines = new List<string>();
            if (!Input.Contains(" "))
            {
                var Start = 0;
                while (Start < Input.Length)
                {
                    Lines.Add(Input.Substring(Start, Math.Min(MaxCharacters, Input.Length - Start)));
                    Start += MaxCharacters;
                }
            }
            else
            {
                var Words = Input.Split(' ');
                var Line = "";
                foreach (var Word in Words)
                {
                    if ((Line + Word).Length > MaxCharacters)
                    {
                        Lines.Add(Line.Trim());
                        Line = "";
                    }

                    Line += string.Format("{0} ", Word);
                }

                if (Line.Length > 0)
                {
                    Lines.Add(Line.Trim());
                }
            }

            var ConectedLines = "";
            foreach (var Line in Lines)
            {
                ConectedLines += Line + "\n\r";
            }

            return ConectedLines;
        }

        private void DrawToLargeMiniMap(EntityWrapper Entity)
        {
            var Icon = GetMapIcon(Entity);
            if (Icon == null)
            {
                return;
            }

            var Camera = GameController.Game.IngameState.Camera;
            var MapWindow = GameController.Game.IngameState.IngameUi.Map;
            var MapRect = MapWindow.GetClientRect();
            var PlayerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var PosZ = GameController.Player.GetComponent<Render>().Z;
            var ScreenCenter = new Vector2(MapRect.Width / 2, MapRect.Height / 2).Translate(0, -20)
                             + new Vector2(MapRect.X, MapRect.Y)
                             + new Vector2(MapWindow.LargeMapShiftX, MapWindow.LargeMapShiftY);
            var Diag = (float) Math.Sqrt(Camera.Width * Camera.Width + Camera.Height * Camera.Height);
            var K = Camera.Width < 1024f ? 1120f : 1024f;
            var Scale = K / Camera.Height * Camera.Width * 3f / 4f / MapWindow.LargeMapZoom;
            var IconZ = Icon.EntityWrapper.GetComponent<Render>().Z;
            var Point = ScreenCenter
                      + MapIcon.DeltaInWorldToMinimapDelta(Icon.WorldPosition - PlayerPos, Diag, Scale,
                                (IconZ - PosZ) / (9f / MapWindow.LargeMapZoom));
            var Texture = Icon.TextureIcon;
            var Size = Icon.Size * 2; // icon.SizeOfLargeIcon.GetValueOrDefault(icon.Size * 2);
            Texture.DrawPluginImage(Graphics, new RectangleF(Point.X - Size / 2f, Point.Y - Size / 2f, Size, Size));
        }

        private void DrawToSmallMiniMap(EntityWrapper Entity)
        {
            var Icon = GetMapIcon(Entity);
            if (Icon == null)
            {
                return;
            }

            var SmallMinimap = GameController.Game.IngameState.IngameUi.Map.SmallMinimap;
            var PlayerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var PosZ = GameController.Player.GetComponent<Render>().Z;
            const float Scale = 240f;
            var MapRect = SmallMinimap.GetClientRect();
            var MapCenter = new Vector2(MapRect.X + MapRect.Width / 2, MapRect.Y + MapRect.Height / 2).Translate(0, 0);
            var Diag = Math.Sqrt(MapRect.Width * MapRect.Width + MapRect.Height * MapRect.Height) / 2.0;
            var IconZ = Icon.EntityWrapper.GetComponent<Render>().Z;
            var Point = MapCenter + MapIcon.DeltaInWorldToMinimapDelta(Icon.WorldPosition - PlayerPos, Diag, Scale, (IconZ - PosZ) / 20);
            var Texture = Icon.TextureIcon;
            var Size = Icon.Size;
            var Rect = new RectangleF(Point.X - Size / 2f, Point.Y - Size / 2f, Size, Size);
            MapRect.Contains(ref Rect, out var IsContain);
            if (IsContain)
            {
                Texture.DrawPluginImage(Graphics, Rect);
            }
        }

        private MapIcon GetMapIcon(EntityWrapper E)
        {
            if (Settings.RoyaleThings)
            {
                if (Settings.RoyaleHoardChests && E.Path.Contains("Metadata/Chests/RoyaleChestMidKitUnique") && !E.GetComponent<Chest>().IsOpened)
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RoyaleHoardColor),
                            () => Settings.RoyaleHoardChests, Settings.RoyaleHoardSize);
                }
                if (Settings.RoyaleTroveChests && E.Path.Contains("Metadata/Chests/RoyaleChestCurrency") && !E.GetComponent<Chest>().IsOpened)
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RoyaleTroveColor),
                            () => Settings.RoyaleTroveChests, Settings.RoyaleTroveSize);
                }
                if (Settings.RoyaleCacheChests && E.Path.Contains("Metadata/Chests/RoyaleChestMidKit") && !E.GetComponent<Chest>().IsOpened)
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RoyaleCacheColor),
                            () => Settings.RoyaleCacheChests, Settings.RoyaleCacheSize);
                }
                if (Settings.RoyaleSuppliesChests && E.Path.Contains("Metadata/Chests/RoyaleChestStarterKit") && !E.GetComponent<Chest>().IsOpened)
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RoyaleSuppliesColor),
                            () => Settings.RoyaleSuppliesChests, Settings.RoyaleSuppliesSize);
                }
                if (Settings.RoyaleExplosiveBarrelsChests && E.Path.Contains("Metadata/Chests/AtlasBarrelExplosive1") && !E.GetComponent<Chest>().IsOpened)
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "chest.png", Settings.RoyaleExplosiveBarrelsColor),
                            () => Settings.RoyaleExplosiveBarrelsChests, Settings.RoyaleExplosiveBarrelsSize);
                }
            }

            if (Settings.BeyondThings)
            {
                if (Settings.BeyondPortal && E.Path.Contains("Metadata/Monsters/BeyondDemons/BeyondPortal3"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "Demon_Altar.png", Settings.BeyondPortalColor),
                            () => Settings.BeyondPortal, Settings.BeyondPortalSize*1.5f);
                }
                if (Settings.BeyondPortal && E.Path.Contains("Metadata/Monsters/BeyondDemons/BeyondPortal2"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "Demon_Altar.png", Settings.BeyondPortalColor),
                            () => Settings.BeyondPortal, Settings.BeyondPortalSize);
                }
                if (Settings.BeyondPortal && E.Path.Contains("Metadata/Monsters/BeyondDemons/BeyondPortal1"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "Demon_Small_Blood.png", Settings.BeyondPortalColor),
                            () => Settings.BeyondPortal, Settings.BeyondPortalSize/2);
                }
            }

            if (Settings.Roombas && Settings.RoombasOnMap)
            {
                if (E.Path.Contains("LabyrinthFlyingRoomba") || E.Path.Contains("LabyrinthRoomba"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "roomba.png", Settings.RoombasOnMapColor), () => Settings.RoombasOnMap,
                            Settings.RoombasOnMapSize);
                }
            }

            if (Settings.Spinners && Settings.SpinnersOnMap)
            {
                if (E.Path.Contains("LabyrinthSpinner"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "roomba.png", Settings.SpinnersOnMapColor), () => Settings.SpinnersOnMap,
                            Settings.SpinnersOnMapSize);
                }
            }

            if (Settings.Saws && Settings.SawsOnMap)
            {
                if (E.Path.Contains("LabyrinthSawblade"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "roomba.png", Settings.SawsOnMapColor), () => Settings.SawsOnMap,
                            Settings.SawsOnMapSize);
                }
            }

            if (Settings.LabyrinthChest && !E.GetComponent<Chest>().IsOpened)
            {
                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTreasureKey"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TreasureKeyChestColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTrinketChest"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TrinketChestColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthSpecificUnique"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.SpecificUniqueChestColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0Currency"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardCurrencyColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0CurrencyQuality"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardCurrencyQualityColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrency"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCurrencyColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrencyQuality"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCurrencyQualityColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerUnique"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerUniqueColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerDivination"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerDivinationColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerSkillGems"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerLowGemColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCorruptedVaal"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCorVaalColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerJewelry"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerJewelleryColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerGeneric"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerGenericColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrency"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverCurrencyColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrencyQuality"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverCurrencyQualityColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverJewelryUnique"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverJewelryUniqueColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverDivination"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverDivinationColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique1"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueOneColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique3"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueTwoColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique2"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueThreeColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (E.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverSkillGems"))
                {
                    return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverSkillGemColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }
            }

            if (Settings.Darkshrines)
            {
                if (Settings.DarkshrinesOnMap)
                {
                    if (E.Path.Contains("Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden"))
                    {
                        return new MapIcon(E, new HudTexture(CustomImagePath + "shrines.png", Settings.DarkshrinesColor),
                                () => Settings.DarkshrinesOnMap, Settings.DarkshrinesIcon);
                    }
                }
            }

            if (Settings.NormalShrines)
            {
                if (Settings.NormalShrineOnMap)
                {
                    if (E.Path.Contains("Metadata/Shrines/Shrine"))
                    {
                        if (E.GetComponent<Shrine>().IsAvailable)
                        {
                            return new MapIcon(E, new HudTexture(CustomImagePath + "shrines.png", Settings.NormalShrinesColor),
                                    () => Settings.NormalShrineOnMap, Settings.NormalShrinesIcon);
                        }
                    }
                }
            }

            if (Settings.LesserShrines)
            {
                if (Settings.LesserShrineOnMap)
                {
                    if (E.Path.Contains("Metadata/Shrines/LesserShrine"))
                    {
                        if (E.GetComponent<Shrine>().IsAvailable)
                        {
                            return new MapIcon(E, new HudTexture(CustomImagePath + "shrines.png", Settings.LesserShrinesColor),
                                () => Settings.LesserShrineOnMap, Settings.LesserShrinesIcon);
                        }
                    }
                }
            }

            if (Settings.HiddenDoorway)
            {
                if (E.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Short")
                 || E.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Long"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "hidden_door.png", Settings.HiddenDoorwayColor),
                            () => Settings.HiddenDoorway, Settings.HiddenDoorwayIcon);
                }
            }

            if (Settings.SecretPassage)
            {
                if (E.Path.Equals("Metadata/Terrain/Labyrinth/Objects/SecretPassage"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "hidden_door.png", Settings.SecretPassageColor),
                            () => Settings.SecretPassage, Settings.SecretPassageIcon);
                }
            }

            if (Settings.VaultPilesOnMap)
            {
                if (E.Path.Contains("Metadata/Chests/VaultTreasurePile"))
                {
                    if (!E.GetComponent<Chest>().IsOpened)
                    {
                        return new MapIcon(E, new HudTexture(CustomImagePath + "Coin.png"), () => true, Settings.VaultPilesIcon);
                    }
                }
            }

            if (Settings.AbyssCracks)
            {
                if (E.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssCrackSpawners/AbyssCrackSkeletonSpawner"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "abyss-crack.png", Settings.AbyssSmallNodeColor), () => true,
                            Settings.AbyssSmallNodeSize);
                }

                if (E.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeLarge")
                 || E.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmall")
                 || E.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmallMortarSpawned")
                 || E.Path.Contains("Metadata/Chests/Abyss/AbyssFinal"))
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "abyss-node-small.png", Settings.AbyssLargeNodeColor), () => true,
                            Settings.AbyssLargeNodeSize);
                }
            }

            if (Settings.AbysshoardChestToggleNode)
            {
                if (E.Path.Contains("Metadata/Chests/AbyssChest") && !E.GetComponent<Chest>().IsOpened)
                {
                    return new MapIcon(E, new HudTexture(CustomImagePath + "exclamationmark.png", Settings.AbysshoardChestColor),
                            () => Settings.AbysshoardChestToggleNode, Settings.AbysshoardChestSize);
                }
            }

            if (Settings.Incursion)
            {
                if (Settings.ClosedIncursionDoor)
                {
                    if (E.Path.Contains("Metadata/Terrain/Leagues/Incursion/Objects/ClosedDoorPresent_Fuse"))
                    {

                        if (Settings.ClosedIncursionDoorOnMapColor != Color.White)
                        {
                            return new MapIcon(E, new HudTexture(CustomImagePath + "gate.png", Settings.ClosedIncursionDoorOnMapColor),
                                () => Settings.ClosedIncursionDoor, Settings.ClosedIncursionDoorOnMapSize);
                        }
                        else
                        {
                            return new MapIcon(E, new HudTexture(CustomImagePath + "gate.png"),
                                () => Settings.ClosedIncursionDoor, Settings.ClosedIncursionDoorOnMapSize);
                        }
                    }
                }
                if (Settings.IncursionBreachChest)
                {
                    if (!E.GetComponent<Chest>().IsOpened)
                    {
                        if (E.Path.Contains("Metadata/Chests/IncursionChests/IncursionChestBreach"))
                        {
                            return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.IncursionBreachChestOnMapColor),
                                () => Settings.IncursionBreachChest, Settings.IncursionBreachChestOnMapSize);
                        }
                        if (E.Path.Contains("Metadata/Chests/IncursionChestArmour"))
                        {
                            return new MapIcon(E, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.IncursionChestArmorChestOnMapColor),
                                () => Settings.IncursionChestArmorChest, Settings.IncursionChestArmorChestOnMapSize);
                        }
                    }
                }
            }

            return null;
        }

        private void RenderAtziriMirrorClone()
        {
            if (!Settings.Atziri)
            {
                return;
            }

            var WantedEntity = new ImageToWorldData
            {
                    Path = new[]
                    {
                            "Metadata/Monsters/Atziri/AtziriMirror"
                    },
                    ImagePath = $"{CustomImagePath}mirror.png",
                    ImageSize = Settings.AtziriMirrorSize,
                    YOffset = -90
            };
            DrawImageToWorld(WantedEntity);
        }

        private void RenderLieutenantSkeletonThorns()
        {
            if (!Settings.LieutenantofRage)
            {
                return;
            }

            var WantedEntity = new ImageToWorldData
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
            DrawImageToWorld(WantedEntity);
        }

        private void RenderMapImages()
        {
            if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
            {
                foreach (var Entity in _EntityCollection)
                {
                    DrawToLargeMiniMap(Entity);
                }
            }
            else if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
            {
                foreach (var Entity in _EntityCollection)
                {
                    DrawToSmallMiniMap(Entity);
                }
            }
        }

        private void RenderShrines()
        {
            var LesserShrine = new ImageToWorldData
            {
                    Path = new[]
                    {
                            "Metadata/Shrines/LesserShrine"
                    },
                    ImagePath = $"{CustomImagePath}shrines.png",
                    ImageSize = Settings.LesserShrineOnFloorSize,
                    YOffset = -90,
                    Color = Settings.LesserShrinesColor,
                    HasColor = true
            };
            var NormalShrine = new ImageToWorldData
            {
                    Path = new[]
                    {
                            "Metadata/Shrines/Shrine"
                    },
                    ImagePath = $"{CustomImagePath}shrines.png",
                    ImageSize = Settings.LesserShrineOnFloorSize,
                    YOffset = -90,
                    Color = Settings.NormalShrinesColor,
                    HasColor = true
            };
            var DarkShrine = new ImageToWorldData
            {
                    Path = new[]
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
            {
                DrawImageToWorld_Shrine(LesserShrine);
            }

            if (Settings.NormalShrines && Settings.NormalShrineOnFloor)
            {
                DrawImageToWorld_Shrine(NormalShrine);
            }

            if (Settings.Darkshrines && Settings.DarkshrinesOnFloor)
            {
                DrawImageToWorld_Shrine(DarkShrine);
            }
        }

        private void RenderTextLabel(ColorBGRA Color, string Text, string Path)
        {
            foreach (var Entity in _EntityCollection)
            {
                if (Entity.Path.ToLower().Contains(Path.ToLower()))
                {
                    var Camera = GameController.Game.IngameState.Camera;
                    var ChestScreenCoords = Camera.WorldToScreen(Entity.Pos.Translate(0, 0, 0), Entity);
                    if (ChestScreenCoords == new Vector2())
                    {
                        continue;
                    }

                    var IconRect = new Vector2(ChestScreenCoords.X, ChestScreenCoords.Y);
                    float MaxWidth = 0;
                    float Maxheight = 0;
                    var Size = Graphics.DrawText(Text, Settings.PluginTextSize, IconRect, Color, FontDrawFlags.Center);
                    ChestScreenCoords.Y += Size.Height;
                    Maxheight += Size.Height;
                    MaxWidth = Math.Max(MaxWidth, Size.Width);
                    var Background = new RectangleF(ChestScreenCoords.X - MaxWidth / 2 - 3, ChestScreenCoords.Y - Maxheight, MaxWidth + 6, Maxheight);
                    Graphics.DrawBox(Background, SharpDX.Color.Black);
                }
            }
        }

        private void RenderTextLabel(ColorBGRA Color, string Text, string Path, int ZOffset)
        {
            foreach (var Entity in _EntityCollection)
            {
                if (Entity.Path.ToLower().Contains(Path.ToLower()))
                {
                    var Camera = GameController.Game.IngameState.Camera;
                    var ChestScreenCoords = Camera.WorldToScreen(Entity.Pos.Translate(0, 0, ZOffset), Entity);
                    if (ChestScreenCoords == new Vector2())
                    {
                        continue;
                    }

                    var IconRect = new Vector2(ChestScreenCoords.X, ChestScreenCoords.Y);
                    float MaxWidth = 0;
                    float Maxheight = 0;
                    var Size = Graphics.DrawText(Text, Settings.PluginTextSize, IconRect, Color, FontDrawFlags.Center);
                    ChestScreenCoords.Y += Size.Height;
                    Maxheight += Size.Height;
                    MaxWidth = Math.Max(MaxWidth, Size.Width);
                    var Background = new RectangleF(ChestScreenCoords.X - MaxWidth / 2 - 3, ChestScreenCoords.Y - Maxheight, MaxWidth + 6, Maxheight);
                    Graphics.DrawBox(Background, SharpDX.Color.Black);
                }
            }
        }

        private void RenderVaultPiles()
        {
            if (!Settings.VaultPilesOnFloor || !Settings.VaultPiles)
            {
                return;
            }

            var WantedEntity = new ImageToWorldData
            {
                    Path = new[]
                    {
                            "Metadata/Chests/VaultTreasurePile"
                    },
                    ImagePath = $"{CustomImagePath}Coin.png",
                    ImageSize = Settings.VaultPilesOnFloorSize
            };
            DrawImageToWorld_Chest(WantedEntity);
        }

        public void DrawImageToWorld(ImageToWorldData Information)
        {
            foreach (var Entity in _EntityCollection)
            {
                foreach (var WantedPath in Information.Path)
                {
                    if (Entity.Path.Contains(WantedPath))
                    {
                        var Camera = GameController.Game.IngameState.Camera;
                        var ChestScreenCoords = Camera.WorldToScreen(Entity.Pos.Translate(0, 0, Information.YOffset), Entity);
                        var IconRect = new RectangleF(ChestScreenCoords.X - Information.ImageSize / 2,
                                ChestScreenCoords.Y - Information.ImageSize / 2, Information.ImageSize, Information.ImageSize);
                        if (ChestScreenCoords == new Vector2())
                        {
                            continue;
                        }

                        // create rect at chest location to draw icon
                        if (Information.HasColor)
                        {
                            Graphics.DrawPluginImage(Information.ImagePath, IconRect, Information.Color);
                        }
                        else
                        {
                            Graphics.DrawPluginImage(Information.ImagePath, IconRect);
                        }
                    }
                }
            }
        }

        public void DrawImageToWorld_Chest(ImageToWorldData Information)
        {
            foreach (var Entity in _EntityCollection)
            {
                foreach (var WantedPath in Information.Path)
                {
                    if (!Entity.GetComponent<Chest>().IsOpened && Entity.Path.Contains(WantedPath))
                    {
                        var Camera = GameController.Game.IngameState.Camera;
                        var ChestScreenCoords = Camera.WorldToScreen(Entity.Pos.Translate(0, 0, Information.YOffset), Entity);
                        var IconRect = new RectangleF(ChestScreenCoords.X - Information.ImageSize / 2,
                                ChestScreenCoords.Y - Information.ImageSize / 2, Information.ImageSize, Information.ImageSize);
                        if (ChestScreenCoords == new Vector2())
                        {
                            continue;
                        }

                        // create rect at chest location to draw icon
                        if (Information.HasColor)
                        {
                            Graphics.DrawPluginImage(Information.ImagePath, IconRect, Information.Color);
                        }
                        else
                        {
                            Graphics.DrawPluginImage(Information.ImagePath, IconRect);
                        }
                    }
                }
            }
        }

        public void DrawImageToWorld_Shrine(ImageToWorldData Information)
        {
            foreach (var Entity in _EntityCollection)
            {
                foreach (var WantedPath in Information.Path)
                {
                    if (Entity.GetComponent<Shrine>().IsAvailable && Entity.Path.Contains(WantedPath))
                    {
                        var Camera = GameController.Game.IngameState.Camera;
                        var ChestScreenCoords = Camera.WorldToScreen(Entity.Pos.Translate(0, 0, Information.YOffset), Entity);
                        if (ChestScreenCoords == new Vector2())
                        {
                            continue;
                        }

                        // create rect at chest location to draw icon
                        var IconRect = new RectangleF(ChestScreenCoords.X - Information.ImageSize / 2,
                                ChestScreenCoords.Y - Information.ImageSize / 2, Information.ImageSize, Information.ImageSize);
                        if (Information.HasColor)
                        {
                            Graphics.DrawPluginImage(Information.ImagePath, IconRect, Information.Color);
                        }
                        else
                        {
                            Graphics.DrawPluginImage(Information.ImagePath, IconRect);
                        }
                    }
                }
            }
        }

        public class ImageToWorldData
        {
            public Color Color;

            public bool HasColor;

            public string ImagePath;

            public int ImageSize = 10;

            public string[] Path =
            {
            };

            public int YOffset;
        }
    }
}