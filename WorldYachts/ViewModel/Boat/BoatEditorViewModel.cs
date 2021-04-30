using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Helpers.Validators;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;
using Validation = WorldYachts.Helpers.Validators.Validation;

namespace WorldYachts.ViewModel.Boat
{
    class BoatEditorViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Поля
        private readonly IBoatModel _boatModel;
        private readonly IBoatTypeModel _boatTypeModel;
        private readonly IBoatWoodModel _boatWoodModel;

        private readonly Data.Entities.Boat _boat;
        private List<Data.Entities.BoatType> _boatTypesCollection;
        private List<Data.Entities.BoatWood> _boatWoodsCollection;
        private List<ColorStruct> _colors;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        private readonly bool _isEdit;

        private ColorStruct _selectedColor;
        private DelegateCommand _selectColor;
        #endregion
        
        #region Конструкторы

        public BoatEditorViewModel(IBoatModel boatModel, 
            IBoatTypeModel boatTypeModel,
            IBoatWoodModel boatWoodModel)
        {
            _boatModel = boatModel;
            _boatTypeModel = boatTypeModel;
            _boatWoodModel = boatWoodModel;

            if (!EntityContainer.IsEmpty)
            {
                _boat = EntityContainer.Pop<Data.Entities.Boat>();
                SelectedBoatType = _boat.BoatType;
                SelectedBoatWood = _boat.BoatWood;
                SelectedColor = ColorWorker.GetColorStructFromString(_boat.Color);
                _isEdit = true;
            }
            else
            {
                _boat = new Data.Entities.Boat();
            }
        }

        #endregion

        #region Свойства

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        /// <summary>
        /// Модель яхты
        /// </summary>
        public string Model
        {
            get => _boat.Model;
            set
            {
                _boat.Model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        /// <summary>
        /// Количество гребцов
        /// </summary>
        public string NumberOfRowers
        {
            get => _boat.NumberOfRowers.ToString();
            set
            {
                int.TryParse(value, out int numberOfRower);
                _boat.NumberOfRowers = numberOfRower;
                OnPropertyChanged(nameof(NumberOfRowers));
            }
        }

        /// <summary>
        /// Наличие мачты
        /// </summary>
        public bool Mast
        {
            get => _boat.Mast;
            set
            {
                _boat.Mast = value;
                OnPropertyChanged(nameof(Mast));
            }
        }

        /// <summary>
        /// Цвет
        /// </summary>
        public string Color
        {
            get => _boat.Color;
            set
            {
                _boat.Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        
        /// <summary>
        /// Цена без НДС
        /// </summary>
        public string BasePrice
        {
            get => _boat.BasePrice.ToString(CultureInfo.InvariantCulture);
            set
            {
                decimal.TryParse(value, out decimal basePrice);
                _boat.BasePrice = basePrice;
                OnPropertyChanged(nameof(BasePrice));
            }
        }

        /// <summary>
        /// Процентная ставка НДС
        /// </summary>
        public string Vat
        {
            get => _boat.Vat.ToString(CultureInfo.InvariantCulture);
            set
            {
                double.TryParse(value, out double vat);
                _boat.Vat = vat;
                OnPropertyChanged(nameof(Vat));
            }
        }

        /// <summary>
        /// Доступность кнопки сохранения лодки
        /// </summary>
        public bool SaveButtonIsEnabled => _errors.Count == 0;

        #region Цвет
        private int _selectedColorIndex;

        public int SelectedColorIndex
        {
            get => _selectedColorIndex;
            set
            {
                _selectedColorIndex = value;
                OnPropertyChanged(nameof(SelectedColorIndex));
            }
        }

        public ColorStruct SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                _boat.Color = _selectedColor.Name;
                OnPropertyChanged(nameof(SelectedColor));
            }
        }

        public List<ColorStruct> ColorsCollection => _colors;
        #endregion

        #region Тип лодок

        private int _selectedBoatTypeIndex;

        public int SelectedBoatTypeIndex
        {
            get => _selectedBoatTypeIndex;
            set
            {
                _selectedBoatTypeIndex = value;
                OnPropertyChanged(nameof(SelectedBoatTypeIndex));
            }
        }

        public Data.Entities.BoatType SelectedBoatType
        {
            get => _boat.BoatType;
            set
            {
                _boat.BoatType = value;
                _boat.TypeId = value.Id;
                OnPropertyChanged(nameof(SelectedBoatType));
            }
        }

        public List<Data.Entities.BoatType> BoatTypesCollection => _boatTypesCollection;

        #endregion

        #region Тип дерева

        private int _selectedBoatWoodIndex;

        public int SelectedBoatWoodIndex
        {
            get => _selectedBoatWoodIndex;
            set
            {
                _selectedBoatWoodIndex = value;
                OnPropertyChanged(nameof(SelectedBoatWoodIndex));
            }
        }

        public Data.Entities.BoatWood SelectedBoatWood
        {
            get => _boat.BoatWood;
            set
            {
                _boat.BoatWood = value;
                _boat.WoodId = value.Id;
                OnPropertyChanged(nameof(SelectedBoatWood));
            }
        }

        public List<Data.Entities.BoatWood> BoatWoodsCollection => _boatWoodsCollection;

        #endregion
        
        #endregion

        #region Команды

        /// <summary>
        /// Сохранение выбранного цвета лодки
        /// </summary>
        public DelegateCommand SelectColor
        {
            get { return _selectColor ??= new DelegateCommand(arg => { Color = (string) arg; }); }
        }
        public AsyncRelayCommand LoadedCommand => new AsyncRelayCommand(LoadCollections,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message }); });
        
        public AsyncRelayCommand SaveItem => new AsyncRelayCommand(SaveMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message }); });
        #endregion

        #region Методы
        private async Task LoadCollections(object parameter)
        {
            _boatTypesCollection = (await _boatTypeModel.GetAllAsync()).ToList();
            int boatTypeIndex = _boatTypesCollection.FindIndex(bt => bt.Id == _boat.TypeId);
            if (boatTypeIndex != -1)
            {
                SelectedBoatTypeIndex = boatTypeIndex;
            }
            OnPropertyChanged(nameof(BoatTypesCollection));

            _boatWoodsCollection = (await _boatWoodModel.GetAllAsync()).ToList();
            int boatWoodIndex = _boatWoodsCollection.FindIndex(bw => bw.Id == _boat.WoodId);
            if (boatWoodIndex != -1)
            {
                SelectedBoatWoodIndex = boatWoodIndex;
            }
            OnPropertyChanged(nameof(BoatWoodsCollection));

            _colors = ColorWorker.GetColorsCollection();
            int colorIndex = _colors.FindIndex(c => c.Name == _boat.Color);
            if (colorIndex != -1)
            {
                SelectedColorIndex = colorIndex;
            }
            OnPropertyChanged(nameof(ColorsCollection));
        }
        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            if (_isEdit)
            {
                await _boatModel.UpdateAsync(_boat);
            }
            else
            {
                await _boatModel.AddAsync(_boat);
            }

            ProgressBarVisibility = Visibility.Collapsed;

            string snackbarMessage = _isEdit
                ? $"Лодка \"{Model}\" успешно отредактирована."
                : $"Лодка \"{Model}\" успешно добавлена.";

            SendSnackbar(snackbarMessage);
            CloseCurrentDialog();
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "EditorDialog");
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
                    nameof(Model) => new Validation(new NotEmptyFieldValidationRule(Model)).Validate(),
                    nameof(SelectedBoatType) => new Validation(new NotEmptyFieldValidationRule(SelectedBoatType.Type)).Validate(),
                    nameof(NumberOfRowers) => new Validation(
                        new PositiveNumberValidationRule(NumberOfRowers),
                        new NotEmptyFieldValidationRule(NumberOfRowers)).Validate(),
                    nameof(SelectedBoatWood) => new Validation(new NotEmptyFieldValidationRule(SelectedBoatWood.Wood)).Validate(),
                    nameof(BasePrice) => new Validation(
                        new PositiveNumberValidationRule(_boat.BasePrice),
                        new NumberValidationRule(BasePrice)).Validate(),
                    nameof(Vat) => new Validation(
                        new PositiveNumberValidationRule(_boat.Vat),
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