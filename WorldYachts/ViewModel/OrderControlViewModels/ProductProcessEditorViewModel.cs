using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Services;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class ProductProcessEditorViewModel:ContractEditorViewModel
    {
        #region Конструкторы

        private readonly AuthUser _authUser;
        public ProductProcessEditorViewModel(Contract contract,AuthUser authUser) : base(contract,authUser)
        {
            _authUser = authUser;
        }

        public ProductProcessEditorViewModel(AuthUser authUser):base(authUser)
        {
            _authUser = authUser;
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
