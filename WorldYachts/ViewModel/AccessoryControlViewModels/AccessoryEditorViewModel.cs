using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryEditorViewModel:BaseEditorViewModel<Accessory>, IDataErrorInfo
    {
        #region Поля

        private readonly Accessory _accessory;
        private readonly IDataModel<Accessory> _accessoryModel;
        private readonly PartnerModel _partnerModel;
        private List<Partner> _partnersCollection;
        private Partner _selectedPartner;
        private AsyncRelayCommand _loadedCommand;
        #endregion

        #region Конструкторы
        public AccessoryEditorViewModel(AccessoryModel accessoryModel, PartnerModel partnerModel, EntityContainer entityContainer) : base(false)
        {
            _accessoryModel = accessoryModel;
            _partnerModel = partnerModel;

            if (!entityContainer.IsEmpty)
            {
                _accessory = entityContainer.Pop<Accessory>();
            }
            else
            {
                _accessory = new Accessory();
            }
        }

        #endregion

        #region Свойства

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

        public override bool SaveButtonIsEnabled => ErrorDictionary.Count == 0;
        public override IDataModel<Accessory> ModelItem => _accessoryModel;

        #endregion
        public AsyncRelayCommand LoadedCommand
        {
            get
            {
                return _loadedCommand ??= new AsyncRelayCommand(GetPartners, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }

        private async Task GetPartners(object parameter)
        {
            _partnersCollection = (await _partnerModel.GetAllAsync()).ToList();
            OnPropertyChanged(nameof(PartnersCollection));
        }

        protected override Accessory GetSaveItem(bool isEdit) => _accessory;

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return _isEdit
                ? $"Аксессуар \"{Name}\" успешно отредактирован."
                : $"Аксессуар \"{Name}\" успешно добавлен.";
        }

        #region Валидация полей
        public string Error { get; }

        private readonly Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        new Validation(new NotEmptyFieldValidationRule(Name)).Validate(ref error);
                        break;
                    case "Description":
                        new Validation(new NotEmptyFieldValidationRule(Description)).Validate(ref error);
                        break;
                    case "Price":
                        new Validation(new NotEmptyFieldValidationRule(Price)).Validate(ref error);
                        break;
                    case "Vat":
                        new Validation(new NotEmptyFieldValidationRule(Vat)).Validate(ref error);
                        break;
                    case "Inventory":
                        new Validation(new NotEmptyFieldValidationRule(Inventory)).Validate(ref error);
                        break;
                }
                ErrorDictionary.Remove(columnName);
                if (error != String.Empty)
                    ErrorDictionary.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }

        #endregion

    }
}
