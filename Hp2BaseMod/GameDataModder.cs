// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hp2BaseMod.GameDataMods;
using Newtonsoft.Json;

namespace Hp2BaseMod
{
    /// <summary>
    /// Interface class to add in data mods.
    /// </summary>
    public class GameDataModder
    {
        private Dictionary<Type, GameDataType> TypeToGameDataType = new Dictionary<Type, GameDataType>()
        {
            { typeof(AbilityDataMod), GameDataType.Ability },
            { typeof(AilmentDataMod), GameDataType.Ailment },
            { typeof(CodeDataMod), GameDataType.Code },
            { typeof(CutsceneDataMod), GameDataType.Cutscene },
            { typeof(DialogTriggerDataMod), GameDataType.DialogTrigger },
            { typeof(DlcDataMod), GameDataType.Dlc },
            { typeof(EnergyDataMod), GameDataType.Energy },
            { typeof(GirlDataMod), GameDataType.Girl },
            { typeof(GirlPairDataMod), GameDataType.GirlPair },
            { typeof(ItemDataMod), GameDataType.Item },
            { typeof(LocationDataMod), GameDataType.Location },
            { typeof(PhotoDataMod), GameDataType.Photo },
            { typeof(QuestionDataMod), GameDataType.Question },
            { typeof(TokenDataMod), GameDataType.Token }
        };

        [JsonProperty]
        private List<AbilityDataMod> AbilityMods = new List<AbilityDataMod>();
        [JsonProperty]
        private List<AilmentDataMod> AilmentMods = new List<AilmentDataMod>();
        [JsonProperty]
        private List<CodeDataMod> CodeMods = new List<CodeDataMod>();
        [JsonProperty]
        private List<CutsceneDataMod> CutsceneMods = new List<CutsceneDataMod>();
        [JsonProperty]
        private List<DialogTriggerDataMod> DialogTriggerMods = new List<DialogTriggerDataMod>();
        [JsonProperty]
        private List<DlcDataMod> DlcMods = new List<DlcDataMod>();
        [JsonProperty]
        private List<EnergyDataMod> EnergyMods = new List<EnergyDataMod>();
        [JsonProperty]
        private List<GirlDataMod> GirlMods = new List<GirlDataMod>();
        [JsonProperty]
        private List<GirlPairDataMod> GirlPairMods = new List<GirlPairDataMod>();
        [JsonProperty]
        private List<ItemDataMod> ItemMods = new List<ItemDataMod>();
        [JsonProperty]
        private List<LocationDataMod> LocationMods = new List<LocationDataMod>();
        [JsonProperty]
        private List<PhotoDataMod> PhotoMods = new List<PhotoDataMod>();
        [JsonProperty]
        private List<QuestionDataMod> QuestionMods = new List<QuestionDataMod>();
        [JsonProperty]
        private List<TokenDataMod> TokenMods = new List<TokenDataMod>();

        [JsonProperty]
        private Dictionary<GameDataType, IList<string>> ModPathListsByType = new Dictionary<GameDataType, IList<string>>
        {
            { GameDataType.Ability, new List<string>() },
            { GameDataType.Ailment, new List<string>() },
            { GameDataType.Code, new List<string>() },
            { GameDataType.Cutscene, new List<string>() },
            { GameDataType.DialogTrigger, new List<string>() },
            { GameDataType.Dlc, new List<string>() },
            { GameDataType.Energy, new List<string>() },
            { GameDataType.Girl, new List<string>() },
            { GameDataType.GirlPair, new List<string>() },
            { GameDataType.Item, new List<string>() },
            { GameDataType.Location, new List<string>() },
            { GameDataType.Photo, new List<string>() },
            { GameDataType.Question, new List<string>() },
            { GameDataType.Token, new List<string>() }
        };

        /// <summary>
        /// loader log
        /// </summary>
        private TextWriter _tw;

        /// <summary>
        /// empty Constructor, needed for serialization
        /// </summary>
        public GameDataModder()
        {
        }

        /// <summary>
        /// Constructor with textWriter
        /// </summary>
        /// <param name="tw">loader log</param>
        public GameDataModder(TextWriter tw)
        {
            _tw = tw ?? throw new ArgumentNullException(nameof(tw));
        }

        /// <summary>
        /// outputs to the loader log
        /// </summary>
        /// <param name="line"></param>
        public void LogLine(string line)
        {
            _tw?.WriteLine("        " + line);
        }

        public void AddData(AbilityDataMod data) { AbilityMods.Add(data); }
        public void AddData(AilmentDataMod data) { AilmentMods.Add(data); }
        public void AddData(CodeDataMod data) { CodeMods.Add(data); }
        public void AddData(CutsceneDataMod data) { CutsceneMods.Add(data); }
        public void AddData(DialogTriggerDataMod data) { DialogTriggerMods.Add(data); }
        public void AddData(DlcDataMod data) { DlcMods.Add(data); }
        public void AddData(EnergyDataMod data) { EnergyMods.Add(data); }
        public void AddData(GirlDataMod data) { GirlMods.Add(data); }
        public void AddData(GirlPairDataMod data) { GirlPairMods.Add(data); }
        public void AddData(ItemDataMod data) { ItemMods.Add(data); }
        public void AddData(LocationDataMod data) { LocationMods.Add(data); }
        public void AddData(PhotoDataMod data) { PhotoMods.Add(data); }
        public void AddData(QuestionDataMod data) { QuestionMods.Add(data); }
        public void AddData(TokenDataMod data) { TokenMods.Add(data); }

        /// <summary>
        /// Adds a data mod for gameDataType from a path
        /// </summary>
        /// <param name="path">type of mod</param>
        /// <param name="path">the path</param>
        public void AddData(GameDataType type, string path)
        {
            ModPathListsByType[type].Add(path);
        }

        /// <summary>
        /// Reads all data mods for a definition from a gameDataModder. Includes serialized and unserialised mods.
        /// </summary>
        /// <typeparam name="M">The mod type</typeparam>
        /// <returns>An IEnumerable of data mods for the definition</returns>
        internal IEnumerable<M> ReadMods<M>()
            where M : class
        {
            IList<M> mods = ModListsByType<M>();

            foreach (var path in ModPathListsByType[TypeToGameDataType[typeof(M)]])
            {
                var deserializedMod = DeserializeMod<M>(path);
                if (deserializedMod != null)
                {
                    mods.Add(deserializedMod);
                }
            }

            return mods;
        }

        /// <summary>
        /// Deserialized a mod from a path
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

        private IList<M> ModListsByType<M>()
        {
            var type = typeof(M);

            if (type == typeof(AbilityDataMod))
            {
                return AbilityMods.ToList() as IList<M>;
            }
            else if (type == typeof(AilmentDataMod))
            {
                return AilmentMods.ToList() as IList<M>;
            }
            else if (type == typeof(CodeDataMod))
            {
                return CodeMods.ToList() as IList<M>;
            }
            else if (type == typeof(CutsceneDataMod))
            {
                return CutsceneMods.ToList() as IList<M>;
            }
            else if (type == typeof(DialogTriggerDataMod))
            {
                return DialogTriggerMods.ToList() as IList<M>;
            }
            else if (type == typeof(DlcDataMod))
            {
                return DlcMods.ToList() as IList<M>;
            }
            else if (type == typeof(EnergyDataMod))
            {
                return EnergyMods.ToList() as IList<M>;
            }
            else if (type == typeof(GirlDataMod))
            {
                return GirlMods.ToList() as IList<M>;
            }
            else if (type == typeof(GirlPairDataMod))
            {
                return GirlPairMods.ToList() as IList<M>;
            }
            else if (type == typeof(ItemDataMod))
            {
                return ItemMods.ToList() as IList<M>;
            }
            else if (type == typeof(LocationDataMod))
            {
                return LocationMods.ToList() as IList<M>;
            }
            else if (type == typeof(PhotoDataMod))
            {
                return PhotoMods.ToList() as IList<M>;
            }
            else if (type == typeof(QuestionDataMod))
            {
                return QuestionMods.ToList() as IList<M>;
            }
            else if (type == typeof(TokenDataMod))
            {
                return TokenMods.ToList() as IList<M>;
            }
            else
            {
                throw new Exception("GameDataModder.ModListsByType: Unhandled Type");
            }
        }
    }
}
