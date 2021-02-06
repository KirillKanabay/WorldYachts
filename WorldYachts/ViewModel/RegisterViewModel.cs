﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using WorldYachts.Annotations;
using WorldYachts.Helpers;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.Validators;
using WorldYachts.View;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;
using IDataErrorInfo = System.ComponentModel.IDataErrorInfo;
using Validation = WorldYachts.Validators.Validation;

namespace WorldYachts.ViewModel
{
    class RegisterViewModel:BaseViewModel, IDataErrorInfo
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
        private Visibility _progressBarVisibility = Visibility.Collapsed;
        private bool _successfulRegistration;

        private AsyncRelayCommand _register;
        private DelegateCommand _changeToLoginWindow;
        private DelegateCommand _changeToMainWindow;
        private DelegateCommand _openSampleMessageDialog;
        #endregion

        #region Свойства

        public Window View { set; get; }

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
        /// <summary>
        /// Видимость прогресс бара
        /// </summary>
        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Команды
        
        /// <summary>
        /// Команда регистрации
        /// </summary>
        public AsyncRelayCommand Register
        {
            get
            {
                return _register ??= new AsyncRelayCommand(RegisterMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }
        /// <summary>
        /// Метод регистрации
        /// </summary>
        /// <param name="parameter">Окно представления</param>
        /// <returns></returns>
        private async Task RegisterMethod(object parameter)
        {
            this.View = (Window) parameter;
            ProgressBarVisibility = Visibility.Visible;
            var registerModel = new RegisterModel()
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
            try
            {
                await Task.Run(() => registerModel.RegisterAsync());
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }
            
            ExecuteRunDialog(new MessageDialogProperty() { Title = "Регистрация", Message = "Регистрация прошла успешно." });
            _successfulRegistration = true;
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
        /// <summary>
        /// Переключение на главное окно
        /// </summary>
        public DelegateCommand ChangeToMainWindow
        {
            get
            {
                return _changeToMainWindow ??= new DelegateCommand((arg) =>
                {
                    var window = (Window) arg;
                    MainWindow.ShowWindow();
                    window.Close();
                });
            }
        }
        #region MessageDialog
        /// <summary>
        /// Открытие messageDialog
        /// </summary>
        public DelegateCommand OpenSampleMessageDialog
        {
            get
            {
                return _openSampleMessageDialog ??= new DelegateCommand(ExecuteRunDialog);
            }
        }
        /// <summary>
        /// Запуск диалога сообщения
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }
        /// <summary>
        /// При закрытии сообщения открываем главное окно при успешной регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if(_successfulRegistration)
                ChangeToMainWindow?.Execute(View);
        }

        #endregion
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
                        new Validation(new NotEmptyFieldValidationRule(Name))
                            .Validate(ref error);
                        break;
                    case "SecondName":
                        new Validation(new NotEmptyFieldValidationRule(SecondName))
                            .Validate(ref error);
                        break;
                    case "BirthDate":
                        new Validation(new YearsOldValidationRule(BirthDate), new NotEmptyFieldValidationRule(BirthDate))
                            .Validate(ref error);
                        break;
                    case "City":
                        new Validation(new NotEmptyFieldValidationRule(City))
                            .Validate(ref error);
                        break;
                    case "Address":
                        new Validation(new NotEmptyFieldValidationRule(Address))
                            .Validate(ref error);
                        break;
                    case "Email":
                        new Validation(new EmailValidationRule(Email), new NotEmptyFieldValidationRule(Email))
                            .Validate(ref error);
                        break;
                    case "Phone":
                        new Validation(new PhoneValidationRule(Phone), new NotEmptyFieldValidationRule(Phone)).Validate(ref error);
                        break;
                    case "IdDocumentName":
                        new Validation(new NotEmptyFieldValidationRule(IdDocumentName)).Validate(ref error);
                        break;
                    case "IdNumber":
                        new Validation(new NotEmptyFieldValidationRule(IdNumber)).Validate(ref error);
                        break;
                    case "Login":
                        new Validation(new LoginValidationRule(Login),new NotEmptyFieldValidationRule(Login)).Validate(ref error);
                        break;
                    case "Password":
                        new Validation(new SafePasswordValidationRule(Password), new NotEmptyFieldValidationRule(Password)).Validate(ref error);
                        break;
                    case "PasswordRepeated":
                        error = (Password == PasswordRepeated) ? error : "Пароль не совпадает.";
                        new Validation(new NotEmptyFieldValidationRule(PasswordRepeated)).Validate(ref error);
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
        
    }
}
