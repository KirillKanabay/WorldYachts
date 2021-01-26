using System;
using System.Collections.Generic;
using System.Text;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.CatalogManagementViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel
{
    class CatalogManagementViewModel:BaseViewModel
    {
        private DelegateCommand _openSampleMessageDialog;
        /// <summary>
        /// Открытие messageDialog
        /// </summary>
        public DelegateCommand OpenManagementDialog
        {
            get
            {
                return _openSampleMessageDialog ??= new DelegateCommand(ExecuteRunDialog);
            }
        }
        /// <summary>
        /// Запуск диалога сообщения
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunDialog(object o)
        {
            BaseViewModel bvm = null;
            switch (o.ToString())
            {
                case "AddAccessory":
                    break;
                case "AddBoat":
                    bvm = new AddBoatViewModel();
                    break;
                case "EditAccessory":
                    break;
                case "ExportExcel":
                    break;
                case "PartnersManagement":
                    break;
                case "RemoveAccessory":
                    break;
                case "RemoveBoat":
                    break;
                default:
                    throw new ArgumentException("Не существует такой команды!");
            }
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(bvm)
            };
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }
        /// <summary>
        /// При закрытии сообщения открываем главное окно при успешной регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            // if (_successfulRegistration)
            //     ChangeToMainWindow?.Execute(View);
        }
    }
}
