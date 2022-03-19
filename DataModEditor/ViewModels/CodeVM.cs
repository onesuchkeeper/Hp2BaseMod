// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Data;
using DataModEditor.Elements;
using DataModEditor.Interfaces;
using Hp2BaseMod.GameDataMods;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DataModEditor
{
    public class CodeVM : NPCBase, IModVM
    {
        public IEnumerable<string> CodeTypeOptions => Default.CodeTypeOptions;

        public int DataId => _codeId;

        public string Title => $"{ModName} {(_unsavedEdits ? "*" : "")}";

        public bool UnsavedEdits
        {
            get => _unsavedEdits;
            set
            {
                if (_unsavedEdits != value)
                {
                    _unsavedEdits = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        private bool _unsavedEdits = false;

        public string ModName
        {
            get => _modName;
            set
            {
                if (_modName != value)
                {
                    _modName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        private string _modName;

        public string CodeId
        {
            get => _codeId.ToString();
            set
            {
                if (int.TryParse(value, out var asInt)
                    && _codeId != asInt)
                {
                    _codeId = asInt;
                    OnPropertyChanged();
                }
            }
        }
        private int _codeId;

        public int CodeType { get; set; } = Default.CodeTypeOptions.Count - 1;

        public string CodeHash
        {
            get => _codeHash;
            set
            {
                if (value != "null")
                {
                    value = value.ToUpper();
                }

                if (value != _codeHash)
                {
                    _codeHash = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _codeHash = "null";

        public string OffMessage { get; set; } = "null";
        public string OnMessage { get; set; } = "null";

        /// <summary>
        /// populates from a data mod, should really be a factory huh? But I don't wanna
        /// </summary>
        /// <param name="codeDataMod"></param>
        public void Populate(CodeDataMod codeDataMod)
        {
            _modName = codeDataMod.ModName ?? "null";
            _codeId = codeDataMod.Id;
            CodeType = codeDataMod.CodeType.HasValue ? (int)codeDataMod.CodeType.Value : Default.CodeTypeOptions.Count - 1;
            OffMessage = codeDataMod.OffMessage ?? "null";
            OnMessage = codeDataMod.OnMessage ?? "null";
            CodeHash = codeDataMod.CodeHash ?? "null";
        }

        /// <summary>
        /// Saves a GirlDataMod to the given path
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            var newMod = new CodeDataMod(_codeId, false);

            newMod.ModName = ModName;

            if (CodeType != Default.CodeTypeOptions.Count - 1)
            {
                newMod.CodeType = (CodeType)CodeType;
            }

            if (OffMessage != "null")
            {
                newMod.OffMessage = OffMessage;
            }

            if (OnMessage != "null")
            {
                newMod.OnMessage = OnMessage;
            }

            if (CodeHash != "null")
            {
                newMod.CodeHash = CodeHash;
            }

            var file = File.CreateText(path);
            var jsonStr = JsonConvert.SerializeObject(newMod);
            file.Write(jsonStr);
            file.Flush();
            UnsavedEdits = false;
        }
    }
}
