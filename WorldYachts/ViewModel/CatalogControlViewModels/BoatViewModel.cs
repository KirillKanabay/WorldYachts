using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Infrastructure;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.Validators;
using WorldYachts.ViewModel.AccessoryControlViewModels;
using WorldYachts.ViewModel.BaseViewModels;
using AccessoryToBoat = WorldYachts.Data.AccessoryToBoat;
using Order = WorldYachts.Data.Order;

namespace WorldYachts.ViewModel.CatalogControlViewModels
{
    public class BoatViewModel:BaseEditorViewModel<Order>,IDataErrorInfo
    {
        #region Поля

        private int _id;
        private string _model;
        private string _type;
        private string _wood;
        private bool _mast;
        private string _color;
        private int _numberOfRowers;
        private List<Data.Entities.AccessoryToBoat> _accessoryToBoats;
        private ObservableCollection<SelectableAccessoryViewModel> _accessories;
        private decimal _price;
        private double _vat;
        private string _deliveryAddress;
        private string _deliveryCity;

        private AsyncRelayCommand _addOrder;

        public static Action<object> OnAccessoryChanged;

        private OrderModel _orderModel;
        private readonly AuthUser _authUser;
        #endregion

        #region Конструкторы

        public BoatViewModel(AuthUser authUser):base(false)
        {
            _authUser = authUser;
        }
        public BoatViewModel(Boat boat, AuthUser authUser):base(false)
        {
            _id = boat.Id;
            _model = boat.Model;
            _type = boat.BoatType.Type;
            _wood = boat.BoatWood.Wood;
            _mast = boat.Mast;
            _color = boat.Color;
            _numberOfRowers = boat.NumberOfRowers;
            _accessoryToBoats = boat.AccessoryToBoat.ToList();
            _price = boat.BasePrice;
            _vat = boat.Vat;

            _orderModel = new OrderModel();

            _authUser = authUser;

            OnAccessoryChanged += (arg) =>
            {
                var acc = (SelectableAccessoryViewModel) arg;
                if (_accessories != null)
                {
                    if(_accessories.FirstOrDefault(a => a.Item.Id == acc.Item.Id).IsSelected != acc.IsSelected)
                        _accessories.FirstOrDefault(a => a.Item.Id == acc.Item.Id).IsSelected = acc.IsSelected;
                }
                    
                OnPropertyChanged(nameof(AccessoriesPrice));
                OnPropertyChanged(nameof(FinishPrice));
            };
        }

       
        #endregion

        #region Свойства
        
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public string Mast => _mast ? "Присутствует" : "Отсутствует";

        public SolidColorBrush Color => new SolidColorBrush(ColorWorker.GetColorFromString(_color??"#000000"));

        public int NumberOfRowers
        {
            get => _numberOfRowers;
            set
            {
                _numberOfRowers = value;
                OnPropertyChanged(nameof(NumberOfRowers));
            }
        }

        public ObservableCollection<SelectableAccessoryViewModel> Accessories
        {
            get
            {
                if (_accessories == null)
                {
                    _accessories = new ObservableCollection<SelectableAccessoryViewModel>();
                    foreach (var accessoryToBoat in _accessoryToBoats)
                    {
                        _accessories.Add(new SelectableAccessoryViewModel(accessoryToBoat.Accessory));
                    }
                }

                return _accessories;
            }
            set
            {
                _accessories = value;
                OnPropertyChanged(nameof(Accessories));
                OnPropertyChanged(nameof(AccessoriesPrice));
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

        public string Wood
        {
            get => _wood;
            set
            {
                _wood = value;
                OnPropertyChanged(nameof(Wood));
            }
        }

        public decimal PriceInclVat => Price + (Price * Convert.ToDecimal(Vat * 0.01));

        public decimal AccessoriesPrice => Accessories.Sum(a => a.IsSelected ? a.PriceInclVat : 0);

        public decimal FinishPrice => PriceInclVat + AccessoriesPrice;

        public string DeliveryAddress
        {
            get => _deliveryAddress;
            set
            {
                _deliveryAddress = value;
                OnPropertyChanged(nameof(DeliveryAddress));
            }
        }

        public string DeliveryCity
        {
            get => _deliveryCity;
            set
            {
                _deliveryCity = value;
                OnPropertyChanged(nameof(DeliveryCity));
            }
        }
        public bool IsCustomer => _authUser.Role == "Customer";
        public override bool SaveButtonIsEnabled => !ErrorDictionary.Any() && IsCustomer;
        public override IDataModel<Order> ModelItem => _orderModel;
        
        protected override Order GetSaveItem(bool isEdit)
        {
            return new Order()
            {
                Id = default,
                CustomerId = _authUser.UserId,
                SalesPersonId = 1,
                Date = DateTime.Now,
                BoatId = _id,
                DeliveryAddress = DeliveryAddress,
                City = DeliveryCity
            };
        }
        
        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return _isEdit
                ? $"Заказ успешно отредактирован."
                : $"Заказ успешно добавлен.";
        }
        
        protected override async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var item = GetSaveItem(false);
            try
            {
                await Task.Run((() => ((OrderModel)ModelItem).AddAsync(item)));
                int orderId = ((OrderModel)ModelItem).LastAddedItem.Id;
                var selectedAccessories = Accessories.Where(a => a.IsSelected);
                foreach (var accessory in selectedAccessories)
                {
                    await Task.Run((() => new OrderDetailsModel().AddAsync(new OrderDetails()
                        {AccessoryId = accessory.Id, OrderId = orderId})));
                }
            }
            finally
            {
                ProgressBarVisibility = Visibility.Collapsed;
            }

            MainWindow.SendSnackbarAction?.Invoke(GetSaveSnackbarMessage(false));

            //Закрываем диалог редактирования партнера
            MainWindow.GetMainWindow?.Invoke().DialogHost.CurrentSession.Close();
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
                    case "DeliveryCity":
                        new Validation(new NotEmptyFieldValidationRule(DeliveryCity)).Validate(ref error);
                        break;
                    case "DeliveryAddress":
                        new Validation(new NotEmptyFieldValidationRule(DeliveryAddress)).Validate(ref error);
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
