using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using Newtonsoft.Json;
using PoeHUD.Controllers;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud;
using PoeHUD.Hud.Settings;
using PoeHUD.Models;
using PoeHUD.Models.Enums;
using PoeHUD.Poe;
using PoeHUD.Poe.Components;
using PoeHUD.Poe.RemoteMemoryObjects;
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
        public float CurrentDelveMapZoom = 0.635625f;
        public void RandomFeaturesMenu(int idIn, out int idPop)
        {
            idPop = idIn;
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
                Settings.ShowInLabOnly.Value = ImGuiExtension.Checkbox("Show Lab Stuff in Lab Only", Settings.ShowInLabOnly);

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
                        ImGui.PushID(idPop);
                        Settings.RewardDangerCurrencyColor.Value = ImGuiExtension.ColorPicker("Currency", Settings.RewardDangerCurrencyColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardDangerCurrencyQualityColor.Value =
                                ImGuiExtension.ColorPicker("Quality Currency", Settings.RewardDangerCurrencyQualityColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardDangerUniqueColor.Value = ImGuiExtension.ColorPicker("Unique", Settings.RewardDangerUniqueColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardDangerDivinationColor.Value = ImGuiExtension.ColorPicker("Divination", Settings.RewardDangerDivinationColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardDangerLowGemColor.Value = ImGuiExtension.ColorPicker("Low Gems", Settings.RewardDangerLowGemColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardDangerCorVaalColor.Value = ImGuiExtension.ColorPicker("Corrupted Vaal", Settings.RewardDangerCorVaalColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardDangerJewelleryColor.Value = ImGuiExtension.ColorPicker("Jewelery", Settings.RewardDangerJewelleryColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardDangerGenericColor.Value = ImGuiExtension.ColorPicker("Generic", Settings.RewardDangerGenericColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Silver"))
                    {
                        ImGui.PushID(idPop);
                        Settings.RewardSilverCurrencyColor.Value = ImGuiExtension.ColorPicker("Currency", Settings.RewardSilverCurrencyColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardSilverCurrencyQualityColor.Value =
                                ImGuiExtension.ColorPicker("Quality Currency", Settings.RewardSilverCurrencyQualityColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardSilverJewelryUniqueColor.Value =
                                ImGuiExtension.ColorPicker("Unique Jewelery", Settings.RewardSilverJewelryUniqueColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardSilverDivinationColor.Value = ImGuiExtension.ColorPicker("Divination", Settings.RewardSilverDivinationColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardSilverUniqueOneColor.Value = ImGuiExtension.ColorPicker("Unique 1", Settings.RewardSilverUniqueOneColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardSilverUniqueTwoColor.Value = ImGuiExtension.ColorPicker("Unique 2", Settings.RewardSilverUniqueTwoColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardSilverUniqueThreeColor.Value = ImGuiExtension.ColorPicker("Unique 3", Settings.RewardSilverUniqueThreeColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardSilverSkillGemColor.Value = ImGuiExtension.ColorPicker("Skill Gems", Settings.RewardSilverSkillGemColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.TreePop();
                    }

                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Dark Shrines"))
                {
                    Settings.DarkshrinesOnFloor.Value = ImGuiExtension.Checkbox("Draw On The Floor", Settings.DarkshrinesOnFloor);
                    ImGui.PushID(idPop);
                    Settings.DarkshrinesOnFloorSize.Value = ImGuiExtension.IntSlider("Size", Settings.DarkshrinesOnFloorSize);
                    ImGui.PopID();
                    idPop++;
                    ImGui.Spacing();
                    Settings.DarkshrinesOnMap.Value = ImGuiExtension.Checkbox("Draw On The Map", Settings.DarkshrinesOnMap);
                    ImGui.PushID(idPop);
                    Settings.DarkshrinesIcon.Value = ImGuiExtension.IntSlider("Size", Settings.DarkshrinesIcon);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    Settings.DarkshrinesColor.Value = ImGuiExtension.ColorPicker("Color", Settings.DarkshrinesColor);
                    ImGui.PopID();
                    idPop++;
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
                    ImGui.PushID(idPop);
                    Settings.HiddenDoorwayIcon.Value = ImGuiExtension.IntSlider("Size", Settings.HiddenDoorwayIcon);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    Settings.HiddenDoorwayColor.Value = ImGuiExtension.ColorPicker("Color", Settings.HiddenDoorwayColor);
                    ImGui.PopID();
                    idPop++;
                    ImGui.Spacing();
                    Settings.SecretPassage.Value = ImGuiExtension.Checkbox("Secret Passage", Settings.SecretPassage);
                    ImGui.PushID(idPop);
                    Settings.SecretPassageIcon.Value = ImGuiExtension.IntSlider("Size", Settings.SecretPassageIcon);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    Settings.SecretPassageColor.Value = ImGuiExtension.ColorPicker("Color", Settings.SecretPassageColor);
                    ImGui.PopID();
                    idPop++;
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
                        ImGui.PushID(idPop);
                        Settings.RoombasColor.Value = ImGuiExtension.ColorPicker("Text", Settings.RoombasColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.Spacing();
                        ImGui.PushID(idPop);
                        Settings.RoombasOnMap.Value = ImGuiExtension.Checkbox("On Map", Settings.RoombasOnMap);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RoombasOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.RoombasOnMapSize);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RoombasOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.RoombasOnMapColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Spinners"))
                    {
                        Settings.Spinners.Value = ImGuiExtension.Checkbox(Settings.Spinners.Value ? "Show" : "Hidden", Settings.Spinners);
                        ImGui.PushID(idPop);
                        Settings.SpinnersColor.Value = ImGuiExtension.ColorPicker("Text", Settings.SpinnersColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.Spacing();
                        ImGui.PushID(idPop);
                        Settings.SpinnersOnMap.Value = ImGuiExtension.Checkbox("On Map", Settings.SpinnersOnMap);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.SpinnersOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.SpinnersOnMapSize);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.SpinnersOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.SpinnersOnMapColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Saws"))
                    {
                        Settings.Saws.Value = ImGuiExtension.Checkbox(Settings.Saws.Value ? "Show" : "Hidden", Settings.Saws);
                        ImGui.PushID(idPop);
                        Settings.SawsColor.Value = ImGuiExtension.ColorPicker("Text", Settings.SawsColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.Spacing();
                        ImGui.PushID(idPop);
                        Settings.SawsOnMap.Value = ImGuiExtension.Checkbox("On Map", Settings.SawsOnMap);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.SawsOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.SawsOnMapSize);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.SawsOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.SawsOnMapColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Arrows"))
                    {
                        Settings.Arrows.Value = ImGuiExtension.Checkbox(Settings.Arrows.Value ? "Show" : "Hidden", Settings.Arrows);
                        ImGui.PushID(idPop);
                        Settings.ArrowsColor.Value = ImGuiExtension.ColorPicker("Text", Settings.ArrowsColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Preassure Plates"))
                    {
                        Settings.PressurePlates.Value =
                                ImGuiExtension.Checkbox(Settings.PressurePlates.Value ? "Show" : "Hidden", Settings.PressurePlates);
                        ImGui.PushID(idPop);
                        Settings.PressurePlatesColor.Value = ImGuiExtension.ColorPicker("Text", Settings.PressurePlatesColor);
                        ImGui.PopID();
                        idPop++;
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
                    ImGui.PushID(idPop);
                    Settings.NormalShrinesIcon.Value = ImGuiExtension.IntSlider("Size", Settings.NormalShrinesIcon);
                    ImGui.PopID();
                    idPop++;
                    ImGui.Spacing();
                    Settings.NormalShrineOnMap.Value = ImGuiExtension.Checkbox("Draw On The Map", Settings.NormalShrineOnMap);
                    ImGui.PushID(idPop);
                    Settings.NormalShrineOnFloorSize.Value = ImGuiExtension.IntSlider("Size", Settings.NormalShrineOnFloorSize);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    Settings.NormalShrinesColor.Value = ImGuiExtension.ColorPicker("Color", Settings.NormalShrinesColor);
                    ImGui.PopID();
                    idPop++;
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("Lesser"))
                {
                    Settings.LesserShrines.Value = ImGuiExtension.Checkbox("Draw On The Floor", Settings.LesserShrines);
                    ImGui.PushID(idPop);
                    Settings.LesserShrinesIcon.Value = ImGuiExtension.IntSlider("Size", Settings.LesserShrinesIcon);
                    ImGui.PopID();
                    idPop++;
                    ImGui.Spacing();
                    Settings.LesserShrineOnMap.Value = ImGuiExtension.Checkbox("Draw On The Map", Settings.LesserShrineOnMap);
                    ImGui.PushID(idPop);
                    Settings.LesserShrineOnFloorSize.Value = ImGuiExtension.IntSlider("Size", Settings.LesserShrineOnFloorSize);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    Settings.LesserShrinesColor.Value = ImGuiExtension.ColorPicker("Color", Settings.LesserShrinesColor);
                    ImGui.PopID();
                    idPop++;
                    ImGui.TreePop();
                }

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Vault Gold Piles"))
            {
                Settings.VaultPilesOnFloor.Value = ImGuiExtension.Checkbox("Draw On The Floor", Settings.VaultPilesOnFloor);
                ImGui.PushID(idPop);
                Settings.VaultPilesOnFloorSize.Value = ImGuiExtension.IntSlider("Size", Settings.VaultPilesOnFloorSize);
                ImGui.PopID();
                idPop++;
                ImGui.Spacing();
                Settings.VaultPilesOnMap.Value = ImGuiExtension.Checkbox("Draw On The Map", Settings.VaultPilesOnMap);
                ImGui.PushID(idPop);
                Settings.VaultPilesIcon.Value = ImGuiExtension.IntSlider("Size", Settings.VaultPilesIcon);
                ImGui.PopID();
                idPop++;
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Specter Bodies"))
            {
                ImGui.PushID(idPop);
                Settings.Specters.Value = ImGuiExtension.Checkbox("Show", Settings.Specters);
                ImGui.PopID();
                idPop++;
                ImGui.Spacing();
                Settings.TukohamasVanguard.Value = ImGuiExtension.Checkbox("Tukohama's Vanguard", Settings.TukohamasVanguard);
                Settings.WickerMan.Value = ImGuiExtension.Checkbox("WickerMan", Settings.WickerMan);
                Settings.PockedLanternbearer.Value = ImGuiExtension.Checkbox("Pocked Lanternbearer", Settings.PockedLanternbearer);
                Settings.SolarGuard.Value = ImGuiExtension.Checkbox("Solar Guard", Settings.SolarGuard);
                Settings.ChieftainMonkey.Value = ImGuiExtension.Checkbox("Chieftain Monkey", Settings.ChieftainMonkey);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("BATTLE ROYALE"))
            {
                Settings.RoyaleThings.Value = ImGuiExtension.Checkbox($"Show##{idPop}", Settings.RoyaleThings);
                idPop++;
                ImGui.Spacing();
                Settings.RoyaleHoardChests.Value = ImGuiExtension.Checkbox($"Hoard Chests (1 Unique)##{idPop}", Settings.RoyaleHoardChests);
                idPop++;
                Settings.RoyaleHoardSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.RoyaleHoardSize);
                idPop++;
                Settings.RoyaleHoardColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.RoyaleHoardColor);
                idPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyaleTroveChests.Value = ImGuiExtension.Checkbox($"Trove Chests (Currency)##{idPop}", Settings.RoyaleTroveChests);
                idPop++;
                Settings.RoyaleTroveSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.RoyaleTroveSize);
                idPop++;
                Settings.RoyaleTroveColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.RoyaleTroveColor);
                idPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyaleCacheChests.Value = ImGuiExtension.Checkbox($"Cache Chests (Mid Kit Gear)##{idPop}", Settings.RoyaleCacheChests);
                idPop++;
                Settings.RoyaleCacheSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.RoyaleCacheSize);
                idPop++;
                Settings.RoyaleCacheColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.RoyaleCacheColor);
                idPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyaleSuppliesChests.Value = ImGuiExtension.Checkbox($"Supplies Chests (Starter Kit Gear)##{idPop}", Settings.RoyaleSuppliesChests);
                idPop++;
                Settings.RoyaleSuppliesSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.RoyaleSuppliesSize);
                idPop++;
                Settings.RoyaleSuppliesColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.RoyaleSuppliesColor);
                idPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyaleExplosiveBarrelsChests.Value = ImGuiExtension.Checkbox($"Explosive Barrels (They dont hurt mobs)##{idPop}", Settings.RoyaleExplosiveBarrelsChests);
                idPop++;
                Settings.RoyaleExplosiveBarrelsSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.RoyaleExplosiveBarrelsSize);
                idPop++;
                Settings.RoyaleExplosiveBarrelsColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.RoyaleExplosiveBarrelsColor);
                idPop++;
                ImGui.Spacing();
                ImGui.Spacing();
                Settings.RoyalBETA.Value = ImGuiExtension.Checkbox($"World Position - Find Your Friends!##{idPop}", Settings.RoyalBETA);
                idPop++;
                Settings.RoyalLockedMudule.Value = ImGuiExtension.Checkbox($"locked Module##{idPop}", Settings.RoyalLockedMudule);
                idPop++;
                Settings.RoyalOverrideColors.Value = ImGuiExtension.Checkbox($"Override Module Colors##{idPop}", Settings.RoyalOverrideColors);
                idPop++;
                Settings.RoyalModuleBackground = ImGuiExtension.ColorPicker($"Module Background Color##{idPop}", Settings.RoyalModuleBackground);

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Beyond Things"))
            {
                Settings.BeyondThings.Value = ImGuiExtension.Checkbox($"Show##{idPop}", Settings.BeyondThings);
                idPop++;
                ImGui.Spacing();
                Settings.BeyondPortal.Value = ImGuiExtension.Checkbox($"Beyond Portal2##{idPop}", Settings.BeyondPortal);
                idPop++;
                Settings.BeyondPortalSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.BeyondPortalSize);
                idPop++;
                Settings.BeyondPortalColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.BeyondPortalColor);
                idPop++;

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Incursion DooDads"))
            {
                if (ImGui.TreeNode("Blocked Gateway"))
                {
                    Settings.ClosedIncursionDoor.Value = ImGuiExtension.Checkbox(Settings.ClosedIncursionDoor.Value ? "Show" : "Hidden", Settings.ClosedIncursionDoor);

                    ImGui.Spacing();
                    ImGui.PushID(idPop);
                    Settings.ClosedIncursionDoorOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.ClosedIncursionDoorOnMapSize);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    Settings.ClosedIncursionDoorOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.ClosedIncursionDoorOnMapColor);
                    ImGui.PopID();
                    idPop++;
                    ImGui.TreePop();
                }
                if (ImGui.TreeNode("Breach Chest"))
                {
                    Settings.IncursionBreachChest.Value = ImGuiExtension.Checkbox(Settings.IncursionBreachChest.Value ? "Show" : "Hidden", Settings.IncursionBreachChest);

                    ImGui.Spacing();
                    ImGui.PushID(idPop);
                    Settings.IncursionBreachChestOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.IncursionBreachChestOnMapSize);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    Settings.IncursionBreachChestOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.IncursionBreachChestOnMapColor);
                    ImGui.PopID();
                    idPop++;
                    ImGui.TreePop();
                }
                if (ImGui.TreeNode("Armour Chests"))
                {
                    Settings.IncursionChestArmorChest.Value = ImGuiExtension.Checkbox(Settings.IncursionChestArmorChest.Value ? "Show" : "Hidden", Settings.IncursionChestArmorChest);

                    ImGui.Spacing();
                    ImGui.PushID(idPop);
                    Settings.IncursionChestArmorChestOnMapSize.Value = ImGuiExtension.IntSlider("Size", Settings.IncursionChestArmorChestOnMapSize);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    Settings.IncursionChestArmorChestOnMapColor.Value = ImGuiExtension.ColorPicker("Color", Settings.IncursionChestArmorChestOnMapColor);
                    ImGui.PopID();
                    idPop++;
                    ImGui.TreePop();
                }
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Delve DooDads"))
            {
                if (ImGui.TreeNode("Delve Map Grid - Shown only when fully zoomed in"))
                {
                    ImGui.PushID(idPop);
                    Settings.DelveGridMap.Value = ImGuiExtension.Checkbox(Settings.DelveGridMap.Value ? "Show" : "Hidden", Settings.DelveGridMap);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    if (ImGui.Button("Set current Delve Zoom as the zoom you want to show grid"))
                    {
                        Settings.DelveGridMapScale = CurrentDelveMapZoom;
                    }
                    ImGui.PopID();
                    idPop++;
                    ImGui.TreePop();
                }
                if (ImGui.TreeNode("Delve Path's"))
                {
                    ImGui.PushID(idPop);
                    Settings.DelvePathWays.Value = ImGuiExtension.Checkbox(Settings.DelvePathWays.Value ? "Show" : "Hidden", Settings.DelvePathWays);
                    ImGui.PopID();
                    idPop++;

                    ImGui.Spacing();
                    ImGui.PushID(idPop);
                    Settings.DelvePathWaysNodeSize.Value = ImGuiExtension.IntSlider("Size", Settings.DelvePathWaysNodeSize);
                    ImGui.PopID();
                    idPop++;
                    ImGui.PushID(idPop);
                    Settings.DelvePathWaysNodeColor = ImGuiExtension.ColorPicker("Color", Settings.DelvePathWaysNodeColor);
                    ImGui.PopID();
                    idPop++;
                    ImGui.Spacing();
                    ImGui.Spacing();
                    Settings.DelveWall.Value = ImGuiExtension.Checkbox($"Breakable Wall##{idPop}", Settings.DelveWall);
                    idPop++;
                    Settings.DelveWallSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.DelveWallSize);
                    idPop++;
                    Settings.DelveWallColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.DelveWallColor);
                    idPop++;
                    ImGui.TreePop();
                }
                if (ImGui.TreeNode("Delve Chests"))
                {
                    ImGui.PushID(idPop);
                    Settings.DelveChests.Value = ImGuiExtension.Checkbox(Settings.DelvePathWays.Value ? "Show" : "Hidden", Settings.DelveChests);
                    ImGui.PopID();
                    idPop++;
                    ImGui.Spacing();
                    ImGui.Spacing();
                    Settings.DelvePathwayChest.Value = ImGuiExtension.Checkbox($"Hidden Chests on the way##{idPop}", Settings.DelvePathwayChest);
                    idPop++;
                    Settings.DelvePathwayChestSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.DelvePathwayChestSize);
                    idPop++;
                    Settings.DelvePathwayChestColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.DelvePathwayChestColor);
                    idPop++;
                    ImGui.Spacing();
                    ImGui.Spacing();
                    Settings.DelveMiningSuppliesDynamiteChest.Value = ImGuiExtension.Checkbox($"Dynamite Supplies##{idPop}", Settings.DelveMiningSuppliesDynamiteChest);
                    idPop++;
                    Settings.DelveMiningSuppliesDynamiteChestSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.DelveMiningSuppliesDynamiteChestSize);
                    idPop++;
                    Settings.DelveMiningSuppliesDynamiteChestColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.DelveMiningSuppliesDynamiteChestColor);
                    idPop++;
                    ImGui.Spacing();
                    ImGui.Spacing();
                    Settings.DelveMiningSuppliesFlaresChest.Value = ImGuiExtension.Checkbox($"Flare Supplies##{idPop}", Settings.DelveMiningSuppliesFlaresChest);
                    idPop++;
                    Settings.DelveMiningSuppliesFlaresChestSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.DelveMiningSuppliesFlaresChestSize);
                    idPop++;
                    Settings.DelveMiningSuppliesFlaresChestColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.DelveMiningSuppliesFlaresChestColor);
                    idPop++;
                    ImGui.Spacing();
                    ImGui.Spacing();
                    Settings.DelveCurrencyChest.Value = ImGuiExtension.Checkbox($"Currency Chests##{idPop}", Settings.DelveCurrencyChest);
                    idPop++;
                    Settings.DelveCurrencyChestSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.DelveCurrencyChestSize);
                    idPop++;
                    Settings.DelveCurrencyChestColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.DelveCurrencyChestColor);
                    idPop++;
                    ImGui.Spacing();
                    ImGui.Spacing();
                    Settings.DelveAzuriteVeinChest.Value = ImGuiExtension.Checkbox($"Azurite Veins##{idPop}", Settings.DelveAzuriteVeinChest);
                    idPop++;
                    Settings.DelveAzuriteVeinChestSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.DelveAzuriteVeinChestSize);
                    idPop++;
                    Settings.DelveAzuriteVeinChestColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.DelveAzuriteVeinChestColor);
                    idPop++;
                    ImGui.Spacing();
                    ImGui.Spacing();
                    Settings.DelveResonatorChest.Value = ImGuiExtension.Checkbox($"Resonator Chests##{idPop}", Settings.DelveResonatorChest);
                    idPop++;
                    Settings.DelveResonatorChestSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.DelveResonatorChestSize);
                    idPop++;
                    Settings.DelveResonatorChestColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.DelveResonatorChestColor);
                    idPop++;
                    ImGui.Spacing();
                    ImGui.Spacing();
                    Settings.DelveFossilChest.Value = ImGuiExtension.Checkbox($"Fossil Chests##{idPop}", Settings.DelveFossilChest);
                    idPop++;
                    Settings.DelveFossilChestSize.Value = ImGuiExtension.IntSlider($"Size##{idPop}", Settings.DelveFossilChestSize);
                    idPop++;
                    Settings.DelveFossilChestColor.Value = ImGuiExtension.ColorPicker($"Color##{idPop}", Settings.DelveFossilChestColor);
                    idPop++;
                    ImGui.Spacing();
                    ImGui.Spacing();
                }
                ImGui.TreePop();
            }
            if (ImGui.TreeNode("Monster Stats On Hover - This is strange and only shows the resistances once that resistance has been affected with + or -"))
            {
                Settings.MonsterHoverStats.Value = ImGuiExtension.Checkbox($"Show##{idPop}", Settings.MonsterHoverStats);
                idPop++;

                ImGui.TreePop();
            }

            Settings._Debug = ImGuiExtension.Checkbox("Debug", Settings._Debug);
        }
        private ImVector4 ToImVector4(Vector4 vector) => new ImVector4(vector.X, vector.Y, vector.Z, vector.W);

        public FossilTiers FossilList = new FossilTiers();

        public void BETA_ROYALE()
        {
            if (!Settings.RoyalBETA)
            {
                return;
            }

            var refBool = true;
            var menuOpacity = ImGui.GetStyle().GetColor(ColorTarget.WindowBg).W;
            if (Settings.RoyalOverrideColors)
            {
                ImGui.PushStyleColor(ColorTarget.WindowBg, ToImVector4(Settings.RoyalModuleBackground.ToVector4()));
                menuOpacity = ImGui.GetStyle().GetColor(ColorTarget.WindowBg).W;
            }

            ImGui.BeginWindow("ROYAL LOCATION", ref refBool, new ImVector2(200, 150), menuOpacity,
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

            var location = LocalPlayer.Entity.GetComponent<Positioned>().WorldPos;
            location = new Vector2((float) Math.Ceiling(location.X), (float) Math.Ceiling(location.Y));
            ImGui.Text("World Position");
            ImGui.BulletText($"X: {location.X}");
            ImGui.BulletText($"Y: {location.Y}");
            ImGui.EndWindow();
        }

        public bool IsTargeted(EntityWrapper @entity)
        {
        return @entity.GetComponent<Targetable>().Address != 0 && Memory.ReadBytes(@entity.GetComponent<Targetable>().Address + 0x30, 3)[2] == 1;
        }


        //private string TryGetStat(GameStat stat, EntityWrapper entity)
        //{
        //    return entity.GetComponent<Stats>().StatDictionary.TryGetValue(stat, out int statInt) ? statInt.ToString() : "?";
        //}

        private void MonsterResistnaceOnHover()
        {
            // TODO: Fix this error
            //return;
            //if (!Settings.MonsterHoverStats) return;
            //foreach (EntityWrapper entity in _entityCollection.Values.ToList())
            //{
            //    if (entity is null) continue;
            //    if (entity.IsValid)
            //        if (entity.HasComponent<Monster>())
            //            if (entity.IsAlive)
            //                if (IsTargeted(entity))
            //                {
            //                    var FireRes = TryGetStat(GameStat.FireDamageResistancePct, entity);
            //                    var ColdRes = TryGetStat(GameStat.ColdDamageResistancePct, entity);
            //                    var LightRes = TryGetStat(GameStat.LightningDamageResistancePct, entity);
            //                    var ChaosRes = TryGetStat(GameStat.ChaosDamageResistancePct, entity);
            //                    Element MonsterBox = MonsterTopName();
            //                    if (MonsterBox.Children[0].Width > 0)
            //                    {
            //                        RectangleF pos = MonsterBox.Children[0].GetClientRect();
            //                        int TextSize = (int) pos.Height;
            //                        int nextTextSpace = 0;
            //                        string NextText = $"{FireRes}";
            //                        string @string = NextText;
            //                        Graphics.DrawText(NextText, TextSize, new Vector2(pos.X + 10 + pos.Width + nextTextSpace, pos.Y), new Color(255, 85, 85, 255));
            //                        nextTextSpace += Graphics.MeasureText(NextText, TextSize).Width;
            //                        NextText = $" {ColdRes}";
            //                        @string += NextText;
            //                        Graphics.DrawText(NextText, TextSize, new Vector2(pos.X + 10 + pos.Width + nextTextSpace, pos.Y), new Color(77, 77, 255, 255));
            //                        nextTextSpace += Graphics.MeasureText(NextText, TextSize).Width;
            //                        NextText = $" {LightRes}";
            //                        @string += NextText;
            //                        Graphics.DrawText(NextText, TextSize, new Vector2(pos.X + 10 + pos.Width + nextTextSpace, pos.Y), new Color(253, 245, 75, 255));
            //                        nextTextSpace += Graphics.MeasureText(NextText, TextSize).Width;
            //                        NextText = $" {ChaosRes}";
            //                        @string += NextText;
            //                        Graphics.DrawText(NextText, TextSize, new Vector2(pos.X + 10 + pos.Width + nextTextSpace, pos.Y), new Color(255, 91, 179, 255));
            //                        Graphics.DrawBox(
            //                                new RectangleF(pos.X + 10 + pos.Width, pos.Y, Graphics.MeasureText(@string, TextSize).Width, pos.Height),
            //                                Color.Black);
            //                    }
            //                }
            //}
        }
        public Element MonsterTopName()
        {
            try
            {
                return GameController.Game.IngameState.UIRoot.Children[1]?.Children[11]?.Children[8];
            }
            catch (Exception)
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
                DelveMapNodes();
                SpectreLabels();

                if (Settings.ShowInLabOnly)
                {
                    if (GameController.Game.IngameState.Data.CurrentWorldArea.IsLabyrinthArea)
                        LabyrinthLabels();
                }
                else
                {
                    LabyrinthLabels();
                }

            }

        }

        private void SpectreLabels()
        {
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

                if (Settings.ChieftainMonkey)
                {
                    DrawTextLabelSpecter(Color.Yellow, "Chieftain Monkey", "Metadata/Monsters/BloodChieftain/MonkeyChiefBloodEnrage", 120);
                }
            }
        }

        private void LabyrinthLabels()
        {
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

        private void DelveMapNodes()
        {
            if (!Settings.DelveGridMap) return;
            var delveMap = GameController.Game.IngameState.UIRoot.Children[1].Children[59];
            if (!delveMap.IsVisible) return;
            try
            {
                var largeGridList = delveMap.Children[0].Children[0].Children[2].Children.ToList();
                var scale = delveMap.Children[0].Children[0].Children[2].Scale;
                CurrentDelveMapZoom = scale;

                if (scale != Settings.DelveGridMapScale) return;
                //LogMessage($"Count: {largeGrids.Count}", 5);
                for (var i = 0; i < largeGridList.Count; i++)
                {
                    var largeGrid = largeGridList[i];

                    if (!largeGrid.GetClientRect().Intersects(delveMap.GetClientRect())) continue;

                    var smallGridList = largeGrid.Children.ToList();
                    for (var j = 0; j < smallGridList.Count - 1; j++)
                    {
                        var smallGrid = smallGridList[j];

                        //var newRec = new RectangleF(
                        //    (smallGrid.GetClientRect().X * 5.69f) * scale, (smallGrid.GetClientRect().Y * 5.69f) * scale,
                        //    (smallGrid.GetClientRect().Width), smallGrid.GetClientRect().Height);

                        if (smallGrid.GetClientRect().Intersects(delveMap.GetClientRect()))
                            Graphics.DrawFrame(smallGrid.GetClientRect(), 1, Color.DarkGray);
                    }
                }
            }
            catch
            {

            }
        }

        private void DrawTextLabelEquals(ColorBGRA color, string text, string path)
        {
            foreach (var entity in _entityCollection.Values.ToList())
            {
                if (entity is null) continue;
                if (entity.Path.ToLower().Equals(path.ToLower()))
                {
                    var camera = GameController.Game.IngameState.Camera;
                    var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords == new Vector2())
                    {
                        continue;
                    }

                    var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);
                    float maxWidth = 0;
                    float maxheight = 0;
                    var size = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y += size.Height;
                    maxheight += size.Height;
                    maxWidth = Math.Max(maxWidth, size.Width);
                    Graphics.DrawBox(new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight),
                            Color.Black);
                }
            }
        }

        private void DrawTextLabelSpecter(ColorBGRA color, string text, string path, int zOffset = 0)
        {
            foreach (var entity in _entityCollection.Values.ToList())
            {
                if (entity is null) continue;
                if (entity.Path.ToLower().Contains(path.ToLower()) && !entity.IsAlive)
                {
                    var camera = GameController.Game.IngameState.Camera;
                    var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, zOffset), entity);
                    if (chestScreenCoords == new Vector2())
                    {
                        continue;
                    }

                    var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);
                    float maxWidth = 0;
                    float maxheight = 0;
                    var size = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y += size.Height;
                    maxheight += size.Height;
                    maxWidth = Math.Max(maxWidth, size.Width);
                    var background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                    Graphics.DrawBox(background, Color.Black);
                }
            }
        }

        public static string WordWrap(string input, int maxCharacters)
        {
            var lines = new List<string>();
            if (!input.Contains(" "))
            {
                var start = 0;
                while (start < input.Length)
                {
                    lines.Add(input.Substring(start, Math.Min(maxCharacters, input.Length - start)));
                    start += maxCharacters;
                }
            }
            else
            {
                var words = input.Split(' ');
                var line = "";
                foreach (var word in words)
                {
                    if ((line + word).Length > maxCharacters)
                    {
                        lines.Add(line.Trim());
                        line = "";
                    }

                    line += string.Format("{0} ", word);
                }

                if (line.Length > 0)
                {
                    lines.Add(line.Trim());
                }
            }

            var conectedLines = "";
            foreach (var line in lines)
            {
                conectedLines += line + "\n\r";
            }

            return conectedLines;
        }

        public class LargeMapData
        {
            public Camera @Camera { get; set; }
            public PoeHUD.Poe.Elements.Map @MapWindow { get; set; }
            public RectangleF @MapRec { get; set; }
            public Vector2 @PlayerPos { get; set; }
            public float @PlayerPosZ { get; set; }
            public Vector2 @ScreenCenter { get; set; }
            public float @Diag { get; set; }
            public float @K { get; set; }
            public float @Scale { get; set; }


            public LargeMapData(GameController GC)
            {
                @Camera = GC.Game.IngameState.Camera;
                @MapWindow = GC.Game.IngameState.IngameUi.Map;
                @MapRec = @MapWindow.GetClientRect();
                @PlayerPos = GC.Player.GetComponent<Positioned>().GridPos;
                @PlayerPosZ = GC.Player.GetComponent<Render>().Z;
                @ScreenCenter = new Vector2(@MapRec.Width / 2, @MapRec.Height / 2).Translate(0, -20)
                                   + new Vector2(@MapRec.X, @MapRec.Y)
                                   + new Vector2(@MapWindow.LargeMapShiftX, @MapWindow.LargeMapShiftY);
                @Diag = (float)Math.Sqrt(@Camera.Width * @Camera.Width + @Camera.Height * @Camera.Height);
                @K = @Camera.Width < 1024f ? 1120f : 1024f;
                @Scale = @K / @Camera.Height * @Camera.Width * 3f / 4f / @MapWindow.LargeMapZoom;
            }
        }

        private void DrawToLargeMiniMap(EntityWrapper entity)
        {
            var icon = GetMapIcon(entity);
            if (icon == null)
            {
                return;
            }

            var iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
            var point = LargeMapInformation.ScreenCenter
                        + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - LargeMapInformation.PlayerPos,
                            LargeMapInformation.Diag, LargeMapInformation.Scale,
                            (iconZ - LargeMapInformation.PlayerPosZ) /
                            (9f / LargeMapInformation.MapWindow.LargeMapZoom));

            var texture = icon.TextureIcon;
            var size = icon.Size * 2; // icon.SizeOfLargeIcon.GetValueOrDefault(icon.Size * 2);
            texture.DrawPluginImage(Graphics, new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size));
        }

        private void DrawToSmallMiniMap(EntityWrapper entity)
        {
            var icon = GetMapIcon(entity);
            if (icon == null)
            {
                return;
            }

            var smallMinimap = GameController.Game.IngameState.IngameUi.Map.SmallMinimap;
            var playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var posZ = GameController.Player.GetComponent<Render>().Z;
            const float scale = 240f;
            var mapRect = smallMinimap.GetClientRect();
            var mapCenter = new Vector2(mapRect.X + mapRect.Width / 2, mapRect.Y + mapRect.Height / 2).Translate(0, 0);
            var diag = Math.Sqrt(mapRect.Width * mapRect.Width + mapRect.Height * mapRect.Height) / 2.0;
            var iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
            var point = mapCenter + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, scale, (iconZ - posZ) / 20);
            var texture = icon.TextureIcon;
            var size = icon.Size;
            var rect = new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size);
            mapRect.Contains(ref rect, out var isContain);
            if (isContain)
            {
                texture.DrawPluginImage(Graphics, rect);
            }
        }

        private MapIcon GetMapIcon(EntityWrapper e)
        {
            if (Settings.RoyaleThings)
            {
                if (!e.GetComponent<Chest>().IsOpened)
                {
                    if (Settings.RoyaleHoardChests && e.Path.Contains("Metadata/Chests/RoyaleChestMidKitUnique"))
                    {
                        return new MapIcon(e,
                            new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RoyaleHoardColor),
                            () => Settings.RoyaleHoardChests, Settings.RoyaleHoardSize);
                    }

                    if (Settings.RoyaleTroveChests && e.Path.Contains("Metadata/Chests/RoyaleChestCurrency"))
                    {
                        return new MapIcon(e,
                            new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RoyaleTroveColor),
                            () => Settings.RoyaleTroveChests, Settings.RoyaleTroveSize);
                    }

                    if (Settings.RoyaleCacheChests && e.Path.Contains("Metadata/Chests/RoyaleChestMidKit"))
                    {
                        return new MapIcon(e,
                            new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RoyaleCacheColor),
                            () => Settings.RoyaleCacheChests, Settings.RoyaleCacheSize);
                    }

                    if (Settings.RoyaleSuppliesChests && e.Path.Contains("Metadata/Chests/RoyaleChestStarterKit"))
                    {
                        return new MapIcon(e,
                            new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RoyaleSuppliesColor),
                            () => Settings.RoyaleSuppliesChests, Settings.RoyaleSuppliesSize);
                    }

                    if (Settings.RoyaleExplosiveBarrelsChests &&
                        e.Path.Contains("Metadata/Chests/AtlasBarrelExplosive1"))
                    {
                        return new MapIcon(e,
                            new HudTexture(PoeHudImageLocation + "chest.png", Settings.RoyaleExplosiveBarrelsColor),
                            () => Settings.RoyaleExplosiveBarrelsChests, Settings.RoyaleExplosiveBarrelsSize);
                    }
                }
            }

            if (Settings.BeyondThings)
            {
                if (Settings.BeyondPortal && e.Path.Contains("Metadata/Monsters/BeyondDemons/BeyondPortal3"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "Demon_Altar.png", Settings.BeyondPortalColor),
                            () => Settings.BeyondPortal, Settings.BeyondPortalSize*1.5f);
                }
                if (Settings.BeyondPortal && e.Path.Contains("Metadata/Monsters/BeyondDemons/BeyondPortal2"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "Demon_Altar.png", Settings.BeyondPortalColor),
                            () => Settings.BeyondPortal, Settings.BeyondPortalSize);
                }
                if (Settings.BeyondPortal && e.Path.Contains("Metadata/Monsters/BeyondDemons/BeyondPortal1"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "Demon_Small_Blood.png", Settings.BeyondPortalColor),
                            () => Settings.BeyondPortal, Settings.BeyondPortalSize/2);
                }
            }

            if (Settings.Roombas && Settings.RoombasOnMap)
            {
                if (e.Path.Contains("LabyrinthFlyingRoomba") || e.Path.Contains("LabyrinthRoomba"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "roomba.png", Settings.RoombasOnMapColor), () => Settings.RoombasOnMap,
                            Settings.RoombasOnMapSize);
                }
            }

            if (Settings.Spinners && Settings.SpinnersOnMap)
            {
                if (e.Path.Contains("LabyrinthSpinner"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "roomba.png", Settings.SpinnersOnMapColor), () => Settings.SpinnersOnMap,
                            Settings.SpinnersOnMapSize);
                }
            }

            if (Settings.Saws && Settings.SawsOnMap)
            {
                if (e.Path.Contains("LabyrinthSawblade"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "roomba.png", Settings.SawsOnMapColor), () => Settings.SawsOnMap,
                            Settings.SawsOnMapSize);
                }
            }

            if (Settings.LabyrinthChest && !e.GetComponent<Chest>().IsOpened)
            {
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTreasureKey"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TreasureKeyChestColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTrinketChest"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TrinketChestColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthSpecificUnique"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.SpecificUniqueChestColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0Currency"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardCurrencyColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0CurrencyQuality"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardCurrencyQualityColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrency"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCurrencyColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrencyQuality"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCurrencyQualityColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerUnique"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerUniqueColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerDivination"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerDivinationColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerSkillGems"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerLowGemColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCorruptedVaal"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCorVaalColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerJewelry"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerJewelleryColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerGeneric"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerGenericColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrency"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverCurrencyColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrencyQuality"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverCurrencyQualityColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverJewelryUnique"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverJewelryUniqueColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverDivination"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverDivinationColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique1"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueOneColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique3"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueTwoColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique2"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueThreeColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }

                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverSkillGems"))
                {
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverSkillGemColor),
                            () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                }
            }

            if (Settings.Darkshrines)
            {
                if (Settings.DarkshrinesOnMap)
                {
                    if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/LabyrinthDarkshrineHidden"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "shrines.png", Settings.DarkshrinesColor),
                                () => Settings.DarkshrinesOnMap, Settings.DarkshrinesIcon);
                    }
                }
            }

            if (Settings.NormalShrines)
            {
                if (Settings.NormalShrineOnMap)
                {
                    if (e.Path.Contains("Metadata/Shrines/Shrine"))
                    {
                        if (e.GetComponent<Shrine>().IsAvailable)
                        {
                            return new MapIcon(e, new HudTexture(CustomImagePath + "shrines.png", Settings.NormalShrinesColor),
                                    () => Settings.NormalShrineOnMap, Settings.NormalShrinesIcon);
                        }
                    }
                }
            }

            if (Settings.LesserShrines)
            {
                if (Settings.LesserShrineOnMap)
                {
                    if (e.Path.Contains("Metadata/Shrines/LesserShrine"))
                    {
                        if (e.GetComponent<Shrine>().IsAvailable)
                        {
                            return new MapIcon(e, new HudTexture(CustomImagePath + "shrines.png", Settings.LesserShrinesColor),
                                () => Settings.LesserShrineOnMap, Settings.LesserShrinesIcon);
                        }
                    }
                }
            }

            if (Settings.HiddenDoorway)
            {
                if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Short")
                 || e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Long"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.HiddenDoorwayColor),
                            () => Settings.HiddenDoorway, Settings.HiddenDoorwayIcon);
                }
            }

            if (Settings.SecretPassage)
            {
                if (e.Path.Equals("Metadata/Terrain/Labyrinth/Objects/SecretPassage"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.SecretPassageColor),
                            () => Settings.SecretPassage, Settings.SecretPassageIcon);
                }
            }

            if (Settings.VaultPilesOnMap)
            {
                if (e.Path.Contains("Metadata/Chests/VaultTreasurePile"))
                {
                    if (!e.GetComponent<Chest>().IsOpened)
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "Coin.png"), () => true, Settings.VaultPilesIcon);
                    }
                }
            }

            if (Settings.AbyssCracks)
            {
                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssCrackSpawners/AbyssCrackSkeletonSpawner"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "abyss-crack.png", Settings.AbyssSmallNodeColor), () => true,
                            Settings.AbyssSmallNodeSize);
                }

                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeLarge")
                 || e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmall")
                 || e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmallMortarSpawned")
                 || e.Path.Contains("Metadata/Chests/Abyss/AbyssFinal"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "abyss-node-small.png", Settings.AbyssLargeNodeColor), () => true,
                            Settings.AbyssLargeNodeSize);
                }
            }

            if (Settings.DelvePathWays)
            {
                if (e.Path.EndsWith("Metadata/Terrain/Leagues/Delve/Objects/DelveLight"))
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "abyss-crack.png", Settings.DelvePathWaysNodeColor), () => true,
                            Settings.DelvePathWaysNodeSize);
                }
            }
            if (Settings.DelveChests)
            {
                if (!e.GetComponent<Chest>().IsOpened)
                {
                    if (e.Path.StartsWith("Metadata/Chests/DelveChests/DelveChestGenericLeague") || e.Path.Contains("Unique") && e.Path.StartsWith("Metadata/Chests/DelveChests"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//LeagueChest.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }
                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveMiningSuppliesDynamite"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Bombs.png", Settings.DelveMiningSuppliesDynamiteChestColor),
                            () => Settings.DelveMiningSuppliesDynamiteChest, Settings.DelveMiningSuppliesDynamiteChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveMiningSuppliesFlares"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Flare.png", Settings.DelveMiningSuppliesFlaresChestColor),
                            () => Settings.DelveMiningSuppliesFlaresChest, Settings.DelveMiningSuppliesFlaresChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("PathCurrency"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Currency.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("DynamiteCurrency"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Currency.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("AdditionalSockets"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//AdditionalSockets.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("AtziriFragment"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Fragment.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("PaleCourtFragment"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//PaleCourtComplete.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Essence"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Essence.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("SilverCoin"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//SilverCoin.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("WisdomScroll"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//WisDomCurrency.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Divination"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//divinationCard.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveChestCurrency"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Currency.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("DelveAzuriteVein") && !e.Path.Contains("Encounter"))
                    {
                        return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.DelveAzuriteVeinChestColor),
                            () => Settings.DelveAzuriteVeinChest, Settings.DelveAzuriteVeinChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Resonator3") && e.Path.Contains("Resonator4") && e.Path.Contains("Resonator5"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//ResonatorT1.png", Settings.DelveResonatorChestColor),
                            () => Settings.DelveResonatorChest, Settings.DelveResonatorChestSize * 0.7f);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Resonator2"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//ResonatorT2.png", Settings.DelveResonatorChestColor),
                            () => Settings.DelveResonatorChest, Settings.DelveResonatorChestSize * 0.7f);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Resonator1"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//ResonatorT3.png", Settings.DelveResonatorChestColor),
                            () => Settings.DelveResonatorChest, Settings.DelveResonatorChestSize * 0.7f);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveChestArmourMovementSpeed"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//SpeedArmour.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests/DelveChestSpecialUniqueMana"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//UniqueManaFlask.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize * 1.3f);
                    }

                    if (e.Path.Contains("RandomEnchant") && e.Path.StartsWith("Metadata/Chests/DelveChests"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Enchant.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }
                    
                    if (e.Path.Contains("FossilChest") && e.Path.StartsWith("Metadata/Chests/DelveChests"))
                    {
                        foreach (var @string in FossilList.T1)
                        {
                            if (e.Path.ToLower().Contains(@string.ToLower()))
                            {
                                return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//AbberantFossilT1.png", Settings.DelveFossilChestColor),
                                    () => Settings.DelveFossilChest, Settings.DelveFossilChestSize);
                            }
                        }
                        foreach (var @string in FossilList.T2)
                        {
                            if (e.Path.ToLower().Contains(@string.ToLower()))
                            {
                                return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//AbberantFossilT2.png", Settings.DelveFossilChestColor),
                                    () => Settings.DelveFossilChest, Settings.DelveFossilChestSize);
                            }
                        }
                        foreach (var @string in FossilList.T3)
                        {
                            if (e.Path.ToLower().Contains(@string.ToLower()))
                            {
                                return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//AbberantFossilT3.png", Settings.DelveFossilChestColor),
                                    () => Settings.DelveFossilChest, Settings.DelveFossilChestSize);
                            }
                        }


                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//AbberantFossil.png", Settings.DelveFossilChestColor),
                            () => Settings.DelveFossilChest, Settings.DelveFossilChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Resonator"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//ResonatorT1.png", Settings.DelveResonatorChestColor),
                            () => Settings.DelveResonatorChest, Settings.DelveResonatorChestSize * 0.7f);
                    }

                    
                    ///////

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Map"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Map.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Corrupted"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//Corrupted.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("6Linked"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//SixLink.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("GemHighLevel"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//HighLevelGem.png", Settings.DelveCurrencyChestColor),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.Contains("Metadata/Chests/DelveChests") && e.Path.Contains("Trinkets"))
                    {
                        return new MapIcon(e, new HudTexture(CustomImagePath + "//Delve//chest_jewelers.png", Color.Yellow),
                            () => Settings.DelveCurrencyChest, Settings.DelveCurrencyChestSize);
                    }

                    if (e.Path.StartsWith("Metadata/Terrain/Leagues/Delve/Objects/DelveWall"))
                    {

                        switch (e.IsAlive)
                        {
                            case false:
                                return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.DelveWallColor),
                                    () => Settings.DelveWall, Settings.DelveWallSize);
                            case true:
                                return new MapIcon(e, new HudTexture(CustomImagePath + "gate.png", Settings.DelveWallColor),
                                    () => Settings.DelveWall, Settings.DelveWallSize);
                        }
                    }

                    // catch missing delve chests
                    if (Settings.DelvePathwayChest)
                    {
                        if (e.Path.Contains("Metadata/Chests/DelveChests") && !e.Path.Contains("Encounter"))
                        {
                            return new MapIcon(e,
                                new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.DelvePathwayChestColor),
                                () => true,
                                Settings.DelvePathwayChestSize);
                        }
                    }
                }
            }

            if (Settings.AbysshoardChestToggleNode)
            {
                if (e.Path.Contains("Metadata/Chests/AbyssChest") && !e.GetComponent<Chest>().IsOpened)
                {
                    return new MapIcon(e, new HudTexture(CustomImagePath + "exclamationmark.png", Settings.AbysshoardChestColor),
                            () => Settings.AbysshoardChestToggleNode, Settings.AbysshoardChestSize);
                }
            }

            if (Settings.Incursion)
            {
                if (Settings.ClosedIncursionDoor)
                {
                    if (e.Path.Contains("Metadata/Terrain/Leagues/Incursion/Objects/ClosedDoorPresent_Fuse"))
                    {

                        if (Settings.ClosedIncursionDoorOnMapColor != Color.White)
                        {
                            return new MapIcon(e, new HudTexture(CustomImagePath + "gate.png", Settings.ClosedIncursionDoorOnMapColor),
                                () => Settings.ClosedIncursionDoor, Settings.ClosedIncursionDoorOnMapSize);
                        }
                        else
                        {
                            return new MapIcon(e, new HudTexture(CustomImagePath + "gate.png"),
                                () => Settings.ClosedIncursionDoor, Settings.ClosedIncursionDoorOnMapSize);
                        }
                    }
                    if (e.Path.Contains("Metadata/Terrain/Leagues/Incursion/Objects/TempleInternalDoor"))
                    {

                        if (Settings.ClosedIncursionDoorOnMapColor != Color.White)
                        {
                            return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.ClosedIncursionDoorOnMapColor),
                                () => Settings.ClosedIncursionDoor, Settings.ClosedIncursionDoorOnMapSize);
                        }
                        else
                        {
                            return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png"),
                                () => Settings.ClosedIncursionDoor, Settings.ClosedIncursionDoorOnMapSize);
                        }
                    }
                }
                if (Settings.IncursionBreachChest)
                {
                    if (!e.GetComponent<Chest>().IsOpened)
                    {
                        if (e.Path.Contains("Metadata/Chests/IncursionChests/IncursionChestBreach"))
                        {
                            return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.IncursionBreachChestOnMapColor),
                                () => Settings.IncursionBreachChest, Settings.IncursionBreachChestOnMapSize);
                        }
                        if (e.Path.Contains("Metadata/Chests/IncursionChestArmour"))
                        {
                            return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.IncursionChestArmorChestOnMapColor),
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

            var wantedEntity = new ImageToWorldData
            {
                    Path = new[]
                    {
                            "Metadata/Monsters/Atziri/AtziriMirror"
                    },
                    ImagePath = $"{CustomImagePath}mirror.png",
                    ImageSize = Settings.AtziriMirrorSize,
                    YOffset = -90
            };
            DrawImageToWorld(wantedEntity);
        }

        private void RenderLieutenantSkeletonThorns()
        {
            if (!Settings.LieutenantofRage)
            {
                return;
            }

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
        
        public LargeMapData LargeMapInformation { get; set; }

        private void RenderMapImages()
        {
            var Area = GameController.Game.IngameState.Data.CurrentArea;
            if (Area.IsTown || Area.RawName.Contains("Hideout")) return;
            if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
            {
                LargeMapInformation = new LargeMapData(GameController);
                foreach (var entity in _entityCollection.Values.ToList())
                {
                    if (entity is null) continue;
                    DrawToLargeMiniMap(entity);
                }
            }
            else if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
            {
                foreach (var entity in _entityCollection.Values.ToList())
                {
                    if (entity is null) continue;
                    DrawToSmallMiniMap(entity);
                }
            }
        }

        private void RenderShrines()
        {
            var lesserShrine = new ImageToWorldData
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
            var normalShrine = new ImageToWorldData
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
            var darkShrine = new ImageToWorldData
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
                DrawImageToWorld_Shrine(lesserShrine);
            }

            if (Settings.NormalShrines && Settings.NormalShrineOnFloor)
            {
                DrawImageToWorld_Shrine(normalShrine);
            }

            if (Settings.Darkshrines && Settings.DarkshrinesOnFloor)
            {
                DrawImageToWorld_Shrine(darkShrine);
            }
        }

        private void RenderTextLabel(ColorBGRA color, string text, string path)
        {
            foreach (var entity in _entityCollection.Values.ToList())
            {
                if (entity is null) continue;
                if (entity.Path.ToLower().Contains(path.ToLower()))
                {
                    var camera = GameController.Game.IngameState.Camera;
                    var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, 0), entity);
                    if (chestScreenCoords == new Vector2())
                    {
                        continue;
                    }

                    var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);
                    float maxWidth = 0;
                    float maxheight = 0;
                    var size = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y += size.Height;
                    maxheight += size.Height;
                    maxWidth = Math.Max(maxWidth, size.Width);
                    var background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                    Graphics.DrawBox(background, Color.Black);
                }
            }
        }

        private void RenderTextLabel(ColorBGRA color, string text, string path, int zOffset)
        {
            foreach (var entity in _entityCollection.Values.ToList())
            {
                if (entity is null) continue;
                if (entity.Path.ToLower().Contains(path.ToLower()))
                {
                    var camera = GameController.Game.IngameState.Camera;
                    var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, zOffset), entity);
                    if (chestScreenCoords == new Vector2())
                    {
                        continue;
                    }

                    var iconRect = new Vector2(chestScreenCoords.X, chestScreenCoords.Y);
                    float maxWidth = 0;
                    float maxheight = 0;
                    var size = Graphics.DrawText(text, Settings.PluginTextSize, iconRect, color, FontDrawFlags.Center);
                    chestScreenCoords.Y += size.Height;
                    maxheight += size.Height;
                    maxWidth = Math.Max(maxWidth, size.Width);
                    var background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                    Graphics.DrawBox(background, Color.Black);
                }
            }
        }

        private void RenderVaultPiles()
        {
            if (!Settings.VaultPilesOnFloor || !Settings.VaultPiles)
            {
                return;
            }

            var wantedEntity = new ImageToWorldData
            {
                    Path = new[]
                    {
                            "Metadata/Chests/VaultTreasurePile"
                    },
                    ImagePath = $"{CustomImagePath}Coin.png",
                    ImageSize = Settings.VaultPilesOnFloorSize
            };
            DrawImageToWorld_Chest(wantedEntity);
        }

        public void DrawImageToWorld(ImageToWorldData information)
        {
            foreach (var entity in _entityCollection.Values.ToList())
            {
                if (entity is null) continue;
                foreach (var wantedPath in information.Path)
                {
                    if (entity.Path.Contains(wantedPath))
                    {
                        var camera = GameController.Game.IngameState.Camera;
                        var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, information.YOffset), entity);
                        var iconRect = new RectangleF(chestScreenCoords.X - information.ImageSize / 2,
                                chestScreenCoords.Y - information.ImageSize / 2, information.ImageSize, information.ImageSize);
                        if (chestScreenCoords == new Vector2())
                        {
                            continue;
                        }

                        // create rect at chest location to draw icon
                        if (information.HasColor)
                        {
                            Graphics.DrawPluginImage(information.ImagePath, iconRect, information.Color);
                        }
                        else
                        {
                            Graphics.DrawPluginImage(information.ImagePath, iconRect);
                        }
                    }
                }
            }
        }

        public void DrawImageToWorld_Chest(ImageToWorldData information)
        {
            foreach (var entity in _entityCollection.Values.ToList())
            {
                if (entity is null) continue;
                foreach (var wantedPath in information.Path)
                {
                    if (!entity.GetComponent<Chest>().IsOpened && entity.Path.Contains(wantedPath))
                    {
                        var camera = GameController.Game.IngameState.Camera;
                        var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, information.YOffset), entity);
                        var iconRect = new RectangleF(chestScreenCoords.X - information.ImageSize / 2,
                                chestScreenCoords.Y - information.ImageSize / 2, information.ImageSize, information.ImageSize);
                        if (chestScreenCoords == new Vector2())
                        {
                            continue;
                        }

                        // create rect at chest location to draw icon
                        if (information.HasColor)
                        {
                            Graphics.DrawPluginImage(information.ImagePath, iconRect, information.Color);
                        }
                        else
                        {
                            Graphics.DrawPluginImage(information.ImagePath, iconRect);
                        }
                    }
                }
            }
        }

        public void DrawImageToWorld_Shrine(ImageToWorldData information)
        {
            foreach (var entity in _entityCollection.Values.ToList())
            {
                if (entity is null) continue;
                foreach (var wantedPath in information.Path)
                {
                    if (entity.GetComponent<Shrine>().IsAvailable && entity.Path.Contains(wantedPath))
                    {
                        var camera = GameController.Game.IngameState.Camera;
                        var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, information.YOffset), entity);
                        if (chestScreenCoords == new Vector2())
                        {
                            continue;
                        }

                        // create rect at chest location to draw icon
                        var iconRect = new RectangleF(chestScreenCoords.X - information.ImageSize / 2,
                                chestScreenCoords.Y - information.ImageSize / 2, information.ImageSize, information.ImageSize);
                        if (information.HasColor)
                        {
                            Graphics.DrawPluginImage(information.ImagePath, iconRect, information.Color);
                        }
                        else
                        {
                            Graphics.DrawPluginImage(information.ImagePath, iconRect);
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


    public class FossilTiers
    {
        [JsonProperty("t1")]
        public string[] T1 { get; set; }

        [JsonProperty("t2")]
        public string[] T2 { get; set; }

        [JsonProperty("t3")]
        public string[] T3 { get; set; }
    }
}