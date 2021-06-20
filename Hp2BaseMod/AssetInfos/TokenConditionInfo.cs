// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a TokenCondition
    /// </summary>
    [Serializable]
    public class TokenConditionInfo
    {
        public TokenConditionType Type;
        public TokenConditionTokenType TokenType;
        public NumberComparisonType Comparison;
        public string Val;
        public int TokenDefinitionID;
        public bool OppositeGirl;
        public bool Inverse;

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

        public TokenCondition ToTokenCondition(GameData gameData)
        {
            var newTC = new TokenCondition();

            newTC.type = Type;
            newTC.tokenType = TokenType;
            newTC.oppositeGirl = OppositeGirl;
            newTC.comparison = Comparison;
            newTC.val = Val;
            newTC.inverse = Inverse;

            newTC.tokenDefinition = gameData.Tokens.Get(TokenDefinitionID);

            return newTC;
        }
    }
}
