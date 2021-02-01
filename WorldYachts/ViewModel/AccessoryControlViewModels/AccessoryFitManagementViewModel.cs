using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.View.AccessoryControlViews;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryFitManagementViewModel:BaseManagementViewModel<AccessoryToBoat>
    {
        
        /// <summary>
        /// Список совместимостей
        /// </summary>
        public IEnumerable<FitExpanderViewModel> Fits
        {
            get
            {
                var fits = new List<FitExpanderViewModel>();
                //Убираем повторяющиеся аксессуары
                var uniqueAccessories = ItemsCollection
                    .Select(i => i.Item.Accessory)
                    .Distinct();
                foreach (var accessory in uniqueAccessories)
                {
                    fits.Add(new FitExpanderViewModel(accessory.Name,
                        ItemsCollection
                            .Where(i=>i.Item.Accessory.Name == accessory.Name)
                            .Select(i=>i.Item.Boat)));
                }

                return fits;
            }
        }
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
