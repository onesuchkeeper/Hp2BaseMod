// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a ItemDefinition
    /// </summary>
    [UiSonElement]
    [UiSonGroup("Type")]
    [UiSonGroup("Appearance")]
    [UiSonGroup("Food")]
    [UiSonGroup("Puzzle")]
    [UiSonGroup("Store")]
    [UiSonGroup("Ailment")]
    public class ItemDataMod : DataMod, IGameDataMod<ItemDefinition>
    {
        [UiSonSelectorUi(DefaultData.ItemTypeNullable_As_String, 0, "Type")]
        public ItemType? ItemType;

        [UiSonSelectorUi(DefaultData.ItemShoesTypeNullable_As_String, 0, "Type")]
        public ItemShoesType? ShoesType;

        [UiSonSelectorUi(DefaultData.ItemUniqueTypeNullable_As_String, 0, "Type")]
        public ItemUniqueType? UniqueType;

        [UiSonSelectorUi(DefaultData.ItemDateGiftTypeNullable_As_String, 0, "Type")]
        public ItemDateGiftType? DateGiftType;

        [UiSonSelectorUi(DefaultData.ItemFoodTypeNullable_As_String, 0, "Type")]
        public ItemFoodType? FoodType;

        [UiSonMemberElement(0, "Appearance")]
        public SpriteInfo ItemSpriteInfo;

        [UiSonTextEditUi(0, "Appearance")]
        public string ItemName;

        [UiSonTextEditUi(0, "Appearance")]
        public string ItemDescription;

        [UiSonTextEditUi(0, "Appearance")]
        public int? TooltipColorIndex;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Store")]
        public bool? StoreSectionPreference;

        [UiSonTextEditUi(0, "Store")]
        public int? StoreCost;

        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, "Ailment", "Id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public int? CutsceneDefinitionID;

        [UiSonElementSelectorUi(nameof(AilmentDataMod), 0, "Ailment", "Id", DefaultData.DefaultAilmentNames_Name, DefaultData.DefaultAilmentIds_Name)]
        public int? AilmentDefinitionID;

        [UiSonSelectorUi(DefaultData.EditorDialogTriggerTabNullable_As_String, 0, "Ailment")]
        public EditorDialogTriggerTab? BaggageGirl;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? DifficultyExclusive;

        [UiSonSelectorUi(DefaultData.SettingDifficultyNullable_As_String)]
        public SettingDifficulty? Difficulty;

        [UiSonElementSelectorUi(nameof(EnergyDataMod), 0, null, "Id", DefaultData.DefaultEnergyNames_Name, DefaultData.DefaultEnergyIds_Name)]
        public int? EnergyDefinitionID;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String, 0, "Puzzle")]
        public PuzzleAffectionType? AffectionType;

        [UiSonSelectorUi(DefaultData.ItemGiveConditionTypeNullable_As_String)]
        public ItemGiveConditionType? GiveConditionType;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 0, null, "Id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public int? GirlDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? NoStaminaCost;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? DateGiftAilment;

        [UiSonElementSelectorUi(nameof(AbilityDataMod), 0, "Puzzle", "Id", DefaultData.DefaultAbilityNames_Name, DefaultData.DefaultAbilityIds_Name)]
        public int? AbilityDefinitionID;

        [UiSonTextEditUi(0, "Puzzle")]
        public int? UseCost;

        [UiSonTextEditUi]
        public string CategoryDescription;

        [UiSonTextEditUi]
        public string CostDescription;

        [UiSonTextEditUi]
        public int? NotifierHeaderIndex;

        public ItemDataMod() { }

        public ItemDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
        }

        public ItemDataMod(int id,
                           string itemName,
                           ItemType? itemType,
                           string itemDescription,
                           int? energyDefinitionID,
                           SpriteInfo itemSpriteInfo,
                           PuzzleAffectionType? affectionType,
                           int? tooltipColorIndex,
                           ItemGiveConditionType? giveConditionType,
                           int? girlDefinitionID,
                           bool? difficultyExclusive,
                           SettingDifficulty? difficulty,
                           bool? storeSectionPreference,
                           int? storeCost,
                           ItemFoodType? foodType,
                           bool? noStaminaCost,
                           ItemShoesType? shoesType,
                           ItemUniqueType? uniqueType,
                           ItemDateGiftType? dateGiftType,
                           bool? dateGiftAilment,
                           int? abilityDefinitionID,
                           int? useCost,
                           EditorDialogTriggerTab? baggageGirl,
                           int? cutsceneDefinitionID,
                           int? ailmentDefinitionID,
                           string categoryDescription,
                           string costDescription,
                           int? notifierHeaderIndex,
                           InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
            ItemName = itemName;
            ItemType = itemType;
            ItemDescription = itemDescription;
            EnergyDefinitionID = energyDefinitionID;
            ItemSpriteInfo = itemSpriteInfo;
            AffectionType = affectionType;
            TooltipColorIndex = tooltipColorIndex;
            GiveConditionType = giveConditionType;
            GirlDefinitionID = girlDefinitionID;
            DifficultyExclusive = difficultyExclusive;
            Difficulty = difficulty;
            StoreSectionPreference = storeSectionPreference;
            StoreCost = storeCost;
            FoodType = foodType;
            NoStaminaCost = noStaminaCost;
            ShoesType = shoesType;
            UniqueType = uniqueType;
            DateGiftType = dateGiftType;
            DateGiftAilment = dateGiftAilment;
            AbilityDefinitionID = abilityDefinitionID;
            UseCost = useCost;
            BaggageGirl = baggageGirl;
            CutsceneDefinitionID = cutsceneDefinitionID;
            AilmentDefinitionID = ailmentDefinitionID;
            CategoryDescription = categoryDescription;
            CostDescription = costDescription;
            NotifierHeaderIndex = notifierHeaderIndex;
        }

        public ItemDataMod(ItemDefinition def, AssetProvider assetProvider)
            : base(def.id, InsertStyle.replace, def.name)
        {
            ItemName = def.itemName;
            ItemType = def.itemType;
            ItemDescription = def.itemDescription;
            AffectionType = def.affectionType;
            TooltipColorIndex = def.tooltipColorIndex;
            GiveConditionType = def.giveConditionType;
            DifficultyExclusive = def.difficultyExclusive;
            Difficulty = def.difficulty;
            StoreSectionPreference = def.storeSectionPreference;
            StoreCost = def.storeCost;
            FoodType = def.foodType;
            NoStaminaCost = def.noStaminaCost;
            ShoesType = def.shoesType;
            UniqueType = def.uniqueType;
            DateGiftType = def.dateGiftType;
            DateGiftAilment = def.dateGiftAilment;
            UseCost = def.useCost;
            BaggageGirl = def.baggageGirl;
            CategoryDescription = def.categoryDescription;
            CostDescription = def.costDescription;
            NotifierHeaderIndex = def.notifierHeaderIndex;

            EnergyDefinitionID = def.energyDefinition?.id ?? -1;
            GirlDefinitionID = def.girlDefinition?.id ?? -1;
            AbilityDefinitionID = def.abilityDefinition?.id ?? -1;
            CutsceneDefinitionID = def.cutsceneDefinition?.id ?? -1;
            AilmentDefinitionID = def.ailmentDefinition?.id ?? -1;

            if (def.itemSprite != null) { ItemSpriteInfo = new SpriteInfo(def.itemSprite, assetProvider); }
        }

        public void SetData(ItemDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for an item");
            ModInterface.Instance.IncreaseLogIndent();

            def.id = Id;

            ValidatedSet.SetValue(ref def.notifierHeaderIndex, NotifierHeaderIndex);
            ValidatedSet.SetValue(ref def.itemType, ItemType);
            ValidatedSet.SetValue(ref def.affectionType, AffectionType);
            ValidatedSet.SetValue(ref def.tooltipColorIndex, TooltipColorIndex);
            ValidatedSet.SetValue(ref def.giveConditionType, GiveConditionType);
            ValidatedSet.SetValue(ref def.difficultyExclusive, DifficultyExclusive);
            ValidatedSet.SetValue(ref def.difficulty, Difficulty);
            ValidatedSet.SetValue(ref def.storeSectionPreference, StoreSectionPreference);
            ValidatedSet.SetValue(ref def.storeCost, StoreCost);
            ValidatedSet.SetValue(ref def.foodType, FoodType);
            ValidatedSet.SetValue(ref def.noStaminaCost, NoStaminaCost);
            ValidatedSet.SetValue(ref def.shoesType, ShoesType);
            ValidatedSet.SetValue(ref def.uniqueType, UniqueType);
            ValidatedSet.SetValue(ref def.dateGiftType, DateGiftType);
            ValidatedSet.SetValue(ref def.dateGiftAilment, DateGiftAilment);
            ValidatedSet.SetValue(ref def.useCost, UseCost);
            ValidatedSet.SetValue(ref def.baggageGirl, BaggageGirl);
            ValidatedSet.SetValue(ref def.cutsceneDefinition, gameDataProvider.GetCutscene(CutsceneDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.ailmentDefinition, gameDataProvider.GetAilment(AilmentDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.energyDefinition, gameDataProvider.GetEnergy(EnergyDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.girlDefinition, gameDataProvider.GetGirl(GirlDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.abilityDefinition, gameDataProvider.GetAbility(AbilityDefinitionID), insertStyle);


            ValidatedSet.SetValue(ref def.itemName, ItemName, insertStyle);
            ValidatedSet.SetValue(ref def.itemDescription, ItemDescription, insertStyle);
            ValidatedSet.SetValue(ref def.categoryDescription, CategoryDescription, insertStyle);
            ValidatedSet.SetValue(ref def.costDescription, CostDescription, insertStyle);
            ValidatedSet.SetValue(ref def.itemSprite, ItemSpriteInfo, insertStyle, gameDataProvider, assetProvider);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
