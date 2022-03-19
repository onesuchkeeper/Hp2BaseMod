// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Elements;
using DataModEditor.Interfaces;
using Hp2BaseMod;
using Hp2BaseMod.Utility;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DataModEditor
{
    public partial class CodeEditor : IEditor
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public GameDataType type => GameDataType.Code;

        public NPCBase Vm => _vm;
        private CodeVM _vm;

        private TabControl _controller;

        public CodeEditor(CodeVM vm, TabControl controller)
        {
            _vm = vm ?? throw new ArgumentNullException(nameof(vm));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));

            DataContext = _vm;
            InitializeComponent();
        }

        public void Save(string path) => _vm.Save(path);

        private void Close(object sender, System.Windows.RoutedEventArgs e) => _controller.Items.Remove(this);

        private void ConvertButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _vm.CodeHash = MD5.Encrypt(_vm.CodeHash);
        }
    }
}
