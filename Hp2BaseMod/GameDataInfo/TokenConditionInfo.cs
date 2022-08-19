// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a TokenCondition
    /// </summary>
    public class TokenConditionInfo : IGameDefinitionInfo<TokenCondition>
    {
        public TokenConditionType? Type;

        public TokenConditionTokenType? TokenType;

        public NumberComparisonType? Comparison;

        public string Val;

        public RelativeId? TokenDefinitionID;

        public bool? OppositeGirl;

        public bool? Inverse;

        /// <summary>
        /// Constructor
        /// </summary>
        public TokenConditionInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public TokenConditionInfo(TokenCondition def)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }

            Type = def.type;
            TokenType = def.tokenType;
            OppositeGirl = def.oppositeGirl;
            Comparison = def.comparison;
            Val = def.val;
            Inverse = def.inverse;

            TokenDefinitionID = new RelativeId(def.tokenDefinition);
        }

        /// <inheritdoc/>
        public void SetData(ref TokenCondition def, GameDefinitionProvider gameDataProvider, AssetProvider _, InsertStyle insertStyle)
        {
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
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewSource)
        {
            TokenDefinitionID = getNewSource(TokenDefinitionID);
        }
    }
}
