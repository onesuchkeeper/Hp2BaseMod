// Hp2RepeatThreesomeMod 2021, By onesuchkeeper

using HarmonyLib;

namespace Hp2RepeatThreesomeMod
{
    [HarmonyPatch(typeof(UiDoll), "ChangeOutfit")]
    class NudeOutfitPatch
    {
        public static bool Prefix(UiDoll __instance, int outfitIndex)
        {
            if (outfitIndex == -69)
            {
                var girlOutfitSubDefinition = new GirlOutfitSubDefinition();
                girlOutfitSubDefinition.hideNipples = false;
                girlOutfitSubDefinition.partIndexOutfit = -1;
                girlOutfitSubDefinition.outfitName = "Nude";

                AccessTools.Method(typeof(UiDoll), "LoadPart")
                           .Invoke(__instance, new object[] { __instance.partOutfit, girlOutfitSubDefinition.partIndexOutfit, -1f });
                __instance.partNipples.Show();
                return false;
            }

            return true;
        }
    }
}
