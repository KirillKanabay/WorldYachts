using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialDesignThemes.Wpf;
using WorldYachts.Annotations;
using WorldYachts.DependencyInjections;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.BaseViewModels
{
    public abstract class BaseViewModel: IBaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region DialogHost

        public virtual void CloseCurrentDialog()
        {
            MainWindow.GetMainWindow?.Invoke().DialogHost.CurrentSession.Close();
        }

        #endregion

        #region Snackbar
        public virtual void SendSnackbar(string message)
        {
            MainWindow.SendSnackbarAction?.Invoke(message);
        }

        #endregion
    }
}
