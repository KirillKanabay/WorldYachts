using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;
using Validation = WorldYachts.Validators.Validation;

namespace WorldYachts.ViewModel.CatalogManagementViewModels
{
    class AddBoatViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Поля

        private string _model;
        private string _type;
        private int _numberOfRower;
        private bool _mast;
        private string _color = "Коричневый";
        private string _wood;
        private decimal _basePrice;
        private double _vat;
        
        private IEnumerable<string> _boatTypes = new List<string>()
            {"Шлюпка", "Парусная лодка", "Галера"};
        private IEnumerable<string> _woodTypes = new List<string>()
            {"Дуб", "Береза", "Eль", "Cосна", "Лиственница"};

        private Visibility _progressBarVisibility = Visibility.Collapsed;
        private bool _successfullAddedBoat;

        private DelegateCommand _selectColor;
        private AsyncRelayCommand _saveBoat;
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
        public int NumberOfRower
        {
            get => _numberOfRower;
            set
            {
                _numberOfRower = value;
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
        public decimal BasePrice
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
        public double Vat
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
        /// Команда регистрации
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

        private async Task SaveBoatMethod(object parameter)
        {
            //Показываем прогрессбар
            ProgressBarVisibility = Visibility.Visible;

            var boatModel = new BoatModel(new Boat()
            {
                Model = this.Model,
                Type = this.Type,
                NumberOfRowers = this.NumberOfRower,
                Mast = this.Mast,
                Color = this.Color,
                Wood = this.Wood,
                Vat = this.Vat,
                BasePrice = this.BasePrice,
            });
            try
            {
                await Task.Run(() => boatModel.AddBoadAsync());
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }

            ExecuteRunDialog(new MessageDialogProperty() { Title = "Добавление лодки", Message = "Добавление лодки прошло успешно" });
            _successfullAddedBoat = true;
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
            // if (_successfullAddedBoat)
            // {
            //     DialogHost.CloseDialogCommand.Execute(this, );
            // }
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
                        new Validation(new PositiveNumberValidationRule(NumberOfRower), 
                            new NotEmptyFieldValidationRule(NumberOfRower)).Validate(ref error);
                        break;
                    case "Wood":
                        new Validation(new NotEmptyFieldValidationRule(Wood)).Validate(ref error);
                        break;
                    case "BasePrice":
                        new Validation(new PositiveNumberValidationRule(BasePrice),
                            new NotEmptyFieldValidationRule(BasePrice)).Validate(ref error);
                        break;
                    case "Vat":
                        new Validation(new PositiveNumberValidationRule(Vat),
                            new NotEmptyFieldValidationRule(Vat)).Validate(ref error);
                        break;
                }
                ErrorDictionary.Remove(columnName);
                if (error != String.Empty)
                    ErrorDictionary.Add(columnName, error);
                OnPropertyChanged("SaveButtonIsEnabled");
                return error;
            }
        }

        #endregion
    }
}
