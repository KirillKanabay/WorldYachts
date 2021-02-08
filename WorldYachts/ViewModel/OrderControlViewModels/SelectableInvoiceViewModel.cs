using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class SelectableInvoiceViewModel:BaseSelectableViewModel<Invoice>
    {
        #region Поля

        private int _id;
        private int _contractId;
        private bool _settled;
        private decimal _sum;
        private decimal _sumInclVat;
        private Contract _contract;

        #endregion

        #region Конструкторы
        public SelectableInvoiceViewModel(Invoice item) : base(item)
        {

        }

        #endregion

        #region Свойства

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int ContractId
        {
            get => _contractId;
            set
            {
                _contractId = value;
                OnPropertyChanged(nameof(ContractId));
            }
        }

        public bool Settled
        {
            get => _settled;
            set
            {
                _settled = value;
                OnPropertyChanged(nameof(Settled));
            }
        }

        public decimal Sum
        {
            get => _sum;
            set
            {
                _sum = value;
                OnPropertyChanged(nameof(Sum));
            }
        }

        public decimal SumInclVat
        {
            get => _sumInclVat;
            set
            {
                _sumInclVat = value;
                OnPropertyChanged(nameof(SumInclVat));
            }
        }



        #endregion

        #region Методы

        #endregion


        #region NotImplementedMembers
        public override BaseEditorViewModel<Invoice> Editor => throw new NotImplementedException();
        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            throw new NotImplementedException();
        }
        protected override void ToggleViewEditorAfterLoaded()
        {
            throw new NotImplementedException();
        }

        protected override BaseViewModel GetEditorViewModel() => throw new NotImplementedException();
        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"ContractId: {ContractId}\n" +
                   $"Сумма: {Sum} ₽";
        }
        #endregion

    }
}
