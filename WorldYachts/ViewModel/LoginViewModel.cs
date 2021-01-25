using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using WorldYachts.Annotations;
using WorldYachts.Helpers;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.Validators;
using WorldYachts.View;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;
using Validation = WorldYachts.Validators.Validation;

namespace WorldYachts.ViewModel
{
    public class LoginViewModel:BaseViewModel, IDataErrorInfo
    {
        #region Поля
        private string _login;
        private string _password;
        private string _statusMessage = "Все ок пока!";
        private Visibility _progressBarVisibility = Visibility.Collapsed;

        private AsyncRelayCommand _authorization;
        private DelegateCommand _changeToRegisterWindow;
        private DelegateCommand _openSampleMessageDialog;
        #endregion

        #region Свойства

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
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Статус входа
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
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
            var loginModel = new LoginModel(_login, _password);
            try
            {
                await Task.Run(() => loginModel.LoginAsync());
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
                Login = "";
                Password = "";
            }

            if (AuthUser.User != null)
            {
                //TODO:Переход на главную форму
            }
        }

        #endregion
        
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
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
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
                        new Validation(new NotEmptyFieldValidationRule(Login)).Validate(ref error);
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
