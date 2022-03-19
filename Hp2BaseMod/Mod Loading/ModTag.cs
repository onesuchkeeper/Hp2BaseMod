// Hp2BaseMod 2021, By OneSuchKeeper

using UiSon.Attribute;

namespace Hp2BaseMod
{
    /// <summary>
    /// Represetns a tag entry for function mods to look up. In place of a dict and KeyValuePair for the UiSon attributes
    /// </summary>
    public class ModTag
    {
        /// <summary>
        /// The tag's name
        /// </summary>
        [UiSonTextEditUi]
        public string Name;

        /// <summary>
        /// The tag's Value
        /// </summary>
        [UiSonTextEditUi]
        public string Value;
    }
}
