using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WorldYachts.Annotations;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.DashboardControlViewModels
{
    class DashboardViewModel:BaseViewModel
    {
        #region Поля
        private IUser _user;

        private string _name;
        private string _secondName;
        private string _typeOfUser;
        #endregion

        #region Конструкторы

        public DashboardViewModel()
        {
            _user = AuthUser.User;
            _name = _user.Name;
            _secondName = _user.SecondName;

            switch (AuthUser.TypeOfUser)
            {
                case TypeOfUser.Admin:
                    _typeOfUser = "Администратор WorldYachts";
                    break;
                case TypeOfUser.Customer:
                    _typeOfUser = "Клиент WorldYachts";
                    break;
                case TypeOfUser.SalesPerson:
                    _typeOfUser = "Менеджер WorldYachts";
                    break;
            }
        }

        #endregion

        #region Свойства

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string SecondName
        {
            get => _secondName;
            set
            {
                _secondName = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }

        public string TypeUser
        {
            get => _typeOfUser;
            set
            {
                _typeOfUser = value;
                OnPropertyChanged(nameof(TypeUser));
            }
        }
        #endregion
    }
}
