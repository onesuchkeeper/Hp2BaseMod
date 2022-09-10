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

        public override int GetHashCode()
        {
            int hashCode = -21478398;
            hashCode = hashCode * -1521134295 + SourceId.GetHashCode();
            hashCode = hashCode * -1521134295 + LocalId.GetHashCode();
            return hashCode;
        }

        public static bool operator !=(RelativeId x, RelativeId y)
        {
            return !(x == y);
        }

        public static bool operator ==(RelativeId x, RelativeId y)
        {
            return x.SourceId == y.SourceId
                   && x.LocalId == y.LocalId;
        }

        public override string ToString() => $"(Source: {SourceId}, Local: {LocalId})";
    }
}
