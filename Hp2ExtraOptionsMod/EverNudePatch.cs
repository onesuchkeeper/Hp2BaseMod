// Hp2RepeatThreesomeMod 2021, By onesuchkeeper

using HarmonyLib;
using System.Linq;

namespace Hp2ExtraOptionsMod
{
    /// <summary>
    /// Makes it so when toggled always changes to a nude outfit
    /// </summary>
    [HarmonyPatch(typeof(UiDoll), "ChangeOutfit")]
    class EverNudePatch
    {
        public static bool Prefix(UiDoll __instance, int outfitIndex)
        {
            if (Game.Persistence.playerData.unlockedCodes.Any(x => x.id == Constants.NudityToggleCodeID))
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
