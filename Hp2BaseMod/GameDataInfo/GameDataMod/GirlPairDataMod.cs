// Hp2BaseMod 2021, By OneSuchKeeper

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
    [UiSonGroup("Girl One", -100)]
    [UiSonGroup("Girl Two", -99)]
    public class GirlPairDataMod : DataMod, IGameDataMod<GirlPairDefinition>
    {
        [UiSonCollection(true, DisplayMode.Grid)]
        [UiSonMemberElement(-98, null, DisplayMode.Grid)]
        public List<GirlPairFavQuestionInfo> FavQuestions;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 0, "Girl One", "id", DefaultData.DefaultGirlNames, DefaultData.DefaultGirlIds)]
        public int? GirlDefinitionOneID;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 0, "Girl Two", "id", DefaultData.DefaultGirlNames, DefaultData.DefaultGirlIds)]
        public int? GirlDefinitionTwoID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions, 0, "Girl One")]
        public bool? HasMeetingStyleOne;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions, 0, "Girl Two")]
        public bool? HasMeetingStyleTwo;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? IntroductionPair;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? IntroSidesFlipped;

        [UiSonElementSelectorUi(nameof(LocationDataMod), 0, null, "id", DefaultData.DefaultMeetingLocationNames, DefaultData.DefaultMeetingLocationIds)]
        public int? MeetingLocationDefinitionID;

        [UiSonSelectorUi(DefaultData.GirlStyleTypeNullable_As_String, 0, "Girl One")]
        public GirlStyleType? MeetingStyleTypeOne;

        [UiSonSelectorUi(DefaultData.GirlStyleTypeNullable_As_String, 0, "Girl Two")]
        public GirlStyleType? MeetingStyleTypeTwo;

        [UiSonElementSelectorUi(nameof(PhotoDataMod), 0, null, "id", DefaultData.DefaultPhotoNames, DefaultData.DefaultPhotoIds)]
        public int? PhotoDefinitionID;

        [UiSonElementSelectorUi(nameof(CutsceneDataMod), -97, null, "id", DefaultData.DefaultCutsceneNames, DefaultData.DefaultCutsceneIds)]
        public List<int> RelationshipCutsceneDefinitionIDs;

        [UiSonSelectorUi(DefaultData.ClockDaytimeTypeNullable_As_String)]
        public ClockDaytimeType? SexDayTime;

        [UiSonElementSelectorUi(nameof(LocationDataMod), 0, null, "id", DefaultData.DefaultLocationNames, DefaultData.DefaultLocationIds)]
        public int? SexLocationDefinitionID;

        [UiSonSelectorUi(DefaultData.GirlStyleTypeNullable_As_String, 0, "Girl One")]
        public GirlStyleType? SexStyleTypeOne;

        [UiSonSelectorUi(DefaultData.GirlStyleTypeNullable_As_String, 0, "Girl Two")]
        public GirlStyleType? SexStyleTypeTwo;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? SpecialPair;

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
                               GirlStyleType? meetingStyleTypeOne,
                               GirlStyleType? meetingStyleTypeTwo,
                               int? photoDefinitionID,
                               List<int> relationshipCutsceneDefinitionIDs,
                               ClockDaytimeType? sexDayTime,
                               int? sexLocationDefinitionID,
                               GirlStyleType? sexStyleTypeOne,
                               GirlStyleType? sexStyleTypeTwo,
                               bool? specialPair,
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
            MeetingStyleTypeOne = meetingStyleTypeOne;
            MeetingStyleTypeTwo = meetingStyleTypeTwo;
            PhotoDefinitionID = photoDefinitionID;
            RelationshipCutsceneDefinitionIDs = relationshipCutsceneDefinitionIDs;
            SexDayTime = sexDayTime;
            SexLocationDefinitionID = sexLocationDefinitionID;
            SexStyleTypeOne = sexStyleTypeOne;
            SexStyleTypeTwo = sexStyleTypeTwo;
            SpecialPair = specialPair;
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
            MeetingStyleTypeOne = def.meetingStyleTypeOne;
            MeetingStyleTypeTwo = def.meetingStyleTypeTwo;
            PhotoDefinitionID = def.photoDefinition?.id ?? -1;
            RelationshipCutsceneDefinitionIDs = def.relationshipCutsceneDefinitions?.Select(x => x?.id ?? -1).ToList();
            SexDayTime = def.sexDaytime;
            SexLocationDefinitionID = def.sexLocationDefinition?.id ?? -1;
            SexStyleTypeOne = def.sexStyleTypeOne;
            SexStyleTypeTwo = def.sexStyleTypeTwo;
            SpecialPair = def.specialPair;
        }

        public void SetData(GirlPairDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            def.id = Id;
            def.active = true;

            ValidatedSet.SetValue(ref def.meetingStyleTypeOne, MeetingStyleTypeOne);
            ValidatedSet.SetValue(ref def.meetingStyleTypeTwo, MeetingStyleTypeTwo);
            ValidatedSet.SetValue(ref def.introductionPair, IntroductionPair);
            ValidatedSet.SetValue(ref def.introSidesFlipped, IntroSidesFlipped);
            ValidatedSet.SetValue(ref def.sexDaytime, SexDayTime);
            ValidatedSet.SetValue(ref def.sexStyleTypeOne, SexStyleTypeOne);
            ValidatedSet.SetValue(ref def.sexStyleTypeTwo, SexStyleTypeTwo);
            ValidatedSet.SetValue(ref def.specialPair, SpecialPair);
            ValidatedSet.SetValue(ref def.hasMeetingStyleOne, HasMeetingStyleOne);
            ValidatedSet.SetValue(ref def.hasMeetingStyleTwo, HasMeetingStyleTwo);

            ValidatedSet.SetValue(ref def.girlDefinitionOne, gameDataProvider.GetGirl(GirlDefinitionOneID), insertStyle);
            ValidatedSet.SetValue(ref def.girlDefinitionTwo, gameDataProvider.GetGirl(GirlDefinitionTwoID), insertStyle);
            ValidatedSet.SetValue(ref def.meetingLocationDefinition, gameDataProvider.GetLocation(MeetingLocationDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.photoDefinition, gameDataProvider.GetPhoto(PhotoDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.sexLocationDefinition, gameDataProvider.GetLocation(SexLocationDefinitionID), insertStyle);

            ValidatedSet.SetListValue(ref def.favQuestions, FavQuestions, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.relationshipCutsceneDefinitions, RelationshipCutsceneDefinitionIDs?.Select(x => gameDataProvider.GetCutscene(x)), insertStyle);
        }
    }
}
