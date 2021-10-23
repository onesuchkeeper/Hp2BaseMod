// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Data;
using DataModEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataModEditor
{
    public class ShoeVm : BaseVM
    {
        public int Index
        {
            get => _index;
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _index = 0;

        public IEnumerable<string> Options => Default.ItemsShoes.Select(x => x.Value);

        private ObservableCollection<ShoeVm> _parent;

        public ShoeVm(ObservableCollection<ShoeVm> parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public void Remove()
        {
            _parent.Remove(this);
        }
    }
}
