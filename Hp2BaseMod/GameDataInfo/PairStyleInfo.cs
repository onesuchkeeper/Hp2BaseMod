﻿using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    public class PairStyleInfo
    {
        [UiSonEncapsulatingUi]
        public GirlStyleInfo MeetingGirlOne;

        [UiSonEncapsulatingUi]
        public GirlStyleInfo MeetingGirlTwo;

        [UiSonEncapsulatingUi]
        public GirlStyleInfo SexGirlOne;

        [UiSonEncapsulatingUi]
        public GirlStyleInfo SexGirlTwo;

        public void SetData(ref PairStyleInfo def)
        {
            MeetingGirlOne?.SetData(ref def.MeetingGirlOne);
            MeetingGirlTwo?.SetData(ref def.MeetingGirlTwo);
            SexGirlOne?.SetData(ref def.SexGirlOne);
            SexGirlTwo?.SetData(ref def.SexGirlTwo);
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            MeetingGirlOne?.ReplaceRelativeIds(getNewId);
            MeetingGirlTwo?.ReplaceRelativeIds(getNewId);
            SexGirlOne?.ReplaceRelativeIds(getNewId);
            SexGirlTwo?.ReplaceRelativeIds(getNewId);
        }
    }
}
