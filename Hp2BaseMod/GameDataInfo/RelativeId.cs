namespace Hp2BaseMod.GameDataInfo
{
    public struct RelativeId
    {
        /// <summary>
        /// The id of the source that defined this.
        /// </summary>
        public int SourceId;

        /// <summary>
        /// The id defined by the source.
        /// </summary>
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
