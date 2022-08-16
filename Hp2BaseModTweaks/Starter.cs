// Hp2BaseModTweaks 2021, By OneSuchKeeper

using HarmonyLib;
using Hp2BaseMod;
using Hp2BaseMod.Extension.IEnumerableExtension;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseModTweaks.CellphoneApps;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Hp2BaseModTweaks
{
    public class Starter : IHp2ModStarter
    {
        IEnumerable<IGameDataMod<QuestionDefinition>> IProvideGameDataMods.QuestionDataMods => _questionDataMods;
        IGameDataMod<QuestionDefinition>[] _questionDataMods;

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
        IEnumerable<IGameDataMod<TokenDefinition>> IProvideGameDataMods.TokenDataMods => null;

        private static readonly string _creditsTagName = "Hp2BaseModTweaks_Credits";
        private static readonly string _logoTagName = "Hp2BaseModTweaks_Logo";

        public void Start(int modId)
        {
            foreach (var mod in ModInterface.Mods)
            {
                foreach (var tag in mod.Tags)
                {
                    // check for credits tags
                    if (tag.Name == _creditsTagName)
                    {
                        var config = JsonConvert.DeserializeObject<CreditsConfig>(tag.Value);
                        var isValid = true;

                        if (config == null)
                        {
                            ModInterface.Log.LogError($"Failed to deserialize {_creditsTagName} from {mod.SourceId}");
                            isValid = false;
                        }
                        else
                        {
                            if (!File.Exists(config.modImagePath))
                            {
                                ModInterface.Log.LogError($"Cannot find {config.modImagePath}");
                                isValid = false;
                            }

                            foreach (var entry in config.CreditsEntries.OrEmptyIfNull())
                            {
                                if (!File.Exists(entry.creditButtonImagePath))
                                {
                                    ModInterface.Log.LogError($"Cannot find {entry.creditButtonImagePath}");
                                    isValid = false;
                                }
                                else if (!File.Exists(entry.creditButtonImagePath))
                                {
                                    ModInterface.Log.LogError($"Cannot find {entry.creditButtonImageOverPath}");
                                    isValid = false;
                                }
                                else if (!File.Exists(entry.creditButtonImagePath))
                                {
                                    ModInterface.Log.LogError($"Cannot find {entry.creditButtonImagePath}");
                                    isValid = false;
                                }
                            }
                        }

                        if (isValid)
                        {
                            Common.Credits.Add(config);
                        } 
                    }
                    // and logo tags
                    else if (tag.Name == _logoTagName)
                    {
                        if (File.Exists(tag.Value))
                        {
                            ModInterface.Log.LogLine($"Logo: {tag.Value}");
                            Common.LogoPaths.Add(tag.Value);
                        }
                        else
                        {
                            ModInterface.Log.LogError($"Cannot find {tag.Value}");
                        }
                    }
                }
            }

            ModInterface.Ui.AddMainAppController(typeof(UiCellphoneAppFinder), (x) => new ExpandedUiCellphoneFinderApp(x as UiCellphoneAppFinder));
            ModInterface.Ui.AddMainAppController(typeof(UiCellphoneAppGirls), (x) => new ExpandedUiCellphoneGirlsApp(x as UiCellphoneAppGirls));
            ModInterface.Ui.AddMainAppController(typeof(UiCellphoneAppPairs), (x) => new ExpandedUiCellphonePairsApp(x as UiCellphoneAppPairs));
            ModInterface.Ui.AddMainAppController(typeof(UiCellphoneAppProfile), (x) => new ExpandedUiCellphoneProfileApp(x as UiCellphoneAppProfile));
            ModInterface.Ui.AddMainAppController(typeof(UiCellphoneAppWardrobe), (x) => new ExpandedUiCellphoneWardrobeApp(x as UiCellphoneAppWardrobe));
            ModInterface.Ui.AddTitleAppController(typeof(UiCellphoneAppCredits), (x) => new ExpandedUiCellphoneCreditsApp(x as UiCellphoneAppCredits));
            ModInterface.Ui.AddUiWindowController(typeof(UiWindowPhotos), (x) => new ExpandedUiWindowPhotos(x as UiWindowPhotos));

            new Harmony("Hp2BaseMod.Hp2BaseModTweaks").PatchAll();
        }
    }
}
