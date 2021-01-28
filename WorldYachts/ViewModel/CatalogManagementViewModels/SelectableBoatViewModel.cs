using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;

namespace WorldYachts.ViewModel.CatalogManagementViewModels
{
    class SelectableBoatViewModel:BaseViewModel
    {
        #region Поля
        private int _id;
        private string _model;
        private string _type;
        private int _numberOfRower;
        private string _mast;
        private string _color;
        private string _wood;
        private string _basePrice;
        private string _vat;

        private bool _isSelected;
        #endregion

        #region Конструкторы
        public SelectableBoatViewModel(Boat boat)
        {
            Id = boat.Id;
            Model = boat.Model;
            Type = boat.Type;
            NumberOfRower = boat.NumberOfRowers;
            Mast = boat.Mast ? "Присутствует" : "Отсутствует";
            Color = boat.Color;
            Wood = boat.Wood;
            BasePrice = $"{boat.BasePrice} ₽";
            Vat = $"{boat.Vat}%";

            IsSelected = false;
        }
        #endregion


        #region Свойства
        /// <summary>
        /// Идентификатор лодки
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        /// <summary>
        /// Выбрана ли лодка
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(_isSelected));
            }
        }
        /// <summary>
        /// Модель лодки
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
        /// Тип лодки
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
        public string Mast
        {
            get => _mast;
            set
            {
                _mast = value;
                OnPropertyChanged(nameof(_mast));
            }
        }
        /// <summary>
        /// Цвет лодки
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
        /// Базовая цена без НДС
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

        #endregion
    }
}
