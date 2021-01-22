using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WorldYachts.Annotations;
using WorldYachts.Data;
using WorldYachts.Model;

namespace WorldYachts.ViewModel
{
    class DashboardViewModel:INotifyPropertyChanged
    {
        private IUser _user = AuthUser.User;
        private string _secondName;

        public string Name => _user.Name;
        public string SecondName => _user.SecondName;

        public DashboardViewModel()
        {
            //_user = AuthUser.User;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
