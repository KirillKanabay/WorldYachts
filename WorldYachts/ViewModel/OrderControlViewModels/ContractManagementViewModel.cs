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
    class ContractManagementViewModel:BaseManagementViewModel<Contract>
    {
        #region Конструкторы
        public ContractManagementViewModel()
        {
        }

        #endregion

        #region Свойства
        public override IDataModel<Contract> ModelItem => new ContractModel();
        public override BaseEditorViewModel<Contract> Editor { get; }

        #endregion

        protected override ObservableCollection<BaseSelectableViewModel<Contract>> GetSelectableViewModels(IEnumerable<Contract> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Contract>>();
            foreach (var contract in items)
            {
                collection.Add(new SelectableContractViewModel(contract));
            }

            return collection;
        }

        protected override ObservableCollection<BaseSelectableViewModel<Contract>> Filter(string filterText)
        {
            var filteredCollection = ItemsCollection.Where(c =>
                c.Item.Id.ToString().Contains(filterText.ToLower()) ||
                c.Item.OrderId.ToString().Contains(filterText.ToLower()));

            var contractCollection = new ObservableCollection<BaseSelectableViewModel<Contract>>();
            foreach (var contract in filteredCollection)
            {
                contractCollection.Add(contract);
            }

            return contractCollection;
        }
    }
}
