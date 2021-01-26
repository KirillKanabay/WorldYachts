using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.ViewModel.CatalogManagementViewModels
{
    class AddBoatViewModel : BaseViewModel
    {
        #region Поля

        private string _name;
        private string _boatType;
        private int _numberOfRower;
        private bool _mast;
        private string _color;
        private string _wood;
        private decimal _basePrice;
        private double _vat;

        private IEnumerable<string> _boatTypes = new List<string>()
            {"Шлюпка", "Парусная лодка", "Галера"};
        private IEnumerable<string> _woodTypes = new List<string>()
            {"Дуб", "Береза", "Eль", "Cосна", "Лиственница"};
        #endregion

        #region Свойства
        /// <summary>
        /// Модель яхты
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Model));
            }
        }
        /// <summary>
        /// Тип яхты
        /// </summary>
        public string BoatType
        {
            get => _boatType;
            set
            {
                _boatType = value;
                OnPropertyChanged(nameof(BoatType));
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
        #endregion


    }
}
