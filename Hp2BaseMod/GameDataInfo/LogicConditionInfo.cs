// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an LogicCondition
    /// </summary>
    public class LogicConditionInfo : IGameDataInfo<LogicCondition>
    {
        public LogicConditionType? Type;
        public NumberComparisonType? ComparisonType;
        public ClockDaytimeType? DaytimeType;
        public DollOrientationType? DollOrientation;
        public PuzzleAffectionType? AffectionType;
        public PuzzleResourceType? ResourceType;
        public SettingDifficulty? SettingDifficulty;
        public PuzzleStatusType? DateType;
        public string StringValue;
        public int? IntValue;
        public int? LocationDefinitionID;
        public int? GirlPairDefinitionID;
        public int? GirlDefinitionID;
        public int? ItemDefinitionID;
        public bool? Inverse;
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
