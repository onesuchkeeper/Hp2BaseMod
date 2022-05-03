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
    /// Serializable information to make an LogicBundle
    /// </summary>
    public class LogicBundleInfo : IGameDataInfo<LogicBundle>
    {
        [UiSonMemberElement]
        public List<LogicConditionInfo> Conditions;

        [UiSonMemberElement]
        public List<LogicActionInfo> Actions;

        public LogicBundleInfo() { }

        public LogicBundleInfo(List<LogicConditionInfo> conditions,
                               List<LogicActionInfo> actions)
        {
            Conditions = conditions;
            Actions = actions;
        }

        public LogicBundleInfo(LogicBundle logicBundle, AssetProvider assetProvider)
        {
            if (logicBundle == null) { throw new ArgumentNullException(nameof(logicBundle)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            if (logicBundle.conditions != null) { Conditions = logicBundle.conditions.Select(x => new LogicConditionInfo(x)).ToList(); }
            if (logicBundle.actions != null) { Actions = logicBundle.actions.Select(x => new LogicActionInfo(x, assetProvider)).ToList(); }
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref LogicBundle def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a logic bundle");
            ModInterface.Instance.IncreaseLogIndent();

            if (def == null)
            {
                def = Activator.CreateInstance<LogicBundle>();
            }

            ValidatedSet.SetListValue(ref def.conditions, Conditions, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetListValue(ref def.actions, Actions, insertStyle, gameDataProvider, assetProvider);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
