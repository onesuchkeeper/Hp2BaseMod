using Hp2BaseMod.GameDataInfo.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hp2BaseMod.GameDataInfo
{
    internal class FileDataModsProvider : IProvideGameDataMods
    {
        public IEnumerable<IGameDataMod<AbilityDefinition>> AbilityDataMods => _abilityDataMods;
        private List<AbilityDataMod> _abilityDataMods = new List<AbilityDataMod>();

        public IEnumerable<IGameDataMod<AilmentDefinition>> AilmentDataMods => _ailmentDataMods;
        private List<AilmentDataMod> _ailmentDataMods = new List<AilmentDataMod>();

        public IEnumerable<IGameDataMod<CodeDefinition>> CodeDataMods => _codeDataMods;
        private List<CodeDataMod> _codeDataMods = new List<CodeDataMod>();

        public IEnumerable<IGameDataMod<CutsceneDefinition>> CutsceneDataMods => _cutsceneDataMods;
        private List<CutsceneDataMod> _cutsceneDataMods = new List<CutsceneDataMod>();

        public IEnumerable<IGameDataMod<DialogTriggerDefinition>> DialogTriggerDataMods => _dialogTriggerDataMods;
        private List<DialogTriggerDataMod> _dialogTriggerDataMods = new List<DialogTriggerDataMod>();

        public IEnumerable<IGameDataMod<DlcDefinition>> DlcDataMods => _dlcDataMods;
        private List<DlcDataMod> _dlcDataMods = new List<DlcDataMod>();

        public IEnumerable<IGameDataMod<EnergyDefinition>> EnergyDataMods => _energyDataMods;
        private List<EnergyDataMod> _energyDataMods = new List<EnergyDataMod>();

        public IEnumerable<IGirlDataMod> GirlDataMods => _girlDataMods;
        private List<GirlDataMod> _girlDataMods = new List<GirlDataMod>();

        public IEnumerable<IGirlPairDataMod> GirlPairDataMods => _girlPairDataMods;
        private List<GirlPairDataMod> _girlPairDataMods = new List<GirlPairDataMod>();

        public IEnumerable<IGameDataMod<ItemDefinition>> ItemDataMods => _itemDataMods;
        private List<ItemDataMod> _itemDataMods = new List<ItemDataMod>();

        public IEnumerable<ILocationDataMod> LocationDataMods => _locationDataMods;
        private List<LocationDataMod> _locationDataMods = new List<LocationDataMod>();

        public IEnumerable<IGameDataMod<PhotoDefinition>> PhotoDataMods => _photoDataMods;
        private List<PhotoDataMod> _photoDataMods = new List<PhotoDataMod>();

        public IEnumerable<IGameDataMod<QuestionDefinition>> QuestionDataMods => _questionDataMods;
        private List<QuestionDataMod> _questionDataMods = new List<QuestionDataMod>();

        public IEnumerable<IGameDataMod<TokenDefinition>> TokenDataMods => _tokenDataMods;
        private List<TokenDataMod> _tokenDataMods = new List<TokenDataMod>();

        public FileDataModsProvider(Hp2ModConfig config, int modId)
        {
            var sourceIdLookups = new Dictionary<int, int>() { { -1, -1 }, { -2, modId } };
            foreach (var dependancy in config.Dependencies)
            {
                sourceIdLookups.Add(dependancy.AssumedId, ModInterface.FindMod(dependancy.SourceIdentifier).Id);
            }

            Func<RelativeId?, RelativeId?> getNewSource = (x) => x.HasValue ? (RelativeId?)new RelativeId(sourceIdLookups[x.Value.SourceId], x.Value.LocalId) : null;

            LoadModFiles(_abilityDataMods,
                config.AbilityDataModPaths,
                getNewSource);

            LoadModFiles(_ailmentDataMods,
                config.AilmentDataModPaths,
                getNewSource);

            LoadModFiles(_codeDataMods,
                config.CodeDataModPaths,
                getNewSource);

            LoadModFiles(_cutsceneDataMods,
                config.CutsceneDataModPaths,
                getNewSource);

            LoadModFiles(_dialogTriggerDataMods,
                config.DialogTriggerDataModPaths,
                getNewSource);

            LoadModFiles(_dlcDataMods,
                config.DlcDataModPaths,
                getNewSource);

            LoadModFiles(_energyDataMods,
                config.EnergyDataModPaths,
                getNewSource);

            LoadModFiles(_girlDataMods,
                config.GirlDataModPaths,
                getNewSource);

            LoadModFiles(_girlPairDataMods,
                config.GirlPairDataModPaths,
                getNewSource);

            LoadModFiles(_itemDataMods,
                config.ItemDataModPaths,
                getNewSource);

            LoadModFiles(_locationDataMods,
                config.LocationDataModPaths,
                getNewSource);

            LoadModFiles(_photoDataMods,
                config.PhotoDataModPaths,
                getNewSource);

            LoadModFiles(_questionDataMods,
                config.QuestionDataModPaths,
                getNewSource);

            LoadModFiles(_tokenDataMods,
                config.TokenDataModPaths,
                getNewSource);
        }

        private void LoadModFiles<T>(List<T> mods, IEnumerable<string> paths, Func<RelativeId?, RelativeId?> getNewSource)
            where T : DataMod
        {
            if (paths != null)
            {
                foreach (var path in paths)
                {
                    if (File.Exists(path))
                    {
                        var deserialized = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

                        if (deserialized == null)
                        {
                            ModInterface.Log.LogError($"Failed to deserialize {path}");
                        }
                        else
                        {
                            deserialized.ReplaceRelativeIds(getNewSource);
                            mods.Add(deserialized);
                        }
                    }
                    else
                    {
                        ModInterface.Log.LogError($"{path} not found");
                    }
                }
            }
        }
    }
}
