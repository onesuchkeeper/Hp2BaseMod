// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a Color
    /// </summary>
    public class GiftConditionInfo : IGameDefinitionInfo<GiftCondition>
    {
        public GiftConditionType? Type;

        public ItemDateGiftType? DateGiftType;

        public RelativeId? ItemDefinitionID;

        public bool? Inverse;

        /// <summary>
        /// Constructor
        /// </summary>
        public GiftConditionInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public GiftConditionInfo(GiftCondition giftCondition)
        {
            if (giftCondition == null) { throw new ArgumentNullException(nameof(giftCondition)); }

            Type = giftCondition.type;
            ItemDefinitionID = new RelativeId(giftCondition.itemDefinition);
            DateGiftType = giftCondition.dateGiftType;
            Inverse = giftCondition.inverse;
        }

        /// <inheritdoc/>
        public void SetData(ref GiftCondition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
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

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            ItemDefinitionID = getNewId(ItemDefinitionID);
        }
    }
}
