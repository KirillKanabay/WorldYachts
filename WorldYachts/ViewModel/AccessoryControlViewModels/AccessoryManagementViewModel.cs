using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryManagementViewModel : BaseManagementViewModel<Accessory>
    {
        public override IDataModel<Accessory> ModelItem => new AccessoryModel();
        public override BaseEditorViewModel<Accessory> Editor => new AccessoryEditorViewModel();
        protected override ObservableCollection<BaseSelectableViewModel<Accessory>> GetSelectableViewModels(IEnumerable<Accessory> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Accessory>>();
            foreach (var accessory in items)
            {
                collection.Add(new SelectableAccessoryViewModel(accessory));
            }

            return collection;
        }

        protected override ObservableCollection<BaseSelectableViewModel<Accessory>> Filter(string filterText)
        {
            var filteredCollection = ItemsCollection.Where(a =>
                a.Item.Name.ToLower().Contains(filterText.ToLower()) ||
                a.Item.Id.ToString() == filterText);

            var accessoryCollection = new ObservableCollection<BaseSelectableViewModel<Accessory>>();
            foreach (var selectableAccessoryViewModel in filteredCollection)
            {
                accessoryCollection.Add(selectableAccessoryViewModel);
            }

            return accessoryCollection;
        }
    }
}