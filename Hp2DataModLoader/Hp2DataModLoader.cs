// Hp2DataModLoader 2021, by onesuchkeeper

using System;
using System.IO;
using Hp2BaseMod;
using Hp2BaseMod.GameDataMods;
using Newtonsoft.Json;

namespace Hp2SampleMod
{
    public class Hp2DataModLoader : IHp2BaseModStarter
    {
        public void Start(GameDataModder gameDataModder)
        {
            //need to be done like this b/c it doesn't have access to definition types :(
            //ability
            string[] abilityFiles = Directory.GetFiles("mods/Hp2DataModLoader/abilityMods");
            foreach (var file in abilityFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(AbilityDataMod)) as AbilityDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //ailment
            string[] ailmentFiles = Directory.GetFiles("mods/Hp2DataModLoader/ailmentMods");
            foreach (var file in ailmentFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(AilmentDataMod)) as AilmentDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //Code
            string[] codeFiles = Directory.GetFiles("mods/Hp2DataModLoader/codeMods");
            foreach (var file in codeFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(CodeDataMod)) as CodeDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //cutscene
            string[] cutsceneFiles = Directory.GetFiles("mods/Hp2DataModLoader/cutsceneMods");
            foreach (var file in cutsceneFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(CutsceneDataMod)) as CutsceneDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //dialogTrigger
            string[] dialogTriggerFiles = Directory.GetFiles("mods/Hp2DataModLoader/dialogTriggerMods");
            foreach (var file in dialogTriggerFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(DialogTriggerDataMod)) as DialogTriggerDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //Energy
            string[] energyFiles = Directory.GetFiles("mods/Hp2DataModLoader/energyMods");
            foreach (var file in energyFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(EnergyDataMod)) as EnergyDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //GirlData
            string[] girlFiles = Directory.GetFiles("mods/Hp2DataModLoader/girlMods");
            foreach (var file in girlFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(GirlDataMod)) as GirlDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //GirlPair
            string[] girlPairFiles = Directory.GetFiles("mods/Hp2DataModLoader/girlPairMods");
            foreach (var file in girlPairFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(GirlPairDataMod)) as GirlPairDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //ItemData
            string[] itemFiles = Directory.GetFiles("mods/Hp2DataModLoader/itemMods");
            foreach (var file in itemFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(ItemDataMod)) as ItemDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //LocationData
            string[] locationFiles = Directory.GetFiles("mods/Hp2DataModLoader/locationMods");
            foreach (var file in locationFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(LocationDataMod)) as LocationDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //PhotoData
            string[] photoFiles = Directory.GetFiles("mods/Hp2DataModLoader/photoMods");
            foreach (var file in photoFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(PhotoDataMod)) as PhotoDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //QuestionData
            string[] questionFiles = Directory.GetFiles("mods/Hp2DataModLoader/questionMods");
            foreach (var file in questionFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(QuestionDataMod)) as QuestionDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
            //TokenData
            string[] tokenFiles = Directory.GetFiles("mods/Hp2DataModLoader/tokenMods");
            foreach (var file in tokenFiles)
            {
                var configString = File.ReadAllText(file);
                var mod = JsonConvert.DeserializeObject(configString, typeof(TokenDataMod)) as TokenDataMod;
                if (mod != null)
                {
                    gameDataModder.AddData(mod);
                }
            }
        }

        //private void foo<T>(string path, GameDataModder gameDataModder)
        //    where T : class
        //{
        //    string[] tokenFiles = Directory.GetFiles(path);
        //    foreach (var file in tokenFiles)
        //    {
        //        var configString = File.ReadAllText(file);
        //        var mod = JsonConvert.DeserializeObject(configString, typeof(T)) as T;
        //        if (mod != null)
        //        {
        //            gameDataModder.AddData(mod);
        //        }
        //    }
        //}
    }
}
