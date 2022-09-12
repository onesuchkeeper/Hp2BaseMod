using Hp2BaseMod;
using Hp2BaseMod.GameDataInfo;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Hp2CutContentMod
{
    public class Starter : IHp2ModStarter
    {
        private readonly string _dataPath = @"mods\HiddenWaterfall\hiddenWaterfall.dat";

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
        IEnumerable<IGirlPairDataMod> IProvideGameDataMods.GirlPairDataMods => null;
        IEnumerable<IGameDataMod<ItemDefinition>> IProvideGameDataMods.ItemDataMods => null;
        IEnumerable<IGameDataMod<PhotoDefinition>> IProvideGameDataMods.PhotoDataMods => null;
        IEnumerable<IGameDataMod<QuestionDefinition>> IProvideGameDataMods.QuestionDataMods => null;
        IEnumerable<IGameDataMod<TokenDefinition>> IProvideGameDataMods.TokenDataMods => null;

        public void Start(int modId)
        {
            var emptySprite = new SpriteInfo() { IsExternal = false, Path = "EmptySprite" };
            var backgrounds = new IGameDefinitionInfo<Sprite>[4];
            IGameDefinitionInfo<Sprite> icon;
            AudioKlipInfo hiddenWaterfallBGMusic;

            //load data
            if (File.Exists(_dataPath))
            {
                var data = FileUtility.SplitFile(_dataPath).ToArray();

                icon = new SpriteInfoBytes()
                {
                    Data = data[4]
                };

                //find config tags
                var mod = ModInterface.FindMod(modId);
                var ostPath = mod.Tags.FirstOrDefault(x => x.Name == "HiddenWaterfall_OST").Value ?? string.Empty;
                var artPath = mod.Tags.FirstOrDefault(x => x.Name == "HiddenWaterfall_DigitalArtCollection").Value ?? string.Empty;

                //find audio
                var hiddenWaterfallBGMusicPath = Path.Combine(ostPath, @"WAV/33 Hidden Waterfall (Bonus).wav");

                if (File.Exists(hiddenWaterfallBGMusicPath))
                {
                    hiddenWaterfallBGMusic = new AudioKlipInfo()
                    {
                        Volume = 0.5f,
                        AudioClipInfo = new AudioClipInfoBytes() { Data = data[5] }
                    };
                }
                else
                {
                    ModInterface.Log.LogError($"Unable to find {hiddenWaterfallBGMusicPath}, defaulting to bgm_secret_grotto");

                    hiddenWaterfallBGMusic = new AudioKlipInfo()
                    {
                        Volume = 0.5f,
                        AudioClipInfo = new AudioClipInfo() { IsExternal = false, Path = "bgm_secret_grotto" }
                    };
                }

                //find bg images
                var morningBg = Path.Combine(artPath, @"Misc\Cut Locations\Waterfall\Morning.jpg");
                backgrounds[0] = File.Exists(morningBg) ? (IGameDefinitionInfo<Sprite>)new SpriteInfoBytes() { Data = data[0] } : emptySprite;

                var afternoonBg = Path.Combine(artPath, @"Misc\Cut Locations\Waterfall\Afternoon.jpg");
                backgrounds[1] = File.Exists(morningBg) ? (IGameDefinitionInfo<Sprite>)new SpriteInfoBytes() { Data = data[1] } : emptySprite;

                var eveningBg = Path.Combine(artPath, @"Misc\Cut Locations\Waterfall\Evening.jpg");
                backgrounds[2] = File.Exists(morningBg) ? (IGameDefinitionInfo<Sprite>)new SpriteInfoBytes() { Data = data[2] } : emptySprite;

                var nightBg = Path.Combine(artPath, @"Misc\Cut Locations\Waterfall\Night.jpg");
                backgrounds[3] = File.Exists(morningBg) ? (IGameDefinitionInfo<Sprite>)new SpriteInfoBytes() { Data = data[3] } : emptySprite;
            }
            else
            {
                ModInterface.Log.LogError($"Unable to find {_dataPath}");

                backgrounds[0] = emptySprite;
                backgrounds[1] = emptySprite;
                backgrounds[2] = emptySprite;
                backgrounds[3] = emptySprite;

                icon = emptySprite;

                hiddenWaterfallBGMusic = new AudioKlipInfo()
                {
                    Volume = 0.5f,
                    AudioClipInfo = new AudioClipInfo() { IsExternal = false, Path = "bgm_secret_grotto" }
                };
            }

            var hiddenWaterfallLocationId = new RelativeId(modId, 0);

            // make data mod
            var girlStyles = new List<(RelativeId, GirlStyleInfo)>();
            foreach (var girl in ModInterface.Data.GetIds(GameDataType.Girl))
            {
                girlStyles.Add((girl, new GirlStyleInfo()
                {
                    HairstyleId = new RelativeId(-1, 4),
                    OutfitId = new RelativeId(-1, 4)
                }));
            }

            _locations = new ILocationDataMod[]
            {
                new LocationDataMod(hiddenWaterfallLocationId, InsertStyle.replace)
                {
                    LocationType = LocationType.SIM,
                    LocationName = "Hidden Waterfall",
                    GirlStyles = girlStyles,
                    NonStopOptionText = "Do you remember where we left that [[highlight]HIDDEN WATERFALL]?",
                    SpecialLabels = new List<LocationSpecialLabelSubDefinition>(),
                    BgMusic = hiddenWaterfallBGMusic,
                    BgYOffset = 0f,
                    Backgrounds = backgrounds.ToList(),
                    FinderLocationIcon = icon,
                    ArriveBundleList = new List<LogicBundleInfo>(),
                    DepartBundleList = new List<LogicBundleInfo>()
                }
            };
        }
    }
}
