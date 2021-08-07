// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a LocationDefinition
    /// </summary>
    [Serializable]
    public class LocationDataMod : DataMod<LocationDefinition>
    {
        public string LocationName;
        public LocationType? LocationType;
        public AudioKlipInfo BgMusic;
        public float? BgYOffset;
        public SpriteInfo FinderLocationIcon;
        public GirlStyleType? DateGirlStyleType;
        public string NonStopOptionText;
        public List<LocationSpecialLabelSubDefinition> SpecialLabels;
        public List<SpriteInfo> Backgrounds;
        public List<LogicBundleInfo> ArriveBundleList;
        public List<LogicBundleInfo> DepartBundleList;

        public LocationDataMod() { }

        public LocationDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
        {
        }

        public LocationDataMod(int id,
                               string locationName,
                               LocationType? locationType,
                               AudioKlipInfo bgMusic,
                               float? bgYOffset,
                               SpriteInfo finderLocationIcon,
                               GirlStyleType? dateGirlStyleType,
                               string nonStopOptionText,
                               List<LocationSpecialLabelSubDefinition> specialLabels,
                               List<SpriteInfo> backgrounds,
                               List<LogicBundleInfo> arriveBundleList,
                               List<LogicBundleInfo> departBundleList,
                               bool isAdditive = false)
            :base(id, isAdditive)
        {
            LocationName = locationName;
            LocationType = locationType;
            BgMusic = bgMusic;
            BgYOffset = bgYOffset;
            FinderLocationIcon = finderLocationIcon;
            DateGirlStyleType = dateGirlStyleType;
            NonStopOptionText = nonStopOptionText;
            SpecialLabels = specialLabels;
            Backgrounds = backgrounds;
            ArriveBundleList = arriveBundleList;
            DepartBundleList = departBundleList;
        }

        public LocationDataMod(LocationDefinition def, AssetProvider assetProvider)
            : base(def.id, false)
        {
            LocationName = def.locationName;
            LocationType = def.locationType;
            BgMusic = new AudioKlipInfo(def.bgMusic, assetProvider);
            BgYOffset = def.bgYOffset;
            FinderLocationIcon = new SpriteInfo(def.finderLocationIcon, assetProvider);
            DateGirlStyleType = def.dateGirlStyleType;
            NonStopOptionText = def.nonStopOptionText;
            SpecialLabels = def.specialLabels;
            Backgrounds = def.backgrounds.Select(x => new SpriteInfo(x, assetProvider)).ToList();
            ArriveBundleList = def.arriveBundleList.Select(x => new LogicBundleInfo(x, assetProvider)).ToList();
            DepartBundleList = def.departBundleList.Select(x => new LogicBundleInfo(x, assetProvider)).ToList();
        }

        public override void SetData(LocationDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullableSet(ref def.dateGirlStyleType, DateGirlStyleType);
            Access.NullableSet(ref def.bgYOffset, BgYOffset);
            Access.NullableSet(ref def.locationType, LocationType);

            Access.NullSet(ref def.locationName, LocationName);
            Access.NullSet(ref def.nonStopOptionText, NonStopOptionText);
            Access.NullSet(ref def.specialLabels, SpecialLabels);

            if (BgMusic != null) { def.bgMusic = BgMusic.ToAudioKlip(assetProvider); }
            if (FinderLocationIcon != null) { def.finderLocationIcon = FinderLocationIcon.ToSprite(assetProvider); }

            SetSprites(ref def.backgrounds, Backgrounds, assetProvider);
            SetLogicBundles(ref def.arriveBundleList, ArriveBundleList, gameData, assetProvider);
            SetLogicBundles(ref def.departBundleList, DepartBundleList, gameData, assetProvider);
        }
    }
}
