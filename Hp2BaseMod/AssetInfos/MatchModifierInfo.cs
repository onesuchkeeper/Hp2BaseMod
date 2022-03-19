// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a MatchModifier
    /// </summary>
    [Serializable]
    public class MatchModifierInfo
    {
        public NumberCombineOperation PointsOperation;
        public NumberCombineOperation PointsOperation2;
        public float PointsFactor;
        public float PointsFactor2;
        public int TokenDefinitionID;
        public int ReplaceDefinitionID;
        public bool Absorb;
        public bool AbsorbAltGirl;
        public bool ReplacePriority;
        public bool SkipMostFavFactor;
        public bool SkipLeastFavFactor;
        public bool PointsOp;
        public bool PointsOp2;

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

        public MatchModifier ToMatchModifier(GameData gameData)
        {
            var newMM = new MatchModifier();

            newMM.absorb = Absorb;
            newMM.absorbAltGirl = AbsorbAltGirl;
            newMM.replacePriority = ReplacePriority;
            newMM.skipMostFavFactor = SkipMostFavFactor;
            newMM.skipLeastFavFactor = SkipLeastFavFactor;
            newMM.pointsOp = PointsOp;
            newMM.pointsOperation = PointsOperation;
            newMM.pointsFactor = PointsFactor;
            newMM.pointsOp2 = PointsOp2;
            newMM.pointsOperation2 = PointsOperation2;
            newMM.pointsFactor2 = PointsFactor2;

            newMM.tokenDefinition = gameData.Tokens.Get(TokenDefinitionID);
            newMM.replaceDefinition = gameData.Tokens.Get(ReplaceDefinitionID);

            return newMM;
        }
    }
}
