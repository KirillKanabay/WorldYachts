using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    public class PartnerEditorViewModel : BaseEditorViewModel<Partner>, IDataErrorInfo
    {
        #region Поля

        private readonly int _id;
        private string _name;
        private string _address;
        private string _city;
        #endregion

        #region Конструкторы

        public PartnerEditorViewModel(Partner partner):base(isEdit: true)
        {
            _id = partner.Id;
            _name = partner.Name;
            _city = partner.City;
            _address = partner.Address;
        }

        public PartnerEditorViewModel():base(isEdit: false)
        {
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Название партнера
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Город партнера
        /// </summary>
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        /// <summary>
        /// Адрес партнера
        /// </summary>
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        
        public override bool SaveButtonIsEnabled => _errors.Count == 0;

        public override IDataModel<Partner> ModelItem => null;

        #endregion
        
        #region Методы

        protected override Partner GetSaveItem(bool isEdit)
        {
            return new Partner()
            {
                Id = (isEdit) ? _id : default,
                Address = _address,
                City = _city,
                Name = _name
            };
        }

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return _isEdit
                ? $"Партнер \"{Name}\" успешно отредактирован."
                : $"Партнер \"{Name}\" успешно добавлен.";
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
                    "Address" => new Validation(new NotEmptyFieldValidationRule(Address)).Validate(),
                    "City" => new Validation(new NotEmptyFieldValidationRule(City)).Validate(),
                    _ => null
                };

                _errors.Remove(columnName);
                if (!string.IsNullOrWhiteSpace(error))
                    _errors.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }
    }

    #endregion
}