using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.Data.ViewModels;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Helpers.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Users.Customers
{
    public class CustomerEditorViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Поля

        private readonly ICustomerModel _customerModel;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly ITranslitGenerator _translitGenerator;

        private readonly Customer _customer;

        private readonly bool _isEdit;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        #endregion

        #region Конструкторы

        public CustomerEditorViewModel(ICustomerModel customerModel, IPasswordGenerator passwordGenerator,
            ITranslitGenerator translitGenerator)
        {
            _customerModel = customerModel;
            _passwordGenerator = passwordGenerator;
            _translitGenerator = translitGenerator;

            if (!EntityContainer.IsEmpty)
            {
                _customer = EntityContainer.Pop<Customer>();
                _isEdit = true;
            }
            else
            {
                _customer = new Customer();
            }
        }

        #endregion

        #region Свойства

        public string FirstName
        {
            get => _customer.FirstName;
            set
            {
                _customer.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string SecondName
        {
            get => _customer.SecondName;
            set
            {
                _customer.SecondName = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }

        public DateTime BirthDate
        {
            get => _customer.BirthDate;
            set
            {
                _customer.BirthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public string Address
        {
            get => _customer.Address;
            set
            {
                _customer.Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string City
        {
            get => _customer.City;
            set
            {
                _customer.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Phone
        {
            get => _customer.Phone;
            set
            {
                _customer.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string OrganizationName
        {
            get => _customer.OrganizationName;
            set
            {
                _customer.OrganizationName = value;
                OnPropertyChanged(nameof(OrganizationName));
            }
        }

        public string IdNumber
        {
            get => _customer.IdNumber;
            set
            {
                _customer.IdNumber = value;
                OnPropertyChanged(nameof(IdNumber));
            }
        }

        public string IdDocumentName
        {
            get => _customer.IdDocumentName;
            set
            {
                _customer.IdDocumentName = value;
                OnPropertyChanged(nameof(IdDocumentName));
            }
        }

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        private string _login;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public bool SaveButtonIsEnabled => !_errors.Any();
        public Visibility UserPropsVisibility => _isEdit ? Visibility.Collapsed : Visibility.Visible;

        #endregion

        #region Команды

        public AsyncRelayCommand SaveItem => new AsyncRelayCommand(SaveMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message}); });

        public DelegateCommand GeneratePassword => new DelegateCommand(GeneratePasswordMethod);

        public DelegateCommand TranslitLogin => new DelegateCommand(TranslitLoginMethod);

        #endregion

        #region Методы

        private void GeneratePasswordMethod(object parameter)
        {
            Password = _passwordGenerator.Generate();
        }

        private void TranslitLoginMethod(object parameter)
        {
            string name = _translitGenerator.Transform(FirstName ?? "");
            string secondName = _translitGenerator.Transform(SecondName ?? "");

            Login = $"{name}.{secondName}";
        }

        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            if (_isEdit)
            {
                await _customerModel.UpdateAsync(_customer);
            }
            else
            {
                await _customerModel.AddAsync(new CustomerUserViewModel(_customer, _login, _email, _password));
            }

            ProgressBarVisibility = Visibility.Collapsed;

            string snackbarMessage = _isEdit
                ? $"Клиент \"{FirstName} {SecondName}\" успешно отредактирован."
                : $"Менеджер \"{FirstName} {SecondName}\" успешно добавлен.";

            SendSnackbar(snackbarMessage);
            CloseCurrentDialog();
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty) o)
            };
            var result = await DialogHost.Show(view, "EditorDialog");
        }

        #endregion

        #region Валидация

        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                var error = columnName switch
                {
                    nameof(FirstName) => new Validation(new NotEmptyFieldValidationRule(FirstName)).Validate(),
                    nameof(SecondName) => new Validation(new NotEmptyFieldValidationRule(SecondName)).Validate(),
                    nameof(Address) => new Validation(new NotEmptyFieldValidationRule(Address)).Validate(),
                    nameof(City) => new Validation(new NotEmptyFieldValidationRule(City)).Validate(),
                    nameof(Phone) => new Validation(new PhoneValidationRule(Phone),
                        new NotEmptyFieldValidationRule(City)).Validate(),
                    nameof(OrganizationName) => new Validation(new NotEmptyFieldValidationRule(OrganizationName))
                        .Validate(),
                    nameof(IdNumber) => new Validation(new NotEmptyFieldValidationRule(IdNumber)).Validate(),
                    nameof(IdDocumentName) => new Validation(new NotEmptyFieldValidationRule(IdDocumentName))
                        .Validate(),
                    nameof(BirthDate) => new Validation(new YearsOldValidationRule(BirthDate),
                        new NotEmptyFieldValidationRule(BirthDate)).Validate(),
                    nameof(Password) => _isEdit
                        ? null
                        : new Validation(new SafePasswordValidationRule(Password),
                            new NotEmptyFieldValidationRule(Password)).Validate(),
                    nameof(Login) => _isEdit
                        ? null
                        : new Validation(new LoginValidationRule(Login), new NotEmptyFieldValidationRule(Login))
                            .Validate(),
                    nameof(Email) => _isEdit
                        ? null
                        : new Validation(new EmailValidationRule(Email), new NotEmptyFieldValidationRule(Email))
                            .Validate(),
                    _ => null
                };

                _errors.Remove(columnName);
                if (!string.IsNullOrWhiteSpace(error))
                    _errors.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }

        #endregion
    }
}