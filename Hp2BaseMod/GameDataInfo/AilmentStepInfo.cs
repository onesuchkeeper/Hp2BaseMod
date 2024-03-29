﻿// Hp2BaseMod 2021, By OneSuchKeeper

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
    /// Serializable information to make an AilmentStep
    /// </summary>
    public class AilmentStepInfo : IGameDefinitionInfo<AilmentStepSubDefinition>
    {
        public AilmentStepType? StepType;

        public AilmentFlagType? FlagType;

        public NumberComparisonType? ComparisonType;

        public string StringValue;

        public int? IntValue;

        public RelativeId? AbilityDefinitionID;

        public bool? BoolValue;

        public MoveModifier MoveModifier;

        public MatchModifierInfo MatchModifierInfo;

        public GiftModifier GiftModifier;

        public List<GirlConditionInfo> GirlConditionInfos;

        public List<MoveConditionInfo> MoveConditionInfos;

        public List<MatchConditionInfo> MatchConditionInfos;

        public List<GiftConditionInfo> GiftConditionInfos;

        /// <summary>
        /// Constructor
        /// </summary>
        public AilmentStepInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public AilmentStepInfo(AilmentStepSubDefinition def)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }

            StepType = def.stepType;
            StringValue = def.stringValue;
            IntValue = def.intValue;
            BoolValue = def.boolValue;
            ComparisonType = def.comparisonType;
            MoveModifier = def.moveModifier;
            GiftModifier = def.giftModifier;
            FlagType = def.flagType;

            AbilityDefinitionID = new RelativeId(def.abilityDefinition);

            if (def.matchModifier != null) { MatchModifierInfo = new MatchModifierInfo(def.matchModifier); }

            if (def.matchConditions != null) { MatchConditionInfos = def.matchConditions.Select(x => new MatchConditionInfo(x)).ToList(); }
            if (def.giftConditions != null) { GiftConditionInfos = def.giftConditions.Select(x => new GiftConditionInfo(x)).ToList(); }
            if (def.girlConditions != null) { GirlConditionInfos = def.girlConditions.Select(x => new GirlConditionInfo(x)).ToList(); }
            if (def.moveConditions != null) { MoveConditionInfos = def.moveConditions.Select(x => new MoveConditionInfo(x)).ToList(); }
        }

        /// <inheritdoc/>
        public void SetData(ref AilmentStepSubDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
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

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            AbilityDefinitionID = getNewId(AbilityDefinitionID);
            MatchModifierInfo?.ReplaceRelativeIds(getNewId);

            foreach (var entry in GirlConditionInfos.OrEmptyIfNull())
            {
                entry?.ReplaceRelativeIds(getNewId);
            }

            foreach (var entry in MoveConditionInfos.OrEmptyIfNull())
            {
                entry?.ReplaceRelativeIds(getNewId);
            }

            foreach (var entry in MatchConditionInfos.OrEmptyIfNull())
            {
                entry?.ReplaceRelativeIds(getNewId);
            }

            foreach (var entry in GiftConditionInfos.OrEmptyIfNull())
            {
                entry?.ReplaceRelativeIds(getNewId);
            }
        }
    }
}
