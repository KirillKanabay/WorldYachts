using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;
using Validation = WorldYachts.Validators.Validation;

namespace WorldYachts.ViewModel.BoatManagementViewModels
{
    class BoatEditorViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Поля

        private readonly int _id;
        private string _model;
        private string _type;
        private int _numberOfRower;
        private bool _mast;
        private string _color = "Коричневый";
        private string _wood;
        private string _basePrice;
        private string _vat;

        private IEnumerable<string> _boatTypes = new List<string>()
            {"Шлюпка", "Парусная лодка", "Галера"};
        private IEnumerable<string> _woodTypes = new List<string>()
            {"Дуб", "Береза", "Eль", "Cосна", "Лиственница"};


        private Visibility _progressBarVisibility = Visibility.Collapsed;
        
        //Флаг редактирования лодки
        private bool _isEdit;

        private DelegateCommand _selectColor;
        private AsyncRelayCommand _saveBoat;
        #endregion

        #region Конструкторы

        public BoatEditorViewModel()
        {
           
        }

        public BoatEditorViewModel(Boat boat)
        {
            _id = boat.Id;
            _model = boat.Model;
            _type = boat.Type;
            _numberOfRower = boat.NumberOfRowers;
            _mast = boat.Mast;
            _color = boat.Color;
            _wood = boat.Wood;
            _basePrice = boat.BasePrice.ToString();
            _vat = boat.Vat.ToString();

            //Устанавливаем флаг редактирования
            _isEdit = true;
        }

        #endregion

        #region Свойства
        /// <summary>
        /// Модель яхты
        /// </summary>
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }
        /// <summary>
        /// Тип яхты
        /// </summary>
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        /// <summary>
        /// Количество гребцов
        /// </summary>
        public string NumberOfRower
        {
            get => _numberOfRower.ToString();
            set
            {
                int.TryParse(value, out _numberOfRower);
                OnPropertyChanged(nameof(NumberOfRower));
            }
        }
        /// <summary>
        /// Наличие мачты
        /// </summary>
        public bool Mast
        {
            get => _mast;
            set
            {
                _mast = value;
                OnPropertyChanged(nameof(Mast));
            }
        }
        /// <summary>
        /// Цвет
        /// </summary>
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        /// <summary>
        /// Тип дерева
        /// </summary>
        public string Wood
        {
            get => _wood;
            set
            {
                _wood = value;
                OnPropertyChanged(nameof(Wood));
            }
        }
        /// <summary>
        /// Цена без НДС
        /// </summary>
        public string BasePrice
        {
            get => _basePrice;
            set
            {
                _basePrice = value;
                OnPropertyChanged(nameof(BasePrice));
            }
        }
        /// <summary>
        /// Процентная ставка НДС
        /// </summary>
        public string Vat
        {
            get => _vat;
            set
            {
                _vat = value;
                OnPropertyChanged(nameof(Vat));
            }
        }
        
        /// <summary>
        /// Типы лодок
        /// </summary>
        public IEnumerable<string> BoatTypes
        {
            get => _boatTypes;
            set
            {
                _boatTypes = value;
                OnPropertyChanged(nameof(BoatTypes));
            }
        }
        /// <summary>
        /// Типы дерева
        /// </summary>
        public IEnumerable<string> WoodTypes
        {
            get => _woodTypes;
            set
            {
                _woodTypes = value;
                OnPropertyChanged(nameof(WoodTypes));
            }
        }
        /// <summary>
        /// Доступность кнопки сохранения лодки
        /// </summary>
        public bool SaveButtonIsEnabled => ErrorDictionary.Count == 0;

        /// <summary>
        /// Получения цветов
        /// </summary>
        public ObservableCollection<ColorStruct> ColorsCollection => ColorWorker.GetColorsCollection();
        

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
        /// Сохранение выбранного цвета лодки
        /// </summary>
        public DelegateCommand SelectColor
        {
            get
            {
                return _selectColor ??= new DelegateCommand(arg =>
                {
                    Color = (string)arg;
                });
            }
        }

        /// <summary>
        /// Команда сохранения лодки
        /// </summary>
        public AsyncRelayCommand SaveBoat
        {
            get
            {
                return _saveBoat ??= new AsyncRelayCommand(SaveBoatMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }
        /// <summary>
        /// Сохранение работы с лодками
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task SaveBoatMethod(object parameter)
        {
            //Показываем прогрессбар
            ProgressBarVisibility = Visibility.Visible;
            var boatModel = new BoatModel();
            var boat = new Boat()
            {
                Model = _model,
                Type = _type,
                NumberOfRowers = _numberOfRower,
                Mast = _mast,
                Color = _color,
                Wood = _wood,
                Vat = double.Parse(_vat),
                BasePrice = decimal.Parse(_basePrice),
            };
            try
            {
                if (_isEdit)
                {
                    boat.Id = _id;
                    await Task.Run(() => boatModel.SaveAsync(boat));
                }
                else
                {
                    await Task.Run(() => boatModel.AddAsync(boat));
                }
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }
            
            //ExecuteRunDialog(new MessageDialogProperty() { Title = "Добавление лодки", Message = "Добавление лодки прошло успешно" });
            
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var snackBarMessage = _isEdit
                ? $"Лодка \"{Model}\" успешно отредактирована."
                : $"Лодка \"{Model}\" успешно добавлена.";
            
            mainWindow.SendSnackbar(snackBarMessage);
            //Закрываем диалог редактирования лодки
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
        /// <summary>
        /// При закрытии сообщения открываем главное окно при успешной регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            
        }
        #endregion
        
        #region Валидация полей

        public string Error { get; }

        private Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Model":
                        new Validation(new NotEmptyFieldValidationRule(Model)).Validate(ref error);
                        break;
                    case "Type":
                        new Validation(new NotEmptyFieldValidationRule(Type)).Validate(ref error);
                        break;
                    case "NumberOfRower":
                        new Validation(
                            new PositiveNumberValidationRule(NumberOfRower),
                            new NotEmptyFieldValidationRule(NumberOfRower)).Validate(ref error);
                        break;
                    case "Wood":
                        new Validation(new NotEmptyFieldValidationRule(Wood)).Validate(ref error);
                        break;
                    case "BasePrice":
                        new Validation(
                            new PositiveNumberValidationRule(_basePrice),
                            new NumberValidationRule(BasePrice)).Validate(ref error);
                        break;
                    case "Vat":
                        new Validation(
                            new PositiveNumberValidationRule(_vat),
                            new NotEmptyFieldValidationRule(Vat)).Validate(ref error);
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
