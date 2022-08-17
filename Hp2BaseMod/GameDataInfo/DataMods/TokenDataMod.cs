// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
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

        [UiSonSelectorUi(DefaultData.PuzzleResourceTypeNullable)]
        public PuzzleResourceType? ResourceType;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable)]
        public PuzzleAffectionType? AffectionType;

        [UiSonElementSelectorUi(nameof(EnergyDataMod), 0, null, "id", DefaultData.DefaultEnergyNames_Name, DefaultData.DefaultEnergyIds_Name)]
        public RelativeId? EnergyDefinitionID;

        [UiSonTextEditUi]
        public int? Weight;

        [UiSonTextEditUi]
        public int[] DifficultyWeightOffset;

        [UiSonTextEditUi]
        public int? BonusWeight;

        [UiSonSelectorUi("NullableBoolNames")]
        public bool? Negative;

        [UiSonEncapsulatingUi]
        public AudioKlipInfo SfxMatchInfo;

        [UiSonSelectorUi("NullableBoolNames", 0, "Sprites", "NullableBoolIds")]
        public bool? AltBonusSprite;

        [UiSonEncapsulatingUi(0, "Sprites")]
        public SpriteInfo TokenSpriteInfo;

        [UiSonEncapsulatingUi(0, "Sprites")]
        public SpriteInfo OverSpriteInfo;

        [UiSonEncapsulatingUi(0, "Sprites")]
        public SpriteInfo AltTokenSpriteInfo;

        [UiSonEncapsulatingUi(0, "Sprites")]
        public SpriteInfo AltOverSpriteInfo;

        /// <inheritdoc/>
        public TokenDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public TokenDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        internal TokenDataMod(TokenDefinition def, AssetProvider assetProvider)
            : base(new RelativeId(def), InsertStyle.replace, 0)
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

            EnergyDefinitionID = new RelativeId(def.energyDefinition);
        }

        /// <inheritdoc/>
        public void SetData(TokenDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider)
        {
            ValidatedSet.SetValue(ref def.resourceType, ResourceType);
            ValidatedSet.SetValue(ref def.affectionType, AffectionType);
            ValidatedSet.SetValue(ref def.weight, Weight);
            ValidatedSet.SetValue(ref def.bonusWeight, BonusWeight);
            ValidatedSet.SetValue(ref def.negative, Negative);
            ValidatedSet.SetValue(ref def.altBonusSprite, AltBonusSprite);
            ValidatedSet.SetValue(ref def.energyDefinition, gameDataProvider.GetEnergy(EnergyDefinitionID), InsertStyle);

            ValidatedSet.SetValue(ref def.tokenName, TokenName, InsertStyle);
            ValidatedSet.SetValue(ref def.resourceName, ResourceName, InsertStyle);
            ValidatedSet.SetValue(ref def.resourceSign, ResourceSign, InsertStyle);
            ValidatedSet.SetValue(ref def.difficultyWeightOffset, DifficultyWeightOffset, InsertStyle);

            ValidatedSet.SetValue(ref def.tokenSprite, TokenSpriteInfo, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.overSprite, OverSpriteInfo, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.altTokenSprite, AltTokenSpriteInfo, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.altOverSprite, AltOverSpriteInfo, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.sfxMatch, SfxMatchInfo, InsertStyle, gameDataProvider, assetProvider);
        }

        /// <inheritdoc/>
        public override void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            Id = getNewId(Id) ?? Id;
            EnergyDefinitionID = getNewId(EnergyDefinitionID);
        }
    }
}
