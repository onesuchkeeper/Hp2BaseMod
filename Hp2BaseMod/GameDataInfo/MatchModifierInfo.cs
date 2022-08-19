// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a MatchModifier
    /// </summary>
    public class MatchModifierInfo : IGameDefinitionInfo<MatchModifier>
    {
        public NumberCombineOperation? PointsOperation;

        public NumberCombineOperation? PointsOperation2;

        public float? PointsFactor;

        public float? PointsFactor2;

        public RelativeId? TokenDefinitionID;

        public RelativeId? ReplaceDefinitionID;

        public bool? Absorb;

        public bool? AbsorbAltGirl;

        public bool? ReplacePriority;

        public bool? SkipMostFavFactor;

        public bool? SkipLeastFavFactor;

        public bool? PointsOp;

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
