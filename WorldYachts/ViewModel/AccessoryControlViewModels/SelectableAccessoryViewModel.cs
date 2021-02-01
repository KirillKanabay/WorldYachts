using System;
using System.Collections.Generic;
using System.Printing.IndexedProperties;
using System.Text;
using WorldYachts.Data;
using WorldYachts.View.MessageDialogs;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class SelectableAccessoryViewModel:BaseSelectableViewModel<Accessory>
    {
        #region Поля

        private int _id;
        private string _name;
        private string _description;
        private decimal _price;
        private double _vat;
        private int _inventory;
        private int _orderLevel;
        private int _orderBatch;
        private int _partnerId;

        #endregion

        #region Конструкторы

        public SelectableAccessoryViewModel(Accessory item) : base(item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            Price = item.Price;
            Vat = item.Vat;
            Inventory = item.Inventory;
            OrderLevel = item.OrderLevel;
            OrderBatch = item.OrderBatch;
            PartnerId = item.PartnerId;
        }

        #endregion

        #region Свойства

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public double Vat
        {
            get => _vat;
            set
            {
                _vat = value;
                OnPropertyChanged(nameof(Vat));
            }
        }

        public int Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
                OnPropertyChanged(nameof(Inventory));
            }
        }

        public int OrderLevel
        {
            get => _orderLevel;
            set
            {
                _orderLevel = value;
                OnPropertyChanged(nameof(OrderLevel));
            }
        }

        public int OrderBatch
        {
            get => _orderBatch;
            set
            {
                _orderBatch = value;
                OnPropertyChanged(nameof(OrderBatch));
            }
        }

        public int PartnerId
        {
            get => _partnerId;
            set
            {
                _partnerId = value;
                OnPropertyChanged(nameof(PartnerId));
            }
        }
        
        public override BaseEditorViewModel<Accessory> Editor => new AccessoryEditorViewModel();
        #endregion

        #region Методы

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Название: {Name}\n" +
                   $"Описание: {Description}\n" +
                   $"Базовая стоимость: {Price} ₽\n" +
                   $"НДС: {Vat} %\n" +
                   $"Инвентарный номер: {Inventory}\n" +
                   $"Уровень доставки: {OrderLevel}\n" +
                   $"Партия доставки: {OrderBatch}\n" +
                   $"Партнер";
        }

        protected override void ToggleViewEditorAfterLoaded()
        {
            throw new NotImplementedException();
        }

        protected override BaseViewModel GetEditorViewModel()
        {
            throw new NotImplementedException();
        }

        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
