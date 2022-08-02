using System;
using System.Collections.Generic;

namespace Hp2BaseMod.GameDataInfo.Interface
{
    public interface IGirlDataMod : IGameDataMod<GirlDefinition>
    {
        IEnumerable<IGirlSubDataMod<GirlPartSubDefinition>> GetPartMods();
        IEnumerable<IGirlSubDataMod<ExpandedHairstyleDefinition>> GetHairstyles();
        IEnumerable<IGirlSubDataMod<ExpandedOutfitDefinition>> GetOutfits();
        IEnumerable<Tuple<RelativeId, IEnumerable<IGirlSubDataMod<DialogLine>>>> GetLinesByDialogTriggerId();
    }
}
