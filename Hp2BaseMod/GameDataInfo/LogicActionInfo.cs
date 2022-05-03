// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an LogicAction
    /// </summary>
    [Serializable]
    public class LogicActionInfo : IGameDataInfo<LogicAction>
    {
        [UiSonSelectorUi(DefaultData.LogicActionTypeNullable_As_String)]
        public LogicActionType? Type;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String)]
        public PuzzleAffectionType? AffectionType;

        [UiSonSelectorUi(DefaultData.PuzzleResourceTypeNullable_As_String)]
        public PuzzleResourceType? ResourceType;

        [UiSonSelectorUi(DefaultData.PuzzleGameStateNullable_As_String)]
        public PuzzleGameState? PuzzleState;

        [UiSonTextEditUi]
        public string StringValue;

        [UiSonTextEditUi]
        public float? FloatValue;

        [UiSonTextEditUi]
        public int? IntValue;

        [UiSonElementSelectorUi(nameof(LocationDataMod), 0, null, "Id", DefaultData.DefaultLocationNames_Name, DefaultData.DefaultLocationIds_Name)]
        public int? LocationDefinitionID;

        [UiSonElementSelectorUi(nameof(GirlPairDataMod), 0, null, "Id", DefaultData.DefaultGirlPairNames_Name, DefaultData.DefaultGirlPairIds_Name)]
        public int? GirlPairDefinitionID;

        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, null, "Id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public int? CutsceneDefinitionID;

        [UiSonElementSelectorUi(nameof(ItemDataMod), 0, null, "Id", DefaultData.DefaultItemNames_Name, DefaultData.DefaultItemIds_Name)]
        public int? ItemDefinitionID;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 0, null, "Id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public int? GirlDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? BoolValue;

        [UiSonMemberElement]
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
