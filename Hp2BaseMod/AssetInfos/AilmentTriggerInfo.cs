// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a Color
    /// </summary>
    [Serializable]
    public class AilmentTriggerInfo
    {
		public AilmentTriggerType TriggerType;
		public AilmentTriggerStepsProcessType StepsProcessType;
		public float PerentChance;
		public int ThresholdValue;
		public int ExecuteLimit;
		public int VerbalizedIndex;
		public bool FocusMatters;
		public bool OnUnfocused;
		public bool ExhaustionMatters;
		public bool OnExhausted;
		public bool UpsetMatters;
		public bool OnUpset;
		public bool ThresholdPersistent;
		public bool DefaultDisabled;
		public bool Audibalized;
		public bool Verbalized;
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

		public AilmentTriggerSubDefinition ToAilmentTrigger(GameData gameData) 
        {
			if (gameData == null) { throw new ArgumentNullException(nameof(gameData)); }

			var newAT = new AilmentTriggerSubDefinition();

			newAT.triggerType = TriggerType;
			newAT.focusMatters = FocusMatters;
			newAT.onUnfocused = OnUnfocused;
			newAT.exhaustionMatters = ExhaustionMatters;
			newAT.onExhausted = OnExhausted;
			newAT.upsetMatters = UpsetMatters;
			newAT.onUpset = OnUpset;
			newAT.thresholdValue = ThresholdValue;
			newAT.thresholdPersistent = ThresholdPersistent;
			newAT.perentChance = PerentChance;
			newAT.executeLimit = ExecuteLimit;
			newAT.defaultDisabled = DefaultDisabled;
			newAT.audibalized = Audibalized;
			newAT.verbalized = Verbalized;
			newAT.verbalizedIndex = VerbalizedIndex;
			newAT.stepsProcessType = StepsProcessType;

			newAT.steps = StepInfos.Select(x => x.ToAilmentStep(gameData)).ToList();

			return newAT;
        }
    }
}
