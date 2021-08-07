// Hp2BaseMododLoader 2021, by OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod.GameDataMods;
using Hp2BaseMod.GameDataMods.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hp2BaseMod.ModLoader
{
    internal static class GameDataPatcher
    {
        public static void Patch(Harmony harmony)
        {
            try
            {
                var mOrigional = AccessTools.Constructor(typeof(GameData));
                var mPostfix = SymbolExtensions.GetMethodInfo(() => AddRemoveData(null));

                harmony.Patch(mOrigional, null, new HarmonyMethod(mPostfix));
            }
            catch (Exception e)
            {
                Harmony.DEBUG = true;
                FileLog.Log("EXCEPTION GameDataPatcher: " + e.Message);
            }
        }
        private static void AddRemoveData(GameData __instance)
        {
            //read config
            var configString = System.IO.File.ReadAllText(@"mods\GameDataModifier.json");
            if (string.IsNullOrEmpty(configString)) { return; }

            var gameDataModder = JsonConvert.DeserializeObject(configString, typeof(GameDataModder)) as GameDataModder;
            if (gameDataModder == null) { return; }

            //grab dicts
            var gameDataPrivateFeilds = typeof(GameData).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            /*
                Index: 0, Name: _debugProfileData
                Index: 1, Name: _dlcData
                Index: 2, Name: _pauseData
                Index: 3, Name: _locationData
                Index: 4, Name: _girlData
                Index: 5, Name: _girlPairData
                Index: 6, Name: _photoData
                Index: 7, Name: _itemData
                Index: 8, Name: _tokenData
                Index: 9, Name: _ailmentData
                Index: 10, Name: _abilityData
                Index: 11, Name: _dialogTriggerData
                Index: 12, Name: _questionData
                Index: 13, Name: _cutsceneData
                Index: 14, Name: _energyData
                Index: 15, Name: _codeData
             */

            
            var abilityDataDict = GetDataDict<AbilityData, AbilityDefinition>(__instance, gameDataPrivateFeilds[10]);
            var ailmentDataDict = GetDataDict<AilmentData, AilmentDefinition>(__instance, gameDataPrivateFeilds[9]);
            var codeDataDict = GetDataDict<CodeData, CodeDefinition>(__instance, gameDataPrivateFeilds[15]);
            var cutsceneDataDict = GetDataDict<CutsceneData, CutsceneDefinition>(__instance, gameDataPrivateFeilds[13]);
            var dialogTriggerDataDict = GetDataDict<DialogTriggerData, DialogTriggerDefinition>(__instance, gameDataPrivateFeilds[11]);
            var dlcDataDict = GetDataDict<DlcData, DlcDefinition>(__instance, gameDataPrivateFeilds[1]);
            var energyDataDict = GetDataDict<EnergyData, EnergyDefinition>(__instance, gameDataPrivateFeilds[14]);
            var girlDataDict = GetDataDict<GirlData, GirlDefinition>(__instance, gameDataPrivateFeilds[4]);
            var girlPairDataDict = GetDataDict<GirlPairData, GirlPairDefinition>(__instance, gameDataPrivateFeilds[5]);
            var itemDataDict = GetDataDict<ItemData, ItemDefinition>(__instance, gameDataPrivateFeilds[7]);
            var locationDataDict = GetDataDict<LocationData, LocationDefinition>(__instance, gameDataPrivateFeilds[3]);
            var photoDataDict = GetDataDict<PhotoData, PhotoDefinition>(__instance, gameDataPrivateFeilds[6]);
            var questionDataDict = GetDataDict<QuestionData, QuestionDefinition>(__instance, gameDataPrivateFeilds[12]);
            var tokenDataDict = GetDataDict<TokenData, TokenDefinition>(__instance, gameDataPrivateFeilds[8]);

            //asset provider
            var assetProvider = new AssetProvider(new Dictionary<string, UnityEngine.Object>());

            assetProvider.AddAsset("None", null);

            #region Defaultmods
            //make all default data mods to load their assets, not efficiant, I should make this better but I'm lazy

            foreach (var ability in abilityDataDict)
            {
                var abilityMod = new AbilityDataMod(ability.Value, assetProvider);

                //var stream = File.CreateText($"mods\\Ability_{abilityMod.Id}.json");
                //var jsonStr = JsonConvert.SerializeObject(abilityMod, Formatting.Indented);
                //stream.Write(jsonStr);
                //stream.Flush();
            }

            //foreach (var ailment in ailmentDataDict)
            //{
            //    var ailmentMod = new AilmentDataMod(ailment.Value);

            //    //var stream = File.CreateText($"mods\\Ailment_{ailmentMod.Id}.json");
            //    //var jsonStr = JsonConvert.SerializeObject(ailmentMod, Formatting.Indented);
            //    //stream.Write(jsonStr);
            //    //stream.Flush();
            //}

            //foreach (var code in codeDataDict)
            //{
            //    var codeMod = new CodeDataMod(code.Value);

            //    //var stream = File.CreateText($"mods\\Code_{codeMod.Id}.json");
            //    //var jsonStr = JsonConvert.SerializeObject(codeMod, Formatting.Indented);
            //    //stream.Write(jsonStr);
            //    //stream.Flush();
            //}

            foreach (var cutscene in cutsceneDataDict)
            {
                var cutsceneMod = new CutsceneDataMod(cutscene.Value, assetProvider);

                //var stream = File.CreateText($"mods\\Cutscene_{cutsceneMod.Id}.json");
                //var jsonStr = JsonConvert.SerializeObject(cutsceneMod, Formatting.Indented);
                //stream.Write(jsonStr);
                //stream.Flush();
            }

            foreach (var dialogTrigger in dialogTriggerDataDict)
            {
                var dialogTriggerMod = new DialogTriggerDataMod(dialogTrigger.Value, assetProvider);

                //var stream = File.CreateText($"mods\\DialogTrigger_{dialogTriggerMod.Id}.json");
                //var jsonStr = JsonConvert.SerializeObject(dialogTriggerMod, Formatting.Indented);
                //stream.Write(jsonStr);
                //stream.Flush();
            }

            //foreach (var dlc in dlcDataDict)
            //{
            //    var dlcMod = new DlcDataMod(dlc.Value);

            //    //var stream = File.CreateText($"mods\\Dlc_{dlcMod.Id}.json");
            //    //var jsonStr = JsonConvert.SerializeObject(dlcMod, Formatting.Indented);
            //    //stream.Write(jsonStr);
            //    //stream.Flush();
            //}

            foreach (var energy in energyDataDict)
            {
                var energyMod = new EnergyDataMod(energy.Value, assetProvider);

                //var stream = File.CreateText($"mods\\Energy_{energyMod.Id}.json");
                //var jsonStr = JsonConvert.SerializeObject(energyMod, Formatting.Indented);
                //stream.Write(jsonStr);
                //stream.Flush();
            }

            foreach (var girl in girlDataDict)
            {
                var girlMod = new GirlDataMod(girl.Value, assetProvider);

                //var stream = File.CreateText($"mods\\Girl_{girlMod.Id}.json");
                //var jsonStr = JsonConvert.SerializeObject(girlMod, Formatting.Indented);
                //stream.Write(jsonStr);
                //stream.Flush();
            }

            //foreach (var girlPair in girlPairDataDict)
            //{
            //    var girlPairMod = new GirlPairDataMod(girlPair.Value);

            //    //var stream = File.CreateText($"mods\\GirlPair_{girlPairMod.Id}.json");
            //    //var jsonStr = JsonConvert.SerializeObject(girlPairMod, Formatting.Indented);
            //    //stream.Write(jsonStr);
            //    //stream.Flush();
            //}

            foreach (var item in itemDataDict)
            {
                var itemMod = new ItemDataMod(item.Value, assetProvider);

                //var stream = File.CreateText($"mods\\Item_{itemMod.Id}.json");
                //var jsonStr = JsonConvert.SerializeObject(itemMod, Formatting.Indented);
                //stream.Write(jsonStr);
                //stream.Flush();
            }

            foreach (var location in locationDataDict)
            {
                var locationMod = new LocationDataMod(location.Value, assetProvider);

                //var stream = File.CreateText($"mods\\Location_{locationMod.Id}.json");
                //var jsonStr = JsonConvert.SerializeObject(locationMod, Formatting.Indented);
                //stream.Write(jsonStr);
                //stream.Flush();
            }

            foreach (var photo in photoDataDict)
            {
                var photoMod = new PhotoDataMod(photo.Value, assetProvider);

                //var stream = File.CreateText($"mods\\Photo_{photoMod.Id}.json");
                //var jsonStr = JsonConvert.SerializeObject(photoMod, Formatting.Indented);
                //stream.Write(jsonStr);
                //stream.Flush();
            }

            //foreach (var question in questionDataDict)
            //{
            //    var questionMod = new QuestionDataMod(question.Value);

            //    //var stream = File.CreateText($"mods\\Question_{questionMod.Id}.json");
            //    //var jsonStr = JsonConvert.SerializeObject(questionMod, Formatting.Indented);
            //    //stream.Write(jsonStr);
            //    //stream.Flush();
            //}

            foreach (var token in tokenDataDict)
            {
                var tokenMod = new TokenDataMod(token.Value, assetProvider);

                //var stream = File.CreateText($"mods\\Token_{tokenMod.Id}.json");
                //var jsonStr = JsonConvert.SerializeObject(tokenMod, Formatting.Indented);
                //stream.Write(jsonStr);
                //stream.Flush();
            }

            #endregion Defaultmods

            //mods
            var abilityDataMods = gameDataModder.ReadMods<AbilityDataMod>();
            var ailmentDataMods = gameDataModder.ReadMods<AilmentDataMod>();
            var codeDataMods = gameDataModder.ReadMods<CodeDataMod>();
            var cutsceneDataMods = gameDataModder.ReadMods<CutsceneDataMod>();
            var dialogTriggerDataMods = gameDataModder.ReadMods<DialogTriggerDataMod>();
            var dlcDataMods = gameDataModder.ReadMods<DlcDataMod>();
            var energyDataMods = gameDataModder.ReadMods<EnergyDataMod>();
            var girlDataMods = gameDataModder.ReadMods<GirlDataMod>();
            var girlPairDataMods = gameDataModder.ReadMods<GirlPairDataMod>();
            var itemDataMods = gameDataModder.ReadMods<ItemDataMod>();
            var locationDataMods = gameDataModder.ReadMods<LocationDataMod>();
            var photoDataMods = gameDataModder.ReadMods<PhotoDataMod>();
            var questionDataMods = gameDataModder.ReadMods<QuestionDataMod>();
            var tokenDataMods = gameDataModder.ReadMods<TokenDataMod>();

            //grab defs to be modded, all need to be grabbed before any are setup
            var modAbilities = CreateEmpties(abilityDataDict, abilityDataMods);
            var modAilments = CreateEmpties(ailmentDataDict, ailmentDataMods);
            var modCodes = CreateEmpties(codeDataDict, codeDataMods);
            var modCutscenes = CreateEmpties(cutsceneDataDict, cutsceneDataMods);
            var modDialogTriggers = CreateEmpties(dialogTriggerDataDict, dialogTriggerDataMods);
            var modDlc = CreateEmpties(dlcDataDict, dlcDataMods);
            var modEnergys = CreateEmpties(energyDataDict, energyDataMods);
            var modGirls = CreateEmpties(girlDataDict, girlDataMods);
            var modGirlPairs = CreateEmpties(girlPairDataDict, girlPairDataMods);
            var modItems = CreateEmpties(itemDataDict, itemDataMods);
            var modLocations = CreateEmpties(locationDataDict, locationDataMods);
            var modPhotos = CreateEmpties(photoDataDict, photoDataMods);
            var modQuestions = CreateEmpties(questionDataDict, questionDataMods);
            var modTokens = CreateEmpties(tokenDataDict, tokenDataMods);

            //setup defs
            SetupDefs(modAbilities, abilityDataMods, __instance, assetProvider);
            SetupDefs(modAilments, ailmentDataMods, __instance, assetProvider);
            SetupDefs(modCodes, codeDataMods, __instance, assetProvider);
            SetupDefs(modCutscenes, cutsceneDataMods, __instance, assetProvider);
            SetupDefs(modDialogTriggers, dialogTriggerDataMods, __instance, assetProvider);
            SetupDefs(modDlc, dlcDataMods, __instance, assetProvider);
            SetupDefs(modEnergys, energyDataMods, __instance, assetProvider);
            SetupDefs(modGirls, girlDataMods, __instance, assetProvider);
            SetupDefs(modGirlPairs, girlPairDataMods, __instance, assetProvider);
            SetupDefs(modItems, itemDataMods, __instance, assetProvider);
            SetupDefs(modLocations, locationDataMods, __instance, assetProvider);
            SetupDefs(modPhotos, photoDataMods, __instance, assetProvider);
            SetupDefs(modQuestions, questionDataMods, __instance, assetProvider);
            SetupDefs(modTokens, tokenDataMods, __instance, assetProvider);
        }

        private static Dictionary<int, D> GetDataDict<Data, D>(GameData __instance, FieldInfo field)
            where Data: class
            where D : Definition
        {
            return typeof(Data).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)[0]
                               .GetValue(field.GetValue(__instance) as Data) as Dictionary<int, D>;
        }

        private static Dictionary<int, D> CreateEmpties<D>(Dictionary<int, D> dict, IEnumerable<IDataMod<D>> mods)
            where D : Definition, new()
        {
            var defs = new Dictionary<int, D>();

            foreach (var mod in mods)
            {
                if (!dict.ContainsKey(mod.Id))
                {
                    dict.Add(mod.Id, new D());
                }
                if (!defs.ContainsKey(mod.Id))
                {
                    defs.Add(mod.Id, dict[mod.Id]);
                }
            }

            return defs;
        }

        private static void SetupDefs<D>(Dictionary<int, D> defs, IEnumerable<IDataMod<D>> mods, GameData __instance, AssetProvider prefabProvider)
            where D : Definition
        {
            foreach(var mod in mods.Where(x => !x.IsAdditive))
            {
                mod.SetData(defs[mod.Id], __instance, prefabProvider);
            }

            foreach (var mod in mods.Where(x => x.IsAdditive))
            {
                mod.SetData(defs[mod.Id], __instance, prefabProvider);
            }
        }
    }
}