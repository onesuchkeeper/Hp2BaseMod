// Hp2BaseMododLoader 2021, by OneSuchKeeper

using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Hp2BaseMod.Save
{
    [HarmonyPatch(typeof(GamePersistence))]
    internal static class SaveLoadPatcher
    {
        private const string _modSavePath = @"mods\ModSaveData.json";

        [HarmonyPrefix]
        [HarmonyPatch("Save")]
        private static bool SavePre(GamePersistence __instance, out ModSaveData __state)
        {
            ModInterface.NotifyPreSave();

            // don't do the normal save if the strip crashes, we don't want to courrupt the normal save
            try
            {
                __state = new ModSaveData();
                var saveData = (SaveData)AccessTools.Field(typeof(GamePersistence), "_saveData").GetValue(__instance);
                __state.Strip(saveData);
            }
            catch (Exception e)
            {
                ModInterface.Log.LogLine(e.ToString());
                __state = null;
                return false;
            }

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch("Save")]
        private static void SavePost(GamePersistence __instance, ModSaveData __state)
        {
            if (__state != null)
            {
                // save must come before set since we change some of the save date to match runtime
                File.WriteAllText(_modSavePath, JsonConvert.SerializeObject(__state, Formatting.Indented));

                var saveDataAccess = AccessTools.Field(typeof(GamePersistence), "_saveData");
                var saveData = saveDataAccess.GetValue(__instance) as SaveData;
                __state.SetData(saveData);
            }

            ModInterface.NotifyPostSave();
        }

        [HarmonyPrefix]
        [HarmonyPatch("Load")]
        private static void LoadPre(GamePersistence __instance)
        {
            // access the game data to init it since it's singlton. Dlc's is arbitrary
            var foo = Game.Data.Dlcs;

            ModInterface.NotifyPreLoad();
        }

        [HarmonyPostfix]
        [HarmonyPatch("Load")]
        private static void LoadPost(GamePersistence __instance)
        {
            if (!(bool)AccessTools.Field(typeof(GamePersistence), "_inited").GetValue(__instance)
                || (bool)AccessTools.Field(typeof(GamePersistence), "_debugMode").GetValue(__instance))
            {
                return;
            }

            if (File.Exists(_modSavePath))
            {
                var modSaveDataStr = File.ReadAllText(_modSavePath);
                if (!string.IsNullOrEmpty(modSaveDataStr))
                {
                    try
                    {
                        var modSaveData = JsonConvert.DeserializeObject(modSaveDataStr, typeof(ModSaveData)) as ModSaveData;
                        if (modSaveData != null)
                        {
                            var saveDataAccess = AccessTools.Field(typeof(GamePersistence), "_saveData");
                            var saveData = saveDataAccess.GetValue(__instance) as SaveData;
                            modSaveData.SetData(saveData);
                            saveDataAccess.SetValue(__instance, saveData);
                        }
                    }
                    catch (Exception e)
                    {
                        ModInterface.Log.LogError($"Exception thrown while loading mod save: {e}");
                    }
                }
                else
                {
                    ModInterface.Log.LogError("Unable to read modded save data");
                }
            }
            else
            {
                ModInterface.Log.LogLine("Unable to locate modded save data");
            }

            ModInterface.NotifyPostLoad();
        }
    }
}
