// Hp2BaseMod 2021, By OneSuchKeeper

using System.ComponentModel;

namespace DataModEditor.Interfaces
{
    public interface IModVM : INotifyPropertyChanged
    {
        string Title { get; }
        string ModName { get; }
        int DataId { get; }
    }
}
