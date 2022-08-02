// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.Extension.StringExtension;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.Save.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.Save
{
    class ModSaveData : IModSave<SaveData>
    {
        public List<Tuple<SourceIdentifier, List<int>>> UnlockedCodes;
        public List<ModSaveFile> Files;

        /// <inheritdoc/>
        public void Strip(SaveData saveData)
        {
            try
            {
                //ModInterface.Log.LogLine("stripping modded data from save");

                List<int> defaultUnlockedCodes;

                //ModInterface.Log.LogLine("unlock codes");
                if (saveData.unlockedCodes != null)
                {
                    defaultUnlockedCodes = new List<int>();
                    UnlockedCodes = new List<Tuple<SourceIdentifier, List<int>>>();

                    // split default codes from modded ones
                    foreach (var code in saveData.unlockedCodes)
                    {
                        var codeId = ModInterface.Data.GetDataId(GameDataType.Code, code);

                        if (codeId.SourceId == -1)
                        {
                            defaultUnlockedCodes.Add(code);
                        }
                        else
                        {
                            var mod = ModInterface.Mods.FirstOrDefault(x => x.Id == codeId.SourceId);

                            if (mod == null)
                            {
                                ModInterface.Log.LogMissingIdError($"Attempting to save code", codeId);
                            }
                            else
                            {
                                var entry = UnlockedCodes.FirstOrDefault(x => x.Item1 == mod.SourceId);

                                if (entry == null)
                                {
                                    entry = new Tuple<SourceIdentifier, List<int>>(mod.SourceId, new List<int>());
                                    UnlockedCodes.Add(entry);
                                }

                                entry.Item2.Add(codeId.LocalId);
                            }
                        }
                    }

                    saveData.unlockedCodes = defaultUnlockedCodes;
                }

                //ModInterface.Log.LogLine("save files");
                if (saveData.files != null)
                {
                    Files = new List<ModSaveFile>();

                    foreach (var file in saveData.files)
                    {
                        var newModSaveFile = new ModSaveFile();
                        newModSaveFile.Strip(file);
                        Files.Add(newModSaveFile);
                    }

                    saveData.files = saveData.files.Take(4).ToList();
                }
            }
            catch (Exception e)
            {
                ModInterface.Log.LogError(e.ToString());
                throw e;
            }
        }

        /// <inheritdoc/>
        public void SetData(SaveData saveData)
        {
            if (UnlockedCodes != null)
            {
                if (saveData.unlockedCodes == null)
                {
                    saveData.unlockedCodes = new List<int>();
                }

                // codes
                foreach (var modCodes in UnlockedCodes)
                {
                    var mod = ModInterface.FindMod(modCodes.Item1);

                    if (mod == null)
                    {
                        ModInterface.Log.LogError($"Failed to load codes from {modCodes.Item1} because the mod was not found.");
                    }
                    else
                    {
                        foreach (var code in modCodes.Item2)
                        {
                            saveData.unlockedCodes.Add(ModInterface.Data.GetRuntimeDataId(GameDataType.Code, new RelativeId(mod.Id, code)));
                        }
                    }
                }
            }

            if (Files != null && saveData.files != null)
            {
                while (saveData.files.Count < Files.Count)
                {
                    saveData.files.Add(new SaveFile());
                }

                var saveDataIt = saveData.files.GetEnumerator();
                saveDataIt.MoveNext();
                foreach (var modSaveFile in Files)
                {
                    modSaveFile.SetData(saveDataIt.Current);

                    // the relationship up count is saved for some reason, instead calculate it on load so we don't have to worry about modded pair status discrepencies
                    saveDataIt.Current.relationshipUpCount = saveDataIt.Current.girlPairs.Select(x =>
                    {
                        switch (x.relationshipLevel)
                        {
                            default:
                                return 0;
                            case 2:
                                return 1;
                            case 3:
                                return 2;
                        }
                    }).Sum();

                    saveDataIt.MoveNext();
                }
            }
        }
    }
}
