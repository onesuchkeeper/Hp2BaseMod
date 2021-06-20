// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make an LogicBundle
    /// </summary>
    [Serializable]
    public class LogicBundleInfo
    {
        public List<LogicConditionInfo> Conditions;
        public List<LogicActionInfo> Actions;

        public LogicBundleInfo() { }

        public LogicBundleInfo(List<LogicConditionInfo> conditions,
                               List<LogicActionInfo> actions)
        {
            Conditions = conditions;
            Actions = actions;
        }

        public LogicBundleInfo(LogicBundle logicBundle, AssetProvider assetProvider)
        {
            if (logicBundle == null) { throw new ArgumentNullException(nameof(logicBundle)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            if (logicBundle.conditions != null) { Conditions = logicBundle.conditions.Select(x => new LogicConditionInfo(x)).ToList(); }
            if (logicBundle.actions != null) { Actions = logicBundle.actions.Select(x => new LogicActionInfo(x, assetProvider)).ToList(); }
        }

        public LogicBundle ToLogicBundle(GameData gameData, AssetProvider assetProvider)
        {
            var newLogicBundle = new LogicBundle();

            if (Conditions != null) { newLogicBundle.conditions = Conditions.Select(x => x.ToLogicCondition(gameData)).ToList(); }
            if (Actions != null) { newLogicBundle.actions = Actions.Select(x => x.ToLogicAction(gameData, assetProvider)).ToList(); }

            return newLogicBundle;
        }
    }

    /// <summary>
    /// Serializable information to make an LogicCondition
    /// </summary>
    [Serializable]
    public class LogicConditionInfo
    {
        public LogicConditionType Type;
        public NumberComparisonType ComparisonType;
        public ClockDaytimeType DaytimeType;
        public DollOrientationType DollOrientation;
        public PuzzleAffectionType AffectionType;
        public PuzzleResourceType ResourceType;
        public SettingDifficulty SettingDifficulty;
        public PuzzleStatusType DateType;
        public string StringValue;
        public int IntValue;
        public int LocationDefinitionID;
        public int GirlPairDefinitionID;
        public int GirlDefinitionID;
        public int ItemDefinitionID;
        public bool Inverse;
        public bool BoolValue;

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

        public LogicCondition ToLogicCondition(GameData gameData)
        {
            var newLogicCondition = new LogicCondition();

            newLogicCondition.type = Type;
            newLogicCondition.inverse = Inverse;
            newLogicCondition.boolValue = BoolValue;
            newLogicCondition.intValue = IntValue;
            newLogicCondition.stringValue = StringValue;
            newLogicCondition.comparisonType = ComparisonType;
            newLogicCondition.daytimeType = DaytimeType;
            newLogicCondition.dollOrientation = DollOrientation;
            newLogicCondition.affectionType = AffectionType;
            newLogicCondition.resourceType = ResourceType;
            newLogicCondition.settingDifficulty = SettingDifficulty;
            newLogicCondition.dateType = DateType;

            newLogicCondition.locationDefinition = gameData.Locations.Get(LocationDefinitionID);
            newLogicCondition.itemDefinition = gameData.Items.Get(ItemDefinitionID);
            newLogicCondition.girlPairDefinition = gameData.GirlPairs.Get(GirlPairDefinitionID);
            newLogicCondition.girlDefinition = gameData.Girls.Get(GirlDefinitionID);

            return newLogicCondition;
        }
    }

    /// <summary>
    /// Serializable information to make an LogicAction
    /// </summary>
    [Serializable]
    public class LogicActionInfo
    {
        public LogicActionType Type;
        public PuzzleAffectionType AffectionType;
        public PuzzleResourceType ResourceType;
        public PuzzleGameState PuzzleState;
        public string StringValue;
        public float FloatValue;
        public int IntValue;
        public int LocationDefinitionID;
        public int GirlPairDefinitionID;
        public int CutsceneDefinitionID;
        public int ItemDefinitionID;
        public int GirlDefinitionID;
        public bool BoolValue;
        public AudioKlipInfo BackgroundMusic;

        public LogicActionInfo() { }

        public LogicActionInfo(LogicActionType type,
                               PuzzleAffectionType affectionType,
                               PuzzleResourceType resourceType,
                               PuzzleGameState puzzleState,
                               string stringValue,
                               float floatValue,
                               int intValue,
                               int locationDefinitionID,
                               int girlPairDefinitionID,
                               int cutsceneDefinitionID,
                               int itemDefinitionID,
                               int girlDefinitionID,
                               bool boolValue,
                               AudioKlipInfo backgroundMusic)
        {
            Type = type;
            BoolValue = boolValue;
            IntValue = intValue;
            FloatValue = floatValue;
            StringValue = stringValue;
            LocationDefinitionID = locationDefinitionID;
            GirlPairDefinitionID = girlPairDefinitionID;
            AffectionType = affectionType;
            ResourceType = resourceType;
            CutsceneDefinitionID = cutsceneDefinitionID;
            PuzzleState = puzzleState;
            ItemDefinitionID = itemDefinitionID;
            GirlDefinitionID = girlDefinitionID;
            BackgroundMusic = backgroundMusic;
        }

        public LogicActionInfo(LogicAction logicAction, AssetProvider assetProvider)
        {
            if (logicAction == null) { throw new ArgumentNullException(nameof(logicAction)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            Type = logicAction.type;
            BoolValue = logicAction.boolValue;
            IntValue = logicAction.intValue;
            FloatValue = logicAction.floatValue;
            StringValue = logicAction.stringValue;
            AffectionType = logicAction.affectionType;
            ResourceType = logicAction.resourceType;
            PuzzleState = logicAction.puzzleState;

            LocationDefinitionID = logicAction.locationDefinition?.id ?? -1;
            GirlPairDefinitionID = logicAction.girlPairDefinition?.id ?? -1;
            ItemDefinitionID = logicAction.itemDefinition?.id ?? -1;
            GirlDefinitionID = logicAction.girlDefinition?.id ?? -1;
            CutsceneDefinitionID = logicAction.cutsceneDefinition?.id ?? -1;

            if (logicAction.backgroundMusic != null) { BackgroundMusic = new AudioKlipInfo(logicAction.backgroundMusic, assetProvider); }
        }

        public LogicAction ToLogicAction(GameData gameData, AssetProvider assetProvider)
        {
            var newLogicAction = new LogicAction();

            newLogicAction.type = Type;
            newLogicAction.boolValue = BoolValue;
            newLogicAction.intValue = IntValue;
            newLogicAction.floatValue = FloatValue;
            newLogicAction.stringValue = StringValue;
            newLogicAction.affectionType = AffectionType;
            newLogicAction.resourceType = ResourceType;
            newLogicAction.puzzleState = PuzzleState;

            newLogicAction.locationDefinition = gameData.Locations.Get(LocationDefinitionID);
            newLogicAction.girlPairDefinition = gameData.GirlPairs.Get(GirlPairDefinitionID);
            newLogicAction.itemDefinition = gameData.Items.Get(ItemDefinitionID);
            newLogicAction.girlDefinition = gameData.Girls.Get(GirlDefinitionID);
            newLogicAction.cutsceneDefinition = gameData.Cutscenes.Get(CutsceneDefinitionID);

            if (BackgroundMusic != null) { newLogicAction.backgroundMusic = BackgroundMusic.ToAudioKlip(assetProvider); }

            return newLogicAction;
        }
    }
}
