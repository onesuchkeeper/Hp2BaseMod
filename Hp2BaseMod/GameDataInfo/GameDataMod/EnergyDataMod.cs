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

        [UiSonMemberElement(0, "Text")]
        public ColorInfo TextColorInfo;

        [UiSonMemberElement(0, "Text")]
        public ColorInfo OutlineColorInfo;

        [UiSonMemberElement(0, "Text")]
        public ColorInfo ShadowColorInfo;

        [UiSonMemberElement(0, "Surge")]
        public ColorInfo SurgeColorInfo;

        [UiSonSelectorUi(DefaultData.GirlExpressionTypeNullable_As_String, 0, "Surge")]
        public GirlExpressionType? SurgeExpression;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Surge")]
        public bool? SurgeEyesClosed;
        public GirlExpressionType? NegSurgeExpression;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Surge")]
        public bool? NegSurgeEyesClosed;
        public GirlExpressionType? BossSurgeExpression;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Surge")]
        public bool? BossSurgeEyesClosed;

        [UiSonMemberElement(0, "Surge")]
        public List<SpriteInfo> SurgeSprites;

        [UiSonMemberElement]
        public List<SpriteInfo> BurstSprites;

        [UiSonMemberElement]
        public List<SpriteInfo> TrailSprites;

        [UiSonMemberElement]
        public List<SpriteInfo> SplashSprites;

        public EnergyDataMod() { }

        public EnergyDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
        }

        public EnergyDataMod(int id,
                             List<SpriteInfo> burstSprites,
                             List<SpriteInfo> trailSprites,
                             List<SpriteInfo> splashSprites,
                             List<SpriteInfo> surgeSprites,
                             string textMaterialName,
                             ColorInfo textColorInfo,
                             ColorInfo outlineColorInfo,
                             ColorInfo shadowColorInfo,
                             ColorInfo surgeColorInfo,
                             GirlExpressionType? surgeExpression,
                             bool? surgeEyesClosed,
                             GirlExpressionType? negSurgeExpression,
                             bool? negSurgeEyesClosed,
                             GirlExpressionType? bossSurgeExpression,
                             bool? bossSurgeEyesClosed,
                             InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
            BurstSprites = burstSprites;
            TrailSprites = trailSprites;
            SplashSprites = splashSprites;
            SurgeSprites = surgeSprites;
            TextMaterialName = textMaterialName;
            TextColorInfo = textColorInfo;
            OutlineColorInfo = outlineColorInfo;
            ShadowColorInfo = shadowColorInfo;
            SurgeColorInfo = surgeColorInfo;
            SurgeExpression = surgeExpression;
            SurgeEyesClosed = surgeEyesClosed;
            NegSurgeExpression = negSurgeExpression;
            NegSurgeEyesClosed = negSurgeEyesClosed;
            BossSurgeExpression = bossSurgeExpression;
            BossSurgeEyesClosed = bossSurgeEyesClosed;
        }

        public EnergyDataMod(EnergyDefinition def, AssetProvider assetProvider)
            : base(def.id, InsertStyle.replace, def.name)
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

        public void SetData(EnergyDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for an energy");
            ModInterface.Instance.IncreaseLogIndent();

            def.id = Id;

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

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
