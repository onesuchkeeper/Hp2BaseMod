// Hp2Sample 2022, By OneSuchKeeper

using HarmonyLib;
using System.Linq;

namespace Hp2ExtraOptionsMod
{
    /// <summary>
    /// If female jizz is on, toggles female UiWindow layout to male
    /// </summary>
    [HarmonyPatch(typeof(UiWindowPhotos), "GetDefaultPhotoViewMode")]
    public static class UiWindowPhotos_GetDefaultPhotoViewMode_Postfix
    {
        public static void Postfix(ref int __result)
        {
            if (__result == 1
                && Game.Persistence.playerData.unlockedCodes.Any(x => x.id == Constants.FemaleJizzToggleCodeID))
            {
                __result = 2;
            }
        }
    }

    /// <summary>
    /// If female jizz is on, fixes the ui on reload
    /// </summary>
    [HarmonyPatch(typeof(UiWindowPhotos), "Refresh")]
    public static class UiWindowPhotos_Refresh_Prefix
    {
        public static void Prefix(UiWindowPhotos __instance)
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
