// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Elements;
using DataModEditor.Interfaces;
using DataModEditor.ModManagers;
using Hp2BaseMod.GameDataMods;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace DataModEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public IEnumerable<GirlVM> GirlMods => _girlModManager.Mods.ToList();
        public IEnumerable<CodeVM> CodeMods => _codeModManager.Mods.ToList();

        private Notifier _notifier;
        private GirlModManager _girlModManager;
        private CodeModManager _codeModManager;

        public MainWindow()
        {
            _notifier = new Notifier();
            _codeModManager = new CodeModManager(new List<CodeVM>(), _notifier);
            _codeModManager.PropertyChanged += (s, e) =>
                { OnPropertyChanged(nameof(CodeMods)); };
            _girlModManager = new GirlModManager(new List<GirlVM>(), _notifier, _codeModManager);
            _girlModManager.PropertyChanged += (s, e) =>
                { OnPropertyChanged(nameof(GirlMods)); };

            DataContext = this;
            InitializeComponent();
        }

        // Girl mods

        private void GirlsListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedVm = this.GirlsListBox.SelectedItem as GirlVM;

            foreach (var tab in this.TabControl.Items)
            {
                if (tab is GirlEditor girlEditor
                    && girlEditor.Vm == selectedVm)
                {
                    this.TabControl.SelectedItem = tab;
                    return;
                }
            }
            DisplayGirlEditor(selectedVm);
        }

        private void DisplayGirlEditor(GirlVM girlEditorVm)
        {
            if (girlEditorVm != null)
            {
                var girlEditor = new GirlEditor(girlEditorVm, this.TabControl);

                this.TabControl.Items.Add(girlEditor);

                this.TabControl.SelectedIndex = this.TabControl.Items.Count - 1;

                OnPropertyChanged(nameof(GirlMods));
            }
        }

        public void RemoveGirl(object sender, System.Windows.RoutedEventArgs e)
        {
            var modVM = (sender as Button).DataContext as GirlVM;

            CloseTab(modVM);

            _girlModManager.RemoveMod(modVM);
        }


        // Code mods

        private void CodesListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedVm = this.CodesListBox.SelectedItem as CodeVM;

            foreach (var tab in this.TabControl.Items)
            {
                if (tab is CodeEditor editor
                    && editor.Vm == selectedVm)
                {
                    this.TabControl.SelectedItem = tab;
                    return;
                }
            }
            DisplayCodeEditor(selectedVm);
        }

        private void DisplayCodeEditor(CodeVM editorVm)
        {
            if (editorVm != null)
            {
                var editor = new CodeEditor(editorVm, this.TabControl);

                this.TabControl.Items.Add(editor);

                this.TabControl.SelectedIndex = this.TabControl.Items.Count - 1;
            }
        }

        public void RemoveCode(object sender, System.Windows.RoutedEventArgs e)
        {
            var modVM = (sender as Button).DataContext as CodeVM;

            CloseTab(modVM);

            _codeModManager.RemoveMod(modVM);
        }

        private void CloseTab(NPCBase vm)
        {
            foreach (var tab in this.TabControl.Items)
            {
                if (tab is IEditor editor
                    && editor.Vm == vm)
                {
                    this.TabControl.Items.Remove(tab);
                    break;
                }
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var startinfo = new ProcessStartInfo(e.Uri.AbsoluteUri);
            startinfo.UseShellExecute = true;

            Process.Start(startinfo);
            e.Handled = true;
        }

        #region Menu items

        private void NewGirlMod(object sender, RoutedEventArgs e) => DisplayGirlEditor(_girlModManager.NewMod());

        private void OpenGirlMod(object sender, RoutedEventArgs e)
        {
            var openDlg = new OpenFileDialog();

            if (openDlg.ShowDialog() ?? false)
            {
                var openedMod = _girlModManager.OpenMod(openDlg.FileName);

                if (openedMod != null)
                {
                    DisplayGirlEditor(openedMod);
                }
            }
        }

        private void NewCodeMod(object sender, RoutedEventArgs e) => DisplayCodeEditor(_codeModManager.NewMod());

        private void OpenCodeMod(object sender, RoutedEventArgs e)
        {
            var openDlg = new OpenFileDialog();

            if (openDlg.ShowDialog() ?? false)
            {
                var openedMod = _codeModManager.OpenMod(openDlg.FileName);

                if (openedMod != null)
                {
                    DisplayCodeEditor(openedMod);
                }
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var saveDlg = new SaveFileDialog();

            if (this.TabControl.SelectedItem is IEditor editor)
            {
                if (saveDlg.ShowDialog() ?? false)
                {
                    editor.Save(saveDlg.FileName);
                }
            }
        }

        private void Hp2DataModLoaderLoad(object sender, RoutedEventArgs e)
        {
            var openDlg = new OpenFileDialog();
            openDlg.Title = "Select your HuniePop 2 - Double Date.exe";
            openDlg.Filter = "HuniePop 2 executable|*.exe";

            if (openDlg.ShowDialog() ?? false)
            {
                // check for data mod loader 
            }
            else
            {
                _notifier.Error("Unable to open Huniepop 2");
            }
        }

        #endregion

        #region dev

        public void DevGenerateBaggageDict(object sender, RoutedEventArgs e)
        {
            OnPropertyChanged(nameof(GirlMods));

            //var result = string.Empty;

            //var openDlg = new OpenFileDialog();
            //openDlg.Title = "First file";

            //if (openDlg.ShowDialog() ?? false)
            //{
            //    // check for data mod loader 
            //    var fileInfo = new FileInfo(openDlg.FileName);

            //    foreach (var path in Directory.GetFiles(fileInfo.Directory.FullName))
            //    {
            //        var configString = File.ReadAllText(path);
            //        var openedItemMod = JsonConvert.DeserializeObject(configString, typeof(ItemDataMod)) as ItemDataMod;
            //        if (openedItemMod != null)
            //        {
            //            if (openedItemMod.ItemType == ItemType.MISC)
            //            {
            //                result += $"{{{openedItemMod.Id}, \"{openedItemMod.ItemName}\"}},{Environment.NewLine}";
            //            }
            //        }
            //        else
            //        {
            //            _notifier.Error($"Invalid ItemMod file");
            //        }
            //    }
            //}
            //else
            //{
            //    _notifier.Error("Unable to open");
            //}

            //_notifier.Error(result);
        }

        #endregion
    }
}
