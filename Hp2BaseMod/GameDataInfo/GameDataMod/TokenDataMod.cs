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
	[UiSonGroup("Sprites", -1)]
	public class TokenDataMod : DataMod, IGameDataMod<TokenDefinition>
	{
		[UiSonTextEditUi]
		public string TokenName;

		[UiSonSelectorUi(DefaultData.TokenResourceNames_Name)]
		public string ResourceName;

		[UiSonTextEditUi]
		public string ResourceSign;

		[UiSonSelectorUi(DefaultData.PuzzleResourceTypeNullable_As_String)]
		public PuzzleResourceType? ResourceType;

		[UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String)]
		public PuzzleAffectionType? AffectionType;

		[UiSonElementSelectorUi(nameof(EnergyDataMod), 0, null, "Id", DefaultData.DefaultEnergyNames_Name, DefaultData.DefaultEnergyIds_Name)]
		public int? EnergyDefinitionID;

		[UiSonTextEditUi]
		public int? Weight;

		[UiSonTextEditUi]
		public int[] DifficultyWeightOffset;

		[UiSonTextEditUi]
		public int? BonusWeight;

		[UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
		public bool? Negative;

		[UiSonMemberElement]
		public AudioKlipInfo SfxMatchInfo;

		[UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Sprites")]
		public bool? AltBonusSprite;

		[UiSonMemberElement(0, "Sprites")]
		public SpriteInfo TokenSpriteInfo;

		[UiSonMemberElement(0, "Sprites")]
		public SpriteInfo OverSpriteInfo;

		[UiSonMemberElement(0, "Sprites")]
		public SpriteInfo AltTokenSpriteInfo;

		[UiSonMemberElement(0, "Sprites")]
		public SpriteInfo AltOverSpriteInfo;

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
			ModInterface.Instance.LogLine("Setting data for an token");
			ModInterface.Instance.IncreaseLogIndent();

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

			ModInterface.Instance.LogLine("done");
			ModInterface.Instance.DecreaseLogIndent();
		}
	}
}
