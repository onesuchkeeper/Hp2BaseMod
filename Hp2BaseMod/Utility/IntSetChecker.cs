using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.Utility
{
    /// <summary>
    /// Constructed with a set of integers, effeciently checks inclusion in the set
    /// </summary>
    public class IntSetChecker
    {
        private struct InclusiveRange
        {
            private int _min;
            private int _max;

            public InclusiveRange(int min, int max)
            {
                _min = min;
                _max = max;
            }

            public bool Contains(int value) => _min <= value && _max >= value;
        }

        private List<InclusiveRange> Ranges = new List<InclusiveRange>();

        public IntSetChecker(IEnumerable<int> ints)
        {
            if (ints != null)
            {
                var set = ints.Distinct().OrderBy(x => x);

                var it = set.GetEnumerator();

                if (it.MoveNext())
                {
                    var min = it.Current;
                    var current = min;

                    while (it.MoveNext())
                    {
                        var next = it.Current;

                        // reached the end of a set
                        if (current != next - 1)
                        {
                            Ranges.Add(new InclusiveRange(min, current));
                            current = min = next;
                        }

                        current = next;
                    }

                    // the last range won't have a gap and won't be discovered previously
                    Ranges.Add(new InclusiveRange(min, current));
                }
            }
        }

        public bool Contains(int value) => Ranges.Any(x => x.Contains(value));
    }
}
