using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using MaterialDesignThemes.Wpf;
using WorldYachts.Annotations;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.BaseViewModels
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Диалоговые окна
        protected async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "MessageDialogRoot");
        }

        private void ClosingEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
        }
        #endregion

        #region Snackbar

        protected virtual void SendSnackbar(string message)
        {
            MainWindow.SendSnackbarAction?.Invoke(message);
        }

        #endregion
    }
}
