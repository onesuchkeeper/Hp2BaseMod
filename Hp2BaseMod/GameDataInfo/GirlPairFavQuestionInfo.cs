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
    public class GirlPairFavQuestionInfo : IGameDefinitionInfo<GirlPairFavQuestionSubDefinition>
    {
        [UiSonElementSelectorUi(nameof(QuestionDataMod), 0, null, "id", DefaultData.DefaultQuestionNames_Name, DefaultData.DefaultQuestionIds_Name)]
        public RelativeId? QuestionDefinitionID;

        [UiSonTextEditUi]
        public int? GirlResponceIndexOne;

        [UiSonTextEditUi]
        public int? GirlResponceIndexTwo;

        /// <summary>
        /// Constructor
        /// </summary>
        public GirlPairFavQuestionInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public GirlPairFavQuestionInfo(GirlPairFavQuestionSubDefinition def)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }

            GirlResponceIndexOne = def.girlResponseIndexOne;
            GirlResponceIndexTwo = def.girlResponseIndexTwo;

            QuestionDefinitionID = new RelativeId(def.questionDefinition);
        }

        /// <inheritdoc/>
        public void SetData(ref GirlPairFavQuestionSubDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<GirlPairFavQuestionSubDefinition>();
            }

            ValidatedSet.SetValue(ref def.girlResponseIndexOne, GirlResponceIndexOne);
            ValidatedSet.SetValue(ref def.girlResponseIndexTwo, GirlResponceIndexTwo);

            ValidatedSet.SetValue(ref def.questionDefinition, gameDataProvider.GetQuestion(QuestionDefinitionID), insertStyle);
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            QuestionDefinitionID = getNewId(QuestionDefinitionID);
        }
    }
}
