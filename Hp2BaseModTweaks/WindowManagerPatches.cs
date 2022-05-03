// Hp2BaseModTweaks 2022, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.Utility;

namespace Hp2BaseModTweaks
{
    /// <summary>
    /// The girls tab can be opened by pressing the bubbles "girls" button, by selecting the girls cellphone button, by selecting the hub "girls" button
    /// </summary>
    [HarmonyPatch(typeof(WindowManager), MethodType.Constructor)]
    public static class WindowManagerPatches
    {
        public static void Postfix(WindowManager __instance)
        {
            //__instance.WindowShowEvent += WindowShowEventHandler;
        }

        //public static void WindowShowEventHandler()
        //{
        //    var currentWindow = (UiWindow)AccessTools.Field(typeof(WindowManager), "_currentWindow").GetValue(Game.Manager.Windows);

        //    if (currentWindow != null)
        //    {
        //        ModInterface.Instance.LogLine($"Window shown event: Current Window: {currentWindow.name}");
        //        ModInterface.Instance.IncreaseLogIndent();
        //        UnityUtility.LogHierarchy(currentWindow.rectTransform);
        //        ModInterface.Instance.DecreaseLogIndent();
        //    }
        //}
    }
}
