// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make an AbilityDefinition
    /// </summary>
    [UiSonElement]
    [UiSonGroup("Target", 1)]
    public class AbilityDataMod : DataMod, IGameDataMod<AbilityDefinition>
    {
        [UiSonCheckboxUi(0, "Target")]
        public bool? SelectableTarget;

        [UiSonTextEditUi(0, "Target")]
        public int? TargetMinimumCount;

        [UiSonMemberElement(0, "Target")]
        public TokenConditionSetInfo TargetConditionSetInfo;

        [UiSonMemberElement]
        public List<AbilityStepInfo> Steps;

        public AbilityDataMod() { }

        public AbilityDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
        }

        public AbilityDataMod(int id,
                              bool? selectableTarget,
                              TokenConditionSetInfo targetConditionSetInfo,
                              int? targetMinimumCount,
                              List<AbilityStepInfo> steps,
                              InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
            SelectableTarget = selectableTarget;
            TargetConditionSetInfo = targetConditionSetInfo;
            TargetMinimumCount = targetMinimumCount;
            Steps = steps;
        }

        public AbilityDataMod(AbilityDefinition def, AssetProvider assetProvider)
            : base(def.id, InsertStyle.replace, def.name)
        {
            SelectableTarget = def.selectableTarget;
            TargetMinimumCount = def.targetMinimumCount;

            if (def.targetConditionSet != null) { TargetConditionSetInfo = new TokenConditionSetInfo(def.targetConditionSet); }
            Steps = def.steps?.Select(x => new AbilityStepInfo(x, assetProvider)).ToList();
        }

        public void SetData(AbilityDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            def.id = Id;

            ValidatedSet.SetValue(ref def.selectableTarget, SelectableTarget);
            ValidatedSet.SetValue(ref def.targetMinimumCount, TargetMinimumCount);

            ValidatedSet.SetValue(ref def.targetConditionSet, TargetConditionSetInfo, InsertStyle, gameDataProvider, assetProvider);

            ValidatedSet.SetListValue(ref def.steps, Steps, InsertStyle, gameDataProvider, assetProvider);
        }
    }
}
