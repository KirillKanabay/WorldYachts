using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryFitEditorViewModel:BaseEditorViewModel<AccessoryToBoat>
    {
        public AccessoryFitEditorViewModel(bool isEdit) : base(isEdit)
        {
        }

        public override bool SaveButtonIsEnabled { get; }
        public override IDataModel<AccessoryToBoat> ModelItem { get; }
        protected override AccessoryToBoat GetSaveItem(bool isEdit)
        {
            throw new NotImplementedException();
        }

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            throw new NotImplementedException();
        }
    }
}
