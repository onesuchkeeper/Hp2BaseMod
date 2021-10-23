using System;
using System.Collections.Generic;
using System.Text;

namespace DataModEditor
{
    public static class Conversion
    {
        public static int NullBoolToInt(bool? x)
        {
            return x.HasValue
                ? x.Value
                    ? 1
                    : 0
                : 2;
        }

        public static bool? IntToNullBool(int x)
        {
            switch (x)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                case 2:
                    return null;
            }

            throw new ArgumentOutOfRangeException(nameof(x));
        }
    }
}
