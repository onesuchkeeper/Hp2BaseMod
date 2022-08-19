// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.Extension.IEnumerableExtension;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a GirlPairDefinition
    /// </summary>
    public class GirlPairDataMod : DataMod, IGirlPairDataMod
    {
        public List<GirlPairFavQuestionInfo> FavQuestions;

        public RelativeId? GirlDefinitionOneID;

        public RelativeId? GirlDefinitionTwoID;

        public bool? SpecialPair;

        public RelativeId? PhotoDefinitionID;

        public bool? IntroductionPair;

        public bool? IntroSidesFlipped;

        public bool? HasMeetingStyleOne;

        public bool? HasMeetingStyleTwo;

        public RelativeId? MeetingLocationDefinitionID;

        public ClockDaytimeType? SexDayTime;

        public PairStyleInfo Styles;

        public RelativeId? SexLocationDefinitionID;

        public RelativeId? IntroRelationshipCutsceneDefinitionID;

        public RelativeId? AttractRelationshipCutsceneDefinitionID;

        public RelativeId? PreSexRelationshipCutsceneDefinitionID;

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

        public override void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            Id = getNewId(Id) ?? Id;

            foreach (var entry in FavQuestions.OrEmptyIfNull())
            {
                entry?.ReplaceRelativeIds(getNewId);
            }

            GirlDefinitionOneID = getNewId(GirlDefinitionOneID);
            GirlDefinitionTwoID = getNewId(GirlDefinitionTwoID);
            PhotoDefinitionID = getNewId(PhotoDefinitionID);
            MeetingLocationDefinitionID = getNewId(MeetingLocationDefinitionID);
            SexLocationDefinitionID = getNewId(SexLocationDefinitionID);
            IntroRelationshipCutsceneDefinitionID = getNewId(IntroRelationshipCutsceneDefinitionID);
            AttractRelationshipCutsceneDefinitionID = getNewId(AttractRelationshipCutsceneDefinitionID);
            PreSexRelationshipCutsceneDefinitionID = getNewId(PreSexRelationshipCutsceneDefinitionID);
            PostSexRelationshipCutsceneDefinitionID = getNewId(PostSexRelationshipCutsceneDefinitionID);
        }
    }
}
