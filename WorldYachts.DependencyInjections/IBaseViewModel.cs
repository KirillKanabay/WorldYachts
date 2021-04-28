using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorldYachts.DependencyInjections
{
    public interface IBaseViewModel : INotifyPropertyChanged
    {
        void OnPropertyChanged([CallerMemberName] string propertyName = null);
        void SendSnackbar(string message);
    }
}