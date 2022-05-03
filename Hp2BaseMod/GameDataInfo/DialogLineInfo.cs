// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using System.Collections.Generic;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a DialogueLine
    /// </summary>
    public class DialogLineInfo : IGameDataInfo<DialogLine>
    {
        [UiSonTextEditUi]
        public string DialogText;

        [UiSonTextEditUi]
        public string YuriDialogText;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions_Name)]
        public bool? Yuri;

        [UiSonMemberElement]
        public AudioClipInfo YuriAudioClipInfo;

        [UiSonMemberElement]
        public AudioClipInfo AudioClipInfo;

        [UiSonMemberElement]
        public DialogLineExpression StartExpression;

        [UiSonMemberElement]
        public DialogLineExpression EndExpression;

        [UiSonMemberElement]
        public List<DialogLineExpression> Expressions;

        public DialogLineInfo() { }

        public DialogLineInfo(string dialogText,
                              string yuriDialogText,
                              bool yuri,
                              AudioClipInfo yuriAudioClipInfo,
                              AudioClipInfo audioClipInfo,
                              DialogLineExpression startExpression,
                              DialogLineExpression endExpression,
                              List<DialogLineExpression> expressions)
        {
            DialogText = dialogText;
            AudioClipInfo = audioClipInfo;
            Yuri = yuri;
            YuriDialogText = yuriDialogText;
            YuriAudioClipInfo = yuriAudioClipInfo;
            StartExpression = startExpression;
            Expressions = expressions;
            EndExpression = endExpression;
        }

        public DialogLineInfo(DialogLine dialogLine, AssetProvider assetProvider)
        {
            if (dialogLine == null) { throw new ArgumentNullException(nameof(dialogLine)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            DialogText = dialogLine.dialogText;
            Yuri = dialogLine.yuri;
            YuriDialogText = dialogLine.yuriDialogText;
            StartExpression = dialogLine.startExpression;
            Expressions = dialogLine.expressions;
            EndExpression = dialogLine.endExpression;

            if (dialogLine.yuriAudioClip != null) { YuriAudioClipInfo = new AudioClipInfo(dialogLine.yuriAudioClip, assetProvider); }
            if (dialogLine.audioClip != null) { AudioClipInfo = new AudioClipInfo(dialogLine.audioClip, assetProvider); }
        }

        /// <summary>
        /// Writes to the game data definition this represents
        /// </summary>
        /// <param name="def">The target game data definition to write to.</param>
        /// <param name="gameData">The game data.</param>
        /// <param name="assetProvider">The asset provider.</param>
        /// <param name="insertStyle">The insert style.</param>
        public void SetData(ref DialogLine def, GameDataProvider gameDataProvider, AssetProvider assetProvider, InsertStyle insertStyle)
        {
            ModInterface.Instance.LogLine("Setting data for a dialog line");
            ModInterface.Instance.IncreaseLogIndent();

            if (def == null)
            {
                def = Activator.CreateInstance<DialogLine>();
            }

            ValidatedSet.SetValue(ref def.dialogText, DialogText, insertStyle);
            ValidatedSet.SetValue(ref def.yuri, Yuri);
            ValidatedSet.SetValue(ref def.yuriDialogText, YuriDialogText, insertStyle);
            ValidatedSet.SetValue(ref def.startExpression, StartExpression, insertStyle);
            ValidatedSet.SetValue(ref def.expressions, Expressions, insertStyle);
            ValidatedSet.SetValue(ref def.endExpression, EndExpression, insertStyle);

            ValidatedSet.SetValue(ref def.yuriAudioClip, YuriAudioClipInfo, insertStyle, gameDataProvider, assetProvider);
            ValidatedSet.SetValue(ref def.audioClip, AudioClipInfo, insertStyle, gameDataProvider, assetProvider);

            ModInterface.Instance.LogLine("done");
            ModInterface.Instance.DecreaseLogIndent();
        }
    }
}
