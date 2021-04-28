using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Helpers.Validators;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Accessory
{
    class AccessoryEditorViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Поля

        private readonly IAccessoryModel _accessoryModel;
        private readonly IPartnerModel _partnerModel;
        
        public readonly Data.Entities.Accessory _accessory;
        private List<Data.Entities.Partner> _partnersCollection;
        
        private Visibility _progressBarVisibility = Visibility.Collapsed;
        
        private readonly bool _isEdit;

        #endregion

        #region Конструкторы

        public AccessoryEditorViewModel(IAccessoryModel accessoryModel, IPartnerModel partnerModel)
        {
            _accessoryModel = accessoryModel;
            _partnerModel = partnerModel;

            if (!EntityContainer.IsEmpty)
            {
                _accessory = EntityContainer.Pop<Data.Entities.Accessory>();
                SelectedPartner = _accessory.Partner;
                _isEdit = true;
            }
            else
            {
                _accessory = new Data.Entities.Accessory();
            }
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

        public string Name
        {
            get => _accessory.Name;
            set
            {
                _accessory.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _accessory.Description;
            set
            {
                _accessory.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public decimal Price
        {
            get => _accessory.Price;
            set
            {
                _accessory.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public double Vat
        {
            get => _accessory.Vat;
            set
            {
                _accessory.Vat = value;
                OnPropertyChanged(nameof(Vat));
            }
        }

        public int Inventory
        {
            get => _accessory.Inventory;
            set
            {
                _accessory.Inventory = value;
                OnPropertyChanged(nameof(Inventory));
            }
        }

        public int _selectedPartnerIndex;
        public int SelectedPartnerIndex
        {
            get => _selectedPartnerIndex;
            set
            {
                _selectedPartnerIndex = value;
                OnPropertyChanged(nameof(SelectedPartnerIndex));
            }
        } 
        public Data.Entities.Partner SelectedPartner
        {
            get => _accessory.Partner;
            set
            {
                _accessory.Partner = value;
                _accessory.PartnerId = value.Id;
                OnPropertyChanged(nameof(SelectedPartner));
            }
        }
        public List<Data.Entities.Partner> PartnersCollection => _partnersCollection;
        public bool SaveButtonIsEnabled => _errors.Count == 0;

        #endregion

        #region Команды

        public AsyncRelayCommand LoadedCommand => new AsyncRelayCommand(GetPartners,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message}); });

        public AsyncRelayCommand SaveItem => new AsyncRelayCommand(SaveMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message}); });

        #endregion

        #region Методы

        private async Task GetPartners(object parameter)
        {
            _partnersCollection = (await _partnerModel.GetAllAsync()).ToList();
            
            int partnerIndex = _partnersCollection.FindIndex(a => a.Id == _accessory.PartnerId);
            if (partnerIndex != -1)
                SelectedPartnerIndex = partnerIndex;

            OnPropertyChanged(nameof(PartnersCollection));
        }

        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            if (_isEdit)
            {
                await _accessoryModel.UpdateAsync(_accessory);
            }
            else
            {
                await _accessoryModel.AddAsync(_accessory);
            }

            ProgressBarVisibility = Visibility.Collapsed;

            string snackbarMessage = _isEdit
                ? $"Аксессуар \"{Name}\" успешно отредактирован."
                : $"Аксессуар \"{Name}\" успешно добавлен.";

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

        #region Валидация полей

        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();
        public string Error { get; }
        
        public string this[string columnName]
        {
            get
            {
                string error = columnName switch
                {
                    "Name" => new Validation(new NotEmptyFieldValidationRule(Name)).Validate(),
                    "Description" => new Validation(new NotEmptyFieldValidationRule(Description)).Validate(),
                    "Price" => new Validation(new NotEmptyFieldValidationRule(Price),
                        new PositiveNumberValidationRule(Price)).Validate(),
                    "Vat" => new Validation(new NotEmptyFieldValidationRule(Vat),
                        new PositiveNumberValidationRule(Price)).Validate(),
                    "Inventory" => new Validation(new NotEmptyFieldValidationRule(Inventory)).Validate(),
                    _ => null
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