using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Infrastructure;
using WorldYachts.Services;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class InvoiceViewModel:InvoiceManagementViewModel
    {
        public override ObservableCollection<BaseSelectableViewModel<Invoice>> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return new ObservableCollection<BaseSelectableViewModel<Invoice>>(Filter(_filterText)
                        .Where(i => i.Item.Contract.Order.CustomerId == AuthUser.GetInstance().User.Id));

                return new ObservableCollection<BaseSelectableViewModel<Invoice>>
                (ItemsCollection.
                    Where(i => i.Item.Contract.Order.CustomerId == AuthUser.GetInstance().User.Id));
            }
        }
    }
}
