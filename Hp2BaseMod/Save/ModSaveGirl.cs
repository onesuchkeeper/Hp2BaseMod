using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.Save
{
    public class ModSaveGirl
    {
        public SavedSourceId? HairstyleId;
        public SavedSourceId? OutfitId;

        public List<SavedSourceId> LearnedBaggage = new List<SavedSourceId>();
        public List<SavedSourceId> ReceivedUniques = new List<SavedSourceId>();
        public List<SavedSourceId> ReceivedShoes = new List<SavedSourceId>();
        public List<SavedSourceId> LearnedFavs = new List<SavedSourceId>();
        public List<SavedSourceId> RecentHerQuestions = new List<SavedSourceId>();
        public List<SavedSourceId> UnlockedOutfits = new List<SavedSourceId>();
        public List<SavedSourceId> UnlockedHairstyles = new List<SavedSourceId>();
        public List<ModSaveItemSlot> DateGiftSlots = new List<ModSaveItemSlot>();

        // maybe better sort these by mod for fewer lookups like in ModSaveFile, but I don't feel like it right now...

        public void Strip(SaveFileGirl saveFileGirl)
        {
            var girlId = ModInterface.Data.GetDataId(GameDataType.Girl, saveFileGirl.girlId);

            // current hairstyle
            var hairstyleId = ModInterface.Data.GetHairstyleId(girlId, saveFileGirl.hairstyleIndex);
            if (hairstyleId.SourceId != -1)
            {
                saveFileGirl.hairstyleIndex = -1;

                var mod = ModInterface.FindMod(hairstyleId.SourceId);

                if (mod == null)
                {
                    ModInterface.Log.LogMissingIdError($"Attempting to save hairstyle for girl {girlId}", hairstyleId);
                }
                else
                {
                    HairstyleId = new SavedSourceId(mod.SourceId, hairstyleId.LocalId);
                }
            }

            // current outfit
            var outfitId = ModInterface.Data.GetOutfitId(girlId, saveFileGirl.outfitIndex);
            if (outfitId.SourceId != -1)
            {
                saveFileGirl.outfitIndex = -1;

                var mod = ModInterface.FindMod(outfitId.SourceId);

                if (mod == null)
                {
                    ModInterface.Log.LogMissingIdError($"Attempting to save outfit for girl {girlId}", outfitId);
                }
                else
                {
                    OutfitId = new SavedSourceId(mod.SourceId, outfitId.LocalId);
                }
            }

            // baggage
            ValidatedSet.StripRuntimeIds(ref saveFileGirl.learnedBaggage,
                            LearnedBaggage,
                            $"Attempting to save baggage for girl {girlId}",
                            (x) => ModInterface.Data.GetDataId(GameDataType.Ailment, x));

            // uniques
            ValidatedSet.StripRuntimeIds(ref saveFileGirl.receivedUniques,
                            ReceivedUniques,
                            $"Attempting to save unique item for girl {girlId}",
                            (x) => ModInterface.Data.GetDataId(GameDataType.Item, x));
            
            // shoes
            ValidatedSet.StripRuntimeIds(ref saveFileGirl.receivedShoes,
                            ReceivedShoes,
                            $"Attempting to save shoe item for girl {girlId}",
                            (x) => ModInterface.Data.GetDataId(GameDataType.Item, x));

            // favs
            ValidatedSet.StripRuntimeIds(ref saveFileGirl.learnedFavs,
                            LearnedFavs,
                            $"Attempting to save learned favorite for girl {girlId}",
                            (x) => ModInterface.Data.GetDataId(GameDataType.Question, x));

            // RecentHerQuestions
            ValidatedSet.StripRuntimeIds(ref saveFileGirl.recentHerQuestions,
                            RecentHerQuestions,
                            $"Attempting to save question for girl {girlId}",
                            (x) => ModInterface.Data.GetDataId(GameDataType.Question, x));

            // Outfits
            ValidatedSet.StripRuntimeIds(ref saveFileGirl.unlockedOutfits,
                            UnlockedOutfits,
                            $"Attempting to save outfit for girl {girlId}",
                            (x) => ModInterface.Data.GetOutfitId(girlId, x));

            // Hairstyles
            ValidatedSet.StripRuntimeIds(ref saveFileGirl.unlockedHairstyles,
                            UnlockedHairstyles,
                            $"Attempting to save hairstyle for girl {girlId}",
                            (x) => ModInterface.Data.GetHairstyleId(girlId, x));

            // DateGiftSlots
            if (saveFileGirl.dateGiftSlots != null)
            {
                foreach (var slot in saveFileGirl.dateGiftSlots)
                {
                    var itemId = ModInterface.Data.GetDataId(GameDataType.Item, slot.itemId);

                    if (itemId.SourceId != -1)
                    {
                        slot.itemId = -1;

                        var mod = ModInterface.FindMod(itemId.SourceId);

                        if (mod == null)
                        {
                            ModInterface.Log.LogMissingIdError("Attempting to save inventory item", itemId.LocalId, itemId.SourceId);
                        }
                        else
                        {
                            slot.itemId = itemId.LocalId;
                            DateGiftSlots.Add(new ModSaveItemSlot(new SavedSourceId(mod.SourceId, itemId.LocalId), slot.slotIndex));
                        }
                    }
                }

                // 4 is the regular ammount
                saveFileGirl.dateGiftSlots = saveFileGirl.dateGiftSlots.Take(4).ToList();
                var count = saveFileGirl.dateGiftSlots.Count();
                while (count < 4)
                {
                    saveFileGirl.dateGiftSlots.Add(new SaveFileInventorySlot(count));
                    count++;
                }
            }
        }

        public void SetData(SaveFileGirl saveFileGirl)
        {
            var girlId = ModInterface.Data.GetDataId(GameDataType.Girl, saveFileGirl.girlId);

            ValidatedSet.SetValue(ref saveFileGirl.hairstyleIndex, ModInterface.Data.GetHairstyleIndex(girlId, HairstyleId?.ToRelativeId()));
            ValidatedSet.SetValue(ref saveFileGirl.outfitIndex, ModInterface.Data.GetOutfitIndex(girlId, OutfitId?.ToRelativeId()));

            ValidatedSet.SetModIds(saveFileGirl.learnedBaggage, LearnedBaggage, (x) => ModInterface.Data.GetRuntimeDataId(GameDataType.Ailment, x));
            ValidatedSet.SetModIds(saveFileGirl.receivedUniques, ReceivedUniques, (x) => ModInterface.Data.GetRuntimeDataId(GameDataType.Item, x));
            ValidatedSet.SetModIds(saveFileGirl.receivedShoes, ReceivedShoes, (x) => ModInterface.Data.GetRuntimeDataId(GameDataType.Item, x));
            ValidatedSet.SetModIds(saveFileGirl.learnedFavs, LearnedFavs, (x) => ModInterface.Data.GetRuntimeDataId(GameDataType.Question, x));
            ValidatedSet.SetModIds(saveFileGirl.recentHerQuestions, RecentHerQuestions, (x) => ModInterface.Data.GetRuntimeDataId(GameDataType.Question, x));
            ValidatedSet.SetModIds(saveFileGirl.unlockedOutfits, UnlockedOutfits, (x) => ModInterface.Data.GetOutfitIndex(girlId, x));
            ValidatedSet.SetModIds(saveFileGirl.unlockedHairstyles, UnlockedHairstyles, (x) => ModInterface.Data.GetHairstyleIndex(girlId, x));

            if (DateGiftSlots != null)
            {
                var slotCount = saveFileGirl.dateGiftSlots.Count();
                foreach (var slot in DateGiftSlots)
                {
                    var itemId = slot.SavedSourceId.ToRelativeId();

                    if (itemId.HasValue)
                    {
                        while (slot.Index < slotCount)
                        {
                            saveFileGirl.dateGiftSlots.Add(new SaveFileInventorySlot(slotCount));
                            slotCount++;
                        }

                        saveFileGirl.dateGiftSlots[slot.Index].itemId = ModInterface.Data.GetRuntimeDataId(GameDataType.Item, itemId.Value);
                    }
                    else
                    {
                        ModInterface.Log.LogError($"Unable get date slot item {slot}");
                    }
                }
            }
        }
    }
}
