using System;

namespace Hp2BaseMod.Extension.StringExtension
{
    public static partial class StringExtension
    {
        public static Version ToVersion(this string value)
        {
            Version version = null;

            if (!Version.TryParse(value, out version))
            {
                version = new Version();
            }

            return version;
        }
    }
}
