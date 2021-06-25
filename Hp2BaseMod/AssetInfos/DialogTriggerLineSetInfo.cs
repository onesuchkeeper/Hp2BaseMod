// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Why did you make an entire class, just to hold one list. WHY JUAST USE THE LIST. DID UNITY MAKE YOU DO THIS? FUCK UNITY, I'LL STAB EM
    /// yesh
    /// </summary>
    [Serializable]
    public class DialogTriggerLineSetInfo
    {
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

        public DialogTriggerLineSet ToDialogTriggerLineSet(GameData gameData, AssetProvider assetProvider)
        {
            var newDTLS = new DialogTriggerLineSet();

            if (DialogLines != null) { newDTLS.dialogLines = DialogLines.Select(x => x.ToDialogLine(assetProvider)).ToList(); }

            return newDTLS;
        }
    }
}
