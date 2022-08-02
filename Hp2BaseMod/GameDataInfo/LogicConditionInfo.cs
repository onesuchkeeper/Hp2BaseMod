// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an LogicCondition
    /// </summary>
    public class LogicConditionInfo : IGameDefinitionInfo<LogicCondition>
    {
        [UiSonSelectorUi(DefaultData.LogicConditionTypeNullable)]
        public LogicConditionType? Type;

        [UiSonSelectorUi(DefaultData.NumberComparisonTypeNullable)]
        public NumberComparisonType? ComparisonType;

        [UiSonSelectorUi(DefaultData.ClockDaytimeTypeNullable)]
        public ClockDaytimeType? DaytimeType;

        [UiSonSelectorUi(DefaultData.DollOrientationTypeNullable)]
        public DollOrientationType? DollOrientation;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable)]
        public PuzzleAffectionType? AffectionType;

        [UiSonSelectorUi(DefaultData.PuzzleResourceTypeNullable)]
        public PuzzleResourceType? ResourceType;

        [UiSonSelectorUi(DefaultData.SettingDifficultyNullable)]
        public SettingDifficulty? SettingDifficulty;

        [UiSonSelectorUi(DefaultData.PuzzleStatusTypeNullable)]
        public PuzzleStatusType? DateType;

        [UiSonTextEditUi]
        public string StringValue;

        [UiSonTextEditUi]
        public int? IntValue;

        [UiSonElementSelectorUi(nameof(LocationDataMod), 0, null, "id", DefaultData.DefaultLocationNames_Name, DefaultData.DefaultLocationIds_Name)]
        public RelativeId? LocationDefinitionID;

        [UiSonElementSelectorUi(nameof(GirlPairDataMod), 0, null, "id", DefaultData.DefaultGirlPairNames_Name, DefaultData.DefaultGirlPairIds_Name)]
        public RelativeId? GirlPairDefinitionID;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 0, null, "id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public RelativeId? GirlDefinitionID;

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, null, "id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public RelativeId? ItemDefinitionID;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? Inverse;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? BoolValue;

        /// <summary>
        /// Constructor
        /// </summary>
        public LogicConditionInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public LogicConditionInfo(LogicCondition def)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }

            Type = def.type;
            Inverse = def.inverse;
            BoolValue = def.boolValue;
            IntValue = def.intValue;
            StringValue = def.stringValue;
            ComparisonType = def.comparisonType;
            DaytimeType = def.daytimeType;
            DollOrientation = def.dollOrientation;
            AffectionType = def.affectionType;
            ResourceType = def.resourceType;
            SettingDifficulty = def.settingDifficulty;
            DateType = def.dateType;

            LocationDefinitionID = new RelativeId(def.locationDefinition);
            GirlPairDefinitionID = new RelativeId(def.girlPairDefinition);
            GirlDefinitionID = new RelativeId(def.girlDefinition);
            ItemDefinitionID = new RelativeId(def.itemDefinition);
        }

        /// <inheritdoc/>
        public void SetData(ref LogicCondition def, GameDefinitionProvider gameDataProvider, AssetProvider _, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<LogicCondition>();
            }

            ValidatedSet.SetValue(ref def.type, Type);
            ValidatedSet.SetValue(ref def.inverse, Inverse);
            ValidatedSet.SetValue(ref def.boolValue, BoolValue);
            ValidatedSet.SetValue(ref def.intValue, IntValue);
            ValidatedSet.SetValue(ref def.stringValue, StringValue, insertStyle);
            ValidatedSet.SetValue(ref def.comparisonType, ComparisonType);
            ValidatedSet.SetValue(ref def.daytimeType, DaytimeType);
            ValidatedSet.SetValue(ref def.dollOrientation, DollOrientation);
            ValidatedSet.SetValue(ref def.affectionType, AffectionType);
            ValidatedSet.SetValue(ref def.resourceType, ResourceType);
            ValidatedSet.SetValue(ref def.settingDifficulty, SettingDifficulty);
            ValidatedSet.SetValue(ref def.dateType, DateType);

            ValidatedSet.SetValue(ref def.locationDefinition, gameDataProvider.GetLocation(LocationDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.itemDefinition, gameDataProvider.GetItem(ItemDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.girlPairDefinition, gameDataProvider.GetGirlPair(GirlPairDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.girlDefinition, gameDataProvider.GetGirl(GirlDefinitionID), insertStyle);
        }
    }
}
