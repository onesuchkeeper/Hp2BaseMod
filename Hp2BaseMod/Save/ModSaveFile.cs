// Hp2BaseModLoader 2021, by OneSuchKeeper

using Hp2BaseMod.Extension.IEnumerableExtension;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.Save.Interface;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.Save
{
    public class ModSaveFile : IModSave<SaveFile>
    {
        public class ModData
        {
            public List<Tuple<SaveFileGirl, ModSaveGirl>> Girls;
            public List<Tuple<SaveFileGirlPair, ModSaveGirlPair>> GirlPairs;
            public List<ModSaveItemSlot> InventorySlots;
            public List<ModSaveItemSlot> StoreProducts;

            public List<SaveFileFinderSlot> LocalPairFinderSlots;
            public List<SaveFileFinderSlot> DefaultPairFinderSlots;
            public List<Tuple<SaveFileFinderSlot, SavedSourceId>> OtherSourcePairFinderSlots;

            public List<int> MetGirlPairs;
            public List<int> CompletedGirlPairs;
        }   

        private const string _wardrobeGirlIdFlagName = "wardrobe_girl_id";
        private const int _hotelRoomLocationID = 21;
        private const int _lolaGirlID = 1;

        public SavedSourceId? WardrobeGirlId;
        public SavedSourceId? LocationId;
        public SavedSourceId? GirlPairId;
        public SavedSourceId? FileIconGirlId;

        public List<Tuple<int, SavedSourceId>> DefaultFinderSlotPairs = new List<Tuple<int, SavedSourceId>>();
        public List<Tuple<int, ModSaveGirl>> DefaultGirlModSaves = new List<Tuple<int, ModSaveGirl>>();
        public List<Tuple<int, ModSaveGirlPair>> DefaultGirlPairModSaves = new List<Tuple<int, ModSaveGirlPair>>();

        public List<Tuple<SourceIdentifier, ModData>> ModGameData = new List<Tuple<SourceIdentifier, ModData>>();

        /// <summary>
        /// Copies the save file and strips modded data from it
        /// </summary>
        /// <param name="saveFile">savefile to strip</param>
        public void Strip(SaveFile saveFile)
        {
            // wardrobe girl
            //ModInterface.Log.LogLine($"flags");
            var wardrobeGirlIdFlag = saveFile.flags.FirstOrDefault(x => x.flagName == _wardrobeGirlIdFlagName);
            var wardrobeGirlId = wardrobeGirlIdFlag?.flagValue;
            if (wardrobeGirlId.HasValue
                && !DefaultData.IsDefaultGirl(wardrobeGirlId.Value))
            {
                var girlId = ModInterface.Data.GetDataId(GameDataType.Girl, wardrobeGirlId.Value);
                var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == girlId.SourceId);

                if (mod == null)
                {
                    ModInterface.Log.LogMissingIdError("Attempting to save flag", girlId.LocalId, girlId.SourceId);
                }
                else
                {
                    WardrobeGirlId = new SavedSourceId(mod.SourceId, girlId.LocalId);
                }

                saveFile.flags.Remove(wardrobeGirlIdFlag);
                saveFile.flags.Add(new SaveFileFlag(_wardrobeGirlIdFlagName) { flagValue = _lolaGirlID });
            }

            // "notification_item_id" has an error catch, so it's fine if it has a bad value
            // and I don't think it's used right after a save anyways
            // no other flags should matter on load, and having unnessisary mod flags in there won't hurt anything,
            // although it's not recomended to use mod flags, there's no way to validate their source. maybe add a place for mod flags in the interface

            // icon
            //ModInterface.Log.LogLine($"icon");
            var iconGirlId = ModInterface.Data.GetDataId(GameDataType.Girl, saveFile.fileIconGirlId);
            if (iconGirlId.SourceId != -1)
            {
                var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == iconGirlId.SourceId);

                if (mod == null)
                {
                    ModInterface.Log.LogMissingIdError("Attempting to save file icon girl", iconGirlId.LocalId, iconGirlId.SourceId);
                }
                else
                {
                    FileIconGirlId = new SavedSourceId(mod.SourceId, iconGirlId.LocalId);
                }

                saveFile.fileIconGirlId = _lolaGirlID; 
            }

            // current location
            //ModInterface.Log.LogLine($"location");
            var currentLocationId = ModInterface.Data.GetDataId(GameDataType.Location, saveFile.locationId);
            if (currentLocationId.SourceId != -1)
            {
                var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == currentLocationId.SourceId);

                if (mod == null)
                {
                    ModInterface.Log.LogMissingIdError("Attempting to save current location", currentLocationId);
                }
                else
                {
                    LocationId = new SavedSourceId(mod.SourceId, currentLocationId.LocalId);
                }

                saveFile.locationId = _hotelRoomLocationID;
            }

            // current pair
            //ModInterface.Log.LogLine($"pair");
            var currentPairId = ModInterface.Data.GetDataId(GameDataType.GirlPair, saveFile.girlPairId);
            if (currentPairId.LocalId != -1)
            {
                var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == currentPairId.SourceId);

                if (mod == null)
                {
                    ModInterface.Log.LogMissingIdError("Attempting to save current pair", currentPairId);
                }
                else
                {
                    GirlPairId = new SavedSourceId(mod.SourceId, currentPairId.LocalId);
                }

                // if the current pair is invalid, go back to the hub
                saveFile.girlPairId = -1;
                saveFile.locationId = _hotelRoomLocationID;
            }

            // girls
            //ModInterface.Log.LogLine($"girls");
            if (saveFile.girls != null)
            {
                var defaults = new List<SaveFileGirl>();

                foreach (var girl in saveFile.girls)
                {
                    var girlId = ModInterface.Data.GetDataId(GameDataType.Girl, girl.girlId);

                    var modSaveGirl = new ModSaveGirl();
                    modSaveGirl.Strip(girl);

                    if (girlId.SourceId == -1)
                    {
                        DefaultGirlModSaves.Add(new Tuple<int, ModSaveGirl>(girlId.LocalId, modSaveGirl));
                        defaults.Add(girl);
                    }
                    else
                    {
                        var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == girlId.SourceId);

                        if (mod == null)
                        {
                            ModInterface.Log.LogMissingIdError("Attempting to save girl", girlId.LocalId, girlId.SourceId);
                        }
                        else
                        {
                            var modData = GetModDataOrAddNew(mod.SourceId);

                            if (modData.Girls == null)
                            {
                                modData.Girls = new List<Tuple<SaveFileGirl, ModSaveGirl>>();
                            }

                            girl.girlId = girlId.LocalId;

                            modData.Girls.Add(new Tuple<SaveFileGirl, ModSaveGirl>(girl, modSaveGirl));
                        }
                    }
                }

                saveFile.girls = defaults;
            }

            // pairs
            //ModInterface.Log.LogLine($"pairs");
            if (saveFile.girlPairs != null)
            {
                var defaults = new List<SaveFileGirlPair>();

                foreach (var pair in saveFile.girlPairs)
                {
                    var girlPairId = ModInterface.Data.GetDataId(GameDataType.GirlPair, pair.girlPairId);

                    var modSavePair = new ModSaveGirlPair();
                    modSavePair.Strip(pair);

                    if (girlPairId.SourceId == -1)
                    {
                        DefaultGirlPairModSaves.Add(new Tuple<int, ModSaveGirlPair>(girlPairId.LocalId, modSavePair));
                        defaults.Add(pair);
                    }
                    else
                    {
                        var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == girlPairId.SourceId);

                        if (mod == null)
                        {
                            ModInterface.Log.LogMissingIdError("Attempting to save pair", girlPairId.LocalId, girlPairId.SourceId);
                        }
                        else
                        {
                            var modData = GetModDataOrAddNew(mod.SourceId);

                            if (modData.GirlPairs == null)
                            {
                                modData.GirlPairs = new List<Tuple<SaveFileGirlPair, ModSaveGirlPair>>();
                            }

                            pair.girlPairId = girlPairId.LocalId;

                            modData.GirlPairs.Add(new Tuple<SaveFileGirlPair, ModSaveGirlPair>(pair, modSavePair));
                        }
                    }
                }

                saveFile.girlPairs = defaults;
            }

            // met pairs
            //ModInterface.Log.LogLine($"met pairs");
            if (saveFile.metGirlPairs != null)
            {
                var defaults = new List<int>();

                foreach (var id in saveFile.metGirlPairs)
                {
                    var girlPairId = ModInterface.Data.GetDataId(GameDataType.GirlPair, id);

                    if (girlPairId.SourceId == -1)
                    {
                        defaults.Add(id);
                    }
                    else
                    {
                        var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == girlPairId.SourceId);

                        if (mod == null)
                        {
                            ModInterface.Log.LogMissingIdError("Attempting to save met pair", girlPairId.LocalId, girlPairId.SourceId);
                        }
                        else
                        {
                            var modData = GetModDataOrAddNew(mod.SourceId);

                            if (modData.MetGirlPairs == null)
                            {
                                modData.MetGirlPairs = new List<int>();
                            }

                            modData.MetGirlPairs.Add(girlPairId.LocalId);
                        }
                    }
                }

                saveFile.metGirlPairs = defaults;
            }

            // complete pairs
            //ModInterface.Log.LogLine($"completed pairs");
            if (saveFile.completedGirlPairs != null)
            {
                var defaults = new List<int>();

                foreach (var id in saveFile.completedGirlPairs)
                {
                    var girlPairId = ModInterface.Data.GetDataId(GameDataType.GirlPair, id);

                    if (girlPairId.SourceId == -1)
                    {
                        defaults.Add(id);
                    }
                    else
                    {
                        var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == girlPairId.SourceId);

                        if (mod == null)
                        {
                            ModInterface.Log.LogMissingIdError("Attempting to save completed pair", girlPairId.LocalId, girlPairId.SourceId);
                        }
                        else
                        {
                            var modData = GetModDataOrAddNew(mod.SourceId);

                            if (modData.CompletedGirlPairs == null)
                            {
                                modData.CompletedGirlPairs = new List<int>();
                            }

                            modData.CompletedGirlPairs.Add(girlPairId.LocalId);
                        }
                    }
                }

                saveFile.completedGirlPairs = defaults;
            }

            // finder
            //ModInterface.Log.LogLine($"finder slots");
            if (saveFile.finderSlots != null)
            {
                var defaults = new List<SaveFileFinderSlot>();

                foreach (var slot in saveFile.finderSlots)
                {
                    var locationId = ModInterface.Data.GetDataId(GameDataType.Location, slot.locationId);

                    // default location
                    if (locationId.SourceId == -1)
                    {
                        defaults.Add(slot);

                        var pairId = ModInterface.Data.GetDataId(GameDataType.GirlPair, slot.girlPairId);

                        // non default pair
                        if (pairId.SourceId != -1)
                        {
                            var mod = ModInterface.FindMod(pairId.SourceId);

                            // non-exsistant pair
                            if (mod == null)
                            {
                                ModInterface.Log.LogMissingIdError($"Attempting to save finder slot pair", locationId);
                                slot.girlPairId = 0;
                            }
                            // modded pair
                            else
                            {
                                slot.girlPairId = pairId.LocalId;
                                DefaultFinderSlotPairs.Add(new Tuple<int, SavedSourceId>(slot.locationId, new SavedSourceId(mod.SourceId, pairId.LocalId)));
                            }
                        }
                    }
                    else
                    {
                        var mod = ModInterface.FindMod(locationId.SourceId);

                        // non-exsistant location
                        if (mod == null)
                        {
                            ModInterface.Log.LogMissingIdError($"Attempting to save finder slot with location", locationId);
                        }
                        // modded location
                        else
                        {
                            slot.locationId = locationId.LocalId;

                            var modData = GetModDataOrAddNew(mod.SourceId);

                            var pairId = ModInterface.Data.GetDataId(GameDataType.GirlPair, slot.girlPairId);

                            ModInterface.Log.LogLine($"Saving slot for modded location {locationId} which has pair {pairId}");

                            // default pair
                            if (pairId.SourceId == -1)
                            {
                                if (modData.DefaultPairFinderSlots == null)
                                {
                                    modData.DefaultPairFinderSlots = new List<SaveFileFinderSlot>();
                                }

                                modData.DefaultPairFinderSlots.Add(slot);
                            }
                            // local pair
                            else if (pairId.SourceId == locationId.SourceId)
                            {
                                if (modData.LocalPairFinderSlots == null)
                                {
                                    modData.LocalPairFinderSlots = new List<SaveFileFinderSlot>();
                                }

                                slot.girlPairId = pairId.LocalId;

                                modData.LocalPairFinderSlots.Add(slot);
                            }
                            else
                            {
                                var pairMod = ModInterface.FindMod(pairId.SourceId);

                                // non-exsistant pair
                                if (pairMod == null)
                                {
                                    slot.girlPairId = 0;
                                    ModInterface.Log.LogMissingIdError($"Attempting to save finder slot with pair", pairId);
                                }
                                // pair from another source
                                else
                                {
                                    if (modData.OtherSourcePairFinderSlots == null)
                                    {
                                        modData.OtherSourcePairFinderSlots = new List<Tuple<SaveFileFinderSlot, SavedSourceId>>();
                                    }

                                    slot.girlPairId = pairId.LocalId;

                                    modData.OtherSourcePairFinderSlots.Add(new Tuple<SaveFileFinderSlot, SavedSourceId>(slot, new SavedSourceId(pairMod.SourceId, pairId.LocalId)));
                                }
                            }
                        }
                    }
                }

                // only default location slots
                saveFile.finderSlots = defaults;
            }

            // inventory
            //ModInterface.Log.LogLine($"inventory slots");
            foreach (var slot in saveFile.inventorySlots.OrEmptyIfNull())
            {
                var itemId = ModInterface.Data.GetDataId(GameDataType.Item, slot.itemId);

                if (itemId.SourceId != -1)
                {
                    slot.itemId = -1;
                    var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == itemId.SourceId);

                    if (mod == null)
                    {
                        ModInterface.Log.LogMissingIdError("Attempting to save inventory item", itemId.LocalId, itemId.SourceId);
                    }
                    else
                    {
                        var modData = GetModDataOrAddNew(mod.SourceId);

                        if (modData.InventorySlots == null)
                        {
                            modData.InventorySlots = new List<ModSaveItemSlot>();
                        }

                        modData.InventorySlots.Add(new ModSaveItemSlot(new SavedSourceId(mod.SourceId, itemId.LocalId), slot.slotIndex));
                    }
                }
            }

            // store
            //ModInterface.Log.LogLine($"store products");
            foreach (var slot in saveFile.storeProducts.OrEmptyIfNull())
            {
                var itemId = ModInterface.Data.GetDataId(GameDataType.Item, slot.itemId);

                if (itemId.SourceId != -1)
                {
                    slot.itemId = -1;
                    var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == itemId.SourceId);

                    if (mod == null)
                    {
                        ModInterface.Log.LogMissingIdError("Attempting to save shop item", itemId.LocalId, itemId.SourceId);
                    }
                    else
                    {
                        var modData = GetModDataOrAddNew(mod.SourceId);

                        if (modData.StoreProducts == null)
                        {
                            modData.StoreProducts = new List<ModSaveItemSlot>();
                        }

                        modData.StoreProducts.Add(new ModSaveItemSlot(new SavedSourceId(mod.SourceId, itemId.LocalId), slot.productIndex));
                    }
                }
            }
        }

        private ModData GetModDataOrAddNew(SourceIdentifier sourceId)
        {
            var entry = ModGameData.FirstOrDefault(x => x.Item1 == sourceId);

            if (entry == null)
            {
                entry = new Tuple<SourceIdentifier, ModData>(sourceId, new ModData());
                ModGameData.Add(entry);
            }

            return entry.Item2;
        }

        public void SetData(SaveFile saveFile)
        {
            // needs slots for all the locations
            // x for all the y's

            ValidatedSet.SetValue(ref saveFile.fileIconGirlId, ModInterface.Data.GetRuntimeDataId(GameDataType.Girl, FileIconGirlId?.ToRelativeId()));

            ValidatedSet.SetValue(ref saveFile.locationId, ModInterface.Data.GetRuntimeDataId(GameDataType.Location, LocationId?.ToRelativeId()));

            ValidatedSet.SetValue(ref saveFile.girlPairId, ModInterface.Data.GetRuntimeDataId(GameDataType.GirlPair, GirlPairId?.ToRelativeId()));

            if (WardrobeGirlId.HasValue)
            {
                saveFile.flags.Remove(saveFile.flags.FirstOrDefault(x => x.flagName == _wardrobeGirlIdFlagName));
                saveFile.flags.Add(new SaveFileFlag(_wardrobeGirlIdFlagName) { flagValue = ModInterface.Data.GetRuntimeDataId(GameDataType.Girl, WardrobeGirlId.Value.ToRelativeId()) ?? _lolaGirlID });
            }

            foreach (var modData in ModGameData)
            {
                var mod = ModInterface.FindMod(modData.Item1);

                if (mod != null)
                {
                    // pairs
                    foreach (var pairEntry in modData.Item2.GirlPairs.OrEmptyIfNull())
                    {
                        pairEntry.Item2.SetData(pairEntry.Item1);
                        saveFile.girlPairs.Add(pairEntry.Item1);
                    }

                    // pair meta
                    ValidatedSet.SetListValue(ref saveFile.metGirlPairs,
                                              modData.Item2.MetGirlPairs?.Select(x => ModInterface.Data.GetRuntimeDataId(GameDataType.GirlPair, new RelativeId(mod.Id, x))),
                                              InsertStyle.append);

                    ValidatedSet.SetListValue(ref saveFile.completedGirlPairs,
                                              modData.Item2.CompletedGirlPairs?.Select(x => ModInterface.Data.GetRuntimeDataId(GameDataType.GirlPair, new RelativeId(mod.Id, x))),
                                              InsertStyle.append);

                    // girls
                    foreach (var girlEntry in modData.Item2.Girls.OrEmptyIfNull())
                    {
                        girlEntry.Item2.SetData(girlEntry.Item1);
                        saveFile.girls.Add(girlEntry.Item1);
                    }

                    // now load in saved pairs at the mod's locations
                    foreach (var defaultFinder in modData.Item2.DefaultPairFinderSlots.OrEmptyIfNull())
                    {
                        defaultFinder.locationId = ModInterface.Data.GetRuntimeDataId(GameDataType.Location, new RelativeId(mod.Id, defaultFinder.locationId));
                        saveFile.finderSlots.Add(defaultFinder);
                    }

                    foreach (var localFinder in modData.Item2.LocalPairFinderSlots.OrEmptyIfNull())
                    {
                        localFinder.locationId = ModInterface.Data.GetRuntimeDataId(GameDataType.Location, new RelativeId(mod.Id, localFinder.locationId));
                        localFinder.girlPairId = ModInterface.Data.GetRuntimeDataId(GameDataType.GirlPair, new RelativeId(mod.Id, localFinder.girlPairId));
                        saveFile.finderSlots.Add(localFinder);
                    }

                    foreach (var otherFinder in modData.Item2.OtherSourcePairFinderSlots.OrEmptyIfNull())
                    {
                        otherFinder.Item1.locationId = ModInterface.Data.GetRuntimeDataId(GameDataType.Location, new RelativeId(mod.Id, otherFinder.Item1.locationId));
                        otherFinder.Item1.girlPairId = ModInterface.Data.GetRuntimeDataId(GameDataType.GirlPair, otherFinder.Item2.ToRelativeId()) ?? -1;
                        saveFile.finderSlots.Add(otherFinder.Item1);
                    }

                    // inventory
                    foreach (var inventory in modData.Item2.InventorySlots.OrEmptyIfNull())
                    {
                        saveFile.inventorySlots[inventory.Index].itemId = ModInterface.Data.GetRuntimeDataId(GameDataType.Item, inventory.SavedSourceId.ToRelativeId()) ?? -1;
                    }

                    // store
                    foreach (var product in modData.Item2.StoreProducts.OrEmptyIfNull())
                    {
                        saveFile.inventorySlots[product.Index].itemId = ModInterface.Data.GetRuntimeDataId(GameDataType.Item, product.SavedSourceId.ToRelativeId()) ?? -1;
                    }
                }
                else
                {
                    ModInterface.Log.LogError($"Unable to find mod {modData.Item1}, it's saved data will be ignored");
                }
            }

            // default locations with modded pairs
            foreach (var slotInfo in DefaultFinderSlotPairs)
            {
                var saveSlot = saveFile.finderSlots.FirstOrDefault(x => x.locationId == slotInfo.Item1);

                if (saveSlot != null)
                {
                    saveSlot.girlPairId = ModInterface.Data.GetRuntimeDataId(GameDataType.GirlPair, slotInfo.Item2.ToRelativeId()) ?? -1;
                }
            }

            // default girls
            foreach (var girlModSave in DefaultGirlModSaves)
            {
                var girl = saveFile.girls.FirstOrDefault(x => x.girlId == girlModSave.Item1);

                if (girl == null)
                {
                    ModInterface.Log.LogError($"Default mod save for girl with local id {girlModSave.Item1} but was not found in the save file.");
                }
                else
                {
                    girlModSave.Item2.SetData(girl);
                }
            }

            // default pairs
            foreach (var pairModSave in DefaultGirlPairModSaves)
            {
                var pair = saveFile.girlPairs.FirstOrDefault(x => x.girlPairId == pairModSave.Item1);

                if (pair == null)
                {
                    ModInterface.Log.LogError($"Default mod save for girl pair with local id {pairModSave.Item1} but was not found in the save file.");
                }
                else
                {
                    pairModSave.Item2.SetData(pair);
                }
            }

            // if at the hub remove the current pair
            if (saveFile.locationId == _hotelRoomLocationID)
            {
                saveFile.girlPairId = -1;
            }
        }
    }
}
