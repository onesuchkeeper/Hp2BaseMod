// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Interfaces;
using Hp2BaseMod.GameDataMods;
using System.Linq;

namespace DataModEditor
{
    public class DollOutfitVm : BaseVM
    {
        public string Path { get; set; } = "null";
        public string AltPath { get; set; } = "null";

        //validate these as doubles
        public string X { get; set; } = "null";
        public string Y { get; set; } = "null";

        public string Name { get; set; } = "null";

        public string Type => _type.ToString();
        private GirlStyleType _type;

        // make another constructor for populate
        public DollOutfitVm(GirlStyleType type)
        {
            _type = type;
        }

        public void Populate(GirlDataMod girlDataMod)
        {
            //still needs offsets
            var outfit = girlDataMod.Outfits.FirstOrDefault(x => x.pairHairstyleIndex == (int)_type);

            if (outfit == null)
            {
                return;
            }

            if (outfit.partIndexOutfit == -1)
            {
                Path = AltPath = "null";
            }
            else
            {
                var part = girlDataMod.Parts[outfit.partIndexOutfit];

                Path = part.SpriteInfo?.IsExternal ?? false
                    ? part.SpriteInfo.Path
                    : "null";

                if (part.AltPartIndex == -1)
                {
                    AltPath = "null";
                }
                else
                {
                    var partAlt = girlDataMod.Parts[part.AltPartIndex];

                    AltPath = partAlt.SpriteInfo?.IsExternal ?? false
                        ? partAlt.SpriteInfo.Path
                        : "null";
                }
            }
        }
    }
}
