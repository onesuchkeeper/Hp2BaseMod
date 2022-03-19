// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Elements;
using Hp2BaseMod.AssetInfos;
using System.Collections.Generic;

namespace DataModEditor
{
    public class DollHairstyleVm : NPCBase
    {
        public IEnumerable<DollPartVm> DollParts => _dollParts;
        private DollPartVm[] _dollParts =
        { new DollPartVm("Hair Front"),
        new DollPartVm("Hair Back"),
        new DollPartVm("Hair Front Alt"),
        new DollPartVm("Hair Back Alt") };

        public string Name { get; set; } = "null";

        public string Type => _type.ToString();
        private GirlStyleType _type;

        public DollHairstyleVm(GirlStyleType type)
        {
            _type = type;
        }

        public void Populate(List<GirlPartInfo> parts, GirlHairstyleSubDefinition hairstyle)
        {
            if (hairstyle == null)
            {
                return;
            }

            Name = hairstyle.hairstyleName;

            var front = _dollParts[0];
            var back = _dollParts[1];
            var frontAlt = _dollParts[2];
            var backAlt = _dollParts[3];

            if (hairstyle.partIndexFronthair == -1)
            {
                front.Populate();
                frontAlt.Populate();
            }
            else
            {
                var part = parts[hairstyle.partIndexFronthair];

                front.Populate(part);

                if (part.AltPartIndex == -1)
                {
                    frontAlt.Populate();
                }
                else
                {
                    frontAlt.Populate(parts[part.AltPartIndex]);
                }
            }

            if (hairstyle.partIndexBackhair == -1)
            {
                back.Populate();
                backAlt.Populate();
            }
            else
            {
                var part = parts[hairstyle.partIndexFronthair];

                back.Populate(part);

                if (part.AltPartIndex == -1)
                {
                    backAlt.Populate();
                }
                else
                {
                    backAlt.Populate(parts[part.AltPartIndex]);
                }
            }
        }
    }
}
