using System;
using WorldYachts.Model;
using WorldYachts.View.Editors;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.CatalogControlViewModels;
using Accessory = WorldYachts.Data.Entities.Accessory;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    public class SelectableAccessoryViewModel:BaseSelectableViewModel<Data.Entities.Accessory>
    {
        private readonly AccessoryModel _accessoryModel;
        public SelectableAccessoryViewModel(Data.Entities.Accessory item, AccessoryModel accessoryModel) 
            : base(item,accessoryModel)
        {
            _accessoryModel = accessoryModel;
        }
        #region Свойства

        public int Id
        {
            get => Item.Id;
            set
            {
                Item.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => Item.Name;
            set
            {
                Item.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => Item.Description;
            set
            {
                Item.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public decimal Price
        {
            get => Item.Price;
            set
            {
                Item.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public double Vat
        {
            get => Item.Vat;
            set
            {
                Item.Vat = value;
                OnPropertyChanged(nameof(Vat));
            }
        }

        public int Inventory
        {
            get => Item.Inventory;
            set
            {
                Item.Inventory = value;
                OnPropertyChanged(nameof(Inventory));
            }
        }

        public int PartnerId
        {
            get => Item.PartnerId;
            set
            {
                Item.PartnerId = value;
                OnPropertyChanged(nameof(PartnerId));
            }
        }

        public decimal PriceInclVat => Price + (Convert.ToDecimal(Vat * 0.01) * Price);
        //public override BaseEditorViewModel<Data.Entities.Accessory> Editor => new AccessoryEditorViewModel();

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
                   $"Инвентарный номер: {Inventory}\n";
        }

        public override BaseEditorViewModel<Accessory> Editor { get; }

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

        protected override BaseViewModel GetEditorViewModel() => new AccessoryEditorViewModel(_item, _accessoryModel);

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
