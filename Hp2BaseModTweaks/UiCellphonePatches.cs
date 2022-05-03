// Hp2BaseModTweaks 2022, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;

namespace Hp2BaseModTweaks
{
    [HarmonyPatch(typeof(UiCellphone), MethodType.Constructor)]
    public static class UiCellphoneConstructorPatch
    {
        public static void Postfix(UiCellphone __instance)
        {
            __instance.OpenedEvent += CellphoneController.OnCellphoneOpened;
            __instance.CloseEvent += CellphoneController.OnCellphoneClose;
        }
    }

    // I can't get code in fast enough to use the LoadApp method directly, so I have to act on all its callers :(
    [HarmonyPatch(typeof(UiCellphone))]
    public static class UiCellphoneLoadAppPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("LoadClosedApp")]
        public static void LoadClosedApp(UiCellphone __instance)
        {
            CellphoneController.OnCellphoneAppLoaded(__instance);
        }

        [HarmonyPostfix]
        [HarmonyPatch("LoadOpenApp")]
        public static void LoadOpenApp(UiCellphone __instance)
        {
            CellphoneController.OnCellphoneAppLoaded(__instance);
        }

        [HarmonyPostfix]
        [HarmonyPatch("OnCellphoneButtonPressed")]
        public static void OnCellphoneButtonPressed(UiCellphone __instance, UiCellphoneButton cellphoneButton)
        {
            CellphoneController.OnCellphoneAppLoaded(__instance);
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppGirls))]
    public static class UiCellphoneAppGirlsPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("OnSlotPressed")]
        public static void OnSlotPressed(UiCellphoneAppGirls __instance, UiGirlSlot girlSlot)
        {
            CellphoneController.OnCellphoneAppLoaded(Game.Session.gameCanvas.cellphone);
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppWardrobe))]
    public static class UiCellphoneAppWardrobePatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("OnFileIconSlotSelected")]
        public static void OnFileIconSlotSelected(UiCellphoneAppWardrobe __instance, UiAppFileIconSlot fileIconSlot)
        {
            CellphoneController.OnFileIconSlotSelected();
        }
    }

    [HarmonyPatch(typeof(UiAppStyleSelectList))]
    public static class UiAppStyleSelectListPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("OnListItemSelected")]
        public static void OnListItemSelected(UiAppStyleSelectList __instance, UiAppSelectListItem listItem)
        {
            CellphoneController.OnListItemSelected(__instance, listItem);
        }
    }
}
