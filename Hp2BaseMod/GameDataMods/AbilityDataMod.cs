// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.AssetInfos;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make an AbilityDefinition
    /// </summary>
    [UiSonClass]
    public class AbilityDataMod : DataMod<AbilityDefinition>
    {
        [UiSonSelectorUi(new string[] { "null", "True", "False"}, 0)]
        public bool? SelectableTarget;

        [UiSonMemberClass]
        public TokenConditionSetInfo TargetConditionSetInfo;

        [UiSonTextEditUi]
        public int? TargetMinimumCount;

        [UiSonCollection]
        [UiSonMemberClass]
        public List<AbilityStepInfo> Steps;

        public AbilityDataMod() { }

        public AbilityDataMod(int id, bool isAdditive)
            : base(id, isAdditive)
        {
        }

        public AbilityDataMod(int id,
                              bool? selectableTarget,
                              TokenConditionSetInfo targetConditionSetInfo,
                              int? targetMinimumCount,
                              List<AbilityStepInfo> steps,
                              bool isAdditive = false)
            : base(id, isAdditive)
        {
            SelectableTarget = selectableTarget;
            TargetConditionSetInfo = targetConditionSetInfo;
            TargetMinimumCount = targetMinimumCount;
            Steps = steps;
        }

        public AbilityDataMod(AbilityDefinition def, AssetProvider assetProvider)
            : base(def.id, false)
        {
            SelectableTarget = def.selectableTarget;
            TargetMinimumCount = def.targetMinimumCount;

            if (def.targetConditionSet != null) { TargetConditionSetInfo = new TokenConditionSetInfo(def.targetConditionSet); }
            Steps = def.steps?.Select(x => new AbilityStepInfo(x, assetProvider)).ToList();
        }

        public override void SetData(AbilityDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullableSet(ref def.selectableTarget, SelectableTarget);
            Access.NullableSet(ref def.targetMinimumCount, TargetMinimumCount);

            if (TargetConditionSetInfo != null) { def.targetConditionSet = TargetConditionSetInfo.ToTokenConditionSet(gameData); }

            SetAbilitySteps(ref def.steps, Steps, gameData, assetProvider);
        }
    }
}
