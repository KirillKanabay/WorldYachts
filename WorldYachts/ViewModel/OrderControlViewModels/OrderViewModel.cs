using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.Infrastructure;
using WorldYachts.Services;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class OrderViewModel : BaseManagementViewModel<Order>
    {
        private readonly AuthUser _authUser;
        public OrderViewModel(AuthUser authUser):base(null, null)
        {
            _authUser = authUser;
            OnItemChanged?.Invoke();
        }

        public override ObservableCollection<BaseSelectableViewModel<Order>> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return new ObservableCollection<BaseSelectableViewModel<Order>>(Filter(_filterText)
                        .Where(i => i.Item.CustomerId == _authUser.User.Id));

                return new ObservableCollection<BaseSelectableViewModel<Order>>(
                    ItemsCollection.Where(i => i.Item.CustomerId == _authUser.User.Id));
            }
        }
        
        protected override ObservableCollection<BaseSelectableViewModel<Order>> GetSelectableViewModels(
            IEnumerable<Order> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Order>>();
            OnPropertyChanged(nameof(ItemsCollection));
            foreach (var order in items)
            {
                collection.Add(new SelectableOrderViewModel(order,_authUser));
            }

            return collection;
        }

        protected override ObservableCollection<BaseSelectableViewModel<Order>> Filter(string filterText)
        {
            var filteredCollection = ItemsCollection.Where(o =>
                o.Item.Id.ToString().Contains(filterText));

            var orderCollection = new ObservableCollection<BaseSelectableViewModel<Order>>();
            foreach (var order in filteredCollection)
            {
                orderCollection.Add(order);
            }

            return orderCollection;
        }
    }
}