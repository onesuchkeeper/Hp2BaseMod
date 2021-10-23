// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Data;
using DataModEditor.Interfaces;
using System.Collections.Generic;

namespace DataModEditor
{
    public class FavoriteVm : BaseVM
    {
        public int Index { get; set; } = 0;

        public IEnumerable<string> Answers => Default.FavoriteAnswers[_key];

        private int _key;

        public FavoriteVm(int key)
        {
            _key = key;
        }
    }

    public class FavoriteVmManager
    {
        public FavoriteVm this[int key]
        {
            get
            {
                if (!_favoriteVms.ContainsKey(key))
                {
                    _favoriteVms.Add(key, new FavoriteVm(key));
                }

                return _favoriteVms[key];
            }
            set
            {
                if (!_favoriteVms.ContainsKey(key))
                {
                    _favoriteVms.Add(key, new FavoriteVm(key));
                }

                _favoriteVms[key] = value;
            }
        }
        private Dictionary<int, FavoriteVm> _favoriteVms = new Dictionary<int, FavoriteVm>();
    }
}
