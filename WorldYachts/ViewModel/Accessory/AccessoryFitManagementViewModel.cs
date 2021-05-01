using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.AccessoryControlViews;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Accessory
{
    class AccessoryFitManagementViewModel:BaseViewModel
    {
        #region Поля

        private readonly IAccessoryToBoatModel _accessoryToBoatModel;
        private readonly IViewModelContainer _viewModelContainer;

        private ObservableCollection<SelectableAccessoryFitViewModel> _accessoryToBoatCollection;

        private Visibility _progressBarVisibility;
        private string _filterText;

        #endregion
        public AccessoryFitManagementViewModel(IAccessoryToBoatModel accessoryToBoatModel, IViewModelContainer viewModelContainer)
        {
            _accessoryToBoatModel = accessoryToBoatModel;
            _viewModelContainer = viewModelContainer;

            _accessoryToBoatModel.AccessoryToBoatModelChanged += GetCollectionMethod;
        }
        
        /// <summary>
        /// Список совместимостей
        /// </summary>
        public IEnumerable<FitExpanderViewModel> Fits
        {
            get
            {
                var fits = new List<FitExpanderViewModel>();
                var filteredCollection = Filter();
                //Убираем повторяющиеся аксессуары
                var uniqueAccessories = filteredCollection?
                    .Select(i => i.AccessoryToBoat.AccessoryId)
                    .Distinct();
                if (uniqueAccessories != null)
                {
                    fits.AddRange(
                        uniqueAccessories.Select(accessoryId => 
                            new FitExpanderViewModel(
                                filteredCollection?.FirstOrDefault(i => i.AccessoryToBoat.AccessoryId == accessoryId)
                        ?.AccessoryToBoat.Accessory.Name,
                                filteredCollection?.Where(i => i.AccessoryToBoat.AccessoryId == accessoryId))));
                }
                return fits.Distinct();
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
                    OnPropertyChanged(nameof(Fits));
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

        #region Команды

        /// <summary>
        /// Команда получения коллекции предметов
        /// </summary>
        public AsyncRelayCommand GetItemsCollection => new AsyncRelayCommand(GetCollectionMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message }); });

        /// <summary>
        /// Команда открытия редактора
        /// </summary>
        public DelegateCommand OpenEditorDialog => new DelegateCommand(ExecuteRunEditorDialog);

        #endregion

        /// <summary>
        /// Получение коллекции предметов из БД
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task GetCollectionMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            _accessoryToBoatCollection = GetSelectableViewModels(await _accessoryToBoatModel.GetAllAsync());
            OnPropertyChanged(nameof(Fits));
            ProgressBarVisibility = Visibility.Collapsed;
        }

        private ObservableCollection<SelectableAccessoryFitViewModel> GetSelectableViewModels(IEnumerable<AccessoryToBoat> items)
        {
            var collection = new ObservableCollection<SelectableAccessoryFitViewModel>();
            
            foreach (var accessoryToBoat in items)
            {
                collection.Add(new SelectableAccessoryFitViewModel(accessoryToBoat, _accessoryToBoatModel, _viewModelContainer));
            }
            
            return collection;
        }

        private ObservableCollection<SelectableAccessoryFitViewModel> Filter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                return _accessoryToBoatCollection;
            }

            var filteredCollection = _accessoryToBoatCollection.Where(atb =>
                atb.AccessoryToBoat.Accessory.Name.ToLower().Contains(FilterText.ToLower()));
            
            return new ObservableCollection<SelectableAccessoryFitViewModel>(filteredCollection);
        }

        #region Editor

        /// <summary>
        /// Запуск редактора
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<AccessoryFitEditorViewModel>())
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
