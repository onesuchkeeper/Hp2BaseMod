// Hp2Sample 2021, By OneSuchKeeper

using Hp2BaseMod;
using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.GameDataMods;
using System;
using System.Collections.Generic;

namespace Hp2SingleDateMod
{
    [Serializable]
    public class SingleDateConfig
    {
        public int GirlId;
        public bool HasMeetingStyle;
        public bool IntroSidesFlipped;
        public int MeetingLocationId;
        public GirlStyleType MeetingStyle;
        public PhotoDataMod Photo;
        public GirlStyleType SexStyle;
        public ClockDaytimeType SexDayTime;
        public int SexLocationDefinitionID;
        public CutsceneDataMod MeetingCutscene;
        public CutsceneDataMod AttractedCutscene;
        public CutsceneDataMod PreSexCutscene;
        public CutsceneDataMod PostSexCutscene;

        public int AddMods(GameDataModder gameDataModder)
        {
            gameDataModder.AddData(Photo);

            gameDataModder.AddData(MeetingCutscene);
            gameDataModder.AddData(AttractedCutscene);
            gameDataModder.AddData(PreSexCutscene);
            gameDataModder.AddData(PostSexCutscene);

            int newPairId = Constants.EmptyGirlId + GirlId;

            var newPairMod = new GirlPairDataMod(newPairId, false)
            {
                FavQuestions = new List<GirlPairFavQuestionInfo>(),
                GirlDefinitionOneID = GirlId,
                GirlDefinitionTwoID = Constants.EmptyGirlId,
                HasMeetingStyleOne = HasMeetingStyle,
                HasMeetingStyleTwo = false,
                IntroductionPair = false,
                IntroSidesFlipped = IntroSidesFlipped,
                MeetingLocationDefinitionID = MeetingLocationId,
                MeetingStyleTypeOne = MeetingStyle,
                MeetingStyleTypeTwo = GirlStyleType.RELAXING,
                PhotoDefinitionID = Photo.Id,
                RelationshipCutsceneDefinitionIDs = new List<int>() { MeetingCutscene.Id,
                                                                      AttractedCutscene.Id,
                                                                      PreSexCutscene.Id,
                                                                      PostSexCutscene.Id},
                SexDayTime = this.SexDayTime,
                SexLocationDefinitionID = this.SexLocationDefinitionID,
                SexStyleTypeOne = SexStyle,
                SexStyleTypeTwo = GirlStyleType.RELAXING,
                SpecialPair = false
            };

            gameDataModder.AddData(newPairMod);

            var AddPairToGirlMod = new GirlDataMod(GirlId, true)
            {
                GirlPairDefIDs = new List<int>() { newPairId }
            };

            gameDataModder.AddData(AddPairToGirlMod);

            return newPairId;
        }
    }
}
