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
        // these three are only used to generate the category descryption,
        // which we're doing manually for the expansion
        public EditorDialogTriggerTab? baggageGirl;
        public ItemShoesType? shoesType;
        public ItemUniqueType? uniqueType;

        [UiSonSelectorUi(DefaultData.ItemTypeNullable_As_String, 0, "Type")]
        public ItemType? itemType;

        [UiSonSelectorUi(DefaultData.ItemDateGiftTypeNullable_As_String, 0, "Type")]
        public ItemDateGiftType? dateGiftType;

        [UiSonSelectorUi(DefaultData.ItemFoodTypeNullable_As_String, 0, "Type")]
        public ItemFoodType? foodType;

        [UiSonMemberElement(0, "Appearance")]
        public SpriteInfo ItemSpriteInfo;

        [UiSonTextEditUi(0, "Appearance")]
        public string itemName;

        [UiSonTextEditUi(0, "Appearance")]
        public string itemDescription;

        [UiSonTextEditUi(0, "Appearance")]
        public int? tooltipColorIndex;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name, 0, "Store")]
        public bool? storeSectionPreference;

        [UiSonTextEditUi(0, "Store")]
        public int? storeCost;

        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, "Ailment", "Id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public int? cutsceneDefinitionID;

        [UiSonElementSelectorUi(nameof(AilmentDataMod), 0, "Ailment", "Id", DefaultData.DefaultAilmentNames_Name, DefaultData.DefaultAilmentIds_Name)]
        public int? ailmentDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? difficultyExclusive;

        [UiSonSelectorUi(DefaultData.SettingDifficultyNullable_As_String)]
        public SettingDifficulty? difficulty;

        [UiSonElementSelectorUi(nameof(EnergyDataMod), 0, null, "Id", DefaultData.DefaultEnergyNames_Name, DefaultData.DefaultEnergyIds_Name)]
        public int? energyDefinitionID;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String, 0, "Puzzle")]
        public PuzzleAffectionType? affectionType;

        [UiSonSelectorUi(DefaultData.ItemGiveConditionTypeNullable_As_String)]
        public ItemGiveConditionType? giveConditionType;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 0, null, "Id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public int? girlDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? noStaminaCost;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? dateGiftAilment;

        [UiSonElementSelectorUi(nameof(AbilityDataMod), 0, "Puzzle", "Id", DefaultData.DefaultAbilityNames_Name, DefaultData.DefaultAbilityIds_Name)]
        public int? abilityDefinitionID;

        [UiSonTextEditUi(0, "Puzzle")]
        public int? useCost;

        [UiSonTextEditUi]
        public string categoryDescription;

        [UiSonTextEditUi]
        public string costDescription;

        [UiSonTextEditUi]
        public int? notifierHeaderIndex;

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
            this.itemName = itemName;
            this.itemType = itemType;
            this.itemDescription = itemDescription;
            this.energyDefinitionID = energyDefinitionID;
            ItemSpriteInfo = itemSpriteInfo;
            this.affectionType = affectionType;
            this.tooltipColorIndex = tooltipColorIndex;
            this.giveConditionType = giveConditionType;
            this.girlDefinitionID = girlDefinitionID;
            this.difficultyExclusive = difficultyExclusive;
            this.difficulty = difficulty;
            this.storeSectionPreference = storeSectionPreference;
            this.storeCost = storeCost;
            this.foodType = foodType;
            this.noStaminaCost = noStaminaCost;
            this.shoesType = shoesType;
            this.uniqueType = uniqueType;
            this.dateGiftType = dateGiftType;
            this.dateGiftAilment = dateGiftAilment;
            this.abilityDefinitionID = abilityDefinitionID;
            this.useCost = useCost;
            this.baggageGirl = baggageGirl;
            this.cutsceneDefinitionID = cutsceneDefinitionID;
            this.ailmentDefinitionID = ailmentDefinitionID;
            this.categoryDescription = categoryDescription;
            this.costDescription = costDescription;
            this.notifierHeaderIndex = notifierHeaderIndex;
        }

        public ItemDataMod(ItemDefinition def, AssetProvider assetProvider)
            : base(def.id, InsertStyle.replace, def.name)
        {
            itemName = def.itemName;
            itemType = def.itemType;
            itemDescription = def.itemDescription;
            affectionType = def.affectionType;
            tooltipColorIndex = def.tooltipColorIndex;
            giveConditionType = def.giveConditionType;
            difficultyExclusive = def.difficultyExclusive;
            difficulty = def.difficulty;
            storeSectionPreference = def.storeSectionPreference;
            storeCost = def.storeCost;
            foodType = def.foodType;
            noStaminaCost = def.noStaminaCost;
            shoesType = def.shoesType;
            uniqueType = def.uniqueType;
            dateGiftType = def.dateGiftType;
            dateGiftAilment = def.dateGiftAilment;
            useCost = def.useCost;
            baggageGirl = def.baggageGirl;
            costDescription = def.costDescription;
            notifierHeaderIndex = def.notifierHeaderIndex;

            // category descryption will now be prioritized, so I'm setting it in the defaults to emphisize
            switch (def.itemType)
            {
                case ItemType.FRUIT:
                case ItemType.SMOOTHIE:
                    categoryDescription = StringUtils.Titleize(def.affectionType.ToString());
                    break;
                case ItemType.FOOD:
                    categoryDescription = StringUtils.Titleize(def.foodType.ToString());
                    break;
                case ItemType.SHOES:
                    categoryDescription = StringUtils.Titleize(def.shoesType.ToString());
                    break;
                case ItemType.UNIQUE_GIFT:
                    categoryDescription = StringUtils.Titleize(def.uniqueType.ToString());
                    break;
                case ItemType.DATE_GIFT:
                    categoryDescription = StringUtils.Titleize(def.dateGiftType.ToString());
                    break;
                case ItemType.BAGGAGE:
                    categoryDescription = StringUtils.Titleize(def.baggageGirl.ToString());
                    break;
                case ItemType.MISC:
                    categoryDescription = def.categoryDescription;
                    break;
            }

            energyDefinitionID = def.energyDefinition?.id ?? -1;
            girlDefinitionID = def.girlDefinition?.id ?? -1;
            abilityDefinitionID = def.abilityDefinition?.id ?? -1;
            cutsceneDefinitionID = def.cutsceneDefinition?.id ?? -1;
            ailmentDefinitionID = def.ailmentDefinition?.id ?? -1;

            if (def.itemSprite != null) { ItemSpriteInfo = new SpriteInfo(def.itemSprite, assetProvider); }
        }

        public void SetData(ItemDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for an item");
            ModInterface.Instance.IncreaseLogIndent();

            def.id = Id;

            ValidatedSet.SetValue(ref def.notifierHeaderIndex, notifierHeaderIndex);
            ValidatedSet.SetValue(ref def.itemType, itemType);
            ValidatedSet.SetValue(ref def.affectionType, affectionType);
            ValidatedSet.SetValue(ref def.tooltipColorIndex, tooltipColorIndex);
            ValidatedSet.SetValue(ref def.giveConditionType, giveConditionType);
            ValidatedSet.SetValue(ref def.difficultyExclusive, difficultyExclusive);
            ValidatedSet.SetValue(ref def.difficulty, difficulty);
            ValidatedSet.SetValue(ref def.storeSectionPreference, storeSectionPreference);
            ValidatedSet.SetValue(ref def.storeCost, storeCost);
            ValidatedSet.SetValue(ref def.foodType, foodType);
            ValidatedSet.SetValue(ref def.noStaminaCost, noStaminaCost);
            ValidatedSet.SetValue(ref def.shoesType, shoesType);
            ValidatedSet.SetValue(ref def.uniqueType, uniqueType);
            ValidatedSet.SetValue(ref def.dateGiftType, dateGiftType);
            ValidatedSet.SetValue(ref def.dateGiftAilment, dateGiftAilment);
            ValidatedSet.SetValue(ref def.useCost, useCost);
            ValidatedSet.SetValue(ref def.baggageGirl, baggageGirl);
            ValidatedSet.SetValue(ref def.cutsceneDefinition, gameDataProvider.GetCutscene(cutsceneDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.ailmentDefinition, gameDataProvider.GetAilment(ailmentDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.energyDefinition, gameDataProvider.GetEnergy(energyDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.girlDefinition, gameDataProvider.GetGirl(girlDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.abilityDefinition, gameDataProvider.GetAbility(abilityDefinitionID), insertStyle);

            ValidatedSet.SetValue(ref def.itemName, itemName, insertStyle);
            ValidatedSet.SetValue(ref def.itemDescription, itemDescription, insertStyle);
            ValidatedSet.SetValue(ref def.costDescription, costDescription, insertStyle);
            ValidatedSet.SetValue(ref def.itemSprite, ItemSpriteInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.categoryDescription, categoryDescription, insertStyle);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
