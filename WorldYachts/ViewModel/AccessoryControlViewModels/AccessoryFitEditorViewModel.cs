using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorldYachts.Data.Entities;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;
using AccessoryToBoat = WorldYachts.Data.AccessoryToBoat;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryFitEditorViewModel:BaseEditorViewModel<AccessoryToBoat>,IDataErrorInfo
    {
        #region Поля

        private readonly int _id;
        private int _accessoryId;
        private int _boatId;

        private Accessory _accessory;
        private Boat _boat;

        private IEnumerable<Accessory> _accessoryCollection;
        private IEnumerable<Boat> _boatCollection;

        #endregion

        #region Конструкторы
        public AccessoryFitEditorViewModel(AccessoryToBoat accessoryToBoat) : base(true)
        {
            _id = accessoryToBoat.Id;
            _accessoryId = accessoryToBoat.AccessoryId;
            _boatId = accessoryToBoat.BoatId;
        }

        public AccessoryFitEditorViewModel() : base(false)
        {

        }
        #endregion

        #region Свойства

        public int BoatId
        {
            get => _boatId;
            set
            {
                _boatId = value;
                OnPropertyChanged(nameof(BoatId));
            }
        }

        public int AccessoryId
        {
            get => _accessoryId;
            set
            {
                _accessoryId = value;
                OnPropertyChanged(nameof(AccessoryId));
            }
        }

        public Accessory Accessory
        {
            get => _accessory;
            set
            {
                _accessory = value;
                AccessoryId = value.Id;
                OnPropertyChanged(nameof(Accessory));
            }
        }

        public Boat Boat
        {
            get => _boat;
            set
            {
                _boat = value;
                BoatId = value.Id;
                OnPropertyChanged(nameof(Boat));
            }
        }
        //public IEnumerable<Accessory> AccessoryCollection => new AccessoryModel().Load();
        public IEnumerable<Boat> BoatCollection => Task.Run(async () => await new BoatModel().GetAllAsync()).Result;
        public override bool SaveButtonIsEnabled => !ErrorDictionary.Any();
        #endregion

        public override IDataModel<AccessoryToBoat> ModelItem => new AccessoryToBoatModel();
        protected override AccessoryToBoat GetSaveItem(bool isEdit)
        {
            return new AccessoryToBoat()
            {
                Id = (isEdit) ? _id : default,
                AccessoryId = _accessoryId,
                BoatId = _boatId,
            };
        }

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return _isEdit
                ? $"Совместимость успешно отредактирована."
                : $"Совместимость успешно добавлена.";
        }


        public string Error { get; }
        private Dictionary<string, string> ErrorDictionary = new Dictionary<string, string>();
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Accessory":
                        break;
                    case "Boat":
                        break;
                }
                ErrorDictionary.Remove(columnName);
                if (error != String.Empty)
                    ErrorDictionary.Add(columnName, error);
                OnPropertyChanged(nameof(SaveButtonIsEnabled));
                return error;
            }
        }
    }
}
