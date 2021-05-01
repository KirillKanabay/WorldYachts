using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Accessory
{
    class AccessoryManagementViewModel : BaseViewModel
    {
        #region Поля

        private readonly IAccessoryModel _accessoryModel;
        private readonly IViewModelContainer _viewModelContainer;

        private ObservableCollection<SelectableAccessoryViewModel> _accessories;

        private Visibility _progressBarVisibility;
        private string _filterText;

        #endregion

        #region Конструкторы
        public AccessoryManagementViewModel(IAccessoryModel accessoryModel, IViewModelContainer viewModelContainer)
        {
            _accessoryModel = accessoryModel;
            _viewModelContainer = viewModelContainer;

            _accessoryModel.AccessoryModelChanged += GetCollectionMethod;
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
                    return Filter();

                return _accessories;
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
        /// Команда открытия редактора
        /// </summary>
        public DelegateCommand OpenEditorDialog => new DelegateCommand(ExecuteRunEditorDialog);

        #endregion

        #region Методы

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
        
        private ObservableCollection<SelectableAccessoryViewModel> GetSelectableViewModels(IEnumerable<Data.Entities.Accessory> items)
        {
            var collection = new ObservableCollection<SelectableAccessoryViewModel>();
            foreach (var accessory in items)
            {
                collection.Add(new SelectableAccessoryViewModel(accessory, _accessoryModel, _viewModelContainer));
            }

            return collection;
        }

        private ObservableCollection<SelectableAccessoryViewModel> Filter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                return _accessories;
            }

            var filteredCollection = _accessories.Where(a =>
                a.Accessory.Name.ToLower().Contains(FilterText.ToLower()) ||
                a.Accessory.Id.ToString() == FilterText);

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

            var result = await DialogHost.Show(view, "RootDialog");
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "DialogRoot");
        }
        
        #endregion
    }
}