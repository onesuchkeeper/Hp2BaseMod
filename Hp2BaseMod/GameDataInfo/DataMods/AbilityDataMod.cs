// Hp2BaseMod 2021, By OneSuchKeeper

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
    /// Serializable information to make an AbilityDefinition
    /// </summary>
    public class AbilityDataMod : DataMod, IGameDataMod<AbilityDefinition>
    {
        public bool? SelectableTarget;

        public int? TargetMinimumCount;

        public TokenConditionSetInfo TargetConditionSetInfo;

        public List<AbilityStepInfo> Steps;

        /// <inheritdoc/>
        public AbilityDataMod() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insertStyle">The way in which mod data should be applied to the data instance.</param>
        public AbilityDataMod(RelativeId id, InsertStyle insertStyle, int loadPriority = 0)
            : base(id, insertStyle, loadPriority)
        {
        }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        internal AbilityDataMod(AbilityDefinition def, AssetProvider assetProvider)
            : base(new RelativeId(def), InsertStyle.replace, 0)
        {
            SelectableTarget = def.selectableTarget;
            TargetMinimumCount = def.targetMinimumCount;

            if (def.targetConditionSet != null) { TargetConditionSetInfo = new TokenConditionSetInfo(def.targetConditionSet); }
            Steps = def.steps?.Select(x => new AbilityStepInfo(x, assetProvider)).ToList();
        }

        /// <inheritdoc/>
        public void SetData(AbilityDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider)
        {
            ValidatedSet.SetValue(ref def.selectableTarget, SelectableTarget);
            ValidatedSet.SetValue(ref def.targetMinimumCount, TargetMinimumCount);

            ValidatedSet.SetValue(ref def.targetConditionSet, TargetConditionSetInfo, InsertStyle, gameDataProvider, assetProvider);

            ValidatedSet.SetListValue(ref def.steps, Steps, InsertStyle, gameDataProvider, assetProvider);
        }

        /// <inheritdoc/>
        public override void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewSource)
        {
            Id = getNewSource.Invoke(Id) ?? Id;

            foreach (var step in Steps.OrEmptyIfNull())
            {
                step?.ReplaceRelativeIds(getNewSource);
            }
        }
    }
}
