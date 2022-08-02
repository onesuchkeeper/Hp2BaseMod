using System.Collections.Generic;

namespace Hp2BaseMod.GameDataInfo.Interface
{
    public interface ILocationDataMod : IGameDataMod<LocationDefinition>
    {
        IEnumerable<(RelativeId, GirlStyleInfo)> GetStyles();
    }
}
