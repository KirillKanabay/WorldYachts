using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data.Entities;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.ViewModel.BaseViewModels;
using Partner = WorldYachts.Data.Partner;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryEditorViewModel:BaseEditorViewModel<Accessory>, IDataErrorInfo
    {
        #region Поля

        private readonly Accessory _accessory;
        private List<Partner> _partners;
        private readonly IDataModel<Accessory> _accessoryModel;
        #endregion

        #region Конструкторы
        public AccessoryEditorViewModel(Data.Entities.Accessory accessory, AccessoryModel accessoryModel) : base(true)
        {
            _accessory = accessory;
            _accessoryModel = accessoryModel;
        }

        public AccessoryEditorViewModel(AccessoryModel accessoryModel):base(false)
        {
            _accessory = new Accessory();
            _accessoryModel = accessoryModel;
            _partners = new PartnerModel().Load().ToList();
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
        
        public ObservableCollection<string> Partners
        {
            get
            {
                var partnersCollection = new ObservableCollection<string>();
                if (_partners != null)
                {
                    foreach (var partner in _partners?.Select(p => p.Name))
                    {
                        partnersCollection.Add(partner);
                    }
                }
                

                return partnersCollection;
            }
        } 

        public override bool SaveButtonIsEnabled => ErrorDictionary.Count == 0;
        public override IDataModel<Accessory> ModelItem => _accessoryModel;

        #endregion

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
