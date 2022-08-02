// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
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
    [UiSonGroup("Items", 6, null, DisplayMode.Horizontal)]
    [UiSonGroup("Emmiters", 4, null, DisplayMode.Horizontal)]
    [UiSonGroup("Special Effect", 3)]
    [UiSonGroup("Style", 2)]
    [UiSonGroup("Cellphone Sprites", 1)]
    [UiSonGroup("Parts", 0)]
    
    public class GirlDataMod : DataMod, IGirlDataMod
    {
        [UiSonEncapsulatingUi(-1, "Parts")]
        public List<GirlPartDataMod> parts;

        [UiSonEncapsulatingUi]
        public List<HairstyleDataMod> hairstyles;

        [UiSonEncapsulatingUi]
        public List<OutfitDataMod> outfits;

        [UiSonEncapsulatingUi]
        public List<(RelativeId, List<DialogLineDataMod>)> linesByDialogTriggerId;

        #region Girl Info

        [UiSonTextEditUi(0, "Girl Info")]
        public string GirlName;

        [UiSonTextEditUi(0, "Girl Info")]
        public string GirlNickName;

        [UiSonTextEditUi(0, "Girl Info")]
        public int? GirlAge;

        [UiSonSelectorUi("NullableBoolNames", 0, "Girl Info", "NullableBoolIds")]
        public bool? SpecialCharacter;

        [UiSonSelectorUi("NullableBoolNames", 0, "Girl Info", "NullableBoolIds")]
        public bool? BossCharacter;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable, 0, "Girl Info")]
        public PuzzleAffectionType? FavoriteAffectionType;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable, 0, "Girl Info")]
        public PuzzleAffectionType? LeastFavoriteAffectionType;

        [UiSonSelectorUi(DefaultData.ItemShoesTypeNullable, 0, "Girl Info")]
        public ItemShoesType? ShoesType;

        [UiSonTextEditUi(0, "Girl Info")]
        public string ShoesAdj;

        [UiSonSelectorUi(DefaultData.ItemUniqueTypeNullable, 0, "Girl Info")]
        public ItemUniqueType? UniqueType;

        [UiSonTextEditUi(0, "Girl Info")]
        public string UniqueAdj;

        [UiSonTextEditUi(0, "Girl Info")]
        public float? VoiceVolume;

        [UiSonTextEditUi(0, "Girl Info")]
        public float? SexVoiceVolume;

        [UiSonMultiChoiceUi(DefaultData.ItemFoodType, 0, "Girl Info")]
        public List<ItemFoodType> BadFoodTypes;

        [UiSonElementSelectorUi(nameof(GirlPairDataMod), 0, null, "id", DefaultData.DefaultGirlPairNames_Name, DefaultData.DefaultGirlPairIds_Name)]
        public List<RelativeId?> GirlPairDefIDs;

        #endregion

        #region Items

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, "Items", "id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public List<RelativeId?> BaggageItemDefIDs;

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, "Items", "id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public List<RelativeId?> UniqueItemDefIDs;

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, "Items", "id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public List<RelativeId?> ShoesItemDefIDs;

        #endregion

        #region Questions

        [UiSonCollection]
        [UiSonEncapsulatingUi]
        public List<GirlQuestionSubDefinition> HerQuestions;

        [UiSonCollection]
        [UiSonTextEditUi]
        public List<int> FavAnswers;

        #endregion

        #region special

        [UiSonSelectorUi(DefaultData.UiDollSpecialEffectNames_Name, 0, "Special Effect")]
        public string SpecialEffectName;

        [UiSonEncapsulatingUi(0, "Special Effect")]
        public VectorInfo SpecialEffectOffset;

        [UiSonSelectorUi("NullableBoolNames", 0, "Style", "NullableBoolIds")]
        public bool? HasAltStyles;

        [UiSonTextEditUi(0, "Style")]
        public string AltStylesFlagName;

        [UiSonElementSelectorUi(nameof(CodeDataMod), 0, "Style", "id", DefaultData.DefaultCodeNames_Name, DefaultData.DefaultCodeIds_Name)]
        public RelativeId? AltStylesCodeDefinitionID;

        [UiSonElementSelectorUi(nameof(CodeDataMod), 0, "Style", "id", DefaultData.DefaultCodeNames_Name, DefaultData.DefaultCodeIds_Name)]
        public RelativeId? UnlockStyleCodeDefinitionID;

        #endregion

        #region Sprites

        [UiSonEncapsulatingUi(0, "Cellphone Sprites")]
        public SpriteInfo CellphonePortrait;

        [UiSonEncapsulatingUi(0, "Cellphone Sprites")]
        public SpriteInfo CellphonePortraitAlt;

        [UiSonEncapsulatingUi(0, "Cellphone Sprites")]
        public SpriteInfo CellphoneHead;

        [UiSonEncapsulatingUi(0, "Cellphone Sprites")]
        public SpriteInfo CellphoneHeadAlt;

        [UiSonEncapsulatingUi(0, "Cellphone Sprites")]
        public SpriteInfo CellphoneMiniHead;

        [UiSonEncapsulatingUi(0, "Cellphone Sprites")]
        public SpriteInfo CellphoneMiniHeadAlt;

        [UiSonEncapsulatingUi(0, "Emmiters")]
        public VectorInfo BreathEmitterPos;

        [UiSonEncapsulatingUi(0, "Emmiters")]
        public VectorInfo UpsetEmitterPos;

        [UiSonEncapsulatingUi(1, "Parts")]
        public RelativeId? PartIdBody;

        [UiSonEncapsulatingUi(1, "Parts")]
        public RelativeId? PartIdNipples;

        [UiSonEncapsulatingUi(1, "Parts")]
        public RelativeId? PartIdBlushLight;

        [UiSonEncapsulatingUi(1, "Parts")]
        public RelativeId? PartIdBlushHeavy;

        [UiSonEncapsulatingUi(1, "Parts")]
        public RelativeId? PartIdBlink;

        [UiSonEncapsulatingUi(1, "Parts")]
        public RelativeId? PartIdMouthNeutral;

        [UiSonTextEditUi(1, "Parts")]
        public int? DefaultExpressionIndex;

        [UiSonTextEditUi(1, "Parts")]
        public int? FailureExpressionIndex;

        [UiSonEncapsulatingUi(1, "Parts")]
        public RelativeId? DefaultHairstyleId;

        [UiSonEncapsulatingUi(1, "Parts")]
        public RelativeId? DefaultOutfitId;

        [UiSonEncapsulatingUi(-1, "Parts")]
        public RelativeId? Phonemes_aeil;

        [UiSonEncapsulatingUi(-1, "Parts")]
        public RelativeId? Phonemes_neutral;

        [UiSonEncapsulatingUi(-1, "Parts")]
        public RelativeId? Phonemes_oquw;

        [UiSonEncapsulatingUi(-1, "Parts")]
        public RelativeId? Phonemes_fv;

        [UiSonEncapsulatingUi(-1, "Parts")]
        public RelativeId? Phonemes_other;

        [UiSonEncapsulatingUi(-2, "Parts")]
        public RelativeId? PhonemesTeeth_aeil;

        [UiSonEncapsulatingUi(-2, "Parts")]
        public RelativeId? PhonemesTeeth_neutral;

        [UiSonEncapsulatingUi(-2, "Parts")]
        public RelativeId? PhonemesTeeth_oquw;

        [UiSonEncapsulatingUi(-2, "Parts")]
        public RelativeId? PhonemesTeeth_fv;

        [UiSonEncapsulatingUi(-2, "Parts")]
        public RelativeId? PhonemesTeeth_other;

        [UiSonEncapsulatingUi]
        public List<GirlExpressionSubDefinition> Expressions;

        [UiSonEncapsulatingUi]
        public List<GirlSpecialPartSubDefinition> SpecialParts;

        #endregion

        /// <inheritdoc/>
        public GirlDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public GirlDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from an unmodified definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        internal GirlDataMod(GirlDefinition def, AssetProvider assetProvider, IEnumerable<DialogTriggerDefinition> dts)
            : base(new RelativeId(def), InsertStyle.replace, 0)
        {
            GirlName = def.girlName;
            GirlNickName = def.girlNickName;
            GirlAge = def.girlAge;
            SpecialCharacter = def.specialCharacter;
            BossCharacter = def.bossCharacter;
            FavoriteAffectionType = def.favoriteAffectionType;
            LeastFavoriteAffectionType = def.leastFavoriteAffectionType;
            VoiceVolume = def.voiceVolume;
            SexVoiceVolume = def.sexVoiceVolume;
            ShoesType = def.shoesType;
            ShoesAdj = def.shoesAdj;
            UniqueType = def.uniqueType;
            UniqueAdj = def.uniqueAdj;
            BadFoodTypes = def.badFoodTypes;
            HasAltStyles = def.hasAltStyles;
            AltStylesFlagName = def.altStylesFlagName;
            Expressions = def.expressions;
            SpecialParts = def.specialParts;
            HerQuestions = def.herQuestions;
            FavAnswers = def.favAnswers;
            DefaultExpressionIndex = def.defaultExpressionIndex;
            FailureExpressionIndex = def.failureExpressionIndex;

            assetProvider.NameAndAddAsset(ref SpecialEffectName, def.specialEffectPrefab);

            GirlPairDefIDs = def.girlPairDefs?.Select(x => (RelativeId?)new RelativeId(x)).ToList();
            BaggageItemDefIDs = def.baggageItemDefs?.Select(x => (RelativeId?)new RelativeId(x)).ToList();
            UniqueItemDefIDs = def.uniqueItemDefs?.Select(x => (RelativeId?)new RelativeId(x)).ToList();
            ShoesItemDefIDs = def.shoesItemDefs?.Select(x => (RelativeId?)new RelativeId(x)).ToList();
            AltStylesCodeDefinitionID = new RelativeId(def);
            UnlockStyleCodeDefinitionID = new RelativeId(def);
            PartIdBody = new RelativeId(-1, def.partIndexBody);
            PartIdNipples = new RelativeId(-1, def.partIndexNipples);
            PartIdBlushLight = new RelativeId(-1, def.partIndexBlushLight);
            PartIdBlushHeavy = new RelativeId(-1, def.partIndexBlushHeavy);
            PartIdBlink = new RelativeId(-1, def.partIndexBlink);
            PartIdMouthNeutral = new RelativeId(-1, def.partIndexMouthNeutral);

            if (def.partIndexesPhonemes != null)
            {
                var it = def.partIndexesPhonemes.GetEnumerator();
                it.MoveNext();
                Phonemes_aeil = it.Current;
                it.MoveNext();
                Phonemes_neutral = it.Current;
                it.MoveNext();
                Phonemes_oquw = it.Current;
                it.MoveNext();
                Phonemes_fv = it.Current;
                it.MoveNext();
                Phonemes_other = it.Current;
            }

            if (def.partIndexesPhonemesTeeth != null)
            {
                var it = def.partIndexesPhonemesTeeth.GetEnumerator();
                it.MoveNext();
                PhonemesTeeth_aeil = it.Current;
                it.MoveNext();
                PhonemesTeeth_neutral = it.Current;
                it.MoveNext();
                PhonemesTeeth_oquw = it.Current;
                it.MoveNext();
                PhonemesTeeth_fv = it.Current;
                it.MoveNext();
                PhonemesTeeth_other = it.Current;
            }

            DefaultHairstyleId = new RelativeId(-1, def.defaultHairstyleIndex);
            DefaultOutfitId = new RelativeId(-1, def.defaultOutfitIndex);

            if (def.cellphonePortrait != null) { CellphonePortrait = new SpriteInfo(def.cellphonePortrait, assetProvider); }
            if (def.cellphonePortraitAlt != null) { CellphonePortraitAlt = new SpriteInfo(def.cellphonePortraitAlt, assetProvider); }
            if (def.cellphoneHead != null) { CellphoneHead = new SpriteInfo(def.cellphoneHead, assetProvider); }
            if (def.cellphoneHeadAlt != null) { CellphoneHeadAlt = new SpriteInfo(def.cellphoneHeadAlt, assetProvider); }
            if (def.cellphoneMiniHead) { CellphoneMiniHead = new SpriteInfo(def.cellphoneMiniHead, assetProvider); }
            if (def.cellphoneMiniHeadAlt) { CellphoneMiniHeadAlt = new SpriteInfo(def.cellphoneMiniHeadAlt, assetProvider); }
            if (def.breathEmitterPos != null) { BreathEmitterPos = new VectorInfo(def.breathEmitterPos); }
            if (def.upsetEmitterPos != null) { UpsetEmitterPos = new VectorInfo(def.upsetEmitterPos); }
            if (def.specialEffectOffset != null) { SpecialEffectOffset = new VectorInfo(def.specialEffectOffset); }

            int i;
            if (def.parts != null)
            {
                i = 0;
                parts = def.parts.Select(x => new GirlPartDataMod(i++, assetProvider, def)).ToList();
            }

            if (def.outfits != null)
            {
                i = 0;
                outfits = def.outfits.Select(x => new OutfitDataMod(i++, def, assetProvider)).ToList();
            }

            if (def.hairstyles != null)
            {
                i = 0;
                hairstyles = def.hairstyles.Select(x => new HairstyleDataMod(i++, def, assetProvider)).ToList();
            }

            linesByDialogTriggerId = new List<(RelativeId, List<DialogLineDataMod>)>();
            
            foreach(var dialogTrigger in dts)
            {
                var dialogTirggerId = new RelativeId(dialogTrigger);

                var found = false;
                var lines = linesByDialogTriggerId.FirstOrDefault(x => { found = x.Item1 == dialogTirggerId; return found; });
                if (!found)
                {
                    lines = (dialogTirggerId, new List<DialogLineDataMod>());
                    linesByDialogTriggerId.Add(lines);
                }

                i = 0;
                foreach(var line in dialogTrigger.GetLineSetByGirl(def).dialogLines)
                {
                    lines.Item2.Add(new DialogLineDataMod(line, assetProvider, new RelativeId(-1, i)));
                    i++;
                }
            }
        }

        /// <inheritdoc/>
        public void SetData(GirlDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider)
        {
            ValidatedSet.SetValue(ref def.girlAge, GirlAge);
            ValidatedSet.SetValue(ref def.specialCharacter, SpecialCharacter);
            ValidatedSet.SetValue(ref def.bossCharacter, BossCharacter);
            ValidatedSet.SetValue(ref def.favoriteAffectionType, FavoriteAffectionType);
            ValidatedSet.SetValue(ref def.leastFavoriteAffectionType, LeastFavoriteAffectionType);
            ValidatedSet.SetValue(ref def.voiceVolume, VoiceVolume);
            ValidatedSet.SetValue(ref def.sexVoiceVolume, SexVoiceVolume);
            ValidatedSet.SetValue(ref def.shoesType, ShoesType);
            ValidatedSet.SetValue(ref def.uniqueType, UniqueType);
            ValidatedSet.SetValue(ref def.hasAltStyles, HasAltStyles);
            ValidatedSet.SetValue(ref def.partIndexBody, ModInterface.Data.GetPartIndex(Id, PartIdBody));
            ValidatedSet.SetValue(ref def.partIndexNipples, ModInterface.Data.GetPartIndex(Id, PartIdNipples));
            ValidatedSet.SetValue(ref def.partIndexBlushLight, ModInterface.Data.GetPartIndex(Id, PartIdBlushLight));
            ValidatedSet.SetValue(ref def.partIndexBlushHeavy, ModInterface.Data.GetPartIndex(Id, PartIdBlushHeavy));
            ValidatedSet.SetValue(ref def.partIndexBlink, ModInterface.Data.GetPartIndex(Id, PartIdBlink));
            ValidatedSet.SetValue(ref def.partIndexMouthNeutral, ModInterface.Data.GetPartIndex(Id, PartIdMouthNeutral));
            ValidatedSet.SetValue(ref def.defaultExpressionIndex, DefaultExpressionIndex);
            ValidatedSet.SetValue(ref def.failureExpressionIndex, FailureExpressionIndex);
            ValidatedSet.SetValue(ref def.defaultHairstyleIndex, ModInterface.Data.GetHairstyleIndex(Id, DefaultHairstyleId));
            ValidatedSet.SetValue(ref def.defaultOutfitIndex, ModInterface.Data.GetOutfitIndex(Id, DefaultOutfitId));

            ValidatedSet.SetValue(ref def.altStylesCodeDefinition, gameDataProvider.GetCode(AltStylesCodeDefinitionID), InsertStyle);
            ValidatedSet.SetValue(ref def.unlockStyleCodeDefinition, gameDataProvider.GetCode(UnlockStyleCodeDefinitionID), InsertStyle);

            ValidatedSet.SetValue(ref def.girlName, GirlName, InsertStyle);
            ValidatedSet.SetValue(ref def.girlNickName, GirlNickName, InsertStyle);
            ValidatedSet.SetListValue(ref def.expressions, Expressions, InsertStyle);

            ValidatedSet.SetListValue(ref def.specialParts, SpecialParts, InsertStyle);
            ValidatedSet.SetListValue(ref def.herQuestions, HerQuestions, InsertStyle);
            ValidatedSet.SetListValue(ref def.favAnswers, FavAnswers, InsertStyle);
            ValidatedSet.SetValue(ref def.altStylesFlagName, AltStylesFlagName, InsertStyle);

            var partIdsPhonemes = new[]
{
                Phonemes_aeil,
                Phonemes_neutral,
                Phonemes_oquw,
                Phonemes_fv,
                Phonemes_other
            };

            ValidatedSet.SetListValue(ref def.partIndexesPhonemes, partIdsPhonemes?.Select(x => ModInterface.Data.GetPartIndex(Id, x)), InsertStyle);

            var partIdsPhonemesTeeth = new[]
            {
                PhonemesTeeth_aeil,
                PhonemesTeeth_neutral,
                PhonemesTeeth_oquw,
                PhonemesTeeth_fv,
                PhonemesTeeth_other
            };

            ValidatedSet.SetListValue(ref def.partIndexesPhonemesTeeth, partIdsPhonemesTeeth.Select(x => ModInterface.Data.GetPartIndex(Id, x)), InsertStyle);

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

            ValidatedSet.SetValue(ref def.specialEffectPrefab, (UiDollSpecialEffect)assetProvider.GetAsset(SpecialEffectName), InsertStyle);

            ValidatedSet.SetListValue(ref def.girlPairDefs, GirlPairDefIDs?.Select(x => gameDataProvider.GetGirlPair(x)), InsertStyle);
            ValidatedSet.SetListValue(ref def.baggageItemDefs, BaggageItemDefIDs?.Select(x => gameDataProvider.GetItem(x)), InsertStyle);
            ValidatedSet.SetListValue(ref def.uniqueItemDefs, UniqueItemDefIDs?.Select(x => gameDataProvider.GetItem(x)), InsertStyle);
            ValidatedSet.SetListValue(ref def.shoesItemDefs, ShoesItemDefIDs?.Select(x => gameDataProvider.GetItem(x)), InsertStyle);
        }

        public IEnumerable<IGirlSubDataMod<ExpandedOutfitDefinition>> GetOutfits() => outfits;
        public IEnumerable<IGirlSubDataMod<ExpandedHairstyleDefinition>> GetHairstyles() => hairstyles;
        public IEnumerable<IGirlSubDataMod<GirlPartSubDefinition>> GetPartMods() => parts;
        public IEnumerable<Tuple<RelativeId, IEnumerable<IGirlSubDataMod<DialogLine>>>> GetLinesByDialogTriggerId() => (IEnumerable<Tuple<RelativeId, IEnumerable<IGirlSubDataMod<DialogLine>>>>)linesByDialogTriggerId;
        
    }
}
