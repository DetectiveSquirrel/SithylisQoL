using System;
using System.Collections.Generic;
using ImGuiNET;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud;
using PoeHUD.Models;
using PoeHUD.Poe.Components;
using Random_Features.Libs;
using SharpDX;
using SharpDX.Direct3D9;

namespace Random_Features
{
    partial class RandomFeatures
    {
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
                    Settings.AbysshoardChestToggleNode.Value = ImGuiExtension.Checkbox(Settings.AbysshoardChestToggleNode ? "Show" : "Hide", Settings.AbysshoardChestToggleNode);
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
                        Settings.RewardCurrencyQualityColor.Value = ImGuiExtension.ColorPicker("Quality Currency", Settings.RewardCurrencyQualityColor);
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Danger"))
                    {
                        ImGui.PushID(idPop);
                        Settings.RewardDangerCurrencyColor.Value = ImGuiExtension.ColorPicker("Currency", Settings.RewardDangerCurrencyColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardDangerCurrencyQualityColor.Value = ImGuiExtension.ColorPicker("Quality Currency", Settings.RewardDangerCurrencyQualityColor);
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
                        Settings.RewardSilverCurrencyQualityColor.Value = ImGuiExtension.ColorPicker("Quality Currency", Settings.RewardSilverCurrencyQualityColor);
                        ImGui.PopID();
                        idPop++;
                        ImGui.PushID(idPop);
                        Settings.RewardSilverJewelryUniqueColor.Value = ImGuiExtension.ColorPicker("Unique Jewelery", Settings.RewardSilverJewelryUniqueColor);
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
                    Settings.LieutenantofRageSize.Value = ImGuiExtension.IntSlider("##LieutenantofRageSize", "Size of icon", Settings.LieutenantofRageSize);
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
                        Settings.PressurePlates.Value = ImGuiExtension.Checkbox(Settings.PressurePlates.Value ? "Show" : "Hidden", Settings.PressurePlates);
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
                ImGui.TreePop();
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
                RenderTextLabel(Settings.PressurePlatesColor.Value, "Pressure" + Environment.NewLine + "Plate", "pressureplate_reset");
            if (Settings.Arrows)
                RenderTextLabel(Settings.ArrowsColor.Value, "Arrows", "Labyrintharrowtrap");
            if (Settings.SmashableDoor)
                RenderTextLabel(Settings.SmashableDoorColor.Value, "Smash Door", "Objects/LabyrinthSmashableDoor");
            if (Settings.SecretPassage)
                DrawTextLabelEquals(Settings.SecretPassageColor.Value, "Secret" + Environment.NewLine + "Passage", "Metadata/Terrain/Labyrinth/Objects/SecretPassage");
            if (Settings.Specters)
            {
                if (Settings.TukohamasVanguard)
                    DrawTextLabelSpecter(Color.Yellow, "Tukohama's" + Environment.NewLine + "Vanguard", "Metadata/Monsters/KaomWarrior/KaomWarrior7", 120);
                if (Settings.WickerMan)
                    DrawTextLabelSpecter(Color.Yellow, "WickerMan", "Metadata/Monsters/WickerMan/WickerMan", 120);
                if (Settings.PockedLanternbearer)
                    DrawTextLabelSpecter(Color.Yellow, "Pocked " + Environment.NewLine + "Lanternbearer", "Metadata/Monsters/Miner/MinerLantern", 120);
                if (Settings.SolarGuard)
                    DrawTextLabelSpecter(Color.Yellow, "Solar Guard", "Metadata/Monsters/HolyFireElemental/HolyFireElementalSolarisBeam", 120);
            }

            if (Settings.Sentinels)
            {
                if (Settings.UnendingLethargy)
                    RenderTextLabel(Settings.UnendingLethargyColor.Value, "Slowing", "LabyrinthPopUpTotemSlow");
                if (Settings.EndlessDrought)
                    RenderTextLabel(Settings.EndlessDroughtColor.Value, $"Flask Charge{Environment.NewLine}Removal", "LabyrinthPopUpTotemFlaskChargeNova");
                if (Settings.EndlessHazard)
                    RenderTextLabel(Settings.EndlessHazardColor.Value, "Movement Damage", "LabyrinthPopUpTotemDamageTakenOnMovementSkillUse");
                if (Settings.EndlessPain)
                    RenderTextLabel(Settings.EndlessPainColor.Value, "Inc Damage Taken", "LabyrinthPopUpTotemIncreasedDamageTaken");
                if (Settings.EndlessSting)
                    RenderTextLabel(Settings.EndlessStingColor.Value, "Bleeding", "LabyrinthPopUpTotemBleedNova");
                if (Settings.UnendingFire)
                    RenderTextLabel(Settings.UnendingFireColor.Value, "Fire Nova", "LabyrinthPopUpTotemMonsterFire");
                if (Settings.UnendingFrost)
                    RenderTextLabel(Settings.UnendingFrostColor.Value, "Ice Nova", "LabyrinthPopUpTotemMonsterIceNova");
                if (Settings.UnendingStorm)
                    RenderTextLabel(Settings.UnendingStormColor.Value, "Shock Nova", "LabyrinthPopUpTotemMonsterLightning");
            }
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
                    Graphics.DrawBox(new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight), Color.Black);
                }
        }

        private void DrawTextLabelSpecter(ColorBGRA color, string text, string path, int zOffset = 0)
        {
            foreach (var entity in _entityCollection)
                if (entity.Path.ToLower().Contains(path.ToLower()) && !entity.IsAlive)
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
                    var background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                    Graphics.DrawBox(background, Color.Black);
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
                    lines.Add(line.Trim());
            }

            var conectedLines = "";
            foreach (var line in lines)
                conectedLines += line + "\n\r";
            return conectedLines;
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
            var screenCenter = new Vector2(mapRect.Width / 2, mapRect.Height / 2).Translate(0, -20) + new Vector2(mapRect.X, mapRect.Y) + new Vector2(mapWindow.LargeMapShiftX, mapWindow.LargeMapShiftY);
            var diag = (float) Math.Sqrt(camera.Width * camera.Width + camera.Height * camera.Height);
            var k = camera.Width < 1024f ? 1120f : 1024f;
            var scale = k / camera.Height * camera.Width * 3f / 4f / mapWindow.LargeMapZoom;
            var iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
            var point = screenCenter + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, scale, (iconZ - posZ) / (9f / mapWindow.LargeMapZoom));
            var texture = icon.TextureIcon;
            var size = icon.Size * 2; // icon.SizeOfLargeIcon.GetValueOrDefault(icon.Size * 2);
            texture.DrawPluginImage(Graphics, new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size));
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
            var mapCenter = new Vector2(mapRect.X + mapRect.Width / 2, mapRect.Y + mapRect.Height / 2).Translate(0, 0);
            var diag = Math.Sqrt(mapRect.Width * mapRect.Width + mapRect.Height * mapRect.Height) / 2.0;
            var iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
            var point = mapCenter + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, scale, (iconZ - posZ) / 20);
            var texture = icon.TextureIcon;
            var size = icon.Size;
            var rect = new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size);
            mapRect.Contains(ref rect, out var isContain);
            if (isContain) texture.DrawPluginImage(Graphics, rect);
        }

        private MapIcon GetMapIcon(EntityWrapper e)
        {
            if (Settings.Roombas && Settings.RoombasOnMap)
                if (e.Path.Contains("LabyrinthFlyingRoomba") || e.Path.Contains("LabyrinthRoomba"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "roomba.png", Settings.RoombasOnMapColor), () => Settings.RoombasOnMap, Settings.RoombasOnMapSize);
            if (Settings.Spinners && Settings.SpinnersOnMap)
                if (e.Path.Contains("LabyrinthSpinner"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "roomba.png", Settings.SpinnersOnMapColor), () => Settings.SpinnersOnMap, Settings.SpinnersOnMapSize);
            if (Settings.Saws && Settings.SawsOnMap)
                if (e.Path.Contains("LabyrinthSawblade"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "roomba.png", Settings.SawsOnMapColor), () => Settings.SawsOnMap, Settings.SawsOnMapSize);
            if (Settings.LabyrinthChest && !e.GetComponent<Chest>().IsOpened)
            {
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTreasureKey"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TreasureKeyChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthTrinketChest"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.TrinketChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthSpecificUnique"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.SpecificUniqueChestColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0Currency"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthReward0CurrencyQuality"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrency"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCurrencyQuality"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerUnique"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerUniqueColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerDivination"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerDivinationColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerSkillGems"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerLowGemColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerCorruptedVaal"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerCorVaalColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerJewelry"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerJewelleryColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardDangerGeneric"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardDangerGenericColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrency"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverCurrencyColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverCurrencyQuality"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverCurrencyQualityColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverJewelryUnique"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverJewelryUniqueColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverDivination"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverDivinationColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique1"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueOneColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique3"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueTwoColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverUnique2"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverUniqueThreeColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
                if (e.Path.Contains("Metadata/Chests/Labyrinth/LabyrinthRewardSilverSkillGems"))
                    return new MapIcon(e, new HudTexture(PoeHudImageLocation + "strongbox.png", Settings.RewardSilverSkillGemColor), () => Settings.LabyrinthChest, Settings.LabyrinthChestSize);
            }

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
            if (Settings.HiddenDoorway)
                if (e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Short") || e.Path.Contains("Metadata/Terrain/Labyrinth/Objects/HiddenDoor_Long"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.HiddenDoorwayColor), () => Settings.HiddenDoorway, Settings.HiddenDoorwayIcon);
            if (Settings.SecretPassage)
                if (e.Path.Equals("Metadata/Terrain/Labyrinth/Objects/SecretPassage"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "hidden_door.png", Settings.SecretPassageColor), () => Settings.SecretPassage, Settings.SecretPassageIcon);
            if (Settings.VaultPilesOnMap)
                if (e.Path.Contains("Metadata/Chests/VaultTreasurePile"))
                    if (!e.GetComponent<Chest>().IsOpened)
                        return new MapIcon(e, new HudTexture(CustomImagePath + "Coin.png"), () => true, Settings.VaultPilesIcon);
            if (Settings.AbyssCracks)
            {
                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssCrackSpawners/AbyssCrackSkeletonSpawner"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "abyss-crack.png", Settings.AbyssSmallNodeColor), () => true, Settings.AbyssSmallNodeSize);
                if (e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeLarge") || e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmall") || e.Path.Contains("Metadata/MiscellaneousObjects/Abyss/AbyssNodeSmallMortarSpawned") || e.Path.Contains("Metadata/Chests/Abyss/AbyssFinal"))
                    return new MapIcon(e, new HudTexture(CustomImagePath + "abyss-node-small.png", Settings.AbyssLargeNodeColor), () => true, Settings.AbyssLargeNodeSize);
            }

            if (Settings.AbysshoardChestToggleNode)
                if (e.Path.Contains("Metadata/Chests/AbyssChest") && !e.GetComponent<Chest>().IsOpened)
                    return new MapIcon(e, new HudTexture(CustomImagePath + "exclamationmark.png", Settings.AbysshoardChestColor), () => Settings.AbysshoardChestToggleNode, Settings.AbysshoardChestSize);
            return null;
        }

        private void RenderAtziriMirrorClone()
        {
            if (!Settings.Atziri) return;
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
                foreach (var entity in _entityCollection)
                    DrawToLargeMiniMap(entity);
            else if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
                foreach (var entity in _entityCollection)
                    DrawToSmallMiniMap(entity);
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
                    var background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
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
                    var background = new RectangleF(chestScreenCoords.X - maxWidth / 2 - 3, chestScreenCoords.Y - maxheight, maxWidth + 6, maxheight);
                    Graphics.DrawBox(background, Color.Black);
                }
        }

        private void RenderVaultPiles()
        {
            if (!Settings.VaultPilesOnFloor || !Settings.VaultPiles) return;
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
            foreach (var entity in _entityCollection)
                foreach (var wantedPath in information.Path)
                    if (entity.Path.Contains(wantedPath))
                    {
                        var camera = GameController.Game.IngameState.Camera;
                        var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, information.YOffset), entity);
                        var iconRect = new RectangleF(chestScreenCoords.X - information.ImageSize / 2, chestScreenCoords.Y - information.ImageSize / 2, information.ImageSize, information.ImageSize);
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
                        var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, information.YOffset), entity);
                        var iconRect = new RectangleF(chestScreenCoords.X - information.ImageSize / 2, chestScreenCoords.Y - information.ImageSize / 2, information.ImageSize, information.ImageSize);
                        if (chestScreenCoords == new Vector2()) continue;
                        // create rect at chest location to draw icon
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
                        var chestScreenCoords = camera.WorldToScreen(entity.Pos.Translate(0, 0, information.YOffset), entity);
                        if (chestScreenCoords == new Vector2()) continue;

                        // create rect at chest location to draw icon
                        var iconRect = new RectangleF(chestScreenCoords.X - information.ImageSize / 2, chestScreenCoords.Y - information.ImageSize / 2, information.ImageSize, information.ImageSize);
                        if (information.HasColor)
                            Graphics.DrawPluginImage(information.ImagePath, iconRect, information.Color);
                        else Graphics.DrawPluginImage(information.ImagePath, iconRect);
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