// Hp2BaseMod 2021, By OneSuchKeeper

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataModEditor.Interfaces
{
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
