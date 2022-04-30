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
    /// Serializable information to make a sprite
    /// </summary>
    public class AbilityStepInfo : IGameDataInfo<AbilityStepSubDefinition>
    {
        [UiSonSelectorUi(DefaultData.AbilityStepTypeNullable_As_String, -100)]
        public AbilityStepType? StepType;

        [UiSonSelectorUi(DefaultData.AbilityStepValueTypeNullable_As_String)]
        public AbilityStepValueType? ValueType;

        [UiSonSelectorUi(DefaultData.AbilityStepConditionTypeNullable_As_String)]
        public AbilityStepConditionType? ConditionType;

        [UiSonSelectorUi(DefaultData.AbilityStepVisualEffectTypeNullable_As_String)]
        public AbilityStepVisualEffectType? VisualEffectType;

        [UiSonSelectorUi(DefaultData.AbilityStepVisualEffectEnergyTypeNullable_As_String)]
        public AbilityStepVisualEffectEnergyType? EnergyType;

        [UiSonSelectorUi(DefaultData.AbilityStepAilmentAlterTypeNullable_As_String)]
        public AbilityStepAilmentAlterType? AilmentAlterType;

        [UiSonSelectorUi(DefaultData.AbilityStepAilmentTargetTypeNullable_As_String)]
        public AbilityStepAilmentTargetType? AilmentTargetType;

        [UiSonSelectorUi(DefaultData.PuzzleResourceTypeNullable_As_String)]
        public PuzzleResourceType? ResourceType;

        [UiSonSelectorUi(DefaultData.PuzzleAffectionTypeNullable_As_String)]
        public PuzzleAffectionType? AffectionType;

        [UiSonSelectorUi(DefaultData.NumberCombineOperationNullable_As_String)]
        public NumberCombineOperation? CombineOperation;

        [UiSonSelectorUi(DefaultData.GirlValueTypeNullable_As_String)]
        public GirlValueType? GirlValueType;

        [UiSonSelectorUi(DefaultData.ItemTypeNullable_As_String)]
        public ItemType? AilmentItemType;

        [UiSonTextEditUi]
        public string Handle;

        [UiSonTextEditUi]
        public string ValueRef;

        [UiSonTextEditUi]
        public string PuzzleSetRef;

        [UiSonTextEditUi]
        public string MinRequirement;

        [UiSonTextEditUi]
        public string Min;

        [UiSonTextEditUi]
        public string Max;

        [UiSonTextEditUi]
        public string Limit;

        [UiSonTextEditUi]
        public string SplashText;

        [UiSonTextEditUi]
        public float? PercentOfValue;

        [UiSonTextEditUi]
        public int? AilmentIndex;

        [UiSonElementSelectorUi(nameof(AilmentDataMod), 0, null, "Id", DefaultData.DefaultAilmentNames, DefaultData.DefaultAilmentIds)]
        public int? AilmentDefinitionID;

        [UiSonElementSelectorUi(nameof(EnergyDataMod), 0, null, "Id", DefaultData.DefaultEnergyIds, DefaultData.DefaultEnergyIds)]
        public int? EnergyDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? Negative;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? OppositeGirl;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? ResourceMaxValue;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? Merged;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? FlatMerge;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? OrCheck;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? Weighted;

        [UiSonMemberElement]
        public TokenConditionSetInfo TokenConditionSetInfo;

        [UiSonMemberElement]
        public AudioKlipInfo AudioKlipInfo;

        [UiSonTextEditUi]
        public List<string> CombineValues;

        [UiSonElementSelectorUi(nameof(TokenDataMod), 0, null, "Id", DefaultData.DefaultTokenNames, DefaultData.DefaultTokenIds)]
        public List<int> TokenDefinitionIDs;

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public AbilityStepInfo() { }

        /// <summary>
        /// Constructor with all parameters
        /// </summary>
        public AbilityStepInfo(AbilityStepType stepType,
                               AbilityStepValueType valueType,
                               PuzzleResourceType resourceType,
                               PuzzleAffectionType affectionType,
                               AbilityStepConditionType conditionType,
                               AbilityStepVisualEffectType visualEffectType,
                               AbilityStepVisualEffectEnergyType energyType,
                               AbilityStepAilmentAlterType ailmentAlterType,
                               AbilityStepAilmentTargetType ailmentTargetType,
                               NumberCombineOperation combineOperation,
                               GirlValueType girlValueType,
                               ItemType ailmentItemType,
                               string handle,
                               string valueRef,
                               string puzzleSetRef,
                               string minRequirement,
                               string min,
                               string max,
                               string splashText,
                               string limit,
                               float percentOfValue,
                               int ailmentIndex,
                               int ailmentDefinitionID,
                               int energyDefinitionID,
                               bool negative,
                               bool oppositeGirl,
                               bool orCheck,
                               bool merged,
                               bool flatMerge,
                               bool resourceMaxValue,
                               bool weighted,
                               TokenConditionSetInfo tokenConditionSetInfo,
                               AudioKlipInfo audioKlipInfo,
                               List<int> tokenDefinitionIDs,
                               List<string> combineValues)
        {
            StepType = stepType;
            Handle = handle;
            ValueRef = valueRef;
            PuzzleSetRef = puzzleSetRef;
            MinRequirement = minRequirement;
            ResourceType = resourceType;
            AffectionType = affectionType;
            Negative = negative;
            OppositeGirl = oppositeGirl;
            ValueType = valueType;
            Min = min;
            Max = max;
            CombineValues = combineValues;
            CombineOperation = combineOperation;
            ResourceMaxValue = resourceMaxValue;
            GirlValueType = girlValueType;
            ConditionType = conditionType;
            PercentOfValue = percentOfValue;
            OrCheck = orCheck;
            TokenConditionSetInfo = tokenConditionSetInfo;
            Limit = limit;
            Merged = merged;
            FlatMerge = flatMerge;
            TokenDefinitionIDs = tokenDefinitionIDs;
            Weighted = weighted;
            AilmentDefinitionID = ailmentDefinitionID;
            VisualEffectType = visualEffectType;
            EnergyType = energyType;
            EnergyDefinitionID = energyDefinitionID;
            SplashText = splashText;
            AudioKlipInfo = audioKlipInfo;
            AilmentItemType = ailmentItemType;
            AilmentAlterType = ailmentAlterType;
            AilmentTargetType = ailmentTargetType;
            AilmentIndex = ailmentIndex;
        }

        /// <summary>
        /// Constructor from data definition
        /// </summary>
        /// <param name="def"></param>
        /// <param name="assetProvider"></param>
        /// <exception cref="ArgumentNullException"></exception>
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

            AilmentDefinitionID = def.ailmentDefinition?.id ?? -1;
            EnergyDefinitionID = def.energyDefinition?.id ?? -1;

            if (def.audioKlip != null) { AudioKlipInfo = new AudioKlipInfo(def.audioKlip, assetProvider); }
            if (def.tokenConditionSet != null) { TokenConditionSetInfo = new TokenConditionSetInfo(def.tokenConditionSet); }

            if (def.tokenDefinitions != null) { TokenDefinitionIDs = def.tokenDefinitions.Select(x => x.id).ToList(); }
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref AbilityStepSubDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
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

            ValidatedSet.SetListValue(ref def.tokenDefinitions, TokenDefinitionIDs?.Select(x => gameDataProvider.GetToken(x)), insertStyle);
            ValidatedSet.SetListValue(ref def.combineValues, CombineValues, insertStyle);
        }
    }
}
