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
    /// Serializable information to make an LogicBundle
    /// </summary>
    public class LogicBundleInfo : IGameDefinitionInfo<LogicBundle>
    {
        [UiSonEncapsulatingUi]
        public List<LogicConditionInfo> Conditions;

        [UiSonEncapsulatingUi]
        public List<LogicActionInfo> Actions;

        /// <summary>
        /// Constructor
        /// </summary>
        public LogicBundleInfo() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        public LogicBundleInfo(LogicBundle def, AssetProvider assetProvider)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            if (def.conditions != null) { Conditions = def.conditions.Select(x => new LogicConditionInfo(x)).ToList(); }
            if (def.actions != null) { Actions = def.actions.Select(x => new LogicActionInfo(x, assetProvider)).ToList(); }
        }

        /// <inheritdoc/>
        public void SetData(ref LogicBundle def, GameDefinitionProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<LogicBundle>();
            }

            ValidatedSet.SetListValue(ref def.conditions, Conditions, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.actions, Actions, insertStyle, gameDataProvider, assetProvider);
        }

        public void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            foreach (var condition in Conditions.OrEmptyIfNull())
            {
                condition?.ReplaceRelativeIds(getNewId);
            }

            foreach (var action in Actions.OrEmptyIfNull())
            {
                action?.ReplaceRelativeIds(getNewId);
            }
        }
    }
}
