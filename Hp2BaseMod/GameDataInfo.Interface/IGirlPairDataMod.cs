namespace Hp2BaseMod.GameDataInfo.Interface
{
    public interface IGirlPairDataMod : IGameDataMod<GirlPairDefinition>
    {
        PairStyleInfo GetStyles();
    }
}
