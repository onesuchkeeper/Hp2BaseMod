// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Interfaces;
using Hp2BaseMod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DataModEditor
{
    public partial class GirlEditor : IEditor
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public GameDataType type => GameDataType.Girl;

        public GirlVM Vm => _vm;
        private GirlVM _vm;

        private TabControl _controller;

        public GirlEditor(GirlVM vm, TabControl controller)
        {
            _vm = vm ?? throw new ArgumentNullException(nameof(vm));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));

            DataContext = _vm;
            InitializeComponent();

            //see if this is still needed
            _vm.PropertyChanged += (s,e) => { OnPropertyChanged(); };
        }

        #region enumerated properties add/remove

        private void AddBaggage(object sender, System.Windows.RoutedEventArgs e) => _vm.AddNewBaggage();

        private void RemoveBaggage(object sender, System.Windows.RoutedEventArgs e)
        {
            ((e.Source as Button).DataContext as BaggageVm).Remove();
        }

        private void AddUniqueItem(object sender, System.Windows.RoutedEventArgs e) => _vm.AddNewUniqueItem();

        private void RemoveUniqueItem(object sender, System.Windows.RoutedEventArgs e)
        {
            ((e.Source as Button).DataContext as UniqueItemVm).Remove();
        }

        private void AddShoe(object sender, System.Windows.RoutedEventArgs e) => _vm.AddNewShoe();

        private void RemoveShoe(object sender, System.Windows.RoutedEventArgs e)
        {
            ((e.Source as Button).DataContext as ShoeVm).Remove();
        }

        private void AddQuestion(object sender, System.Windows.RoutedEventArgs e) => _vm.AddNewQuestion();

        private void RemoveQuestion(object sender, System.Windows.RoutedEventArgs e)
        {
            ((e.Source as Button).DataContext as GirlQuestionVm).Remove();
        }

        #endregion

        public void Save(string path) => _vm.Save(path);

        private void Close(object sender, System.Windows.RoutedEventArgs e) => _controller.Items.Remove(this);

        // Two way binding doesn't work, but this does. No clue why
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            ((KeyValuePair<string, FavoriteVm>)comboBox.DataContext).Value.Index = comboBox.SelectedIndex;
        }
    }
}
