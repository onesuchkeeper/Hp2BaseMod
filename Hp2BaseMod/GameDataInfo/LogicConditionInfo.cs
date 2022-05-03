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
    public class LogicConditionInfo : IGameDataInfo<LogicCondition>
    {
        [UiSonSelectorUi(DefaultData.LogicConditionTypeNullable_As_String)]
        public LogicConditionType? Type;

        [UiSonSelectorUi(DefaultData.NumberComparisonTypeNullable_As_String)]
        public NumberComparisonType? ComparisonType;

        [UiSonSelectorUi(DefaultData.ClockDaytimeTypeNullable_As_String)]
        public ClockDaytimeType? DaytimeType;

        [UiSonSelectorUi(DefaultData.DollOrientationTypeNullable_As_String)]
        public DollOrientationType? DollOrientation;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String)]
        public PuzzleAffectionType? AffectionType;

        [UiSonSelectorUi(DefaultData.PuzzleResourceTypeNullable_As_String)]
        public PuzzleResourceType? ResourceType;

        [UiSonSelectorUi(DefaultData.SettingDifficultyNullable_As_String)]
        public SettingDifficulty? SettingDifficulty;

        [UiSonSelectorUi(DefaultData.PuzzleStatusTypeNullable_As_String)]
        public PuzzleStatusType? DateType;

        [UiSonTextEditUi]
        public string StringValue;

        [UiSonTextEditUi]
        public int? IntValue;

        [UiSonElementSelectorUi(nameof(LocationDataMod), 0, null, "Id", DefaultData.DefaultLocationNames_Name, DefaultData.DefaultLocationIds_Name)]
        public int? LocationDefinitionID;

        [UiSonElementSelectorUi(nameof(GirlPairDataMod), 0, null, "Id", DefaultData.DefaultGirlPairNames_Name, DefaultData.DefaultGirlPairIds_Name)]
        public int? GirlPairDefinitionID;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 0, null, "Id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public int? GirlDefinitionID;

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, null, "Id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public int? ItemDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? Inverse;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? BoolValue;

        public LogicConditionInfo() { }

        public LogicConditionInfo(LogicConditionType type,
                                  NumberComparisonType comparisonType,
                                  ClockDaytimeType daytimeType,
                                  DollOrientationType dollOrientation,
                                  PuzzleAffectionType affectionType,
                                  PuzzleResourceType resourceType,
                                  SettingDifficulty settingDifficulty,
                                  PuzzleStatusType dateType,
                                  string stringValue,
                                  int intValue,
                                  int locationDefinitionID,
                                  int girlPairDefinitionID,
                                  int girlDefinitionID,
                                  int itemDefinitionID,
                                  bool inverse,
                                  bool boolValue)
        {
            Type = type;
            Inverse = inverse;
            BoolValue = boolValue;
            IntValue = intValue;
            StringValue = stringValue;
            ComparisonType = comparisonType;
            LocationDefinitionID = locationDefinitionID;
            DaytimeType = daytimeType;
            GirlPairDefinitionID = girlPairDefinitionID;
            GirlDefinitionID = girlDefinitionID;
            DollOrientation = dollOrientation;
            AffectionType = affectionType;
            ResourceType = resourceType;
            ItemDefinitionID = itemDefinitionID;
            SettingDifficulty = settingDifficulty;
            DateType = dateType;
        }

        public LogicConditionInfo(LogicCondition logicCondition)
        {
            if (logicCondition == null) { throw new ArgumentNullException(nameof(logicCondition)); }

            Type = logicCondition.type;
            Inverse = logicCondition.inverse;
            BoolValue = logicCondition.boolValue;
            IntValue = logicCondition.intValue;
            StringValue = logicCondition.stringValue;
            ComparisonType = logicCondition.comparisonType;
            DaytimeType = logicCondition.daytimeType;
            DollOrientation = logicCondition.dollOrientation;
            AffectionType = logicCondition.affectionType;
            ResourceType = logicCondition.resourceType;
            SettingDifficulty = logicCondition.settingDifficulty;
            DateType = logicCondition.dateType;

            LocationDefinitionID = logicCondition.locationDefinition?.id ?? -1;
            GirlPairDefinitionID = logicCondition.girlPairDefinition?.id ?? -1;
            GirlDefinitionID = logicCondition.girlDefinition?.id ?? -1;
            ItemDefinitionID = logicCondition.itemDefinition?.id ?? -1;
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref LogicCondition def, GameDataProvider gameDataProvider, AssetProvider _, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a logic condition");
            ModInterface.Instance.IncreaseLogIndent();

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

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
