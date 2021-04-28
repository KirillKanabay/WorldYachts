using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Helpers.Validators;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.Services.Authenticate;
using WorldYachts.View;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;
using Validation = WorldYachts.Helpers.Validators.Validation;

namespace WorldYachts.ViewModel
{
    [Serializable]
    internal class SerializableAuthenticateRequest
    {
        internal string Username { get; set; }
        internal string Password { get; set; }
    }

    public class LoginViewModel:BaseViewModel, IDataErrorInfo
    {
        #region Поля
        private SerializableAuthenticateRequest _request;

        private bool _autoSignInCheck;
        private bool _inputIsEnabled = true;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        private AsyncRelayCommand _authorization;
        private DelegateCommand _changeToRegisterWindow;
        private DelegateCommand _openSampleMessageDialog;

        private readonly UserModel _userModel;
        private readonly AuthUser _authUser;
        private MainWindow _mainWindow;
        private readonly IViewModelContainer _viewModelContainer;

        #endregion

        public LoginViewModel(UserModel userModel, AuthUser authUser, IViewModelContainer viewModelContainer)
        {
            _request = new SerializableAuthenticateRequest();
            _userModel = userModel;
            _authUser = authUser;
            _viewModelContainer = viewModelContainer;
        }

        #region Свойства

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Username
        {
            get => _request.Username;
            set
            {
                _request.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password
        {
            get => _request.Password;
            set
            {
                _request.Password = value;
                OnPropertyChanged(nameof(Password));
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

        public bool AutoSignInCheck
        {
            get => _autoSignInCheck;
            set
            {
                _autoSignInCheck = value;
                OnPropertyChanged(nameof(AutoSignInCheck));
            }
        }

        public bool InputIsEnabled
        {
            get => _inputIsEnabled;
            set
            {
                _inputIsEnabled = value;
                OnPropertyChanged(nameof(InputIsEnabled));
            }
        }

        public bool ButtonIsEnabled => _errors.Count == 0;
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
        
        public AsyncRelayCommand LoadedCommand => new AsyncRelayCommand(Deserializable, (ex) =>
        {
            ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
        });

        /// <summary>
        /// Метод авторизации
        /// </summary>
        /// <returns></returns>
        private async Task LoginMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            InputIsEnabled = false;
            try
            {
                var authRequest = new AuthenticateRequest() {Username = _request.Username, Password = _request.Password};
                await _userModel.LoginAsync(authRequest);
                if (_authUser.IsAuthenticated)
                {
                    await Serializable();

                    _mainWindow = new MainWindow(_viewModelContainer.GetViewModel<MainViewModel>());
                    _mainWindow.Show();

                    ((Window)parameter)?.Close();
                }
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
                Username = "";
                Password = "";
                InputIsEnabled = true;
            }

            
        }
        #endregion

        private async Task Deserializable(object parameter)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists("bin"))
            {
                await using (FileStream fs = new FileStream("bin", FileMode.OpenOrCreate))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    _request = (SerializableAuthenticateRequest)formatter.Deserialize(fs);
                }
            }
            if (!string.IsNullOrWhiteSpace(_request.Username) && !string.IsNullOrWhiteSpace(_request.Password))
            {
                AutoSignInCheck = true;
                await LoginMethod(parameter);
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(Password));
            }
        }
        private async Task Serializable()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            await using (FileStream fs = new FileStream("bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, AutoSignInCheck ? _request : new SerializableAuthenticateRequest());
                fs.Seek(0, SeekOrigin.Begin);
            }
        }


        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "DialogRoot");
        }
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
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();
        public string this[string columnName]
        {
            get
            {
                string error = columnName switch
                {
                    nameof(Username) => new Validation(new NotEmptyFieldValidationRule(Username)).Validate(),
                    nameof(Password) => new Validation(new NotEmptyFieldValidationRule(Password)).Validate(),
                    _ => null
                };
                _errors.Remove(columnName);
                if(!string.IsNullOrWhiteSpace(error))
                    _errors.Add(columnName, error);
                OnPropertyChanged(nameof(ButtonIsEnabled));
                return error;
            }
        }

        #endregion
    }
}
