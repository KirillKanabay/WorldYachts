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
        private AsyncRelayCommand _changeProductionProcess;
        private readonly DepositEditorViewModel _depositEditorViewModel;
        private readonly ProductProcessEditorViewModel _productProcessEditorViewModel;
        #endregion

        #region Конструкторы

        public SelectableContractViewModel(Contract item, 
            DepositEditorViewModel depositEditorViewModel, 
            ProductProcessEditorViewModel productProcessEditorViewModel) : base(item,null)
        {
            Id = item.Id;
            OrderId = item.OrderId;
            Date = item.Date;
            DepositPayed = item.DepositPayed;
            ContractTotalPrice = item.ContractTotalPrice;
            ContractTotalPriceInclVat = item.ContractTotalPriceInclVat;
            ProductionProcess = item.ProductionProcess;

            _order = item.Order;

            _depositEditorViewModel = depositEditorViewModel;
            _productProcessEditorViewModel = productProcessEditorViewModel;
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
        //Приступить к выполнению контракта можем после погашения 1/3 стоимости
        public bool ChangeProductionProcessEnabled => DepositPayed >= (ContractTotalPriceInclVat / 3);

        #endregion

        #region Команды

        //public AsyncRelayCommand Deposit
        //{
        //    get
        //    {
        //        return _deposit ??= new AsyncRelayCommand(DepositMethod, (ex) =>
        //        {
        //            ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
        //        });
        //    }
        //}

        //public AsyncRelayCommand ChangeProductionProcess
        //{
        //    get
        //    {
        //        return _changeProductionProcess ??= new AsyncRelayCommand(ChangeProductionProcessMethod, (ex) =>
        //        {
        //            ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message });
        //        });
        //    }
        //}

        #endregion

        #region Методы

        private async Task DepositMethod(object parameter)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_depositEditorViewModel)
            };
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            
            //Извещаем об изменении предмета
            BaseManagementViewModel<Contract>.OnItemChanged?.Invoke();
        }

        private void ClosingEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            //throw new NotImplementedException();
        }

        private async Task ChangeProductionProcessMethod(object parameter)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_productProcessEditorViewModel)
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
