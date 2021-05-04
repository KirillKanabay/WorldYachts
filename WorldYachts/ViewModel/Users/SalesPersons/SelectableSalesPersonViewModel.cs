using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.Editors;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;
using WorldYachts.ViewModel.Partner;

namespace WorldYachts.ViewModel.Users.SalesPersons
{
    class SelectableSalesPersonViewModel : BaseViewModel
    {
        #region Поля

        private AsyncRelayCommand _removeCommand;
        private AsyncRelayCommand _editCommand;

        private readonly ISalesPersonModel _salesPersonModel;
        private readonly IViewModelContainer _viewModelContainer;

        #endregion

        #region Конструкторы
        public SelectableSalesPersonViewModel(SalesPerson salesPerson, ISalesPersonModel salesPersonModel,
            IViewModelContainer viewModelContainer)
        {
            SalesPerson = salesPerson;
            _salesPersonModel = salesPersonModel;
            _viewModelContainer = viewModelContainer;
        }
        #endregion

        #region Свойства
        public SalesPerson SalesPerson { get; }
        public string Info => SalesPerson.ToString();
        #endregion

        #region Команды

        public AsyncRelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??= new AsyncRelayCommand(ShowConfirmDeleteDialog,
                    (ex) =>
                    {
                        ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                    });
            }
        }

        public AsyncRelayCommand EditCommand
        {
            get
            {
                return _editCommand ??= new AsyncRelayCommand(ExecuteRunEditorDialog,
                    (ex) =>
                    {
                        ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                    });
            }
        }

        #endregion

        #region Методы

        private MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            return new MessageDialogProperty()
            {
                Title = "Подтверждение удаления",
                Message = "Будет удален следующий менеджер:\n\n" + this
            };
        }

        #endregion

        #region Диалоги

        private async Task ExecuteRunEditorDialog(object o)
        {
            EntityContainer.Push(SalesPerson);
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<SalesPersonEditorViewModel>())
            };

            var result = await DialogHost.Show(view, "RootDialog");
        }

        /// <summary>
        /// Показывает простой диалог сообщения
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "RootDialog");
        }

        /// <summary>
        /// Показывает диалог подтверждения удаления предмета
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task ShowConfirmDeleteDialog(object parameter)
        {
            var view = new MessageDialogOkCancel()
            {
                DataContext = new SampleMessageDialogViewModel(GetConfirmDeleteDialogProperty())
            };
            var result = await DialogHost.Show(view, "RootDialog", ClosingDeleteDialogEventHandler);
        }

        /// <summary>
        /// Обработчик события закрытия диалога подтверждения удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventargs"></param>
        private void ClosingDeleteDialogEventHandler(object sender, DialogClosingEventArgs eventargs)
        {
            if (Equals((eventargs.Parameter), true))
                _salesPersonModel.DeleteAsync(SalesPerson);
        }

        #endregion
    }
}