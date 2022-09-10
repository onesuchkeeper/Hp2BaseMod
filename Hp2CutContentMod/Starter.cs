using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Hp2CutContentMod
{
    public class Starter : IHp2ModStarter
    {
        private const string _configPath = @"mods/Hp2CutContentMod/CutContentConfig.json";

        IEnumerable<ILocationDataMod> IProvideGameDataMods.LocationDataMods => _locations;
        ILocationDataMod[] _locations;

        IEnumerable<IGameDataMod<AbilityDefinition>> IProvideGameDataMods.AbilityDataMods => null;
        IEnumerable<IGameDataMod<AilmentDefinition>> IProvideGameDataMods.AilmentDataMods => null;
        IEnumerable<IGameDataMod<CodeDefinition>> IProvideGameDataMods.CodeDataMods => null;
        IEnumerable<IGameDataMod<CutsceneDefinition>> IProvideGameDataMods.CutsceneDataMods => null;
        IEnumerable<IGameDataMod<DialogTriggerDefinition>> IProvideGameDataMods.DialogTriggerDataMods => null;
        IEnumerable<IGameDataMod<DlcDefinition>> IProvideGameDataMods.DlcDataMods => null;
        IEnumerable<IGameDataMod<EnergyDefinition>> IProvideGameDataMods.EnergyDataMods => null;
        IEnumerable<IGirlDataMod> IProvideGameDataMods.GirlDataMods => null;

        IEnumerable<IGirlPairDataMod> IProvideGameDataMods.GirlPairDataMods => _pairs;
        IGirlPairDataMod[] _pairs;

        IEnumerable<IGameDataMod<ItemDefinition>> IProvideGameDataMods.ItemDataMods => null;

        IEnumerable<IGameDataMod<PhotoDefinition>> IProvideGameDataMods.PhotoDataMods => null;
        IEnumerable<IGameDataMod<QuestionDefinition>> IProvideGameDataMods.QuestionDataMods => null;
        IEnumerable<IGameDataMod<TokenDefinition>> IProvideGameDataMods.TokenDataMods => null;

        public void Start(int modId)
        {
            var config = GetConfig();

            AudioKlipInfo hiddenWaterfallBGMusic;

            var hiddenWaterfallBGMusicPath = Path.Combine(config.Hunipop_2_Double_Date_OST_WAV_Path, @"33 Hidden Waterfall (Bonus).wav");

            if (!File.Exists(hiddenWaterfallBGMusicPath))
            {
                ModInterface.Log.LogError($"Unable to find {hiddenWaterfallBGMusicPath}, defaulting to bgm_secret_grotto");

                hiddenWaterfallBGMusic = new AudioKlipInfo()
                {
                    Volume = 0.5f,
                    AudioClipInfo = new AudioClipInfo() { IsExternal = false, Path = "bgm_secret_grotto" }
                };
            }
            else
            {
                hiddenWaterfallBGMusic = new AudioKlipInfo()
                {
                    Volume = 0.5f,
                    AudioClipInfo = new AudioClipInfo() { IsExternal = true, Path = hiddenWaterfallBGMusicPath }
                };
            }

            var hiddenWaterfallLocationId = new RelativeId(modId, 0);

            // hidden waterfall
            var styleId = new RelativeId(-1,4);
            _locations = new ILocationDataMod[]
            {
                new LocationDataMod(hiddenWaterfallLocationId, InsertStyle.replace)
                {
                    LocationType = LocationType.SIM,
                    LocationName = "Hidden Waterfall",
                    GirlStyles = new List<(RelativeId, GirlStyleInfo)>()
                    {
                        (new RelativeId(-1,1), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,2), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,3), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,4), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,5), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,6), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,7), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,8), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,9), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,10), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,11), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,12), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,13), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,14), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        }),
                        (new RelativeId(-1,15), new GirlStyleInfo()
                        {
                            HairstyleId = styleId,
                            OutfitId = styleId
                        })
                    },
                    NonStopOptionText = "Do you remember where we left that [[highlight]HIDDEN WATERFALL]?",
                    SpecialLabels = new List<LocationSpecialLabelSubDefinition>(),
                    BgMusic = hiddenWaterfallBGMusic,
                    BgYOffset = 0f,
                    Backgrounds = new List<SpriteInfo>()
                    {
                        new SpriteInfo()
                        {
                            IsExternal = true,
                            Path = @"mods/Hp2CutContentMod/Images/loc_bg_sim_hiddenwaterfall_0.png"
                        },
                        new SpriteInfo()
                        {
                            IsExternal = true,
                            Path = @"mods/Hp2CutContentMod/Images/loc_bg_sim_hiddenwaterfall_1.png"
                        },
                        new SpriteInfo()
                        {
                            IsExternal = true,
                            Path = @"mods/Hp2CutContentMod/Images/loc_bg_sim_hiddenwaterfall_2.png"
                        },
                        new SpriteInfo()
                        {
                            IsExternal = true,
                            Path = @"mods/Hp2CutContentMod/Images/loc_bg_sim_hiddenwaterfall_3.png"
                        }
                    },
                    FinderLocationIcon = new SpriteInfo ()
                    {
                        IsExternal = true,
                        Path = @"mods/Hp2CutContentMod/Images/ui_location_icon_hiddenwaterfall.png"
                    },
                    ArriveBundleList = new List<LogicBundleInfo>(),
                    DepartBundleList = new List<LogicBundleInfo>()
                }
            };

            // test, change zoey lola sex to hidden waterfall
            _pairs = new IGirlPairDataMod[]
            {
                new GirlPairDataMod(new RelativeId(-1, 16), InsertStyle.replace)
                {
                    SexLocationDefinitionID = hiddenWaterfallLocationId
                }
            };
        }

        private Config GetConfig()
        {
            if (!File.Exists(_configPath))
            {
                ModInterface.Log.LogLine($"Unable to find {_configPath}, creating a new one");

                var newConfig = new Config();
                newConfig.ApplyMissingDefaults();
                File.WriteAllText(_configPath, JsonConvert.SerializeObject(newConfig));
                return newConfig;
            }
            else
            {
                var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configPath));

                if (config == null)
                {
                    ModInterface.Log.LogLine($"Unable to read {_configPath}, proceeding with default");
                    return new Config();
                }
                else
                {
                    config.ApplyMissingDefaults();
                    return config;
                }
            }
        }
    }
}
