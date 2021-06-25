// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.GameDataMods.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a ItemDefinition
    /// </summary>
    [Serializable]
    public class ItemDataMod : DataMod<ItemDefinition>
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

		public ItemDataMod(int id, bool isAdditive)
			: base(id, isAdditive)
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
						   bool isAdditive = false)
			:base(id, isAdditive)
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
			: base(def.id, false)
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

		public override void SetData(ItemDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

			Access.NullableSet(ref def.notifierHeaderIndex, NotifierHeaderIndex);
			Access.NullableSet(ref def.itemType, ItemType);
			Access.NullableSet(ref def.affectionType, AffectionType);
			Access.NullableSet(ref def.tooltipColorIndex, TooltipColorIndex);
			Access.NullableSet(ref def.giveConditionType, GiveConditionType);
			Access.NullableSet(ref def.difficultyExclusive, DifficultyExclusive);
			Access.NullableSet(ref def.difficulty, Difficulty);
			Access.NullableSet(ref def.storeSectionPreference, StoreSectionPreference);
			Access.NullableSet(ref def.storeCost, StoreCost);
			Access.NullableSet(ref def.foodType, FoodType);
			Access.NullableSet(ref def.noStaminaCost, NoStaminaCost);
			Access.NullableSet(ref def.shoesType, ShoesType);
			Access.NullableSet(ref def.uniqueType, UniqueType);
			Access.NullableSet(ref def.dateGiftType, DateGiftType);
			Access.NullableSet(ref def.dateGiftAilment, DateGiftAilment);
			Access.NullableSet(ref def.useCost, UseCost);
			Access.NullableSet(ref def.baggageGirl, BaggageGirl);

			if (CutsceneDefinitionID.HasValue) { def.cutsceneDefinition = gameData.Cutscenes.Get(CutsceneDefinitionID.Value); }
			if (AilmentDefinitionID.HasValue) { def.ailmentDefinition = gameData.Ailments.Get(AilmentDefinitionID.Value); }
			if (EnergyDefinitionID.HasValue) { def.energyDefinition = gameData.Energy.Get(EnergyDefinitionID.Value); }
			if (GirlDefinitionID.HasValue) { def.girlDefinition = gameData.Girls.Get(GirlDefinitionID.Value); }
			if (AbilityDefinitionID.HasValue) { def.abilityDefinition = gameData.Abilities.Get(AbilityDefinitionID.Value); }
			

			if (IsAdditive)
            {
				Access.NullSet(ref def.itemName,ItemName);
				Access.NullSet(ref def.itemDescription, ItemDescription);
				Access.NullSet(ref def.categoryDescription, CategoryDescription);
				Access.NullSet(ref def.costDescription, CostDescription);
				if (ItemSpriteInfo != null) { def.itemSprite = ItemSpriteInfo.ToSprite(assetProvider); }
			}
            else
            {
				def.itemName = ItemName;
				def.itemDescription = ItemDescription;
				def.categoryDescription = CategoryDescription;
				def.costDescription = CostDescription;
				def.itemSprite = ItemSpriteInfo?.ToSprite(assetProvider);
			}
		}
    }
}
