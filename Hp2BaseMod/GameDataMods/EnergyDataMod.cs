// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make an EnergyDefinition
    /// </summary>
    [Serializable]
    public class EnergyDataMod : DataMod<EnergyDefinition>
    {
        public List<SpriteInfo> BurstSprites;
		public List<SpriteInfo> TrailSprites;
		public List<SpriteInfo> SplashSprites;
		public List<SpriteInfo> SurgeSprites;
		public string TextMaterialName;
		public ColorInfo TextColorInfo;
		public ColorInfo OutlineColorInfo;
		public ColorInfo ShadowColorInfo;
		public ColorInfo SurgeColorInfo;
		public GirlExpressionType? SurgeExpression;
		public bool? SurgeEyesClosed;
		public GirlExpressionType? NegSurgeExpression;
		public bool? NegSurgeEyesClosed;
		public GirlExpressionType? BossSurgeExpression;
		public bool? BossSurgeEyesClosed;

        public EnergyDataMod() { }

        public EnergyDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
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
                             bool isAdditive = false)
            : base(id, isAdditive)
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
            : base(def.id, false)
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

        public override void SetData(EnergyDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullableSet(ref def.surgeExpression, SurgeExpression);
            Access.NullableSet(ref def.surgeEyesClosed, SurgeEyesClosed);
            Access.NullableSet(ref def.negSurgeExpression, NegSurgeExpression);
            Access.NullableSet(ref def.negSurgeEyesClosed, NegSurgeEyesClosed);
            Access.NullableSet(ref def.bossSurgeExpression, BossSurgeExpression);
            Access.NullableSet(ref def.bossSurgeEyesClosed, BossSurgeEyesClosed);

            if (IsAdditive)
            {
                if (TextMaterialName != null) { def.textMaterial = (Material)assetProvider.GetAsset(TextMaterialName); }
                if (TextColorInfo != null) { def.textColor = TextColorInfo.ToColor(); }
                if (OutlineColorInfo != null) { def.outlineColor = OutlineColorInfo.ToColor(); }
                if (ShadowColorInfo != null) { def.shadowColor = ShadowColorInfo.ToColor(); }
                if (SurgeColorInfo != null) { def.surgeColor = SurgeColorInfo.ToColor(); }
            }
            else
            {
                def.textMaterial = (Material)assetProvider.GetAsset(TextMaterialName);
                def.textColor = TextColorInfo?.ToColor() ?? Color.white;
                def.outlineColor = OutlineColorInfo?.ToColor() ?? Color.white;
                def.shadowColor = ShadowColorInfo?.ToColor() ?? Color.white;
                def.surgeColor = SurgeColorInfo?.ToColor() ?? Color.white;
            }

            SetSprites(ref def.burstSprites, BurstSprites, assetProvider);
            SetSprites(ref def.trailSprites, TrailSprites, assetProvider);
            SetSprites(ref def.splashSprites, SplashSprites, assetProvider);
            SetSprites(ref def.surgeSprites, SurgeSprites, assetProvider);
        }
    }
}
