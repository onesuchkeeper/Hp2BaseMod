// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a MatchModifier
    /// </summary>
    public class MatchModifierInfo : IGameDefinitionInfo<MatchModifier>
    {
        [UiSonSelectorUi(DefaultData.NumberCombineOperationNullable)]
        public NumberCombineOperation? PointsOperation;

        [UiSonSelectorUi(DefaultData.NumberCombineOperationNullable)]
        public NumberCombineOperation? PointsOperation2;

        [UiSonTextEditUi]
        public float? PointsFactor;

        [UiSonTextEditUi]
        public float? PointsFactor2;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, null, "id", DefaultData.DefaultTokenNames_Name, DefaultData.DefaultTokenIds_Name)]
        public RelativeId? TokenDefinitionID;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, null, "id", DefaultData.DefaultTokenNames_Name, DefaultData.DefaultTokenIds_Name)]
        public RelativeId? ReplaceDefinitionID;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? Absorb;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? AbsorbAltGirl;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? ReplacePriority;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? SkipMostFavFactor;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? SkipLeastFavFactor;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? PointsOp;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? PointsOp2;

        /// <summary>
        /// Constructor
        /// </summary>
        public MatchModifierInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public MatchModifierInfo(MatchModifier def)
        {
            if (def == null) { return; }

            Absorb = def.absorb;
            AbsorbAltGirl = def.absorbAltGirl;
            ReplacePriority = def.replacePriority;
            SkipMostFavFactor = def.skipMostFavFactor;
            SkipLeastFavFactor = def.skipLeastFavFactor;
            PointsOp = def.pointsOp;
            PointsOperation = def.pointsOperation;
            PointsFactor = def.pointsFactor;
            PointsOp2 = def.pointsOp2;
            PointsOperation2 = def.pointsOperation2;
            PointsFactor2 = def.pointsFactor2;

            TokenDefinitionID = new RelativeId(def.tokenDefinition);
            ReplaceDefinitionID = new RelativeId(def.replaceDefinition);
        }

        /// <inheritdoc/>
        public void SetData(ref MatchModifier def, GameDefinitionProvider gameDataProvider, AssetProvider _, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<MatchModifier>();
            }

            ValidatedSet.SetValue(ref def.absorb, Absorb);
            ValidatedSet.SetValue(ref def.absorbAltGirl, AbsorbAltGirl);
            ValidatedSet.SetValue(ref def.replacePriority, ReplacePriority);
            ValidatedSet.SetValue(ref def.skipMostFavFactor, SkipMostFavFactor);
            ValidatedSet.SetValue(ref def.skipLeastFavFactor, SkipLeastFavFactor);
            ValidatedSet.SetValue(ref def.pointsOp, PointsOp);
            ValidatedSet.SetValue(ref def.pointsOperation, PointsOperation);
            ValidatedSet.SetValue(ref def.pointsFactor, PointsFactor);
            ValidatedSet.SetValue(ref def.pointsOp2, PointsOp2);
            ValidatedSet.SetValue(ref def.pointsOperation2, PointsOperation2);
            ValidatedSet.SetValue(ref def.pointsFactor2, PointsFactor2);

            ValidatedSet.SetValue(ref def.tokenDefinition, gameDataProvider.GetToken(TokenDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.replaceDefinition, gameDataProvider.GetToken(ReplaceDefinitionID), insertStyle);
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            TokenDefinitionID = getNewId(TokenDefinitionID);
            ReplaceDefinitionID = getNewId(ReplaceDefinitionID);
        }
    }
}
