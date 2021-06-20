// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod.Utility;
using System;

namespace Hp2BaseMod.GameDataMods
{
    /// <summary>
    /// Serializable information to make a CodeDefinition
    /// </summary>
    [Serializable]
    public class CodeDataMod : DataMod<CodeDefinition>
    {
        public string CodeHash;
        public CodeType? CodeType;
        public bool? Disabled;
        public string OffMessage;
        public string OnMessage;

        public CodeDataMod() { }

        public CodeDataMod(int id,
                           string codeHash,
                           CodeType? codeType,
                           bool? disabled,
                           string offMessage,
                           string onMessage,
                           bool isAdditive = false)
            : base(id, isAdditive)
        {
            CodeType = codeType;
            OffMessage = offMessage;
            OnMessage = onMessage;
            CodeHash = codeHash;
            Disabled = disabled;
        }

        public CodeDataMod(CodeDefinition def)
            : base(def.id, false)
        {
            CodeType = def.codeType;
            OffMessage = def.offMessage;
            OnMessage = def.onMessage;
            CodeHash = def.codeHash;
            Disabled = def.disabled;
        }

        public override void SetData(CodeDefinition def, GameData gameData, AssetProvider assetProvider)
        {
            def.id = Id;

            Access.NullableSet(ref def.codeType, CodeType);
            Access.NullableSet(ref def.disabled, Disabled);

            if (IsAdditive)
            {
                Access.NullSet(ref def.offMessage, OffMessage);
                Access.NullSet(ref def.onMessage, OnMessage);
                Access.NullSet(ref def.codeHash, CodeHash);
            }
            else
            {
                def.offMessage = OffMessage;
                def.onMessage = OnMessage;
                def.codeHash = CodeHash;
            }
        }
    }
}
