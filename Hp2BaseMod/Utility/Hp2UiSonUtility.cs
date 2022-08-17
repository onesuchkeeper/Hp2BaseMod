// Hp2BaseMod 2022, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo;
using Newtonsoft.Json;
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
        private static readonly char _questionMark = '?';

        private static readonly IEnumerable<string> _nullListStr = new List<string>() { "null" };
        private static readonly List<RelativeId?> _nullListIdNullable = new List<RelativeId?>() { null };

        private static readonly List<KeyValuePair<string, IEnumerable<string>>> _staticStringArrays = new List<KeyValuePair<string, IEnumerable<string>>>()
        {
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
                new List<string> { "null", "Affection", "Stamina", "Sentiment", "Move", "!?", "Passion"}),
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

        private static readonly List<KeyValuePair<string, IEnumerable<int?>>> _staticEnumerableArrays = new List<KeyValuePair<string, IEnumerable<int?>>>()
        {
            new KeyValuePair<string, IEnumerable<int?>>("EaseTypeIds", new List<int?>
            { null, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 })
        };

        private static readonly List<KeyValuePair<string, IEnumerable<RelativeId?>>> _staticIdArrays = new List<KeyValuePair<string, IEnumerable<RelativeId?>>>()
        {
            new KeyValuePair<string, IEnumerable<RelativeId?>>("DefaultMeetingLocationIds",
                new List<RelativeId?>
                {
                    null,
                    new RelativeId(-1,1),
                    new RelativeId(-1,2),
                    new RelativeId(-1,3),
                    new RelativeId(-1,4),
                    new RelativeId(-1,5),
                    new RelativeId(-1,6),
                    new RelativeId(-1,7),
                    new RelativeId(-1,8)
                })
        };

        private static readonly List<string> _enumArrays = new List<string>()
        {
            nameof(InsertStyle),
            nameof(ItemFoodType),
            nameof(GirlStyleType) + _questionMark,
            nameof(ClockDaytimeType) + _questionMark,
            nameof(PuzzleAffectionType) + _questionMark,
            nameof(ItemShoesType) + _questionMark,
            nameof(ItemUniqueType) + _questionMark,
            nameof(ItemFoodType) + _questionMark,
            nameof(AilmentEnableType) + _questionMark,
            nameof(CodeType) + _questionMark,
            nameof(CutsceneCleanUpType) + _questionMark,
            nameof(EditorDialogTriggerTab) + _questionMark,
            nameof(DialogTriggerForceType) + _questionMark,
            nameof(GirlExpressionType) + _questionMark,
            nameof(AbilityStepType) + _questionMark,
            nameof(AbilityStepValueType) + _questionMark,
            nameof(AbilityStepConditionType) + _questionMark,
            nameof(AbilityStepVisualEffectType) + _questionMark,
            nameof(AbilityStepVisualEffectEnergyType) + _questionMark,
            nameof(AbilityStepAilmentAlterType) + _questionMark,
            nameof(AbilityStepAilmentTargetType) + _questionMark,
            nameof(PuzzleResourceType) + _questionMark,
            nameof(NumberCombineOperation) + _questionMark,
            nameof(GirlValueType) + _questionMark,
            nameof(ItemType) + _questionMark,
            nameof(AilmentStepType) + _questionMark,
            nameof(AilmentFlagType) + _questionMark,
            nameof(NumberComparisonType) + _questionMark,
            nameof(ItemGiveConditionType) + _questionMark,
            nameof(SettingDifficulty) + _questionMark,
            nameof(ItemDateGiftType) + _questionMark,
            nameof(LocationType) + _questionMark,
            nameof(AilmentTriggerType) + _questionMark,
            nameof(AilmentTriggerStepsProcessType) + _questionMark,
            nameof(CutsceneStepType) + _questionMark,
            nameof(CutsceneStepProceedType) + _questionMark,
            nameof(CutsceneStepDollTargetType) + _questionMark,
            nameof(DollOrientationType) + _questionMark,
            nameof(DollPositionType) + _questionMark,
            nameof(CutsceneStepAnimationType) + _questionMark,
            nameof(CutsceneStepSubCutsceneType) + _questionMark,
            nameof(CutsceneStepNotificationType) + _questionMark,
            nameof(GirlPairRelationshipType) + _questionMark,
            nameof(GiftConditionType) + _questionMark,
            nameof(GirlPartType) + _questionMark,
            nameof(LogicActionType) + _questionMark,
            nameof(PuzzleGameState) + _questionMark,
            nameof(LogicConditionType) + _questionMark,
            nameof(MatchConditionType) + _questionMark,
            nameof(MoveConditionType) + _questionMark,
            nameof(MoveConditionTokenType) + _questionMark,
            nameof(TokenConditionType) + _questionMark,
            nameof(TokenConditionTokenType) + _questionMark,
            nameof(GirlConditionType) + _questionMark,
            nameof(PuzzleStatusType) + _questionMark
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

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultTokenNames", (gameData) => _nullListStr.Concat(gameData.Tokens.GetAll().Select(x => x.name))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultOutfitNames", (gameData) => _nullListStr.Concat(gameData.Girls.GetAll().SelectMany(x => x.outfits.Select(y => x.name + y.outfitName)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<string>>>("DefaultHairstyleNames", (gameData) => _nullListStr.Concat(gameData.Girls.GetAll().SelectMany(x => x.hairstyles.Select(y => x.name + y.hairstyleName))))
        };

        private static readonly List<KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>> _gameDataIdArrays = new List<KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>>()
        {
            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultAbilityIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Abilities.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultAilmentIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Ailments.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultCodeIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Codes.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultCutsceneIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Cutscenes.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultDialogTriggerIds", (gameData)
                => _nullListIdNullable.Concat(gameData.DialogTriggers.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultDlcIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Dlcs.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultEnergyIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Energy.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultGirlIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Girls.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultGirlPairIds", (gameData)
                => _nullListIdNullable.Concat(gameData.GirlPairs.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultItemIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Items.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultLocationIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Locations.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultPhotoIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Photos.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultQuestionIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Questions.GetAll().Select(x => (RelativeId?)new RelativeId(x)))),

            new KeyValuePair<string, Func<GameData, IEnumerable<RelativeId?>>>("DefaultTokenIds", (gameData)
                => _nullListIdNullable.Concat(gameData.Tokens.GetAll().Select(x => (RelativeId?)new RelativeId(x))))
        };

        private static string MakeEnumName(string enumName)
            => (enumName.EndsWith("?") ? enumName.Substring(0, enumName.Length - 1) + "Nullable" : enumName);

        private static string MakeArrayAttribute(string name, IEnumerable<string> entries, string formatString = "{0}")
            => $"[UiSonArray(\"{name}\", new object [] {{{ToCsv(entries, formatString)}}})]";

        private static string MakeRelativeIdArrayAttribute(string name, IEnumerable<string> entries, string formatString = "{0}")
            => $"[UiSonArray(\"{name}\", new object [] {{{ToCsv(entries, formatString)}}}, typeof(RelativeId?))]";

        private static string ToCsv(IEnumerable<string> items, string formatString = "{0}") => string.Join(",", items.Select(x => string.Format(formatString, x ?? "null")));

        internal static void MakeDefaultDataDotCs(GameData gameData)
        {
            try
            {
                ModInterface.Log.LogLine("Creating file");
                var file = File.CreateText(_defaultDataPath);

                ModInterface.Log.LogLine("Header and usings");
                file.WriteLine("// Hp2BaseMod 2022, by OneSuchKeeper");
                file.WriteLine($"// This file was auto-generated on {DateTime.Now}. Changes made manually may be overridden.");

                file.WriteLine(string.Empty);

                file.WriteLine(@"using System;");
                file.WriteLine(@"using System.Collections.Generic;");
                file.WriteLine(@"using System.Linq;");
                file.WriteLine(@"using UiSon.Attribute;");
                file.WriteLine(@"using Hp2BaseMod.GameDataInfo;");
                file.WriteLine(@"using Hp2BaseMod.Utility;");

                file.WriteLine(string.Empty);

                file.WriteLine(@"namespace Hp2BaseMod{");
                ModInterface.Log.LogLine("Attributes");

                file.WriteLine("    [UiSonArray(\"NullableBoolNames\", new object [] {\"null\", \"true\", \"false\"})]");
                file.WriteLine("    [UiSonArray(\"NullableBoolIds\", new object [] {\"null\", true, false})]");

                foreach (var entry in _staticStringArrays)
                {
                    file.WriteLine("    " + MakeArrayAttribute(entry.Key, entry.Value, _addQoutesFormat));
                }
                foreach (var entry in _staticIdArrays)
                {
                    file.WriteLine("    " + MakeRelativeIdArrayAttribute(entry.Key, entry.Value?.Select(x => $"\"{JsonConvert.SerializeObject(x).Replace("\"", "\\\"")}\"")));
                }
                foreach (var entry in _staticEnumerableArrays)
                {
                    file.WriteLine("    " + MakeArrayAttribute(entry.Key, entry.Value.Select(x => x?.ToString() ?? "null")));
                }

                file.WriteLine(string.Empty);

                foreach (var entry in _enumArrays)
                {
                    file.WriteLine($"    [UiSonArray(\"{MakeEnumName(entry)}\", typeof({entry}))]");
                }

                file.WriteLine(string.Empty);

                foreach (var entry in _gameDataNameArrays)
                {
                    file.WriteLine("    " + MakeArrayAttribute(entry.Key, entry.Value.Invoke(gameData), _addQoutesFormat));
                }
                foreach (var entry in _gameDataIdArrays)
                {
                    file.WriteLine("    " + MakeRelativeIdArrayAttribute(entry.Key, entry.Value.Invoke(gameData)?.Select(x => $"\"{JsonConvert.SerializeObject(x).Replace("\"", "\\\"")}\"")));
                }
                ModInterface.Log.LogLine("Class");
                file.WriteLine(@"   public class DefaultData{");
                foreach (var entry in _staticStringArrays)
                {
                    file.WriteLine($"       internal const string {entry.Key}_Name = \"{entry.Key}\";");
                    file.WriteLine($"       public static readonly IEnumerable<string> {entry.Key} = new string[] {{{ToCsv(entry.Value.Skip(1), _addQoutesFormat)}}};");
                }
                foreach (var entry in _staticIdArrays)
                {
                    file.WriteLine($"       internal const string {entry.Key}_Name = \"{entry.Key}\";");
                    file.WriteLine($"       public static readonly IEnumerable<{nameof(RelativeId)}> {entry.Key} = new {nameof(RelativeId)}[] {{{ToCsv(entry.Value.Skip(1).Select(x => $"new {nameof(RelativeId)}({x.Value.SourceId}, {x.Value.LocalId})"))}}};");
                }
                foreach (var entry in _staticEnumerableArrays)
                {
                    file.WriteLine($"       internal const string {entry.Key}_Name = \"{entry.Key}\";");
                    file.WriteLine($"       public static readonly IEnumerable<int> {entry.Key} = new int[] {{{ToCsv(entry.Value.Skip(1).Select(x => x.ToString()))}}};");
                }

                file.WriteLine(string.Empty);

                foreach (var entry in _enumArrays)
                {
                    var name = MakeEnumName(entry);
                    file.WriteLine($"       internal const string {name} = \"{name}\";");
                }

                file.WriteLine(string.Empty);

                foreach (var entry in _gameDataNameArrays)
                {
                    file.WriteLine($"       internal const string {entry.Key}_Name = \"{entry.Key}\";");
                    file.WriteLine($"       public static readonly IEnumerable<string> {entry.Key} = new string[] {{{ToCsv(entry.Value.Invoke(gameData).Skip(1), _addQoutesFormat)}}};");
                }

                file.WriteLine(string.Empty);

                foreach (var entry in _gameDataIdArrays)
                {
                    file.WriteLine($"       internal const string {entry.Key}_Name = \"{entry.Key}\";");

                    var ids = entry.Value.Invoke(gameData);

                    file.WriteLine($"       public static readonly IEnumerable<{nameof(RelativeId)}> {entry.Key} = new {nameof(RelativeId)}[] {{{ToCsv(ids.Skip(1).Select(x => $"new {nameof(RelativeId)}({x.Value.SourceId}, {x.Value.LocalId})"))}}};");

                    file.WriteLine($"       private static {nameof(IntSetChecker)} _{entry.Key}_Checker = new {nameof(IntSetChecker)}({entry.Key}.Select(x => x.LocalId));");

                    var sansIds = entry.Key.Substring(0, entry.Key.Length - 3);

                    file.WriteLine($"       public static bool Is{sansIds}(int runtimeId) => _{entry.Key}_Checker.Contains(runtimeId);");
                    file.WriteLine($"       public static int {sansIds}_Count => {ids.Count() - 1};");
                }

                file.WriteLine(string.Empty);

                file.WriteLine(@"   }");
                file.WriteLine(@"}");

                file.Flush();
            }
            catch (Exception e)
            {
                ModInterface.Log.LogLine(e.ToString());
            }
        }
    }
}
