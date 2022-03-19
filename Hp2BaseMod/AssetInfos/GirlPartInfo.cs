// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.ModLoader;
using System;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a GirlPartSubDefinition
    /// </summary>
    [Serializable]
    public class GirlPartInfo
    {
        public GirlPartType PartType;
        public string PartName;
        public int X;
        public int Y;
        public int MirroredPartIndex = -1;
        public int AltPartIndex = -1;

        public SpriteInfo SpriteInfo;

        public GirlPartInfo() { }

        public GirlPartInfo(GirlPartType partType,
                            string partName,
                            int x,
                            int y,
                            int mirroredPartIndex,
                            int altPartIndex,
                            SpriteInfo spriteInfo)
        {
            PartType = partType;
            PartName = partName;
            SpriteInfo = spriteInfo;
            X = x;
            Y = y;
            MirroredPartIndex = mirroredPartIndex;
            AltPartIndex = altPartIndex;
        }

        public GirlPartInfo(GirlPartSubDefinition girlPartSubDef, AssetProvider assetProvider, int GirlId)
        {
            if (girlPartSubDef == null) { throw new ArgumentNullException(nameof(girlPartSubDef)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            PartType = girlPartSubDef.partType;
            PartName = girlPartSubDef.partName;
            X = girlPartSubDef.x;
            Y = girlPartSubDef.y;
            MirroredPartIndex = girlPartSubDef.mirroredPartIndex;
            AltPartIndex = girlPartSubDef.altPartIndex;

            // Special handling, prefixes id because all the part sprites have the same name...
            if (girlPartSubDef.sprite != null)
            {
                SpriteInfo = new SpriteInfo(GirlId.ToString() + "_" + girlPartSubDef.sprite.name, false);
                assetProvider.AddAsset(SpriteInfo.Path, girlPartSubDef.sprite);
            }
        }

        public GirlPartSubDefinition ToGirlPart(AssetProvider assetProvider)
        {
            var newDef = new GirlPartSubDefinition();

            newDef.partType = PartType;
            newDef.partName = PartName;
            newDef.x = X;
            newDef.y = Y;
            newDef.mirroredPartIndex = MirroredPartIndex;
            newDef.altPartIndex = AltPartIndex;

            if (SpriteInfo != null) { newDef.sprite = SpriteInfo?.ToSprite(assetProvider); }

            return newDef;
        }
    }
}
