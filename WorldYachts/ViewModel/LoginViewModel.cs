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
        private DelegateCommand _authorization;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand Authorization
        {
            get
            {
                return _authorization ??= new DelegateCommand( arg =>
                {
                    var password = ((PasswordBox) arg).Password;
                    LoginModel lm = new LoginModel(_login, password);
                    LoginWindow.CheckLoginResultEvent(lm.Authorization());
                });
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
