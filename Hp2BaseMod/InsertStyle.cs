// Hp2BaseMod 2022, By OneSuchKeeper

namespace Hp2BaseMod
{
    /// <summary>
    /// Dictates the way a data mod is loaded into an exsisting data instance.
    /// replace: assigns the value to that of the mod if the mod's value isn't null. For collections assigns the elements, not the collection itself.
    /// append: appends the mod's value to the collection. If the collection is null assigns it instead.
    /// prepend: prepends the mod's value to the collection. If the collection is null assigns it instead.
    /// assignNull: if the value is nullable, assigns the value to that of the mod even if the mod's value is null. Will not assign null to value types.
    /// </summary>
    public enum InsertStyle
    {
        assignNull, append, prepend, replace
    }
}
