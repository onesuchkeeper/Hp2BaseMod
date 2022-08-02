// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.Utility;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a definition
    /// </summary>
    [UiSonGroup("Mod Info", 100, null, DisplayMode.Vertial)]
    public abstract class DataMod
    {
        [UiSonEncapsulatingUi(2, "Mod Info")]
        [UiSonTag("id")]
        public RelativeId Id { get; set; }

        [UiSonTextEditUi(2, "Mod Info")]
        public int LoadPriority { get; set; }

        [UiSonSelectorUi(DefaultData.InsertStyle, 2, "Mod Info")]
        public InsertStyle InsertStyle { get; set; }

        /// <summary>
        /// Required for serialization, it is not recomended that you use this.
        /// Use the constructor with id parameters instead.
        /// </summary>
        public DataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The game data id.</param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public DataMod(RelativeId id, InsertStyle insertStyle, int loadPriority)
        {
            Id = id;
            InsertStyle = insertStyle;
            LoadPriority = loadPriority;
        }
    }
}
