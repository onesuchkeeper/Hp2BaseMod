// Hp2LadyJizzMod 2021, by OneSuchKeeper

using System;
using System.Linq;
using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.GameDataMods;

namespace Hp2LadyJizzMod
{
    public class Hp2LadyJizzMod : IHp2BaseModStarter
    {
        public void Start(GameDataModder gameDataMod)
        {
            try
            {
                int dummy = 0;

                var harmony = new Harmony("Hp2BaseMod.Hp2LadyJizzMod");

                var femaleJizzToggleCode = new CodeDataMod(Constants.FemaleJizzToggleCodeID, 
                                                           "509B82A2A4E16DF3774EA93B133F840F",
                                                           CodeType.TOGGLE,
                                                           false,
                                                           "Female jizz off.",
                                                           "Female jizz on.");
                gameDataMod.AddData(femaleJizzToggleCode);

                var mInit = AccessTools.Method(typeof(UiWindowPhotos), "GetDefaultPhotoViewMode");
                var mInitPostfix = SymbolExtensions.GetMethodInfo(() => InitPostfix(ref dummy));

                harmony.Patch(mInit, null, new HarmonyMethod(mInitPostfix));

                var mRefresh = AccessTools.Method(typeof(UiWindowPhotos), "Refresh");
                var mfreshPrefix = SymbolExtensions.GetMethodInfo(() => RefreshPrefix(null));

                harmony.Patch(mRefresh, null, new HarmonyMethod(mfreshPrefix));
            }
            catch (Exception e)
            {
                Harmony.DEBUG = true;
                FileLog.Log("EXCEPTION Hp2LadyJizzMod: " + e.Message);
            }
        }

        public static void InitPostfix(ref int __result)
        {
            if (__result == 1
                && Game.Persistence.playerData.unlockedCodes.Any(x => x.id == Constants.FemaleJizzToggleCodeID)) 
            {
                __result = 2; 
            }
        }

        public static void RefreshPrefix(UiWindowPhotos __instance)
        {
            if (!Game.Persistence.playerData.uncensored 
                || !Game.Persistence.playerData.unlockedCodes.Any(x => x.id == Constants.FemaleJizzToggleCodeID))
            {
                return;
            }
            __instance.bpButtonJizzCanvasGroup.blocksRaycasts = true;
            __instance.bpButtonJizzCanvasGroup.alpha = 1f;
        }
    }
}
