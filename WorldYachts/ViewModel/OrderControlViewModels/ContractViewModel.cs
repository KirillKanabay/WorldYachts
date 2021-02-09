using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Infrastructure;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class ContractViewModel : ContractManagementViewModel
    {
        public override ObservableCollection<BaseSelectableViewModel<Contract>> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return new ObservableCollection<BaseSelectableViewModel<Contract>>(Filter(_filterText)
                        .Where(i => i.Item.Order.CustomerId == AuthUser.User.Id));

                return new ObservableCollection<BaseSelectableViewModel<Contract>>(ItemsCollection.Where(i => i.Item.Order.CustomerId == AuthUser.User.Id));
            }
        }
    }
}