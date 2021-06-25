// Hp2BaseModLoader 2021, by OneSuchKeeper

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.Save
{
    [Serializable]
    public class ModSaveFile
    {
        private readonly int hotelRoomLocationID = 21; 

        public bool Started;
        public int FileIconGirlId;
        public int SettingGender;
        public int SettingDifficulty;
        public int StoryProgress;
        public int DaytimeElapsed;
        public int FinderRestockTime;
        public int StoreRestockDay;
        public int StaminaFoodLimit;
        public int[] FruitCounts;
        public int[] AffectionLevelExps;
        public int RelationshipPoints;
        public int RelationshipUpCount;
        public int AlphaDateCount;
        public int NonstopDateCount;
        public int LocationID;
        public int GirlPairID;
        public List<SaveFileGirl> Girls;
        public List<SaveFileGirlPair> GirlPairs;
        public List<int> MetGirlPairs;
        public List<int> CompletedGirlPairs;
        public List<SaveFileFinderSlot> FinderSlots;
        public List<SaveFileInventorySlot> InventorySlots;
        public List<SaveFileStoreProduct> StoreProducts;
        public List<SaveFileFlag> Flags;

        private bool IsDefaultPair(int id) => id >= 1 && id <= 26;
        private bool IsDefaultGirl(int id) => id >= 1 && id <= 15;
        private bool IsDefaultNonSpecialGirl(int id) => id >= 1 && id <= 12;
        private bool IsDefaultFinderLocation(int id) => id == hotelRoomLocationID || (id >= 1 && id <= 8);
        private bool IsDefaultItem(int id) => id != 55
                                              && id != 62
                                              && id != 67
                                              && id != 69
                                              && id != 73
                                              && id != 79
                                              && id != 86
                                              && id != 92
                                              && id != 260
                                              && id != 267
                                              && id != 290
                                              && id >= 1 && id <= 320;

        /// <summary>
        /// Copies the save file and strips modded data from it
        /// </summary>
        /// <param name="saveFile">savefile to strip</param>
        public void Strip(SaveFile saveFile)
        {
            Started = saveFile.started;

            FileIconGirlId = saveFile.fileIconGirlId;
            if (!IsDefaultNonSpecialGirl(saveFile.fileIconGirlId)) { saveFile.fileIconGirlId = 1; }

            SettingGender = saveFile.settingGender;

            SettingDifficulty = saveFile.settingDifficulty;

            StoryProgress = saveFile.storyProgress;

            DaytimeElapsed = saveFile.daytimeElapsed;

            FinderRestockTime = saveFile.finderRestockTime;

            StoreRestockDay = saveFile.storeRestockDay;

            StaminaFoodLimit = saveFile.staminaFoodLimit;

            FruitCounts = saveFile.fruitCounts?.ToArray();

            AffectionLevelExps = saveFile.affectionLevelExps?.ToArray();

            RelationshipPoints = saveFile.relationshipPoints;

            RelationshipUpCount = saveFile.relationshipUpCount;

            AlphaDateCount = saveFile.alphaDateCount;

            NonstopDateCount = saveFile.nonstopDateCount;

            LocationID = saveFile.locationId;
            if (!IsDefaultFinderLocation(saveFile.locationId)) { saveFile.locationId = hotelRoomLocationID; }

            GirlPairID = saveFile.girlPairId;
            if (!IsDefaultPair(saveFile.girlPairId)) { saveFile.girlPairId = 0; saveFile.locationId = hotelRoomLocationID; }

            if (saveFile.girls != null)
            {
                Girls = saveFile.girls.ToList();
                saveFile.girls = saveFile.girls.Where(x => IsDefaultGirl(x.girlId)).ToList();
            }

            if (saveFile.girlPairs != null)
            {
                GirlPairs = saveFile.girlPairs.ToList();
                saveFile.girlPairs = saveFile.girlPairs.Where(x => IsDefaultPair(x.girlPairId)).ToList();
            }

            if (saveFile.metGirlPairs != null)
            {
                MetGirlPairs = saveFile.metGirlPairs.ToList();
                saveFile.metGirlPairs = saveFile.metGirlPairs.Where(x => IsDefaultPair(x)).ToList();
            }

            if (saveFile.completedGirlPairs != null)
            {
                CompletedGirlPairs = saveFile.completedGirlPairs.ToList();
                saveFile.completedGirlPairs = saveFile.completedGirlPairs.Where(x => IsDefaultPair(x)).ToList();
            }

            if (saveFile.finderSlots != null)
            {
                FinderSlots = saveFile.finderSlots.ToList();
                saveFile.finderSlots = saveFile.finderSlots.Where(x => IsDefaultPair(x.girlPairId) && IsDefaultFinderLocation(x.locationId)).ToList();
            }
            
            if (saveFile.inventorySlots != null)
            {
                InventorySlots = saveFile.inventorySlots.ToList();
                saveFile.inventorySlots = saveFile.inventorySlots.Where(x => IsDefaultItem(x.itemId)).ToList();
            }

            if (saveFile.storeProducts != null)
            {
                StoreProducts = saveFile.storeProducts.ToList();
                saveFile.storeProducts = saveFile.storeProducts.Where(x => IsDefaultItem(x.itemId)).ToList();
            }

            Flags = saveFile.flags?.ToList();
        }

        public SaveFile ToSaveFile()
        {
            var newSaveFile = new SaveFile();

            newSaveFile.started = Started;
            newSaveFile.fileIconGirlId = FileIconGirlId;
            newSaveFile.settingGender = SettingGender;
            newSaveFile.settingDifficulty = SettingDifficulty;
            newSaveFile.storyProgress = StoryProgress;
            newSaveFile.daytimeElapsed = DaytimeElapsed;
            newSaveFile.finderRestockTime = FinderRestockTime;
            newSaveFile.storeRestockDay = StoreRestockDay;
            newSaveFile.staminaFoodLimit = StaminaFoodLimit;
            newSaveFile.fruitCounts = FruitCounts;
            newSaveFile.affectionLevelExps = AffectionLevelExps;
            newSaveFile.relationshipPoints = RelationshipPoints;
            newSaveFile.relationshipUpCount = RelationshipUpCount;
            newSaveFile.alphaDateCount = AlphaDateCount;
            newSaveFile.nonstopDateCount = NonstopDateCount;
            newSaveFile.locationId = LocationID;
            newSaveFile.girlPairId = GirlPairID;
            newSaveFile.girls = Girls;
            newSaveFile.girlPairs = GirlPairs;
            newSaveFile.metGirlPairs = MetGirlPairs;
            newSaveFile.completedGirlPairs = CompletedGirlPairs;
            newSaveFile.finderSlots = FinderSlots;
            newSaveFile.inventorySlots = InventorySlots;
            newSaveFile.storeProducts = StoreProducts;
            newSaveFile.flags = Flags;

            return newSaveFile;
        }
    }
}
