// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a Color
    /// </summary>
    public class GiftConditionInfo : IGameDataInfo<GiftCondition>
    {
        [UiSonSelectorUi(DefaultData.GiftConditionTypeNullable_As_String)]
        public GiftConditionType? Type;

        [UiSonSelectorUi(DefaultData.ItemDateGiftTypeNullable_As_String)]
        public ItemDateGiftType? DateGiftType;

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, null, "Id", DefaultData.DefaultItemNames, DefaultData.DefaultItemIds)]
        public int? ItemDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? Inverse;

        public GiftConditionInfo() { }

        public GiftConditionInfo(GiftConditionType type,
                                 ItemDateGiftType dateGiftType,
                                 int itemDefinitionID,
                                 bool inverse)
        {
            Type = type;
            ItemDefinitionID = itemDefinitionID;
            DateGiftType = dateGiftType;
            Inverse = inverse;
        }

        public GiftConditionInfo(GiftCondition giftCondition)
        {
            if (giftCondition == null) { throw new ArgumentNullException(nameof(giftCondition)); }

            Type = giftCondition.type;
            ItemDefinitionID = giftCondition.itemDefinition?.id ?? -1;
            DateGiftType = giftCondition.dateGiftType;
            Inverse = giftCondition.inverse;
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref GiftCondition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<GiftCondition>();
            }

            ValidatedSet.SetValue(ref def.type, Type);
            ValidatedSet.SetValue(ref def.dateGiftType, DateGiftType);
            ValidatedSet.SetValue(ref def.inverse, Inverse);

            ValidatedSet.SetValue(ref def.itemDefinition, gameDataProvider.GetItem(ItemDefinitionID), insertStyle);
        }
    }
}
