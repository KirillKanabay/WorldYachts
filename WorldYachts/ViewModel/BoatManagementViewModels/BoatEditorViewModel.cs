using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;
using Validation = WorldYachts.Validators.Validation;

namespace WorldYachts.ViewModel.BoatManagementViewModels
{
    class BoatEditorViewModel : BaseEditorViewModel<Boat>, IDataErrorInfo
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
        #endregion

        private DelegateCommand _selectColor;

        #region Конструкторы

        public BoatEditorViewModel():base(false)
        {
           
        }

        public BoatEditorViewModel(Boat boat):base(true)
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
        public override bool SaveButtonIsEnabled => ErrorDictionary.Count == 0;

        /// <summary>
        /// Получения цветов
        /// </summary>
        public ObservableCollection<ColorStruct> ColorsCollection => ColorWorker.GetColorsCollection();
        
        public override IDataModel<Boat> ModelItem => new BoatModel();

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
        
        protected override Boat GetSaveItem(bool isEdit)
        {
            return new Boat()
            {
                Id = (isEdit) ? _id : default,
                Model = _model,
                Type = _type,
                NumberOfRowers = _numberOfRower,
                Mast = _mast,
                Color = _color,
                Wood = _wood,
                BasePrice = Decimal.Parse(_basePrice),
                Vat = double.Parse(_vat),
            };
        }

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return _isEdit
                ? $"Лодка \"{Model}\" успешно отредактирована."
                : $"Лодка \"{Model}\" успешно добавлена.";
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
