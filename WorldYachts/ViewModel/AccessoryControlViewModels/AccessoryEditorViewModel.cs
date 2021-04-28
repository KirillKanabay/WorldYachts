using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryEditorViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Поля

        private readonly AccessoryModel _accessoryModel;
        private readonly PartnerModel _partnerModel;

        private readonly Accessory _accessory;

        private List<Partner> _partnersCollection;
        private Partner _selectedPartner;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        private readonly bool _isEdit;

        #endregion

        #region Конструкторы

        public AccessoryEditorViewModel(AccessoryModel accessoryModel, PartnerModel partnerModel,
            EntityContainer entityContainer)
        {
            _accessoryModel = accessoryModel;
            _partnerModel = partnerModel;

            if (!entityContainer.IsEmpty)
            {
                _accessory = entityContainer.Pop<Accessory>();
                _isEdit = true;
            }
            else
            {
                _accessory = new Accessory();
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

        public Partner SelectedPartner
        {
            get => _selectedPartner;
            set
            {
                _selectedPartner = value;
                _accessory.PartnerId = _selectedPartner.Id;
                OnPropertyChanged(nameof(SelectedPartner));
            }
        }

        public List<Partner> PartnersCollection => _partnersCollection;
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

            string SnackbarMessage = _isEdit
                ? $"Аксессуар \"{Name}\" успешно отредактирован."
                : $"Аксессуар \"{Name}\" успешно добавлен.";

            SendSnackbar(SnackbarMessage);
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