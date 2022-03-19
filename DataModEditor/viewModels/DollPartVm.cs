// Hp2BaseMod 2021, By OneSuchKeeper

using DataModEditor.Elements;
using Hp2BaseMod.AssetInfos;
using System;

namespace DataModEditor
{
    public class DollPartVm : NPCBase
    {
        public string XOffset
        {
            get => _x.ToString();
            set
            {
                if (double.TryParse(value, out var asDouble)
                    && _x != asDouble)
                {
                    _x = asDouble;
                    OnPropertyChanged();
                }
            }
        }
        private double _x = 0;

        public string YOffset
        {
            get => _y.ToString();
            set
            {
                if (double.TryParse(value, out var asDouble)
                    && _y != asDouble)
                {
                    _y = asDouble;
                    OnPropertyChanged();
                }
            }
        }
        private double _y = 0;

        public string Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _path = "null";

        public string Name => _name;
        private string _name;

        public DollPartVm(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Populate(GirlPartInfo info = null)
        {
            if (info == null)
            {
                _path = "null";
                _x = 0;
                _y = 0;
            }
            else
            {
                _path = info.SpriteInfo?.IsExternal ?? false
                       ? info.SpriteInfo.Path
                       : "null";
                _x = info.X;
                _y = info.Y;
            }

            OnPropertyChanged(nameof(Path));
            OnPropertyChanged(nameof(XOffset));
            OnPropertyChanged(nameof(YOffset));
        }
    }
}
