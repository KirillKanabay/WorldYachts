using System;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.Editors;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.CatalogControlViewModels;
using WorldYachts.ViewModel.MessageDialog;
using Accessory = WorldYachts.Data.Entities.Accessory;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    public class SelectableAccessoryViewModel:BaseViewModel
    {
        #region Поля

        protected bool _isSelected;
        protected bool _isDeleted = false;

        protected AsyncRelayCommand _removeCommand;
        protected AsyncRelayCommand _editCommand;

        public Action OnItemChanged;

        private readonly IAccessoryModel _accessoryModel;
        private readonly IViewModelContainer _viewModelContainer;
        #endregion
        
        #region Конструкторы
        public SelectableAccessoryViewModel(Accessory accessory, 
            IAccessoryModel accessoryModel, 
            IViewModelContainer viewModelContainer)
        {
            Accessory = accessory;
            _accessoryModel = accessoryModel;
            _viewModelContainer = viewModelContainer;
        }

        #endregion

        #region Свойства
        public Accessory Accessory { get; }
        public bool IsSelected
        {
            get => _isSelected;

            set
            {
                _isSelected = value;
                BoatViewModel.OnAccessoryChanged.Invoke(this);
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                if (_isDeleted)
                    BaseManagementViewModel<Accessory>.OnItemChanged?.Invoke();
                OnPropertyChanged(nameof(IsDeleted));
            }
        }
        public string Info => Accessory.ToString();

        #endregion

        #region Методы

        protected void ToggleViewEditorAfterLoaded()
        {
            if (AccessoryEditorView.EditorAfterLoad != null)
            {
                AccessoryEditorView.EditorAfterLoad = null;
            }
            else
            {
                AccessoryEditorView.EditorAfterLoad = () => _viewModelContainer.GetViewModel<AccessoryEditorViewModel>();
            }
        }

        protected MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            return new MessageDialogProperty()
            {
                Title = "Подтверждение удаления",
                Message = "Будет удален следующий аксессуар:\n\n" + Info,
            };
        }

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

        #region Диалоги
        private async Task ExecuteRunEditorDialog(object o)
        {
            EntityContainer.Push(Accessory);
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<AccessoryEditorViewModel>())
            };

            var result = await DialogHost.Show(view, "RootDialog");
        }

        /// <summary>
        /// Показывает простой диалог сообщения
        /// </summary>
        /// <param name="o"></param>
        protected void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            //var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
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
            OnItemChanged?.Invoke();
        }

        /// <summary>
        /// Обработчик события закрытия диалога подтверждения удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventargs"></param>
        private void ClosingDeleteDialogEventHandler(object sender, DialogClosingEventArgs eventargs)
        {
            if (Equals((eventargs.Parameter), true))
                _accessoryModel.DeleteAsync(Accessory);
        }
        #endregion

    }
}
