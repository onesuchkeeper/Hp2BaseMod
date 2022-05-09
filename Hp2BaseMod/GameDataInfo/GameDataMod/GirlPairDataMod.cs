// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.EnumExpansion;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a GirlPairDefinition
    /// </summary>
    [UiSonElement]
    [UiSonGroup("Meeting", 2)]
    [UiSonGroup("Sex", 1)]
    public class GirlPairDataMod : DataMod, IGameDataMod<GirlPairDefinition>
    {
        [UiSonCollection(true, DisplayMode.Grid)]
        [UiSonMemberElement(0, null, DisplayMode.Grid)]
        public List<GirlPairFavQuestionInfo> FavQuestions;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 10, null, "id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public int? GirlDefinitionOneID;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 10, null, "id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public int? GirlDefinitionTwoID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 10)]
        public bool? SpecialPair;

        [UiSonElementSelectorUi(nameof(PhotoDataMod), 10, null, "id", DefaultData.DefaultPhotoNames_Name, DefaultData.DefaultPhotoIds_Name)]
        public int? PhotoDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Meeting")]
        public bool? IntroductionPair;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Meeting")]
        public bool? IntroSidesFlipped;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Meeting")]
        public bool? HasMeetingStyleOne;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Meeting")]
        public bool? HasMeetingStyleTwo;

        [UiSonElementSelectorUi(nameof(LocationDataMod), 0, "Meeting", "id", DefaultData.DefaultLocationNames_Name, DefaultData.DefaultLocationIds_Name)]
        public int? MeetingLocationDefinitionID;

        [UiSonSelectorUi(DefaultData.ClockDaytimeTypeNullable_As_String, 0, "Sex")]
        public ClockDaytimeType? SexDayTime;

        [UiSonMemberElement]
        public PairStyleInfo Styles;

        [UiSonElementSelectorUi(nameof(LocationDataMod), 0, "Sex", "id", DefaultData.DefaultLocationNames_Name, DefaultData.DefaultLocationIds_Name)]
        public int? SexLocationDefinitionID;

        [UiSonCollection(false)]
        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, null, "id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public List<int?> RelationshipCutsceneDefinitionIDs = new List<int?>() { -1,-1,-1,-1};

        public GirlPairDataMod() { }

        public GirlPairDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
        }

        public GirlPairDataMod(int id,
                               List<GirlPairFavQuestionInfo> favQuestions,
                               int? girlDefinitionOneID,
                               int? girlDefinitionTwoID,
                               bool? hasMeetingStyleOne,
                               bool? hasMeetingStyleTwo,
                               bool? introductionPair,
                               bool? introSidesFlipped,
                               int? meetingLocationDefinitionID,
                               int? photoDefinitionID,
                               List<int?> relationshipCutsceneDefinitionIDs,
                               ClockDaytimeType? sexDayTime,
                               int? sexLocationDefinitionID,
                               bool? specialPair,
                               PairStyleInfo styles,
                               InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
            FavQuestions = favQuestions;
            GirlDefinitionOneID = girlDefinitionOneID;
            GirlDefinitionTwoID = girlDefinitionTwoID;
            HasMeetingStyleOne = hasMeetingStyleOne;
            HasMeetingStyleTwo = hasMeetingStyleTwo;
            IntroductionPair = introductionPair;
            IntroSidesFlipped = introSidesFlipped;
            MeetingLocationDefinitionID = meetingLocationDefinitionID;
            PhotoDefinitionID = photoDefinitionID;
            RelationshipCutsceneDefinitionIDs = relationshipCutsceneDefinitionIDs;
            SexDayTime = sexDayTime;
            SexLocationDefinitionID = sexLocationDefinitionID;
            SpecialPair = specialPair;
            Styles = styles;
        }

        public GirlPairDataMod(GirlPairDefinition def)
            : base(def.id, InsertStyle.replace, def.name)
        {
            FavQuestions = def.favQuestions?.Select(x => new GirlPairFavQuestionInfo(x)).ToList();
            GirlDefinitionOneID = def.girlDefinitionOne?.id ?? -1;
            GirlDefinitionTwoID = def.girlDefinitionTwo?.id ?? -1;
            HasMeetingStyleOne = def.hasMeetingStyleOne;
            HasMeetingStyleTwo = def.hasMeetingStyleTwo;
            Id = def.id;
            IntroductionPair = def.introductionPair;
            IntroSidesFlipped = def.introSidesFlipped;
            MeetingLocationDefinitionID = def.meetingLocationDefinition?.id ?? -1;

            PhotoDefinitionID = def.photoDefinition?.id ?? -1;
            RelationshipCutsceneDefinitionIDs = def.relationshipCutsceneDefinitions?.Select(x => x?.id).ToList();
            SexDayTime = def.sexDaytime;
            SexLocationDefinitionID = def.sexLocationDefinition?.id ?? -1;

            SpecialPair = def.specialPair;

            EnumLookups.AddPairInfo(def);
            Styles = EnumLookups.GetPairStyleInfo(def.id);
        }

        public void SetData(GirlPairDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a girl pair");
            ModInterface.Instance.IncreaseLogIndent();

            def.id = Id;
            def.active = true;

            // Styles will be looked up, so there's no need to actually set them
            EnumLookups.AddPairInfo(Id, Styles);

            ValidatedSet.SetValue(ref def.introductionPair, IntroductionPair);
            ValidatedSet.SetValue(ref def.introSidesFlipped, IntroSidesFlipped);
            ValidatedSet.SetValue(ref def.sexDaytime, SexDayTime);
            ValidatedSet.SetValue(ref def.specialPair, SpecialPair);
            ValidatedSet.SetValue(ref def.hasMeetingStyleOne, HasMeetingStyleOne);
            ValidatedSet.SetValue(ref def.hasMeetingStyleTwo, HasMeetingStyleTwo);

            ValidatedSet.SetValue(ref def.girlDefinitionOne, gameDataProvider.GetGirl(GirlDefinitionOneID), insertStyle);
            ValidatedSet.SetValue(ref def.girlDefinitionTwo, gameDataProvider.GetGirl(GirlDefinitionTwoID), insertStyle);
            ValidatedSet.SetValue(ref def.meetingLocationDefinition, gameDataProvider.GetLocation(MeetingLocationDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.photoDefinition, gameDataProvider.GetPhoto(PhotoDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.sexLocationDefinition, gameDataProvider.GetLocation(SexLocationDefinitionID), insertStyle);

            ValidatedSet.SetListValue(ref def.favQuestions, FavQuestions, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.relationshipCutsceneDefinitions,
                                      RelationshipCutsceneDefinitionIDs?.Select(x => gameDataProvider.GetCutscene(x)), insertStyle);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
