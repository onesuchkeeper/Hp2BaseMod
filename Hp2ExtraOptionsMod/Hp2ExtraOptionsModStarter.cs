// Hp2LadyJizzMod 2022, by OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;

namespace Hp2ExtraOptionsMod
{
    public class Hp2ExtraOptionsModStarter : IHp2BaseModStarter
    {
        public void Start(ModInterface modInterface)
        {
            modInterface.AddData(new CodeDataMod(Constants.FemaleJizzToggleCodeID,
                                                "509B82A2A4E16DF3774EA93B133F840F",
                                                CodeType.TOGGLE,
                                                false,
                                                "Female jizz off.",
                                                "Female jizz on."));

            // add toggle for slow drain on bonus round TODO

            new Harmony("Hp2BaseMod.Hp2BaseModTweaks").PatchAll();
        }
    }
}
