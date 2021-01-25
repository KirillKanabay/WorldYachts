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
using WorldYachts.Annotations;
using WorldYachts.Helpers;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.Validators;
using WorldYachts.View;
using Validation = WorldYachts.Validators.Validation;

namespace WorldYachts.ViewModel
{
    public class LoginViewModel:INotifyPropertyChanged, IDataErrorInfo
    {
        #region Поля
        private string _login;
        private string _password;
        private string _statusMessage = "Все ок пока!";
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
        #endregion

        #region Команды

        private AsyncRelayCommand _authorization;
        private DelegateCommand _changeToRegisterWindow;
        /// <summary>
        /// Команда авторизации
        /// </summary>
        public AsyncRelayCommand Authorization
        {
            get
            {
                return _authorization ??= new AsyncRelayCommand(LoginMethod,(ex)=>StatusMessage = ex.Message);
            }
        }

        private async Task LoginMethod()
        {
            StatusMessage = "Начинаю вход";
            var loginModel = new LoginModel(_login, _password);
            
            await Task.Run(() => loginModel.LoginAsync());
            
            if (AuthUser.User != null)
            {
                StatusMessage = "Вход прошел успешно";
            }
        }
        /// <summary>
        /// Переключение на форму регистрации
        /// </summary>
        public DelegateCommand ChangeToRegisterWindow
        {
            get
            {
                return _changeToRegisterWindow ??= new DelegateCommand(arg =>
                {
                    var loginWindow = (Window) arg;
                    RegisterWindow.ShowWindow();
                    loginWindow.Close();
                });
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                return error;
            }
        }

        #endregion

    }
}
