using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using WorldYachts.Annotations;
using WorldYachts.Helpers;
using WorldYachts.Model;
using WorldYachts.Validators;
using IDataErrorInfo = System.ComponentModel.IDataErrorInfo;

namespace WorldYachts.ViewModel
{
    class RegisterViewModel:INotifyPropertyChanged, IDataErrorInfo
    {
        #region Поля
        private string _name;
        private string _secondName;
        private DateTime _birthDate = new DateTime(2000, 1, 1);
        private string _organizationName;
        private string _city;
        private string _address;
        private string _email;
        private string _phone;
        private string _idDocumentName;
        private string _idNumber;
        private string _login;
        private string _password;
        private string _passwordRepeated;
        private bool _buttonIsEnabled;
        private string _fieldsError;
        #endregion

        #region Свойства
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string SecondName
        {
            get => _secondName;
            set
            {
                _secondName = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Название организации
        /// </summary>
        public string OrganizationName
        {
            get => _organizationName;
            set
            {
                _organizationName = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Город проживания
        /// </summary>
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Адрес проживания
        /// </summary>
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Телефон пользователя
        /// </summary>
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Документ подтверждающий личность
        /// </summary>
        public string IdDocumentName
        {
            get => _idDocumentName;
            set
            {
                _idDocumentName = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Серия документа
        /// </summary>
        public string IdNumber
        {
            get => _idNumber;
            set
            {
                _idNumber = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("PasswordRepeated");
            }
        }
        /// <summary>
        /// Повторно введенный пароль пользователя
        /// </summary>
        public string PasswordRepeated
        {
            get => _passwordRepeated;
            set
            {
                _passwordRepeated = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion

        #region Команды
        private DelegateCommand _register;

        /// <summary>
        /// Команда регистрации
        /// </summary>
        public DelegateCommand Register
        {
            get
            {
                return _register ??= new DelegateCommand((arg) =>
                {
                    var rm = new RegisterModel()
                    {
                        Name = _name,
                        SecondName = _secondName,
                        Address = _address,
                        City = _city,
                        BirthDate = _birthDate,
                        Email = _email,
                        IdDocumentName = _idDocumentName,
                        Login = _login,
                        OrganizationName = _organizationName,
                        IdNumber = _idNumber,
                        Phone = _phone,
                        Password = _password
                    };
                    rm.Register();
                });
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }

        #region Валидация полей
        private Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();
        private string _error;

        public string Error
        {
            get => _error;
            set
            {
                if (value != null)
                    _error = value;
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        NotEmptyFieldValidationRule.Validate(Name,ref error);
                        break;
                    case "SecondName":
                        NotEmptyFieldValidationRule.Validate(SecondName, ref error);
                        break;
                    case "BirthDate":
                        YearsOldValidationRule.Validate(BirthDate, ref error);
                        NotEmptyFieldValidationRule.Validate(BirthDate, ref error);
                        break;
                    case "City":
                        NotEmptyFieldValidationRule.Validate(City, ref error);
                        break;
                    case "Address":
                        NotEmptyFieldValidationRule.Validate(Address, ref error);
                        break;
                    case "Email":
                        EmailValidationRule.Validate(Email, ref error);
                        NotEmptyFieldValidationRule.Validate(Email, ref error);
                        break;
                    case "Phone":
                        PhoneValidationRule.Validate(Phone, ref error);
                        NotEmptyFieldValidationRule.Validate(Phone, ref error);
                        break;
                    case "IdDocumentName":
                        NotEmptyFieldValidationRule.Validate(IdDocumentName, ref error);
                        break;
                    case "IdNumber":
                        NotEmptyFieldValidationRule.Validate(IdNumber, ref error);
                        break;
                    case "Login":
                        LoginValidationRule.Validate(Login, ref error);
                        NotEmptyFieldValidationRule.Validate(Login, ref error);
                        break;
                    case "Password":
                        SafePasswordValidationRule.Validate(Password, ref error);
                        NotEmptyFieldValidationRule.Validate(Password, ref error);
                        break;
                    case "PasswordRepeated":
                        error = (Password == PasswordRepeated) ? error : "Пароль не совпадает.";
                        NotEmptyFieldValidationRule.Validate(Password, ref error);
                        break;
                }
                if (error == String.Empty)
                    ErrorDictionary.Remove(columnName);
                return error;
            }
        }
        #endregion

    }
}
