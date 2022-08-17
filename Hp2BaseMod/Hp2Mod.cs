// Hp2BaseMododLoader 2022, by OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod
{
    public class Hp2Mod : IProvideGameDataMods
    {
        #region IProvideGameDataMods

        public IEnumerable<IGameDataMod<AbilityDefinition>> AbilityDataMods => _abilityDataMods;
        private List<IGameDataMod<AbilityDefinition>> _abilityDataMods = new List<IGameDataMod<AbilityDefinition>>();

        public IEnumerable<IGameDataMod<AilmentDefinition>> AilmentDataMods => _ailmentDataMods;
        private List<IGameDataMod<AilmentDefinition>> _ailmentDataMods = new List<IGameDataMod<AilmentDefinition>>();

        public IEnumerable<IGameDataMod<CodeDefinition>> CodeDataMods => _codeDataMods;
        private List<IGameDataMod<CodeDefinition>> _codeDataMods = new List<IGameDataMod<CodeDefinition>>();

        public IEnumerable<IGameDataMod<CutsceneDefinition>> CutsceneDataMods => _cutsceneDataMods;
        private List<IGameDataMod<CutsceneDefinition>> _cutsceneDataMods = new List<IGameDataMod<CutsceneDefinition>>();

        public IEnumerable<IGameDataMod<DialogTriggerDefinition>> DialogTriggerDataMods => _dialogTriggerDataMods;
        private List<IGameDataMod<DialogTriggerDefinition>> _dialogTriggerDataMods = new List<IGameDataMod<DialogTriggerDefinition>>();

        public IEnumerable<IGameDataMod<DlcDefinition>> DlcDataMods => _dlcDataMods;
        private List<IGameDataMod<DlcDefinition>> _dlcDataMods = new List<IGameDataMod<DlcDefinition>>();

        public IEnumerable<IGameDataMod<EnergyDefinition>> EnergyDataMods => _energyDataMods;
        private List<IGameDataMod<EnergyDefinition>> _energyDataMods = new List<IGameDataMod<EnergyDefinition>>();

        public IEnumerable<IGirlDataMod> GirlDataMods => _girlDataMods;
        private List<IGirlDataMod> _girlDataMods = new List<IGirlDataMod>();

        public IEnumerable<IGirlPairDataMod> GirlPairDataMods => _girlPairDataMods;
        private List<IGirlPairDataMod> _girlPairDataMods = new List<IGirlPairDataMod>();

        public IEnumerable<IGameDataMod<ItemDefinition>> ItemDataMods => _itemDataMods;
        private List<IGameDataMod<ItemDefinition>> _itemDataMods = new List<IGameDataMod<ItemDefinition>>();

        public IEnumerable<ILocationDataMod> LocationDataMods => _locationDataMods;
        private List<ILocationDataMod> _locationDataMods = new List<ILocationDataMod>();

        public IEnumerable<IGameDataMod<PhotoDefinition>> PhotoDataMods => _photoDataMods;
        private List<IGameDataMod<PhotoDefinition>> _photoDataMods = new List<IGameDataMod<PhotoDefinition>>();

        public IEnumerable<IGameDataMod<QuestionDefinition>> QuestionDataMods => _questionDataMods;
        private List<IGameDataMod<QuestionDefinition>> _questionDataMods = new List<IGameDataMod<QuestionDefinition>>();

        public IEnumerable<IGameDataMod<TokenDefinition>> TokenDataMods => _tokenDataMods;
        private List<IGameDataMod<TokenDefinition>> _tokenDataMods = new List<IGameDataMod<TokenDefinition>>();

        #endregion

        public int Id => _id;
        private readonly int _id;

        public SourceIdentifier SourceId => _sourceId;
        private readonly SourceIdentifier _sourceId;

        public IEnumerable<ModTag> Tags => _tags;
        private readonly IEnumerable<ModTag> _tags;

        public Hp2Mod(int id, SourceIdentifier sourceId, IEnumerable<ModTag> tags)
        {
            _id = id;
            _sourceId = sourceId;
            _tags = tags ?? Enumerable.Empty<ModTag>();
        }

        internal void AddDataMods(IProvideGameDataMods provider)
        {
            HandleDataMods(_abilityDataMods, provider.AbilityDataMods, GameDataType.Ability);
            HandleDataMods(_ailmentDataMods, provider.AilmentDataMods, GameDataType.Ailment);
            HandleDataMods(_codeDataMods, provider.CodeDataMods, GameDataType.Code);
            HandleDataMods(_cutsceneDataMods, provider.CutsceneDataMods, GameDataType.Cutscene);
            HandleDataMods(_dialogTriggerDataMods, provider.DialogTriggerDataMods, GameDataType.DialogTrigger);
            HandleDataMods(_dlcDataMods, provider.DlcDataMods, GameDataType.Dlc);
            HandleDataMods(_energyDataMods, provider.EnergyDataMods, GameDataType.Energy);
            HandleDataMods(_itemDataMods, provider.ItemDataMods, GameDataType.Item);
            HandleDataMods(_photoDataMods, provider.PhotoDataMods, GameDataType.Photo);
            HandleDataMods(_questionDataMods, provider.QuestionDataMods, GameDataType.Question);
            HandleDataMods(_tokenDataMods, provider.TokenDataMods, GameDataType.Token);

            if (provider.LocationDataMods != null)
            {
                foreach (var mod in provider.LocationDataMods)
                {
                    ModInterface.Data.TryRegisterData(GameDataType.Location, mod.Id);
                    _locationDataMods.Add(mod);
                }
            }

            if (provider.GirlDataMods != null)
            {
                foreach (var mod in provider.GirlDataMods)
                {
                    ModInterface.Data.TryRegisterData(GameDataType.Girl, mod.Id);
                    _girlDataMods.Add(mod);
                }
            }

            if (provider.GirlPairDataMods != null)
            {
                foreach (var mod in provider.GirlPairDataMods)
                {
                    ModInterface.Data.TryRegisterData(GameDataType.GirlPair, mod.Id);
                    _girlPairDataMods.Add(mod);
                }
            }
        }

        internal void HandleDataMods<T>(IList<IGameDataMod<T>> modList, IEnumerable<IGameDataMod<T>> modSource, GameDataType gameDataType)
        {
            if (modSource != null)
            {
                foreach (var mod in modSource)
                {
                    ModInterface.Data.TryRegisterData(gameDataType, mod.Id);
                    modList.Add(mod);
                }
            }
        }
    }
}
