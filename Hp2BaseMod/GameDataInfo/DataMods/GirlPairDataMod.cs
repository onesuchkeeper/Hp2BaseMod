﻿// Hp2BaseMod 2021, By OneSuchKeeper

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
    public class GirlPairDataMod : DataMod, IGirlPairDataMod
    {
        [UiSonCollection(true, false, DisplayMode.Grid)]
        [UiSonEncapsulatingUi(0, null, DisplayMode.Grid)]
        public List<GirlPairFavQuestionInfo> FavQuestions;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 10, null, "id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public RelativeId? GirlDefinitionOneID;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 10, null, "id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public RelativeId? GirlDefinitionTwoID;

        [UiSonSelectorUi("NullableBoolNames", 10, null, "NullableBoolIds")]
        public bool? SpecialPair;

        [UiSonElementSelectorUi(nameof(PhotoDataMod), 10, null, "id", DefaultData.DefaultPhotoNames_Name, DefaultData.DefaultPhotoIds_Name)]
        public RelativeId? PhotoDefinitionID;

        [UiSonSelectorUi("NullableBoolNames", 0, "Meeting", "NullableBoolIds")]
        public bool? IntroductionPair;

        [UiSonSelectorUi("NullableBoolNames", 0, "Meeting", "NullableBoolIds")]
        public bool? IntroSidesFlipped;

        [UiSonSelectorUi("NullableBoolNames", 0, "Meeting", "NullableBoolIds")]
        public bool? HasMeetingStyleOne;

        [UiSonSelectorUi("NullableBoolNames", 0, "Meeting", "NullableBoolIds")]
        public bool? HasMeetingStyleTwo;

        [UiSonElementSelectorUi(nameof(LocationDataMod), 0, "Meeting", "id", DefaultData.DefaultLocationNames_Name, DefaultData.DefaultLocationIds_Name)]
        public RelativeId? MeetingLocationDefinitionID;

        [UiSonSelectorUi(DefaultData.ClockDaytimeTypeNullable, 0, "Sex")]
        public ClockDaytimeType? SexDayTime;

        [UiSonEncapsulatingUi]
        public PairStyleInfo Styles;

        [UiSonElementSelectorUi(nameof(LocationDataMod), 0, "Sex", "id", DefaultData.DefaultLocationNames_Name, DefaultData.DefaultLocationIds_Name)]
        public RelativeId? SexLocationDefinitionID;

        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, null, "id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public RelativeId? IntroRelationshipCutsceneDefinitionID;

        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, null, "id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public RelativeId? AttractRelationshipCutsceneDefinitionID;

        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, null, "id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public RelativeId? PreSexRelationshipCutsceneDefinitionID;

        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, null, "id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public RelativeId? PostSexRelationshipCutsceneDefinitionID;

        /// <inheritdoc/>
        public GirlPairDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public GirlPairDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        internal GirlPairDataMod(GirlPairDefinition def)
            : base(new RelativeId(def), InsertStyle.replace, 0)
        {
            FavQuestions = def.favQuestions?.Select(x => new GirlPairFavQuestionInfo(x)).ToList();
            GirlDefinitionOneID = new RelativeId(def.girlDefinitionOne);
            GirlDefinitionTwoID = new RelativeId(def.girlDefinitionTwo);
            HasMeetingStyleOne = def.hasMeetingStyleOne;
            HasMeetingStyleTwo = def.hasMeetingStyleTwo;
            IntroductionPair = def.introductionPair;
            IntroSidesFlipped = def.introSidesFlipped;
            MeetingLocationDefinitionID = new RelativeId(def.meetingLocationDefinition);

            PhotoDefinitionID = new RelativeId(def.photoDefinition);

            var cutsceneIt = def.relationshipCutsceneDefinitions.GetEnumerator();
            cutsceneIt.MoveNext();
            IntroRelationshipCutsceneDefinitionID = new RelativeId(cutsceneIt.Current);
            cutsceneIt.MoveNext();
            AttractRelationshipCutsceneDefinitionID = new RelativeId(cutsceneIt.Current);
            cutsceneIt.MoveNext();
            PreSexRelationshipCutsceneDefinitionID = new RelativeId(cutsceneIt.Current);
            cutsceneIt.MoveNext();
            PostSexRelationshipCutsceneDefinitionID = new RelativeId(cutsceneIt.Current);

            SexDayTime = def.sexDaytime;
            SexLocationDefinitionID = new RelativeId(def.sexLocationDefinition);

            SpecialPair = def.specialPair;

            Styles = new PairStyleInfo()
            {
                MeetingGirlOne = new GirlStyleInfo() { HairstyleId = new RelativeId(-1, (int)def.meetingStyleTypeOne), OutfitId = new RelativeId(-1, (int)def.meetingStyleTypeOne) },
                MeetingGirlTwo = new GirlStyleInfo() { HairstyleId = new RelativeId(-1, (int)def.meetingStyleTypeTwo), OutfitId = new RelativeId(-1, (int)def.meetingStyleTypeTwo) },
                SexGirlOne = new GirlStyleInfo() { HairstyleId = new RelativeId(-1, (int)def.sexStyleTypeOne), OutfitId = new RelativeId(-1, (int)def.sexStyleTypeOne) },
                SexGirlTwo = new GirlStyleInfo() { HairstyleId = new RelativeId(-1, (int)def.sexStyleTypeTwo), OutfitId = new RelativeId(-1, (int)def.sexStyleTypeTwo) }
            };
        }

        /// <inheritdoc/>
        public void SetData(GirlPairDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider)
        {
            ValidatedSet.SetValue(ref def.introductionPair, IntroductionPair);
            ValidatedSet.SetValue(ref def.introSidesFlipped, IntroSidesFlipped);
            ValidatedSet.SetValue(ref def.sexDaytime, SexDayTime);
            ValidatedSet.SetValue(ref def.specialPair, SpecialPair);
            ValidatedSet.SetValue(ref def.hasMeetingStyleOne, HasMeetingStyleOne);
            ValidatedSet.SetValue(ref def.hasMeetingStyleTwo, HasMeetingStyleTwo);

            ValidatedSet.SetValue(ref def.girlDefinitionOne, (GirlDefinition)gameDataProvider.GetDefinition(GameDataType.Girl, GirlDefinitionOneID), InsertStyle);
            ValidatedSet.SetValue(ref def.girlDefinitionTwo, (GirlDefinition)gameDataProvider.GetDefinition(GameDataType.Girl, GirlDefinitionTwoID), InsertStyle);
            ValidatedSet.SetValue(ref def.meetingLocationDefinition, (LocationDefinition)gameDataProvider.GetDefinition(GameDataType.Location, MeetingLocationDefinitionID), InsertStyle);
            ValidatedSet.SetValue(ref def.photoDefinition, (PhotoDefinition)gameDataProvider.GetDefinition(GameDataType.Photo, PhotoDefinitionID), InsertStyle);
            ValidatedSet.SetValue(ref def.sexLocationDefinition, (LocationDefinition)gameDataProvider.GetDefinition(GameDataType.Location, SexLocationDefinitionID), InsertStyle);

            ValidatedSet.SetListValue(ref def.favQuestions, FavQuestions, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.relationshipCutsceneDefinitions,
                                      new[] { gameDataProvider.GetCutscene(IntroRelationshipCutsceneDefinitionID),
                                              gameDataProvider.GetCutscene(AttractRelationshipCutsceneDefinitionID),
                                              gameDataProvider.GetCutscene(PreSexRelationshipCutsceneDefinitionID),
                                              gameDataProvider.GetCutscene(PostSexRelationshipCutsceneDefinitionID)},
                                      InsertStyle);
        }

        public PairStyleInfo GetStyles() => Styles;
    }
}
