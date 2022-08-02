// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.Extension.StringExtension;
using Hp2BaseMod.GameDataInfo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod
{
    /// <summary>
    /// Class to add data mods and look up them and their tags at runtime.
    /// A static instance of this class, ModInterface, will be available at Hp2 runtime
    /// </summary>
    public static class ModInterface
    {
        #region events

        public static event Action PreSave;
        internal static void NotifyPreSave() => PreSave?.Invoke();

        public static event Action PostSave;
        internal static void NotifyPostSave() => PostSave?.Invoke();

        public static event Action PreLoad;
        internal static void NotifyPreLoad() => PreLoad?.Invoke();

        public static event Action PostLoad;
        internal static void NotifyPostLoad() => PostLoad?.Invoke();

        #endregion

        /// <summary>
        /// The session's log
        /// </summary>
        public static ModLog Log => _log;
        private static readonly ModLog _log = new ModLog(@"mods\Hp2BaseMod.log");

        /// <summary>
        /// Meta information about modded data
        /// </summary>
        public static ModData Data => _data;
        private readonly static ModData _data = new ModData();

        /// <summary>
        /// All loaded mods
        /// </summary>
        public static IEnumerable<Hp2Mod> Mods => _mods;
        private readonly static List<Hp2Mod> _mods = new List<Hp2Mod>();

        #region Mods

        /// <summary>
        /// Finds the first mod with the given name and minimum version. Returns null if not found.
        /// </summary>
        public static Hp2Mod FindMod(SourceIdentifier sourceIdentifier) => FindMod(sourceIdentifier.Name, sourceIdentifier.Version.ToVersion());
        public static Hp2Mod FindMod(string name, Version minVersion) => _mods.FirstOrDefault(x => x.SourceId.Name == name && x.SourceId.Version.ToVersion() >= minVersion);
        public static Hp2Mod FindMod(int id) => _mods.FirstOrDefault(x => x.Id == id);

        internal static Hp2Mod MakeNewMod(Hp2ModConfig config)
        {
            var id = _mods.Count();
            var newMod = new Hp2Mod(id, config.Identifier, config.Tags);
            _mods.Add(newMod);
            return newMod;
        }

        #endregion

        #region Utility

        /// <summary>
        /// Checks if the code is unlocked
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsCodeUnlocked(RelativeId id) 
            => Game.Persistence?.playerData?.unlockedCodes.Any(x => x.id == _data.GetRuntimeDataId(GameDataType.Code, id)) ?? false;

        #endregion
    }
}
