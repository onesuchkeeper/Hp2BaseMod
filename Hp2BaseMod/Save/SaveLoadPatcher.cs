// Hp2BaseMododLoader 2021, by OneSuchKeeper

using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Hp2BaseMod.Save
{
    internal static class SaveLoadPatcher
    {
        public static void Patch(Harmony harmony)
        {
            try
            {
                ModSave(harmony);
                ModLoad(harmony);
            }
            catch (Exception e)
            {
                Harmony.DEBUG = true;
                FileLog.Log("EXCEPTION SaveLoadPatcher: " + e.Message);
            }
        }

        private static void ModSave(Harmony harmony)
        {
            ModSaveData dummy;

            var mOrigional = AccessTools.Method(typeof(GamePersistence), "Save");
            var mSavePre = SymbolExtensions.GetMethodInfo(() => SavePre(null, out dummy));
            var mSavePost = SymbolExtensions.GetMethodInfo(() => SavePost(null, null));

            harmony.Patch(mOrigional, new HarmonyMethod(mSavePre), new HarmonyMethod(mSavePost));
        }

        private static void ModLoad(Harmony harmony)
        {
            var mOrigional = AccessTools.Method(typeof(GamePersistence), "Load");
            var mLoadPre = SymbolExtensions.GetMethodInfo(() => LoadPre(null));

            harmony.Patch(mOrigional, new HarmonyMethod(mLoadPre));
        }

        private static void SavePre(GamePersistence __instance, out ModSaveData __state)
        {
            __state = new ModSaveData();
            var saveData = (SaveData)AccessTools.Field(typeof(GamePersistence), "_saveData").GetValue(__instance);
            __state.Strip(saveData);
        }

        private static void SavePost(GamePersistence __instance, ModSaveData __state)
        {
            AccessTools.Field(typeof(GamePersistence), "_saveData").SetValue(__instance, __state.ToSaveData());
            var modSaveDataFile = File.CreateText(@"mods\ModSaveData.json");
            var jsonStr = JsonConvert.SerializeObject(__state);
            modSaveDataFile.Write(jsonStr);
            modSaveDataFile.Flush();
            modSaveDataFile.Close();
        }

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