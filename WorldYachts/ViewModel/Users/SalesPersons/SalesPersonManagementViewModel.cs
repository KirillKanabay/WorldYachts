﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorldYachts.Data.Entities;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.UserControlViewModels;

namespace WorldYachts.ViewModel.Users.SalesPersons
{
    class SalesPersonManagementViewModel:BaseManagementViewModel<SalesPerson>
    {
        public SalesPersonManagementViewModel():base(null, null)
        {
        }

        protected override ObservableCollection<BaseSelectableViewModel<SalesPerson>> GetSelectableViewModels(IEnumerable<SalesPerson> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<SalesPerson>>();
            foreach (var sp in items)
            {
                //collection.Add(new SelectableSalesPersonViewModel(sp));
            }

            return collection;
        }

        protected override ObservableCollection<BaseSelectableViewModel<SalesPerson>> Filter(string filterText)
        {
            var filteredCollection = ItemsCollection.Where(sp =>
                sp.Item.FirstName.ToLower().Contains(filterText.ToLower()) ||
                sp.Item.SecondName.ToLower().Contains(filterText.ToLower()) ||
                sp.Item.Id.ToString() == filterText);

            var spCollection = new ObservableCollection<BaseSelectableViewModel<SalesPerson>>();
            foreach (var sp in filteredCollection)
            {
                spCollection.Add(sp);
            }

            return spCollection;
        }
    }
}