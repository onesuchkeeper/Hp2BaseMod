// Hp2BaseMod 2022, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hp2BaseMod.Utility
{
    internal static class Hp2UiSonUtility
    {
        private static readonly string _defaultDataPath = @"C:\Git\onesuchkeeper\Hp2BaseMod\Hp2BaseMod\DefaultData.cs";
        private static readonly string _addQoutesFormat = '"' + "{0}" + '"';

        private static readonly IEnumerable<string> _nullListStr = new List<string>() { null };
        private static readonly IEnumerable<object> _nullListIntNullable = new List<object>() { null };

        private static readonly List<KeyValuePair<string, IEnumerable<string>>> _staticStringArrays = new List<KeyValuePair<string, IEnumerable<string>>>()
        {
            new KeyValuePair<string, IEnumerable<string>>("NullableBoolOptions",
                new List<string> { "null", true.ToString(), false.ToString() }),
            new KeyValuePair<string, IEnumerable<string>>("SpecialStepNames",
                new List<string> { "null", "CutsceneStepForceShowAilments", "CutsceneStepPostRewards", "CutsceneStepRoundClear" }),
            new KeyValuePair<string, IEnumerable<string>>("BannerTextNames",
                new List<string> { "null", "BannerTextFailure","BannerTextBonusRound","BannerTextSuccess","BannerTextAttraction","BannerTextRoundClear","BannerTextCompatibility" }),
            new KeyValuePair<string, IEnumerable<string>>("UiWindowNames",
                new List<string> { "null", "PhotosWindow", "ItemNotifierWindow","KyuButtWindow" }),
            new KeyValuePair<string, IEnumerable<string>>("TextMaterialNames",
                new List<string> { "null", "stm_talent","stm_flirtation","stm_romance","stm_sexuality","stm_passion","stm_broken","stm_joy","stm_sentiment","stm_stamina" }),
            new KeyValuePair<string, IEnumerable<string>>("EmitterNames",
                new List<string> { "null", "EmitterSummon" }),
            new KeyValuePair<string, IEnumerable<string>>("UiDollSpecialEffectNames",
                new List<string> { "null", "FairyWingsKyu","GloWingsMoxie","GloWingsJewn" }),
            new KeyValuePair<string, IEnumerable<string>>("TokenResourceNames",
                new List<string> { "null", "Stamina", "Sentiment", "Move", "!?", "Passion"}),
            new KeyValuePair<string, IEnumerable<string>>("DefaultFlagValues",
                new List<string> { "null", "kyu_hole_selection", "wardrobe_girl_id", "GloWingsJewn", "notification_item_id", "tutorial_progress", "nymphojinn_failure", "pollys_junk", "alpha_mode", "store_filter_button", "photo_view_mode"}),

            new KeyValuePair<string, IEnumerable<string>>("DefaultMeetingLocationNames",
                new List<string> { "null", "Tourist Plaza", "Boardwalk", "Surf Shack", "Courtyard", "Gift Shop", "Airport", "Hotel Lobby", "Marina" }),

            new KeyValuePair<string, IEnumerable<string>>("EaseTypeNames",
                new List<string> { "null", "Unset","Linear","InSine","OutSine","InOutSine","InQuad","OutQuad","InOutQuad","InCubic","OutCubic","InOutCubic","InQuart",
                                    "OutQuart","InOutQuart","InQuint","OutQuint","InOutQuint","InExpo","OutExpo","InOutExpo","InCirc","OutCirc","InOutCirc","InElastic",
                                    "OutElastic","InOutElastic","InBack","OutBack","InOutBack","InBounce","OutBounce","InOutBounce","Flash","InFlash","OutFlash",
                                    "InOutFlash","INTERNAL_Zero","INTERNAL_Custom" }),
        };

        private static readonly List<KeyValuePair<string, IEnumerable<object>>> _staticIdArrays = new List<KeyValuePair<string, IEnumerable<object>>>()
        {
            new KeyValuePair<string, IEnumerable<object>>("DefaultMeetingLocationIds",
                new List<object> { null, 1, 2, 3, 4, 5, 6, 7, 8 }),

            new KeyValuePair<string, IEnumerable<object>>("EaseTypeIds",
                new List<object> { null, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 }),
        };

        private static readonly List<KeyValuePair<string, string>> _enumArrays = new List<KeyValuePair<string, string>>()
        { 
            new KeyValuePair<string, string>(nameof(InsertStyle), nameof(String)),
            new KeyValuePair<string, string>(nameof(GirlStyleType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ClockDaytimeType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(PuzzleAffectionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemShoesType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemUniqueType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemFoodType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemFoodType), nameof(String)),
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
            new KeyValuePair<string, string>(nameof(NumberComparisonType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemGiveConditionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(SettingDifficulty) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(ItemDateGiftType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(LocationType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AilmentTriggerType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(AilmentTriggerStepsProcessType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(CutsceneStepType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(CutsceneStepProceedType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(CutsceneStepDollTargetType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(DollOrientationType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(DollPositionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(CutsceneStepAnimationType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(CutsceneStepSubCutsceneType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(CutsceneStepNotificationType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(GirlPairRelationshipType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(GiftConditionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(GirlPartType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(LogicActionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(PuzzleGameState) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(LogicConditionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(MatchConditionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(MoveConditionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(MoveConditionTokenType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(TokenConditionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(TokenConditionTokenType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(GirlConditionType) + '?', nameof(String)),
            new KeyValuePair<string, string>(nameof(PuzzleStatusType) + '?', nameof(String)),
        };

        private static readonly List<KeyValuePair<string, Func<GameData, IEnumerable<string>>>> _gameDataNameArrays = new List<KeyValuePair<string, Func<GameData, IEnumerable<string>>>>()
        { 
            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultAbilityNames", (gameData) => _nullListStr.Concat(gameData.Abilities.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultAilmentNames", (gameData) => _nullListStr.Concat(gameData.Ailments.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultCodeNames", (gameData) => _nullListStr.Concat(gameData.Codes.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultCutsceneNames", (gameData) => _nullListStr.Concat(gameData.Cutscenes.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultDialogTriggerNames", (gameData) => _nullListStr.Concat(gameData.DialogTriggers.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultDlcNames", (gameData) => _nullListStr.Concat(gameData.Dlcs.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultEnergyNames", (gameData) => _nullListStr.Concat(gameData.Energy.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultGirlNames", (gameData) => _nullListStr.Concat(gameData.Girls.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultGirlPairNames", (gameData) => _nullListStr.Concat(gameData.GirlPairs.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultItemNames", (gameData) => _nullListStr.Concat(gameData.Items.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultLocationNames", (gameData) => _nullListStr.Concat(gameData.Locations.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultPhotoNames", (gameData) => _nullListStr.Concat(gameData.Photos.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultQuestionNames", (gameData) => _nullListStr.Concat(gameData.Questions.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultTokenNames", (gameData) => _nullListStr.Concat(gameData.Tokens.GetAll().Select(x => x.name)))
        };

        private static readonly List<KeyValuePair<string, Func<GameData, IEnumerable<object>>>> _gameDataIdArrays = new List<KeyValuePair<string, Func<GameData, IEnumerable<object>>>>()
        {
            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultAbilityIds", (gameData) => _nullListIntNullable.Concat(gameData.Abilities.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultAilmentIds", (gameData) => _nullListIntNullable.Concat(gameData.Ailments.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultCodeIds", (gameData) => _nullListIntNullable.Concat(gameData.Codes.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultCutsceneIds", (gameData) => _nullListIntNullable.Concat(gameData.Cutscenes.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultDialogTriggerIds", (gameData) => _nullListIntNullable.Concat(gameData.DialogTriggers.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultDlcIds", (gameData) => _nullListIntNullable.Concat(gameData.Dlcs.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultEnergyIds", (gameData) => _nullListIntNullable.Concat(gameData.Energy.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultGirlIds", (gameData) => _nullListIntNullable.Concat(gameData.Girls.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultGirlPairIds", (gameData) => _nullListIntNullable.Concat(gameData.GirlPairs.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultItemIds", (gameData) => _nullListIntNullable.Concat(gameData.Items.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultLocationIds", (gameData) => _nullListIntNullable.Concat(gameData.Locations.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultPhotoIds", (gameData) => _nullListIntNullable.Concat(gameData.Photos.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultQuestionIds", (gameData) => _nullListIntNullable.Concat(gameData.Questions.GetAll().Select(x => x?.id as object))),

            new KeyValuePair<string, Func<GameData, IEnumerable<object>>>("DefaultTokenIds", (gameData) => _nullListIntNullable.Concat(gameData.Tokens.GetAll().Select(x => x?.id as object)))
        };

        private static string MakeEnumName(string enumName, string castAsTypeName)
            => (enumName.EndsWith("?") ? enumName.Substring(0, enumName.Length - 1) + "Nullable" : enumName) + "_As_" + castAsTypeName;

        private static string MakeArrayAttribute(string name, IEnumerable<object> entries, string formatString = "{0}")
            => $"[UiSonArray(\"{name}\", new object [] {{{ToCsv(entries, formatString)}}})]";

        private static string ToCsv(IEnumerable<object> items, string formatString = "{0}") => string.Join(",", items.Select(x => string.Format(formatString,x ?? "null")));

        internal static void MakeDefaultDataDotCs(GameData gameData)
        {
            try
            {
                var file = File.CreateText(_defaultDataPath);

                file.WriteLine("// Hp2BaseMod 2022, by OneSuchKeeper");
                file.WriteLine($"// This file was auto-generated on {DateTime.Now}. Changes made manually may be overridden.");
                file.WriteLine(String.Empty);
                file.WriteLine(@"using System;");
                file.WriteLine(@"using System.Collections.Generic;");
                file.WriteLine(@"using UiSon.Attribute;");
                file.WriteLine(String.Empty);
                file.WriteLine(@"namespace Hp2BaseMod{");
                    foreach (var entry in _staticStringArrays)
                    {
                        file.WriteLine("    " + MakeArrayAttribute(entry.Key, entry.Value, _addQoutesFormat));
                    }
                    foreach (var entry in _staticIdArrays)
                    {
                        file.WriteLine("    " + MakeArrayAttribute(entry.Key, entry.Value));
                    }
                    file.WriteLine(String.Empty);
                    foreach (var entry in _enumArrays)
                    {
                        file.WriteLine($"    [UiSonArray(\"{MakeEnumName(entry.Key, entry.Value)}\", typeof({entry.Key}), typeof({entry.Value}))]");
                    }
                    file.WriteLine(String.Empty);
                    foreach (var entry in _gameDataNameArrays)
                    {
                        file.WriteLine("    " + MakeArrayAttribute(entry.Key, entry.Value.Invoke(gameData), _addQoutesFormat));
                    }
                    foreach (var entry in _gameDataIdArrays)
                    {
                        file.WriteLine("    " + MakeArrayAttribute(entry.Key, entry.Value.Invoke(gameData)));
                    }
                    file.WriteLine(@"   public class DefaultData{");
                        foreach (var entry in _staticStringArrays)
                        {
                            file.WriteLine($"       internal const string {entry.Key}_Name = \"{entry.Key}\";");
                            file.WriteLine($"       public static readonly IEnumerable<string> {entry.Key} = new string[] {{{ToCsv(entry.Value.Skip(1), _addQoutesFormat)}}};");
                        }
                        foreach (var entry in _staticIdArrays)
                        {
                            file.WriteLine($"       internal const string {entry.Key}_Name = \"{entry.Key}\";");
                            file.WriteLine($"       public static readonly IEnumerable<int> {entry.Key} = new int[] {{{ToCsv(entry.Value.Skip(1))}}};");
                        }
                        file.WriteLine(String.Empty);
                        foreach (var entry in _enumArrays)
                        {
                            var name = MakeEnumName(entry.Key, entry.Value);
                            file.WriteLine($"       internal const string {name} = \"{name}\";");
                        }
                        file.WriteLine(String.Empty);
                        foreach (var entry in _gameDataNameArrays)
                        {
                            file.WriteLine($"       internal const string {entry.Key}_Name = \"{entry.Key}\";");
                            file.WriteLine($"       public static readonly IEnumerable<string> {entry.Key} = new string[] {{{ToCsv(entry.Value.Invoke(gameData).Skip(1), _addQoutesFormat)}}};");
                        }
                        file.WriteLine(String.Empty);
                        foreach (var entry in _gameDataIdArrays)
                        {
                            file.WriteLine($"       internal const string {entry.Key}_Name = \"{entry.Key}\";");
                            file.WriteLine($"       public static readonly IEnumerable<int> {entry.Key} = new int[] {{{ToCsv(entry.Value.Invoke(gameData).Skip(1))}}};");
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
