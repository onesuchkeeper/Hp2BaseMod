﻿// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a LocationDefinition
    /// </summary>
    [UiSonElement]
    public class LocationDataMod : DataMod, IGameDataMod<LocationDefinition>
    {
        [UiSonTextEditUi]
        public string LocationName;

        [UiSonSelectorUi(DefaultData.LocationTypeNullable_As_String)]
        public LocationType? LocationType;

        [UiSonSelectorUi(DefaultData.GirlStyleTypeNullable_As_String)]
        public GirlStyleType? DateGirlStyleType;

        [UiSonTextEditUi]
        public string NonStopOptionText;

        [UiSonMemberElement]
        public List<LocationSpecialLabelSubDefinition> SpecialLabels;

        [UiSonMemberElement]
        public AudioKlipInfo BgMusic;

        [UiSonTextEditUi]
        public float? BgYOffset;

        [UiSonMemberElement]
        public List<SpriteInfo> Backgrounds;

        [UiSonMemberElement]
        public SpriteInfo FinderLocationIcon;

        [UiSonMemberElement]
        public List<LogicBundleInfo> ArriveBundleList;

        [UiSonMemberElement]
        public List<LogicBundleInfo> DepartBundleList;

        public LocationDataMod() { }

        public LocationDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
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
                               InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
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
            : base(def.id, InsertStyle.replace, def.name)
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

        public void SetData(LocationDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            def.id = Id;

            ValidatedSet.SetValue(ref def.dateGirlStyleType, DateGirlStyleType);
            ValidatedSet.SetValue(ref def.bgYOffset, BgYOffset);
            ValidatedSet.SetValue(ref def.locationType, LocationType);

            ValidatedSet.SetValue(ref def.locationName, LocationName, insertStyle);
            ValidatedSet.SetValue(ref def.nonStopOptionText, NonStopOptionText, insertStyle);
            ValidatedSet.SetValue(ref def.specialLabels, SpecialLabels, insertStyle);

            ValidatedSet.SetValue(ref def.bgMusic, BgMusic, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.finderLocationIcon, FinderLocationIcon, insertStyle, gameDataProvider, assetProvider);

            ValidatedSet.SetListValue(ref def.backgrounds, Backgrounds, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.arriveBundleList, ArriveBundleList, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.departBundleList, DepartBundleList, insertStyle, gameDataProvider, assetProvider);
        }
    }
}
