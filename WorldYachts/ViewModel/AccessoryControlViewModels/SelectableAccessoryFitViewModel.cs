using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;
using WorldYachts.View.MessageDialogs;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class SelectableAccessoryFitViewModel:BaseSelectableViewModel<AccessoryToBoat>
    {
        public SelectableAccessoryFitViewModel(AccessoryToBoat item) : base(item)
        {
        }

        public override BaseEditorViewModel<AccessoryToBoat> Editor { get; }
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
    }
}
