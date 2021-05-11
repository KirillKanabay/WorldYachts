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
using WorldYachts.ViewModel.Accessory;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.OrderMaker
{
    class StepperSelectAccessoriesViewModel : BaseViewModel
    {
        #region Поля

        private readonly OrderContainerViewModel _orderViewModel;
        private readonly IAccessoryToBoatModel _accessoryToBoatModel;
        private readonly IAccessoryModel _accessoryModel;
        private Visibility _progressBarVisibility = Visibility.Collapsed;
        private ObservableCollection<SelectableAccessoryViewModel> _accessories;
        private IViewModelContainer _viewModelContainer;
        #endregion

        public StepperSelectAccessoriesViewModel(IAccessoryToBoatModel accessoryToBoatModel,
            IAccessoryModel accessoryModel,
            OrderContainerViewModel orderViewModel,
            IViewModelContainer viewModelContainer)
        {
            _accessoryModel = accessoryModel;
            _orderViewModel = orderViewModel;
            _accessoryToBoatModel = accessoryToBoatModel;
            _viewModelContainer = viewModelContainer;
        }

        #region Свойства

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        public ObservableCollection<SelectableAccessoryViewModel> AccessoriesCollection
        {
            get => _accessories;
            set
            {
                _accessories = value;
                OnPropertyChanged(nameof(AccessoriesCollection));
            }
        }

        #endregion

        public AsyncRelayCommand LoadedCommand => new AsyncRelayCommand(LoadCollections,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message}); });

        public DelegateCommand SaveAccessories => new DelegateCommand(SaveAccessoriesMethod);

        private void SaveAccessoriesMethod(object arg)
        {
            _orderViewModel.SelectedAccessories = AccessoriesCollection.Where(a => a.IsSelected)
                .Select(a => a.Accessory)
                .ToList();

            string snackbarMessage = "Аксессуары успешно добавлены";
            SendSnackbar(snackbarMessage);
        }

        #region Методы

        public async Task LoadCollections(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            AccessoriesCollection = GetSelectableViewModels((await _accessoryToBoatModel.GetAllAsync())
                .Where(atb => atb.BoatId == _orderViewModel.Boat.Id).Select(atb => atb.Accessory));
            OnPropertyChanged(nameof(AccessoriesCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }

        private ObservableCollection<SelectableAccessoryViewModel> GetSelectableViewModels(
            IEnumerable<Data.Entities.Accessory> items)
        {
            var collection = new ObservableCollection<SelectableAccessoryViewModel>();
            foreach (var accessory in items)
            {
                collection.Add(new SelectableAccessoryViewModel(accessory, _accessoryModel, _viewModelContainer));
            }

            return collection;
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty) o)
            };
            var result = await DialogHost.Show(view, "DialogRoot");
        }
        
        #endregion
    }
}