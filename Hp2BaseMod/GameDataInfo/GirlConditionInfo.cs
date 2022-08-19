// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a GirlCondition
    /// </summary>
    public class GirlConditionInfo : IGameDefinitionInfo<GirlCondition>
    {
        public GirlConditionType? Type;

        public RelativeId? AilmentDefinitionID;

        public bool? OtherGirl;

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

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            AilmentDefinitionID = getNewId(AilmentDefinitionID);
        }
    }
}
