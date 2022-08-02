// Hp2DataModLoader 2021, by onesuchkeeper

using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hp2SampleMod
{
    public class Starter : IHp2ModStarter
    {
        public IEnumerable<IGameDataMod<AbilityDefinition>> AbilityDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<AilmentDefinition>> AilmentDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<CodeDefinition>> CodeDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<CutsceneDefinition>> CutsceneDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<DialogTriggerDefinition>> DialogTriggerDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<DlcDefinition>> DlcDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<EnergyDefinition>> EnergyDataMods => throw new NotImplementedException();
        public IEnumerable<IGirlDataMod> GirlDataMods => throw new NotImplementedException();
        public IEnumerable<IGirlPairDataMod> GirlPairDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<ItemDefinition>> ItemDataMods => throw new NotImplementedException();
        public IEnumerable<ILocationDataMod> LocationDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<PhotoDefinition>> PhotoDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<QuestionDefinition>> QuestionDataMods => throw new NotImplementedException();
        public IEnumerable<IGameDataMod<TokenDefinition>> TokenDataMods => throw new NotImplementedException();

        //private static readonly string _modDir = "mods/Hp2DataModLoader";

        //public IEnumerable<AbilityDataMod> AbilityDataMods => _abilityDataMods;
        //private List<AbilityDataMod> _abilityDataMods = new List<AbilityDataMod>();

        //public IEnumerable<AilmentDataMod> AilmentDataMods => _ailmentDataMods;
        //private List<AilmentDataMod> _ailmentDataMods = new List<AilmentDataMod>();

        //public IEnumerable<CodeDataMod> CodeDataMods => _codeDataMods;
        //private List<CodeDataMod> _codeDataMods = new List<CodeDataMod>();

        //public IEnumerable<CutsceneDataMod> CutsceneDataMods => _cutsceneDataMods;
        //private List<CutsceneDataMod> _cutsceneDataMods = new List<CutsceneDataMod>();

        //public IEnumerable<DialogTriggerDataMod> DialogTriggerDataMods => _dialogTriggerDataMods;
        //private List<DialogTriggerDataMod> _dialogTriggerDataMods = new List<DialogTriggerDataMod>();

        //public IEnumerable<DlcDataMod> DlcDataMods => _dlcDataMods;
        //private List<DlcDataMod> _dlcDataMods = new List<DlcDataMod>();

        //public IEnumerable<EnergyDataMod> EnergyDataMods => _energyDataMods;
        //private List<EnergyDataMod> _energyDataMods = new List<EnergyDataMod>();

        //public IEnumerable<GirlDataMod> GirlDataMods => _girlDataMods;
        //private List<GirlDataMod> _girlDataMods = new List<GirlDataMod>();

        //public IEnumerable<GirlPairDataMod> GirlPairDataMods => _girlPairDataMods;
        //private List<GirlPairDataMod> _girlPairDataMods = new List<GirlPairDataMod>();

        //public IEnumerable<ItemDataMod> ItemDataMods => _itemDataMods;
        //private List<ItemDataMod> _itemDataMods = new List<ItemDataMod>();

        //public IEnumerable<LocationDataMod> LocationDataMods => _locationDataMods;
        //private List<LocationDataMod> _locationDataMods = new List<LocationDataMod>();

        //public IEnumerable<PhotoDataMod> PhotoDataMods => _photoDataMods;
        //private List<PhotoDataMod> _photoDataMods = new List<PhotoDataMod>();

        //public IEnumerable<QuestionDataMod> QuestionDataMods => _questionDataMods;
        //private List<QuestionDataMod> _questionDataMods = new List<QuestionDataMod>();

        //public IEnumerable<TokenDataMod> TokenDataMods => _tokenDataMods;
        //private List<TokenDataMod> _tokenDataMods = new List<TokenDataMod>();

        //public IEnumerable<OutfitInfo> OutfitMods => _outfitMods;
        //private List<OutfitInfo> _outfitMods = new List<OutfitInfo>();

        //public IEnumerable<HairstyleInfo> HairstyleMods => _hairstyleMods;
        //private List<HairstyleInfo> _hairstyleMods = new List<HairstyleInfo>();

        //private Dictionary<Type, Func<int, bool>> TypeToDefaultIdCheck = new Dictionary<Type, Func<int, bool>>()
        //{
        //    { typeof(AbilityDataMod), DefaultData.IsDefaultAbility },
        //    { typeof(AilmentDataMod), DefaultData.IsDefaultAilment },
        //    { typeof(CodeDataMod), DefaultData.IsDefaultCode },
        //    { typeof(CutsceneDataMod), DefaultData.IsDefaultCutscene },
        //    { typeof(DialogTriggerDataMod), DefaultData.IsDefaultDialogTrigger },
        //    { typeof(DlcDataMod), DefaultData.IsDefaultDlc },
        //    { typeof(EnergyDataMod),  DefaultData.IsDefaultEnergy },
        //    { typeof(GirlDataMod), DefaultData.IsDefaultGirl },
        //    { typeof(GirlPairDataMod), DefaultData.IsDefaultGirlPair },
        //    { typeof(ItemDataMod), DefaultData.IsDefaultItem },
        //    { typeof(LocationDataMod), DefaultData.IsDefaultLocation },
        //    { typeof(PhotoDataMod), DefaultData.IsDefaultPhoto },
        //    { typeof(QuestionDataMod), DefaultData.IsDefaultQuestion },
        //    { typeof(TokenDataMod), DefaultData.IsDefaultToken },
        //    { typeof(OutfitInfo), (i) => true },
        //    { typeof(HairstyleInfo), (i) => true }
        //};

        //private int _modId;

        public void Start(int modId)
        {
            //_modId = modId;

            //LoadModFiles(_abilityDataMods);
            //LoadModFiles(_ailmentDataMods);
            //LoadModFiles(_codeDataMods);
            //LoadModFiles(_cutsceneDataMods);
            //LoadModFiles(_dialogTriggerDataMods);
            //LoadModFiles(_dlcDataMods);
            //LoadModFiles(_energyDataMods);
            //LoadModFiles(_girlDataMods);
            //LoadModFiles(_girlPairDataMods);
            //LoadModFiles(_itemDataMods);
            //LoadModFiles(_locationDataMods);
            //LoadModFiles(_photoDataMods);
            //LoadModFiles(_questionDataMods);
            //LoadModFiles(_tokenDataMods);

            //LoadProviders<NewGirlMod>();
        }

        //private IEnumerable<string> GetFiles(Type type)
        //{
        //    var path = Path.Combine(_modDir, type.Name);

        //    if (Directory.Exists(path))
        //    {
        //        return Directory.GetFiles(path);
        //    }
        //    else
        //    {
        //        Directory.CreateDirectory(path);
        //        return Enumerable.Empty<string>();
        //    }
        //}

        //private void LoadModFiles<T>(List<T> list)
        //    where T : DataMod
        //{
        //    foreach (var file in GetFiles(typeof(T)))
        //    {
        //        var deserialized = JsonUtility.DeserializeFromFilePath<T>(file);

        //        if (deserialized == null)
        //        {
        //            ModInterface.Log.LogLine($"Failed to deserialize {file}");
        //        }
        //        else
        //        {
        //            AddMod(list, deserialized);
        //        }
        //    }
        //}

        //private void AddMod<T>(List<T> list, T mod)
        //    where T : DataMod
        //{
        //    var ModId = TypeToDefaultIdCheck[typeof(T)].Invoke(mod.IdInfo.LocalId) ? -1 : _modId;

        //    mod.IdInfo = new GameDataIdInfo(new RelativeId(ModId, mod.IdInfo.LocalId), mod.IdInfo.GameDataType);

        //    list.Add(mod);
        //}

        //private void LoadProviders<T>()
        //    where T : class, IProvideMods
        //{
        //    foreach (var file in GetFiles(typeof(T)))
        //    {
        //        var deserialized = JsonUtility.DeserializeFromFilePath<T>(file) as IProvideMods;

        //        if (deserialized == null)
        //        {
        //            ModInterface.Log.LogLine($"Failed to deserialize {file}");
        //        }
        //        else
        //        {
        //            AddMods(_abilityDataMods, deserialized.AbilityDataMods);
        //            AddMods(_ailmentDataMods, deserialized.AilmentDataMods);
        //            AddMods(_codeDataMods, deserialized.CodeDataMods);
        //            AddMods(_cutsceneDataMods, deserialized.CutsceneDataMods);
        //            AddMods(_dialogTriggerDataMods, deserialized.DialogTriggerDataMods);
        //            AddMods(_dlcDataMods, deserialized.DlcDataMods);
        //            AddMods(_energyDataMods, deserialized.EnergyDataMods);
        //            AddMods(_girlDataMods, deserialized.GirlDataMods);
        //            AddMods(_girlPairDataMods, deserialized.GirlPairDataMods);
        //            AddMods(_itemDataMods, deserialized.ItemDataMods);
        //            AddMods(_locationDataMods, deserialized.LocationDataMods);
        //            AddMods(_photoDataMods, deserialized.PhotoDataMods);
        //            AddMods(_questionDataMods, deserialized.QuestionDataMods);
        //            AddMods(_tokenDataMods, deserialized.TokenDataMods);
        //        }
        //    }
        //}

        //private void AddMods<T>(List<T> list, IEnumerable<T> mods)
        //    where T : DataMod
        //{
        //    if (mods != null)
        //    {
        //        foreach (var mod in mods)
        //        {
        //            AddMod(list, mod);
        //        }
        //    }
        //}
    }
}
