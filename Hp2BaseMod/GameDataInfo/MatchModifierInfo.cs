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
    public class MatchModifierInfo : IGameDataInfo<MatchModifier>
    {
        [UiSonSelectorUi(DefaultData.NumberCombineOperationNullable_As_String)]
        public NumberCombineOperation? PointsOperation;

        [UiSonSelectorUi(DefaultData.NumberCombineOperationNullable_As_String)]
        public NumberCombineOperation? PointsOperation2;

        [UiSonTextEditUi]
        public float? PointsFactor;

        [UiSonTextEditUi]
        public float? PointsFactor2;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, null, "Id", DefaultData.DefaultTokenNames_Name, DefaultData.DefaultTokenIds_Name)]
        public int? TokenDefinitionID;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, null, "Id", DefaultData.DefaultTokenNames_Name, DefaultData.DefaultTokenIds_Name)]
        public int? ReplaceDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? Absorb;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? AbsorbAltGirl;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? ReplacePriority;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? SkipMostFavFactor;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? SkipLeastFavFactor;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? PointsOp;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? PointsOp2;

        public MatchModifierInfo() { }

        public MatchModifierInfo(NumberCombineOperation pointsOperation,
                                 NumberCombineOperation pointsOperation2,
                                 float pointsFactor,
                                 float pointsFactor2,
                                 int tokenDefinitionID,
                                 int replaceDefinitionID,
                                 bool absorb,
                                 bool absorbAltGirl,
                                 bool replacePriority,
                                 bool skipMostFavFactor,
                                 bool skipLeastFavFactor,
                                 bool pointsOp,
                                 bool pointsOp2)
        {
            Absorb = absorb;
            AbsorbAltGirl = absorbAltGirl;
            TokenDefinitionID = tokenDefinitionID;
            ReplaceDefinitionID = replaceDefinitionID;
            ReplacePriority = replacePriority;
            SkipMostFavFactor = skipMostFavFactor;
            SkipLeastFavFactor = skipLeastFavFactor;
            PointsOp = pointsOp;
            PointsOperation = pointsOperation;
            PointsFactor = pointsFactor;
            PointsOp2 = pointsOp2;
            PointsOperation2 = pointsOperation2;
            PointsFactor2 = pointsFactor2;
        }

        public MatchModifierInfo(MatchModifier matchModifier)
        {
            if (matchModifier == null) { return; }

            Absorb = matchModifier.absorb;
            AbsorbAltGirl = matchModifier.absorbAltGirl;
            ReplacePriority = matchModifier.replacePriority;
            SkipMostFavFactor = matchModifier.skipMostFavFactor;
            SkipLeastFavFactor = matchModifier.skipLeastFavFactor;
            PointsOp = matchModifier.pointsOp;
            PointsOperation = matchModifier.pointsOperation;
            PointsFactor = matchModifier.pointsFactor;
            PointsOp2 = matchModifier.pointsOp2;
            PointsOperation2 = matchModifier.pointsOperation2;
            PointsFactor2 = matchModifier.pointsFactor2;

            TokenDefinitionID = matchModifier.tokenDefinition?.id ?? -1;
            ReplaceDefinitionID = matchModifier.replaceDefinition?.id ?? -1;
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref MatchModifier def, GameDataProvider gameDataProvider, AssetProvider _, InsertStyle insertStyle)
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
    }
}
