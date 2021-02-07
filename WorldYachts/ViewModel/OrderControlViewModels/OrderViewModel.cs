using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class OrderViewModel:BaseManagementViewModel<Order>
    {
        public OrderViewModel()
        {
            OnItemChanged?.Invoke();
        }

        public override IDataModel<Order> ModelItem => new OrderModel();
        public override BaseEditorViewModel<Order> Editor { get; }
        protected override ObservableCollection<BaseSelectableViewModel<Order>> GetSelectableViewModels(IEnumerable<Order> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Order>>();
            OnPropertyChanged(nameof(ItemsCollection));
            foreach (var order in items)
            {
                collection.Add(new SelectableOrderViewModel(order));
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
