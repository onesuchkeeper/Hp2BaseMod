// Hp2BaseModLoader 2021, by OneSuchKeeper

namespace Hp2BaseMod
{
    public class ConfigEntry
    {
        public string path { get; }
        public int priority { get; }
        public bool IsValid { get; }

        public ConfigEntry(string str)
        {
            string prioStr = "";
            bool comma = false;
            foreach (var c in str)
            {
                if (c == ',')
                {
                    comma = true;
                }
                else if (comma)
                {
                    prioStr += c;
                }
                else
                {
                    path += c;
                }
            }

            if (!path.EndsWith(".dll")
               || prioStr == "")
            {
                priority = -1;
                IsValid = false;
            }
            else
            {
                priority = int.Parse(prioStr);
                IsValid = true;
            }
        }
    }
}
