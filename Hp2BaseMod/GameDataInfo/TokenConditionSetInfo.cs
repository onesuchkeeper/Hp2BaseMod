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
    /// Serializable information to make a TokenConditionSet
    /// </summary>
    public class TokenConditionSetInfo : IGameDataInfo<TokenConditionSet>
    {
        [UiSonMemberElement]
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

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref TokenConditionSet def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<TokenConditionSet>();
            }

            ValidatedSet.SetListValue(ref def.conditions, Conditions, insertStyle, gameDataProvider, assetProvider);
        }
    }
}
