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
    /// Serializable information to make a CutsceneDialogOptionSubDefinition
    /// </summary>
    public class CutsceneDialogOptionInfo : IGameDataInfo<CutsceneDialogOptionSubDefinition>
    {
        [UiSonTextEditUi]
        public string DialogOptionText;

        [UiSonTextEditUi]
        public string YuriDialogOptionText;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? Yuri;

        [UiSonMemberElement]
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

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref CutsceneDialogOptionSubDefinition def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            if (def == null)
            {
                def = Activator.CreateInstance<CutsceneDialogOptionSubDefinition>();
            }

            ValidatedSet.SetValue(ref def.dialogOptionText, DialogOptionText, insertStyle);
            ValidatedSet.SetValue(ref def.yuri, Yuri);
            ValidatedSet.SetValue(ref def.yuriDialogOptionText, YuriDialogOptionText, insertStyle);
            ValidatedSet.SetListValue(ref def.steps, Steps, insertStyle, gameDataProvider, assetProvider);
        }
    }
}
