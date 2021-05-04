using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Data.ViewModels;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Helpers.Validators;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Users.SalesPersons
{
    class SalesPersonEditorViewModel : BaseViewModel
    {
        #region Поля

        private readonly ISalesPersonModel _salesPersonModel;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly ITranslitGenerator _translitGenerator;

        private readonly SalesPerson _salesPerson;

        private readonly bool _isEdit;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        #endregion

        #region Конструкторы

        public SalesPersonEditorViewModel(ISalesPersonModel partnerModel, IPasswordGenerator passwordGenerator,
            ITranslitGenerator translitGenerator)
        {
            _salesPersonModel = partnerModel;
            _passwordGenerator = passwordGenerator;
            _translitGenerator = translitGenerator;

            if (!EntityContainer.IsEmpty)
            {
                _salesPerson = EntityContainer.Pop<SalesPerson>();
                _isEdit = true;
            }
            else
            {
                _salesPerson = new SalesPerson();
            }
        }

        #endregion

        #region Свойства

        public string Name
        {
            get => _salesPerson.FirstName;
            set
            {
                _salesPerson.FirstName = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string SecondName
        {
            get => _salesPerson.SecondName;
            set
            {
                _salesPerson.SecondName = value;
                OnPropertyChanged(nameof(SecondName));
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

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
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
            string name = _translitGenerator.Transform(Name ?? "");
            string secondName = _translitGenerator.Transform(SecondName ?? "");

            Login = $"{name}.{secondName}";
        } 

        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            if (_isEdit)
            {
                await _salesPersonModel.UpdateAsync(_salesPerson);
            }
            else
            {
                await _salesPersonModel.AddAsync(new SalesPersonUserViewModel(_salesPerson, _login, _email, _password));
            }

            ProgressBarVisibility = Visibility.Collapsed;

            string snackbarMessage = _isEdit
                ? $"Менеджер \"{Name}\" успешно отредактирован."
                : $"Менеджер \"{Name}\" успешно добавлен.";

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

        #region Валидация полей

        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                var error = columnName switch
                {
                    nameof(Name) => new Validation(new NotEmptyFieldValidationRule(Name)).Validate(),
                    nameof(SecondName) => new Validation(new NotEmptyFieldValidationRule(SecondName)).Validate(),
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