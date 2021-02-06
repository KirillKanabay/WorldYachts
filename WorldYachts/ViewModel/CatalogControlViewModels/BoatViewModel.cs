using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Model;
using WorldYachts.ViewModel.AccessoryControlViewModels;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.CatalogControlViewModels
{
    public class BoatViewModel:BaseViewModel
    {
        #region Поля

        private string _model;
        private string _type;
        private string _wood;
        private bool _mast;
        private string _color;
        private int _numberOfRowers;
        private List<AccessoryToBoat> _accessoryToBoats;
        private ObservableCollection<SelectableAccessoryViewModel> _accessories;
        private decimal _price;
        private double _vat;

        public static Action<object> OnAccessoryChanged;
        #endregion

        #region Конструкторы

        public BoatViewModel(Boat boat)
        {
            _model = boat.Model;
            _type = boat.Type;
            _wood = boat.Wood;
            _mast = boat.Mast;
            _color = boat.Color;
            _numberOfRowers = boat.NumberOfRowers;
            _accessoryToBoats = boat.AccessoryToBoat;
            _price = boat.BasePrice;
            _vat = boat.Vat;

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

        #endregion
    }
}
