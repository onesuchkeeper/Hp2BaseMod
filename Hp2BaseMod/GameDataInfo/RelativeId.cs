using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    [UiSonArray("SourceIdNames", new object[] { "Game", "Mod" })]
    [UiSonArray("SourceIdIdentifiers", new object[] { -1, -2 })]
    public struct RelativeId
    {
        /// <summary>
        /// The id of the source that defined this.
        /// </summary>
        [UiSonSelectorUi("SourceIdNames", 0, null, "SourceIdIdentifiers")]
        public int SourceId;

        /// <summary>
        /// The id defined by the source.
        /// </summary>
        [UiSonTextEditUi]
        public int LocalId;

        public RelativeId(int sourceId, int localId)
        {
            SourceId = sourceId;
            LocalId = localId;
        }

        internal RelativeId(Definition def)
        {
            SourceId = -1;
            LocalId = def?.id ?? -1;
        }

        public static RelativeId Default => new RelativeId(-1, -1);
        public static RelativeId Zero => new RelativeId(-1, 0);

        public override string ToString() => $"(Source: {SourceId}, Local: {LocalId})";

        public static implicit operator int(RelativeId id) => id.LocalId;
        public static implicit operator RelativeId(int localId) => new RelativeId() { SourceId = -1, LocalId = localId };
    }
}
