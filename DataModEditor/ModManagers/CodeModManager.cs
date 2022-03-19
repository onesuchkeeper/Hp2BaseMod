// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Data;
using Hp2BaseMod.GameDataMods;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataModEditor.ModManagers
{
    public class CodeModManager : BaseModManager<CodeVM>
    {
        private static string DefaultName = "New Code Mod";

        protected override IEnumerable<KeyValuePair<int, string>> AvailableData
        {
            get
            {
                var options = Default.Codes.ToList();

                foreach (var mod in Mods)
                {
                    if (!options.Any(x => x.Key == mod.DataId))
                    {
                        options.Add(new KeyValuePair<int, string>(mod.DataId, mod.ModName));
                    }
                }

                return options;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mods">The girl mods dict</param>
        /// <param name="notifier">The notifier</param>
        public CodeModManager(List<CodeVM> mods, Notifier notifier)
            : base(mods, notifier)
        {
        }

        /// <summary>
        /// Creates a new mod
        /// </summary>
        /// <returns>The mod loaded into a Vm</returns>
        public CodeVM NewMod()
        {
            var name = UniqueName(DefaultName);

            var newMod = new CodeVM();
            newMod.CodeId = "10000";
            newMod.ModName = name;
            newMod.UnsavedEdits = true;

            AddMod(newMod);
            return newMod;
        }

        /// <summary>
        /// Opens a mod from a file
        /// </summary>
        /// <param name="path">Path to the mod file</param>
        /// <returns>The mod loaded into a Vm</returns>
        public CodeVM OpenMod(string path)
        {
            var configString = File.ReadAllText(path);
            var openedMod = JsonConvert.DeserializeObject(configString, typeof(CodeDataMod)) as CodeDataMod;
            if (openedMod != null)
            {
                openedMod.ModName = UniqueName(openedMod.ModName ?? DefaultName);

                var newMod = new CodeVM();
                newMod.Populate(openedMod);

                AddMod(newMod);
                return newMod;
            }
            else
            {
                _notifier.Error($"Invalid CodeDataMod file {path}");
            }

            return null;
        }
    }
}
