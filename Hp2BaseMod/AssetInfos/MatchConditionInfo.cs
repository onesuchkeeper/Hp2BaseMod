// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a MatchCondition
    /// </summary>
    [Serializable]
    public class MatchConditionInfo
    {
        public MatchConditionType Type;
        public MatchConditionTokenType TokenType;
        public PuzzleResourceType ResourceType;
        public NumberComparisonType Comparison;
        public int Val;
        public int TokenDefinitionID;
        public bool BoolValue;
        public bool Inverse;

        public MatchConditionInfo() { }

        public MatchConditionInfo(MatchConditionType type,
                                  MatchConditionTokenType tokenType,
                                  PuzzleResourceType resourceType,
                                  NumberComparisonType comparison,
                                  int val,
                                  int tokenDefinitionID,
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

        public MatchConditionInfo(MatchCondition matchCondition)
        {
            if (matchCondition == null) { throw new ArgumentNullException(nameof(matchCondition)); }

            Type = matchCondition.type;
            TokenType = matchCondition.tokenType;
            BoolValue = matchCondition.boolValue;
            ResourceType = matchCondition.resourceType;
            Comparison = matchCondition.comparison;
            Val = matchCondition.val;
            Inverse = matchCondition.inverse;

            TokenDefinitionID = matchCondition.tokenDefinition?.id ?? -1;
        }

        public MatchCondition ToMatchCondition(GameData gameData)
        {
            var newMC = new MatchCondition();

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
