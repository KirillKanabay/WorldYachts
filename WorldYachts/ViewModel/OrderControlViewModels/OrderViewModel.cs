using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class OrderViewModel:BaseManagementViewModel<Order>
    {
        public override IDataModel<Order> ModelItem { get; }
        public override BaseEditorViewModel<Order> Editor { get; }
        protected override ObservableCollection<BaseSelectableViewModel<Order>> GetSelectableViewModels(IEnumerable<Order> items)
        {
            throw new NotImplementedException();
        }

        protected override ObservableCollection<BaseSelectableViewModel<Order>> Filter(string filterText)
        {
            throw new NotImplementedException();
        }
    }
}
