// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a ItemDefinition
    /// </summary>
    public class ItemDataMod : DataMod, IGameDataMod<ItemDefinition>
    {
        // these three are only used to generate the category descryption,
        // which we're doing manually for the expansion
        //public EditorDialogTriggerTab? baggageGirl;
        //public ItemShoesType? shoesType;
        //public ItemUniqueType? uniqueType;

        public ItemType? ItemType;

        public ItemDateGiftType? DateGiftType;

        public ItemFoodType? FoodType;

        public SpriteInfo ItemSpriteInfo;

        public string ItemName;

        public string ItemDescription;

        public int? TooltipColorIndex;

        public bool? StoreSectionPreference;

        public int? StoreCost;

        public RelativeId? CutsceneDefinitionID;

        public RelativeId? AilmentDefinitionID;

        public bool? DifficultyExclusive;

        public SettingDifficulty? Difficulty;

        public RelativeId? EnergyDefinitionID;

        public PuzzleAffectionType? AffectionType;

        public ItemGiveConditionType? GiveConditionType;

        public RelativeId? GirlDefinitionID;

        public bool? NoStaminaCost;

        public bool? DateGiftAilment;

        public RelativeId? AbilityDefinitionID;

        public int? UseCost;

        public string CategoryDescription;

        public string CostDescription;

        public int? NotifierHeaderIndex;

        /// <inheritdoc/>
        public ItemDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public ItemDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        internal ItemDataMod(ItemDefinition def, AssetProvider assetProvider)
            : base(new RelativeId(def), InsertStyle.replace, 0)
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
            DateGiftType = def.dateGiftType;
            DateGiftAilment = def.dateGiftAilment;
            UseCost = def.useCost;
            CostDescription = def.costDescription;
            NotifierHeaderIndex = def.notifierHeaderIndex;

            // category descryption will now be prioritized, so I'm setting it in the defaults to emphisize
            switch (def.itemType)
            {
                case global::ItemType.FRUIT:
                case global::ItemType.SMOOTHIE:
                    CategoryDescription = StringUtils.Titleize(def.affectionType.ToString());
                    break;
                case global::ItemType.FOOD:
                    CategoryDescription = StringUtils.Titleize(def.foodType.ToString());
                    break;
                case global::ItemType.SHOES:
                    CategoryDescription = StringUtils.Titleize(def.shoesType.ToString());
                    break;
                case global::ItemType.UNIQUE_GIFT:
                    CategoryDescription = StringUtils.Titleize(def.uniqueType.ToString());
                    break;
                case global::ItemType.DATE_GIFT:
                    CategoryDescription = StringUtils.Titleize(def.dateGiftType.ToString());
                    break;
                case global::ItemType.BAGGAGE:
                    CategoryDescription = StringUtils.Titleize(def.baggageGirl.ToString());
                    break;
                case global::ItemType.MISC:
                    CategoryDescription = def.categoryDescription;
                    break;
            }

            EnergyDefinitionID = new RelativeId(def.energyDefinition);
            GirlDefinitionID = new RelativeId(def.girlDefinition);
            AbilityDefinitionID = new RelativeId(def.abilityDefinition);
            CutsceneDefinitionID = new RelativeId(def.cutsceneDefinition);
            AilmentDefinitionID = new RelativeId(def.ailmentDefinition);

            if (def.itemSprite != null) { ItemSpriteInfo = new SpriteInfo(def.itemSprite, assetProvider); }
        }

        /// <inheritdoc/>
        public void SetData(ItemDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider)
        {
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
            ValidatedSet.SetValue(ref def.dateGiftType, DateGiftType);
            ValidatedSet.SetValue(ref def.dateGiftAilment, DateGiftAilment);
            ValidatedSet.SetValue(ref def.useCost, UseCost);
            ValidatedSet.SetValue(ref def.cutsceneDefinition, gameDataProvider.GetCutscene(CutsceneDefinitionID), InsertStyle);
            ValidatedSet.SetValue(ref def.ailmentDefinition, gameDataProvider.GetAilment(AilmentDefinitionID), InsertStyle);
            ValidatedSet.SetValue(ref def.energyDefinition, gameDataProvider.GetEnergy(EnergyDefinitionID), InsertStyle);
            ValidatedSet.SetValue(ref def.girlDefinition, gameDataProvider.GetGirl(GirlDefinitionID), InsertStyle);
            ValidatedSet.SetValue(ref def.abilityDefinition, gameDataProvider.GetAbility(AbilityDefinitionID), InsertStyle);

            ValidatedSet.SetValue(ref def.itemName, ItemName, InsertStyle);
            ValidatedSet.SetValue(ref def.itemDescription, ItemDescription, InsertStyle);
            ValidatedSet.SetValue(ref def.costDescription, CostDescription, InsertStyle);
            ValidatedSet.SetValue(ref def.itemSprite, ItemSpriteInfo, InsertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.categoryDescription, CategoryDescription, InsertStyle);
        }

        /// <inheritdoc/>
        public override void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            Id = getNewId(Id) ?? Id;

            CutsceneDefinitionID = getNewId(CutsceneDefinitionID);
            AilmentDefinitionID = getNewId(AilmentDefinitionID);
            EnergyDefinitionID = getNewId(EnergyDefinitionID);
            GirlDefinitionID = getNewId(GirlDefinitionID);
            AbilityDefinitionID = getNewId(AbilityDefinitionID);
        }
    }
}
