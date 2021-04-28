using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Helpers.Validators;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryEditorViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Поля

        private readonly IAccessoryModel _accessoryModel;
        private readonly PartnerModel _partnerModel;

        public readonly Accessory Accessory;

        private List<Partner> _partnersCollection;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        private readonly bool _isEdit;

        #endregion

        #region Конструкторы

        public AccessoryEditorViewModel(IAccessoryModel accessoryModel, PartnerModel partnerModel)
        {
            _accessoryModel = accessoryModel;
            _partnerModel = partnerModel;

            if (!EntityContainer.IsEmpty)
            {
                Accessory = EntityContainer.Pop<Accessory>();
                SelectedPartner = Accessory.Partner;
                _isEdit = true;
            }
            else
            {
                Accessory = new Accessory();
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
            get => Accessory.Name;
            set
            {
                Accessory.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => Accessory.Description;
            set
            {
                Accessory.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public decimal Price
        {
            get => Accessory.Price;
            set
            {
                Accessory.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public double Vat
        {
            get => Accessory.Vat;
            set
            {
                Accessory.Vat = value;
                OnPropertyChanged(nameof(Vat));
            }
        }

        public int Inventory
        {
            get => Accessory.Inventory;
            set
            {
                Accessory.Inventory = value;
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
        public Partner SelectedPartner
        {
            get => Accessory.Partner;
            set
            {
                Accessory.Partner = value;
                Accessory.PartnerId = value.Id;
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
            
            int partnerIndex = _partnersCollection.FindIndex(a => a.Id == Accessory.PartnerId);
            if (partnerIndex != -1)
                SelectedPartnerIndex = partnerIndex;

            OnPropertyChanged(nameof(PartnersCollection));
        }

        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            if (_isEdit)
            {
                await _accessoryModel.UpdateAsync(Accessory);
            }
            else
            {
                await _accessoryModel.AddAsync(Accessory);
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