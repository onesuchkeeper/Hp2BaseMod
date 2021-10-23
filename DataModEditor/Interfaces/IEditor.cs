// Hp2BaseMod 2021, By OneSuchKeeper

using Hp2BaseMod;
using System.ComponentModel;

namespace DataModEditor.Interfaces
{
    public interface IEditor : INotifyPropertyChanged
    {
        GameDataType type { get; }
        void Save(string path);
    }
}
