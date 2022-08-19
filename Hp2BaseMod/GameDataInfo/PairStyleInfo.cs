using System;

namespace Hp2BaseMod.GameDataInfo
{
    public class PairStyleInfo
    {
        public GirlStyleInfo MeetingGirlOne;

        public GirlStyleInfo MeetingGirlTwo;

        public GirlStyleInfo SexGirlOne;

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
