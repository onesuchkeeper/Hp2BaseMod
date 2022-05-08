// Hp2BaseModTweaks 2022, By OneSuchKeeper

using HarmonyLib;

namespace Hp2BaseModTweaks
{
    [HarmonyPatch(typeof(UiCellphone), MethodType.Constructor)]
    public static class UiCellphoneConstructorPatch
    {
        public static void Prefix(UiCellphone __instance)
        {
            __instance.OpenEvent += CellphoneController.OnCellphoneOpen;
            __instance.OpenedEvent += CellphoneController.OnCellphoneOpened;
            __instance.CloseEvent += CellphoneController.OnCellphoneClose;

            CellphoneController.PostUiCellphoneConstructor();
        }
    }

    [HarmonyPatch(typeof(UiCellphone))]
    public static class UiCellphoneMethodsPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("OnCellphoneButtonPressed")]
        public static void OnCellphoneButtonPressed(UiCellphone __instance, UiCellphoneButton cellphoneButton)
        {
            CellphoneController.PostCellphoneButtonPressed();
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppGirls))]
    public static class UiCellphoneAppGirlsPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("OnSlotPressed")]
        public static void OnSlotPressed(UiCellphoneAppGirls __instance, UiGirlSlot girlSlot)
        {
            CellphoneController.UiCellphoneAppGirls_PostGirlSlotPressed();
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppWardrobe))]
    public static class UiCellphoneAppWardrobePatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnFileIconSlotSelected")]
        public static void PreFileIconSlotSelected(UiCellphoneAppWardrobe __instance, UiAppFileIconSlot fileIconSlot)
        {
            CellphoneController.UiCellphoneAppWardrobe_PreFileIconSlotSelected(__instance, fileIconSlot);
        }

        [HarmonyPostfix]
        [HarmonyPatch("OnFileIconSlotSelected")]
        public static void PostFileIconSlotSelected(UiCellphoneAppWardrobe __instance, UiAppFileIconSlot fileIconSlot)
        {
            CellphoneController.UiCellphoneAppWardrobe_PostFileIconSlotSelected(__instance, fileIconSlot);
        }

        [HarmonyPrefix]
        [HarmonyPatch("Refresh")]
        public static void Refresh(UiCellphoneAppWardrobe __instance)
        {
            CellphoneController.UiCellphoneAppWardrobe_PostWardrobeRefresh(__instance);
        }

        [HarmonyPrefix]
        [HarmonyPatch("OnListItemSelected")]
        public static void OnListItemSelected(UiCellphoneAppWardrobe __instance, UiAppStyleSelectList selectList, bool unlocked)
        {
            CellphoneController.UiCellphoneAppWardrobe_PreListItemSelected(selectList);
        }
    }
}
