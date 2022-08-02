using Hp2BaseMod.Save.Interface;
using Hp2BaseMod.Utility;
using System.Collections.Generic;

namespace Hp2BaseMod.Save
{
    public class ModSaveGirlPair : IModSave<SaveFileGirlPair>
    {
        public List<SavedSourceId> LearnedFavs = new List<SavedSourceId>();
        public List<SavedSourceId> RecentFavQuestions = new List<SavedSourceId>();

        public void Strip(SaveFileGirlPair save)
        {
            ValidatedSet.StripRuntimeIds(ref save.learnedFavs,
                                         LearnedFavs,
                                         $"Attempting to save learned favorites for pair {save.girlPairId}",
                                         (x) => ModInterface.Data.GetDataId(GameDataType.Question, x));

            ValidatedSet.StripRuntimeIds(ref save.recentFavQuestions,
                                         RecentFavQuestions,
                                         $"Attempting to save recient favorites for pair {save.girlPairId}",
                                         (x) => ModInterface.Data.GetDataId(GameDataType.Question, x));
        }

        public void SetData(SaveFileGirlPair save)
        {
            ValidatedSet.SetModIds(save.learnedFavs, LearnedFavs, (x) => ModInterface.Data.GetRuntimeDataId(GameDataType.Question, x));
            ValidatedSet.SetModIds(save.recentFavQuestions, RecentFavQuestions, (x) => ModInterface.Data.GetRuntimeDataId(GameDataType.Question, x));
        }
    }
}
