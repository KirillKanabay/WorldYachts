using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class SelectableContractViewModel:BaseSelectableViewModel<Contract>
    {
        #region Поля

        private int _id;
        private int _orderId;
        private DateTime _date;
        private decimal _depositPayed;
        private decimal _contractTotalPrice;
        private decimal _contractTotalPriceInclVat;
        private string _productionProcess;
        private Order _order;

        private AsyncRelayCommand _deposit;
        #endregion

        #region Конструкторы

        public SelectableContractViewModel(Contract item) : base(item)
        {
            Id = item.Id;
            OrderId = item.OrderId;
            Date = item.Date;
            DepositPayed = item.DepositPayed;
            ContractTotalPrice = item.ContractTotalPrice;
            ContractTotalPriceInclVat = item.ContractTotalPriceInclVat;
            ProductionProcess = item.ProductionProcess;

            _order = item.Order;
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

        public int OrderId
        {
            get => _orderId;
            set
            {
                _orderId = value;
                OnPropertyChanged(nameof(OrderId));
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

        public decimal DepositPayed
        {
            get => _depositPayed;
            set
            {
                _depositPayed = value;
                OnPropertyChanged(nameof(DepositPayed));
            }
        }

        public decimal ContractTotalPrice
        {
            get => _contractTotalPrice;
            set
            {
                _contractTotalPrice = value;
                OnPropertyChanged(nameof(ContractTotalPrice));
            }
        }

        public decimal ContractTotalPriceInclVat
        {
            get => _contractTotalPriceInclVat;
            set
            {
                _contractTotalPriceInclVat = value;
                OnPropertyChanged(nameof(ContractTotalPrice));
            }
        }

        public string ProductionProcess
        {
            get => _productionProcess;
            set
            {
                _productionProcess = value;
                OnPropertyChanged(nameof(ProductionProcess));
            }
        }
        #endregion

        #region Команды

        public AsyncRelayCommand Deposit
        {
            get
            {
                return _deposit ??= new AsyncRelayCommand(DepositMethod, (ex) =>
                {
                    ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
                });
            }
        }

        #endregion

        #region Методы

        private async Task DepositMethod(object parameter)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(new DepositEditorViewModel(Item))
            };
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            
            //Извещаем об изменении предмета
            BaseManagementViewModel<Contract>.OnItemChanged?.Invoke();
        }

        #endregion

        protected override void ToggleViewEditorAfterLoaded()
        {
            throw new NotImplementedException();
        }

        #region NotImplementedMembers
        public override BaseEditorViewModel<Contract> Editor { get; }
        
        protected override BaseViewModel GetEditorViewModel()
        {
            throw new NotImplementedException();
        }
        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
