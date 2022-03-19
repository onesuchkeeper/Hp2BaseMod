// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;
using UnityEngine;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a GirlDefinition
    /// </summary>
    [UiSonClass]
    [UiSonGroup("Girl Info")]
    [UiSonGroup("Items")]
    [UiSonGroup("Questions")]
    [UiSonGroup("Sprites")]
    public class GirlDataMod : DataMod<GirlDefinition>
    {
        public EditorGirlDefinitionTab? EditorTab;
        public EditorDialogTriggerTab? DialogTriggerTab;

        #region Girl Info

        [UiSonTextEditUi(0, "Girl Info")]
        public string GirlName;

        [UiSonTextEditUi(0, "Girl Info")]
        public string GirlNickName;

        [UiSonTextEditUi(0, "Girl Info")]
        public int? GirlAge;

        [UiSonSelectorUi(new string[] { "null", "True", "False" }, 0, "Girl Info")]
        public bool? SpecialCharacter;

        [UiSonSelectorUi(new string[] { "null", "True", "False" }, 0, "Girl Info")]
        public bool? BossCharacter;

        [UiSonEnumSelectorUi(typeof(PuzzleAffectionType?), 0, "Girl Info")]
        public PuzzleAffectionType? FavoriteAffectionType;

        [UiSonEnumSelectorUi(typeof(PuzzleAffectionType?), 0, "Girl Info")]
        public PuzzleAffectionType? LeastFavoriteAffectionType;

        [UiSonEnumSelectorUi(typeof(ItemShoesType?), 0, "Girl Info")]
        public ItemShoesType? ShoesType;

        [UiSonTextEditUi(0, "Girl Info")]
        public string ShoesAdj;

        [UiSonEnumSelectorUi(typeof(ItemUniqueType), 0, "Girl Info")]
        public ItemUniqueType? UniqueType;

        [UiSonTextEditUi(0, "Girl Info")]
        public string UniqueAdj;

        [UiSonTextEditUi(0, "Girl Info")]
        public float? VoiceVolume;

        [UiSonTextEditUi(0, "Girl Info")]
        public float? SexVoiceVolume;

        [UiSonCollection]
        [UiSonEnumSelectorUi(typeof(ItemFoodType), 0, "Girl Info")]
        public List<ItemFoodType> BadFoodTypes;

        #endregion

        #region Items
        //I'll have to add in all the extra options

        [UiSonCollection]
        [UiSonClassSelectorUi(nameof(GirlPairDataMod), 0, "Items", "Id")]
        public List<int> GirlPairDefIDs;

        [UiSonCollection]
        [UiSonClassSelectorUi(nameof(AilmentDataMod), 0, "Items", "Id")]
        public List<int> BaggageItemDefIDs;

        [UiSonCollection]
        [UiSonClassSelectorUi(nameof(ItemDataMod), 0, "Items", "Id")]
        public List<int> UniqueItemDefIDs;

        [UiSonCollection]
        [UiSonClassSelectorUi(nameof(GirlPairDataMod), 0, "Items", "Id")]
        public List<int> ShoesItemDefIDs;

        #endregion


        #region Questions

        [UiSonCollection]
        [UiSonMemberClass]
        public List<GirlQuestionSubDefinition> HerQuestions;

        [UiSonCollection]
        [UiSonTextEditUi]
        public List<int> FavAnswers;

        #endregion

        #region Sprites

        [UiSonMemberClass(0, "Sprites")]
        public SpriteInfo CellphonePortrait;

        [UiSonMemberClass(0, "Sprites")]
        public SpriteInfo CellphonePortraitAlt;

        [UiSonMemberClass(0, "Sprites")]
        public SpriteInfo CellphoneHead;

        [UiSonMemberClass(0, "Sprites")]
        public SpriteInfo CellphoneHeadAlt;

        [UiSonMemberClass(0, "Sprites")]
        public SpriteInfo CellphoneMiniHead;

        [UiSonMemberClass(0, "Sprites")]
        public SpriteInfo CellphoneMiniHeadAlt;

        [UiSonMemberClass(0, "Sprites")]
        public VectorInfo BreathEmitterPos;

        [UiSonMemberClass(0, "Sprites")]
        public VectorInfo UpsetEmitterPos;

        [UiSonTextEditUi(0, "Sprites")]
        public string SpecialEffectName;

        [UiSonMemberClass(0, "Sprites")]
        public VectorInfo SpecialEffectOffset;

        [UiSonSelectorUi(new string[] { "null", "True", "False" }, 0, "Sprites")]
        public bool? HasAltStyles;

        [UiSonTextEditUi(0, "Sprites")]
        public string AltStylesFlagName;

        [UiSonClassSelectorUi(nameof(CodeDataMod), 0, "Sprites", "Id")]
        public int? AltStylesCodeDefinitionID;

        [UiSonClassSelectorUi(nameof(CodeDataMod), 0, "Sprites", "Id")]
        public int? UnlockStyleCodeDefinitionID;

        [UiSonTextEditUi(0, "Sprites")]
        public int? PartIndexBody;

        [UiSonTextEditUi(0, "Sprites")]
        public int? PartIndexNipples;

        [UiSonTextEditUi(0, "Sprites")]
        public int? PartIndexBlushLight;

        [UiSonTextEditUi(0, "Sprites")]
        public int? PartIndexBlushHeavy;

        [UiSonTextEditUi(0, "Sprites")]
        public int? PartIndexBlink;

        [UiSonTextEditUi(0, "Sprites")]
        public int? PartIndexMouthNeutral;

        [UiSonTextEditUi(0, "Sprites")]
        public List<int> PartIndexesPhonemes;

        [UiSonCollection]
        [UiSonTextEditUi]
        public List<int> PartIndexesPhonemesTeeth;

        [UiSonCollection]
        [UiSonMemberClass]
        public List<GirlPartInfo> Parts;

        [UiSonTextEditUi(0, "Sprites")]
        public int? DefaultExpressionIndex;

        [UiSonTextEditUi(0, "Sprites")]
        public int? FailureExpressionIndex;

        [UiSonTextEditUi(0, "Sprites")]
        public int? DefaultHairstyleIndex;

        [UiSonTextEditUi(0, "Sprites")]
        public int? DefaultOutfitIndex;

        [UiSonCollection]
        [UiSonMemberClass]
        public List<GirlExpressionSubDefinition> Expressions;

        [UiSonCollection]
        [UiSonMemberClass]
        public List<GirlHairstyleSubDefinition> Hairstyles;

        [UiSonCollection]
        [UiSonMemberClass]
        public List<GirlOutfitSubDefinition> Outfits;

        [UiSonCollection]
        [UiSonMemberClass]
        public List<GirlSpecialPartSubDefinition> SpecialParts;

        #endregion

        public GirlDataMod() { }

        public GirlDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
        {
        }

        public GirlDataMod(int id,
                           EditorGirlDefinitionTab? editorTab,
                           string girlName,
                           string girlNickName,
                           int? girlAge,
                           EditorDialogTriggerTab? dialogTriggerTab,
                           bool? specialCharacter,
                           bool? bossCharacter,
                           PuzzleAffectionType? favoriteAffectionType,
                           PuzzleAffectionType? leastFavoriteAffectionType,
                           float? voiceVolume,
                           float? sexVoiceVolume,
                           SpriteInfo cellphonePortrait,
                           SpriteInfo cellphonePortraitAlt,
                           SpriteInfo cellphoneHead,
                           SpriteInfo cellphoneHeadAlt,
                           SpriteInfo cellphoneMiniHead,
                           SpriteInfo cellphoneMiniHeadAlt,
                           VectorInfo breathEmitterPos,
                           VectorInfo upsetEmitterPos,
                           string specialEffectName,
                           VectorInfo specialEffectOffset,
                           ItemShoesType? shoesType,
                           string shoesAdj,
                           ItemUniqueType? uniqueType,
                           string uniqueAdj,
                           List<ItemFoodType> badFoodTypes,
                           List<int> girlPairDefIDs,
                           List<int> baggageItemDefIDs,
                           List<int> uniqueItemDefIDs,
                           List<int> shoesItemDefIDs,
                           bool? hasAltStyles,
                           string altStylesFlagName,
                           int? altStylesCodeDefinitionID,
                           int? unlockStyleCodeDefinitionID,
                           int? partIndexBody,
                           int? partIndexNipples,
                           int? partIndexBlushLight,
                           int? partIndexBlushHeavy,
                           int? partIndexBlink,
                           int? partIndexMouthNeutral,
                           List<int> partIndexesPhonemes,
                           List<int> partIndexesPhonemesTeeth,
                           List<GirlPartInfo> parts,
                           int? defaultExpressionIndex,
                           int? failureExpressionIndex,
                           int? defaultHairstyleIndex,
                           int? defaultOutfitIndex,
                           List<GirlExpressionSubDefinition> expressions,
                           List<GirlHairstyleSubDefinition> hairstyles,
                           List<GirlOutfitSubDefinition> outfits,
                           List<GirlSpecialPartSubDefinition> specialParts,
                           List<GirlQuestionSubDefinition> herQuestions,
                           List<int> favAnswers,
                           bool isAdditive = false)
            : base(id, isAdditive)
        {
            EditorTab = editorTab;
            GirlName = girlName;
            GirlNickName = girlNickName;
            GirlAge = girlAge;
            DialogTriggerTab = dialogTriggerTab;
            SpecialCharacter = specialCharacter;
            BossCharacter = bossCharacter;
            FavoriteAffectionType = favoriteAffectionType;
            LeastFavoriteAffectionType = leastFavoriteAffectionType;
            VoiceVolume = voiceVolume;
            SexVoiceVolume = sexVoiceVolume;
            CellphonePortrait = cellphonePortrait;
            CellphonePortraitAlt = cellphonePortraitAlt;
            CellphoneHead = cellphoneHead;
            CellphoneHeadAlt = cellphoneHeadAlt;
            CellphoneMiniHead = cellphoneMiniHead;
            CellphoneMiniHeadAlt = cellphoneMiniHeadAlt;
            BreathEmitterPos = breathEmitterPos;
            UpsetEmitterPos = upsetEmitterPos;
            SpecialEffectName = specialEffectName;
            SpecialEffectOffset = specialEffectOffset;
            ShoesType = shoesType;
            ShoesAdj = shoesAdj;
            UniqueType = uniqueType;
            UniqueAdj = uniqueAdj;
            BadFoodTypes = badFoodTypes;
            GirlPairDefIDs = girlPairDefIDs;
            BaggageItemDefIDs = baggageItemDefIDs;
            UniqueItemDefIDs = uniqueItemDefIDs;
            ShoesItemDefIDs = shoesItemDefIDs;
            HasAltStyles = hasAltStyles;
            AltStylesFlagName = altStylesFlagName;
            AltStylesCodeDefinitionID = altStylesCodeDefinitionID;
            UnlockStyleCodeDefinitionID = unlockStyleCodeDefinitionID;
            PartIndexBody = partIndexBody;
            PartIndexNipples = partIndexNipples;
            PartIndexBlushLight = partIndexBlushLight;
            PartIndexBlushHeavy = partIndexBlushHeavy;
            PartIndexBlink = partIndexBlink;
            PartIndexMouthNeutral = partIndexMouthNeutral;
            PartIndexesPhonemes = partIndexesPhonemes;
            PartIndexesPhonemesTeeth = partIndexesPhonemesTeeth;
            Parts = parts;
            DefaultExpressionIndex = defaultExpressionIndex;
            FailureExpressionIndex = failureExpressionIndex;
            DefaultHairstyleIndex = defaultHairstyleIndex;
            DefaultOutfitIndex = defaultOutfitIndex;
            Expressions = expressions;
            Hairstyles = hairstyles;
            Outfits = outfits;
            SpecialParts = specialParts;
            HerQuestions = herQuestions;
            FavAnswers = favAnswers;
        }

        public GirlDataMod(GirlDefinition def, AssetProvider assetProvider)
            : base(def.id, false)
        {
            EditorTab = def.editorTab;
            GirlName = def.girlName;
            GirlNickName = def.girlNickName;
            GirlAge = def.girlAge;
            DialogTriggerTab = def.dialogTriggerTab;
            SpecialCharacter = def.specialCharacter;
            BossCharacter = def.bossCharacter;
            FavoriteAffectionType = def.favoriteAffectionType;
            LeastFavoriteAffectionType = def.leastFavoriteAffectionType;
            VoiceVolume = def.voiceVolume;
            SexVoiceVolume = def.sexVoiceVolume;
            assetProvider.NameAndAddAsset(ref SpecialEffectName, def.specialEffectPrefab);
            ShoesType = def.shoesType;
            ShoesAdj = def.shoesAdj;
            UniqueType = def.uniqueType;
            UniqueAdj = def.uniqueAdj;
            BadFoodTypes = def.badFoodTypes;
            GirlPairDefIDs = def.girlPairDefs?.Select(x => x.id).ToList();
            BaggageItemDefIDs = def.baggageItemDefs?.Select(x => x.id).ToList();
            UniqueItemDefIDs = def.uniqueItemDefs?.Select(x => x.id).ToList();
            ShoesItemDefIDs = def.shoesItemDefs?.Select(x => x.id).ToList();
            HasAltStyles = def.hasAltStyles;
            AltStylesFlagName = def.altStylesFlagName;
            AltStylesCodeDefinitionID = def.altStylesCodeDefinition?.id ?? -1;
            UnlockStyleCodeDefinitionID = def.unlockStyleCodeDefinition?.id ?? -1;
            PartIndexBody = def.partIndexBody;
            PartIndexNipples = def.partIndexNipples;
            PartIndexBlushLight = def.partIndexBlushLight;
            PartIndexBlushHeavy = def.partIndexBlushHeavy;
            PartIndexBlink = def.partIndexBlink;
            PartIndexMouthNeutral = def.partIndexMouthNeutral;
            PartIndexesPhonemes = def.partIndexesPhonemes;
            PartIndexesPhonemesTeeth = def.partIndexesPhonemesTeeth;
            DefaultExpressionIndex = def.defaultExpressionIndex;
            FailureExpressionIndex = def.failureExpressionIndex;
            DefaultHairstyleIndex = def.defaultHairstyleIndex;
            DefaultOutfitIndex = def.defaultOutfitIndex;
            Expressions = def.expressions;
            Hairstyles = def.hairstyles;
            Outfits = def.outfits;
            SpecialParts = def.specialParts;
            HerQuestions = def.herQuestions;
            FavAnswers = def.favAnswers;

            if (def.cellphonePortrait != null) { CellphonePortrait = new SpriteInfo(def.cellphonePortrait, assetProvider); }
            if (def.cellphonePortraitAlt != null) { CellphonePortraitAlt = new SpriteInfo(def.cellphonePortraitAlt, assetProvider); }
            if (def.cellphoneHead != null) { CellphoneHead = new SpriteInfo(def.cellphoneHead, assetProvider); }
            if (def.cellphoneHeadAlt != null) { CellphoneHeadAlt = new SpriteInfo(def.cellphoneHeadAlt, assetProvider); }
            if (def.cellphoneMiniHead) { CellphoneMiniHead = new SpriteInfo(def.cellphoneMiniHead, assetProvider); }
            if (def.cellphoneMiniHeadAlt) { CellphoneMiniHeadAlt = new SpriteInfo(def.cellphoneMiniHeadAlt, assetProvider); }
            if (def.breathEmitterPos != null) { BreathEmitterPos = new VectorInfo(def.breathEmitterPos); }
            if (def.upsetEmitterPos != null) { UpsetEmitterPos = new VectorInfo(def.upsetEmitterPos); }
            if (def.specialEffectOffset != null) { SpecialEffectOffset = new VectorInfo(def.specialEffectOffset); }
            if (def.parts != null) { Parts = def.parts.Select(x => new GirlPartInfo(x, assetProvider, Id)).ToList(); }
        }

        public override void SetData(GirlDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullableSet(ref def.editorTab, EditorTab);
            Access.NullableSet(ref def.girlAge, GirlAge);
            Access.NullableSet(ref def.dialogTriggerTab, DialogTriggerTab);
            Access.NullableSet(ref def.specialCharacter, SpecialCharacter);
            Access.NullableSet(ref def.bossCharacter, BossCharacter);
            Access.NullableSet(ref def.favoriteAffectionType, FavoriteAffectionType);
            Access.NullableSet(ref def.leastFavoriteAffectionType, LeastFavoriteAffectionType);
            Access.NullableSet(ref def.voiceVolume, VoiceVolume);
            Access.NullableSet(ref def.sexVoiceVolume, SexVoiceVolume);
            Access.NullableSet(ref def.shoesType, ShoesType);
            Access.NullableSet(ref def.uniqueType, UniqueType);
            Access.NullableSet(ref def.hasAltStyles, HasAltStyles);
            Access.NullableSet(ref def.partIndexBody, PartIndexBody);
            Access.NullableSet(ref def.partIndexNipples, PartIndexNipples);
            Access.NullableSet(ref def.partIndexBlushLight, PartIndexBlushLight);
            Access.NullableSet(ref def.partIndexBlushHeavy, PartIndexBlushHeavy);
            Access.NullableSet(ref def.partIndexBlink, PartIndexBlink);
            Access.NullableSet(ref def.partIndexMouthNeutral, PartIndexMouthNeutral);
            Access.NullableSet(ref def.defaultExpressionIndex, DefaultExpressionIndex);
            Access.NullableSet(ref def.failureExpressionIndex, FailureExpressionIndex);
            Access.NullableSet(ref def.defaultHairstyleIndex, DefaultHairstyleIndex);
            Access.NullableSet(ref def.defaultOutfitIndex, DefaultOutfitIndex);

            if (AltStylesCodeDefinitionID.HasValue) { def.altStylesCodeDefinition = gameData.Codes.Get(AltStylesCodeDefinitionID.Value); }
            if (UnlockStyleCodeDefinitionID.HasValue) { def.unlockStyleCodeDefinition = gameData.Codes.Get(UnlockStyleCodeDefinitionID.Value); }

            Access.NullSet(ref def.girlName, GirlName);
            Access.NullSet(ref def.girlNickName, GirlNickName);
            Access.NullSet(ref def.expressions, Expressions);
            Access.NullSet(ref def.hairstyles, Hairstyles);
            Access.NullSet(ref def.outfits, Outfits);
            Access.NullSet(ref def.specialParts, SpecialParts);
            Access.NullSet(ref def.herQuestions, HerQuestions);
            Access.NullSet(ref def.favAnswers, FavAnswers);
            Access.NullSet(ref def.altStylesFlagName, AltStylesFlagName);
            Access.NullSet(ref def.partIndexesPhonemes, PartIndexesPhonemes);
            Access.NullSet(ref def.partIndexesPhonemesTeeth, PartIndexesPhonemesTeeth);
            Access.NullSet(ref def.shoesAdj, ShoesAdj);
            Access.NullSet(ref def.uniqueAdj, UniqueAdj);
            Access.NullSet(ref def.badFoodTypes, BadFoodTypes);

            if (CellphonePortrait != null) { def.cellphonePortrait = CellphonePortrait.ToSprite(assetProvider); }
            if (CellphonePortraitAlt != null) { def.cellphonePortraitAlt = CellphonePortraitAlt.ToSprite(assetProvider); }
            if (CellphoneHead != null) { def.cellphoneHead = CellphoneHead.ToSprite(assetProvider); }
            if (CellphoneHeadAlt != null) { def.cellphoneHeadAlt = CellphoneHeadAlt.ToSprite(assetProvider); }
            if (CellphoneMiniHead != null) { def.cellphoneMiniHead = CellphoneMiniHead.ToSprite(assetProvider); }
            if (CellphoneMiniHeadAlt != null) { def.cellphoneMiniHeadAlt = CellphoneMiniHeadAlt.ToSprite(assetProvider); }

            if (BreathEmitterPos != null) { def.breathEmitterPos = BreathEmitterPos.ToVector2(); }
            if (UpsetEmitterPos != null) { def.upsetEmitterPos = UpsetEmitterPos.ToVector2(); }
            if (SpecialEffectOffset != null) { def.specialEffectOffset = SpecialEffectOffset.ToVector2(); }

            if (SpecialEffectName != null) { def.specialEffectPrefab = (UiDollSpecialEffect)assetProvider.GetAsset(SpecialEffectName); }

            SetGirlPairs(ref def.girlPairDefs, GirlPairDefIDs, gameData);
            SetItems(ref def.baggageItemDefs, BaggageItemDefIDs, gameData);
            SetItems(ref def.uniqueItemDefs, UniqueItemDefIDs, gameData);
            SetItems(ref def.shoesItemDefs, ShoesItemDefIDs, gameData);
            SetGirlParts(ref def.parts, Parts, assetProvider);
        }
    }
}
