using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorldYachts.Data;
using WorldYachts.Infrastructure;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.OrderControlViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class OrderManagementViewModel : BaseManagementViewModel<Order>
    {
        #region Конструкторы

        public OrderManagementViewModel()
        {
            OnItemChanged += () =>
            {
                GetItemsCollection?.Execute(null);
            };
        }

        #endregion

        #region Свойства

        public override ObservableCollection<BaseSelectableViewModel<Order>> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(_filterText);

                return ItemsCollection;
            }
        }

        public ObservableCollection<BaseSelectableViewModel<Order>> InProcessingOrders
        {
            get
            {
                var ipCollection = ItemsCollection
                    .Where(o => o.Item.Status == (int) OrderStatus.InProcessing);
                var oc = new ObservableCollection<BaseSelectableViewModel<Order>>();
                foreach (var order in ipCollection)
                {
                    oc.Add(order);
                }

                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(oc,_filterText);

                return oc;
            }
        }

        public ObservableCollection<BaseSelectableViewModel<Order>> AcceptedOrders
        {
            get
            {
                var ipCollection = ItemsCollection
                    .Where(o => o.Item.Status == (int) OrderStatus.Accepted
                                && o.Item.SalesPersonId == AuthUser.User.Id);
                var oc = new ObservableCollection<BaseSelectableViewModel<Order>>();
                foreach (var order in ipCollection)
                {
                    oc.Add(order);
                }

                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(oc, _filterText);

                return oc;
            }
        }

        public ObservableCollection<BaseSelectableViewModel<Order>> CompletedOrders
        {
            get
            {
                var ipCollection = ItemsCollection
                    .Where(o => o.Item.Status == (int) OrderStatus.Completed
                                && o.Item.SalesPersonId == AuthUser.User.Id);
                var oc = new ObservableCollection<BaseSelectableViewModel<Order>>();
                foreach (var order in ipCollection)
                {
                    oc.Add(order);
                }

                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(oc, _filterText);


                return oc;
            }
        }

        public ObservableCollection<BaseSelectableViewModel<Order>> CanceledOrders
        {
            get
            {
                var ipCollection = ItemsCollection
                    .Where(o => o.Item.Status == (int) OrderStatus.Canceled
                                && o.Item.SalesPersonId == AuthUser.User.Id);
                var oc = new ObservableCollection<BaseSelectableViewModel<Order>>();
                foreach (var order in ipCollection)
                {
                    oc.Add(order);
                }

                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(oc, _filterText);

                return oc;
            }
        }

        public override string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                if (_filterText != null)
                {
                    UpdateCollections();
                }
            }
        }
        public override IDataModel<Order> ModelItem => new OrderModel();
        public override BaseEditorViewModel<Order> Editor { get; }

        #endregion

        #region Методы

        protected override async Task GetCollectionMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var items = await ModelItem.LoadAsync();
            ItemsCollection = GetSelectableViewModels(items);
            //OnPropertyChanged(nameof(FilteredCollection));
            UpdateCollections();
            ProgressBarVisibility = Visibility.Collapsed;
        }

        private void UpdateCollections()
        {
            OnPropertyChanged(nameof(InProcessingOrders));
            OnPropertyChanged(nameof(CompletedOrders));
            OnPropertyChanged(nameof(CanceledOrders));
            OnPropertyChanged(nameof(AcceptedOrders));
        }

        protected override ObservableCollection<BaseSelectableViewModel<Order>> GetSelectableViewModels(
            IEnumerable<Order> items)
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

        protected ObservableCollection<BaseSelectableViewModel<Order>> Filter(
            ObservableCollection<BaseSelectableViewModel<Order>> oc, string filterText)
        {
            var filteredCollection = oc.Where(o =>
                o.Item.Id.ToString().Contains(filterText));

            var orderCollection = new ObservableCollection<BaseSelectableViewModel<Order>>();
            foreach (var order in filteredCollection)
            {
                orderCollection.Add(order);
            }

            return orderCollection;
        }

        #endregion
    }
}