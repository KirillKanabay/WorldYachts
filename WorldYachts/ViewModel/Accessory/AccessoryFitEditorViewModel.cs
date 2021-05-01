using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers.Commands;
using WorldYachts.Helpers.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Accessory
{
    class AccessoryFitEditorViewModel:BaseViewModel
    {
        #region Поля

        private readonly IAccessoryModel _accessoryModel;
        private readonly IBoatModel _boatModel;
        private readonly IAccessoryToBoatModel _accessoryToBoatModel;

        private readonly AccessoryToBoat _accessoryToBoat;

        private List<Data.Entities.Boat> _boatsCollection;
        private List<Data.Entities.Accessory> _accessoriesCollection;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        private readonly bool _isEdit;

        #endregion

        #region Конструкторы

        public AccessoryFitEditorViewModel(IAccessoryToBoatModel accessoryToBoatModel,
            IBoatModel boatModel,
            IAccessoryModel accessoryModel)
        {
            _accessoryToBoatModel = accessoryToBoatModel;
            _boatModel = boatModel;
            _accessoryModel = accessoryModel;

            _accessoryToBoat = new AccessoryToBoat();
        }
        #endregion

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

        private int _selectedBoatIndex;
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
            get => _accessoryToBoat.Boat;
            set
            {
                _accessoryToBoat.Boat = value;
                _accessoryToBoat.BoatId = value.Id;
                OnPropertyChanged(nameof(SelectedBoat));
            }
        }
        public List<Data.Entities.Boat> BoatsCollection => _boatsCollection;

        private int _selectedAccessoryIndex;
        public int SelectedAccessoryIndex
        {
            get => _selectedAccessoryIndex;
            set
            {
                _selectedAccessoryIndex = value;
                OnPropertyChanged(nameof(SelectedAccessoryIndex));
            }
        }
        public Data.Entities.Accessory SelectedAccessory
        {
            get => _accessoryToBoat.Accessory;
            set
            {
                _accessoryToBoat.Accessory = value;
                _accessoryToBoat.AccessoryId = value.Id;
                OnPropertyChanged(nameof(SelectedAccessory));
            }
        }
        public List<Data.Entities.Accessory> AccessoriesCollection => _accessoriesCollection;

        public bool SaveButtonIsEnabled => _errors.Count == 0;

        #endregion

        #region Команды

        public AsyncRelayCommand LoadedCommand => new AsyncRelayCommand(GetCollections,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message }); });

        public AsyncRelayCommand SaveItem => new AsyncRelayCommand(SaveMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message }); });

        #endregion

        #region Методы

        private async Task GetCollections(object parameter)
        {
            _accessoriesCollection = (await _accessoryModel.GetAllAsync()).ToList();
            _boatsCollection = (await _boatModel.GetAllAsync()).ToList();

            OnPropertyChanged(nameof(AccessoriesCollection));
            OnPropertyChanged(nameof(BoatsCollection));
        }

        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            if (_isEdit)
            {
                await _accessoryToBoatModel.UpdateAsync(_accessoryToBoat);
            }
            else
            {
                await _accessoryToBoatModel.AddAsync(_accessoryToBoat);
            }

            ProgressBarVisibility = Visibility.Collapsed;

            string snackbarMessage = _isEdit
                ? $"Совместимость успешно отредактирована."
                : $"Совместимость успешно добавлена.";

            SendSnackbar(snackbarMessage);
            CloseCurrentDialog();
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

        #region Валидация
        public string Error { get; }
        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        public string this[string columnName]
        {
            get
            {
                string error = columnName switch
                {
                    nameof(SelectedAccessory) => new Validation(new NotEmptyFieldValidationRule(SelectedAccessory)).Validate(),
                    nameof(SelectedBoat) => new Validation(new NotEmptyFieldValidationRule(SelectedBoat)).Validate()
                };

                _errors.Remove(columnName);
                if (!string.IsNullOrWhiteSpace(error))
                    _errors.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }
        #endregion

    }
}
