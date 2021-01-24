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
using WorldYachts.Model;
using WorldYachts.View;

namespace WorldYachts.ViewModel
{
    class LoginViewModel:INotifyPropertyChanged
    {
        private string _login;
        
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

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
                    var password = ((PasswordBox)arg).Password;
                    LoginModel lm = new LoginModel(_login, password);
                    //LoginWindow.CheckLoginResultEvent(lm.Authorization());
                });
            }
        }

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

    }
}
