﻿// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a PhotoDefinition
    /// </summary>
    [UiSonClass]
    public class PhotoDataMod : DataMod<PhotoDefinition>
    {
        public int? GirlPairDefinitionID;
        public int? NextPhotoDefinitionID;
        public List<SpriteInfo> BigPhotoImages;//inited
        public List<SpriteInfo> ThumbnailImages;//inited
        public bool? HasAlts;
        public string AltFlagName;
        public int? AltCodeDefinitionID;

        public PhotoDataMod() { }

        public PhotoDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
        {
        }

        public PhotoDataMod(int id,
                            int? girlPairDefinitionID,
                            int? nextPhotoDefinitionID,
                            List<SpriteInfo> bigPhotoImages,
                            List<SpriteInfo> thumbnailImages,
                            bool? hasAlts,
                            string altFlagName,
                            int? altCodeDefinitionID,
                            bool isAdditive = false)
            : base(id, isAdditive)
        {
            GirlPairDefinitionID = girlPairDefinitionID;
            NextPhotoDefinitionID = nextPhotoDefinitionID;
            BigPhotoImages = bigPhotoImages;
            ThumbnailImages = thumbnailImages;
            HasAlts = hasAlts;
            AltFlagName = altFlagName;
            AltCodeDefinitionID = altCodeDefinitionID;
        }

        public PhotoDataMod(PhotoDefinition def, AssetProvider assetProvider)
            : base(def.id, false)
        {
            HasAlts = def.hasAlts;
            AltFlagName = def.altFlagName;

            if (def.bigPhotoImages != null) { BigPhotoImages = def.bigPhotoImages.Select(x => new SpriteInfo(x, assetProvider)).ToList(); }
            if (def.thumbnailImages != null) { ThumbnailImages = def.thumbnailImages.Select(x => new SpriteInfo(x, assetProvider)).ToList(); }

            AltCodeDefinitionID = def.altCodeDefinition?.id ?? -1;
            GirlPairDefinitionID = def.girlPairDefinition?.id ?? -1;
            NextPhotoDefinitionID = def.nextPhotoDefinition?.id ?? -1;
        }

        public override void SetData(PhotoDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullableSet(ref def.hasAlts, HasAlts);

            if (GirlPairDefinitionID.HasValue) { def.girlPairDefinition = gameData.GirlPairs.Get(GirlPairDefinitionID.Value); }
            if (NextPhotoDefinitionID.HasValue) { def.nextPhotoDefinition = gameData.Photos.Get(NextPhotoDefinitionID.Value); }
            if (AltCodeDefinitionID.HasValue) { def.altCodeDefinition = gameData.Codes.Get(AltCodeDefinitionID.Value); }

            Access.NullSet(ref def.altFlagName, AltFlagName);

            SetSprites(ref def.bigPhotoImages, BigPhotoImages, assetProvider);
            SetSprites(ref def.thumbnailImages, ThumbnailImages, assetProvider);
        }
    }
}
