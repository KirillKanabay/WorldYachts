﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Infrastructure;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.UserControlViewModels
{
    class SalesPersonEditorViewModel : BaseEditorViewModel<SalesPerson>, IDataErrorInfo
    {
        #region Поля

        private readonly int _id;
        private string _name;
        private string _secondName;
        private string _login;
        private string _password;
        private User _user;

        #endregion

        #region Конструкторы

        public SalesPersonEditorViewModel(SalesPerson item) : base(true)
        {
            _id = item.Id;
            _name = item.FirstName;
            _secondName = item.SecondName;
            _user = new UserModel().Load()
                .FirstOrDefault(u => u.TypeUser == (int) TypeOfUser.SalesPerson && u.UserId == item.Id);
            
            _password = _user.Password;
            _login = _user.Login;
        }

        public SalesPersonEditorViewModel() : base(false)
        {
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

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        #endregion

        public override bool SaveButtonIsEnabled => !ErrorDictionary.Any();
        public override IDataModel<SalesPerson> ModelItem => new SalesPersonModel();

        #region Методы

        protected override async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;

            var um = new UserModel();
            var spm = new SalesPersonModel();

            try
            {
                if (_isEdit)
                {
                    await spm.SaveAsync(GetSaveItem(_isEdit));
                    await um.SaveAsync(new User()
                    {
                        Id = _user.Id,
                        TypeUser = (int) TypeOfUser.SalesPerson,
                        Login = Login,
                        Password = Password,
                        UserId = _user.UserId
                    });
                }
                else
                {
                    await Task.Run((() => um.AddSalesPersonAsync(GetSaveItem(_isEdit), Login, Password)));
                }
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }

            MainWindow.SendSnackbarAction?.Invoke(GetSaveSnackbarMessage(_isEdit));

            //Закрываем диалог редактирования партнера
            MainWindow.GetMainWindow?.Invoke().DialogHost.CurrentSession.Close();
        }

        protected override SalesPerson GetSaveItem(bool isEdit)
        {
            return new SalesPerson()
            {
                Id = (isEdit) ? _id : default,
                FirstName = _name,
                SecondName = _secondName
            };
        }

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return _isEdit
                ? $"Менеджер \"{Name}\" успешно отредактирован."
                : $"Менеджер \"{Name}\" успешно добавлен.";
        }

        #endregion

        #region Валидация полей

        private readonly Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        new Validation(
                            new NotEmptyFieldValidationRule(Name)).Validate(ref error);
                        break;
                    case "SecondName":
                        new Validation(
                            new NotEmptyFieldValidationRule(SecondName)).Validate(ref error);
                        break;
                    case "Password":
                        new Validation(new SafePasswordValidationRule(Password),
                            new NotEmptyFieldValidationRule(Password)).Validate(ref error);
                        break;
                    case "Login":
                        new Validation(
                            new LoginValidationRule(Login),
                            new NotEmptyFieldValidationRule(Login)).Validate(ref error);
                        break;
                }

                ErrorDictionary.Remove(columnName);
                if (error != String.Empty)
                    ErrorDictionary.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }

        #endregion
    }
}