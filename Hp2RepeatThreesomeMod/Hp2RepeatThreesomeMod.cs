// Hp2RepeatThreesomeMod 2021, By onesuchkeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.GameDataMods;

namespace Hp2RepeatThreesomeMod
{
    public class Hp2RepeatThreesomeMod : IHp2BaseModStarter
    {
        public void Start(GameDataModder gameDataMod)
        {
            var localCode = new CodeDataMod(Constants.LocalCodeId,
                                            "EA29B6A7A0AB1F669743E6C792F930F7",
                                            CodeType.TOGGLE,
                                            false,
                                            "Lovers' threesome location requirement on.",
                                            "Lovers' threesome location requirement off.");
            gameDataMod.AddData(localCode);

            var nudeCode = new CodeDataMod(Constants.NudeCodeId,
                                           "40F45CA75FE6A9E007131D26FF9D36F6",
                                           CodeType.TOGGLE,
                                           false,
                                           "Nudity durring bonus rounds off.",
                                           "Nudity durring bonus rounds on.");
            gameDataMod.AddData(nudeCode);

            new Harmony("Hp2BaseMod.Hp2RepeatThreesomeMod").PatchAll();
        }
    }
}
