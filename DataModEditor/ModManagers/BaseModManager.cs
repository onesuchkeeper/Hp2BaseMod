// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataModEditor.ModManagers
{
    public abstract class BaseModManager<t> : INotifyPropertyChanged
        where t : IModVM
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int Count => AvailableData.Count();

        protected abstract IEnumerable<KeyValuePair<int, string>> AvailableData { get; }

        public IEnumerable<string> Options => AvailableData.Select(x => x.Value);

        public int OptionIndexToId(int index) => AvailableData.ElementAt(index).Key;

        public int IdToOptionIndex(int id)
        {
            int index = 0;

            foreach (var data in AvailableData)
            {
                if (data.Key == id)
                {
                    return index;
                }
                index++;
            }

            return -1;
        }

        protected string UniqueName(string name)
        {
            if (!_mods.Any(x => x.ModName == name)) { return name; }

            var i = 1;
            while (Mods.Any(x => x.ModName == $"{name} - {i}")) { i++; }

            return $"{name} - {i}";
        }

        public List<t> Mods => _mods;
        private List<t> _mods;

        protected Notifier _notifier;

        public BaseModManager(List<t> mods, Notifier notifier)
        {
            _mods = mods ?? throw new ArgumentNullException(nameof(mods));
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        public void AddMod(t newMod)
        {
            _mods.Add(newMod);
            OnPropertyChanged(nameof(Options));
            newMod.PropertyChanged += (s, e) => { OnPropertyChanged(nameof(Options)); };
        }

        public void RemoveMod(t mod)
        {
            //mod should be cleaned up, no need to unsub
            Mods.Remove(mod);
            OnPropertyChanged(nameof(Options));
        }
    }
}
