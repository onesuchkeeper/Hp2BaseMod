// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a ItemDefinition
    /// </summary>
    [UiSonElement]
    public class ItemDataMod : DataMod, IGameDataMod<ItemDefinition>
    {
        public string ItemName;
        public ItemType? ItemType;
        public string ItemDescription;
        public int? EnergyDefinitionID;
        public SpriteInfo ItemSpriteInfo;
        public PuzzleAffectionType? AffectionType;
        public int? TooltipColorIndex;
        public ItemGiveConditionType? GiveConditionType;
        public int? GirlDefinitionID;
        public bool? DifficultyExclusive;
        public SettingDifficulty? Difficulty;
        public bool? StoreSectionPreference;
        public int? StoreCost;
        public ItemFoodType? FoodType;
        public bool? NoStaminaCost;
        public ItemShoesType? ShoesType;
        public ItemUniqueType? UniqueType;
        public ItemDateGiftType? DateGiftType;
        public bool? DateGiftAilment;
        public int? AbilityDefinitionID;
        public int? UseCost;
        public EditorDialogTriggerTab? BaggageGirl;
        public int? CutsceneDefinitionID;
        public int? AilmentDefinitionID;
        public string CategoryDescription;
        public string CostDescription;
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
        }
    }
}
