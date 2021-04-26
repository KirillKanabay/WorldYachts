using WorldYachts.Data.Entities;
using WorldYachts.Model;
using WorldYachts.View.Editors;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using AccessoryToBoat = WorldYachts.Data.AccessoryToBoat;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class SelectableAccessoryFitViewModel:BaseSelectableViewModel<AccessoryToBoat>
    {
        #region Поля

        private int _id;
        private int _accessoryId;
        private int _boatId;

        #endregion
        public SelectableAccessoryFitViewModel(AccessoryToBoat item, AccessoryToBoatModel accessoryToBoatModel) : base(item, accessoryToBoatModel)
        {
            Id = item.Id;
            AccessoryId = item.AccessoryId;
            BoatId = item.BoatId;
        }

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

        public int AccessoryId
        {
            get => _accessoryId;
            set
            {
                _accessoryId = value;
                OnPropertyChanged(nameof(AccessoryId));
            }
        }

        public int BoatId
        {
            get => _boatId;
            set
            {
                _boatId = value;
                OnPropertyChanged(nameof(BoatId));
            }
        }

        public Boat Boat => _item.Boat;
        public Accessory Accessory => _item.Accessory;
        public override BaseEditorViewModel<AccessoryToBoat> Editor => new AccessoryFitEditorViewModel();
        #endregion

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"AccessoryId: {AccessoryId}\n" +
                   $"BoatId: {BoatId}";
        }

        protected override void ToggleViewEditorAfterLoaded()
        {
            if (AccessoryFitEditorView.EditorAfterLoad != null)
            {
                AccessoryFitEditorView.EditorAfterLoad = null;
            }
            else
            {
                AccessoryFitEditorView.EditorAfterLoad = GetEditorViewModel;
            }
        }

        protected override BaseViewModel GetEditorViewModel()=> new AccessoryFitEditorViewModel(_item);

        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            return new MessageDialogProperty()
            {
                Title = "Подтверждение удаления",
                Message = "Будет удалена следующая связь:\n\n" + this
            };
        }
    }
}
