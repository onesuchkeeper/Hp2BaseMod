// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a GirlPairFavQuestionSubDefinition
    /// </summary>
    [Serializable]
    public class GirlPairFavQuestionInfo
    {
        public int QuestionDefinitionID;
        public int GirlResponceIndexOne;
        public int GirlResponceIndexTwo;

        public GirlPairFavQuestionInfo() { }

        public GirlPairFavQuestionInfo(int questionDefinitionID,
                                       int girlResponceIndexOne,
                                       int girlResponceIndexTwo)
        {
            QuestionDefinitionID = questionDefinitionID;
            GirlResponceIndexOne = girlResponceIndexOne;
            GirlResponceIndexTwo = girlResponceIndexTwo;
        }

        public GirlPairFavQuestionInfo(GirlPairFavQuestionSubDefinition girlPairFavQuestion)
        {
            if (girlPairFavQuestion == null) { throw new ArgumentNullException(nameof(girlPairFavQuestion)); }
            
            GirlResponceIndexOne = girlPairFavQuestion.girlResponseIndexOne;
            GirlResponceIndexTwo = girlPairFavQuestion.girlResponseIndexTwo;

            QuestionDefinitionID = girlPairFavQuestion.questionDefinition?.id ?? -1;
        }

        public GirlPairFavQuestionSubDefinition ToGirlPairFavQuestion(GameData gameData)
        {
            var newDef = new GirlPairFavQuestionSubDefinition();
            
            newDef.girlResponseIndexOne = GirlResponceIndexOne;
            newDef.girlResponseIndexTwo = GirlResponceIndexTwo;

            newDef.questionDefinition = gameData.Questions.Get(QuestionDefinitionID);

            return newDef;
        }
    }
}
