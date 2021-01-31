using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    public class PartnerEditorViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Поля

        private readonly int _id;
        private string _name;
        private string _address;
        private string _city;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        /// <summary>
        /// Флаг редактирования партнера
        /// </summary>
        private bool _isEdit;

        private AsyncRelayCommand _saveCommand;

        #endregion

        #region Конструкторы

        public PartnerEditorViewModel(Partner partner)
        {
            _id = partner.Id;
            _name = partner.Name;
            _city = partner.City;
            _address = partner.Address;

            _isEdit = true;
        }

        public PartnerEditorViewModel()
        {
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Название партнера
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Город партнера
        /// </summary>
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        /// <summary>
        /// Адрес партнера
        /// </summary>
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        /// <summary>
        /// Доступность сохранения
        /// </summary>
        public bool SaveButtonIsEnabled => ErrorDictionary.Count == 0;

        /// <summary>
        /// Видимость прогресс бара
        /// </summary>
        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда сохранения партнера
        /// </summary>
        public AsyncRelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? new AsyncRelayCommand(SaveMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message});
                });
            }
        }

        #endregion

        #region Методы
        /// <summary>
        /// Сохранение работы с партнерами
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var partnerModel = new PartnerModel();
            
            var partner = new Partner()
            {
                Name = _name,
                Address = _address,
                City = _city
            };

            try
            {
                if (_isEdit)
                {
                    partner.Id = _id;
                    await Task.Run(() => partnerModel.SaveAsync(partner));
                }
                else
                {
                    await Task.Run((() => partnerModel.AddAsync(partner)));
                }
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }

            var mainWindow = (MainWindow) Application.Current.MainWindow;
            var snackBarMessage = _isEdit
                ? $"Партнер \"{Name}\" успешно отредактирован."
                : $"Партнер \"{Name}\" успешно добавлен";

            mainWindow.SendSnackbar(snackBarMessage);
            //Закрываем диалог редактирования партнера
            mainWindow.DialogHost.CurrentSession.Close();
        }

        /// <summary>
        /// Запуск диалога сообщения
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "MessageDialogRoot", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
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
                    case "Address":
                        new Validation(
                            new NotEmptyFieldValidationRule(Address)).Validate(ref error);
                        break;
                    case "City":
                        new Validation(
                            new NotEmptyFieldValidationRule(City)).Validate(ref error);
                        break;
                }

                ErrorDictionary.Remove(columnName);
                if (error != String.Empty)
                    ErrorDictionary.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }
    }

    #endregion
}