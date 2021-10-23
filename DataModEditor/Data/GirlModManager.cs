// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataMods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataModEditor
{
    public class GirlModManager
    {
        public Dictionary<string, GirlVM> Mods;

        private Notifier _notifier;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="girlMods">The girl mods dict</param>
        /// <param name="notifier">The notifier</param>
        public GirlModManager(Dictionary<string, GirlVM> girlMods, Notifier notifier)
        {
            Mods = girlMods ?? throw new ArgumentNullException(nameof(girlMods));
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        /// <summary>
        /// Gets formatted string collection of mods for display
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetReadout() => Mods.Select(x => $"{x.Key} - {x.Value.GirlName ?? "null"}");

        /// <summary>
        /// Creates a new mod
        /// </summary>
        /// <returns>The mod loaded into a Vm</returns>
        public GirlVM NewMod()
        {
            var name = UniqueName("New Girl Mod");

            Mods[name] = new GirlVM(new FavoriteVmManager());
            Mods[name].GirlId = "10000";
            Mods[name].ModName = name;
            Mods[name].UnsavedEdits = true;
            return Mods[name];
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

                return Mods[openedGirlMod.ModName] = new GirlVM(new FavoriteVmManager(), openedGirlMod);
            }
            else
            {
                _notifier.Error($"Invalid GirlMod file");
            }

            return null;
        }

        private string UniqueName(string name)
        {
            if (name == null)
            {
                name = UniqueName("New Girl Mod");
            }
            else if (!Mods.ContainsKey(name)) { return name; }

            var i = 1;
            while (Mods.ContainsKey($"{name} - {i}")) { i++; }

            return $"{name} - {i}";
        }
    }
}
