using HarmonyLib;

namespace Hp2BaseMod.EnumExpansion
{
    [HarmonyPatch(typeof(DialogTriggerDefinition))]
    internal class DialogTriggerDefinition_GetLineSetByGirlPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("GetLineSetByGirl")]
        public static bool GetLineSetByGirl(DialogTriggerDefinition __instance, GirlDefinition girlDef, ref DialogTriggerLineSet __result)
        {
            var dialogTriggerIndex = EnumLookups.GetGirlDialogTriggerIndex(girlDef.id);
            if (__instance.dialogLineSets[dialogTriggerIndex].dialogLines.Count > 0)
            {
                __result = __instance.dialogLineSets[dialogTriggerIndex];
            }
            else if (__instance.dialogLineSets[0].dialogLines.Count > 0)
            {
                __result = __instance.dialogLineSets[0];
            }
            else
            {
                __result = null;
            }
            return true;
        }
    }
}
