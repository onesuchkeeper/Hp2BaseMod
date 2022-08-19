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
    public class LogicActionInfo : IGameDefinitionInfo<LogicAction>
    {
        public LogicActionType? Type;

        public PuzzleAffectionType? AffectionType;

        public PuzzleResourceType? ResourceType;

        public PuzzleGameState? PuzzleState;

        public string StringValue;

        public float? FloatValue;

        public int? IntValue;

        public RelativeId? LocationDefinitionID;

        public RelativeId? GirlPairDefinitionID;

        public RelativeId? CutsceneDefinitionID;

        public RelativeId? ItemDefinitionID;

        public RelativeId? GirlDefinitionID;

        public bool? BoolValue;

        public AudioKlipInfo BackgroundMusic;

        /// <summary>
        /// Constructor
        /// </summary>
        public LogicActionInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        public LogicActionInfo(LogicAction def, AssetProvider assetProvider)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            Type = def.type;
            BoolValue = def.boolValue;
            IntValue = def.intValue;
            FloatValue = def.floatValue;
            StringValue = def.stringValue;
            AffectionType = def.affectionType;
            ResourceType = def.resourceType;
            PuzzleState = def.puzzleState;

            LocationDefinitionID = new RelativeId(def.locationDefinition);
            GirlPairDefinitionID = new RelativeId(def.girlPairDefinition);
            ItemDefinitionID = new RelativeId(def.itemDefinition);
            GirlDefinitionID = new RelativeId(def.girlDefinition);
            CutsceneDefinitionID = new RelativeId(def.cutsceneDefinition);

            if (def.backgroundMusic != null) { BackgroundMusic = new AudioKlipInfo(def.backgroundMusic, assetProvider); }
        }

        /// <inheritdoc/>
        public void SetData(ref LogicAction def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
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

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            LocationDefinitionID = getNewId(LocationDefinitionID);
            GirlPairDefinitionID = getNewId(GirlPairDefinitionID);
            CutsceneDefinitionID = getNewId(CutsceneDefinitionID);
            ItemDefinitionID = getNewId(ItemDefinitionID);
            GirlDefinitionID = getNewId(GirlDefinitionID);
        }
    }
}
