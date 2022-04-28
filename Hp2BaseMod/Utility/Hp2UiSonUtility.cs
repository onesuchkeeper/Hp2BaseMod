// Hp2BaseMod 2022, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hp2BaseMod.Utility
{
    public static class Hp2UiSonUtility
    {
        private static readonly string _defaultDataPath = @"C:\Git\onesuchkeeper\Hp2BaseMod\Hp2BaseMod\DefaultData.cs";

        private static readonly List<string> _nullList = new List<string>() { "null" };

        private static string MakeEnumName(string enumName, string castAsTypeName)
            => (enumName.EndsWith("?") ? enumName.Substring(0, enumName.Length - 1) + "Nullable" : enumName) + "_As_" + castAsTypeName;

        private static readonly List<KeyValuePair<string, IEnumerable<string>>> _staticStringArrays = new List<KeyValuePair<string, IEnumerable<string>>>()
        {
            new KeyValuePair<string, IEnumerable<string>>("NullableBoolOptions",
                new List<string> { "null", true.ToString(), false.ToString() }),
            new KeyValuePair<string, IEnumerable<string>>("SpecialStepPrefabNames",
                new List<string> { "null", "CutsceneStepForceShowAilments", "CutsceneStepPostRewards", "CutsceneStepRoundClear" }),
            new KeyValuePair<string, IEnumerable<string>>("BannerTextPrefabNames",
                new List<string> { "null", "BannerTextSuccess", "BannerTextFailure", "BannerTextBonusRound","BannerTextRoundClear", "BannerTextCompatibility", "BannerTextAttraction" }),
            new KeyValuePair<string, IEnumerable<string>>("WindowPrefabNames",
                new List<string> { "null", "PhotosWindow", "ItemNotifierWindow" }),
            new KeyValuePair<string, IEnumerable<string>>("TextMaterialNames",
                new List<string> { "null", "stm_talent", "stm_flirtation", "stm_romance", "stm_sexuality", "stm_passion", "stm_broken", "stm_joy", "stm_sentiment", "stm_stamina" }),
            new KeyValuePair<string, IEnumerable<string>>("DefaultMeetingLocationNames",
                new List<string> { "null", "Tourist Plaza", "Boardwalk", "Surf Shack", "Courtyard", "Gift Shop", "Airport", "Hotel Lobby", "Marina" }),
            new KeyValuePair<string, IEnumerable<string>>("DefaultMeetingLocationIds",
                new List<string> { "null", "1", "2", "3", "4", "5", "6", "7", "8" }),
        };

        private static readonly List<KeyValuePair<string, string>> _enumStringArrays = new List<KeyValuePair<string, string>>()
        { 
            new KeyValuePair<string, string>(nameof(InsertStyle), nameof(String)),
            new KeyValuePair<string, string>(nameof(GirlStyleType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ClockDaytimeType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(PuzzleAffectionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemShoesType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemUniqueType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemFoodType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AilmentEnableType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(CodeType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(CutsceneCleanUpType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(EditorDialogTriggerTab) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(DialogTriggerForceType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(GirlExpressionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AbilityStepType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AbilityStepValueType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AbilityStepConditionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AbilityStepVisualEffectType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AbilityStepVisualEffectEnergyType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AbilityStepAilmentAlterType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AbilityStepAilmentTargetType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(PuzzleResourceType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(NumberCombineOperation) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(GirlValueType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AilmentStepType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AilmentFlagType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(NumberComparisonType) + '?', nameof(String))
        };

        private static readonly List<KeyValuePair<string, Func<GameData, IEnumerable<string>>>> _gameDataStringArrays = new List<KeyValuePair<string, Func<GameData, IEnumerable<string>>>>()
        { 
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultAbilityNames", (gameData) => _nullList.Concat(gameData.Abilities.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultAbilityIds", (gameData) => _nullList.Concat(gameData.Abilities.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultAilmentNames", (gameData) => _nullList.Concat(gameData.Ailments.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultAilmentIds", (gameData) => _nullList.Concat(gameData.Ailments.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultCodeNames", (gameData) => _nullList.Concat(gameData.Codes.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultCodeIds", (gameData) => _nullList.Concat(gameData.Codes.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultCutsceneNames", (gameData) => _nullList.Concat(gameData.Cutscenes.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultCutsceneIds", (gameData) => _nullList.Concat(gameData.Cutscenes.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultDialogTriggerNames", (gameData) => _nullList.Concat(gameData.DialogTriggers.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultDialogTriggerIds", (gameData) => _nullList.Concat(gameData.DialogTriggers.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultDlcNames", (gameData) => _nullList.Concat(gameData.Dlcs.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultDlcIds", (gameData) => _nullList.Concat(gameData.Dlcs.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultEnergyNames", (gameData) => _nullList.Concat(gameData.Energy.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultEnergyIds", (gameData) => _nullList.Concat(gameData.Energy.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultGirlNames", (gameData) => _nullList.Concat(gameData.Girls.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultGirlIds", (gameData) => _nullList.Concat(gameData.Girls.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultGirlPairNames", (gameData) => _nullList.Concat(gameData.GirlPairs.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultGirlPairIds", (gameData) => _nullList.Concat(gameData.GirlPairs.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultItemNames", (gameData) => _nullList.Concat(gameData.Items.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultItemIds", (gameData) => _nullList.Concat(gameData.Items.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultLocationNames", (gameData) => _nullList.Concat(gameData.Locations.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultLocationIds", (gameData) => _nullList.Concat(gameData.Locations.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultPhotoNames", (gameData) => _nullList.Concat(gameData.Photos.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultPhotoIds", (gameData) => _nullList.Concat(gameData.Photos.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultQuestionNames", (gameData) => _nullList.Concat(gameData.Questions.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultQuestionIds", (gameData) => _nullList.Concat(gameData.Questions.GetAll().Select(x => x.id.ToString()))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultTokenNames", (gameData) => _nullList.Concat(gameData.Tokens.GetAll().Select(x => x.name))),
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultTokenIds", (gameData) => _nullList.Concat(gameData.Tokens.GetAll().Select(x => x.id.ToString())))
        };

        private static string MakeStringArrayAttribute(string name, IEnumerable<string> entries)
        {
            if (name == null) { throw new ArgumentNullException(nameof(name)); }

            var result = $"[UiSonStringArray(\"{name}\", new string [] " + "{";

            if (entries != null)
            {
                foreach (var entry in entries)
                {
                    result += $" \"{entry}\",";
                }
            }

            result.TrimEnd(',');

            result += " })]";

            return result;
        }

        public static void MakeDefaultDataDotCs(GameData gameData)
        {
            try
            {
                var file = File.CreateText(_defaultDataPath);

                file.WriteLine("// Hp2BaseMod 2022, by OneSuchKeeper");
                file.WriteLine($"// This file was auto-generated on {DateTime.Now}. Changes made manually may be overridden.");
                file.WriteLine(String.Empty);
                file.WriteLine(@"using UiSon.Attribute;");
                file.WriteLine(@"using System;");
                file.WriteLine(String.Empty);
                file.WriteLine(@"namespace Hp2BaseMod{");
                    foreach (var entry in _staticStringArrays)
                    {
                        file.WriteLine("    " + MakeStringArrayAttribute(entry.Key, entry.Value));
                    }
                    file.WriteLine(String.Empty);
                    foreach (var entry in _enumStringArrays)
                    {
                        file.WriteLine($"    [UiSonStringArray(\"{MakeEnumName(entry.Key, entry.Value)}\", typeof({entry.Key}), typeof({entry.Value}))]");
                    }
                    file.WriteLine(String.Empty);
                    foreach (var entry in _gameDataStringArrays)
                    {
                        file.WriteLine("    " + MakeStringArrayAttribute(entry.Key, entry.Value.Invoke(gameData)));
                    }
                    file.WriteLine(@"   public static class DefaultData{");
                        foreach (var entry in _staticStringArrays)
                        {
                            file.WriteLine($"       public const string {entry.Key} = \"{entry.Key}\";");
                        }
                        file.WriteLine(String.Empty);
                        foreach (var entry in _enumStringArrays)
                        {
                            var name = MakeEnumName(entry.Key, entry.Value);
                            file.WriteLine($"       public const string {name} = \"{name}\";");
                        }
                        file.WriteLine(String.Empty);
                        foreach (var entry in _gameDataStringArrays)
                        {
                            file.WriteLine($"       public const string {entry.Key} = \"{entry.Key}\";");
                        }
                    file.WriteLine(@"   }");
                file.WriteLine(@"}");

                file.Flush();
            }
            catch (Exception e)
            {
                ModInterface.Instance.LogLine(e.Message);
            }
        }
    }
}
