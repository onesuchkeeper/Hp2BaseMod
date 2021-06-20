// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseModLoader
{
    [Serializable]
    class ModSaveData
    {

        public bool WindowMode;
        public int ResolutionIndex;
        public bool CensoredMode;
        public int MusicVol;
        public int SoundVol;
        public int VoiceVol;
        public bool CensorPatched;
        public List<int> UnlockedCodes;
        public List<ModSaveFile> Files = new List<ModSaveFile>();

        private bool IsDefaultCode(int id) => id >= 1 && id <= 16;

        /// <summary>
        /// Copies the save data and strips modded data from it
        /// </summary>
        /// <param name="saveData"></param>
        public void Strip(SaveData saveData)
        {
            WindowMode = saveData.windowMode;
            ResolutionIndex = saveData.resolutionIndex;
            CensoredMode = saveData.censoredMode;
            MusicVol = saveData.musicVol;
            SoundVol = saveData.soundVol;
            VoiceVol = saveData.voiceVol;
            CensorPatched = saveData.censorPatched;

            if (saveData.unlockedCodes != null)
            {
                UnlockedCodes = saveData.unlockedCodes.ToList();
                saveData.unlockedCodes = saveData.unlockedCodes.Where(x => IsDefaultCode(x)).ToList();
            }

            if (saveData.files != null)
            {
                foreach (var file in saveData.files)
                {
                    var newModSaveFile = new ModSaveFile();
                    newModSaveFile.Strip(file);
                    Files.Add(newModSaveFile);
                }
            }
        }

        public SaveData ToSaveData()
        {
            // The nuumber of save files on construct doesn't matter, I'm overwriting it
            var newSaveData = new SaveData(0);

            newSaveData.windowMode = WindowMode;
            newSaveData.resolutionIndex = ResolutionIndex;
            newSaveData.censoredMode = CensoredMode;
            newSaveData.musicVol = MusicVol;
            newSaveData.soundVol = SoundVol;
            newSaveData.voiceVol = VoiceVol;
            newSaveData.censorPatched = CensorPatched;
            newSaveData.unlockedCodes = UnlockedCodes;
            newSaveData.files = Files.Select(x => x.ToSaveFile()).ToList();

            return newSaveData;
        }
    }
}
