// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;
using UnityEngine;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an EnergyDefinition
    /// </summary>
    [UiSonElement]
    [UiSonGroup("Text", 2)]
    [UiSonGroup("Surge", 1)]
    public class EnergyDataMod : DataMod, IGameDataMod<EnergyDefinition>
    {
        [UiSonSelectorUi(DefaultData.TextMaterialNames_Name, 0, "Text")]
        public string TextMaterialName;

        [UiSonEncapsulatingUi(0, "Text")]
        public ColorInfo TextColorInfo;

        [UiSonEncapsulatingUi(0, "Text")]
        public ColorInfo OutlineColorInfo;

        [UiSonEncapsulatingUi(0, "Text")]
        public ColorInfo ShadowColorInfo;

        [UiSonEncapsulatingUi(0, "Surge")]
        public ColorInfo SurgeColorInfo;

        [UiSonSelectorUi(DefaultData.GirlExpressionTypeNullable, 0, "Surge")]
        public GirlExpressionType? SurgeExpression;

        [UiSonSelectorUi("NullableBoolNames", 0, "Surge", "NullableBoolIds")]
        public bool? SurgeEyesClosed;
        public GirlExpressionType? NegSurgeExpression;

        [UiSonSelectorUi("NullableBoolNames", 0, "Surge", "NullableBoolIds")]
        public bool? NegSurgeEyesClosed;
        public GirlExpressionType? BossSurgeExpression;

        [UiSonSelectorUi("NullableBoolNames", 0, "Surge", "NullableBoolIds")]
        public bool? BossSurgeEyesClosed;

        [UiSonEncapsulatingUi(0, "Surge")]
        public List<SpriteInfo> SurgeSprites;

        [UiSonEncapsulatingUi]
        public List<SpriteInfo> BurstSprites;

        [UiSonEncapsulatingUi]
        public List<SpriteInfo> TrailSprites;

        [UiSonEncapsulatingUi]
        public List<SpriteInfo> SplashSprites;

        /// <inheritdoc/>
        public EnergyDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public EnergyDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        internal EnergyDataMod(EnergyDefinition def, AssetProvider assetProvider)
            : base(new RelativeId(def), InsertStyle.replace, 0)
        {
            BurstSprites = def.burstSprites?.Select(x => new SpriteInfo(x, assetProvider)).ToList();
            TrailSprites = def.trailSprites?.Select(x => new SpriteInfo(x, assetProvider)).ToList();
            SplashSprites = def.splashSprites?.Select(x => new SpriteInfo(x, assetProvider)).ToList();
            SurgeSprites = def.surgeSprites?.Select(x => new SpriteInfo(x, assetProvider)).ToList();
            assetProvider.NameAndAddAsset(ref TextMaterialName, def.textMaterial);
            SurgeExpression = def.surgeExpression;
            SurgeEyesClosed = def.surgeEyesClosed;
            NegSurgeExpression = def.negSurgeExpression;
            NegSurgeEyesClosed = def.negSurgeEyesClosed;
            BossSurgeExpression = def.bossSurgeExpression;
            BossSurgeEyesClosed = def.bossSurgeEyesClosed;

            if (def.textColor != null) { TextColorInfo = new ColorInfo(def.textColor); }
            if (def.outlineColor != null) { OutlineColorInfo = new ColorInfo(def.outlineColor); }
            if (def.shadowColor != null) { ShadowColorInfo = new ColorInfo(def.shadowColor); }
            if (def.surgeColor != null) { SurgeColorInfo = new ColorInfo(def.surgeColor); }
        }

        /// <inheritdoc/>
        public void SetData(EnergyDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider)
        {
            ValidatedSet.SetValue(ref def.surgeExpression, SurgeExpression);
            ValidatedSet.SetValue(ref def.surgeEyesClosed, SurgeEyesClosed);
            ValidatedSet.SetValue(ref def.negSurgeExpression, NegSurgeExpression);
            ValidatedSet.SetValue(ref def.negSurgeEyesClosed, NegSurgeEyesClosed);
            ValidatedSet.SetValue(ref def.bossSurgeExpression, BossSurgeExpression);
            ValidatedSet.SetValue(ref def.bossSurgeEyesClosed, BossSurgeEyesClosed);

            ValidatedSet.SetValue(ref def.textMaterial, (Material)assetProvider.GetAsset(TextMaterialName), InsertStyle);
            ValidatedSet.SetValue(ref def.textColor, TextColorInfo, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.outlineColor, OutlineColorInfo, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.shadowColor, ShadowColorInfo, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.surgeColor, SurgeColorInfo, InsertStyle, gameDataProvider, assetProvider);

            ValidatedSet.SetListValue(ref def.burstSprites, BurstSprites, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.trailSprites, TrailSprites, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.splashSprites, SplashSprites, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.surgeSprites, SurgeSprites, InsertStyle, gameDataProvider, assetProvider);
        }
    }
}
