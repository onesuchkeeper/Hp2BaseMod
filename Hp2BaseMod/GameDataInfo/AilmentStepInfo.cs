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
    /// Serializable information to make an AilmentStep
    /// </summary>
    public class AilmentStepInfo : IGameDataInfo<AilmentStepSubDefinition>
    {
        [UiSonSelectorUi(DefaultData.AilmentStepTypeNullable_As_String)]
        public AilmentStepType? StepType;

        [UiSonSelectorUi(DefaultData.AilmentFlagTypeNullable_As_String)]
        public AilmentFlagType? FlagType;

        [UiSonSelectorUi(DefaultData.NumberComparisonTypeNullable_As_String)]
        public NumberComparisonType? ComparisonType;

        [UiSonTextEditUi]
        public string StringValue;

        [UiSonTextEditUi]
        public int? IntValue;

        [UiSonElementSelectorUi(nameof(AbilityDataMod), 0, null, "Id", DefaultData.DefaultAbilityNames, DefaultData.DefaultAbilityIds)]
        public int? AbilityDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? BoolValue;

        [UiSonMemberElement]
        public MoveModifier MoveModifier;

        [UiSonMemberElement]
        public MatchModifierInfo MatchModifierInfo;

        [UiSonMemberElement]
        public GiftModifier GiftModifier;

        [UiSonMemberElement]
        public List<GirlConditionInfo> GirlConditionInfos;

        [UiSonMemberElement]
        public List<MoveConditionInfo> MoveConditionInfos;

        [UiSonMemberElement]
        public List<MatchConditionInfo> MatchConditionInfos;

        [UiSonMemberElement]
        public List<GiftConditionInfo> GiftConditionInfos;

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public AilmentStepInfo() { }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref AilmentStepSubDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<AilmentStepSubDefinition>();
            }

            ValidatedSet.SetValue(ref def.stepType, StepType);
            ValidatedSet.SetValue(ref def.stringValue, StringValue, insertStyle);
            ValidatedSet.SetValue(ref def.intValue, IntValue);
            ValidatedSet.SetValue(ref def.boolValue, BoolValue);
            ValidatedSet.SetValue(ref def.comparisonType, ComparisonType);
            ValidatedSet.SetValue(ref def.moveModifier, MoveModifier, insertStyle);
            ValidatedSet.SetValue(ref def.giftModifier, GiftModifier, insertStyle);
            ValidatedSet.SetValue(ref def.flagType, FlagType);

            ValidatedSet.SetValue(ref def.abilityDefinition, gameDataProvider.GetAbility(AbilityDefinitionID), insertStyle);

            ValidatedSet.SetValue(ref def.matchModifier, MatchModifierInfo, insertStyle, gameDataProvider, assetProvider);

            ValidatedSet.SetListValue(ref def.girlConditions, GirlConditionInfos, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.moveConditions, MoveConditionInfos, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.matchConditions, MatchConditionInfos, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.giftConditions, GiftConditionInfos, insertStyle, gameDataProvider, assetProvider);
        }
    }
}
