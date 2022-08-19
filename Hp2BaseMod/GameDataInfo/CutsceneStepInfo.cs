// Hp2BaseMod 2021, By OneSuchKeeper

using DG.Tweening;
using Hp2BaseMod.Extension.IEnumerableExtension;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an AudioKlip
    /// </summary>
    public class CutsceneStepInfo : IGameDefinitionInfo<CutsceneStepSubDefinition>
    {
        public CutsceneStepType? StepType;

        public CutsceneStepProceedType? ProceedType;

        public CutsceneStepDollTargetType? DollTargetType;

        public DollOrientationType? TargetDollOrientation;

        public GirlExpressionType? ExpressionType;

        public DollPositionType? DollPositionType;

        public CutsceneStepAnimationType? AnimationType;

        public CutsceneStepSubCutsceneType? SubCutsceneType;

        public CutsceneStepNotificationType? NotificationType;

        public GirlPairRelationshipType? GirlPairRelationshipType;

        public string SpecialStepPrefabName;

        public string BannerTextPrefabName;

        public string WindowPrefabName;

        public string EmitterBehaviorName;

        public string StringValue;

        public float? FloatValue;

        public float? ProceedFloat;

        public RelativeId? TargetGirlDefinitionID;

        public int? IntValue;

        public int? EaseType;

        public RelativeId? DialogTriggerDefinitionID;

        public RelativeId? GirlDefinitionID;

        public int? ExpressionIndex;

        public RelativeId? HairstyleId;

        public RelativeId? OutfitId;

        public RelativeId? SubCutsceneDefinitionID;

        public bool? SkipStep;

        public bool? TargetAlt;

        public bool? BoolValue;

        public bool? SetMood;

        public bool? ProceedBool;

        public VectorInfo PositionInfo;

        public AudioKlipInfo AudioKlipInfo;

        public LogicActionInfo LogicActionInfo;

        public RelativeId? DialogLineId;

        public List<CutsceneDialogOptionInfo> DialogOptionInfos;

        public List<CutsceneBranchInfo> BranchInfos;

        //not used in game, probably for huniedevs editing interface
        public int? EditorSelectedOptionIndex;

        //not used in game, probably for huniedevs editing interface
        public int? EditorSelectedBranchIndex;

        /// <summary>
        /// Constructor
        /// </summary>
        public CutsceneStepInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        public CutsceneStepInfo(CutsceneStepSubDefinition def, AssetProvider assetProvider)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            SkipStep = def.skipStep;
            StepType = def.stepType;
            ProceedType = def.proceedType;
            DollTargetType = def.dollTargetType;
            TargetDollOrientation = def.targetDollOrientation;
            TargetAlt = def.targetAlt;
            BoolValue = def.boolValue;
            IntValue = def.intValue;
            FloatValue = def.floatValue;
            StringValue = def.stringValue;
            EaseType = (int)def.easeType;
            EditorSelectedBranchIndex = def.editorSelectedBranchIndex;
            ExpressionType = def.expressionType;
            SetMood = def.setMood;
            EditorSelectedOptionIndex = def.editorSelectedOptionIndex;
            DollPositionType = def.dollPositionType;
            ExpressionIndex = def.expressionIndex;
            HairstyleId = new RelativeId(-1, def.hairstyleIndex);
            OutfitId = new RelativeId(-1, def.outfitIndex);
            AnimationType = def.animationType;
            SubCutsceneType = def.subCutsceneType;
            GirlPairRelationshipType = def.girlPairRelationshipType;
            NotificationType = def.notificationType;
            ProceedBool = def.proceedBool;
            ProceedFloat = def.proceedFloat;

            SubCutsceneDefinitionID = new RelativeId(def.subCutsceneDefinition);
            DialogTriggerDefinitionID = new RelativeId(def.dialogTriggerDefinition);
            GirlDefinitionID = new RelativeId(def.girlDefinition);
            TargetGirlDefinitionID = new RelativeId(def.targetGirlDefinition);

            assetProvider.NameAndAddAsset(ref SpecialStepPrefabName, def.specialStepPrefab);
            assetProvider.NameAndAddAsset(ref WindowPrefabName, def.windowPrefab);
            assetProvider.NameAndAddAsset(ref EmitterBehaviorName, def.emitterBehavior);
            assetProvider.NameAndAddAsset(ref BannerTextPrefabName, def.bannerTextPrefab);

            if (def.audioKlip != null) { AudioKlipInfo = new AudioKlipInfo(def.audioKlip, assetProvider); }
            if (def.position != null) { PositionInfo = new VectorInfo(def.position); }

            if (def.dialogTriggerDefinition != null
                && def.girlDefinition != null
                && def.dialogLine != null)
            {
                DialogLineId = ModInterface.Data.GetLineId(new RelativeId(def.dialogTriggerDefinition),
                                                           new RelativeId(def.girlDefinition),
                                                           def.dialogTriggerDefinition.GetLineSetByGirl(def.girlDefinition).dialogLines.IndexOf(def.dialogLine));
            }

            if (def.logicAction != null) { LogicActionInfo = new LogicActionInfo(def.logicAction, assetProvider); }

            if (def.dialogOptions != null) { DialogOptionInfos = def.dialogOptions.Select(x => new CutsceneDialogOptionInfo(x, assetProvider)).ToList(); }
            if (def.branches != null) { BranchInfos = def.branches.Select(x => new CutsceneBranchInfo(x, assetProvider)).ToList(); }
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameDataProvider">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref CutsceneStepSubDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<CutsceneStepSubDefinition>();
            }

            ValidatedSet.SetValue(ref def.skipStep, SkipStep);
            ValidatedSet.SetValue(ref def.stepType, StepType);
            ValidatedSet.SetValue(ref def.proceedType, ProceedType);
            ValidatedSet.SetValue(ref def.dollTargetType, DollTargetType);
            ValidatedSet.SetValue(ref def.targetDollOrientation, TargetDollOrientation);
            ValidatedSet.SetValue(ref def.targetAlt, TargetAlt);
            ValidatedSet.SetValue(ref def.boolValue, BoolValue);
            ValidatedSet.SetValue(ref def.intValue, IntValue);
            ValidatedSet.SetValue(ref def.floatValue, FloatValue);
            ValidatedSet.SetValue(ref def.stringValue, StringValue, insertStyle);
            ValidatedSet.SetValue(ref def.easeType, (Ease?)EaseType);
            ValidatedSet.SetValue(ref def.editorSelectedBranchIndex, EditorSelectedBranchIndex);
            ValidatedSet.SetValue(ref def.expressionType, ExpressionType);
            ValidatedSet.SetValue(ref def.setMood, SetMood);
            ValidatedSet.SetValue(ref def.editorSelectedOptionIndex, EditorSelectedOptionIndex);
            ValidatedSet.SetValue(ref def.dollPositionType, DollPositionType);
            ValidatedSet.SetValue(ref def.expressionIndex, ExpressionIndex);
            ValidatedSet.SetValue(ref def.hairstyleIndex, ModInterface.Data.GetHairstyleIndex(GirlDefinitionID, HairstyleId));
            ValidatedSet.SetValue(ref def.outfitIndex, ModInterface.Data.GetOutfitIndex(GirlDefinitionID, OutfitId));
            ValidatedSet.SetValue(ref def.animationType, AnimationType);
            ValidatedSet.SetValue(ref def.subCutsceneType, SubCutsceneType);
            ValidatedSet.SetValue(ref def.girlPairRelationshipType, GirlPairRelationshipType);
            ValidatedSet.SetValue(ref def.notificationType, NotificationType);
            ValidatedSet.SetValue(ref def.proceedBool, ProceedBool);
            ValidatedSet.SetValue(ref def.proceedFloat, ProceedFloat);

            ValidatedSet.SetValue(ref def.targetGirlDefinition, gameDataProvider.GetGirl(TargetGirlDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.girlDefinition, gameDataProvider.GetGirl(GirlDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.dialogTriggerDefinition, gameDataProvider.GetDialogTrigger(DialogTriggerDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.subCutsceneDefinition, gameDataProvider.GetCutscene(SubCutsceneDefinitionID), insertStyle);

            ValidatedSet.SetValue(ref def.windowPrefab, (UiWindow)assetProvider.GetAsset(WindowPrefabName), insertStyle);
            ValidatedSet.SetValue(ref def.emitterBehavior, (EmitterBehavior)assetProvider.GetAsset(EmitterBehaviorName), insertStyle);
            ValidatedSet.SetValue(ref def.specialStepPrefab, (CutsceneStepSpecial)assetProvider.GetAsset(SpecialStepPrefabName), insertStyle);
            ValidatedSet.SetValue(ref def.bannerTextPrefab, (BannerTextBehavior)assetProvider.GetAsset(BannerTextPrefabName), insertStyle);

            ValidatedSet.SetValue(ref def.logicAction, LogicActionInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.audioKlip, AudioKlipInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.position, PositionInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.branches, BranchInfos, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.dialogOptions, DialogOptionInfos, insertStyle, gameDataProvider, assetProvider);

            if (DialogLineId.HasValue
                && def.dialogTriggerDefinition != null
                && def.girlDefinition != null)
            {
                var dtId = ModInterface.Data.GetDataId(GameDataType.DialogTrigger, def.dialogTriggerDefinition.id);
                var girlId = ModInterface.Data.GetDataId(GameDataType.Girl, def.girlDefinition.id);

                def.dialogLine = def.dialogTriggerDefinition
                                    .GetLineSetByGirl(def.girlDefinition).dialogLines[ModInterface.Data.GetLineIndex(dtId, girlId, DialogLineId.Value)];
            }
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            TargetGirlDefinitionID = getNewId(TargetGirlDefinitionID);
            DialogTriggerDefinitionID = getNewId(DialogTriggerDefinitionID);
            GirlDefinitionID = getNewId(GirlDefinitionID);
            HairstyleId = getNewId(HairstyleId);
            OutfitId = getNewId(OutfitId);
            SubCutsceneDefinitionID = getNewId(SubCutsceneDefinitionID);
            LogicActionInfo?.ReplaceRelativeIds(getNewId);
            DialogLineId = getNewId(DialogLineId);

            foreach (var entry in DialogOptionInfos.OrEmptyIfNull())
            {
                entry?.ReplaceRelativeIds(getNewId);
            }

            foreach (var entry in BranchInfos.OrEmptyIfNull())
            {
                entry?.ReplaceRelativeIds(getNewId);
            }
        }
    }
}
