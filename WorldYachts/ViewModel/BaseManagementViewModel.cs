using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel
{
    abstract class BaseManagementViewModel<TItem> : BaseViewModel
    {
        #region Поля

        /// <summary>
        /// Коллекция управляемых предметов
        /// </summary>
        protected ObservableCollection<BaseSelectableViewModel<TItem>> _itemsCollection;

        /// <summary>
        /// Поисковая строка
        /// </summary>
        private string _filterText;

        private AsyncRelayCommand _getItemsCollection;
        private AsyncRelayCommand _removeItem;
        private AsyncRelayCommand _removeItemsCollection;
        private DelegateCommand _openEditorDialog;


        private Visibility _progressBarVisibility;

        #endregion

        #region Конструкторы

        protected BaseManagementViewModel()
        {
            _itemsCollection.CollectionChanged += CheckDeletedItems;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Фильтрованная коллекция
        /// </summary>
        public ObservableCollection<BaseSelectableViewModel<TItem>> FilteredCollection
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_filterText))
                    return Filter(_filterText);

                return _itemsCollection;
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                if (_filterText != null)
                {
                    OnPropertyChanged(nameof(FilteredCollection));
                }
            }
        }

        /// <summary>
        /// Видимость прогресс бара
        /// </summary>
        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        /// <summary>
        /// Модель предмета
        /// </summary>
        public abstract IDataModel<TItem> Model { get; }

        /// <summary>
        /// Редактор предмета
        /// </summary>
        public abstract BaseEditorViewModel Editor { get; }

        public abstract BaseSelectableViewModel<TItem> BaseSelectableViewModel { get; }

        #endregion

        #region Команды

        /// <summary>
        /// Команда получения коллекции предметов
        /// </summary>
        public AsyncRelayCommand GetItemsCollection
        {
            get
            {
                return _getItemsCollection ??= new AsyncRelayCommand(GetCollectionMethod,
                    (ex) =>
                    {
                        ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message});
                    });
            }
        }
        /// <summary>
        /// Удаляет предмет помеченный IsDeleted
        /// </summary>
        public AsyncRelayCommand RemoveItem
        {
            get
            {
               return _removeItem ??= new AsyncRelayCommand(RemoveItemMethod,
                    (ex) =>
                    {
                        ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message});
                    });
            }
        }
        /// <summary>
        /// Удаляет коллекцию предметов
        /// </summary>
        public AsyncRelayCommand RemoveItemsCollection
        {
            get
            {
                return _removeItemsCollection ??= new AsyncRelayCommand(RemoveItemsCollectionMethod,
                    (ex) =>
                    {
                        ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                    });
            }
        }
        /// <summary>
        /// Команда открытия редактора
        /// </summary>
        public DelegateCommand OpenEditorDialog
        {
            get
            {
                return _openEditorDialog ??= new DelegateCommand(ExecuteRunEditorDialog);
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверка удаляемых предметов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDeletedItems(object sender, NotifyCollectionChangedEventArgs e)
        {
            RemoveItem.Execute(null);
        }

        private async Task RemoveItemsCollectionMethod(object parameter)
        {
            var removeItems = _itemsCollection.Where(i => i.IsSelected)
                .Select(i => i.Item);

            if (removeItems.Any())
            {
                await Task.Run(() => Model.RemoveAsync(removeItems));

                //Получаем главное окно для показа уведомления о удалении
                var mainWindow = (MainWindow)Application.Current.MainWindow;

                GetItemsCollection.Execute(null);

                mainWindow.SendSnackbar($"Успешно удалено.");
            }
        }

        /// <summary>
        /// Удаляет предметы помеченные isDeleted
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task RemoveItemMethod(object parameter)
        {
            var removeItems = _itemsCollection.Where(i => i.IsDeleted)
                .Select(i => i.Item);

            if (removeItems.Any())
            {
                await Task.Run(() => Model.RemoveAsync(removeItems));

                //Получаем главное окно для показа уведомления о удалении
                var mainWindow = (MainWindow) Application.Current.MainWindow;
                
                GetItemsCollection.Execute(null);

                mainWindow.SendSnackbar($"Успешно удалено.");
            }
        }

        /// <summary>
        /// Получение коллекции предметов из БД
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task GetCollectionMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var items = await Model.LoadAsync();
            _itemsCollection = GetSelectableViewModels(items);

            ProgressBarVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Преобразования списка предметов в коллекцию выбираемых предметов
        /// </summary>
        protected abstract ObservableCollection<BaseSelectableViewModel<TItem>> GetSelectableViewModels(
            IEnumerable<TItem> items);

        /// <summary>
        /// Метод фильтрации по поисковой строке
        /// </summary>
        /// <param name="filterText"></param>
        /// <returns></returns>
        protected abstract ObservableCollection<BaseSelectableViewModel<TItem>> Filter(string filterText);

        /// <summary>
        /// Запуск диалога сообщения
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty) o)
            };
            var result = await DialogHost.Show(view, "MessageDialogRoot", ClosingEventHandler);
        }

        /// <summary>
        /// Запуск редактора
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(Editor)
            };

            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        /// <summary>
        /// Обработчик события закрытия диалога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            //GetCollection.Execute(null);
            OnPropertyChanged(nameof(FilteredCollection));
        }

        #endregion
    }
}