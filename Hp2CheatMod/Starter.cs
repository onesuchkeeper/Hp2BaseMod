using System;
using HarmonyLib;
using Hp2BaseMod;

namespace Hp2NudeDurringSexMod
{
    public class Starter : IHp2BaseModStarter
    {
        public void Start(GameDataModder gameDataMod)
        {
            try
            {
                var harmony = new Harmony("Hp2BaseMod.Hp2CheatMod");

                var mOrigional = AccessTools.Method(typeof(PuzzleStatus), "AddPuzzleReward");
                var mPostfix = SymbolExtensions.GetMethodInfo(() => PuzzleSetGetMatchRewards_Patch.TransplierA(null));

                harmony.Patch(mOrigional, new HarmonyMethod(mPostfix));
            }
            catch (Exception e)
            {
                Harmony.DEBUG = true;
                FileLog.Log("EXCEPTION Hp2CheatMod: " + e.Message);
            }
        }
    }

    public static class PuzzleSetGetMatchRewards_Patch
    {
        public static void TransplierA(PuzzleStatus __instance)
        {
            if (__instance.bonusRound) { return; }
            __instance.AddResourceValue(PuzzleResourceType.AFFECTION, 100000, false);
        }
    }
}