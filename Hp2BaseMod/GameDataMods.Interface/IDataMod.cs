// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.ModLoader;

namespace Hp2BaseMod.GameDataMods.Interface
{
    /// <summary>
    /// Interface for serializable information to make a GameData type
    /// </summary>
    /// <typeparam name="T">GameData type</typeparam>
    public interface IDataMod<T>
    {
        int Id { get; }
        bool IsAdditive { get; }

        /// <summary>
        /// Creates a GameData definition
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        void SetData(T def, GameData gameData, AssetProvider prefabProvider);
    }
}
