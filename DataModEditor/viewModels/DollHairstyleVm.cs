// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Interfaces;
using Hp2BaseMod.GameDataMods;
using System.Linq;

namespace DataModEditor
{
    public class DollHairstyleVm : BaseVM
    {
        public string Front { get; set; } = "null";
        public string Back { get; set; } = "null";

        public string AltFront { get; set; } = "null";
        public string AltBack { get; set; } = "null";

        //validate these as doubles, so I actually need 4 of theses... :/
        public string X { get; set; } = "null";
        public string Y { get; set; } = "null";

        public string Name { get; set; } = "null";

        public string Type => _type.ToString();
        private GirlStyleType _type;

        // make another constructor for populate
        public DollHairstyleVm(GirlStyleType type)
        {
            _type = type;
        }

        public void Populate(GirlDataMod girlDataMod)
        {
            //still needs offsets
            var hairstyle = girlDataMod.Hairstyles.FirstOrDefault(x => x.pairOutfitIndex == (int)_type);

            if (hairstyle == null)
            {
                return;
            }

            if (hairstyle.partIndexFronthair == -1)
            {
                Front = AltFront = "null";
            }
            else
            {
                var front = girlDataMod.Parts[hairstyle.partIndexFronthair];

                Front = front.SpriteInfo?.IsExternal ?? false
                    ? front.SpriteInfo.Path
                    : "null";

                if (front.AltPartIndex == -1)
                {
                    AltFront = "null";
                }
                else
                {
                    var frontAlt = girlDataMod.Parts[front.AltPartIndex];

                    AltFront = frontAlt.SpriteInfo?.IsExternal ?? false
                        ? frontAlt.SpriteInfo.Path
                        : "null";
                }
            }

            if (hairstyle.partIndexBackhair == -1)
            {
                Back = AltBack = "null";
            }
            else
            {
                var back = girlDataMod.Parts[hairstyle.partIndexBackhair];

                Back = back.SpriteInfo?.IsExternal ?? false
                    ? back.SpriteInfo.Path
                    : "null";

                if (back.AltPartIndex == -1)
                {
                    AltFront = "null";
                }
                else
                {
                    var backAlt = girlDataMod.Parts[back.AltPartIndex];

                    AltBack = backAlt.SpriteInfo?.IsExternal ?? false
                        ? backAlt.SpriteInfo.Path
                        : "null";
                }
            }
        }
    }
}
