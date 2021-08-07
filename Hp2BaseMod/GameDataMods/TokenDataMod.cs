// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a TokenDefinition
    /// </summary>
    [Serializable]
    public class TokenDataMod : DataMod<TokenDefinition>
    {
		public string TokenName;
		public string ResourceName;
		public string ResourceSign;
		public PuzzleResourceType? ResourceType;
		public PuzzleAffectionType? AffectionType;
		public int? Weight;
		public int[] DifficultyWeightOffset;
		public int? BonusWeight;
		public bool? Negative;
		public bool? AltBonusSprite;
		public SpriteInfo TokenSpriteInfo;
		public SpriteInfo OverSpriteInfo;
		public SpriteInfo AltTokenSpriteInfo;
		public SpriteInfo AltOverSpriteInfo;
		public int? EnergyDefinitionID;
		public AudioKlipInfo SfxMatchInfo;

		public TokenDataMod() { }

		public TokenDataMod(int id, bool isAdditive)
			: base(id, isAdditive)
		{
		}

		public TokenDataMod(int id,
							string tokenName,
							string resourceName,
							string resourceSign,
							PuzzleResourceType? resourceType,
							PuzzleAffectionType? affectionType,
							int? weight,
							int[] difficultyWeightOffset,
							int? bonusWeight,
							bool? negative,
							bool?altBonusSprite,
							SpriteInfo tokenSpriteInfo,
							SpriteInfo overSpriteInfo,
							SpriteInfo altTokenSpriteInfo,
							SpriteInfo altOverSpriteInfo,
							int? energyDefinitionID,
							AudioKlipInfo sfxMatchInfo,
							bool isAdditive = false)
			: base(id, isAdditive)
		{
			TokenName = tokenName;
			ResourceName = resourceName;
			ResourceSign = resourceSign;
			ResourceType = resourceType;
			AffectionType = affectionType;
			Weight = weight;
			DifficultyWeightOffset = difficultyWeightOffset;
			BonusWeight = bonusWeight;
			Negative = negative;
			AltBonusSprite = altBonusSprite;
			TokenSpriteInfo = tokenSpriteInfo;
			OverSpriteInfo = overSpriteInfo;
			AltTokenSpriteInfo = altTokenSpriteInfo;
			AltOverSpriteInfo = altOverSpriteInfo;
			EnergyDefinitionID = energyDefinitionID;
			SfxMatchInfo = sfxMatchInfo;
		}

		public TokenDataMod(TokenDefinition def, AssetProvider assetProvider)
			: base(def.id, false)
		{
			TokenName = def.tokenName;
			ResourceName = def.resourceName;
			ResourceSign = def.resourceSign;
			ResourceType = def.resourceType;
			AffectionType = def.affectionType;
			Weight = def.weight;
			DifficultyWeightOffset = def.difficultyWeightOffset;
			BonusWeight = def.bonusWeight;
			Negative = def.negative;
			AltBonusSprite = def.altBonusSprite;

			if (def.tokenSprite != null) { TokenSpriteInfo = new SpriteInfo(def.tokenSprite, assetProvider); }
			if (def.overSprite != null) { OverSpriteInfo = new SpriteInfo(def.overSprite, assetProvider); }
			if (def.altTokenSprite != null) { AltTokenSpriteInfo = new SpriteInfo(def.altTokenSprite, assetProvider); }
			if (def.altOverSprite != null) { AltOverSpriteInfo = new SpriteInfo(def.altOverSprite, assetProvider); }
			if (def.sfxMatch != null) { SfxMatchInfo = new AudioKlipInfo(def.sfxMatch, assetProvider); }

			EnergyDefinitionID = def.energyDefinition?.id ?? -1;
		}

		public override void SetData(TokenDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

			Access.NullableSet(ref def.resourceType, ResourceType);
			Access.NullableSet(ref def.affectionType, AffectionType);
			Access.NullableSet(ref def.weight, Weight);
			Access.NullableSet(ref def.bonusWeight, BonusWeight);
			Access.NullableSet(ref def.negative, Negative);
			Access.NullableSet(ref def.altBonusSprite, AltBonusSprite);
			if (EnergyDefinitionID.HasValue) { def.energyDefinition = gameData.Energy.Get(EnergyDefinitionID.Value); }

			Access.NullSet(ref def.tokenName, TokenName);
			Access.NullSet(ref def.resourceName, ResourceName);
			Access.NullSet(ref def.resourceSign, ResourceSign);
			Access.NullSet(ref def.difficultyWeightOffset, DifficultyWeightOffset);

			if (TokenSpriteInfo != null) { def.tokenSprite = TokenSpriteInfo.ToSprite(assetProvider); }
			if (OverSpriteInfo != null) { def.overSprite = OverSpriteInfo.ToSprite(assetProvider); }
			if (AltTokenSpriteInfo != null) { def.altTokenSprite = AltTokenSpriteInfo.ToSprite(assetProvider); }
			if (AltOverSpriteInfo != null) { def.altOverSprite = AltOverSpriteInfo.ToSprite(assetProvider); }
			if (SfxMatchInfo != null) { def.sfxMatch = SfxMatchInfo.ToAudioKlip(assetProvider); }
		}
    }
}
