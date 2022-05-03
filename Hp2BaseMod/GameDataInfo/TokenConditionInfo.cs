// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a TokenCondition
    /// </summary>
    public class TokenConditionInfo : IGameDataInfo<TokenCondition>
    {
        [UiSonSelectorUi(DefaultData.TokenConditionTypeNullable_As_String)]
        public TokenConditionType? Type;

        [UiSonSelectorUi(DefaultData.TokenConditionTokenTypeNullable_As_String)]
        public TokenConditionTokenType? TokenType;

        [UiSonSelectorUi(DefaultData.NumberComparisonTypeNullable_As_String)]
        public NumberComparisonType? Comparison;

        [UiSonTextEditUi]
        public string Val;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, null, "Id", DefaultData.DefaultTokenNames_Name, DefaultData.DefaultTokenIds_Name)]
        public int? TokenDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? OppositeGirl;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? Inverse;

        public TokenConditionInfo() { }

        public TokenConditionInfo(TokenConditionType type,
                                  TokenConditionTokenType tokenType,
                                  NumberComparisonType comparison,
                                  string val,
                                  int tokenDefinitionID,
                                  bool oppositeGirl,
                                  bool inverse)
        {
            Type = type;
            TokenType = tokenType;
            TokenDefinitionID = tokenDefinitionID;
            OppositeGirl = oppositeGirl;
            Comparison = comparison;
            Val = val;
            Inverse = inverse;
        }

        public TokenConditionInfo(TokenCondition tokenCondition)
        {
            if (tokenCondition == null) { throw new ArgumentNullException(nameof(tokenCondition)); }

            Type = tokenCondition.type;
            TokenType = tokenCondition.tokenType;
            OppositeGirl = tokenCondition.oppositeGirl;
            Comparison = tokenCondition.comparison;
            Val = tokenCondition.val;
            Inverse = tokenCondition.inverse;

            TokenDefinitionID = tokenCondition.tokenDefinition?.id ?? -1;
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref TokenCondition def, GameDataProvider gameDataProvider, AssetProvider _, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a token condition");
            ModInterface.Instance.IncreaseLogIndent();

            if (def == null)
            {
                def = Activator.CreateInstance<TokenCondition>();
            }

            ValidatedSet.SetValue(ref def.type, Type);
            ValidatedSet.SetValue(ref def.tokenType, TokenType);
            ValidatedSet.SetValue(ref def.oppositeGirl, OppositeGirl);
            ValidatedSet.SetValue(ref def.comparison, Comparison);
            ValidatedSet.SetValue(ref def.val, Val, insertStyle);
            ValidatedSet.SetValue(ref def.inverse, Inverse);

            ValidatedSet.SetValue(ref def.tokenDefinition, gameDataProvider.GetToken(TokenDefinitionID), insertStyle);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
