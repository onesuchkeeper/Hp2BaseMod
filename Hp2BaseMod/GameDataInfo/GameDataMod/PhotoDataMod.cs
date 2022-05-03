// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a PhotoDefinition
    /// </summary>
    [UiSonElement]
    [UiSonGroup("Alt")]
    public class PhotoDataMod : DataMod, IGameDataMod<PhotoDefinition>
    {
        [UiSonElementSelectorUi(nameof(GirlPairDataMod), 0, null, "Id", DefaultData.DefaultGirlPairNames_Name, DefaultData.DefaultGirlPairIds_Name)]
        public int? GirlPairDefinitionID;

        [UiSonElementSelectorUi(nameof(PhotoDataMod), 0, null, "Id", DefaultData.DefaultPhotoNames_Name, DefaultData.DefaultPhotoIds_Name)]
        public int? NextPhotoDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Alt")]
        public bool? HasAlts;

        [UiSonTextEditUi(0, "Alt")]
        public string AltFlagName;

        [UiSonElementSelectorUi(nameof(CodeDataMod), 0, "Alt", "Id", DefaultData.DefaultCodeNames_Name, DefaultData.DefaultCodeIds_Name)]
        public int? AltCodeDefinitionID;

        [UiSonMemberElement]
        public List<SpriteInfo> BigPhotoImages;

        [UiSonMemberElement]
        public List<SpriteInfo> ThumbnailImages;

        public PhotoDataMod() { }

        public PhotoDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
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
                            InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
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
            : base(def.id, InsertStyle.replace, def.name)
        {
            HasAlts = def.hasAlts;
            AltFlagName = def.altFlagName;

            if (def.bigPhotoImages != null) { BigPhotoImages = def.bigPhotoImages.Select(x => new SpriteInfo(x, assetProvider)).ToList(); }
            if (def.thumbnailImages != null) { ThumbnailImages = def.thumbnailImages.Select(x => new SpriteInfo(x, assetProvider)).ToList(); }

            AltCodeDefinitionID = def.altCodeDefinition?.id ?? -1;
            GirlPairDefinitionID = def.girlPairDefinition?.id ?? -1;
            NextPhotoDefinitionID = def.nextPhotoDefinition?.id ?? -1;
        }

        public void SetData(PhotoDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            def.id = Id;

            ValidatedSet.SetValue(ref def.hasAlts, HasAlts);

            ValidatedSet.SetValue(ref def.girlPairDefinition, gameDataProvider.GetGirlPair(GirlPairDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.nextPhotoDefinition, gameDataProvider.GetPhoto(NextPhotoDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.altCodeDefinition, gameDataProvider.GetCode(AltCodeDefinitionID), insertStyle);

            ValidatedSet.SetValue(ref def.altFlagName, AltFlagName, insertStyle);

            ValidatedSet.SetListValue(ref def.bigPhotoImages, BigPhotoImages, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.thumbnailImages, ThumbnailImages, insertStyle, gameDataProvider, assetProvider);
        }
    }
}
