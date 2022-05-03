// Hp2BaseMod 2021, By OneSuchKeeper

using DG.Tweening;
using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an AudioKlip
    /// </summary>
    public class CutsceneStepInfo : IGameDataInfo<CutsceneStepSubDefinition>
    {
        [UiSonSelectorUi(DefaultData.CutsceneStepTypeNullable_As_String)]
        public CutsceneStepType? StepType;

        [UiSonSelectorUi(DefaultData.CutsceneStepProceedTypeNullable_As_String)]
        public CutsceneStepProceedType? ProceedType;

        [UiSonSelectorUi(DefaultData.CutsceneStepDollTargetTypeNullable_As_String)]
        public CutsceneStepDollTargetType? DollTargetType;

        [UiSonSelectorUi(DefaultData.DollOrientationTypeNullable_As_String)]
        public DollOrientationType? TargetDollOrientation;

        [UiSonSelectorUi(DefaultData.GirlExpressionTypeNullable_As_String)]
        public GirlExpressionType? ExpressionType;

        [UiSonSelectorUi(DefaultData.DollPositionTypeNullable_As_String)]
        public DollPositionType? DollPositionType;

        [UiSonSelectorUi(DefaultData.CutsceneStepAnimationTypeNullable_As_String)]
        public CutsceneStepAnimationType? AnimationType;

        [UiSonSelectorUi(DefaultData.CutsceneStepSubCutsceneTypeNullable_As_String)]
        public CutsceneStepSubCutsceneType? SubCutsceneType;

        [UiSonSelectorUi(DefaultData.CutsceneStepNotificationTypeNullable_As_String)]
        public CutsceneStepNotificationType? NotificationType;

        [UiSonSelectorUi(DefaultData.GirlPairRelationshipTypeNullable_As_String)]
        public GirlPairRelationshipType? GirlPairRelationshipType;

        [UiSonSelectorUi(DefaultData.SpecialStepNames_Name)]
        public string SpecialStepPrefabName;

        [UiSonSelectorUi(DefaultData.BannerTextNames_Name)]
        public string BannerTextPrefabName;

        [UiSonSelectorUi(DefaultData.UiWindowNames_Name)]
        public string WindowPrefabName;

        [UiSonSelectorUi(DefaultData.EmitterNames_Name)]
        public string EmitterBehaviorName;

        [UiSonTextEditUi]
        public string StringValue;

        [UiSonTextEditUi]
        public float? FloatValue;

        [UiSonTextEditUi]
        public float? ProceedFloat;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 0, null, "Id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public int? TargetGirlDefinitionID;

        [UiSonTextEditUi]
        public int? IntValue;

        [UiSonSelectorUi(DefaultData.EaseTypeNames_Name, 0, null, DefaultData.EaseTypeIds_Name)]
        public int? EaseType;

        [UiSonElementSelectorUi(nameof(DialogTriggerDataMod), 0, null, "Id", DefaultData.DefaultDialogTriggerNames_Name, DefaultData.DefaultDialogTriggerIds_Name)]
        public int? DialogTriggerDefinitionID;

        [UiSonElementSelectorUi(nameof(GirlDataMod), 0, null, "Id", DefaultData.DefaultGirlNames_Name, DefaultData.DefaultGirlIds_Name)]
        public int? GirlDefinitionID;

        [UiSonTextEditUi]
        public int? ExpressionIndex;

        [UiSonTextEditUi]
        public int? HairstyleIndex;

        [UiSonTextEditUi]
        public int? OutfitIndex;

        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, null, "Id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public int? SubCutsceneDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? SkipStep;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? TargetAlt;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? BoolValue;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? SetMood;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? ProceedBool;

        [UiSonMemberElement]
        public VectorInfo PositionInfo;

        [UiSonMemberElement]
        public AudioKlipInfo AudioKlipInfo;

        [UiSonMemberElement]
        public LogicActionInfo LogicActionInfo;

        [UiSonMemberElement]
        public DialogLineInfo DialogLineInfo;

        [UiSonMemberElement]
        public List<CutsceneDialogOptionInfo> DialogOptionInfos;

        [UiSonMemberElement]
        public List<CutsceneBranchInfo> BranchInfos;

        //not used in game, probably for huniedevs editing interface
        public int? EditorSelectedOptionIndex;

        //not used in game, probably for huniedevs editing interface
        public int? EditorSelectedBranchIndex;

        public CutsceneStepInfo() { }

        public CutsceneStepInfo(CutsceneStepType stepType,
                                CutsceneStepProceedType proceedType,
                                CutsceneStepDollTargetType dollTargetType,
                                GirlExpressionType expressionType,
                                DollOrientationType targetDollOrientation,
                                DollPositionType dollPositionType,
                                CutsceneStepAnimationType animationType,
                                CutsceneStepSubCutsceneType subCutsceneType,
                                GirlPairRelationshipType girlPairRelationshipType,
                                CutsceneStepNotificationType notificationType,
                                string specialStepPrefabName,
                                string bannerTextPrefabName,
                                string windowPrefabName,
                                string emitterBehaviorName,
                                string stringValue,
                                float floatValue,
                                float proceedFloat,
                                int targetGirlDefinitionID,
                                int girlDefinitionID,
                                int expressionIndex,
                                int hairstyleIndex,
                                int outfitIndex,
                                int intValue,
                                int easeType,
                                int editorSelectedBranchIndex,
                                int dialogTriggerDefinitionID,
                                int editorSelectedOptionIndex,
                                int subCutsceneDefinitionID,
                                bool skipStep,
                                bool targetAlt,
                                bool boolValue,
                                bool setMood,
                                bool proceedBool,
                                LogicActionInfo logicActionInfo,
                                DialogLineInfo dialogLineInfo,
                                AudioKlipInfo audioKlipInfo,
                                VectorInfo positionInfo,
                                List<CutsceneDialogOptionInfo> dialogOptionInfos,
                                List<CutsceneBranchInfo> branchInfos)
        {
            SkipStep = skipStep;
            StepType = stepType;
            ProceedType = proceedType;
            DollTargetType = dollTargetType;
            TargetGirlDefinitionID = targetGirlDefinitionID;
            TargetDollOrientation = targetDollOrientation;
            TargetAlt = targetAlt;
            BoolValue = boolValue;
            IntValue = intValue;
            FloatValue = floatValue;
            StringValue = stringValue;
            EaseType = easeType;
            EditorSelectedBranchIndex = editorSelectedBranchIndex;
            BranchInfos = branchInfos;
            LogicActionInfo = logicActionInfo;
            SpecialStepPrefabName = specialStepPrefabName;
            ExpressionType = expressionType;
            SetMood = setMood;
            DialogLineInfo = dialogLineInfo;
            DialogTriggerDefinitionID = dialogTriggerDefinitionID;
            EditorSelectedOptionIndex = editorSelectedOptionIndex;
            DialogOptionInfos = dialogOptionInfos;
            DollPositionType = dollPositionType;
            GirlDefinitionID = girlDefinitionID;
            ExpressionIndex = expressionIndex;
            HairstyleIndex = hairstyleIndex;
            OutfitIndex = outfitIndex;
            BannerTextPrefabName = bannerTextPrefabName;
            AnimationType = animationType;
            SubCutsceneType = subCutsceneType;
            SubCutsceneDefinitionID = subCutsceneDefinitionID;
            GirlPairRelationshipType = girlPairRelationshipType;
            WindowPrefabName = windowPrefabName;
            AudioKlipInfo = audioKlipInfo;
            EmitterBehaviorName = emitterBehaviorName;
            PositionInfo = positionInfo;
            NotificationType = notificationType;
            ProceedBool = proceedBool;
            ProceedFloat = proceedFloat;
        }

        public CutsceneStepInfo(CutsceneStepSubDefinition cutsceneStep, AssetProvider assetProvider)
        {
            if (cutsceneStep == null) { throw new ArgumentNullException(nameof(cutsceneStep)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            SkipStep = cutsceneStep.skipStep;
            StepType = cutsceneStep.stepType;
            ProceedType = cutsceneStep.proceedType;
            DollTargetType = cutsceneStep.dollTargetType;
            TargetDollOrientation = cutsceneStep.targetDollOrientation;
            TargetAlt = cutsceneStep.targetAlt;
            BoolValue = cutsceneStep.boolValue;
            IntValue = cutsceneStep.intValue;
            FloatValue = cutsceneStep.floatValue;
            StringValue = cutsceneStep.stringValue;
            EaseType = (int)cutsceneStep.easeType;
            EditorSelectedBranchIndex = cutsceneStep.editorSelectedBranchIndex;
            ExpressionType = cutsceneStep.expressionType;
            SetMood = cutsceneStep.setMood;
            EditorSelectedOptionIndex = cutsceneStep.editorSelectedOptionIndex;
            DollPositionType = cutsceneStep.dollPositionType;
            ExpressionIndex = cutsceneStep.expressionIndex;
            HairstyleIndex = cutsceneStep.hairstyleIndex;
            OutfitIndex = cutsceneStep.outfitIndex;
            AnimationType = cutsceneStep.animationType;
            SubCutsceneType = cutsceneStep.subCutsceneType;
            GirlPairRelationshipType = cutsceneStep.girlPairRelationshipType;
            NotificationType = cutsceneStep.notificationType;
            ProceedBool = cutsceneStep.proceedBool;
            ProceedFloat = cutsceneStep.proceedFloat;

            SubCutsceneDefinitionID = cutsceneStep.subCutsceneDefinition?.id ?? -1;
            DialogTriggerDefinitionID = cutsceneStep.dialogTriggerDefinition?.id ?? -1;
            GirlDefinitionID = cutsceneStep.girlDefinition?.id ?? -1;
            TargetGirlDefinitionID = cutsceneStep.targetGirlDefinition?.id ?? -1;

            assetProvider.NameAndAddAsset(ref SpecialStepPrefabName, cutsceneStep.specialStepPrefab);
            assetProvider.NameAndAddAsset(ref WindowPrefabName, cutsceneStep.windowPrefab);
            assetProvider.NameAndAddAsset(ref EmitterBehaviorName, cutsceneStep.emitterBehavior);
            assetProvider.NameAndAddAsset(ref BannerTextPrefabName, cutsceneStep.bannerTextPrefab);

            if (cutsceneStep.audioKlip != null) { AudioKlipInfo = new AudioKlipInfo(cutsceneStep.audioKlip, assetProvider); }
            if (cutsceneStep.position != null) { PositionInfo = new VectorInfo(cutsceneStep.position); }
            if (cutsceneStep.dialogLine != null) { DialogLineInfo = new DialogLineInfo(cutsceneStep.dialogLine, assetProvider); }
            if (cutsceneStep.logicAction != null) { LogicActionInfo = new LogicActionInfo(cutsceneStep.logicAction, assetProvider); }

            if (cutsceneStep.dialogOptions != null) { DialogOptionInfos = cutsceneStep.dialogOptions.Select(x => new CutsceneDialogOptionInfo(x, assetProvider)).ToList(); }
            if (cutsceneStep.branches != null) { BranchInfos = cutsceneStep.branches.Select(x => new CutsceneBranchInfo(x, assetProvider)).ToList(); }
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref CutsceneStepSubDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a cutscene step");
            ModInterface.Instance.IncreaseLogIndent();

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
            ValidatedSet.SetValue(ref def.hairstyleIndex, HairstyleIndex);
            ValidatedSet.SetValue(ref def.outfitIndex, OutfitIndex);
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

            ValidatedSet.SetValue(ref def.dialogLine, DialogLineInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.logicAction, LogicActionInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.audioKlip, AudioKlipInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.position, PositionInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.branches, BranchInfos, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.dialogOptions, DialogOptionInfos, insertStyle, gameDataProvider, assetProvider);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
