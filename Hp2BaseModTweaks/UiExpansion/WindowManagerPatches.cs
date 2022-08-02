// Hp2BaseModTweaks 2022, By OneSuchKeeper

using HarmonyLib;

namespace Hp2BaseModTweaks
{
    [HarmonyPatch(typeof(WindowManager), MethodType.Constructor)]
    public static class WindowManagerPatches
    {
        public static void Postfix(WindowManager __instance)
        {
            __instance.WindowShowEvent += WindowController.OnWindowShow;
            __instance.WindowHideEvent += WindowController.OnWindowHide;

            WindowController.OnWindowManagerConstructor();
        }
    }
}
