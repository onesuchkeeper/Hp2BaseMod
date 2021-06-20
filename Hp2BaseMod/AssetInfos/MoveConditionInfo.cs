// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a MoveCondition
    /// </summary>
    [Serializable]
    public class MoveConditionInfo
    {
        public MoveConditionType Type;
        public MoveConditionTokenType TokenType;
        public PuzzleResourceType ResourceType;
        public NumberComparisonType Comparison;
        public int TokenDefinitionID;
        public int Val;
        public bool BoolValue;
        public bool Inverse;

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

        public MoveCondition ToMoveCondition(GameData gameData) 
        {
            var newMC = new MoveCondition();

            newMC.type = Type;
            newMC.tokenType = TokenType;
            newMC.boolValue = BoolValue;
            newMC.resourceType = ResourceType;
            newMC.comparison = Comparison;
            newMC.val = Val;
            newMC.inverse = Inverse;

            newMC.tokenDefinition = gameData.Tokens.Get(TokenDefinitionID);

            return newMC;
        }
    }
}
