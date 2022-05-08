// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hp2BaseMod.GameDataInfo;
using Newtonsoft.Json;

namespace Hp2BaseMod
{
    /// <summary>
    /// Class to add data mods and look up them and their tags at runtime.
    /// A static instance of this class, ModInterface.Instance, will be available at Hp2 runtime
    /// </summary>
    public class ModInterface
    {
        /// <summary>
        /// Instance of the mod interface for use at runtime
        /// </summary>
        public static ModInterface Instance { get; private set; }

        /// <summary>
        /// True when mods have finished being loaded and an instance of huniepop 2 is running.
        /// </summary>
        public bool IsAtRuntime { get; internal set; }

        /// <summary>
        /// Sets up instance
        /// </summary>
        internal void Awake()
        {
            if (Instance != null) { throw new Exception("ModInterface instance already exists"); }

            Instance = this;
        }

        #region Mod Collections

        /// <summary>
        /// All installed mods
        /// </summary>
        public IEnumerable<Hp2Mod> Mods => _mods;
        private List<Hp2Mod> _mods = new List<Hp2Mod>();

        /// <summary>
        /// All installed AbilityDataMods
        /// </summary>
        public IEnumerable<AbilityDataMod> AbilityMods => _abilityMods;
        private List<AbilityDataMod> _abilityMods = new List<AbilityDataMod>();

        /// <summary>
        /// All installed CodeDataMods
        /// </summary>
        public IEnumerable<AilmentDataMod> AilmentMods => _ailmentMods;
        private List<AilmentDataMod> _ailmentMods = new List<AilmentDataMod>();

        /// <summary>
        /// All installed ability data mods
        /// </summary>
        public IEnumerable<CodeDataMod> CodeMods => _codeMods;
        private List<CodeDataMod> _codeMods = new List<CodeDataMod>();

        /// <summary>
        /// All installed CutsceneDataMods
        /// </summary>
        public IEnumerable<CutsceneDataMod> CutsceneMods => _cutsceneMods;
        private List<CutsceneDataMod> _cutsceneMods = new List<CutsceneDataMod>();

        /// <summary>
        /// All installed DialogTriggerDataMods
        /// </summary>
        public IEnumerable<DialogTriggerDataMod> DialogTriggerMods => _dialogTriggerMods;
        private List<DialogTriggerDataMod> _dialogTriggerMods = new List<DialogTriggerDataMod>();

        /// <summary>
        /// All installed DlcDataMods
        /// </summary>
        public IEnumerable<DlcDataMod> DlcMods => _dlcMods;
        private List<DlcDataMod> _dlcMods = new List<DlcDataMod>();

        /// <summary>
        /// All installed EnergyDataMods
        /// </summary>
        public IEnumerable<EnergyDataMod> EnergyMods => _energyMods;
        private List<EnergyDataMod> _energyMods = new List<EnergyDataMod>();

        /// <summary>
        /// All installed GirlDataMods
        /// </summary>
        public IEnumerable<GirlDataMod> GirlMods => _girlMods;
        private List<GirlDataMod> _girlMods = new List<GirlDataMod>();

        /// <summary>
        /// All installed GirlPairDataMods
        /// </summary>
        public IEnumerable<GirlPairDataMod> GirlPairMods => _girlPairMods;
        private List<GirlPairDataMod> _girlPairMods = new List<GirlPairDataMod>();

        /// <summary>
        /// All installed ItemDataMods
        /// </summary>
        public IEnumerable<ItemDataMod> ItemMods => _itemMods;
        private List<ItemDataMod> _itemMods = new List<ItemDataMod>();

        /// <summary>
        /// All installed LocationDataMods
        /// </summary>
        public IEnumerable<LocationDataMod> LocationMods => _locationMods;
        private List<LocationDataMod> _locationMods = new List<LocationDataMod>();

        /// <summary>
        /// All installed PhotoDataMods
        /// </summary>
        public IEnumerable<PhotoDataMod> PhotoMods => _photoMods;
        private List<PhotoDataMod> _photoMods = new List<PhotoDataMod>();

        /// <summary>
        /// All installed QuestionDataMods
        /// </summary>
        public IEnumerable<QuestionDataMod> QuestionMods => _questionMods;
        private List<QuestionDataMod> _questionMods = new List<QuestionDataMod>();

        /// <summary>
        /// All installed TokenDataMods
        /// </summary>
        public IEnumerable<TokenDataMod> TokenMods => _tokenMods;
        private List<TokenDataMod> _tokenMods = new List<TokenDataMod>();

        #endregion

        /// <summary>
        /// How indented the log messages will be
        /// </summary>
        private int _logIndent;

        /// <summary>
        /// loader log
        /// </summary>
        private TextWriter _tw;

        /// <summary>
        /// Flags for referenceing at runtime cross mod
        /// </summary>
        private Dictionary<string, bool> Flags = new Dictionary<string, bool>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tw">loader log</param>
        internal ModInterface(TextWriter tw)
        {
            _tw = tw ?? throw new ArgumentNullException(nameof(tw));
        }

        /// <summary>
        /// Destructor, flushes the log
        /// </summary>
        ~ModInterface()
        {
            LogLine("Mod Interface destruction.");
            _tw.Flush();
        }

        /// <summary>
        /// How indented the log messages will be
        /// </summary>
        public void IncreaseLogIndent() => _logIndent++;

        /// <summary>
        /// How indented the log messages will be
        /// </summary>
        public void DecreaseLogIndent()
        {
            _logIndent--;
            if (_logIndent < 0)
            {
                _logIndent = 0;
            }
        }

        #region log

        /// <summary>
        /// outputs a newline to the loader log
        /// </summary>
        /// <param name="line"></param>
        public void LogNullCheck(object target, string name = null) => _tw.WriteLine($"Is {name} null? {target == null}");

        /// <summary>
        /// outputs a newline to the loader log
        /// </summary>
        /// <param name="line"></param>
        public void LogNewLine() => _tw.WriteLine();

        /// <summary>
        /// outputs to the loader log
        /// </summary>
        /// <param name="line"></param>

        public void LogLine([System.Runtime.CompilerServices.CallerMemberName] string line = "")
        {
            var lines = line.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var tab = new string(' ', _logIndent * 2);

            foreach (var l in lines)
            {
                _tw.WriteLine(tab + l);
            }

            _tw.Flush();
        }

        /// <summary>
        /// outputs a formatted title to the loader log
        /// </summary>
        /// <param name="line"></param>
        public void LogTitle(string line)
        {
            LogNewLine();
            LogLine($"-----{line}-----");
        }

        #endregion log



        /// <summary>
        /// Adds a mod
        /// </summary>
        /// <param name="mod"></param>
        internal void AddMod(Hp2Mod mod) => _mods.Add(mod);

        /// <summary>
        /// Adds a data mod to be loaded at runtime
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="data"></param>
        public void AddData<M>(M data)
        {
            if (IsAtRuntime)
            {
                LogLine($"Ivalid attempt to add dataMod durring game runtime. {data}");
                return;
            }

            if (data is AbilityDataMod asAbilityMod)
            {
                _abilityMods.Add(asAbilityMod);
            }
            else if (data is AilmentDataMod asAilmentMod)
            {
                _ailmentMods.Add(asAilmentMod);
            }
            else if (data is CodeDataMod asCodeMod)
            {
                _codeMods.Add(asCodeMod);
            }
            else if (data is CutsceneDataMod asCutsceneMod)
            {
                _cutsceneMods.Add(asCutsceneMod);
            }
            else if (data is DialogTriggerDataMod asDialogTriggerMod)
            {
                _dialogTriggerMods.Add(asDialogTriggerMod);
            }
            else if (data is DlcDataMod asDlcMod)
            {
                _dlcMods.Add(asDlcMod);
            }
            else if (data is EnergyDataMod asEnergyMod)
            {
                _energyMods.Add(asEnergyMod); ;
            }
            else if (data is GirlDataMod asGirlMod)
            {
                _girlMods.Add(asGirlMod);
            }
            else if (data is GirlPairDataMod asGirlPair)
            {
                _girlPairMods.Add(asGirlPair);
            }
            else if (data is ItemDataMod asItemMod)
            {
                _itemMods.Add(asItemMod);
            }
            else if (data is LocationDataMod asLocationMod)
            {
                _locationMods.Add(asLocationMod);
            }
            else if (data is PhotoDataMod asPhotoMod)
            {
                _photoMods.Add(asPhotoMod);
            }
            else if (data is QuestionDataMod asQuestionMod)
            {
                _questionMods.Add(asQuestionMod);
            }
            else if (data is TokenDataMod asTokenMod)
            {
                _tokenMods.Add(asTokenMod);
            }
            else
            {
                LogLine($"Ivalid attempt to add unknown dataMod type. {data}");
            }
        }

        /// <summary>
        /// Adds a data mod for gameDataType from a path
        /// </summary>
        /// <param name="path">type of mod</param>
        /// <param name="path">the path</param>
        public void AddData(GameDataType type, string path)
        {
            if (IsAtRuntime)
            {
                LogLine($"Inalid attempt to add dataMod durring game runtime. {path}");
                return;
            }

            switch (type)
            {
                case GameDataType.Ability:
                    _abilityMods.Add(DeserializeMod<AbilityDataMod>(path));
                    break;
                case GameDataType.Ailment:
                    _ailmentMods.Add(DeserializeMod<AilmentDataMod>(path));
                    break;
                case GameDataType.Code:
                    _codeMods.Add(DeserializeMod<CodeDataMod>(path));
                    break;
                case GameDataType.Cutscene:
                    _cutsceneMods.Add(DeserializeMod<CutsceneDataMod>(path));
                    break;
                case GameDataType.DialogTrigger:
                    _dialogTriggerMods.Add(DeserializeMod<DialogTriggerDataMod>(path));
                    break;
                case GameDataType.Dlc:
                    _dlcMods.Add(DeserializeMod<DlcDataMod>(path));
                    break;
                case GameDataType.Energy:
                    _energyMods.Add(DeserializeMod<EnergyDataMod>(path));
                    break;
                case GameDataType.Girl:
                    _girlMods.Add(DeserializeMod<GirlDataMod>(path));
                    break;
                case GameDataType.GirlPair:
                    _girlPairMods.Add(DeserializeMod<GirlPairDataMod>(path));
                    break;
                case GameDataType.Item:
                    _itemMods.Add(DeserializeMod<ItemDataMod>(path));
                    break;
                case GameDataType.Location:
                    _locationMods.Add(DeserializeMod<LocationDataMod>(path));
                    break;
                case GameDataType.Photo:
                    _photoMods.Add(DeserializeMod<PhotoDataMod>(path));
                    break;
                case GameDataType.Question:
                    _questionMods.Add(DeserializeMod<QuestionDataMod>(path));
                    break;
                case GameDataType.Token:
                    _tokenMods.Add(DeserializeMod<TokenDataMod>(path));
                    break;
                default:
                    throw new Exception("Unhandled GameDataType");
            }
        }

        /// <summary>
        /// Sets a flag to a value. If the flag doesn't exsist one is created
        /// </summary>
        /// <param name="name">The name of the flag.</param>
        /// <param name="value">The value to set the flag.</param>
        public void SetFlag(string name, bool value)
        {
            if (!Flags.ContainsKey(name))
            {
                Flags.Add(name, value);
            }
            else
            {
                Flags[name] = value;
            }
        }

        /// <summary>
        /// Attemplts to toggle a flag.
        /// </summary>
        /// <param name="name">The name of the flag to toggle.</param>
        /// <param name="result">The resulting value of the flag.</param>
        /// <returns>True if the flag exists, false otherwise.</returns>
        public bool TryToggleFlag(string name, out bool result)
        {
            if (!Flags.ContainsKey(name))
            {
                result = false;
                return false;
            }

            result = Flags[name] = !Flags[name];
            return true;
        }

        /// <summary>
        /// Checks the value of a flag.
        /// </summary>
        /// <param name="name">The name of the flag to check.</param>
        /// <param name="result">The value of the checked flag.</param>
        /// <returns>True if the flag exists, false otherwise.</returns>
        public bool TryCheckFlag(string name, out bool result)
        {
            if (!Flags.ContainsKey(name))
            {
                result = false;
                return false;
            }

            result = Flags[name];
            return true;
        }

        /// <summary>
        /// Deserializes a mod from a path
        /// </summary>
        /// <typeparam name="M">The type of definition modded</typeparam>
        /// <param name="path">The path to the mod file</param>
        /// <returns>The deserialaied mod</returns>
        private static M DeserializeMod<M>(string path)
            where M : class
        {
            var configString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject(configString, typeof(M)) as M;
        }

        /// <summary>
        /// Reads all data mods for a definition from a gameDataModder. Includes serialized and unserialised mods.
        /// </summary>
        /// <typeparam name="M">The mod type</typeparam>
        /// <returns>An IEnumerable of data mods for the definition</returns>
        internal IEnumerable<M> ReadMods<M>()
        {
            var type = typeof(M);

            if (type == typeof(AbilityDataMod))
            {
                return _abilityMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(AilmentDataMod))
            {
                return _ailmentMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(CodeDataMod))
            {
                return _codeMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(CutsceneDataMod))
            {
                return _cutsceneMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(DialogTriggerDataMod))
            {
                return _dialogTriggerMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(DlcDataMod))
            {
                return _dlcMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(EnergyDataMod))
            {
                return _energyMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(GirlDataMod))
            {
                return _girlMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(GirlPairDataMod))
            {
                return _girlPairMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(ItemDataMod))
            {
                return _itemMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(LocationDataMod))
            {
                return _locationMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(PhotoDataMod))
            {
                return _photoMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(QuestionDataMod))
            {
                return _questionMods.ToList() as IEnumerable<M>;
            }
            else if (type == typeof(TokenDataMod))
            {
                return _tokenMods.ToList() as IEnumerable<M>;
            }
            else
            {
                throw new Exception("GameDataModder.ModListsByType: Unhandled Type");
            }
        }
    }
}
