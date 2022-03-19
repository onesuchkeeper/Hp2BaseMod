// Hp2LadyJizzMod 2022, by OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.GameDataMods;
using Hp2BaseMod.Utility;

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

            modInterface.AddData(new CodeDataMod(Constants.NudityToggleCodeID,
                                                "B257CA0175FE0E66A9D3FF3F005D9EE4",
                                                CodeType.TOGGLE,
                                                false,
                                                "Constant Nudity off.",
                                                "Constant Nudity on."));

            // add toggle for slow drain on bonus round

            new Harmony("Hp2BaseMod.Hp2BaseModTweaks").PatchAll();
        }
    }
}
