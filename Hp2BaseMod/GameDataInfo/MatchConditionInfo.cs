// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a MatchCondition
    /// </summary>
    public class MatchConditionInfo : IGameDefinitionInfo<MatchCondition>
    {
        [UiSonSelectorUi(DefaultData.MatchConditionTypeNullable)]
        public MatchConditionType? Type;

        [UiSonSelectorUi(DefaultData.MatchConditionTypeNullable)]
        public MatchConditionTokenType? TokenType;

        [UiSonSelectorUi(DefaultData.PuzzleResourceTypeNullable)]
        public PuzzleResourceType? ResourceType;

        [UiSonSelectorUi(DefaultData.NumberComparisonTypeNullable)]
        public NumberComparisonType? Comparison;

        [UiSonTextEditUi]
        public int? Val;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, null, "id", DefaultData.DefaultTokenNames_Name, DefaultData.DefaultTokenIds_Name)]
        public RelativeId? TokenDefinitionID;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? BoolValue;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? Inverse;

        /// <summary>
        /// Constructor
        /// </summary>
        public MatchConditionInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public MatchConditionInfo(MatchCondition def)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }

            Type = def.type;
            TokenType = def.tokenType;
            BoolValue = def.boolValue;
            ResourceType = def.resourceType;
            Comparison = def.comparison;
            Val = def.val;
            Inverse = def.inverse;

            TokenDefinitionID = new RelativeId(def.tokenDefinition);
        }

        /// <inheritdoc/>
        public void SetData(ref MatchCondition def, GameDefinitionProvider gameDataProvider, AssetProvider _, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<MatchCondition>();
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

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            TokenDefinitionID = getNewId(TokenDefinitionID);
        }
    }
}
