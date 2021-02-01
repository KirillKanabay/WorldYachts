using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryFitManagementViewModel:BaseManagementViewModel<AccessoryToBoat>
    {
        public override IDataModel<AccessoryToBoat> ModelItem => new AccessoryToBoatModel();
        public override BaseEditorViewModel<AccessoryToBoat> Editor => new AccessoryFitEditorViewModel();
        protected override ObservableCollection<BaseSelectableViewModel<AccessoryToBoat>> GetSelectableViewModels(IEnumerable<AccessoryToBoat> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<AccessoryToBoat>>();
            foreach (var accessoryToBoat in items)
            {
                collection.Add(new SelectableAccessoryFitViewModel(accessoryToBoat));
            }

            return collection;
        }

        protected override ObservableCollection<BaseSelectableViewModel<AccessoryToBoat>> Filter(string filterText)
        {
            var filteredCollection = ItemsCollection.Where(atb =>
                atb.Item.Accessory.Name.ToLower().Contains(filterText.ToLower()));

            var accessoryCollection = new ObservableCollection<BaseSelectableViewModel<AccessoryToBoat>>();
            foreach (var atb in filteredCollection)
            {
                accessoryCollection.Add(atb);
            }

            return accessoryCollection;
        }
    }
}
