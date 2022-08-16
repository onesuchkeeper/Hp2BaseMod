using HarmonyLib;

namespace Hp2BaseMod.Ui
{
    [HarmonyPatch(typeof(WindowManager), MethodType.Constructor)]
    public static class UiWindowPatchesCtor
    {
        public static void Postfix(WindowManager __instance)
        {
            ModInterface.Ui.SetWindowManager(__instance);
        }
    }

    [HarmonyPatch(typeof(UiWindowPhotos))]
    public static class UiWindowPhotosPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch("Refresh")]
        public static void PreRefresh(UiWindowPhotos __instance, bool initial)
        {
            ModInterface.Ui.WindowPreRefresh();
        }

        [HarmonyPostfix]
        [HarmonyPatch("Refresh")]
        public static void PostRefresh(UiWindowPhotos __instance, bool initial)
        {
            ModInterface.Ui.WindowPostRefresh();
        }
    }
}
