﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Helpers;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.Validators;
using WorldYachts.View;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.DashboardControlViewModels;
using WorldYachts.ViewModel.MessageDialog;
using Validation = WorldYachts.Validators.Validation;

namespace WorldYachts.ViewModel
{
    public class LoginViewModel:BaseViewModel, IDataErrorInfo
    {
        #region Поля

        private string _username;
        private string _password;

        private bool _signOutCheck;
        private bool _inputIsEnabled = true;
        private Visibility _progressBarVisibility = Visibility.Collapsed;

        private AsyncRelayCommand _authorization;
        private DelegateCommand _changeToRegisterWindow;
        private DelegateCommand _openSampleMessageDialog;

        private readonly UserModel _userModel;
        #endregion

        public LoginViewModel(UserModel userModel)
        {
            _userModel = userModel;
        }

        #region Свойства

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
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
                OnPropertyChanged();
            }
        }

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

        public bool SignOutCheck
        {
            get => _signOutCheck;
            set
            {
                _signOutCheck = value;
                OnPropertyChanged(nameof(SignOutCheck));
            }
        }

        public bool InputIsEnabled
        {
            get => _inputIsEnabled;
            set
            {
                _inputIsEnabled = value;
                OnPropertyChanged(nameof(InputIsEnabled));
            }
        }

        public bool ButtonIsEnabled => ErrorDictionary.Count == 0;
        #endregion

        #region Команды

        #region Авторизация
        /// <summary>
        /// Команда авторизации
        /// </summary>
        public AsyncRelayCommand Authorization
        {
            get
            {
                return _authorization ??= new AsyncRelayCommand(LoginMethod,(ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }

        /// <summary>
        /// Метод авторизации
        /// </summary>
        /// <returns></returns>
        private async Task LoginMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            InputIsEnabled = false;
            try
            {
                await Task.Run(() => _userModel.LoginAsync(_username, _password));
                if (AuthUser.GetInstance().IsAuthenticated)
                {
                    new MainWindow().Show();
                    ((Window)parameter).Close();
                }
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
                Username = "";
                Password = "";
                InputIsEnabled = true;
            }

            
        }

        //private async Task LoginFromFileMethod(object parameter)
        //{
        //    ProgressBarVisibility = Visibility.Visible;
        //    InputIsEnabled = false;
        //    var um = new UserModel();
        //    try
        //    {
        //        await Task.Run(() => um.LoginAsync(((SerializableUserInfo)parameter).Login, ((SerializableUserInfo)parameter).Password));
        //    }
        //    finally
        //    {
        //        ProgressBarVisibility = Visibility.Collapsed;
        //        Login = "";
        //        Password = "";
        //        SignOutCheck = false;
        //        InputIsEnabled = true;
        //    }

        //    if (AuthUser.User != null)
        //    {
        //        MainWindow.ShowWindow();
        //        LoginWindow.CloseWindow?.Invoke();
        //    }
        //}
        #endregion

        //private SerializableUserInfo? Deserializable()
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    SerializableUserInfo ui = new SerializableUserInfo(_login,_password);
        //    if (File.Exists("bin"))
        //    {
        //        using (FileStream fs = new FileStream("bin", FileMode.OpenOrCreate))
        //        {
        //            fs.Seek(0, SeekOrigin.Begin);
        //            ui = (SerializableUserInfo)formatter.Deserialize(fs);
        //        }

        //        return ui;
        //    }

        //    return null;
        //}

        //private void Serializable()
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    using (FileStream fs = new FileStream("bin",FileMode.OpenOrCreate))
        //    {
        //        formatter.Serialize(fs,new SerializableUserInfo(_login, _password));
        //        fs.Seek(0, SeekOrigin.Begin);
        //    }
        //}

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
        private async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty) o)
            };
            var result = await DialogHost.Show(view, "RootDialogLogin", ClosingEventHandler);
        }
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here."); //TODO: отловить ошибку
        }

        #endregion

        /// <summary>
        /// Переключение на форму регистрации
        /// </summary>
        public DelegateCommand ChangeToRegisterWindow
        {
            get
            {
                return _changeToRegisterWindow ??= new DelegateCommand(arg =>
                {
                    var loginWindow = (Window)arg;
                    RegisterWindow.ShowWindow();
                    loginWindow.Close();
                });
            }
        }
        #endregion

        #region Валидация полей
        public string Error { get; }
        public Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Login":
                        new Validation(new NotEmptyFieldValidationRule(Username)).Validate(ref error);
                        break;
                    case "Password":
                        new Validation(new NotEmptyFieldValidationRule(Password)).Validate(ref error);
                        break;
                }
                ErrorDictionary.Remove(columnName);
                if(error != String.Empty)
                    ErrorDictionary.Add(columnName, error);
                OnPropertyChanged("ButtonIsEnabled");
                return error;
            }
        }

        #endregion
    }
}
