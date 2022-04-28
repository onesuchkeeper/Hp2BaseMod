// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a MoveCondition
    /// </summary>
    public class MoveConditionInfo : IGameDataInfo<MoveCondition>
    {
        public MoveConditionType? Type;
        public MoveConditionTokenType? TokenType;
        public PuzzleResourceType? ResourceType;
        public NumberComparisonType? Comparison;
        public int? TokenDefinitionID;
        public int? Val;
        public bool? BoolValue;
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
        }
    }
}
