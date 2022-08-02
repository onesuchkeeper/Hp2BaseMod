// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;

namespace Hp2BaseMod
{
    /// <summary>
    /// Used to define starting point of mods.
    /// The base mod will call the <see cref="IProvideGameDataMods"/> properties after it has called the start method.
    /// Then it calls Started
    /// Must have a parameterless constructor.
    /// </summary>
    public interface IHp2ModStarter : IProvideGameDataMods
    {
        /// <summary>
        /// Starts the mod, called by the Hp2BaseMod before runtime.
        /// </summary>
        /// <param name="modId">The mod's id.</param>
        void Start(int modId);
    }
}
