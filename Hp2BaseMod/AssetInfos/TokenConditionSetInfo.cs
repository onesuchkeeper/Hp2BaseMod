// Hp2BaseMod 2021, By OneSuchKeeper

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a TokenConditionSet
    /// Why did you make an entire class, just to hold one list. WHY JUAST USE THE LIST. DID UNITY MAKE YOU DO THIS? FUCK UNITY, I'LL STAB EM
    /// </summary>
    [Serializable]
    public class TokenConditionSetInfo
    {
        public List<TokenConditionInfo> Conditions;

        public TokenConditionSetInfo() { }

        public TokenConditionSetInfo(List<TokenConditionInfo> conditions)
        {
            Conditions = conditions;
        }

        public TokenConditionSetInfo(TokenConditionSet tokenConditionSet)
        {
            if (tokenConditionSet == null) { throw new ArgumentNullException(nameof(tokenConditionSet)); }

            if (tokenConditionSet.conditions != null) { Conditions = tokenConditionSet.conditions.Select(x => new TokenConditionInfo(x)).ToList();}
        }

        public TokenConditionSet ToTokenConditionSet(GameData gameData)
        {
            var newTCS = new TokenConditionSet();

            if (Conditions != null) { newTCS.conditions = Conditions.Select(x => x.ToTokenCondition(gameData)).ToList(); }

            return newTCS;
        }
    }
}
