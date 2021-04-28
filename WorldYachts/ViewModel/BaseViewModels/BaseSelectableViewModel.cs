using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.BaseViewModels
{
    public abstract class BaseSelectableViewModel<TItem> : BaseViewModel
    {
        #region Поля

        protected readonly TItem _item;
        protected readonly IDataModel<TItem> _dataModel;
        protected bool _isSelected;
        protected bool _isDeleted = false;

        protected AsyncRelayCommand _removeCommand;
        protected AsyncRelayCommand _editCommand;

        public Action OnItemChanged;
        #endregion

        #region Конструкторы

        protected BaseSelectableViewModel(TItem item, IDataModel<TItem> dataModel)
        {
            _item = item;
            _dataModel = dataModel;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Является ли предмет выбранным
        /// </summary>
        public virtual bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                if (_isSelected)
                    BaseManagementViewModel<TItem>.OnItemChanged?.Invoke();
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                if (_isDeleted)
                    BaseManagementViewModel<TItem>.OnItemChanged?.Invoke();
                OnPropertyChanged(nameof(IsDeleted));
            }
        }

        /// <summary>
        /// Экземпляр предмета
        /// </summary>
        public TItem Item => _item;

        /// <summary>
        /// Экземпляр редактора
        /// </summary>
        public abstract BaseEditorViewModel<TItem> Editor { get; }

        #endregion

        #region Команды

        /// <summary>
        /// Команда удаления предмета
        /// </summary>
        public AsyncRelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??= new AsyncRelayCommand(ShowConfirmDeleteDialog,
                    (ex) =>
                    {
                        ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message});
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
                        ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message});
                    });
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Запуск диалога редактирования объекта
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private async Task ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(Editor)
            };
            //Добавляем метод обновления редактора после загрузки при редактировании
            ToggleViewEditorAfterLoaded();

            //var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //Убираем метод обновления редактора после загрузки при редактировании
            ToggleViewEditorAfterLoaded();

            //Извещаем об изменении предмета
            BaseManagementViewModel<TItem>.OnItemChanged?.Invoke();
        }

        /// <summary>
        /// Добавление/удаление метода обновления данных редактора после загрузки
        /// </summary>
        protected abstract void ToggleViewEditorAfterLoaded();

        protected abstract BaseViewModel GetEditorViewModel();

        /// <summary>
        /// Показывает простой диалог сообщения
        /// </summary>
        /// <param name="o"></param>
        protected void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty) o)
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
        /// Возвращает параметры для диалога подтверждения удаления
        /// </summary>
        /// <returns>Параметры диалога</returns>
        protected abstract MessageDialogProperty GetConfirmDeleteDialogProperty();
        

        /// <summary>
        /// Обработчик события закрытия диалога подтверждения удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventargs"></param>
        protected void ClosingDeleteDialogEventHandler(object sender, DialogClosingEventArgs eventargs)
        {
            if (Equals((eventargs.Parameter), true))
                IsDeleted = true;
        }

        #endregion
    }
}