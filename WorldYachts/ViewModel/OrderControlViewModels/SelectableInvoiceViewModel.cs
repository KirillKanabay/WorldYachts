using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
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
        private DateTime _date;
        private Contract _contract;
        private Invoice _item;

        private AsyncRelayCommand _accept;
        #endregion

        #region Конструкторы
        public SelectableInvoiceViewModel(Invoice item) : base(item)
        {
            Id = item.Id;
            ContractId = item.ContractId;
            Settled = item.Settled;
            Sum = item.Sum;
            SumInclVat = item.SumInclVat;
            Date = item.Date;

            _contract = item.Contract;
            _item = item;
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

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }


        #endregion

        #region Команды

        public AsyncRelayCommand Accept
        {
            get
            {
               return _accept ??= new AsyncRelayCommand(AcceptMethod, null);
            }
        }
        

        #endregion

        private async Task AcceptMethod(object parameter)
        {
            _item.Settled = true;
            await Task.Run(() => new InvoiceModel().UpdateAsync(_item));
            //Извещаем об изменении предмета
            BaseManagementViewModel<Invoice>.OnItemChanged?.Invoke();
        }

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
