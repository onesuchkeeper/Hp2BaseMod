using HarmonyLib;
using UnityEngine;

namespace Hp2BaseMod.EnumExpansion
{
    //[HarmonyPatch(typeof(UiDoll), "ChangeHairstyle")]
    class UiDoll_ChangeHairstylePatch
    {
        public static bool Prefix(UiDoll __instance, int hairstyleIndex)
        {
            var girlDefinition = AccessTools.Field(typeof(UiDoll), "_girlDefinition").GetValue(__instance) as GirlDefinition;

            if (girlDefinition != null)
            {
                var playerFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(girlDefinition);
                hairstyleIndex = ((hairstyleIndex == -1) ? playerFileGirl.hairstyleIndex : hairstyleIndex);
                hairstyleIndex = Mathf.Clamp(hairstyleIndex, 0, girlDefinition.hairstyles.Count - 1);

                var uiDollType = typeof(UiDoll);
                AccessTools.Field(uiDollType, "_currentHairstyleIndex").SetValue(__instance, hairstyleIndex);

                if (hairstyleIndex >= 0 && hairstyleIndex < girlDefinition.hairstyles.Count)
                {
                    var loadPart = AccessTools.Method(uiDollType, "LoadPart");

                    var girlHairstyleSubDefinition = girlDefinition.hairstyles[hairstyleIndex];
                    loadPart.Invoke(__instance, new object[] { __instance.partBackhair, girlHairstyleSubDefinition.partIndexBackhair, -1f });
                    loadPart.Invoke(__instance, new object[] { __instance.partFronthair, girlHairstyleSubDefinition.partIndexFronthair, -1f });
                    for (int i = 0; i < __instance.partSpecials.Length; i++)
                    {
                        __instance.partSpecials[i].StopAnimation();
                        __instance.partSpecials[i].dollPart.rectTransform.SetSiblingIndex(0);
                        __instance.partSpecials[i].dollPart.UnloadPart();
                    }
                    if (!girlHairstyleSubDefinition.hideSpecials)
                    {
                        var getDollPartByType = AccessTools.Method(uiDollType, "GetDollPartByType");

                        for (int j = 0; j < girlDefinition.specialParts.Count; j++)
                        {
                            loadPart.Invoke(__instance, new object[] { __instance.partSpecials[j].dollPart, girlDefinition.specialParts[j].partIndexSpecial, -1f });
                            if (girlDefinition.specialParts[j].sortingPartType != GirlPartType.SPECIAL1 && girlDefinition.specialParts[j].sortingPartType != GirlPartType.SPECIAL2 && girlDefinition.specialParts[j].sortingPartType != GirlPartType.SPECIAL3)
                            {
                                __instance.partSpecials[j].dollPart.rectTransform.SetSiblingIndex((getDollPartByType.Invoke(__instance, new object[] { girlDefinition.specialParts[j].sortingPartType }) as UiDollPart).rectTransform.GetSiblingIndex());
                            }
                            __instance.partSpecials[j].StartAnimation(girlDefinition.specialParts[j].animType);
                        }
                    }
                }

                return false;
            }

            return false;
        }
    }
}
