using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryManagementViewModel : BaseViewModel
    {
        #region Поля

        private ObservableCollection<SelectableAccessoryViewModel> _accessories =
            new ObservableCollection<SelectableAccessoryViewModel>();

        protected string _filterText;
        
        private Visibility _progressBarVisibility;

        private readonly IAccessoryModel _accessoryModel;
        private readonly IViewModelContainer _viewModelContainer;

        #endregion

        #region Конструкторы
        public AccessoryManagementViewModel(IAccessoryModel accessoryModel, IViewModelContainer viewModelContainer)
        {
            _accessoryModel = accessoryModel;
            _viewModelContainer = viewModelContainer;
        }

        #endregion
        
        #region Свойства

        /// <summary>
        /// Фильтрованная коллекция
        /// </summary>
        public virtual ObservableCollection<SelectableAccessoryViewModel> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(_filterText);

                return _accessories;
            }
        }

        public virtual string FilterText
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

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        #endregion

        #region Команды

        /// <summary>
        /// Команда получения коллекции предметов
        /// </summary>
        public AsyncRelayCommand GetItemsCollection => new AsyncRelayCommand(GetCollectionMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message}); });

        /// <summary>
        /// Удаляет предмет помеченный IsDeleted
        /// </summary>
        public AsyncRelayCommand RemoveItem => new AsyncRelayCommand(RemoveItemMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message}); });

        /// <summary>
        /// Удаляет коллекцию предметов
        /// </summary>
        public AsyncRelayCommand RemoveItemsCollection => new AsyncRelayCommand(RemoveItemsCollectionMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message}); });


        /// <summary>
        /// Команда открытия редактора
        /// </summary>
        public DelegateCommand OpenEditorDialog => new DelegateCommand(ExecuteRunEditorDialog);

        #endregion

        #region Методы
        private async Task RemoveItemsCollectionMethod(object parameter)
        {
            var removeItems = _accessories.Where(i => i.IsSelected)
                .Select(i => i.Item);

            if (removeItems.Any())
            {
                await Task.Run(() => _accessoryModel.DeleteAsync(removeItems));

                //Получаем главное окно для показа уведомления о удалении
                var mainWindow = (MainWindow) Application.Current.MainWindow;

                GetItemsCollection.Execute(null);

                mainWindow.SendSnackbar($"Успешно удалено.");
            }
        }

        private async Task RemoveItemMethod(object parameter)
        {
            var removeItems = _accessories.Where(i => i.IsDeleted)
                .Select(i => i.Item);

            if (removeItems.Any())
            {
                await _accessoryModel.DeleteAsync(removeItems);

                //Получаем главное окно для показа уведомления о удалении
                var mainWindow = (MainWindow) Application.Current.MainWindow;

                GetItemsCollection.Execute(null);

                SendSnackbar($"Успешно удалено.");
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
            _accessories = GetSelectableViewModels(await _accessoryModel.GetAllAsync());
            OnPropertyChanged(nameof(FilteredCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }
        
        private ObservableCollection<SelectableAccessoryViewModel> GetSelectableViewModels(IEnumerable<Accessory> items)
        {
            var collection = new ObservableCollection<SelectableAccessoryViewModel>();
            foreach (var accessory in items)
            {
                collection.Add(new SelectableAccessoryViewModel(accessory, _accessoryModel));
            }

            return collection;
        }

        private ObservableCollection<SelectableAccessoryViewModel> Filter(string filterText)
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                return _accessories;
            }

            var filteredCollection = _accessories.Where(a =>
                a.Item.Name.ToLower().Contains(filterText.ToLower()) ||
                a.Item.Id.ToString() == filterText);

            return new ObservableCollection<SelectableAccessoryViewModel>(filteredCollection);
        }

        #endregion

        #region Editor

        /// <summary>
        /// Запуск редактора
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<AccessoryEditorViewModel>())
            };

            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "DialogRoot", ClosingEventHandler);
        }

        /// <summary>
        /// Обработчик события закрытия диалога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private async void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            await GetCollectionMethod(null);
            OnPropertyChanged(nameof(FilteredCollection));
        }

        #endregion
    }
}