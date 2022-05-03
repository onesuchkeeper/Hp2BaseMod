// Hp2RepeatThreesomeMod 2021, By onesuchkeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;
using System.Collections.Generic;
using System.Linq;

namespace Hp2RepeatThreesomeMod
{
    public class Hp2RepeatThreesomeMod : IHp2BaseModStarter
    {
        public void Start(ModInterface modInterface)
        {
            modInterface.AddData(new CodeDataMod(Constants.LocalCodeId,
                                            "405DE09E6B1CBB24DF756FFE413CBE09",
                                            CodeType.TOGGLE,
                                            false,
                                            "Lovers' threesome location requirement on.",
                                            "Lovers' threesome location requirement off."));

            modInterface.AddData(new CodeDataMod(Constants.NudeCodeId,
                                           "40F45CA75FE6A9E007131D26FF9D36F6",
                                           CodeType.TOGGLE,
                                           false,
                                           "Nudity durring bonus rounds off.",
                                           "Nudity durring bonus rounds on."));

            // add nude outfits
            foreach (var girlId in DefaultData.DefaultGirlIds.Concat(modInterface.GirlMods.Select(x => x.Id)).Distinct())
            {
                modInterface.AddData(new GirlDataMod(girlId, InsertStyle.append) 
                {
                    Outfits = new List<GirlOutfitSubDefinition> 
                    { 
                        new GirlOutfitSubDefinition()
                        {
                            outfitName = Constants.NudeOutfitName,
                            partIndexOutfit = -1,
                            pairHairstyleIndex = -1,
                            tightlyPaired = false,
                            hideNipples = false,
                            editorExpanded = true
                        }
                    }
                });
            }

            new Harmony("Hp2BaseMod.Hp2RepeatThreesomeMod").PatchAll();
        }
    }
}
