// Hp2BaseMod 2021, by OneSuchKeeper

using UiSon.Attribute;

namespace Hp2BaseMod
{
    /// <summary>
    /// Info for a single assembly to be loaded.
    /// </summary>
    public class ModAssembly
    {
        /// <summary>
        /// Path to assembly file
        /// </summary>
        [UiSonTextEditUi]
        public string Path;

        /// <summary>
        /// Load order priority
        /// </summary>
        [UiSonTextEditUi]
        public int Priority;
    }
}
