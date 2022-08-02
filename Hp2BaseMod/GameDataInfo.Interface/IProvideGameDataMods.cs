// Hp2BaseMod 2022, By OneSuchKeeper

using System.Collections.Generic;

namespace Hp2BaseMod.GameDataInfo.Interface
{
    /// <summary>
    /// Provides game data mods.
    /// </summary>
    public interface IProvideGameDataMods
    {
        IEnumerable<IGameDataMod<AbilityDefinition>> AbilityDataMods { get; }
        IEnumerable<IGameDataMod<AilmentDefinition>> AilmentDataMods { get; }
        IEnumerable<IGameDataMod<CodeDefinition>> CodeDataMods { get; }
        IEnumerable<IGameDataMod<CutsceneDefinition>> CutsceneDataMods { get; }
        IEnumerable<IGameDataMod<DialogTriggerDefinition>> DialogTriggerDataMods { get; }
        IEnumerable<IGameDataMod<DlcDefinition>> DlcDataMods { get; }
        IEnumerable<IGameDataMod<EnergyDefinition>> EnergyDataMods { get; }
        IEnumerable<IGirlDataMod> GirlDataMods { get; }
        IEnumerable<IGirlPairDataMod> GirlPairDataMods { get; }
        IEnumerable<IGameDataMod<ItemDefinition>> ItemDataMods { get; }
        IEnumerable<ILocationDataMod> LocationDataMods { get; }
        IEnumerable<IGameDataMod<PhotoDefinition>> PhotoDataMods { get; }
        IEnumerable<IGameDataMod<QuestionDefinition>> QuestionDataMods { get; }
        IEnumerable<IGameDataMod<TokenDefinition>> TokenDataMods { get; }
    }
}
