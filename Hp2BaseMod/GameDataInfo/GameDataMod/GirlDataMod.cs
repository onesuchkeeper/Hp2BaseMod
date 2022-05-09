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
    /// Serializable information to make a GirlDefinition
    /// </summary>
    [UiSonElement]
    [UiSonGroup("Girl Info", 7)]
    [UiSonGroup("Items", 6, DisplayMode.Horizontal)]
    [UiSonGroup("Questions", 5)]
    [UiSonGroup("Emmiters", 4, DisplayMode.Horizontal)]
    [UiSonGroup("Special Effect", 3)]
    [UiSonGroup("Style", 2)]
    [UiSonGroup("Cellphone Sprites", 1)]
    [UiSonGroup("Parts", 0)]
    
    public class GirlDataMod : DataMod, IGameDataMod<GirlDefinition>
    {
        // editor tab isn't used in game
        public EditorGirlDefinitionTab? EditorTab;
        // dialog trigger tab replaced with the girl's index in girls collection orderd by giorl id
        public EditorDialogTriggerTab? DialogTriggerTab;

        #region Girl Info

        [UiSonTextEditUi(0, "Girl Info")]
        public string GirlName;

        [UiSonTextEditUi(0, "Girl Info")]
        public string GirlNickName;

        [UiSonTextEditUi(0, "Girl Info")]
        public int? GirlAge;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Girl Info")]
        public bool? SpecialCharacter;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Girl Info")]
        public bool? BossCharacter;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String, 0, "Girl Info")]
        public PuzzleAffectionType? FavoriteAffectionType;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String, 0, "Girl Info")]
        public PuzzleAffectionType? LeastFavoriteAffectionType;

        [UiSonSelectorUi(DefaultData.ItemShoesTypeNullable_As_String, 0, "Girl Info")]
        public ItemShoesType? ShoesType;

        [UiSonTextEditUi(0, "Girl Info")]
        public string ShoesAdj;

        [UiSonSelectorUi(DefaultData.ItemUniqueTypeNullable_As_String, 0, "Girl Info")]
        public ItemUniqueType? UniqueType;

        [UiSonTextEditUi(0, "Girl Info")]
        public string UniqueAdj;

        [UiSonTextEditUi(0, "Girl Info")]
        public float? VoiceVolume;

        [UiSonTextEditUi(0, "Girl Info")]
        public float? SexVoiceVolume;

        [UiSonMultiChoiceUi(DefaultData.ItemFoodType_As_String, 0, "Girl Info")]
        public List<ItemFoodType> BadFoodTypes;

        [UiSonCollection]
        [UiSonElementSelectorUi(nameof(GirlPairDataMod), 0, null, "Id", DefaultData.DefaultGirlPairNames_Name, DefaultData.DefaultGirlPairIds_Name)]
        public List<int?> GirlPairDefIDs;

        #endregion

        #region Items

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, "Items", "Id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public List<int?> BaggageItemDefIDs;

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, "Items", "Id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public List<int?> UniqueItemDefIDs;

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, "Items", "Id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public List<int?> ShoesItemDefIDs;

        #endregion

        #region Questions

        [UiSonCollection]
        [UiSonMemberElement]
        public List<GirlQuestionSubDefinition> HerQuestions;

        [UiSonCollection]
        [UiSonTextEditUi]
        public List<int> FavAnswers;

        #endregion

        [UiSonSelectorUi(DefaultData.UiDollSpecialEffectNames_Name, 0, "Special Effect")]
        public string SpecialEffectName;

        [UiSonMemberElement(0, "Special Effect")]
        public VectorInfo SpecialEffectOffset;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Style")]
        public bool? HasAltStyles;

        [UiSonTextEditUi(0, "Style")]
        public string AltStylesFlagName;

        [UiSonElementSelectorUi(nameof(CodeDataMod), 0, "Style", "Id", DefaultData.DefaultCodeNames_Name, DefaultData.DefaultCodeIds_Name)]
        public int? AltStylesCodeDefinitionID;

        [UiSonElementSelectorUi(nameof(CodeDataMod), 0, "Style", "Id", DefaultData.DefaultCodeNames_Name, DefaultData.DefaultCodeIds_Name)]
        public int? UnlockStyleCodeDefinitionID;

        #region Sprites

        [UiSonMemberElement(0, "Cellphone Sprites")]
        public SpriteInfo CellphonePortrait;

        [UiSonMemberElement(0, "Cellphone Sprites")]
        public SpriteInfo CellphonePortraitAlt;

        [UiSonMemberElement(0, "Cellphone Sprites")]
        public SpriteInfo CellphoneHead;

        [UiSonMemberElement(0, "Cellphone Sprites")]
        public SpriteInfo CellphoneHeadAlt;

        [UiSonMemberElement(0, "Cellphone Sprites")]
        public SpriteInfo CellphoneMiniHead;

        [UiSonMemberElement(0, "Cellphone Sprites")]
        public SpriteInfo CellphoneMiniHeadAlt;

        [UiSonMemberElement(0, "Emmiters")]
        public VectorInfo BreathEmitterPos;

        [UiSonMemberElement(0, "Emmiters")]
        public VectorInfo UpsetEmitterPos;

        [UiSonTextEditUi(1, "Parts")]
        public int? PartIndexBody;

        [UiSonTextEditUi(1, "Parts")]
        public int? PartIndexNipples;

        [UiSonTextEditUi(1, "Parts")]
        public int? PartIndexBlushLight;

        [UiSonTextEditUi(1, "Parts")]
        public int? PartIndexBlushHeavy;

        [UiSonTextEditUi(1, "Parts")]
        public int? PartIndexBlink;

        [UiSonTextEditUi(1, "Parts")]
        public int? PartIndexMouthNeutral;

        [UiSonTextEditUi(-1, "Parts")]
        public List<int?> PartIndexesPhonemes;

        [UiSonTextEditUi(1, "Parts")]
        public int? DefaultExpressionIndex;

        [UiSonTextEditUi(1, "Parts")]
        public int? FailureExpressionIndex;

        [UiSonTextEditUi(1, "Parts")]
        public int? DefaultHairstyleIndex;

        [UiSonTextEditUi(1, "Parts")]
        public int? DefaultOutfitIndex;

        [UiSonCollection]
        [UiSonTextEditUi(-1, "Parts")]
        public List<int?> PartIndexesPhonemesTeeth;

        [UiSonCollection]
        [UiSonMemberElement(-1, "Parts")]
        public List<GirlPartInfo> Parts;

        [UiSonCollection]
        [UiSonMemberElement]
        public List<GirlExpressionSubDefinition> Expressions;

        [UiSonCollection]
        [UiSonMemberElement]
        public List<GirlHairstyleSubDefinition> Hairstyles;

        [UiSonCollection]
        [UiSonMemberElement]
        public List<GirlOutfitSubDefinition> Outfits;

        [UiSonCollection]
        [UiSonMemberElement]
        public List<GirlSpecialPartSubDefinition> SpecialParts;

        #endregion

        public GirlDataMod() { }

        public GirlDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
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
                           List<int?> girlPairDefIDs,
                           List<int?> baggageItemDefIDs,
                           List<int?> uniqueItemDefIDs,
                           List<int?> shoesItemDefIDs,
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
                           List<int?> partIndexesPhonemes,
                           List<int?> partIndexesPhonemesTeeth,
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
                           InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
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
            : base(def.id, InsertStyle.replace, def.name)
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
            GirlPairDefIDs = def.girlPairDefs?.Select(x => x.id as int?).ToList();
            BaggageItemDefIDs = def.baggageItemDefs?.Select(x => x.id as int?).ToList();
            UniqueItemDefIDs = def.uniqueItemDefs?.Select(x => x.id as int?).ToList();
            ShoesItemDefIDs = def.shoesItemDefs?.Select(x => x.id as int?).ToList();
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
            PartIndexesPhonemes = def.partIndexesPhonemes?.Select(x => x as int?).ToList();
            PartIndexesPhonemesTeeth = def.partIndexesPhonemesTeeth?.Select(x => x as int?).ToList();
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

        public void SetData(GirlDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a girl");
            ModInterface.Instance.IncreaseLogIndent();

            def.id = Id;

            ValidatedSet.SetValue(ref def.editorTab, EditorTab);
            ValidatedSet.SetValue(ref def.girlAge, GirlAge);
            ValidatedSet.SetValue(ref def.dialogTriggerTab, DialogTriggerTab);
            ValidatedSet.SetValue(ref def.specialCharacter, SpecialCharacter);
            ValidatedSet.SetValue(ref def.bossCharacter, BossCharacter);
            ValidatedSet.SetValue(ref def.favoriteAffectionType, FavoriteAffectionType);
            ValidatedSet.SetValue(ref def.leastFavoriteAffectionType, LeastFavoriteAffectionType);
            ValidatedSet.SetValue(ref def.voiceVolume, VoiceVolume);
            ValidatedSet.SetValue(ref def.sexVoiceVolume, SexVoiceVolume);
            ValidatedSet.SetValue(ref def.shoesType, ShoesType);
            ValidatedSet.SetValue(ref def.uniqueType, UniqueType);
            ValidatedSet.SetValue(ref def.hasAltStyles, HasAltStyles);
            ValidatedSet.SetValue(ref def.partIndexBody, PartIndexBody);
            ValidatedSet.SetValue(ref def.partIndexNipples, PartIndexNipples);
            ValidatedSet.SetValue(ref def.partIndexBlushLight, PartIndexBlushLight);
            ValidatedSet.SetValue(ref def.partIndexBlushHeavy, PartIndexBlushHeavy);
            ValidatedSet.SetValue(ref def.partIndexBlink, PartIndexBlink);
            ValidatedSet.SetValue(ref def.partIndexMouthNeutral, PartIndexMouthNeutral);
            ValidatedSet.SetValue(ref def.defaultExpressionIndex, DefaultExpressionIndex);
            ValidatedSet.SetValue(ref def.failureExpressionIndex, FailureExpressionIndex);
            ValidatedSet.SetValue(ref def.defaultHairstyleIndex, DefaultHairstyleIndex);
            ValidatedSet.SetValue(ref def.defaultOutfitIndex, DefaultOutfitIndex);

            ValidatedSet.SetValue(ref def.altStylesCodeDefinition, gameDataProvider.GetCode(AltStylesCodeDefinitionID), InsertStyle);
            ValidatedSet.SetValue(ref def.unlockStyleCodeDefinition, gameDataProvider.GetCode(UnlockStyleCodeDefinitionID), InsertStyle);

            ValidatedSet.SetValue(ref def.girlName, GirlName, InsertStyle);
            ValidatedSet.SetValue(ref def.girlNickName, GirlNickName, InsertStyle);
            ValidatedSet.SetListValue(ref def.expressions, Expressions, InsertStyle);
            ValidatedSet.SetListValue(ref def.hairstyles, Hairstyles, InsertStyle);
            ValidatedSet.SetListValue(ref def.outfits, Outfits, InsertStyle);
            ValidatedSet.SetListValue(ref def.specialParts, SpecialParts, InsertStyle);
            ValidatedSet.SetListValue(ref def.herQuestions, HerQuestions, InsertStyle);
            ValidatedSet.SetListValue(ref def.favAnswers, FavAnswers, InsertStyle);
            ValidatedSet.SetValue(ref def.altStylesFlagName, AltStylesFlagName, InsertStyle);
            ValidatedSet.SetListValue(ref def.partIndexesPhonemes, PartIndexesPhonemes, InsertStyle);
            ValidatedSet.SetListValue(ref def.partIndexesPhonemesTeeth, PartIndexesPhonemesTeeth, InsertStyle);
            ValidatedSet.SetListValue(ref def.badFoodTypes, BadFoodTypes, InsertStyle);
            ValidatedSet.SetValue(ref def.shoesAdj, ShoesAdj, InsertStyle);
            ValidatedSet.SetValue(ref def.uniqueAdj, UniqueAdj, InsertStyle);

            ValidatedSet.SetValue(ref def.cellphonePortrait, CellphonePortrait, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.cellphonePortraitAlt, CellphonePortraitAlt, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.cellphoneHead, CellphoneHead, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.cellphoneHeadAlt, CellphoneHeadAlt, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.cellphoneMiniHead, CellphoneMiniHead, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.cellphoneMiniHeadAlt, CellphoneMiniHeadAlt, InsertStyle, gameDataProvider, assetProvider);

            ValidatedSet.SetValue(ref def.breathEmitterPos, BreathEmitterPos, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.upsetEmitterPos, UpsetEmitterPos, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.specialEffectOffset, SpecialEffectOffset, InsertStyle, gameDataProvider, assetProvider);

            ValidatedSet.SetValue(ref def.specialEffectPrefab, (UiDollSpecialEffect)assetProvider.GetAsset(SpecialEffectName), insertStyle);

            ValidatedSet.SetListValue(ref def.girlPairDefs, GirlPairDefIDs?.Select(x => gameDataProvider.GetGirlPair(x)), InsertStyle);
            ValidatedSet.SetListValue(ref def.baggageItemDefs, BaggageItemDefIDs?.Select(x => gameDataProvider.GetItem(x)), InsertStyle);
            ValidatedSet.SetListValue(ref def.uniqueItemDefs, UniqueItemDefIDs?.Select(x => gameDataProvider.GetItem(x)), InsertStyle);
            ValidatedSet.SetListValue(ref def.shoesItemDefs, ShoesItemDefIDs?.Select(x => gameDataProvider.GetItem(x)), InsertStyle);
            ValidatedSet.SetListValue(ref def.parts, Parts, InsertStyle, gameDataProvider, assetProvider);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
