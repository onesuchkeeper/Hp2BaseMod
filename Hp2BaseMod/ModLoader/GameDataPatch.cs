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
using UnityEngine;

namespace Hp2BaseMod.ModLoader
{
    /// <summary>
    /// Patches in game data
    /// </summary>
    [HarmonyPatch(typeof(GameData), MethodType.Constructor)]
    internal static class GameDataPatch
    {
        private static readonly string _defaultDataDir = @"mods\DefaultData";
        private static readonly bool _isDevMode = false;
        private static bool _hasRun = false;

        private static void Postfix(GameData __instance)
        {
            // if the gamedata is accessed from here this method will be called again, shouldn't happen if
            // it's coded right but just as a failsafe
            if (_hasRun)
            {
                ModInterface.Log.LogTitle("WHY ARE YOU RUNNING?");
                return;
            }
            _hasRun = true;

            //okay, here's the plan
            //1) get all of the default data
            //2) register all the default data
            //3) if in dev mode, make mods for and save daefault data
            //4) get all the mods and create empties and register
            //5) get sub mods create empties and register
            //6) apply mods
            try
            {
                ModInterface.Log.LogTitle("Modifying GameData");
                using (ModInterface.Log.MakeIndent())
                {
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

                    // register default sub data
                    ModInterface.Log.LogLine("registering default sub data");
                    using (ModInterface.Log.MakeIndent())
                    {
                        foreach (var dt in dialogTriggerDataDict.Values)
                        {
                            var id = new RelativeId(-1, dt.id);

                            // lines are looked up by trigger id and girl id.
                            var girlIndex = 0;
                            foreach (var lineSet in dt.dialogLineSets)
                            {
                                var lineIndex = 0;
                                foreach (var line in lineSet.dialogLines)
                                {
                                    ModInterface.Data.TryRegisterLine(id, new RelativeId(-1, girlIndex), lineIndex, new RelativeId(-1, lineIndex));

                                    lineIndex++;
                                }

                                girlIndex++;
                            }
                        }
                        foreach (var girl in girlDataDict.Values)
                        {
                            var id = new RelativeId(-1, girl.id);
                            ModInterface.Data.TryRegisterGirlDialogTrigger(id, girl.id);

                            for (var i = 0; i < girl.parts.Count; i++)
                            {
                                ModInterface.Data.TryRegisterPart(id, i, new RelativeId(-1, i));
                            }

                            for (var i = 0; i < girl.hairstyles.Count; i++)
                            {
                                ModInterface.Data.TryRegisterHairstyle(id, i, new RelativeId(-1, i));
                            }

                            for (var i = 0; i < girl.outfits.Count; i++)
                            {
                                ModInterface.Data.TryRegisterOutfit(id, i, new RelativeId(-1, i));
                            }
                        }
                        foreach (var def in girlPairDataDict.Values)
                        {
                            var id = new RelativeId(-1, def.id);
                            ModInterface.Data.RegisterPairStyle(id, new PairStyleInfo()
                            {
                                MeetingGirlOne = new GirlStyleInfo() { HairstyleId = new RelativeId(-1, (int)def.meetingStyleTypeOne), OutfitId = new RelativeId(-1, (int)def.meetingStyleTypeOne) },
                                MeetingGirlTwo = new GirlStyleInfo() { HairstyleId = new RelativeId(-1, (int)def.meetingStyleTypeTwo), OutfitId = new RelativeId(-1, (int)def.meetingStyleTypeTwo) },
                                SexGirlOne = new GirlStyleInfo() { HairstyleId = new RelativeId(-1, (int)def.sexStyleTypeOne), OutfitId = new RelativeId(-1, (int)def.sexStyleTypeOne) },
                                SexGirlTwo = new GirlStyleInfo() { HairstyleId = new RelativeId(-1, (int)def.sexStyleTypeTwo), OutfitId = new RelativeId(-1, (int)def.sexStyleTypeTwo) }
                            });
                        }
                        foreach (var def in locationDataDict.Values)
                        {
                            var id = new RelativeId(-1, def.id);

                            var girlStyles = new Dictionary<RelativeId, GirlStyleInfo>();

                            foreach (var girl in girlDataDict.Values)
                            {
                                var girlId = new RelativeId(-1, girl.id);
                                girlStyles.Add(girlId, new GirlStyleInfo()
                                {
                                    HairstyleId = new RelativeId(-1, (int)def.dateGirlStyleType),
                                    OutfitId = new RelativeId(-1, (int)def.dateGirlStyleType)
                                });
                            }
                            ModInterface.Data.RegisterLocationStyles(id, girlStyles);
                        }
                    }

                    // asset provider
                    var assetProvider = new AssetProvider(new Dictionary<string, UnityEngine.Object>());
                    assetProvider.AddAsset("None", null);
                    var emptyTexture = TextureUtility.Empty();
                    assetProvider.AddAsset("EmptySprite", Sprite.Create(emptyTexture, new Rect(0, 0, emptyTexture.width, emptyTexture.height), Vector2.zero));
                    var gameDataProvider = new GameDefinitionProvider(__instance);

                    ModInterface.Log.LogLine("loading internal assets");
                    ModInterface.Log.IncreaseIndent();
                    foreach (var entry in abilityDataDict) { assetProvider.Load(entry.Value); }
                    foreach (var entry in cutsceneDataDict) { assetProvider.Load(entry.Value); }
                    foreach (var entry in dialogTriggerDataDict) { assetProvider.Load(entry.Value); }
                    foreach (var entry in energyDataDict) { assetProvider.Load(entry.Value); }
                    foreach (var entry in girlDataDict) { assetProvider.Load(entry.Value); }
                    foreach (var entry in itemDataDict) { assetProvider.Load(entry.Value); }
                    foreach (var entry in locationDataDict) { assetProvider.Load(entry.Value); }
                    foreach (var entry in photoDataDict) { assetProvider.Load(entry.Value); }
                    foreach (var entry in tokenDataDict) { assetProvider.Load(entry.Value); }
                    ModInterface.Log.DecreaseIndent();

                    // dev output default mods
                    if (_isDevMode)
                    {
                        try
                        {
                            ModInterface.Log.LogLine("Generating DefaultData.cs");
                            ModInterface.Log.IncreaseIndent();
                            Hp2UiSonUtility.MakeDefaultDataDotCs(__instance);
                            ModInterface.Log.DecreaseIndent();

                            ModInterface.Log.LogLine("Generating Dev Files");
                            ModInterface.Log.IncreaseIndent();
                            SaveDataMods(abilityDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new AbilityDataMod(x.Value, assetProvider))), nameof(AbilityDataMod));
                            SaveDataMods(ailmentDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new AilmentDataMod(x.Value))), nameof(AilmentDataMod));
                            SaveDataMods(codeDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new CodeDataMod(x.Value))), nameof(CodeDataMod));
                            SaveDataMods(cutsceneDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new CutsceneDataMod(x.Value, assetProvider))), nameof(CutsceneDataMod));
                            SaveDataMods(dialogTriggerDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new DialogTriggerDataMod(x.Value, assetProvider))), nameof(DialogTriggerDataMod));
                            SaveDataMods(dlcDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new DlcDataMod(x.Value))), nameof(DlcDataMod));
                            SaveDataMods(energyDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new EnergyDataMod(x.Value, assetProvider))), nameof(EnergyDataMod));

                            var girlDts = dialogTriggerDataDict.Values.Where(x => IsGirlDialogTrigger(x));
                            SaveDataMods(girlDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new GirlDataMod(x.Value, assetProvider, girlDts))), nameof(GirlDataMod));

                            SaveDataMods(girlPairDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new GirlPairDataMod(x.Value))), nameof(GirlPairDataMod));
                            SaveDataMods(itemDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new ItemDataMod(x.Value, assetProvider))), nameof(ItemDataMod));
                            SaveDataMods(locationDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new LocationDataMod(x.Value, girlDataDict.Values, assetProvider))), nameof(LocationDataMod));
                            SaveDataMods(photoDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new PhotoDataMod(x.Value, assetProvider))), nameof(PhotoDataMod));
                            SaveDataMods(questionDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new QuestionDataMod(x.Value))), nameof(QuestionDataMod));
                            SaveDataMods(tokenDataDict.Select(x => new KeyValuePair<string, DataMod>(x.Value.name, new TokenDataMod(x.Value, assetProvider))), nameof(TokenDataMod));
                            ModInterface.Log.DecreaseIndent();

                            ModInterface.Log.LogLine("Generating Prefabs File");
                            ModInterface.Log.IncreaseIndent();
                            assetProvider.SaveToFolder(Path.Combine(_defaultDataDir, "InternalData"));
                            ModInterface.Log.DecreaseIndent();
                        }
                        catch (Exception e)
                        {
                            ModInterface.Log.LogLine($"Failed generating dev data: {e}");
                        }
                    }

                    // grab data mods
                    ModInterface.Log.LogLine("grabbing data mods from the mod interface");
                    ModInterface.Log.IncreaseIndent();

                    var abilityDataMods = ModInterface.Mods.SelectMany(x => x.AbilityDataMods);
                    var ailmentDataMods = ModInterface.Mods.SelectMany(x => x.AilmentDataMods);
                    var codeDataMods = ModInterface.Mods.SelectMany(x => x.CodeDataMods);
                    var cutsceneDataMods = ModInterface.Mods.SelectMany(x => x.CutsceneDataMods);
                    var dialogTriggerDataMods = ModInterface.Mods.SelectMany(x => x.DialogTriggerDataMods);
                    var dlcDataMods = ModInterface.Mods.SelectMany(x => x.DlcDataMods);
                    var energyDataMods = ModInterface.Mods.SelectMany(x => x.EnergyDataMods);
                    var girlDataMods = ModInterface.Mods.SelectMany(x => x.GirlDataMods);
                    var girlPairDataMods = ModInterface.Mods.SelectMany(x => x.GirlPairDataMods);
                    var itemDataMods = ModInterface.Mods.SelectMany(x => x.ItemDataMods);
                    var locationDataMods = ModInterface.Mods.SelectMany(x => x.LocationDataMods);
                    var photoDataMods = ModInterface.Mods.SelectMany(x => x.PhotoDataMods);
                    var questionDataMods = ModInterface.Mods.SelectMany(x => x.QuestionDataMods);
                    var tokenDataMods = ModInterface.Mods.SelectMany(x => x.TokenDataMods);

                    var partModsByIdByGirl = new Dictionary<RelativeId, Dictionary<RelativeId, List<IGirlSubDataMod<GirlPartSubDefinition>>>>();
                    var outfitModsByIdByGirl = new Dictionary<RelativeId, Dictionary<RelativeId, List<IGirlSubDataMod<ExpandedOutfitDefinition>>>>();
                    var hairstyleModsByIdByGirl = new Dictionary<RelativeId, Dictionary<RelativeId, List<IGirlSubDataMod<ExpandedHairstyleDefinition>>>>();
                    var dialogLineModsByIdByDialogTriggerByGirlId = new Dictionary<RelativeId, Dictionary<RelativeId, Dictionary<RelativeId, List<IGirlSubDataMod<DialogLine>>>>>();

                    foreach (var girlMod in girlDataMods)
                    {
                        AddGirlSubMods(girlMod.Id, girlMod.GetPartMods(), partModsByIdByGirl);
                        AddGirlSubMods(girlMod.Id, girlMod.GetOutfits(), outfitModsByIdByGirl);
                        AddGirlSubMods(girlMod.Id, girlMod.GetHairstyles(), hairstyleModsByIdByGirl);

                        var linesByDialogTriggerId = girlMod.GetLinesByDialogTriggerId();

                        if (linesByDialogTriggerId != null)
                        {
                            if (!dialogLineModsByIdByDialogTriggerByGirlId.ContainsKey(girlMod.Id))
                            {
                                dialogLineModsByIdByDialogTriggerByGirlId.Add(girlMod.Id, new Dictionary<RelativeId, Dictionary<RelativeId, List<IGirlSubDataMod<DialogLine>>>>());
                            }

                            foreach (var dtIdToLines in linesByDialogTriggerId)
                            {
                                if (!dialogLineModsByIdByDialogTriggerByGirlId[girlMod.Id].ContainsKey(dtIdToLines.Item1))
                                {
                                    dialogLineModsByIdByDialogTriggerByGirlId[girlMod.Id].Add(dtIdToLines.Item1, new Dictionary<RelativeId, List<IGirlSubDataMod<DialogLine>>>());
                                }

                                foreach (var lineMod in dtIdToLines.Item2)
                                {
                                    if (!dialogLineModsByIdByDialogTriggerByGirlId[girlMod.Id][dtIdToLines.Item1].ContainsKey(lineMod.Id))
                                    {
                                        dialogLineModsByIdByDialogTriggerByGirlId[girlMod.Id][dtIdToLines.Item1].Add(lineMod.Id, new List<IGirlSubDataMod<DialogLine>>());
                                    }

                                    dialogLineModsByIdByDialogTriggerByGirlId[girlMod.Id][dtIdToLines.Item1][lineMod.Id].Add(lineMod);
                                }
                            }
                        }
                    }

                    ModInterface.Log.DecreaseIndent();

                    // create and register missing empty mods for used ids, all need to exist before any are setup because they reference one another
                    ModInterface.Log.LogLine("creating data for new ids");
                    ModInterface.Log.IncreaseIndent();
                    CreateEmpties(abilityDataDict, abilityDataMods, GameDataType.Ability);
                    CreateEmpties(ailmentDataDict, ailmentDataMods, GameDataType.Ailment);
                    CreateEmpties(codeDataDict, codeDataMods, GameDataType.Code);
                    CreateEmpties(cutsceneDataDict, cutsceneDataMods, GameDataType.Cutscene);
                    CreateEmpties(dialogTriggerDataDict, dialogTriggerDataMods, GameDataType.DialogTrigger);
                    CreateEmpties(dlcDataDict, dlcDataMods, GameDataType.Dlc);
                    CreateEmpties(energyDataDict, energyDataMods, GameDataType.Energy);
                    CreateEmpties(girlDataDict, girlDataMods, GameDataType.Girl);
                    CreateEmpties(girlPairDataDict, girlPairDataMods, GameDataType.GirlPair);
                    CreateEmpties(itemDataDict, itemDataMods, GameDataType.Item);
                    CreateEmpties(locationDataDict, locationDataMods, GameDataType.Location);
                    CreateEmpties(photoDataDict, photoDataMods, GameDataType.Photo);
                    CreateEmpties(questionDataDict, questionDataMods, GameDataType.Question);
                    CreateEmpties(tokenDataDict, tokenDataMods, GameDataType.Token);

                    // for these sub mods we can create empties and load them, because they don't reference one another
                    ModInterface.Log.LogLine("creating and registering data for new ids for sub mods");
                    using (ModInterface.Log.MakeIndent())
                    {
                        ModInterface.Log.LogLine("dialog trigger indexes");
                        int nextIndex = 16;//by default has one default (0), 12 for the normal girls, 1 for kyu, 2 for nymphojinn. 1+12+1+2=16
                        foreach (var girlId in girlDataMods.Select(x => x.Id).Distinct())
                        {
                            if (ModInterface.Data.TryRegisterGirlDialogTrigger(girlId, nextIndex))
                            {
                                foreach (var dialogTrigger in dialogTriggerDataDict.Values)
                                {
                                    dialogTrigger.dialogLineSets.Add(new DialogTriggerLineSet());
                                }
                                nextIndex++;
                            }
                        }

                        ModInterface.Log.LogLine("girl parts");
                        foreach (var girlIdToPartModsById in partModsByIdByGirl)
                        {
                            var girlDef = girlDataDict[ModInterface.Data.GetRuntimeDataId(GameDataType.Girl, girlIdToPartModsById.Key)];

                            nextIndex = girlDef.parts.Count;
                            foreach (var partId in girlIdToPartModsById.Value.Select(x => x.Key).Distinct())
                            {
                                if (ModInterface.Data.TryRegisterPart(girlIdToPartModsById.Key, nextIndex, partId))
                                {
                                    girlDef.parts.Add(new GirlPartSubDefinition());
                                    nextIndex++;
                                }
                            }
                        }

                        ModInterface.Log.LogLine("girl outfits");
                        foreach (var girlIdToOutfitModsById in outfitModsByIdByGirl)
                        {
                            var girlDef = girlDataDict[ModInterface.Data.GetRuntimeDataId(GameDataType.Girl, girlIdToOutfitModsById.Key)];

                            nextIndex = girlDef.outfits.Count;
                            foreach (var outfitId in girlIdToOutfitModsById.Value.Select(x => x.Key).Distinct())
                            {
                                if (ModInterface.Data.TryRegisterOutfit(girlIdToOutfitModsById.Key, nextIndex, outfitId))
                                {
                                    girlDef.outfits.Add(new ExpandedOutfitDefinition());
                                    nextIndex++;
                                }
                            }
                        }

                        ModInterface.Log.LogLine("girl hairstyles");
                        foreach (var girlIdToHairstyleModsById in hairstyleModsByIdByGirl)
                        {
                            var girlDef = girlDataDict[ModInterface.Data.GetRuntimeDataId(GameDataType.Girl, girlIdToHairstyleModsById.Key)];

                            nextIndex = girlDef.hairstyles.Count;
                            foreach (var hairstyleId in girlIdToHairstyleModsById.Value.Select(x => x.Key).Distinct())
                            {
                                if (ModInterface.Data.TryRegisterHairstyle(girlIdToHairstyleModsById.Key, nextIndex, hairstyleId))
                                {
                                    girlDef.hairstyles.Add(new ExpandedHairstyleDefinition());
                                    nextIndex++;
                                }
                            }
                        }

                        ModInterface.Log.LogLine("girl lines");
                        foreach (var girlIdToDialogLineModsByIdByDialogTrigger in dialogLineModsByIdByDialogTriggerByGirlId)
                        {
                            var index = ModInterface.Data.GetGirlDialogTriggerIndex(girlIdToDialogLineModsByIdByDialogTrigger.Key);

                            foreach (var dialogTriggerIdToDialogLineModsById in girlIdToDialogLineModsByIdByDialogTrigger.Value)
                            {
                                var dialogTriggerSet = dialogTriggerDataDict[ModInterface.Data.GetRuntimeDataId(GameDataType.DialogTrigger, dialogTriggerIdToDialogLineModsById.Key)].dialogLineSets[index];

                                nextIndex = dialogTriggerSet.dialogLines.Count;

                                foreach (var idToDialogLineMods in dialogTriggerIdToDialogLineModsById.Value)
                                {
                                    if (ModInterface.Data.TryRegisterLine(dialogTriggerIdToDialogLineModsById.Key, girlIdToDialogLineModsByIdByDialogTrigger.Key, nextIndex, idToDialogLineMods.Key))
                                    {
                                        dialogTriggerSet.dialogLines.Add(new DialogLine());
                                        nextIndex++;
                                    }
                                }
                            }
                        }

                        ModInterface.Log.LogLine("location slots");
                        foreach (var location in ModInterface.Data.GetIds(GameDataType.Location).Where(x => x.SourceId != -1))
                        {
                            // do good stuff here pls
                        }
                    }

                    ModInterface.Log.DecreaseIndent();

                    //setup defs
                    ModInterface.Log.LogLine("applying mod data to game data");
                    using (ModInterface.Log.MakeIndent())
                    {
                        ModInterface.Log.LogLine("abilities");
                        SetData(abilityDataDict, abilityDataMods, gameDataProvider, assetProvider, GameDataType.Ability);

                        ModInterface.Log.LogLine("ailments");
                        SetData(ailmentDataDict, ailmentDataMods, gameDataProvider, assetProvider, GameDataType.Ailment);

                        ModInterface.Log.LogLine("codes");
                        SetData(codeDataDict, codeDataMods, gameDataProvider, assetProvider, GameDataType.Code);

                        ModInterface.Log.LogLine("cutscenes");
                        SetData(cutsceneDataDict, cutsceneDataMods, gameDataProvider, assetProvider, GameDataType.Cutscene);

                        ModInterface.Log.LogLine("dialog triggers");
                        SetData(dialogTriggerDataDict, dialogTriggerDataMods, gameDataProvider, assetProvider, GameDataType.DialogTrigger);

                        ModInterface.Log.LogLine("dlcs");
                        SetData(dlcDataDict, dlcDataMods, gameDataProvider, assetProvider, GameDataType.Dlc);

                        ModInterface.Log.LogLine("energys");
                        SetData(energyDataDict, energyDataMods, gameDataProvider, assetProvider, GameDataType.Energy);

                        ModInterface.Log.LogLine("girls");
                        SetData(girlDataDict, girlDataMods, gameDataProvider, assetProvider, GameDataType.Girl);
                        using (ModInterface.Log.MakeIndent())
                        {
                            ModInterface.Log.LogLine("parts");
                            foreach (var girlIdToModsByPartId in partModsByIdByGirl)
                            {
                                var girl = girlDataDict[ModInterface.Data.GetRuntimeDataId(GameDataType.Girl, girlIdToModsByPartId.Key)];

                                foreach (var partIdToMods in girlIdToModsByPartId.Value)
                                {
                                    var part = girl.parts[ModInterface.Data.GetPartIndex(girlIdToModsByPartId.Key, partIdToMods.Key)];

                                    foreach (var mod in partIdToMods.Value.OrderBy(x => x.LoadPriority))
                                    {
                                        mod.SetData(ref part, gameDataProvider, assetProvider, InsertStyle.replace, girlIdToModsByPartId.Key);
                                    }
                                }
                            }

                            ModInterface.Log.LogLine("outfits");
                            foreach (var girlIdToModsByOutfitId in outfitModsByIdByGirl)
                            {
                                var girl = girlDataDict[ModInterface.Data.GetRuntimeDataId(GameDataType.Girl, girlIdToModsByOutfitId.Key)];

                                foreach (var outfitIdToMods in girlIdToModsByOutfitId.Value)
                                {
                                    var outfit = girl.outfits[ModInterface.Data.GetOutfitIndex(girlIdToModsByOutfitId.Key, outfitIdToMods.Key)];
                                    var expandedOutfit = outfit is ExpandedOutfitDefinition definition ? definition : new ExpandedOutfitDefinition(outfit);

                                    foreach (var mod in outfitIdToMods.Value.OrderBy(x => x.LoadPriority))
                                    {
                                        mod.SetData(ref expandedOutfit, gameDataProvider, assetProvider, InsertStyle.replace, girlIdToModsByOutfitId.Key);
                                    }
                                }
                            }

                            ModInterface.Log.LogLine("hairstyles");
                            foreach (var girlIdToModsByHairstyleId in hairstyleModsByIdByGirl)
                            {
                                var girl = girlDataDict[ModInterface.Data.GetRuntimeDataId(GameDataType.Girl, girlIdToModsByHairstyleId.Key)];

                                foreach (var hairstyleIdToMods in girlIdToModsByHairstyleId.Value)
                                {
                                    var hairstyle = girl.hairstyles[ModInterface.Data.GetHairstyleIndex(girlIdToModsByHairstyleId.Key, hairstyleIdToMods.Key)];
                                    var expandedHairstye = hairstyle is ExpandedHairstyleDefinition definition ? definition : new ExpandedHairstyleDefinition(hairstyle);

                                    foreach (var mod in hairstyleIdToMods.Value.OrderBy(x => x.LoadPriority))
                                    {
                                        mod.SetData(ref expandedHairstye, gameDataProvider, assetProvider, InsertStyle.replace, girlIdToModsByHairstyleId.Key);
                                    }
                                }
                            }

                            ModInterface.Log.LogLine("lines");
                            foreach (var girlIdToModsByIdByDialogTrigger in dialogLineModsByIdByDialogTriggerByGirlId)
                            {
                                var girl = girlDataDict[ModInterface.Data.GetRuntimeDataId(GameDataType.Girl, girlIdToModsByIdByDialogTrigger.Key)];

                                foreach (var dialogTriggerIdToModsById in girlIdToModsByIdByDialogTrigger.Value)
                                {
                                    var dialogTrigger = dialogTriggerDataDict[ModInterface.Data.GetRuntimeDataId(GameDataType.DialogTrigger, dialogTriggerIdToModsById.Key)];

                                    foreach (var lineIdToMods in dialogTriggerIdToModsById.Value)
                                    {
                                        var line = dialogTrigger.GetLineSetByGirl(girl).dialogLines[ModInterface.Data.GetLineIndex(dialogTriggerIdToModsById.Key, girlIdToModsByIdByDialogTrigger.Key, lineIdToMods.Key)];

                                        foreach (var mod in lineIdToMods.Value.OrderBy(x => x.LoadPriority))
                                        {
                                            mod.SetData(ref line, gameDataProvider, assetProvider, InsertStyle.replace, girlIdToModsByIdByDialogTrigger.Key);
                                        }
                                    }
                                }
                            }
                        }
                        
                        ModInterface.Log.LogLine("girl pairs");
                        SetData(girlPairDataDict, girlPairDataMods, gameDataProvider, assetProvider, GameDataType.GirlPair);
                        using (ModInterface.Log.MakeIndent())
                        {
                            ModInterface.Log.LogLine("styles");
                            foreach (var pairMod in girlPairDataMods)
                            {
                                ModInterface.Data.RegisterPairStyle(pairMod.Id, pairMod.GetStyles());
                            }
                        }

                        ModInterface.Log.LogLine("items");
                        SetData(itemDataDict, itemDataMods, gameDataProvider, assetProvider, GameDataType.Item);

                        ModInterface.Log.LogLine("locations");
                        SetData(locationDataDict, locationDataMods, gameDataProvider, assetProvider, GameDataType.Location);
                        using (ModInterface.Log.MakeIndent())
                        {
                            ModInterface.Log.LogLine("styles");
                            foreach (var locationMod in locationDataMods)
                            {
                                var styles = locationMod.GetStyles();

                                var stylesDict = new Dictionary<RelativeId, GirlStyleInfo>();

                                if (styles != null)
                                {
                                    foreach (var style in styles)
                                    {
                                        if (stylesDict.ContainsKey(style.Item1))
                                        {
                                            ModInterface.Log.LogError($"Repeat Style for {style.Item1}, ignoring");
                                        }
                                        else
                                        {
                                            stylesDict.Add(style.Item1, style.Item2);
                                        }
                                    }
                                }

                                ModInterface.Data.RegisterLocationStyles(locationMod.Id, locationMod.GetStyles().ToDictionary(x => x.Item1, x => x.Item2));
                            }
                        }

                        ModInterface.Log.LogLine("photos");
                        SetData(photoDataDict, photoDataMods, gameDataProvider, assetProvider, GameDataType.Photo);

                        ModInterface.Log.LogLine("questions");
                        SetData(questionDataDict, questionDataMods, gameDataProvider, assetProvider, GameDataType.Question);

                        ModInterface.Log.LogLine("tokens");
                        SetData(tokenDataDict, tokenDataMods, gameDataProvider, assetProvider, GameDataType.Token);
                    }
                }
            }
            catch (Exception e)
            {
                ModInterface.Log.LogLine($"{e}");
            }
        }

        #region Dev

        // there are inconsistencies in how dt's are handled, yay.
        // Some are indexed by girl id, others are made specifically for the hub, and there's no easy way to differentiate them
        // so here's just a manual list, maybe add this to default data later. If someone wants to mod these they can make a dll I'm not gonna support it via json
        // greeting = 49
        // valediction = 50
        // nap = 51
        // wakeup = 52
        // wing check = 53
        // pre nymph = 54
        // pos nymph = 55
        // nonstop = 56
        // wardrobe = 57
        // album = 58
        private static bool IsGirlDialogTrigger(DialogTriggerDefinition dt) => dt.id < 49 || dt.id > 58;

        private static void SaveDataMods(IEnumerable<KeyValuePair<string,DataMod>> mods, string name)
        {
            ModInterface.Log.LogLine($"Dev: saving default {name}");

            var folderPath = Path.Combine(_defaultDataDir, name);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            ModInterface.Log.IncreaseIndent();
            foreach(var mod in mods)
            {
                var filePath = Path.Combine(folderPath, $"{mod.Key}.json");

                ModInterface.Log.LogLine($"Dev: Saving {mod.Key} to {filePath}");

                File.WriteAllText(filePath, JsonConvert.SerializeObject(mod.Value, Formatting.Indented));
            }
            ModInterface.Log.DecreaseIndent();
        }

        #endregion Dev

        private static void AddGirlSubMods<T>(RelativeId girlId,
                                              IEnumerable<IGirlSubDataMod<T>> subMods,
                                              Dictionary<RelativeId, Dictionary<RelativeId, List<IGirlSubDataMod<T>>>> collection)
        {
            if (subMods != null)
            {
                if (!collection.ContainsKey(girlId))
                {
                    collection.Add(girlId, new Dictionary<RelativeId, List<IGirlSubDataMod<T>>>());
                }

                foreach (var subMod in subMods)
                {
                    if (!collection[girlId].ContainsKey(subMod.Id))
                    {
                        collection[girlId].Add(subMod.Id, new List<IGirlSubDataMod<T>>());
                    }

                    collection[girlId][subMod.Id].Add(subMod);
                }
            }
        }

        private static Dictionary<int, T> GetDataDict<T>(GameData gameData, Type dataType, string dataName)
            => AccessTools.DeclaredField(dataType, "_definitions")
                          .GetValue(AccessTools.DeclaredField(typeof(GameData), dataName)
                          .GetValue(gameData)) as Dictionary<int, T>;

        private static void CreateEmpties<D>(Dictionary<int, D> dict, IEnumerable<IGameDataMod<D>> mods, GameDataType type)
            where D : Definition, new()
        {
            foreach (var mod in mods)
            {
                var runtimeId = ModInterface.Data.GetRuntimeDataId(type, mod.Id);

                if (!dict.ContainsKey(runtimeId))
                {
                    var newDef = new D
                    {
                        id = runtimeId
                    };
                    dict.Add(runtimeId, newDef);
                }
            }
        }

        private static void SetData<T>(Dictionary<int, T> data,
                                       IEnumerable<IGameDataMod<T>> mods,
                                       GameDefinitionProvider gameDataProvider,
                                       AssetProvider prefabProvider,
                                       GameDataType type)
        {
            //first split into groups of the same id
            var modsByRuntimeId = new Dictionary<int, List<IGameDataMod<T>>>();
            foreach (var mod in mods)
            {
                var runtimeId = ModInterface.Data.GetRuntimeDataId(type, mod.Id);
                if (!modsByRuntimeId.ContainsKey(runtimeId))
                {
                    modsByRuntimeId.Add(runtimeId, new List<IGameDataMod<T>>() { mod });
                }
                else
                {
                    modsByRuntimeId[runtimeId].Add(mod);
                }
            }

            // then set the data
            foreach (var entry in modsByRuntimeId)
            {
                // order by the load priority
                foreach (var mod in entry.Value.OrderBy(x => x.LoadPriority))
                {
                    mod.SetData(data[entry.Key], gameDataProvider, prefabProvider);
                }
            }
        }
    }
}
