using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WorldYachts.Annotations;
using WorldYachts.Helpers;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.View;
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
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
                OnPropertyChanged("ButtonIsEnabled");
            }
        }

        public bool ButtonIsEnabled => ErrorDictionary.Count == 0;
        
        #endregion

        #region Команды
        private DelegateCommand _register;
        private DelegateCommand _changeToLoginWindow;

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
        /// <summary>
        /// Команда переключения на окно логина
        /// </summary>
        public DelegateCommand ChangeToLoginWindow
        {
            get { return _changeToLoginWindow ??= new DelegateCommand((arg) =>
            {
                var window = (Window) arg;
                LoginWindow.ShowWindow();
                window.Close();
            }); }

        }

        #endregion
        
        #region Валидация полей
        private Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();
        
        public string Error { get; }
        /// <summary>
        /// Валидация полей окна
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
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
                ErrorDictionary.Remove(columnName);
                if(error != String.Empty)
                    ErrorDictionary.Add(columnName,error);
                OnPropertyChanged("ButtonIsEnabled");
                return error;
            }
        }
        #endregion
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
