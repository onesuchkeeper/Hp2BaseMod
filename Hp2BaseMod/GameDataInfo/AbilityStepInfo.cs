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
    /// Serializable information to make a sprite
    /// </summary>
    public class AbilityStepInfo : IGameDefinitionInfo<AbilityStepSubDefinition>
    {
        public AbilityStepType? StepType;

        public AbilityStepValueType? ValueType;

        public AbilityStepConditionType? ConditionType;

        public AbilityStepVisualEffectType? VisualEffectType;

        public AbilityStepVisualEffectEnergyType? EnergyType;

        public AbilityStepAilmentAlterType? AilmentAlterType;

        public AbilityStepAilmentTargetType? AilmentTargetType;

        public PuzzleResourceType? ResourceType;

        public PuzzleAffectionType? AffectionType;

        public NumberCombineOperation? CombineOperation;

        public GirlValueType? GirlValueType;

        public ItemType? AilmentItemType;

        public string Handle;

        public string ValueRef;

        public string PuzzleSetRef;

        public string MinRequirement;

        public string Min;

        public string Max;

        public string Limit;

        public string SplashText;

        public float? PercentOfValue;

        public int? AilmentIndex;

        public RelativeId? AilmentDefinitionID;

        public RelativeId? EnergyDefinitionID;

        public bool? Negative;

        public bool? OppositeGirl;

        public bool? ResourceMaxValue;

        public bool? Merged;

        public bool? FlatMerge;

        public bool? OrCheck;

        public bool? Weighted;

        public TokenConditionSetInfo TokenConditionSetInfo;

        public AudioKlipInfo AudioKlipInfo;

        public List<string> CombineValues;

        public List<RelativeId?> TokenDefinitionIDs;

        /// <summary>
        /// Constructor
        /// </summary>
        public AbilityStepInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        public AbilityStepInfo(AbilityStepSubDefinition def, AssetProvider assetProvider)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            StepType = def.stepType;
            Handle = def.handle;
            ValueRef = def.valueRef;
            PuzzleSetRef = def.puzzleSetRef;
            MinRequirement = def.minRequirement;
            ResourceType = def.resourceType;
            AffectionType = def.affectionType;
            Negative = def.negative;
            OppositeGirl = def.oppositeGirl;
            ValueType = def.valueType;
            Min = def.min;
            Max = def.max;
            CombineValues = def.combineValues;
            CombineOperation = def.combineOperation;
            ResourceMaxValue = def.resourceMaxValue;
            GirlValueType = def.girlValueType;
            ConditionType = def.conditionType;
            PercentOfValue = def.percentOfValue;
            OrCheck = def.orCheck;
            Limit = def.limit;
            Merged = def.merged;
            FlatMerge = def.flatMerge;
            Weighted = def.weighted;
            VisualEffectType = def.visualEffectType;
            EnergyType = def.energyType;
            SplashText = def.splashText;
            AilmentItemType = def.ailmentItemType;
            AilmentAlterType = def.ailmentAlterType;
            AilmentTargetType = def.ailmentTargetType;
            AilmentIndex = def.ailmentIndex;

            AilmentDefinitionID = new RelativeId(def.ailmentDefinition);
            EnergyDefinitionID = new RelativeId(def.energyDefinition);

            if (def.audioKlip != null) { AudioKlipInfo = new AudioKlipInfo(def.audioKlip, assetProvider); }
            if (def.tokenConditionSet != null) { TokenConditionSetInfo = new TokenConditionSetInfo(def.tokenConditionSet); }

            if (def.tokenDefinitions != null) { TokenDefinitionIDs = def.tokenDefinitions.Select(x => (RelativeId?)new RelativeId(x)).ToList(); }
        }

        /// <inheritdoc/>
        public void SetData(ref AbilityStepSubDefinition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<AbilityStepSubDefinition>();
            }

            ValidatedSet.SetValue(ref def.stepType, StepType);
            ValidatedSet.SetValue(ref def.handle, Handle, insertStyle);
            ValidatedSet.SetValue(ref def.valueRef, ValueRef, insertStyle);
            ValidatedSet.SetValue(ref def.puzzleSetRef, PuzzleSetRef, insertStyle);
            ValidatedSet.SetValue(ref def.minRequirement, MinRequirement, insertStyle);
            ValidatedSet.SetValue(ref def.resourceType, ResourceType);
            ValidatedSet.SetValue(ref def.affectionType, AffectionType);
            ValidatedSet.SetValue(ref def.negative, Negative);
            ValidatedSet.SetValue(ref def.oppositeGirl, OppositeGirl);
            ValidatedSet.SetValue(ref def.valueType, ValueType);
            ValidatedSet.SetValue(ref def.min, Min, insertStyle);
            ValidatedSet.SetValue(ref def.max, Max, insertStyle);
            ValidatedSet.SetValue(ref def.combineOperation, CombineOperation);
            ValidatedSet.SetValue(ref def.resourceMaxValue, ResourceMaxValue);
            ValidatedSet.SetValue(ref def.girlValueType, GirlValueType);
            ValidatedSet.SetValue(ref def.conditionType, ConditionType);
            ValidatedSet.SetValue(ref def.percentOfValue, PercentOfValue);
            ValidatedSet.SetValue(ref def.orCheck, OrCheck);
            ValidatedSet.SetValue(ref def.limit, Limit, insertStyle);
            ValidatedSet.SetValue(ref def.merged, Merged);
            ValidatedSet.SetValue(ref def.flatMerge, FlatMerge);
            ValidatedSet.SetValue(ref def.weighted, Weighted);
            ValidatedSet.SetValue(ref def.visualEffectType, VisualEffectType);
            ValidatedSet.SetValue(ref def.energyType, EnergyType);
            ValidatedSet.SetValue(ref def.splashText, SplashText, insertStyle);
            ValidatedSet.SetValue(ref def.ailmentItemType, AilmentItemType);
            ValidatedSet.SetValue(ref def.ailmentAlterType, AilmentAlterType);
            ValidatedSet.SetValue(ref def.ailmentTargetType, AilmentTargetType);
            ValidatedSet.SetValue(ref def.ailmentIndex, AilmentIndex);

            ValidatedSet.SetValue(ref def.energyDefinition, gameDataProvider.GetEnergy(EnergyDefinitionID), insertStyle);
            ValidatedSet.SetValue(ref def.ailmentDefinition, gameDataProvider.GetAilment(AilmentDefinitionID), insertStyle);

            ValidatedSet.SetValue(ref def.audioKlip, AudioKlipInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.tokenConditionSet, TokenConditionSetInfo, insertStyle, gameDataProvider, assetProvider);

            ValidatedSet.SetListValue(ref def.tokenDefinitions, TokenDefinitionIDs?.Select(x => gameDataProvider.GetToken(x)).ToList(), insertStyle);
            ValidatedSet.SetListValue(ref def.combineValues, CombineValues, insertStyle);
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewSource)
        {
            AilmentDefinitionID = getNewSource.Invoke(AilmentDefinitionID);
            EnergyDefinitionID = getNewSource.Invoke(EnergyDefinitionID);

            TokenConditionSetInfo?.ReplaceRelativeIds(getNewSource);

            TokenDefinitionIDs = TokenDefinitionIDs?.Select(x => getNewSource(x)).ToList();
        }
    }
}
