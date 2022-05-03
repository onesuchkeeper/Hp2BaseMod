// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a GirlCondition
    /// </summary>
    public class GirlConditionInfo : IGameDataInfo<GirlCondition>
    {
        [UiSonSelectorUi(DefaultData.GirlConditionTypeNullable_As_String)]
        public GirlConditionType? Type;

        [UiSonElementSelectorUi(nameof(AilmentDataMod), 0, null, "Id", DefaultData.DefaultAilmentNames_Name, DefaultData.DefaultAilmentIds_Name)]
        public int? AilmentDefinitionID;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? OtherGirl;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? Inverse;

        public GirlConditionInfo() { }

        public GirlConditionInfo(GirlConditionType type,
                                 int ailmentDefinitionID,
                                 bool otherGirl,
                                 bool inverse)
        {
            Type = type;
            AilmentDefinitionID = ailmentDefinitionID;
            OtherGirl = otherGirl;
            Inverse = inverse;
        }

        public GirlConditionInfo(GirlCondition girlCondition)
        {
            if (girlCondition == null) { return; }

            Type = girlCondition.type;
            OtherGirl = girlCondition.otherGirl;
            Inverse = girlCondition.inverse;

            AilmentDefinitionID = girlCondition.ailmentDefinition?.id ?? -1;
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref GirlCondition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a girl condition");
            ModInterface.Instance.IncreaseLogIndent();

            if (def == null)
            {
                def = Activator.CreateInstance<GirlCondition>();
            }

            ValidatedSet.SetValue(ref def.type, Type);
            ValidatedSet.SetValue(ref def.inverse, Inverse);

            ValidatedSet.SetValue(ref def.ailmentDefinition, gameDataProvider.GetAilment(AilmentDefinitionID), insertStyle);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
