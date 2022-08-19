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
    public class LogicConditionInfo : IGameDefinitionInfo<LogicCondition>
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

        public RelativeId? LocationDefinitionID;

        public RelativeId? GirlPairDefinitionID;

        public RelativeId? GirlDefinitionID;

        public RelativeId? ItemDefinitionID;

        public bool? Inverse;

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

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            LocationDefinitionID = getNewId(LocationDefinitionID);
            GirlPairDefinitionID = getNewId(GirlPairDefinitionID);
            GirlDefinitionID = getNewId(GirlDefinitionID);
            ItemDefinitionID = getNewId(ItemDefinitionID);
        }
    }
}
