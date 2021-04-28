using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialDesignThemes.Wpf;
using WorldYachts.Annotations;
using WorldYachts.DependencyInjections;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.BaseViewModels
{
    public class BaseViewModel: IBaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Диалоговые окна
        //public async void ExecuteRunDialog(object o)
        //{
        //    var view = new SampleMessageDialog()
        //    {
        //        DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
        //    };
        //    var result = await DialogHost.Show(view, "DialogRoot", ClosingEventHandler);
        //}

        //public virtual async void ClosingEventHandler(object sender, DialogOpenedEventArgs eventargs)
        //{
        //}
        #endregion

        #region Snackbar
        public virtual void SendSnackbar(string message)
        {
            MainWindow.SendSnackbarAction?.Invoke(message);
        }

        #endregion
    }
}
