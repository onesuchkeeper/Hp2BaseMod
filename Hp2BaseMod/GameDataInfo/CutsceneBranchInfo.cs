// Hp2BaseMod 2021, By OneSuchKeeper

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
    /// Serializable information to make a CutsceneBranchSubDefinition
    /// </summary>
    public class CutsceneBranchInfo : IGameDataInfo<CutsceneBranchSubDefinition>
    {
        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, null, "Id", DefaultData.DefaultCutsceneNames, DefaultData.DefaultCutsceneIds)]
        public int? CutsceneDefinitionID;

        [UiSonMemberElement]
        public List<LogicConditionInfo> Conditions;

        [UiSonMemberElement]
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

            if (cutsceneBranch.conditions != null) { Conditions = cutsceneBranch.conditions.Select(x => new LogicConditionInfo(x)).ToList(); }
            if (cutsceneBranch.steps != null) { Steps = cutsceneBranch.steps.Select(x => new CutsceneStepInfo(x, assetProvider)).ToList(); }
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref CutsceneBranchSubDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<CutsceneBranchSubDefinition>();
            }

            ValidatedSet.SetValue(ref def.cutsceneDefinition, gameDataProvider.GetCutscene(CutsceneDefinitionID), insertStyle);

            ValidatedSet.SetListValue(ref def.conditions, Conditions, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.steps, Steps, insertStyle, gameDataProvider, assetProvider);
        }
    }
}
