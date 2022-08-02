namespace Hp2BaseMod.GameDataInfo
{
    public class ExpandedOutfitDefinition : GirlOutfitSubDefinition
    {
        public bool IsNSFW;

        public ExpandedOutfitDefinition()
        {
        }

        public ExpandedOutfitDefinition(GirlOutfitSubDefinition def)
        {
            outfitName = def.outfitName;
            partIndexOutfit = def.partIndexOutfit;
            pairHairstyleIndex = def.pairHairstyleIndex;
            tightlyPaired = def.tightlyPaired;
            hideNipples = def.hideNipples;
            IsNSFW = false;
        }
    }
}
