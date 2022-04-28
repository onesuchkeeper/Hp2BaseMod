// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a Color
    /// </summary>
    public class AilmentTriggerInfo : IGameDataInfo<AilmentTriggerSubDefinition>
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

        public AilmentTriggerInfo() { }

        public AilmentTriggerInfo(AilmentTriggerType triggerType,
                                  AilmentTriggerStepsProcessType stepsProcessType,
                                  float perentChance,
                                  int thresholdValue,
                                  int executeLimit,
                                  int verbalizedIndex,
                                  bool focusMatters,
                                  bool onUnfocused,
                                  bool exhaustionMatters,
                                  bool onExhausted,
                                  bool upsetMatters,
                                  bool onUpset,
                                  bool thresholdPersistent,
                                  bool defaultDisabled,
                                  bool audibalized,
                                  bool verbalized,
                                  List<AilmentStepInfo> stepInfos)
        {
            TriggerType = triggerType;
            FocusMatters = focusMatters;
            OnUnfocused = onUnfocused;
            ExhaustionMatters = exhaustionMatters;
            OnExhausted = onExhausted;
            UpsetMatters = upsetMatters;
            OnUpset = onUpset;
            ThresholdValue = thresholdValue;
            ThresholdPersistent = thresholdPersistent;
            PerentChance = perentChance;
            ExecuteLimit = executeLimit;
            DefaultDisabled = defaultDisabled;
            Audibalized = audibalized;
            Verbalized = verbalized;
            VerbalizedIndex = verbalizedIndex;
            StepsProcessType = stepsProcessType;
            StepInfos = stepInfos;
        }

        public AilmentTriggerInfo(AilmentTriggerSubDefinition ailmentTrigger)
        {
            TriggerType = ailmentTrigger.triggerType;
            FocusMatters = ailmentTrigger.focusMatters;
            OnUnfocused = ailmentTrigger.onUnfocused;
            ExhaustionMatters = ailmentTrigger.exhaustionMatters;
            OnExhausted = ailmentTrigger.onExhausted;
            UpsetMatters = ailmentTrigger.upsetMatters;
            OnUpset = ailmentTrigger.onUpset;
            ThresholdValue = ailmentTrigger.thresholdValue;
            ThresholdPersistent = ailmentTrigger.thresholdPersistent;
            PerentChance = ailmentTrigger.perentChance;
            ExecuteLimit = ailmentTrigger.executeLimit;
            DefaultDisabled = ailmentTrigger.defaultDisabled;
            Audibalized = ailmentTrigger.audibalized;
            Verbalized = ailmentTrigger.verbalized;
            VerbalizedIndex = ailmentTrigger.verbalizedIndex;
            StepsProcessType = ailmentTrigger.stepsProcessType;

            if (ailmentTrigger.steps != null) { StepInfos = ailmentTrigger.steps.Select(x => new AilmentStepInfo(x)).ToList(); }
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref AilmentTriggerSubDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
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
    }
}
