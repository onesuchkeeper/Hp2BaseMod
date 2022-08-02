namespace Hp2BaseMod.GameDataInfo
{
    public class ExpandedHairstyleDefinition : GirlHairstyleSubDefinition
    {
        public bool IsNSFW;

        public ExpandedHairstyleDefinition()
        {

        }

        public ExpandedHairstyleDefinition(GirlHairstyleSubDefinition def)
        {
            hairstyleName = def.hairstyleName;
            partIndexFronthair = def.partIndexFronthair;
            partIndexBackhair = def.partIndexBackhair;
            pairOutfitIndex = def.pairOutfitIndex;
            tightlyPaired = def.tightlyPaired;
            hideSpecials = def.hideSpecials;
            IsNSFW = false;
        }
    }
}
