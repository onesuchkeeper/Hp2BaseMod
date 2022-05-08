// Hp2BaseModTweaks 2022, By OneSuchKeeper

namespace Hp2BaseModTweaks
{
    internal static class Utility
    {
        public static int WrapIndex(int index, int max)
        {
            if (index < 0)
            {
                index = max;
            }
            else if (index > max)
            {
                index = 0;
            }

            return index;
        }
    }
}
