using System.Collections.Generic;

namespace Hp2BaseModTweaks
{
    public class CreditsConfig
    {
        public string modImagePath;
        public List<CreditsEntry> CreditsEntries;
    }

    public struct CreditsEntry
    {
        public string creditButtonImagePath;
        public string creditButtonImageOverPath;
        public string redirectLink;
    }
}
