using System;
using System.Collections.Generic;
using System.Text;

namespace Hp2BaseMod.GameDataInfo.Interface
{
    public interface IGirlPairDataMod : IGameDataMod<GirlPairDefinition>
    {
        PairStyleInfo GetStyles();
    }
}
