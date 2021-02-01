using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryFitManagementViewModel:BaseManagementViewModel<AccessoryToBoat>
    {
        public override IDataModel<AccessoryToBoat> ModelItem { get; }
        public override BaseEditorViewModel<AccessoryToBoat> Editor { get; }
        protected override ObservableCollection<BaseSelectableViewModel<AccessoryToBoat>> GetSelectableViewModels(IEnumerable<AccessoryToBoat> items)
        {
            throw new NotImplementedException();
        }

        protected override ObservableCollection<BaseSelectableViewModel<AccessoryToBoat>> Filter(string filterText)
        {
            throw new NotImplementedException();
        }
    }
}
