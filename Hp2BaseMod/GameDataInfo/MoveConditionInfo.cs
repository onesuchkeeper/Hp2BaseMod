// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a MoveCondition
    /// </summary>
    public class MoveConditionInfo : IGameDataInfo<MoveCondition>
    {
        [UiSonSelectorUi(DefaultData.MoveConditionTypeNullable_As_String)]
        public MoveConditionType? Type;

        [UiSonSelectorUi(DefaultData.MoveConditionTokenTypeNullable_As_String)]
        public MoveConditionTokenType? TokenType;

        [UiSonSelectorUi(DefaultData.PuzzleResourceTypeNullable_As_String)]
        public PuzzleResourceType? ResourceType;

        [UiSonSelectorUi(DefaultData.NumberComparisonTypeNullable_As_String)]
        public NumberComparisonType? Comparison;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, null, "Id", DefaultData.DefaultTokenNames_Name, DefaultData.DefaultTokenIds_Name)]
        public int? TokenDefinitionID;

        [UiSonTextEditUi]
        public int? Val;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? BoolValue;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? Inverse;

        public MoveConditionInfo() { }

        public MoveConditionInfo(MoveConditionType type,
                                 MoveConditionTokenType tokenType,
                                 PuzzleResourceType resourceType,
                                 NumberComparisonType comparison,
                                 int tokenDefinitionID,
                                 int val,
                                 bool boolValue,
                                 bool inverse)
        {
            Type = type;
            TokenType = tokenType;
            TokenDefinitionID = tokenDefinitionID;
            BoolValue = boolValue;
            ResourceType = resourceType;
            Comparison = comparison;
            Val = val;
            Inverse = inverse;
        }

        public MoveConditionInfo(MoveCondition moveCondition)
        {
            if (moveCondition == null) { throw new ArgumentNullException(nameof(moveCondition)); }

            Type = moveCondition.type;
            TokenType = moveCondition.tokenType;
            BoolValue = moveCondition.boolValue;
            ResourceType = moveCondition.resourceType;
            Comparison = moveCondition.comparison;
            Val = moveCondition.val;
            Inverse = moveCondition.inverse;

            TokenDefinitionID = moveCondition.tokenDefinition?.id ?? -1;
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref MoveCondition def, GameDataProvider gameDataProvider, AssetProvider _, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a move condition");
            ModInterface.Instance.IncreaseLogIndent();

            if (def == null)
            {
                def = Activator.CreateInstance<MoveCondition>();
            }

            ValidatedSet.SetValue(ref def.type, Type);
            ValidatedSet.SetValue(ref def.tokenType, TokenType);
            ValidatedSet.SetValue(ref def.boolValue, BoolValue);
            ValidatedSet.SetValue(ref def.resourceType, ResourceType);
            ValidatedSet.SetValue(ref def.comparison, Comparison);
            ValidatedSet.SetValue(ref def.val, Val);
            ValidatedSet.SetValue(ref def.inverse, Inverse);
            ValidatedSet.SetValue(ref def.tokenDefinition, gameDataProvider.GetToken(TokenDefinitionID), insertStyle);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
