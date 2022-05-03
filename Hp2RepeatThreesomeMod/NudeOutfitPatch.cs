// Hp2RepeatThreesomeMod 2021, By onesuchkeeper

using HarmonyLib;
using Hp2BaseMod;
using UnityEngine;

namespace Hp2RepeatThreesomeMod
{
    [HarmonyPatch(typeof(UiDoll), "ChangeOutfit")]
    class NudeOutfitPatch
    {
        public static bool Prefix(UiDoll __instance, int outfitIndex)
        {
            var girlDefinition = AccessTools.Field(typeof(UiDoll), "_girlDefinition").GetValue(__instance) as GirlDefinition;

            if (girlDefinition != null)
            {
                var clamped = Mathf.Clamp(outfitIndex, 0, girlDefinition.outfits.Count - 1);

                var outfit = girlDefinition.outfits[clamped];

                if (outfit.outfitName == Constants.NudeOutfitName)
                {
                    AccessTools.Field(typeof(UiDoll), "_currentOutfitIndex").SetValue(__instance, clamped);

                    __instance.partOutfit.Hide();
                    __instance.partNipples.Show();


                    return false;
                }
                else
                {
                    __instance.partOutfit.Show();
                }
            }

            return true;
        }
    }
}
