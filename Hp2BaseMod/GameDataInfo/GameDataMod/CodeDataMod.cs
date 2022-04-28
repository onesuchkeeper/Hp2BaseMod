// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.GameDataInfo.Interface;
using Hp2BaseMod.ModLoader;
using Hp2BaseMod.Utility;
using System;
using UiSon.Attribute;

namespace Hp2BaseMod.GameDataInfo
{
    /// <summary>
    /// Serializable information to make a CodeDefinition
    /// </summary>
    [UiSonElement]
    public class CodeDataMod : DataMod, IGameDataMod<CodeDefinition>
    {
        [UiSonTextEditUi]
        public string CodeHash;

        [UiSonSelectorUi(DefaultData.CodeTypeNullable_As_String)]
        public CodeType? CodeType;

        [UiSonSelectorUi(DefaultData.NullableBoolOptions)]
        public bool? Disabled;

        [UiSonTextEditUi]
        public string OffMessage;

        [UiSonTextEditUi]
        public string OnMessage;

        public CodeDataMod() { }

        public CodeDataMod(int id, InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
        }

        public CodeDataMod(int id,
                           string codeHash,
                           CodeType? codeType,
                           bool? disabled,
                           string offMessage,
                           string onMessage,
                           InsertStyle insertStyle = InsertStyle.replace)
            : base(id, insertStyle)
        {
            CodeType = codeType;
            OffMessage = offMessage;
            OnMessage = onMessage;
            CodeHash = codeHash;
            Disabled = disabled;
        }

        public CodeDataMod(CodeDefinition def)
            : base(def.id, InsertStyle.replace, def.name)
        {
            CodeType = def.codeType;
            OffMessage = def.offMessage;
            OnMessage = def.onMessage;
            CodeHash = def.codeHash;
            Disabled = def.disabled;
        }

        public void SetData(CodeDefinition def, GameDataProvider _, AssetProvider __, InsertStyle insertStyle)
        {
            def.id = Id;

            ValidatedSet.SetValue(ref def.codeType, CodeType);
            ValidatedSet.SetValue(ref def.disabled, Disabled);

            ValidatedSet.SetValue(ref def.offMessage, OffMessage, InsertStyle);
            ValidatedSet.SetValue(ref def.onMessage, OnMessage, InsertStyle);
            ValidatedSet.SetValue(ref def.codeHash, CodeHash, InsertStyle);
        }
    }
}
