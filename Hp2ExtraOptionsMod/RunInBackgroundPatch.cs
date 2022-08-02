using HarmonyLib;
using Hp2BaseMod;
using UnityEngine;

namespace Hp2ExtraOptionsMod
{
    [HarmonyPatch(typeof(UiCellphoneAppCode), "OnSubmitButtonPressed")]
    public static class RunInBackgroundPatch
    {
        public static void Postfix()
        {
            Application.runInBackground = ModInterface.IsCodeUnlocked(Constants.RunInBackgroundCodeId);
        }
    }

    [HarmonyPatch(typeof(UiTitleCanvas), "OnInitialAnimationComplete")]
    public static class RunInBackgroundLoadPatch
    {
        public static void Postfix()
        {
            Application.runInBackground = ModInterface.IsCodeUnlocked(Constants.RunInBackgroundCodeId);
        }
    }
}
