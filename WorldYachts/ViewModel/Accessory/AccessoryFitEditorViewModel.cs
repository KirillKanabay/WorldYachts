﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;
using AccessoryToBoat = WorldYachts.Data.AccessoryToBoat;

namespace WorldYachts.ViewModel.Accessory
{
    class AccessoryFitEditorViewModel:BaseEditorViewModel<AccessoryToBoat>,IDataErrorInfo
    {
        #region Поля

        private readonly int _id;
        private int _accessoryId;
        private int _boatId;

        private Data.Entities.Accessory _accessory;
        private Data.Entities.Boat _boat;

        private IEnumerable<Data.Entities.Accessory> _accessoryCollection;
        private IEnumerable<Data.Entities.Boat> _boatCollection;

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

        public Data.Entities.Accessory Accessory
        {
            get => _accessory;
            set
            {
                _accessory = value;
                AccessoryId = value.Id;
                OnPropertyChanged(nameof(Accessory));
            }
        }

        public Data.Entities.Boat Boat
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
        //public IEnumerable<Data.Entities.Boat> BoatCollection => Task.Run(async () => await new BoatModel().GetAllAsync()).Result;
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
