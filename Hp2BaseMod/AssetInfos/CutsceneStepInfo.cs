// Hp2BaseMod 2021, By OneSuchKeeper

using DG.Tweening;
using Hp2BaseMod.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make an AudioKlip
    /// </summary>
    [Serializable]
    public class CutsceneStepInfo
    {
        public CutsceneStepType StepType;
        public CutsceneStepProceedType ProceedType;
        public CutsceneStepDollTargetType DollTargetType;
        public DollOrientationType TargetDollOrientation;
        public GirlExpressionType ExpressionType;
        public DollPositionType DollPositionType;
        public CutsceneStepAnimationType AnimationType;
        public CutsceneStepSubCutsceneType SubCutsceneType;
        public CutsceneStepNotificationType NotificationType;
        public GirlPairRelationshipType GirlPairRelationshipType;
        public string SpecialStepPrefabName;
        public string BannerTextPrefabName;
        public string WindowPrefabName;
        public string EmitterBehaviorName;
        public string StringValue;
        public float FloatValue;
        public float ProceedFloat;
        public int TargetGirlDefinitionID;
        public int IntValue;
        public int EaseType;
        public int EditorSelectedBranchIndex;
        public int DialogTriggerDefinitionID;
        public int EditorSelectedOptionIndex;
        public int GirlDefinitionID;
        public int ExpressionIndex;
        public int HairstyleIndex;
        public int OutfitIndex;
        public int SubCutsceneDefinitionID;
        public bool SkipStep;
        public bool TargetAlt;
        public bool BoolValue;
        public bool SetMood;
        public bool ProceedBool;
        public VectorInfo PositionInfo;
        public AudioKlipInfo AudioKlipInfo;
        public LogicActionInfo LogicActionInfo;
        public DialogLineInfo DialogLineInfo;
        public List<CutsceneDialogOptionInfo> DialogOptionInfos;
        public List<CutsceneBranchInfo> BranchInfos;

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

        public CutsceneStepSubDefinition ToCutsceneStep(GameData gameData, AssetProvider assetProvider)
        {
            var newCSS = new CutsceneStepSubDefinition();

            newCSS.skipStep = SkipStep;
            newCSS.stepType = StepType;
            newCSS.proceedType = ProceedType;
            newCSS.dollTargetType = DollTargetType;
            newCSS.targetDollOrientation = TargetDollOrientation;
            newCSS.targetAlt = TargetAlt;
            newCSS.boolValue = BoolValue;
            newCSS.intValue = IntValue;
            newCSS.floatValue = FloatValue;
            newCSS.stringValue = StringValue;
            newCSS.easeType = (Ease)EaseType;
            newCSS.editorSelectedBranchIndex = EditorSelectedBranchIndex;
            newCSS.expressionType = ExpressionType;
            newCSS.setMood = SetMood;
            newCSS.editorSelectedOptionIndex = EditorSelectedOptionIndex;
            newCSS.dollPositionType = DollPositionType;
            newCSS.expressionIndex = ExpressionIndex;
            newCSS.hairstyleIndex = HairstyleIndex;
            newCSS.outfitIndex = OutfitIndex;
            newCSS.animationType = AnimationType;
            newCSS.subCutsceneType = SubCutsceneType;
            newCSS.girlPairRelationshipType = GirlPairRelationshipType;
            newCSS.notificationType = NotificationType;
            newCSS.proceedBool = ProceedBool;
            newCSS.proceedFloat = ProceedFloat;

            newCSS.targetGirlDefinition = gameData.Girls.Get(TargetGirlDefinitionID);
            newCSS.girlDefinition = gameData.Girls.Get(GirlDefinitionID);
            newCSS.dialogTriggerDefinition = gameData.DialogTriggers.Get(DialogTriggerDefinitionID);
            newCSS.subCutsceneDefinition = gameData.Cutscenes.Get(SubCutsceneDefinitionID);

            newCSS.windowPrefab = (UiWindow)assetProvider.GetAsset(WindowPrefabName);
            newCSS.emitterBehavior = (EmitterBehavior)assetProvider.GetAsset(EmitterBehaviorName);
            newCSS.specialStepPrefab = (CutsceneStepSpecial)assetProvider.GetAsset(SpecialStepPrefabName);
            newCSS.bannerTextPrefab = (BannerTextBehavior)assetProvider.GetAsset(BannerTextPrefabName);

            if (DialogLineInfo != null) { newCSS.dialogLine = DialogLineInfo.ToDialogLine(assetProvider); }
            if (LogicActionInfo != null) { newCSS.logicAction = LogicActionInfo.ToLogicAction(gameData, assetProvider); }
            if (AudioKlipInfo != null) { newCSS.audioKlip = AudioKlipInfo.ToAudioKlip(assetProvider); }
            if (PositionInfo != null) { newCSS.position = PositionInfo.ToVector2(); }

            if (BranchInfos != null) { newCSS.branches = BranchInfos.Select(x => x.ToCutsceneBranch(gameData, assetProvider)).ToList(); }
            if (DialogOptionInfos != null) { newCSS.dialogOptions = DialogOptionInfos.Select(x => x.ToCutsceneDialogOption(gameData, assetProvider)).ToList(); }

            return newCSS;
        }
    }
}
