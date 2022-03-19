// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Elements;
using Hp2BaseMod.AssetInfos;
using System.Collections.Generic;

namespace DataModEditor
{
    public class DollOutfitVm : NPCBase
    {
        public IEnumerable<DollPartVm> DollParts => _dollParts;
        private DollPartVm[] _dollParts =
        { new DollPartVm("Outfit Part"),
        new DollPartVm("Alt Outfit Part")};

        public string Name { get; set; } = "null";

        public string Type => _type.ToString();
        private GirlStyleType _type;

        public DollOutfitVm(GirlStyleType type)
        {
            _type = type;
        }

        public void Populate(List<GirlPartInfo> parts, GirlOutfitSubDefinition outfit)
        {
            if (outfit == null)
            {
                return;
            }

            Name = outfit.outfitName;

            var outfitPart = _dollParts[0];
            var altOutfitPart = _dollParts[1];

            if (outfit.partIndexOutfit == -1)
            {
                outfitPart.Populate();
                altOutfitPart.Populate();
            }
            else
            {
                var part = parts[outfit.partIndexOutfit];

                outfitPart.Populate(part);

                if (part.AltPartIndex == -1)
                {
                    altOutfitPart.Populate();
                }
                else
                {
                    altOutfitPart.Populate(parts[part.AltPartIndex]);
                }
            }
        }
    }
}
