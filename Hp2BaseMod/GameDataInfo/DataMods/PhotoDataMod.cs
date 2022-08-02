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
        [UiSonElementSelectorUi(nameof(GirlPairDataMod), 0, null, "id", DefaultData.DefaultGirlPairNames_Name, DefaultData.DefaultGirlPairIds_Name)]
        public RelativeId? GirlPairDefinitionID;

        [UiSonSelectorUi("NullableBoolNames", 0, "Alt", "NullableBoolIds")]
        public bool? HasAlts;

        [UiSonTextEditUi(0, "Alt")]
        public string AltFlagName;

        [UiSonElementSelectorUi(nameof(CodeDataMod), 0, "Alt", "id", DefaultData.DefaultCodeNames_Name, DefaultData.DefaultCodeIds_Name)]
        public RelativeId? AltCodeDefinitionID;

        [UiSonEncapsulatingUi]
        SpriteInfo BigPhotoCensored;

        [UiSonEncapsulatingUi]
        SpriteInfo BigPhotoUncensored;

        [UiSonEncapsulatingUi]
        SpriteInfo BigPhotoWet;

        [UiSonEncapsulatingUi]
        SpriteInfo ThumbnailCensored;

        [UiSonEncapsulatingUi]
        SpriteInfo ThumbnailUncensored;

        [UiSonEncapsulatingUi]
        SpriteInfo ThumbnailWet;

        /// <inheritdoc/>
        public PhotoDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public PhotoDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        internal PhotoDataMod(PhotoDefinition def, AssetProvider assetProvider)
            : base(new RelativeId(def), InsertStyle.replace, 0)
        {
            HasAlts = def.hasAlts;
            AltFlagName = def.altFlagName;

            if (def.bigPhotoImages != null)
            {
                var it = def.bigPhotoImages.GetEnumerator();
                it.MoveNext();
                BigPhotoCensored = new SpriteInfo(it.Current, assetProvider);
                it.MoveNext();
                BigPhotoUncensored = new SpriteInfo(it.Current, assetProvider);
                it.MoveNext();
                BigPhotoWet = new SpriteInfo(it.Current, assetProvider);
            }

            if (def.thumbnailImages != null)
            {
                var it = def.thumbnailImages.GetEnumerator();
                it.MoveNext();
                ThumbnailCensored = new SpriteInfo(it.Current, assetProvider);
                it.MoveNext();
                ThumbnailUncensored = new SpriteInfo(it.Current, assetProvider);
                it.MoveNext();
                ThumbnailWet = new SpriteInfo(it.Current, assetProvider);
            }

            AltCodeDefinitionID = new RelativeId(def.altCodeDefinition);
            GirlPairDefinitionID = new RelativeId(def.girlPairDefinition);
        }

        /// <inheritdoc/>
        public void SetData(PhotoDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider)
        {
            ValidatedSet.SetValue(ref def.hasAlts, HasAlts);

            ValidatedSet.SetValue(ref def.girlPairDefinition, (GirlPairDefinition)gameDataProvider.GetDefinition(GameDataType.GirlPair, GirlPairDefinitionID) , InsertStyle);
            ValidatedSet.SetValue(ref def.altCodeDefinition, (CodeDefinition)gameDataProvider.GetDefinition(GameDataType.Code, AltCodeDefinitionID), InsertStyle);

            ValidatedSet.SetValue(ref def.altFlagName, AltFlagName, InsertStyle);

            ValidatedSet.SetListValue(ref def.bigPhotoImages, new[] { BigPhotoCensored, BigPhotoUncensored, BigPhotoWet }, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.thumbnailImages, new[] { ThumbnailCensored, ThumbnailUncensored, ThumbnailWet }, InsertStyle, gameDataProvider, assetProvider);
        }
    }
}
