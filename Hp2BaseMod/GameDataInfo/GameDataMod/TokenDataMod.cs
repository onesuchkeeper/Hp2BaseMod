// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
	/// <summary>
	/// Serializable information to make a TokenDefinition
	/// </summary>
	[UiSonElement]
	public class TokenDataMod : DataMod, IGameDataMod<TokenDefinition>
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

		public TokenDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
			: base(id, insertStyle)
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
							bool? altBonusSprite,
							SpriteInfo tokenSpriteInfo,
							SpriteInfo overSpriteInfo,
							SpriteInfo altTokenSpriteInfo,
							SpriteInfo altOverSpriteInfo,
							int? energyDefinitionID,
							AudioKlipInfo sfxMatchInfo,
							InsertStyle insertStyle = InsertStyle.replace)
			: base(id, insertStyle)
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
			: base(def.id, InsertStyle.replace, def.name)
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

		public void SetData(TokenDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
		{
			def.id = Id;

			ValidatedSet.SetValue(ref def.resourceType, ResourceType);
			ValidatedSet.SetValue(ref def.affectionType, AffectionType);
			ValidatedSet.SetValue(ref def.weight, Weight);
			ValidatedSet.SetValue(ref def.bonusWeight, BonusWeight);
			ValidatedSet.SetValue(ref def.negative, Negative);
			ValidatedSet.SetValue(ref def.altBonusSprite, AltBonusSprite);
			ValidatedSet.SetValue(ref def.energyDefinition, gameDataProvider.GetEnergy(EnergyDefinitionID), insertStyle);

			ValidatedSet.SetValue(ref def.tokenName, TokenName, insertStyle);
			ValidatedSet.SetValue(ref def.resourceName, ResourceName, insertStyle);
			ValidatedSet.SetValue(ref def.resourceSign, ResourceSign, insertStyle);
			ValidatedSet.SetValue(ref def.difficultyWeightOffset, DifficultyWeightOffset, insertStyle);

			ValidatedSet.SetValue(ref def.tokenSprite, TokenSpriteInfo, insertStyle, gameDataProvider, assetProvider);
			ValidatedSet.SetValue(ref def.overSprite, OverSpriteInfo, insertStyle, gameDataProvider, assetProvider);
			ValidatedSet.SetValue(ref def.altTokenSprite, AltTokenSpriteInfo, insertStyle, gameDataProvider, assetProvider);
			ValidatedSet.SetValue(ref def.altOverSprite, AltOverSpriteInfo, insertStyle, gameDataProvider, assetProvider);
			ValidatedSet.SetValue(ref def.sfxMatch, SfxMatchInfo, insertStyle, gameDataProvider, assetProvider);
		}
	}
}
