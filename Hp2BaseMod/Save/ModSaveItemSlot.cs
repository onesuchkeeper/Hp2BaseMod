namespace Hp2BaseMod.Save
{
    public struct ModSaveItemSlot
    {
        public int Index;
        public SavedSourceId SavedSourceId;

        public ModSaveItemSlot(SavedSourceId savedSourceId, int index)
        {
            Index = index;
            SavedSourceId = savedSourceId;
        }

        public override string ToString() => $"{SavedSourceId} at index {Index}";
    }
}
