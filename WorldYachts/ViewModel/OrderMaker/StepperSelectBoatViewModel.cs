using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.OrderMaker
{
    public class StepperSelectBoatViewModel : Step
    {
        #region Поля

        private readonly OrderContainerViewModel _orderViewModel;
        private readonly IBoatModel _boatModel;
        private Visibility _progressBarVisibility = Visibility.Collapsed;
        private List<Data.Entities.Boat> _boats;
        private int _selectedBoatIndex;
        #endregion
        public StepperSelectBoatViewModel(IBoatModel boatModel, OrderContainerViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
            _boatModel = boatModel;
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

        public List<Data.Entities.Boat> BoatsCollection
        {
            get => _boats;
            set
            {
                _boats = value;
                OnPropertyChanged(nameof(BoatsCollection));
            }
        }

        public int SelectedBoatIndex
        {
            get => _selectedBoatIndex;
            set
            {
                _selectedBoatIndex = value;
                OnPropertyChanged(nameof(SelectedBoatIndex));
            }
        }

        public Data.Entities.Boat SelectedBoat
        {
            get => _orderViewModel.Boat;
            set
            {
                _orderViewModel.Boat = value;
                OnPropertyChanged(nameof(BoatInfo));
                OnPropertyChanged(nameof(SelectedBoat));
            }
        }

        public string BoatInfo => _orderViewModel.Boat?.ToString();
        #endregion

        public AsyncRelayCommand LoadedCommand => new AsyncRelayCommand(LoadCollections,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message }); });

        #region Методы

        public async Task LoadCollections(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            BoatsCollection = (await _boatModel.GetAllAsync()).ToList();
            OnPropertyChanged(nameof(BoatsCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "DialogRoot");
        }

        public override void Validate()
        {
            HasValidationErrors = _orderViewModel.Boat == null;
        }

        #endregion
    }
}
