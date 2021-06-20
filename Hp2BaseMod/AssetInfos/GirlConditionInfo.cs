// Hp2BaseMod 2021, By OneSuchKeeper

using System;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a GirlCondition
    /// </summary>
    [Serializable]
    public class GirlConditionInfo
    {
        public GirlConditionType Type;
        public int AilmentDefinitionID;
        public bool OtherGirl;
        public bool Inverse;

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

        public GirlCondition ToGirlCondition(GameData gameData)
        {
            var newGC = new GirlCondition();

            newGC.type = Type;
            newGC.inverse = Inverse;

            newGC.ailmentDefinition = gameData.Ailments.Get(AilmentDefinitionID);

            return newGC;
        }
    }
}
