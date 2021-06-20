// Hp2BaseMododLoader 2021, by OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.GameDataMods;
using Hp2BaseMod.GameDataMods.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hp2BaseModLoader
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
            if (configString == "") { return; }

            var gameDataModder = JsonConvert.DeserializeObject(configString, typeof(GameDataModder)) as GameDataModder;

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

            //grab dicts
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

            //make all default data mods to load their assets, not efficiant, I should make this better but I'm lazy
            var assetProvider = new AssetProvider(new Dictionary<string, UnityEngine.Object>());

            #region Defaultmods

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

            //grab defs to be modded, all need to be grabbed before any are setup
            var modAbilities = CreateEmpties<AbilityDefinition, AbilityDataMod>(abilityDataDict, gameDataModder);
            var modAilments = CreateEmpties<AilmentDefinition, AilmentDataMod>(ailmentDataDict, gameDataModder);
            var modCodes = CreateEmpties<CodeDefinition, CodeDataMod>(codeDataDict, gameDataModder);
            var modCutscenes = CreateEmpties<CutsceneDefinition, CutsceneDataMod>(cutsceneDataDict, gameDataModder);
            var modDialogTriggers = CreateEmpties<DialogTriggerDefinition, DialogTriggerDataMod>(dialogTriggerDataDict, gameDataModder);
            var modDlc = CreateEmpties<DlcDefinition, DlcDataMod>(dlcDataDict, gameDataModder);
            var modEnergys = CreateEmpties<EnergyDefinition, EnergyDataMod>(energyDataDict, gameDataModder);
            var modGirls = CreateEmpties<GirlDefinition, GirlDataMod>(girlDataDict, gameDataModder);
            var modGirlPairs = CreateEmpties<GirlPairDefinition, GirlPairDataMod>(girlPairDataDict, gameDataModder);
            var modItems = CreateEmpties<ItemDefinition, ItemDataMod>(itemDataDict, gameDataModder);
            var modLocations = CreateEmpties<LocationDefinition, LocationDataMod>(locationDataDict, gameDataModder);
            var modPhotos = CreateEmpties<PhotoDefinition, PhotoDataMod>(photoDataDict, gameDataModder);
            var modQuestions = CreateEmpties<QuestionDefinition, QuestionDataMod>(questionDataDict, gameDataModder);
            var modTokens = CreateEmpties<TokenDefinition, TokenDataMod>(tokenDataDict, gameDataModder);

            //setup defs
            SetupDef<AbilityDefinition, AbilityDataMod>(modAbilities, gameDataModder, __instance, assetProvider);
            SetupDef<AilmentDefinition, AilmentDataMod>(modAilments, gameDataModder, __instance, assetProvider);
            SetupDef<CodeDefinition, CodeDataMod>(modCodes, gameDataModder, __instance, assetProvider);
            SetupDef<CutsceneDefinition, CutsceneDataMod>(modCutscenes, gameDataModder, __instance, assetProvider);
            SetupDef<DialogTriggerDefinition, DialogTriggerDataMod>(modDialogTriggers, gameDataModder, __instance, assetProvider);
            SetupDef<DlcDefinition, DlcDataMod>(modDlc, gameDataModder, __instance, assetProvider);
            SetupDef<EnergyDefinition, EnergyDataMod>(modEnergys, gameDataModder, __instance, assetProvider);
            SetupDef<GirlDefinition, GirlDataMod>(modGirls, gameDataModder, __instance, assetProvider);
            SetupDef<GirlPairDefinition, GirlPairDataMod>(modGirlPairs, gameDataModder, __instance, assetProvider);
            SetupDef<ItemDefinition, ItemDataMod>(modItems, gameDataModder, __instance, assetProvider);
            SetupDef<LocationDefinition, LocationDataMod>(modLocations, gameDataModder, __instance, assetProvider);
            SetupDef<PhotoDefinition, PhotoDataMod>(modPhotos, gameDataModder, __instance, assetProvider);
            SetupDef<QuestionDefinition, QuestionDataMod>(modQuestions, gameDataModder, __instance, assetProvider);
            SetupDef<TokenDefinition, TokenDataMod>(modTokens, gameDataModder, __instance, assetProvider);
        }

        private static Dictionary<int, Def> GetDataDict<Data, Def>(GameData __instance, FieldInfo field)
            where Data: class
            where Def : Definition
        {
            return typeof(Data).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)[0]
                               .GetValue(field.GetValue(__instance) as Data) as Dictionary<int, Def>;
        }

        private static Dictionary<int, V> CreateEmpties<V,M>(Dictionary<int, V> dict, GameDataModder gameDataModder)
            where V : Definition, new()
            where M : IDataMod<V>
        {
            var defs = new Dictionary<int, V>();

            foreach (var mod in gameDataModder.GetMods<M>())
            {
                if (!dict.ContainsKey(mod.Id))
                {
                    dict.Add(mod.Id, new V());
                }
                defs.Add(mod.Id, dict[mod.Id]);
            }

            return defs;
        }

        private static void SetupDef<V, M>(Dictionary<int, V> defs, GameDataModder gameDataModder, GameData __instance, AssetProvider prefabProvider)
            where V : Definition, new()
            where M : IDataMod<V>
        {
            foreach(var mod in gameDataModder.GetMods<M>().Where(x => !x.IsAdditive))
            {
                mod.SetData(defs[mod.Id], __instance, prefabProvider);
            }

            foreach (var mod in gameDataModder.GetMods<M>().Where(x => x.IsAdditive))
            {
                mod.SetData(defs[mod.Id], __instance, prefabProvider);
            }
        }
    }
}