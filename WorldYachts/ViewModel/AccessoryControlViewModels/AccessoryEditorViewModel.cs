﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Printing.IndexedProperties;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.Validators;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryEditorViewModel:BaseEditorViewModel<Accessory>, IDataErrorInfo
    {
        #region Поля

        private readonly int _id;
        private string _name;
        private string _description;
        private decimal _price;
        private double _vat;
        private int _inventory;
        private int _orderLevel;
        private int _orderBatch;
        private int _partnerId;

        #endregion

        #region Конструкторы
        public AccessoryEditorViewModel(Accessory accessory) : base(true)
        {
            _id = accessory.Id;
            _name = accessory.Name;
            _description = accessory.Description;
            _price = accessory.Price;
            _vat = accessory.Vat;
            _inventory = accessory.Inventory;
            _orderLevel = accessory.OrderLevel;
            _orderBatch = accessory.OrderBatch;
            _partnerId = accessory.PartnerId;
        }

        public AccessoryEditorViewModel():base(false)
        {

        }
        #endregion

        #region Свойства

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public double Vat
        {
            get => _vat;
            set
            {
                _vat = value;
                OnPropertyChanged(nameof(Vat));
            }
        }

        public int Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
                OnPropertyChanged(nameof(Inventory));
            }
        }

        public int OrderLevel
        {
            get => _orderLevel;
            set
            {
                _orderLevel = value;
                OnPropertyChanged(nameof(OrderLevel));
            }
        }

        public int OrderBatch
        {
            get => _orderBatch;
            set
            {
                _orderBatch = value;
                OnPropertyChanged(nameof(OrderBatch));
            }
        }

        public int PartnerId
        {
            get => _partnerId;
            set
            {
                _partnerId = value;
                OnPropertyChanged(nameof(PartnerId));
            }
        }

        public override bool SaveButtonIsEnabled => ErrorDictionary.Count == 0;
        public override IDataModel<Accessory> ModelItem => new AccessoryModel();
        #endregion

        protected override Accessory GetSaveItem(bool isEdit)
        {
            return new Accessory()
            {
                Id = (isEdit) ? _id : default,
                Name = _name,
                Description = _description,
                Price = _price,
                Vat = _vat,
                Inventory = _inventory,
                OrderLevel = _orderLevel,
                OrderBatch = _orderBatch,
                PartnerId = _partnerId
            };
        }

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
                    case "OrderLevel":
                        new Validation(new NotEmptyFieldValidationRule(OrderLevel)).Validate(ref error);
                        break;
                    case "OrderBatch":
                        new Validation(new NotEmptyFieldValidationRule(OrderBatch)).Validate(ref error);
                        break;
                    case "PartnerId":
                        new Validation(new NotEmptyFieldValidationRule(PartnerId)).Validate(ref error);
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