// Hp2BaseMod 2022, By OneSuchKeeper

using HarmonyLib;

namespace Hp2BaseMod
{
    /// <summary>
    /// source code uses a bunch of enums for the names exclusivly, but those can't be expanded so default to using the misc "category description"
    /// for everything instead
    /// </summary>
    [HarmonyPatch(typeof(ItemDefinition), "GetCategoryName")]
    public static class ItemDefinition_GetCategoryNamePatch
    {
        public static bool Prefix(ItemDefinition __instance, ref string __result)
        {
            // prioritize category description over all else
            if (!string.IsNullOrEmpty(__instance.categoryDescription))
            {
                __result = StringUtils.Titleize(__instance.itemType.ToString()) + " • " + __instance.categoryDescription;
            }

            return true;
        }
    }
}
