using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    public class GirlStyleInfo
    {
        [UiSonEncapsulatingUi]
        public RelativeId? OutfitId;

        [UiSonEncapsulatingUi]
        public RelativeId? HairstyleId;

        public void SetData(ref GirlStyleInfo def)
        {
            if (def == null)
            {
                def = Default();
            }

            ValidatedSet.SetValue(ref def.OutfitId, OutfitId, InsertStyle.replace);
            ValidatedSet.SetValue(ref def.HairstyleId, HairstyleId, InsertStyle.replace);
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            OutfitId = getNewId(OutfitId);
            HairstyleId = getNewId(OutfitId);
        }

        public static GirlStyleInfo Default() => new GirlStyleInfo() { OutfitId = RelativeId.Default, HairstyleId = RelativeId.Default };
    }
}
