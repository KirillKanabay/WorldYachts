using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.View.AccessoryControlViews;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryFitManagementViewModel:BaseManagementViewModel<AccessoryToBoat>
    {
        public AccessoryFitManagementViewModel():base(null, null)
        {
            OnItemChanged?.Invoke();
        }
        
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
                    .Select(i => i.Item.AccessoryId)
                    .Distinct();
                foreach (var accessoryId in uniqueAccessories)
                {
                    fits.Add(new FitExpanderViewModel
                        (ItemsCollection
                            .FirstOrDefault(i=>i.Item.AccessoryId == accessoryId)
                            ?.Item.Accessory.Name,
                        ItemsCollection
                            .Where(i=> i.Item.AccessoryId == accessoryId)
                            .Select(i=>i.Item.Boat)));
                }

                return fits.Distinct();
            }
        }

        protected override ObservableCollection<BaseSelectableViewModel<AccessoryToBoat>> GetSelectableViewModels(IEnumerable<AccessoryToBoat> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<AccessoryToBoat>>();
            OnPropertyChanged(nameof(Fits));
            foreach (var accessoryToBoat in items)
            {
                //collection.Add(new SelectableAccessoryFitViewModel(accessoryToBoat));
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
