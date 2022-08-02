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
    public class CutsceneBranchInfo : IGameDefinitionInfo<CutsceneBranchSubDefinition>
    {
        [UiSonElementSelectorUi(nameof(CutsceneDataMod), 0, null, "id", DefaultData.DefaultCutsceneNames_Name, DefaultData.DefaultCutsceneIds_Name)]
        public RelativeId? CutsceneDefinitionID;

        [UiSonEncapsulatingUi]
        public List<LogicConditionInfo> Conditions;

        [UiSonEncapsulatingUi]
        public List<CutsceneStepInfo> Steps;

        /// <summary>
        /// Constructor
        /// </summary>
        public CutsceneBranchInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        public CutsceneBranchInfo(CutsceneBranchSubDefinition def, AssetProvider assetProvider)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            CutsceneDefinitionID = new RelativeId(def.cutsceneDefinition);

            if (def.conditions != null) { Conditions = def.conditions.Select(x => new LogicConditionInfo(x)).ToList(); }
            if (def.steps != null) { Steps = def.steps.Select(x => new CutsceneStepInfo(x, assetProvider)).ToList(); }
        }

        /// <inheritdoc/>
        public void SetData(ref CutsceneBranchSubDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
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
