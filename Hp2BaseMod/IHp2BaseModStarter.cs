// Hp2BaseMod 2021, By OneSuchKeeper

namespace Hp2BaseMod
{
    /// <summary>
    /// Used to define starting point of mods. The base mod will run the start method.
    /// </summary>
    public interface IHp2BaseModStarter
    {
        /// <summary>
        /// Starts the mod, called by the Hp2BaseMod
        /// </summary>
        void Start(GameDataModder gameDataModder);
    }
}
