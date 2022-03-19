// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.Linq;
using UiSon.Attribute;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a TokenConditionSet
    /// </summary>
    public class TokenConditionSetInfo
    {
        [UiSonCollection]
        [UiSonMemberClass]
        public List<TokenConditionInfo> Conditions;

        public TokenConditionSetInfo() { }

        public TokenConditionSetInfo(List<TokenConditionInfo> conditions)
        {
            Conditions = conditions;
        }

        public TokenConditionSetInfo(TokenConditionSet tokenConditionSet)
        {
            if (tokenConditionSet == null) { throw new ArgumentNullException(nameof(tokenConditionSet)); }

            if (tokenConditionSet.conditions != null) { Conditions = tokenConditionSet.conditions.Select(x => new TokenConditionInfo(x)).ToList(); }
        }

        public TokenConditionSet ToTokenConditionSet(GameData gameData)
        {
            var newTCS = new TokenConditionSet();

            if (Conditions != null) { newTCS.conditions = Conditions.Select(x => x.ToTokenCondition(gameData)).ToList(); }

            return newTCS;
        }
    }
}
