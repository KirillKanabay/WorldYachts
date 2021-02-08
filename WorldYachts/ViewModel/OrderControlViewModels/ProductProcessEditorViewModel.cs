using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class ProductProcessEditorViewModel:ContractEditorViewModel
    {
        #region Конструкторы

        public ProductProcessEditorViewModel(Contract contract) : base(contract)
        {
        }

        public ProductProcessEditorViewModel()
        {
        }


        #endregion

        #region Свойства

        public string Title => $"Установка готовности контракта #{_contract.Id}";

        #endregion

        #region Методы

        protected override string GetSaveSnackbarMessage(bool _isEdit)
        {
            return $"Готовность контракта #{_contract.Id} изменена";
        }

        #endregion
    }
}
