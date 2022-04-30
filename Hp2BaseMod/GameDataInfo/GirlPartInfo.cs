// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a GirlPartSubDefinition
    /// </summary>
    public class GirlPartInfo : IGameDataInfo<GirlPartSubDefinition>
    {
        [UiSonSelectorUi(DefaultData.GirlPartTypeNullable_As_String)]
        public GirlPartType? PartType;

        [UiSonTextEditUi]
        public string PartName;

        [UiSonTextEditUi]
        public int? X;

        [UiSonTextEditUi]
        public int? Y;

        [UiSonTextEditUi]
        public int? MirroredPartIndex;

        [UiSonTextEditUi]
        public int? AltPartIndex;

        [UiSonMemberElement]
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

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref GirlPartSubDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<GirlPartSubDefinition>();
            }

            ValidatedSet.SetValue(ref def.partType, PartType);
            ValidatedSet.SetValue(ref def.partName, PartName, insertStyle);
            ValidatedSet.SetValue(ref def.x, X);
            ValidatedSet.SetValue(ref def.y, Y);
            ValidatedSet.SetValue(ref def.mirroredPartIndex, MirroredPartIndex);
            ValidatedSet.SetValue(ref def.altPartIndex, AltPartIndex);

            ValidatedSet.SetValue(ref def.sprite, SpriteInfo, insertStyle, gameDataProvider, assetProvider);
        }
    }
}
