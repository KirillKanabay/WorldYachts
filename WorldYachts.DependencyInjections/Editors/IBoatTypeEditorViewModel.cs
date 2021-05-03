using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WorldYachts.DependencyInjections.Editors
{
    public interface IBoatTypeEditorViewModel
    {
        string Type { get; set; }
        bool SaveButtonIsEnabled { get; }
        string Error { get; }
        Task SaveMethod(object parameter);
        void ExecuteRunDialog(object o);
        string this[string columnName] { get; }
        event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null);
        void CloseCurrentDialog();
        void SendSnackbar(string message);
    }
}