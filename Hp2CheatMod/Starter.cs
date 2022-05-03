using System;
using HarmonyLib;
using Hp2BaseMod;
using UnityEngine.UI;

namespace Hp2NudeDurringSexMod
{
    public class Starter : IHp2BaseModStarter
    {
        public void Start(ModInterface gameDataMod)
        {
            new Harmony("Hp2BaseMod.Hp2CheatMod").PatchAll();
        }
    }

    [HarmonyPatch(typeof(PuzzleStatus), "AddPuzzleReward")]
    public static class PuzzleSetGetMatchRewards_Patch
    {
        public static void Prefix(PuzzleStatus __instance)
        {
            if (__instance.bonusRound) { return; }
            __instance.AddResourceValue(PuzzleResourceType.AFFECTION, 100000, false);
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppCode), "OnSubmitButtonPressed")]
    public static class Code_Patch
    {
        public static void Prefix(UiCellphoneAppCode __instance)
        {
            var input = (AccessTools.Field(typeof(UiCellphoneAppCode), "inputField").GetValue(__instance) as InputField).text.ToUpper().Trim();

            ModInterface.Instance.LogLine($"Submitted code: {input}, hashed to {StringUtils.MD5(input)}");
        }
    }
}