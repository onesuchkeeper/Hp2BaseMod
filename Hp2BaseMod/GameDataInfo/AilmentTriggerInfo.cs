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
    /// Serializable information to make an ailment trigger
    /// </summary>
    public class AilmentTriggerInfo : IGameDefinitionInfo<AilmentTriggerSubDefinition>
    {
        public AilmentTriggerType? TriggerType;

        public AilmentTriggerStepsProcessType? StepsProcessType;

        public float? PerentChance;

        public int? ThresholdValue;

        public int? ExecuteLimit;

        public int? VerbalizedIndex;

        public bool? FocusMatters;

        public bool? OnUnfocused;

        public bool? ExhaustionMatters;

        public bool? OnExhausted;

        public bool? UpsetMatters;

        public bool? OnUpset;

        public bool? ThresholdPersistent;

        public bool? DefaultDisabled;

        public bool? Audibalized;

        public bool? Verbalized;

        public List<AilmentStepInfo> StepInfos;

        /// <summary>
        /// Constructor
        /// </summary>
        public AilmentTriggerInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public AilmentTriggerInfo(AilmentTriggerSubDefinition def)
        {
            TriggerType = def.triggerType;
            FocusMatters = def.focusMatters;
            OnUnfocused = def.onUnfocused;
            ExhaustionMatters = def.exhaustionMatters;
            OnExhausted = def.onExhausted;
            UpsetMatters = def.upsetMatters;
            OnUpset = def.onUpset;
            ThresholdValue = def.thresholdValue;
            ThresholdPersistent = def.thresholdPersistent;
            PerentChance = def.perentChance;
            ExecuteLimit = def.executeLimit;
            DefaultDisabled = def.defaultDisabled;
            Audibalized = def.audibalized;
            Verbalized = def.verbalized;
            VerbalizedIndex = def.verbalizedIndex;
            StepsProcessType = def.stepsProcessType;

            if (def.steps != null) { StepInfos = def.steps.Select(x => new AilmentStepInfo(x)).ToList(); }
        }

        /// <inheritdoc/>
        public void SetData(ref AilmentTriggerSubDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<AilmentTriggerSubDefinition>();
            }

            ValidatedSet.SetValue(ref def.triggerType, TriggerType);
            ValidatedSet.SetValue(ref def.focusMatters, FocusMatters);
            ValidatedSet.SetValue(ref def.onUnfocused, OnUnfocused);
            ValidatedSet.SetValue(ref def.exhaustionMatters, ExhaustionMatters);
            ValidatedSet.SetValue(ref def.onExhausted, OnExhausted);
            ValidatedSet.SetValue(ref def.upsetMatters, UpsetMatters);
            ValidatedSet.SetValue(ref def.onUpset, OnUpset);
            ValidatedSet.SetValue(ref def.thresholdValue, ThresholdValue);
            ValidatedSet.SetValue(ref def.thresholdPersistent, ThresholdPersistent);
            ValidatedSet.SetValue(ref def.perentChance, PerentChance);
            ValidatedSet.SetValue(ref def.executeLimit, ExecuteLimit);
            ValidatedSet.SetValue(ref def.defaultDisabled, DefaultDisabled);
            ValidatedSet.SetValue(ref def.audibalized, Audibalized);
            ValidatedSet.SetValue(ref def.verbalized, Verbalized);
            ValidatedSet.SetValue(ref def.verbalizedIndex, VerbalizedIndex);
            ValidatedSet.SetValue(ref def.stepsProcessType, StepsProcessType);

            ValidatedSet.SetListValue(ref def.steps, StepInfos, insertStyle, gameDataProvider, assetProvider);
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            foreach (var step in StepInfos.OrEmptyIfNull())
            {
                step?.ReplaceRelativeIds(getNewId);
            }
        }
    }
}
