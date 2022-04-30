// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a GirlPairFavQuestionSubDefinition
    /// </summary>
    public class GirlPairFavQuestionInfo : IGameDataInfo<GirlPairFavQuestionSubDefinition>
    {
        [UiSonElementSelectorUi(nameof(QuestionDataMod), 0, null, "Id", DefaultData.DefaultQuestionNames, DefaultData.DefaultQuestionIds)]
        public int? QuestionDefinitionID;

        [UiSonTextEditUi]
        public int? GirlResponceIndexOne;

        [UiSonTextEditUi]
        public int? GirlResponceIndexTwo;

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

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref GirlPairFavQuestionSubDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<GirlPairFavQuestionSubDefinition>();
            }

            ValidatedSet.SetValue(ref def.girlResponseIndexOne, GirlResponceIndexOne);
            ValidatedSet.SetValue(ref def.girlResponseIndexTwo, GirlResponceIndexTwo);

            ValidatedSet.SetValue(ref def.questionDefinition, gameDataProvider.GetQuestion(QuestionDefinitionID), insertStyle);
        }
    }
}
