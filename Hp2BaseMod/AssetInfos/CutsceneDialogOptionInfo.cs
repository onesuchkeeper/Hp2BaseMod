// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a CutsceneDialogOptionSubDefinition
    /// </summary>
    [Serializable]
    public class CutsceneDialogOptionInfo
    {
        public string DialogOptionText;
        public string YuriDialogOptionText;
        public bool Yuri;
        public List<CutsceneStepInfo> Steps;

        public CutsceneDialogOptionInfo() { }

        public CutsceneDialogOptionInfo(string dialogOptionText,
                                        string yuriDialogOptionText,
                                        bool yuri,
                                        List<CutsceneStepInfo> steps)
        {
            DialogOptionText = dialogOptionText;
            Yuri = yuri;
            YuriDialogOptionText = yuriDialogOptionText;
            Steps = steps;
        }

        public CutsceneDialogOptionInfo(CutsceneDialogOptionSubDefinition def, AssetProvider assetProvider)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            DialogOptionText = def.dialogOptionText;
            Yuri = def.yuri;
            YuriDialogOptionText = def.yuriDialogOptionText;

            if (def.steps != null) { Steps = def.steps.Select(x => new CutsceneStepInfo(x, assetProvider)).ToList(); }
        }

        public CutsceneDialogOptionSubDefinition ToCutsceneDialogOption(GameData gameData, AssetProvider assetProvider)
        {
            var newCDOSD = new CutsceneDialogOptionSubDefinition();
            newCDOSD.dialogOptionText = DialogOptionText;
            newCDOSD.yuri = Yuri;
            newCDOSD.yuriDialogOptionText = YuriDialogOptionText;
            newCDOSD.steps = Steps.Select(x => x.ToCutsceneStep(gameData, assetProvider)).ToList();

            return newCDOSD;
        }
    }
}
