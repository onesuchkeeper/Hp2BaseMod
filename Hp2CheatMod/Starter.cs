using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo.Interface;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Hp2CheatMod
{
    public class Starter : IHp2ModStarter
    {
        IEnumerable<IGameDataMod<AbilityDefinition>> IProvideGameDataMods.AbilityDataMods => null;
        IEnumerable<IGameDataMod<AilmentDefinition>> IProvideGameDataMods.AilmentDataMods => null;
        IEnumerable<IGameDataMod<CodeDefinition>> IProvideGameDataMods.CodeDataMods => null;
        IEnumerable<IGameDataMod<CutsceneDefinition>> IProvideGameDataMods.CutsceneDataMods => null;
        IEnumerable<IGameDataMod<DialogTriggerDefinition>> IProvideGameDataMods.DialogTriggerDataMods => null;
        IEnumerable<IGameDataMod<DlcDefinition>> IProvideGameDataMods.DlcDataMods => null;
        IEnumerable<IGameDataMod<EnergyDefinition>> IProvideGameDataMods.EnergyDataMods => null;
        IEnumerable<IGirlDataMod> IProvideGameDataMods.GirlDataMods => null;
        IEnumerable<IGirlPairDataMod> IProvideGameDataMods.GirlPairDataMods => null;
        IEnumerable<IGameDataMod<ItemDefinition>> IProvideGameDataMods.ItemDataMods => null;
        IEnumerable<ILocationDataMod> IProvideGameDataMods.LocationDataMods => null;
        IEnumerable<IGameDataMod<PhotoDefinition>> IProvideGameDataMods.PhotoDataMods => null;
        IEnumerable<IGameDataMod<QuestionDefinition>> IProvideGameDataMods.QuestionDataMods => null;
        IEnumerable<IGameDataMod<TokenDefinition>> IProvideGameDataMods.TokenDataMods => null;

        public void Start(int modId)
        {
            new Harmony("Hp2BaseMod.Hp2CheatMod").PatchAll();
        }
    }

    [HarmonyPatch(typeof(PuzzleStatus), "AddPuzzleReward")]
    public static class PuzzleSetGetMatchRewards_Patch
    {
        public static void Prefix(PuzzleStatus __instance)
        {
            //if (__instance.bonusRound) { return; }
            __instance.AddResourceValue(PuzzleResourceType.AFFECTION, 100000, false);
        }
    }

    [HarmonyPatch(typeof(UiCellphoneAppCode), "OnSubmitButtonPressed")]
    public static class Code_Patch
    {
        public static void Prefix(UiCellphoneAppCode __instance)
        {
            var input = (AccessTools.Field(typeof(UiCellphoneAppCode), "inputField").GetValue(__instance) as InputField).text.ToUpper().Trim();

            ModInterface.Log.LogLine($"Submitted code: {input}, hashed to {StringUtils.MD5(input)}");
        }
    }
}
