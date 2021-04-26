using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class AccessoryManagementViewModel : BaseManagementViewModel<Accessory>
    {
        public AccessoryManagementViewModel(AccessoryModel accessoryModel, 
            AccessoryEditorViewModel accessoryEditorViewModel)
            :base(accessoryModel, accessoryEditorViewModel)
        {
        }
        protected override ObservableCollection<BaseSelectableViewModel<Accessory>> GetSelectableViewModels(IEnumerable<Accessory> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Accessory>>();
            foreach (var accessory in items)
            {
               collection.Add(new SelectableAccessoryViewModel(accessory,(AccessoryModel)_dataModel));
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