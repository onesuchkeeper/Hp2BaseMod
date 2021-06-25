// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.ModLoader;
using System;
using System.Collections.Generic;

namespace Hp2BaseMod.AssetInfos
{
    /// <summary>
    /// Serializable information to make a DialogueLine
    /// </summary>
    [Serializable]
    public class DialogLineInfo
    {
        public string DialogText;
        public string YuriDialogText;
        public bool Yuri;
        public AudioClipInfo YuriAudioClipInfo;
        public AudioClipInfo AudioClipInfo;
        public DialogLineExpression StartExpression;
        public DialogLineExpression EndExpression;
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

        public DialogLine ToDialogLine(AssetProvider assetProvider)
        {
            var newDL = new DialogLine();

            newDL.dialogText = DialogText;
            newDL.yuri = Yuri;
            newDL.yuriDialogText = YuriDialogText;
            newDL.startExpression = StartExpression;
            newDL.expressions = Expressions;
            newDL.endExpression = EndExpression;

            if (YuriAudioClipInfo != null) { newDL.yuriAudioClip = YuriAudioClipInfo.ToAudioClip(assetProvider); }
            if (AudioClipInfo != null) { newDL.audioClip = AudioClipInfo.ToAudioClip(assetProvider); }

            return newDL;
        }
    }
}
