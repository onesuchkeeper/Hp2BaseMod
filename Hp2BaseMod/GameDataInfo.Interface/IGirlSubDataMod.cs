using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;

namespace Hp2BaseMod.GameDataInfo.Interface
{
    public interface IGirlSubDataMod<T>
    {
        RelativeId Id { get; }
        int LoadPriority { get; }

        /// <summary>
        /// Writes to the definition this modifies
        /// </summary>
        /// <param name="def"></param>
        /// <param name="gameData"></param>
        /// <param name="assetProvider"></param>
        /// <param name="insertStyle"></param>
        /// <param name="girlId"></param>
        void SetData(ref T def,
                     GameDefinitionProvider gameData,
                     AssetProvider assetProvider,
                     InsertStyle insertStyle,
                     RelativeId girlId);
    }
}
