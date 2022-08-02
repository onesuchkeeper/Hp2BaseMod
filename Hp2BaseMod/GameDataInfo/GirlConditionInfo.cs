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
    public class GirlConditionInfo : IGameDefinitionInfo<GirlCondition>
    {
        [UiSonSelectorUi(DefaultData.GirlConditionTypeNullable)]
        public GirlConditionType? Type;

        [UiSonElementSelectorUi(nameof(AilmentDataMod), 0, null, "id", DefaultData.DefaultAilmentNames_Name, DefaultData.DefaultAilmentIds_Name)]
        public RelativeId? AilmentDefinitionID;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? OtherGirl;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? Inverse;

        /// <summary>
        /// Constructor
        /// </summary>
        public GirlConditionInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public GirlConditionInfo(GirlCondition def)
        {
            if (def == null) { return; }

            Type = def.type;
            OtherGirl = def.otherGirl;
            Inverse = def.inverse;

            AilmentDefinitionID = new RelativeId(def.ailmentDefinition);
        }

        /// <inheritdoc/>
        public void SetData(ref GirlCondition def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<GirlCondition>();
            }

            ValidatedSet.SetValue(ref def.type, Type);
            ValidatedSet.SetValue(ref def.inverse, Inverse);

            ValidatedSet.SetValue(ref def.ailmentDefinition, gameDataProvider.GetAilment(AilmentDefinitionID), insertStyle);
        }
    }
}
