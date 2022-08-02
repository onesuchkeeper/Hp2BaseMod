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
            LoadModFiles(_abilityDataMods,
                config.AbilityDataModPaths,
                (x) => DefaultData.IsDefaultAbility(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_ailmentDataMods,
                config.AilmentDataModPaths,
                (x) => DefaultData.IsDefaultAilment(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_codeDataMods,
                config.CodeDataModPaths,
                (x) => DefaultData.IsDefaultCode(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_cutsceneDataMods,
                config.CutsceneDataModPaths,
                (x) => DefaultData.IsDefaultCutscene(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_dialogTriggerDataMods,
                config.DialogTriggerDataModPaths,
                (x) => DefaultData.IsDefaultDialogTrigger(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_dlcDataMods,
                config.DlcDataModPaths,
                (x) => DefaultData.IsDefaultDlc(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_energyDataMods,
                config.EnergyDataModPaths,
                (x) => DefaultData.IsDefaultEnergy(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_girlDataMods,
                config.GirlDataModPaths,
                (x) => DefaultData.IsDefaultGirl(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_girlPairDataMods,
                config.GirlPairDataModPaths,
                (x) => DefaultData.IsDefaultGirlPair(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_itemDataMods,
                config.ItemDataModPaths,
                (x) => DefaultData.IsDefaultItem(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_locationDataMods,
                config.LocationDataModPaths,
                (x) => DefaultData.IsDefaultLocation(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_photoDataMods,
                config.PhotoDataModPaths,
                (x) => DefaultData.IsDefaultPhoto(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_questionDataMods,
                config.QuestionDataModPaths,
                (x) => DefaultData.IsDefaultQuestion(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));

            LoadModFiles(_tokenDataMods,
                config.TokenDataModPaths,
                (x) => DefaultData.IsDefaultToken(x.LocalId) ? new RelativeId(-1, x.LocalId) : new RelativeId(modId, x.LocalId));
        }

        private void LoadModFiles<T>(List<T> mods, IEnumerable<string> paths, Func<RelativeId, RelativeId> correctId)
            where T : DataMod
        {
            if (paths != null)
            {
                foreach (var path in paths)
                {
                    if (File.Exists(path))
                    {
                        var deserialized = JsonConvert.DeserializeObject<AbilityDataMod>(File.ReadAllText(path)) as T;

                        if (deserialized == null)
                        {
                            ModInterface.Log.LogError($"Failed to deserialize {path}");
                        }
                        else
                        {
                            deserialized.Id = correctId.Invoke(deserialized.Id);
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
