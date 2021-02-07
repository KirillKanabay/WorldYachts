using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel
{
    class SalesPersonManagementViewModel:BaseManagementViewModel<SalesPerson>
    {
        public override IDataModel<SalesPerson> ModelItem { get; }
        public override BaseEditorViewModel<SalesPerson> Editor { get; }
        protected override ObservableCollection<BaseSelectableViewModel<SalesPerson>> GetSelectableViewModels(IEnumerable<SalesPerson> items)
        {
            throw new NotImplementedException();
        }

        protected override ObservableCollection<BaseSelectableViewModel<SalesPerson>> Filter(string filterText)
        {
            throw new NotImplementedException();
        }
    }
}
