using System.Collections.Generic;
using System.IO;
using ImGuiNET;
using Newtonsoft.Json;
using Random_Features.Libs;
using Random_Features.PoeNinjaApi;

namespace Random_Features
{
    partial class RandomFeatures
    {
        public string JsonDownload { get; set; }
        public Fossils.RootObject Fossils { get; set; } = new Fossils.RootObject();


        public void FielTierMenu()
        {
            ImGui.Text("Low Tier - This starts from 0 up to Tier 2 Price");
            Settings.FossilTierMidPrice.Value = ImGuiExtension.IntSlider("Mid Tier - Price Tier 2 to start from", Settings.FossilTierMidPrice);
            Settings.FossilTierHighPrice.Value = ImGuiExtension.IntSlider("High Tier - Price Tier 3 to start from", Settings.FossilTierHighPrice);


            if (ImGui.Button("Set new fossil tiers"))
            {
                // download fossil data from current league via poe.ninja api
                using (var webClient = new System.Net.WebClient())
                {
                    JsonDownload = webClient.DownloadString($"https://poe.ninja/api/data/itemoverview?type=Fossil&league={GameController.Game.IngameState.ServerData.League}");
                    Fossils = JsonConvert.DeserializeObject<Fossils.RootObject>(JsonDownload);
                }

                // declare our data struct
                var tiers = new FossilTiers
                {
                    T1 = new List<string>(),
                    T2 = new List<string>(),
                    T3 = new List<string>()
                };

                // set thresholds for price range

                // place each fossil into each tier
                foreach (var objLine in Fossils.Lines)
                {

                    if (objLine.ChaosValue < Settings.FossilTierMidPrice)
                        tiers.T3.Add(objLine.Name.Replace(" Fossil", ""));
                    else if (objLine.ChaosValue >= Settings.FossilTierMidPrice && objLine.ChaosValue < Settings.FossilTierHighPrice)
                        tiers.T2.Add(objLine.Name.Replace(" Fossil", ""));
                    else if (objLine.ChaosValue >= Settings.FossilTierHighPrice)
                        tiers.T1.Add(objLine.Name.Replace(" Fossil", ""));
                }

                // Save data to file
                Json.SaveSettingFile($@"{DirectoryFullName}\Fossil_Tiers.json", tiers);

                // Reload data
                if (File.Exists($@"{DirectoryFullName}\Fossil_Tiers.json"))
                {
                    var jsonFIle = File.ReadAllText($@"{DirectoryFullName}\Fossil_Tiers.json");
                    FossilList = JsonConvert.DeserializeObject<FossilTiers>(jsonFIle, JsonSettings);
                }
                else
                {
                    LogError("Error loading Fossil_Tiers.json, Please re download from Random Features github repository", 10);
                }
            }
        }
    }



    public class Json
    {
        public static string ReadJson(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public static void SaveJson(string filePath, string data)
        {
            using (var file = File.CreateText(filePath))
            {
                file.WriteLine(data);
            }
        }

        public static TSettingType LoadSettingFile<TSettingType>(string fileName)
        {
            return !File.Exists(fileName) ? default(TSettingType) : JsonConvert.DeserializeObject<TSettingType>(File.ReadAllText(fileName));
        }

        public static void SaveSettingFile<TSettingType>(string fileName, TSettingType setting)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(setting, Formatting.Indented));
        }
    }
}
