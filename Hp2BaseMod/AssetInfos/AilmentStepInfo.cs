// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.AssetInfos
{
	/// <summary>
	/// Serializable information to make an AilmentStep
	/// </summary>
	[Serializable]
    public class AilmentStepInfo
    {
		public AilmentStepType StepType;
		public AilmentFlagType FlagType;
		public NumberComparisonType ComparisonType;
		public string StringValue;
		public int IntValue;
		public int AbilityDefinitionID;
		public bool BoolValue;
		public MoveModifier MoveModifier;
		public MatchModifierInfo MatchModifierInfo;
		public GiftModifier GiftModifier;
		public List<GirlConditionInfo> GirlConditionInfos;
		public List<MoveConditionInfo> MoveConditionInfos;
		public List<MatchConditionInfo> MatchConditionInfos;
		public List<GiftConditionInfo> GiftConditionInfos;

		public AilmentStepInfo() { }

		public AilmentStepInfo(AilmentStepType stepType,
							   NumberComparisonType comparisonType,
							   AilmentFlagType flagType,
							   string stringValue,
							   int abilityDefinitionID,
							   int intValue,
							   bool boolValue,
							   MoveModifier moveModifier,
							   MatchModifierInfo matchModifierInfo,
							   GiftModifier giftModifier,
							   List<GirlConditionInfo> girlConditionInfos,
							   List<MoveConditionInfo> moveConditionInfos,
							   List<MatchConditionInfo> matchConditionInfos,
							   List<GiftConditionInfo> giftConditionInfos)
        {
			StepType = stepType;
			StringValue = stringValue;
			IntValue = intValue;
			BoolValue = boolValue;
			ComparisonType = comparisonType;
			AbilityDefinitionID = abilityDefinitionID;
			MoveConditionInfos = moveConditionInfos;
			MoveModifier = moveModifier;
			MatchConditionInfos = matchConditionInfos;
			MatchModifierInfo = matchModifierInfo;
			GiftConditionInfos = giftConditionInfos;
			GiftModifier = giftModifier;
			GirlConditionInfos = girlConditionInfos;
			FlagType = flagType;
        }

		public AilmentStepInfo(AilmentStepSubDefinition ailmentStep)
		{
			if (ailmentStep == null) { throw new ArgumentNullException(nameof(ailmentStep)); }

			StepType = ailmentStep.stepType;
			StringValue = ailmentStep.stringValue;
			IntValue = ailmentStep.intValue;
			BoolValue = ailmentStep.boolValue;
			ComparisonType = ailmentStep.comparisonType;
			MoveModifier = ailmentStep.moveModifier;
			GiftModifier = ailmentStep.giftModifier;
			FlagType = ailmentStep.flagType;

			AbilityDefinitionID = ailmentStep.abilityDefinition?.id ?? -1;

			if (ailmentStep.matchModifier != null) { MatchModifierInfo = new MatchModifierInfo(ailmentStep.matchModifier); }

			if (ailmentStep.matchConditions != null) { MatchConditionInfos = ailmentStep.matchConditions.Select(x => new MatchConditionInfo(x)).ToList(); }
			if (ailmentStep.giftConditions != null) { GiftConditionInfos = ailmentStep.giftConditions.Select(x => new GiftConditionInfo(x)).ToList(); }
			if (ailmentStep.girlConditions != null) { GirlConditionInfos = ailmentStep.girlConditions.Select(x => new GirlConditionInfo(x)).ToList(); }
			if (ailmentStep.moveConditions != null) { MoveConditionInfos = ailmentStep.moveConditions.Select(x => new MoveConditionInfo(x)).ToList(); }
		}

		public AilmentStepSubDefinition ToAilmentStep(GameData gameData)
        {
			if (gameData == null) { throw new ArgumentNullException(nameof(gameData)); }

			var newAS = new AilmentStepSubDefinition();

			newAS.stepType = StepType;
			newAS.stringValue = StringValue;
			newAS.intValue = IntValue;
			newAS.boolValue = BoolValue;
			newAS.comparisonType = ComparisonType;
			newAS.moveModifier = MoveModifier;
			newAS.giftModifier = GiftModifier;
			newAS.flagType = FlagType;

			newAS.abilityDefinition = gameData.Abilities.Get(AbilityDefinitionID);

			if (MatchModifierInfo != null) { newAS.matchModifier = MatchModifierInfo.ToMatchModifier(gameData); }

			if (MoveConditionInfos != null) { newAS.moveConditions = MoveConditionInfos.Select(x => x.ToMoveCondition(gameData)).ToList(); }
			if (MatchConditionInfos != null) { newAS.matchConditions = MatchConditionInfos.Select(x => x.ToMatchCondition(gameData)).ToList(); }
			if (GiftConditionInfos != null) { newAS.giftConditions = GiftConditionInfos.Select(x => x.ToGiftCondition(gameData)).ToList(); }
			if (GirlConditionInfos != null) { newAS.girlConditions = GirlConditionInfos.Select(x => x.ToGirlCondition(gameData)).ToList(); }

			return newAS;
        }
    }
}
