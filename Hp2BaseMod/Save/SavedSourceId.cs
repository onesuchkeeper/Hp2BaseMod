using Hp2BaseMod.Extension.StringExtension;
using Hp2BaseMod.GameDataInfo;

namespace Hp2BaseMod.Save
{
    public struct SavedSourceId
    {
        public SourceIdentifier SourceId;
        public int LocalId;

        public SavedSourceId(SourceIdentifier sourceId, int localId)
        {
            SourceId = sourceId;
            LocalId = localId;
        }

        public RelativeId? ToRelativeId()
        {
            var mod = ModInterface.FindMod(SourceId);

            return mod == null
                ? null
                : (RelativeId?)new RelativeId(mod.Id, LocalId);
        }

        public override string ToString() => $"{LocalId} from {SourceId}";
    }
}
