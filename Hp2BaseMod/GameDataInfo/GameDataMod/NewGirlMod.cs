//using System;
//using System.Collections.Generic;
//using System.Text;
//using UiSon.Attribute;

//namespace Hp2BaseMod.GameDataInfo.GameDataMod
//{
//    internal class NewGirlMod : DataMod
//    {
//        // girl needs:
//        // girl id
//        // ailment ids
//        // question
//        // 
//        [UiSonTextEditUi(0, "Girl Info")]
//        public string GirlName;

//        [UiSonTextEditUi(0, "Girl Info")]
//        public string GirlNickName;

//        [UiSonTextEditUi(0, "Girl Info")]
//        public int? GirlAge;

//        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String, 0, "Girl Info")]
//        public PuzzleAffectionType? FavoriteAffectionType;

//        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String, 0, "Girl Info")]
//        public PuzzleAffectionType? LeastFavoriteAffectionType;

//        [UiSonSelectorUi(DefaultData.ItemShoesTypeNullable_As_String, 0, "Girl Info")]
//        public ItemShoesType? ShoesType;

//        [UiSonTextEditUi(0, "Girl Info")]
//        public string ShoesAdj;

//        [UiSonSelectorUi(DefaultData.ItemUniqueTypeNullable_As_String, 0, "Girl Info")]
//        public ItemUniqueType? UniqueType;

//        [UiSonTextEditUi(0, "Girl Info")]
//        public string UniqueAdj;

//        [UiSonTextEditUi(0, "Girl Info")]
//        public float? VoiceVolume;

//        [UiSonTextEditUi(0, "Girl Info")]
//        public float? SexVoiceVolume;

//        [UiSonMultiChoiceUi(DefaultData.ItemFoodTypeNullable_As_String, 0, "Girl Info")]
//        public List<ItemFoodType> BadFoodTypes;

//        [UiSonCollection]
//        [UiSonElementSelectorUi(nameof(GirlPairDataMod), 0, null, "Id", DefaultData.DefaultGirlPairNames_Name, DefaultData.DefaultGirlPairIds_Name)]
//        public List<int> GirlPairDefIDs;

//        [UiSonElementSelectorUi(nameof(AilmentDataMod), 0, "Items", "Id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
//        public List<int> BaggageItemDefIDs;

//        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, "Items", "Id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
//        public List<int> UniqueItemDefIDs;

//        [UiSonElementSelectorUi(nameof(GirlPairDataMod), 0, "Items", "Id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
//        public List<int> ShoesItemDefIDs;

//        [UiSonCollection]
//        [UiSonMemberElement]
//        public List<GirlQuestionSubDefinition> HerQuestions;

//        [UiSonCollection]
//        [UiSonTextEditUi]
//        public List<int> FavAnswers;

//        [UiSonSelectorUi(DefaultData.UiDollSpecialEffectNames_Name, 0, "Special Effect")]
//        public string SpecialEffectName;

//        [UiSonMemberElement(0, "Special Effect")]
//        public VectorInfo SpecialEffectOffset;

//        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Style")]
//        public bool? HasAltStyles;

//        [UiSonTextEditUi(0, "Style")]
//        public string AltStylesFlagName;

//        [UiSonElementSelectorUi(nameof(CodeDataMod), 0, "Style", "Id", DefaultData.DefaultCodeNames_Name, DefaultData.DefaultCodeIds_Name)]
//        public int? AltStylesCodeDefinitionID;

//        [UiSonElementSelectorUi(nameof(CodeDataMod), 0, "Style", "Id", DefaultData.DefaultCodeNames_Name, DefaultData.DefaultCodeIds_Name)]
//        public int? UnlockStyleCodeDefinitionID;

//        #region Sprites

//        [UiSonMemberElement(0, "Cellphone Sprites")]
//        public SpriteInfo CellphonePortrait;

//        [UiSonMemberElement(0, "Cellphone Sprites")]
//        public SpriteInfo CellphonePortraitAlt;

//        [UiSonMemberElement(0, "Cellphone Sprites")]
//        public SpriteInfo CellphoneHead;

//        [UiSonMemberElement(0, "Cellphone Sprites")]
//        public SpriteInfo CellphoneHeadAlt;

//        [UiSonMemberElement(0, "Cellphone Sprites")]
//        public SpriteInfo CellphoneMiniHead;

//        [UiSonMemberElement(0, "Cellphone Sprites")]
//        public SpriteInfo CellphoneMiniHeadAlt;

//        [UiSonMemberElement(0, "Emmiters")]
//        public VectorInfo BreathEmitterPos;

//        [UiSonMemberElement(0, "Emmiters")]
//        public VectorInfo UpsetEmitterPos;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? PartIndexBody;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? PartIndexNipples;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? PartIndexBlushLight;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? PartIndexBlushHeavy;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? PartIndexBlink;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? PartIndexMouthNeutral;

//        [UiSonTextEditUi(-1, "Parts")]
//        public List<int> PartIndexesPhonemes;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? DefaultExpressionIndex;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? FailureExpressionIndex;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? DefaultHairstyleIndex;

//        [UiSonTextEditUi(1, "Parts")]
//        public int? DefaultOutfitIndex;

//        [UiSonCollection]
//        [UiSonTextEditUi(-1, "Parts")]
//        public List<int> PartIndexesPhonemesTeeth;

//        [UiSonCollection]
//        [UiSonMemberElement(-1, "Parts")]
//        public List<GirlPartInfo> Parts;

//        [UiSonCollection]
//        [UiSonMemberElement]
//        public List<GirlExpressionSubDefinition> Expressions;

//        [UiSonCollection]
//        [UiSonMemberElement]
//        public List<GirlHairstyleSubDefinition> Hairstyles;

//        [UiSonCollection]
//        [UiSonMemberElement]
//        public List<GirlOutfitSubDefinition> Outfits;

//        [UiSonCollection]
//        [UiSonMemberElement]
//        public List<GirlSpecialPartSubDefinition> SpecialParts;

//        #endregion
//    }
//}
