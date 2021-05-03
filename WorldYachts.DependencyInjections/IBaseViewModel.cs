using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorldYachts.DependencyInjections
{
    public interface IBaseViewModel : INotifyPropertyChanged
    {
        public void OnPropertyChanged([CallerMemberName] string propertyName = null);
        public void SendSnackbar(string message);
    }
}