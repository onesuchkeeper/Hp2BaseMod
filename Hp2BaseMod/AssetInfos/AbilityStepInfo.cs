// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a sprite
    /// </summary>
    public class AbilityStepInfo
    {
        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public AbilityStepType StepType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public AbilityStepValueType ValueType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public AbilityStepConditionType ConditionType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public AbilityStepVisualEffectType VisualEffectType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public AbilityStepVisualEffectEnergyType EnergyType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public AbilityStepAilmentAlterType AilmentAlterType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public AbilityStepAilmentTargetType AilmentTargetType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public PuzzleResourceType ResourceType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public PuzzleAffectionType AffectionType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public NumberCombineOperation CombineOperation;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public GirlValueType GirlValueType;

        [UiSonEnumSelectorUi(typeof(AbilityStepType))]
        public ItemType AilmentItemType;

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
        public float PercentOfValue;

        [UiSonTextEditUi]
        public int AilmentIndex;

        [UiSonTextEditUi]
        public int AilmentDefinitionID;

        [UiSonTextEditUi]
        public int EnergyDefinitionID;

        [UiSonCheckboxUi]
        public bool Negative;

        [UiSonCheckboxUi]
        public bool OppositeGirl;

        [UiSonCheckboxUi]
        public bool ResourceMaxValue;

        [UiSonCheckboxUi]
        public bool Merged;

        [UiSonCheckboxUi]
        public bool FlatMerge;

        [UiSonCheckboxUi]
        public bool OrCheck;

        [UiSonCheckboxUi]
        public bool Weighted;

        [UiSonMemberClass]
        public TokenConditionSetInfo TokenConditionSetInfo;

        [UiSonMemberClass]
        public AudioKlipInfo AudioKlipInfo;

        [UiSonCollection]
        [UiSonTextEditUi]
        public List<string> CombineValues;

        [UiSonCollection]
        [UiSonTextEditUi]
        public List<int> TokenDefinitionIDs;

        public AbilityStepInfo() { }

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

        public AbilityStepSubDefinition ToAbilityStep(GameData gameData, AssetProvider assetProvider)
        {
            if (gameData == null) { throw new ArgumentNullException(nameof(gameData)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            var newDef = new AbilityStepSubDefinition();

            newDef.stepType = StepType;
            newDef.handle = Handle;
            newDef.valueRef = ValueRef;
            newDef.puzzleSetRef = PuzzleSetRef;
            newDef.minRequirement = MinRequirement;
            newDef.resourceType = ResourceType;
            newDef.affectionType = AffectionType;
            newDef.negative = Negative;
            newDef.oppositeGirl = OppositeGirl;
            newDef.valueType = ValueType;
            newDef.min = Min;
            newDef.max = Max;
            newDef.combineValues = CombineValues;
            newDef.combineOperation = CombineOperation;
            newDef.resourceMaxValue = ResourceMaxValue;
            newDef.girlValueType = GirlValueType;
            newDef.conditionType = ConditionType;
            newDef.percentOfValue = PercentOfValue;
            newDef.orCheck = OrCheck;
            newDef.limit = Limit;
            newDef.merged = Merged;
            newDef.flatMerge = FlatMerge;
            newDef.weighted = Weighted;
            newDef.visualEffectType = VisualEffectType;
            newDef.energyType = EnergyType;
            newDef.splashText = SplashText;
            newDef.ailmentItemType = AilmentItemType;
            newDef.ailmentAlterType = AilmentAlterType;
            newDef.ailmentTargetType = AilmentTargetType;
            newDef.ailmentIndex = AilmentIndex;

            newDef.energyDefinition = gameData.Energy.Get(EnergyDefinitionID);
            newDef.ailmentDefinition = gameData.Ailments.Get(AilmentDefinitionID);
            newDef.energyDefinition = gameData.Energy.Get(EnergyDefinitionID);

            if (AudioKlipInfo != null) { newDef.audioKlip = AudioKlipInfo.ToAudioKlip(assetProvider); }
            if (TokenConditionSetInfo != null) { newDef.tokenConditionSet = TokenConditionSetInfo.ToTokenConditionSet(gameData); }

            if (TokenDefinitionIDs != null) { newDef.tokenDefinitions = TokenDefinitionIDs.Select(x => gameData.Tokens.Get(x)).ToList(); }

            return newDef;
        }
    }
}
