// Hp2BaseMododLoader 2021, by OneSuchKeeper

using HarmonyLib;
using Newtonsoft.Json;
using System.IO;

namespace Hp2BaseMod.Save
{
    [HarmonyPatch(typeof(GamePersistence))]
    internal static class SaveLoadPatcher
    {
        [HarmonyPrefix]
        [HarmonyPatch("Save")]
        private static void SavePre(GamePersistence __instance, out ModSaveData __state)
        {
            __state = new ModSaveData();
            var saveData = (SaveData)AccessTools.Field(typeof(GamePersistence), "_saveData").GetValue(__instance);
            __state.Strip(saveData);
        }

        [HarmonyPostfix]
        [HarmonyPatch("Save")]
        private static void SavePost(GamePersistence __instance, ModSaveData __state)
        {
            AccessTools.Field(typeof(GamePersistence), "_saveData").SetValue(__instance, __state.ToSaveData());
            var modSaveDataFile = File.CreateText(@"mods\ModSaveData.json");
            var jsonStr = JsonConvert.SerializeObject(__state);
            modSaveDataFile.Write(jsonStr);
            modSaveDataFile.Flush();
            modSaveDataFile.Close();
        }

        [HarmonyPrefix]
        [HarmonyPatch("Load")]
        private static bool LoadPre(GamePersistence __instance)
        {
            var inited = (bool)AccessTools.Field(typeof(GamePersistence), "_inited").GetValue(__instance);
            var debugMode = (bool)AccessTools.Field(typeof(GamePersistence), "_debugMode").GetValue(__instance);
            if (!inited || debugMode) { return false; }

            var path = @"mods\ModSaveData.json";

            if (File.Exists(path))
            {
                var modSaveDataStr = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(modSaveDataStr))
                {
                    var modSaveData = JsonConvert.DeserializeObject(modSaveDataStr, typeof(ModSaveData)) as ModSaveData;
                    if (modSaveData != null)
                    {
                        AccessTools.Field(typeof(GamePersistence), "_saveData").SetValue(__instance, modSaveData.ToSaveData());
                        return false;
                    };

                };
            }

            return true;
        }
    }
}