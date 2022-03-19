// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a GirlPairDefinition
    /// </summary>
    [UiSonClass]
    public class GirlPairDataMod : DataMod<GirlPairDefinition>
    {
        public List<GirlPairFavQuestionInfo> FavQuestions;
        public int? GirlDefinitionOneID;
        public int? GirlDefinitionTwoID;
        public bool? HasMeetingStyleOne;
        public bool? HasMeetingStyleTwo;
        public bool? IntroductionPair;
        public bool? IntroSidesFlipped;
        public int? MeetingLocationDefinitionID;
        public GirlStyleType? MeetingStyleTypeOne;
        public GirlStyleType? MeetingStyleTypeTwo;
        public int? PhotoDefinitionID;
        public List<int> RelationshipCutsceneDefinitionIDs;
        public ClockDaytimeType? SexDayTime;
        public int? SexLocationDefinitionID;
        public GirlStyleType? SexStyleTypeOne;
        public GirlStyleType? SexStyleTypeTwo;
        public bool? SpecialPair;

        public GirlPairDataMod() { }

        public GirlPairDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
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
                               bool isAdditive = false)
            : base(id, isAdditive)
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
            : base(def.id, false)
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

        public override void SetData(GirlPairDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;
            def.active = true;

            Access.NullableSet(ref def.meetingStyleTypeOne, MeetingStyleTypeOne);
            Access.NullableSet(ref def.meetingStyleTypeTwo, MeetingStyleTypeTwo);
            Access.NullableSet(ref def.introductionPair, IntroductionPair);
            Access.NullableSet(ref def.introSidesFlipped, IntroSidesFlipped);
            Access.NullableSet(ref def.sexDaytime, SexDayTime);
            Access.NullableSet(ref def.sexStyleTypeOne, SexStyleTypeOne);
            Access.NullableSet(ref def.sexStyleTypeTwo, SexStyleTypeTwo);
            Access.NullableSet(ref def.specialPair, SpecialPair);
            Access.NullableSet(ref def.hasMeetingStyleOne, HasMeetingStyleOne);
            Access.NullableSet(ref def.hasMeetingStyleTwo, HasMeetingStyleTwo);

            if (GirlDefinitionOneID.HasValue) { def.girlDefinitionOne = gameData.Girls.Get(GirlDefinitionOneID.Value); }
            if (GirlDefinitionTwoID.HasValue) { def.girlDefinitionTwo = gameData.Girls.Get(GirlDefinitionTwoID.Value); }
            if (MeetingLocationDefinitionID.HasValue) { def.meetingLocationDefinition = gameData.Locations.Get(MeetingLocationDefinitionID.Value); }
            if (PhotoDefinitionID.HasValue) { def.photoDefinition = gameData.Photos.Get(PhotoDefinitionID.Value); }
            if (SexLocationDefinitionID.HasValue) { def.sexLocationDefinition = gameData.Locations.Get(SexLocationDefinitionID.Value); }

            SetGirlPairFavQuestions(ref def.favQuestions, FavQuestions, gameData);
            SetCutscenes(ref def.relationshipCutsceneDefinitions, RelationshipCutsceneDefinitionIDs, gameData);
        }
    }
}
