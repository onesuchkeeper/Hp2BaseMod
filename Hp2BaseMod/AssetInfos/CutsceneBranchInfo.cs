// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a CutsceneBranchSubDefinition
    /// </summary>
    [Serializable]
    public class CutsceneBranchInfo
    {
        public int CutsceneDefinitionID;
        public List<LogicConditionInfo> Conditions;
        public List<CutsceneStepInfo> Steps;

        public CutsceneBranchInfo() { }

        public CutsceneBranchInfo(int cutsceneDefinitionID, 
                                  List<LogicConditionInfo> conditions,
                                  List<CutsceneStepInfo> steps)
        {
            Conditions = conditions;
            CutsceneDefinitionID = cutsceneDefinitionID;
            Steps = steps;
        }

        public CutsceneBranchInfo(CutsceneBranchSubDefinition cutsceneBranch, AssetProvider assetProvider)
        {
            if (cutsceneBranch == null) { throw new ArgumentNullException(nameof(cutsceneBranch)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            CutsceneDefinitionID = cutsceneBranch.cutsceneDefinition?.id ?? -1;

            if (cutsceneBranch.conditions != null)  { Conditions = cutsceneBranch.conditions.Select(x => new LogicConditionInfo(x)).ToList(); }
            if (cutsceneBranch.steps != null) { Steps = cutsceneBranch.steps.Select(x => new CutsceneStepInfo(x, assetProvider)).ToList(); }
        }

        public CutsceneBranchSubDefinition ToCutsceneBranch(GameData gameData, AssetProvider assetProvider)
        {
            var newDef = new CutsceneBranchSubDefinition();

            newDef.cutsceneDefinition = gameData.Cutscenes.Get(CutsceneDefinitionID);

            if (Conditions != null) { newDef.conditions = Conditions.Select(x => x.ToLogicCondition(gameData)).ToList(); }
            if (Steps != null) { newDef.steps = Steps.Select(x => x.ToCutsceneStep(gameData, assetProvider)).ToList(); }

            return newDef;
        }
    }
}
