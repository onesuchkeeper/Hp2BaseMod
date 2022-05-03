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
    public class DialogTriggerLineSetInfo : IGameDataInfo<DialogTriggerLineSet>
    {
        [UiSonMemberElement]
        public List<DialogLineInfo> DialogLines;

        public DialogTriggerLineSetInfo() { }

        public DialogTriggerLineSetInfo(List<DialogLineInfo> dialogLines)
        {
            DialogLines = dialogLines;
        }

        public DialogTriggerLineSetInfo(DialogTriggerLineSet dialogTriggerLineSet, AssetProvider assetProvider)
        {
            if (dialogTriggerLineSet == null) { throw new ArgumentNullException(nameof(dialogTriggerLineSet)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            if (dialogTriggerLineSet.dialogLines != null) { DialogLines = dialogTriggerLineSet.dialogLines.Select(x => new DialogLineInfo(x, assetProvider)).ToList(); }
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref DialogTriggerLineSet def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a dialog trigger line");
            ModInterface.Instance.IncreaseLogIndent();

            if (def == null)
            {
                def = Activator.CreateInstance<DialogTriggerLineSet>();
            }

            ValidatedSet.SetListValue(ref def.dialogLines, DialogLines, insertStyle, gameDataProvider, assetProvider);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
