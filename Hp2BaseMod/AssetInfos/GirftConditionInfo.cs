// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a Color
    /// </summary>
    [Serializable]
    public class GiftConditionInfo
    {
        public GiftConditionType Type;
        public ItemDateGiftType DateGiftType;
        public int ItemDefinitionID;
        public bool Inverse;

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

        public GiftCondition ToGiftCondition(GameData gameData)
        {
            var newGC = new GiftCondition();

            newGC.type = Type;
            newGC.dateGiftType = DateGiftType;
            newGC.inverse = Inverse;

            newGC.itemDefinition = gameData.Items.Get(ItemDefinitionID);

            return newGC;
        }
    }
}
