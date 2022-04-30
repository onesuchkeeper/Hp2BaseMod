// Hp2BaseMododLoader 2021, by OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hp2BaseMod.ModLoader
{
    /// <summary>
    /// Patches in game data
    /// </summary>
    internal static class GameDataPatcher
    {
        private static readonly string _defaultDataDir = @"mods\DefaultData";
        private static bool _isDevMode = false;

        public static void Patch(Harmony harmony, bool isDevMode)
        {
            try
            {
                _isDevMode = isDevMode;

                var mOrigional = AccessTools.Constructor(typeof(GameData));
                var mPostfix = SymbolExtensions.GetMethodInfo(() => AddRemoveData(null));

                harmony.Patch(mOrigional, null, new HarmonyMethod(mPostfix));
            }
            catch (Exception e)
            {
                ModInterface.Instance.LogLine("EXCEPTION GameDataPatcher: " + e.Message);
            }
        }

        private static void AddRemoveData(GameData __instance)
        {
            ModInterface.Instance.LogTitle("Modifying GameData");
            ModInterface.Instance.IncreaseLogIndent();

            Hp2UiSonUtility.MakeDefaultDataDotCs(__instance);
            return;

            //grab dicts
            var abilityDataDict = GetDataDict<AbilityDefinition>(__instance, typeof(AbilityData), "_abilityData");
            var ailmentDataDict = GetDataDict<AilmentDefinition>(__instance, typeof(AilmentData), "_ailmentData");
            var codeDataDict = GetDataDict<CodeDefinition>(__instance, typeof(CodeData), "_codeData");
            var cutsceneDataDict = GetDataDict<CutsceneDefinition>(__instance, typeof(CutsceneData), "_cutsceneData");
            var dialogTriggerDataDict = GetDataDict<DialogTriggerDefinition>(__instance, typeof(DialogTriggerData), "_dialogTriggerData");
            var dlcDataDict = GetDataDict<DlcDefinition>(__instance, typeof(DlcData), "_dlcData");
            var energyDataDict = GetDataDict<EnergyDefinition>(__instance, typeof(EnergyData), "_energyData");
            var girlDataDict = GetDataDict<GirlDefinition>(__instance, typeof(GirlData), "_girlData");
            var girlPairDataDict = GetDataDict<GirlPairDefinition>(__instance, typeof(GirlPairData), "_girlPairData");
            var itemDataDict = GetDataDict<ItemDefinition>(__instance, typeof(ItemData), "_itemData");
            var locationDataDict = GetDataDict<LocationDefinition>(__instance, typeof(LocationData), "_locationData");
            var photoDataDict = GetDataDict<PhotoDefinition>(__instance, typeof(PhotoData), "_photoData");
            var questionDataDict = GetDataDict<QuestionDefinition>(__instance, typeof(QuestionData), "_questionData");
            var tokenDataDict = GetDataDict<TokenDefinition>(__instance, typeof(TokenData), "_tokenData");

            // providers
            var assetProvider = new AssetProvider(new Dictionary<string, UnityEngine.Object>());
            assetProvider.AddAsset("None", null);
            var gameDataProvider = new GameDataProvider(__instance);

            if (_isDevMode)
            {
                ModInterface.Instance.LogLine("Generating Dev Files");
                ModInterface.Instance.IncreaseLogIndent();
                SaveDataMods(abilityDataDict.Select(x => new AbilityDataMod(x.Value, assetProvider)), nameof(AbilityDataMod));
                SaveDataMods(cutsceneDataDict.Select(x => new CutsceneDataMod(x.Value, assetProvider)), nameof(CutsceneDataMod));
                SaveDataMods(dialogTriggerDataDict.Select(x => new DialogTriggerDataMod(x.Value, assetProvider)), nameof(DialogTriggerDataMod));
                SaveDataMods(energyDataDict.Select(x => new EnergyDataMod(x.Value, assetProvider)), nameof(EnergyDataMod));
                SaveDataMods(girlDataDict.Select(x => new GirlDataMod(x.Value, assetProvider)), nameof(GirlDataMod));
                SaveDataMods(itemDataDict.Select(x => new ItemDataMod(x.Value, assetProvider)), nameof(ItemDataMod));
                SaveDataMods(locationDataDict.Select(x => new LocationDataMod(x.Value, assetProvider)), nameof(LocationDataMod));
                SaveDataMods(photoDataDict.Select(x => new PhotoDataMod(x.Value, assetProvider)), nameof(PhotoDataMod));
                SaveDataMods(tokenDataDict.Select(x => new TokenDataMod(x.Value, assetProvider)), nameof(TokenDataMod));
                SaveDataMods(questionDataDict.Select(x => new QuestionDataMod(x.Value)), nameof(QuestionDataMod));
                SaveDataMods(girlPairDataDict.Select(x => new GirlPairDataMod(x.Value)), nameof(GirlPairDataMod));
                SaveDataMods(ailmentDataDict.Select(x => new AilmentDataMod(x.Value)), nameof(AilmentDataMod));
                SaveDataMods(codeDataDict.Select(x => new CodeDataMod(x.Value)), nameof(CodeDataMod));
                SaveDataMods(dlcDataDict.Select(x => new DlcDataMod(x.Value)), nameof(DlcDataMod));
                ModInterface.Instance.LogLine("done");
                ModInterface.Instance.DecreaseLogIndent();

                ModInterface.Instance.LogLine("Generating Prefabs File");
                ModInterface.Instance.IncreaseLogIndent();
                assetProvider.SaveToFolder(Path.Combine(_defaultDataDir, "InternalData"));
                ModInterface.Instance.LogLine("Done");
                ModInterface.Instance.DecreaseLogIndent();
            }
            else
            {
                ModInterface.Instance.LogLine("Loading internal assets");
                ModInterface.Instance.IncreaseLogIndent();
                foreach (var entry in abilityDataDict) { assetProvider.Load(entry.Value); }
                foreach (var entry in cutsceneDataDict) { assetProvider.Load(entry.Value); }
                foreach (var entry in dialogTriggerDataDict) { assetProvider.Load(entry.Value); }
                foreach (var entry in energyDataDict) { assetProvider.Load(entry.Value); }
                foreach (var entry in girlDataDict) { assetProvider.Load(entry.Value); }
                foreach (var entry in itemDataDict) { assetProvider.Load(entry.Value); }
                foreach (var entry in locationDataDict) { assetProvider.Load(entry.Value); }
                foreach (var entry in photoDataDict) { assetProvider.Load(entry.Value); }
                foreach (var entry in tokenDataDict) { assetProvider.Load(entry.Value); }
                ModInterface.Instance.LogLine("done");
                ModInterface.Instance.DecreaseLogIndent();
            }



            //mods
            ModInterface.Instance.LogLine("reading data mods");
            ModInterface.Instance.IncreaseLogIndent();
            var abilityDataMods = ModInterface.Instance.ReadMods<AbilityDataMod>();
            var ailmentDataMods = ModInterface.Instance.ReadMods<AilmentDataMod>();
            var codeDataMods = ModInterface.Instance.ReadMods<CodeDataMod>();
            var cutsceneDataMods = ModInterface.Instance.ReadMods<CutsceneDataMod>();
            var dialogTriggerDataMods = ModInterface.Instance.ReadMods<DialogTriggerDataMod>();
            var dlcDataMods = ModInterface.Instance.ReadMods<DlcDataMod>();
            var energyDataMods = ModInterface.Instance.ReadMods<EnergyDataMod>();
            var girlDataMods = ModInterface.Instance.ReadMods<GirlDataMod>();
            var girlPairDataMods = ModInterface.Instance.ReadMods<GirlPairDataMod>();
            var itemDataMods = ModInterface.Instance.ReadMods<ItemDataMod>();
            var locationDataMods = ModInterface.Instance.ReadMods<LocationDataMod>();
            var photoDataMods = ModInterface.Instance.ReadMods<PhotoDataMod>();
            var questionDataMods = ModInterface.Instance.ReadMods<QuestionDataMod>();
            var tokenDataMods = ModInterface.Instance.ReadMods<TokenDataMod>();
            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();

            //grab defs to be modded, all need to be grabbed before any are setup
            ModInterface.Instance.LogLine("creating data for new ids");
            ModInterface.Instance.IncreaseLogIndent();
            var abilities = CreateEmpties(abilityDataDict, abilityDataMods);
            var ailments = CreateEmpties(ailmentDataDict, ailmentDataMods);
            var codes = CreateEmpties(codeDataDict, codeDataMods);
            var cutscenes = CreateEmpties(cutsceneDataDict, cutsceneDataMods);
            var dialogTriggers = CreateEmpties(dialogTriggerDataDict, dialogTriggerDataMods);
            var dlc = CreateEmpties(dlcDataDict, dlcDataMods);
            var energy = CreateEmpties(energyDataDict, energyDataMods);
            var girls = CreateEmpties(girlDataDict, girlDataMods);
            var girlPairs = CreateEmpties(girlPairDataDict, girlPairDataMods);
            var items = CreateEmpties(itemDataDict, itemDataMods);
            var locations = CreateEmpties(locationDataDict, locationDataMods);
            var photos = CreateEmpties(photoDataDict, photoDataMods);
            var questions = CreateEmpties(questionDataDict, questionDataMods);
            var tokens = CreateEmpties(tokenDataDict, tokenDataMods);
            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();

            //setup defs
            ModInterface.Instance.LogLine("applying mod data to game data");
            ModInterface.Instance.IncreaseLogIndent();
            SetData(abilities, abilityDataMods, gameDataProvider, assetProvider);
            SetData(ailments, ailmentDataMods, gameDataProvider, assetProvider);
            SetData(codes, codeDataMods, gameDataProvider, assetProvider);
            SetData(cutscenes, cutsceneDataMods, gameDataProvider, assetProvider);
            SetData(dialogTriggers, dialogTriggerDataMods, gameDataProvider, assetProvider);
            SetData(dlc, dlcDataMods, gameDataProvider, assetProvider);
            SetData(energy, energyDataMods, gameDataProvider, assetProvider);
            SetData(girls, girlDataMods, gameDataProvider, assetProvider);
            SetData(girlPairs, girlPairDataMods, gameDataProvider, assetProvider);
            SetData(items, itemDataMods, gameDataProvider, assetProvider);
            SetData(locations, locationDataMods, gameDataProvider, assetProvider);
            SetData(photos, photoDataMods, gameDataProvider, assetProvider);
            SetData(questions, questionDataMods, gameDataProvider, assetProvider);
            SetData(tokens, tokenDataMods, gameDataProvider, assetProvider);
            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();

            ModInterface.Instance.DecreaseLogIndent();
        }

        private static Dictionary<int, T> GetDataDict<T>(GameData gameData, Type dataType, string dataName) => AccessTools.DeclaredField(dataType, "_definitions")
                                                                                                                          .GetValue(AccessTools.DeclaredField(typeof(GameData), dataName)
                                                                                                                          .GetValue(gameData)) as Dictionary<int, T>;
        #region Dev

        private static void SaveDataMods(IEnumerable<DataMod> mods, string name)
        {
            ModInterface.Instance.LogLine($"Dev: saving default {name}");

            var folderPath = Path.Combine(_defaultDataDir, name);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            ModInterface.Instance.IncreaseLogIndent();
            foreach(var mod in mods)
            {
                var filePath = Path.Combine(folderPath, $"{mod.ModName}.json");

                ModInterface.Instance.LogLine($"Dev: Saving {mod.ModName} to {filePath}");

                File.WriteAllText(filePath, JsonConvert.SerializeObject(mod, Formatting.Indented));
            }
            ModInterface.Instance.DecreaseLogIndent();
        }

        #endregion Dev
        private static Dictionary<int, D> CreateEmpties<D>(Dictionary<int, D> dict, IEnumerable<DataMod> mods)
            where D : Definition, new()
        {
            foreach (var mod in mods)
            {
                if (!dict.ContainsKey(mod.Id))
                {
                    var newDef = new D();
                    newDef.id = mod.Id;
                    dict.Add(mod.Id, newDef);
                }
            }

            return dict;
        }

        private static void SetData<T>(Dictionary<int, T> data, IEnumerable<IGameDataMod<T>> mods, GameDataProvider gameDataProvider, AssetProvider prefabProvider)
        {
            foreach (var mod in mods.OrderBy(x => x.LoadPriority))
            {
                mod.SetData(data[mod.Id], gameDataProvider, prefabProvider, mod.InsertStyle) ;
            }
        }
    }
}
