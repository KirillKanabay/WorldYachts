using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Validators;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;
using Validation = WorldYachts.Helpers.Validators.Validation;

namespace WorldYachts.ViewModel.Boat
{
    class BoatEditorViewModel : BaseEditorViewModel<Data.Entities.Boat>, IDataErrorInfo
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
        private ColorStruct _selectedColor;

        private IEnumerable<string> _boatTypes = new List<string>()
            {"Шлюпка", "Парусная лодка", "Галера"};

        private IEnumerable<string> _woodTypes = new List<string>()
            {"Дуб", "Береза", "Eль", "Cосна", "Лиственница"};

        #endregion

        private DelegateCommand _selectColor;

        #region Конструкторы

        public BoatEditorViewModel() : base(false)
        {
        }

        public BoatEditorViewModel(Data.Entities.Boat boat) : base(true)
        {
            _id = boat.Id;
            _model = boat.Model;
            _type = boat.BoatType.Type;
            _numberOfRower = boat.NumberOfRowers;
            _mast = boat.Mast;
            _color = boat.Color;
            _wood = boat.BoatWood.Wood;
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

        public ColorStruct SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
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
        public override bool SaveButtonIsEnabled => _errors.Count == 0;

        /// <summary>
        /// Получения цветов
        /// </summary>
        public ObservableCollection<ColorStruct> ColorsCollection => ColorWorker.GetColorsCollection();

        public override IDataModel<Data.Entities.Boat> ModelItem => new BoatModel();

        #endregion

        #region Команды

        /// <summary>
        /// Сохранение выбранного цвета лодки
        /// </summary>
        public DelegateCommand SelectColor
        {
            get { return _selectColor ??= new DelegateCommand(arg => { Color = (string) arg; }); }
        }

        protected override Data.Entities.Boat GetSaveItem(bool isEdit)
        {
            return new Data.Entities.Boat()
            {
                Id = (isEdit) ? _id : default,
                Model = _model,
                //BoatType = _type,
                NumberOfRowers = _numberOfRower,
                Mast = _mast,
                Color = _selectedColor.Name,
                //Wood = _wood,
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

        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string error = columnName switch
                {
                    "Model" => new Validation(new NotEmptyFieldValidationRule(Model)).Validate(),
                    "Type" => new Validation(new NotEmptyFieldValidationRule(Type)).Validate(),
                    "NumberOfRower" => new Validation(
                        new PositiveNumberValidationRule(NumberOfRower),
                        new NotEmptyFieldValidationRule(NumberOfRower)).Validate(),
                    "Wood" => new Validation(new NotEmptyFieldValidationRule(Wood)).Validate(),
                    "BasePrice" => new Validation(
                        new PositiveNumberValidationRule(_basePrice),
                        new NumberValidationRule(BasePrice)).Validate(),
                    "Vat" => new Validation(
                        new PositiveNumberValidationRule(_vat),
                        new NotEmptyFieldValidationRule(Vat)).Validate(),
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