using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WorldYachts.Annotations;
using WorldYachts.Helpers;
using WorldYachts.Data;
using WorldYachts.Validators;
using WorldYachts.View;

namespace WorldYachts.ViewModel
{
    class LoginViewModel:INotifyPropertyChanged, IDataErrorInfo
    {
        #region Поля
        private string _login;
        private string _password;
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
        #endregion

        #region Команды

        private DelegateCommand _authorization;
        private DelegateCommand _changeToRegisterWindow;
        /// <summary>
        /// Команда авторизации
        /// </summary>
        public DelegateCommand Authorization
        {
            get
            {
                return _authorization ??= new DelegateCommand(arg =>
                {
                    LoginModel lm = new LoginModel(_login, _password);

                    //LoginWindow.CheckLoginResultEvent(lm.Authorization());
                });
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
                        NotEmptyFieldValidationRule.Validate(Login,ref error);
                        break;
                    case "Password":
                        NotEmptyFieldValidationRule.Validate(Password, ref error);
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
