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
    public class DialogLineDataMod : DataMod, IGirlSubDataMod<DialogLine>
    {
        [UiSonTextEditUi]
        public string DialogText;

        [UiSonTextEditUi]
        public string YuriDialogText;

        [UiSonSelectorUi("NullableBoolNames", 0, null, "NullableBoolIds")]
        public bool? Yuri;

        [UiSonEncapsulatingUi]
        public AudioClipInfo YuriAudioClipInfo;

        [UiSonEncapsulatingUi]
        public AudioClipInfo AudioClipInfo;

        [UiSonEncapsulatingUi]
        public DialogLineExpression StartExpression;

        [UiSonEncapsulatingUi]
        public DialogLineExpression EndExpression;

        [UiSonEncapsulatingUi]
        public List<DialogLineExpression> Expressions;

        /// <summary>
        /// Constructor
        /// </summary>
        public DialogLineDataMod() { }

        /// <summary>
        /// Constructor from a definition instance.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <param name="assetProvider">Asset provider containing the assest referenced by the definition.</param>
        public DialogLineDataMod(DialogLine def, AssetProvider assetProvider, RelativeId id)
        {
            if (def == null) { throw new ArgumentNullException(nameof(def)); }
            if (assetProvider == null) { throw new ArgumentNullException(nameof(assetProvider)); }

            DialogText = def.dialogText;
            Yuri = def.yuri;
            YuriDialogText = def.yuriDialogText;
            StartExpression = def.startExpression;
            Expressions = def.expressions;
            EndExpression = def.endExpression;
            Id = id;

            if (def.yuriAudioClip != null) { YuriAudioClipInfo = new AudioClipInfo(def.yuriAudioClip, assetProvider); }
            if (def.audioClip != null) { AudioClipInfo = new AudioClipInfo(def.audioClip, assetProvider); }
        }

        /// <inheritdoc/>
        public void SetData(ref DialogLine def, GameDefinitionProvider gameData, AssetProvider assetProvider, InsertStyle insertStyle, RelativeId girlId)
        {
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

            ValidatedSet.SetValue(ref def.yuriAudioClip, YuriAudioClipInfo, insertStyle, gameData, assetProvider);
            ValidatedSet.SetValue(ref def.audioClip, AudioClipInfo, insertStyle, gameData, assetProvider);
        }

        /// <inheritdoc/>
        public override void ReplaceRelativeIds(Func<RelativeId?, RelativeId?> getNewId)
        {
            Id = getNewId(Id) ?? Id;
        }
    }
}
