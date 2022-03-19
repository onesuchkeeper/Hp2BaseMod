// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Data;
using Hp2BaseMod.GameDataMods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataModEditor.ModManagers
{
    public class GirlModManager : BaseModManager<GirlVM>
    {
        protected override IEnumerable<KeyValuePair<int, string>> AvailableData
        {
            get
            {
                var options = Default.Girls.ToList();

                foreach (var mod in Mods)
                {
                    if (!options.Any(x => x.Key == mod.DataId))
                    {
                        options.Add(new KeyValuePair<int, string>(mod.DataId, mod.GirlName));
                    }
                }

                return options;
            }
        }

        private CodeModManager _codeModManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="girlMods">The girl mods dict</param>
        /// <param name="notifier">The notifier</param>
        public GirlModManager(List<GirlVM> mods, Notifier notifier, CodeModManager codeModManager)
            : base(mods, notifier)
        {
            _codeModManager = codeModManager ?? throw new ArgumentNullException(nameof(codeModManager));
        }

        /// <summary>
        /// Creates a new mod
        /// </summary>
        /// <returns>The mod loaded into a Vm</returns>
        public GirlVM NewMod()
        {
            var name = UniqueName("New Girl Mod");

            var newMod = new GirlVM(new FavoriteVmManager(), _codeModManager);
            newMod.GirlId = "10000";
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
        public GirlVM OpenMod(string path)
        {
            var configString = File.ReadAllText(path);
            var openedGirlMod = JsonConvert.DeserializeObject(configString, typeof(GirlDataMod)) as GirlDataMod;
            if (openedGirlMod != null)
            {
                openedGirlMod.ModName = UniqueName(openedGirlMod.ModName);

                var newModVM = new GirlVM(new FavoriteVmManager(), _codeModManager);
                newModVM.Populate(openedGirlMod);

                AddMod(newModVM);

                return newModVM;
            }
            else
            {
                _notifier.Error($"Invalid GirlMod file");
            }

            return null;
        }
    }
}
