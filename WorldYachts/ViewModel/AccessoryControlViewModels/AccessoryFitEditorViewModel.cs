using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryFitEditorViewModel:BaseEditorViewModel<AccessoryToBoat>
    {
        #region Поля

        private readonly int _id;
        private int _accessoryId;
        private int _boatId;

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
                AccessoryId = value;
                OnPropertyChanged(nameof(AccessoryId));
            }
        }
        public override bool SaveButtonIsEnabled => true;
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
    }
}
