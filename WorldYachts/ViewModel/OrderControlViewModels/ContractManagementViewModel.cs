using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class ContractManagementViewModel:BaseManagementViewModel<Contract>
    {
        private readonly AuthUser _authUser;
        #region Конструкторы
        public ContractManagementViewModel(AuthUser authUser):base(null,null)
        {
            _authUser = authUser;
        }

        #endregion

        #region Свойства

        #endregion

        protected override ObservableCollection<BaseSelectableViewModel<Contract>> GetSelectableViewModels(IEnumerable<Contract> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Contract>>();
            foreach (var contract in items)
            {
               // collection.Add(new SelectableContractViewModel(contract, new DepositEditorViewModel(_authUser),new ProductProcessEditorViewModel(_authUser) ));
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
