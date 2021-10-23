// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Data;
using DataModEditor.Interfaces;
using Hp2BaseMod.GameDataMods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace DataModEditor
{
    public class GirlVM : BaseVM
    {
        public IEnumerable<string> AffectionTypes => _affectionTypes;
        private static List<string> _affectionTypes = new List<string>() {"Talent",
                                                                                 "Flirtation",
                                                                                 "Romance",
                                                                                 "Sexuality",
                                                                                 "null"};

        public IEnumerable<string> NullableBoolOptions => _nullableBoolOptions;
        private static List<string> _nullableBoolOptions = new List<string>() {"False",
                                                                                      "True",
                                                                                      "null"};

        public IEnumerable<string> ShoeOptions => _shoeOptions;
        private static List<string> _shoeOptions = new List<string>() {"Winter Boots",
                                                                              "Peep Toes",
                                                                              "Booties",
                                                                              "Cyber Boots",
                                                                              "Platforms",
                                                                              "Flip Flops",
                                                                              "Stripper Heels",
                                                                              "Sneakers",
                                                                              "Wedges",
                                                                              "Gladiators",
                                                                              "Flats",
                                                                              "Pumps",
                                                                              "null"};

        public IEnumerable<KeyValuePair<string, FavoriteVm>> FavoriteOptions =>
            Default.FavoriteTopics.Select(x => new KeyValuePair<string, FavoriteVm> (x.Value, _favoriteVmManager[x.Key]));

        public IEnumerable<string> UniqueItemOptions => _uniqueItemOptions;
        private static List<string> _uniqueItemOptions = new List<string>()
        {"Tailoring",
         "Alchohol",
         "Occult",
         "Spiritual",
         "Weeaboo",
         "Spa",
         "Toddler Toys",
         "Baby Boy",
         "Handbags",
         "Band",
         "Kinky",
         "Antiques",
         "null"};

        public IEnumerable<string> SpecialEffectOptions => _specialEffectOptions;
        private static List<string> _specialEffectOptions = new List<string>()
        {"FairyWingsKyu",
         "GloWingsMoxie",
         "GloWingsJewn",
         "null"};

        //WILL ALSO NEED SPECIAL PARTS, MAYBE, MAYBE JUST DITCH EM IDK. Need to change to dict for populate :/
        public IEnumerable<DollPartVm> DollParts => _dollParts;
        private IEnumerable<DollPartVm> _dollParts = new List<DollPartVm>()
        {new DollPartVm("Body"),
         new DollPartVm("Nipples"),
         new DollPartVm("Blush Light"),
         new DollPartVm("Blush Heavy"),
         new DollPartVm("Eyebrows Neutral"),
         new DollPartVm("Eyebrows Annoyed"),
         new DollPartVm("Eyebrows Disappointed"),
         new DollPartVm("Eyebrows Excited"),
         new DollPartVm("Eyebrows Confused"),
         new DollPartVm("Eyebrows inquisitive"),
         new DollPartVm("Eyebrows Sarcastic"),
         new DollPartVm("Eyebrows Shy"),
         new DollPartVm("Eyebrows Horny"),
         new DollPartVm("Eyes Neutral"),
         new DollPartVm("Eyes Annoyed"),
         new DollPartVm("Eyes Disappointed"),
         new DollPartVm("Eyes Excited"),
         new DollPartVm("Eyes Confused"),
         new DollPartVm("Eyes inquisitive"),
         new DollPartVm("Eyes Sarcastic"),
         new DollPartVm("Eyes Shy"),
         new DollPartVm("Eyes Horny"),
         new DollPartVm("Glow Eyes Neutral"),
         new DollPartVm("Glow Eyes Annoyed"),
         new DollPartVm("Glow Eyes Horny"),
         new DollPartVm("Eyes Blink"),
         new DollPartVm("Mouth Neutral"),
         new DollPartVm("Mouth Annoyed"),
         new DollPartVm("Mouth Disappointed"),
         new DollPartVm("Mouth Excited"),
         new DollPartVm("Mouth Confused"),
         new DollPartVm("Mouth inquisitive"),
         new DollPartVm("Mouth Sarcastic"),
         new DollPartVm("Mouth Shy"),
         new DollPartVm("Mouth Horny"),
         new DollPartVm("Mouth Open Neutral"),
         new DollPartVm("Mouth Open Annoyed"),
         new DollPartVm("Mouth Open Disappointed"),
         new DollPartVm("Mouth Open Excited"),
         new DollPartVm("Mouth Open Confused"),
         new DollPartVm("Mouth Open inquisitive"),
         new DollPartVm("Mouth Open Sarcastic"),
         new DollPartVm("Mouth Open Shy"),
         new DollPartVm("Mouth Open Horny"),
         new DollPartVm("Mouth Phonemes AEIL"),
         new DollPartVm("Mouth Phonemes BMP"),
         new DollPartVm("Mouth Phonemes OQUW"),
         new DollPartVm("Mouth Phonemes FV"),
         new DollPartVm("Mouth Phonemes Other"),
         new DollPartVm("Mouth Phonemes Teeth AEIL"),
         new DollPartVm("Mouth Phonemes Teeth BMP"),
         new DollPartVm("Mouth Phonemes Teeth OQUW"),
         new DollPartVm("Mouth Phonemes Teeth FV"),
         new DollPartVm("Mouth Phonemes Teeth Other")};
 
        public IEnumerable<DollOutfitVm> DollOutfits => _dollOutfits.Select(x =>x.Value).ToList();
        private Dictionary<GirlStyleType, DollOutfitVm> _dollOutfits = new Dictionary<GirlStyleType, DollOutfitVm>()
        { {GirlStyleType.RELAXING, new DollOutfitVm(GirlStyleType.RELAXING) },
          {GirlStyleType.ACTIVITY, new DollOutfitVm(GirlStyleType.ACTIVITY) },
          {GirlStyleType.ROMANTIC, new DollOutfitVm(GirlStyleType.ROMANTIC) },
          {GirlStyleType.PARTY, new DollOutfitVm(GirlStyleType.PARTY) },
          {GirlStyleType.WATER, new DollOutfitVm(GirlStyleType.WATER) },
          {GirlStyleType.SEXY, new DollOutfitVm(GirlStyleType.SEXY) },
          {GirlStyleType.BONUS1, new DollOutfitVm(GirlStyleType.BONUS1) },
          {GirlStyleType.BONUS2, new DollOutfitVm(GirlStyleType.BONUS2) },
          {GirlStyleType.BONUS3, new DollOutfitVm(GirlStyleType.BONUS3) },
          {GirlStyleType.BONUS4, new DollOutfitVm(GirlStyleType.BONUS4) } };

        public IEnumerable<DollHairstyleVm> DollHairstyles => _dollHairstyles.Select(x => x.Value).ToList();
        private Dictionary<GirlStyleType, DollHairstyleVm> _dollHairstyles = new Dictionary<GirlStyleType, DollHairstyleVm>()
        { {GirlStyleType.RELAXING, new DollHairstyleVm(GirlStyleType.RELAXING) },
        {GirlStyleType.ACTIVITY, new DollHairstyleVm(GirlStyleType.ACTIVITY) },
        {GirlStyleType.ROMANTIC, new DollHairstyleVm(GirlStyleType.ROMANTIC) },
        {GirlStyleType.PARTY, new DollHairstyleVm(GirlStyleType.PARTY) },
        {GirlStyleType.WATER, new DollHairstyleVm(GirlStyleType.WATER) },
        {GirlStyleType.SEXY, new DollHairstyleVm(GirlStyleType.SEXY) },
        {GirlStyleType.BONUS1, new DollHairstyleVm(GirlStyleType.BONUS1) },
        {GirlStyleType.BONUS2, new DollHairstyleVm(GirlStyleType.BONUS2) },
        {GirlStyleType.BONUS3, new DollHairstyleVm(GirlStyleType.BONUS3) },
        {GirlStyleType.BONUS4, new DollHairstyleVm(GirlStyleType.BONUS4) } };

        public string Title => $"{ModName} {(_unsavedEdits ? "*" : "")}";

        public bool UnsavedEdits
        {
            get => _unsavedEdits;
            set
            {
                if (_unsavedEdits != value)
                {
                    _unsavedEdits = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        private bool _unsavedEdits = false;

        public string ModName
        {
            get => _modName;
            set
            {
                if (_modName != value)
                {
                    _modName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        private string _modName;

        public string GirlId
        {
            get => _girlId.ToString();
            set
            {
                if (_girlId != value)
                {
                    _girlId = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private string _girlId = "10000";

        public string GirlName
        {
            get => _girlName ?? "null";
            set
            {
                if (_girlName != value)
                {
                    _girlName = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private string _girlName = "null";

        public string GirlNickName
        {
            get => _girlNickName ?? "null";
            set
            {
                if (_girlNickName != value)
                {
                    _girlNickName = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private string _girlNickName = "null" ;

        public string GirlAge
        {
            get => _girlAge ?? "null";
            set
            {
                if (_girlAge != value)
                {
                    _girlAge = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private string _girlAge = "null";

        public int SpecialCharacter
        {
            get => _specialCharacter;
            set
            {
                if (_specialCharacter != value)
                {
                    _specialCharacter = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private int _specialCharacter = 2;

        public int BossCharacter
        {
            get => _bossCharacter;
            set
            {
                if (_bossCharacter != value)
                {
                    _bossCharacter = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private int _bossCharacter = 2;

        public int HasAltStyles
        {
            get => _hasAltStyles;
            set
            {
                if (_hasAltStyles != value)
                {
                    _hasAltStyles = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private int _hasAltStyles = 2;

        public int FavoriteAffection
        {
            get => _favoriteAffection;
            set
            {
                if (_favoriteAffection != value)
                {
                    _favoriteAffection = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private int _favoriteAffection = 4;

        public int LeastFavoriteAffection
        {
            get => _leastFavotireAffectionType;
            set
            {
                if (_leastFavotireAffectionType != value)
                {
                    _leastFavotireAffectionType = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private int _leastFavotireAffectionType = 4;

        public string VoiceVolume
        {
            get => _voiceVolume;
            set
            {
                if (_voiceVolume != value)
                {
                    _voiceVolume = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private string _voiceVolume = "null";

        public string SexVoiceVolume
        {
            get => _sexVoiceVolume;
            set
            {
                if (_sexVoiceVolume != value)
                {
                    _sexVoiceVolume = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private string _sexVoiceVolume = "null";

        public int ShoesType
        {
            get => _shoesType;
            set
            {
                if (_shoesType != value)
                {
                    _shoesType = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private int _shoesType = 12;

        public string ShoesAdj
        {
            get => _shoesAdj;
            set
            {
                if (_shoesAdj != value)
                {
                    _shoesAdj = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private string _shoesAdj = "null";

        public int UniqueItemType
        {
            get => _uniqueItemType;
            set
            {
                if (_uniqueItemType != value)
                {
                    _uniqueItemType = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private int _uniqueItemType = 12;

        public string UniqueItemAdj
        {
            get => _itemAdj;
            set
            {
                if (_itemAdj != value)
                {
                    _itemAdj = value;
                    UnsavedEdits = true;
                    OnPropertyChanged();
                }
            }
        }
        private string _itemAdj = "null";

        public string CellphonePortrait { get; set; } = "null";
        public string CellphonePortraitAlt { get; set; } = "null";
        public string CellphoneHead { get; set; } = "null";
        public string CellphoneHeadAlt { get; set; } = "null";
        public string CellphoneMiniHead { get; set; } = "null";
        public string CellphoneMiniHeadAlt { get; set; } = "null";
        public string BreathPosX { get; set; } = "null";
        public string BreathPosY { get; set; } = "null";
        public string UpsetPosX { get; set; } = "null";
        public string UpsetPosY { get; set; } = "null";
        public int SpecialEffect { get; set; } = 3;
        public string SpecialEffectPosX { get; set; } = "null";
        public string SpecialEffectPosY { get; set; } = "null";
        public string AltStyleFlagName { get; set; } = "null";

        public ObservableCollection<BaggageVm> Baggage => _baggage;
        private ObservableCollection<BaggageVm> _baggage = new ObservableCollection<BaggageVm>();

        public ObservableCollection<UniqueItemVm> UniqueItems => _uniqueItems;
        private ObservableCollection<UniqueItemVm> _uniqueItems = new ObservableCollection<UniqueItemVm>();

        public ObservableCollection<ShoeVm> Shoes => _shoes;
        private ObservableCollection<ShoeVm> _shoes = new ObservableCollection<ShoeVm>();

        public ObservableCollection<GirlQuestionVm> Questions => _questions;
        private ObservableCollection<GirlQuestionVm> _questions = new ObservableCollection<GirlQuestionVm>();

        private FavoriteVmManager _favoriteVmManager;

        /// <summary>
        /// Base Constructor
        /// </summary>
        public GirlVM(FavoriteVmManager favoriteVmManager)
        {
            _favoriteVmManager = favoriteVmManager ?? throw new ArgumentNullException(nameof(favoriteVmManager));
        }

        /// <summary>
        /// Populating constructor
        /// </summary>
        /// <param name="girlDataMod">mod to populate from</param>
        /// <param name="girlModManager"></param>
        public GirlVM(FavoriteVmManager favoriteVmManager, GirlDataMod girlDataMod)
        {
            _favoriteVmManager = favoriteVmManager ?? throw new ArgumentNullException(nameof(favoriteVmManager));
            Populate(girlDataMod);
        }

        /// <summary>
        /// populates from a girl data mod, should really be a factory huh?
        /// </summary>
        /// <param name="girlDataMod"></param>
        public void Populate(GirlDataMod girlDataMod)
        {
            _modName = girlDataMod.ModName ?? "null";

            _girlId = girlDataMod.Id.ToString();
            _girlName = girlDataMod.GirlName ?? "null";
            _girlNickName = girlDataMod.GirlNickName ?? "null";
            _girlAge = girlDataMod.GirlAge?.ToString() ?? "null";

            _specialCharacter = Conversion.NullBoolToInt(girlDataMod.SpecialCharacter);
            _bossCharacter = Conversion.NullBoolToInt(girlDataMod.BossCharacter);
            _hasAltStyles = Conversion.NullBoolToInt(girlDataMod.HasAltStyles);

            _favoriteAffection = girlDataMod.FavoriteAffectionType.HasValue
                ? (int)girlDataMod.FavoriteAffectionType.Value
                : _affectionTypes.Count-1;
            _leastFavotireAffectionType = girlDataMod.LeastFavoriteAffectionType.HasValue
                ? (int)girlDataMod.LeastFavoriteAffectionType.Value
                : _affectionTypes.Count - 1;

            _voiceVolume = girlDataMod.VoiceVolume?.ToString() ?? "null";
            _sexVoiceVolume = girlDataMod.SexVoiceVolume?.ToString() ?? "null";

            CellphonePortrait = girlDataMod.CellphonePortrait?.IsExternal ?? false
                ? girlDataMod.CellphonePortrait.Path
                : "null";
            CellphonePortraitAlt = girlDataMod.CellphonePortraitAlt?.IsExternal ?? false
                ? girlDataMod.CellphonePortraitAlt.Path
                : "null";
            CellphoneHead = girlDataMod.CellphoneHead?.IsExternal ?? false
                ? girlDataMod.CellphoneHead.Path
                : "null";
            CellphoneHeadAlt = girlDataMod.CellphoneHeadAlt?.IsExternal ?? false
                ? girlDataMod.CellphoneHeadAlt.Path
                : "null";
            CellphoneMiniHead = girlDataMod.CellphoneMiniHead?.IsExternal ?? false
                ? girlDataMod.CellphoneMiniHead.Path
                : "null";
            CellphoneMiniHeadAlt = girlDataMod.CellphoneMiniHeadAlt?.IsExternal ?? false
                ? girlDataMod.CellphoneMiniHeadAlt.Path
                : "null";

            BreathPosX = girlDataMod.BreathEmitterPos?.Xpos.ToString() ?? "null";
            BreathPosY = girlDataMod.BreathEmitterPos?.Ypos.ToString() ?? "null";
            UpsetPosX = girlDataMod.UpsetEmitterPos?.Xpos.ToString() ?? "null";
            UpsetPosY = girlDataMod.UpsetEmitterPos?.Ypos.ToString() ?? "null";
            SpecialEffectPosX = girlDataMod.SpecialEffectOffset?.Xpos.ToString() ?? "null";
            SpecialEffectPosY = girlDataMod.SpecialEffectOffset?.Ypos.ToString() ?? "null";

            SpecialEffect = _specialEffectOptions.Contains(girlDataMod.SpecialEffectName)
                ? _specialEffectOptions.IndexOf(girlDataMod.SpecialEffectName)
                : _specialEffectOptions.Count-1;

            _shoesType = girlDataMod.ShoesType.HasValue ? (int)girlDataMod.ShoesType.Value : _shoeOptions.Count - 1;
            _shoesAdj = girlDataMod.ShoesAdj ?? "null";

            _uniqueItemType = girlDataMod.UniqueType.HasValue ? (int)girlDataMod.UniqueType.Value : _uniqueItemOptions.Count - 1;
            _itemAdj = girlDataMod.UniqueAdj ?? "null";

            foreach (var food in girlDataMod.BadFoodTypes)
            {
                //todo
            }

            foreach (var baggage in girlDataMod.BaggageItemDefIDs)
            {
                var newBaggageVm = new BaggageVm(_baggage);

                //VERY temp, fix when implimenting items manager
                newBaggageVm.Index= Default.ItemsBaggage.ContainsKey(baggage)
                    ? Default.ItemsBaggage.Select(x => x.Value).ToList().IndexOf(Default.ItemsBaggage[baggage])
                    : 0;

                _baggage.Add(newBaggageVm);
            }

            foreach (var uniqueItem in girlDataMod.UniqueItemDefIDs)
            {
                var newUniqueItemVm = new UniqueItemVm(_uniqueItems);

                //VERY temp, fix when implimenting items manager
                newUniqueItemVm.Index = Default.ItemsUniqueGift.ContainsKey(uniqueItem)
                    ? Default.ItemsUniqueGift.Select(x => x.Value).ToList().IndexOf(Default.ItemsUniqueGift[uniqueItem])
                    : 0;

                _uniqueItems.Add(newUniqueItemVm);
            }

            foreach (var shoe in girlDataMod.ShoesItemDefIDs)
            {
                var newShoeVm = new ShoeVm(_shoes);

                //VERY temp, fix when implimenting items manager
                newShoeVm.Index = Default.ItemsShoes.ContainsKey(shoe)
                    ? Default.ItemsShoes.Select(x => x.Value).ToList().IndexOf(Default.ItemsShoes[shoe])
                    : 0;

                _shoes.Add(newShoeVm);
            }

            AltStyleFlagName = girlDataMod.AltStylesFlagName ?? "null";
            //AltStylesCodeDefinitionID
            //ALL the parts
            //Default indexes
            foreach (var hairstyle in girlDataMod.Hairstyles)
            {
                _dollHairstyles[(GirlStyleType)hairstyle.pairOutfitIndex].Populate(girlDataMod);
            }

            foreach (var outfit in girlDataMod.Outfits)
            {
                _dollOutfits[(GirlStyleType)outfit.pairHairstyleIndex].Populate(girlDataMod);
            }
            //specialparts
            foreach(var question in girlDataMod.HerQuestions)
            {
                var newQuestion = new GirlQuestionVm(_questions);

                newQuestion.Populate(question);

                _questions.Add(newQuestion);
            }
        }

        /// <summary>
        /// Saves a GirlDataMod to the given path
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            // Don't run save yet, wait for all the data, else we may break smthin

            //var newGirlDataMod = new GirlDataMod()
            //{ Id = int.Parse(_girlId) };

            //var file = File.CreateText(path);
            //var jsonStr = JsonConvert.SerializeObject(newGirlDataMod);
            //file.Write(jsonStr);
            //file.Flush();
            //_unsavedEdits = false;
            //OnPropertyChanged(nameof(Title));
        }

        /// <summary>
        /// Adds a new baggage vm
        /// </summary>
        public void AddNewBaggage()
        {
            _baggage.Add(new BaggageVm(_baggage));
            OnPropertyChanged(nameof(Baggage));
        }

        /// <summary>
        /// Adds a new unique item vm
        /// </summary>
        public void AddNewUniqueItem()
        {
            _uniqueItems.Add(new UniqueItemVm(_uniqueItems));
            OnPropertyChanged(nameof(UniqueItems));
        }

        /// <summary>
        /// Adds a new unique item vm
        /// </summary>
        public void AddNewShoe()
        {
            _shoes.Add(new ShoeVm(_shoes));
            OnPropertyChanged(nameof(Shoes));
        }

        /// <summary>
        /// Adds a new question vm
        /// </summary>
        public void AddNewQuestion()
        {
            _questions.Add(new GirlQuestionVm(_questions));
            OnPropertyChanged(nameof(Questions));
        }
    }
}
