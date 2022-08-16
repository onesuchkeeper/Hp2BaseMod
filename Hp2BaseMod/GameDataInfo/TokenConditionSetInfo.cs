// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.Extension.IEnumerableExtension;
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
    public class TokenConditionSetInfo : IGameDefinitionInfo<TokenConditionSet>
    {
        [UiSonEncapsulatingUi]
        public List<TokenConditionInfo> Conditions;

        /// <summary>
        /// Constructor
        /// </summary>
        public TokenConditionSetInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        public TokenConditionSetInfo(TokenConditionSet def)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }

            if (def.conditions != null) { Conditions = def.conditions.Select(x => new TokenConditionInfo(x)).ToList(); }
        }

        /// <inheritdoc/>
        public void SetData(ref TokenConditionSet def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<TokenConditionSet>();
            }

            ValidatedSet.SetListValue(ref def.conditions, Conditions, insertStyle, gameDataProvider, assetProvider);
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewSource)
        {
            foreach (var condition in Conditions.OrEmptyIfNull())
            {
                condition?.ReplaceRelativeIds(getNewSource);
            }
        }
    }
}
