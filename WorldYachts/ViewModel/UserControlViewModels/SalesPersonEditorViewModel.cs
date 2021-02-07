using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.UserControlViewModels
{
    class SalesPersonEditorViewModel:BaseEditorViewModel<SalesPerson>,IDataErrorInfo
    {
        #region Поля

        private readonly int _id;
        private string _name;
        private string _secondName;
        private string _login;
        private string _password;
        private User _user;
        #endregion

        #region Конструкторы
        public SalesPersonEditorViewModel(SalesPerson item) : base(true)
        {
            _id = item.Id;
            _name = item.Name;
            _secondName = item.SecondName;
        }

        public SalesPersonEditorViewModel():base(false)
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

        public string SecondName
        {
            get => _secondName;
            set
            {
                _secondName = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }


        #endregion

        public override bool SaveButtonIsEnabled => !ErrorDictionary.Any();
        public override IDataModel<SalesPerson> ModelItem => new SalesPersonModel();

        #region Методы

        protected override SalesPerson GetSaveItem(bool isEdit)
        {
            return new SalesPerson()
            {
                Id = (isEdit) ? _id : default,
                Name = _name,
                SecondName = _secondName
            };
        }

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return _isEdit
                ? $"Менеджер \"{Name}\" успешно отредактирован."
                : $"Менеджер \"{Name}\" успешно добавлен.";
        }

        #endregion

        #region Валидация полей
        
        private readonly Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();
        
        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                return error;
            }
        }
        
        #endregion

    }
}
