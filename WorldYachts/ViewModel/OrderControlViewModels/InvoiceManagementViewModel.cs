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
using WorldYachts.Services;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class InvoiceManagementViewModel : BaseManagementViewModel<Invoice>
    {
        #region Конструкторы

        public InvoiceManagementViewModel()
        {
            OnItemChanged += () => { GetItemsCollection?.Execute(null); };
        }

        #endregion

        #region Свойства

        public ObservableCollection<BaseSelectableViewModel<Invoice>> SettledInvoices
        {
            get
            {
                var siCollection = ItemsCollection.Where(o => o.Item.Settled
                                                              && o.Item.Contract.Order.SalesPersonId ==
                                                              AuthUser.GetInstance().User.Id);
                //Invoices collection
                var ic = new ObservableCollection<BaseSelectableViewModel<Invoice>>(siCollection);
                
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(ic, _filterText);

                return ic;
            }
        }

        public ObservableCollection<BaseSelectableViewModel<Invoice>> NotSettledInvoices
        {
            get
            {
                var nsiCollection = ItemsCollection.Where(o => !o.Item.Settled
                                                     && o.Item.Contract.Order.SalesPersonId ==
                                                     AuthUser.GetInstance().User.Id);

                var ic = new ObservableCollection<BaseSelectableViewModel<Invoice>>(nsiCollection);

                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(ic, _filterText);

                return ic;
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

        public override IDataModel<Invoice> ModelItem => new InvoiceModel();
        public override BaseEditorViewModel<Invoice> Editor { get; }

        #endregion

        #region Методы

        protected override async Task GetCollectionMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var items = await ModelItem.GetAllAsync();
            ItemsCollection = GetSelectableViewModels(items);
            //OnPropertyChanged(nameof(FilteredCollection));
            UpdateCollections();
            ProgressBarVisibility = Visibility.Collapsed;
        }

        private void UpdateCollections()
        {
            OnPropertyChanged(nameof(SettledInvoices));
            OnPropertyChanged(nameof(NotSettledInvoices));
            OnPropertyChanged(nameof(FilteredCollection));
        }

        protected override ObservableCollection<BaseSelectableViewModel<Invoice>> GetSelectableViewModels(
            IEnumerable<Invoice> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Invoice>>();
            OnPropertyChanged(nameof(ItemsCollection));
            foreach (var invoice in items)
            {
                collection.Add(new SelectableInvoiceViewModel(invoice));
            }

            return collection;
        }


        protected ObservableCollection<BaseSelectableViewModel<Invoice>> Filter(
            ObservableCollection<BaseSelectableViewModel<Invoice>> oc, string filterText)
        {
            var filteredCollection = oc.Where(o =>
                o.Item.Id.ToString().Contains(filterText));

            var invoiceCollection = new ObservableCollection<BaseSelectableViewModel<Invoice>>(filteredCollection);
            
            return invoiceCollection;
        }

        protected override ObservableCollection<BaseSelectableViewModel<Invoice>> Filter(string filterText)
        {
            var filteredCollection = ItemsCollection.Where(o =>
                o.Item.Id.ToString().Contains(filterText));

            var invoiceCollection = new ObservableCollection<BaseSelectableViewModel<Invoice>>(filteredCollection);

            return invoiceCollection;
        }

        #endregion
    }
}