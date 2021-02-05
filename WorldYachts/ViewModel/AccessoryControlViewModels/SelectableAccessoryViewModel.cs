using System;
using System.Collections.Generic;
using System.Printing.IndexedProperties;
using System.Text;
using WorldYachts.Data;
using WorldYachts.View.Editors;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.CatalogControlViewModels;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    public class SelectableAccessoryViewModel:BaseSelectableViewModel<Accessory>
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

        public decimal PriceInclVat => Price + (Convert.ToDecimal(Vat * 0.01) * Price);
        public override BaseEditorViewModel<Accessory> Editor => new AccessoryEditorViewModel();

        public override bool IsSelected
        {
            get => _isSelected;

            set
            {
                _isSelected = value;
                BoatViewModel.OnAccessoryChanged.Invoke(this);
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public string Info => ToString();

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
            if (AccessoryEditorView.EditorAfterLoad != null)
            {
                AccessoryEditorView.EditorAfterLoad = null;
            }
            else
            {
                AccessoryEditorView.EditorAfterLoad = GetEditorViewModel;
            }
        }

        protected override BaseViewModel GetEditorViewModel() => new AccessoryEditorViewModel(_item);

        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            return new MessageDialogProperty()
            {
                Title = "Подтверждение удаления",
                Message = "Будет удален следующий аксессуар:\n\n" + this
            };
        }

        #endregion

    }
}
