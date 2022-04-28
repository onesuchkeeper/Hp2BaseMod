// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an LogicAction
    /// </summary>
    [Serializable]
    public class LogicActionInfo : IGameDataInfo<LogicAction>
    {
        public LogicActionType? Type;
        public PuzzleAffectionType? AffectionType;
        public PuzzleResourceType? ResourceType;
        public PuzzleGameState? PuzzleState;
        public string StringValue;
        public float? FloatValue;
        public int? IntValue;
        public int? LocationDefinitionID;
        public int? GirlPairDefinitionID;
        public int? CutsceneDefinitionID;
        public int? ItemDefinitionID;
        public int? GirlDefinitionID;
        public bool? BoolValue;
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

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref LogicAction def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<LogicAction>();
            }

            ValidatedSet.SetValue(ref def.type, Type);
            ValidatedSet.SetValue(ref def.boolValue, BoolValue);
            ValidatedSet.SetValue(ref def.intValue, IntValue);
            ValidatedSet.SetValue(ref def.floatValue, FloatValue);
            ValidatedSet.SetValue(ref def.stringValue, StringValue, insertStyle);
            ValidatedSet.SetValue(ref def.affectionType, AffectionType);
            ValidatedSet.SetValue(ref def.resourceType, ResourceType);
            ValidatedSet.SetValue(ref def.puzzleState, PuzzleState);

            ValidatedSet.SetValue(ref def.locationDefinition, gameDataProvider.GetLocation(LocationDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.girlPairDefinition, gameDataProvider.GetGirlPair(GirlPairDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.itemDefinition, gameDataProvider.GetItem(ItemDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.girlDefinition, gameDataProvider.GetGirl(GirlDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.cutsceneDefinition, gameDataProvider.GetCutscene(CutsceneDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.backgroundMusic, BackgroundMusic, insertStyle, gameDataProvider, assetProvider);
        }
    }
}
