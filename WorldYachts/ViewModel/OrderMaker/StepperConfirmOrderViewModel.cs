using System;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Services;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.OrderMaker
{
    class StepperConfirmOrderViewModel : BaseViewModel
    {
        private readonly OrderContainerViewModel _orderContainerViewModel;
        private readonly IOrderModel _orderModel;
        private readonly IOrderDetailModel _orderDetailModel;
        private readonly IContractModel _contractModel;
        private readonly AuthUser _authUser;

        private Order _addedOrder;
        private Contract _addedContract;

        private Visibility _progressBarVisibility = Visibility.Collapsed;

        public StepperConfirmOrderViewModel(OrderContainerViewModel orderContainerViewModel, IOrderModel orderModel,
            IOrderDetailModel orderDetailModel, IInvoiceModel invoiceModel, IContractModel contractModel,
            AuthUser authUser)
        {
            _orderContainerViewModel = orderContainerViewModel;
            _orderModel = orderModel;
            _orderDetailModel = orderDetailModel;
            _contractModel = contractModel;
            _authUser = authUser;
        }

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        public string OrderInfo => _orderContainerViewModel.ToString();
        public decimal Price => _orderContainerViewModel.Price;
        public decimal PriceInclVat => _orderContainerViewModel.PriceInclVat;

        public DelegateCommand UpdateCommand => new DelegateCommand(UpdateMethod);

        public AsyncRelayCommand SaveItem => new AsyncRelayCommand(SaveMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message}); });

        private void UpdateMethod(object parameter)
        {
            OnPropertyChanged(nameof(OrderInfo));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(PriceInclVat));
        }

        private async Task SaveMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;

            await AddOrder();
            await AddOrderDetails();
            await AddContract();

            ProgressBarVisibility = Visibility.Collapsed;

            string snackbarMessage = "Заказ успешно добавлен";

            SendSnackbar(snackbarMessage);
        }

        private async Task AddOrder()
        {
            var order = new Order()
            {
                CustomerId = _orderContainerViewModel.Customer.Id,
                SalesPersonId = _authUser.UserId,
                Date = DateTime.Now,
                BoatId = _orderContainerViewModel.Boat.Id,
                DeliveryAddress = _orderContainerViewModel.DeliveryAddress,
                City = _orderContainerViewModel.City,
                Status = 0
            };
            _addedOrder = await _orderModel.AddAsync(order);
        }

        private async Task AddOrderDetails()
        {
            foreach (var accessory in _orderContainerViewModel.SelectedAccessories)
            {
                var orderDetail = new OrderDetail() {AccessoryId = accessory.Id, OrderId = _addedOrder.Id};
                await _orderDetailModel.AddAsync(orderDetail);
            }
        }
        
        private async Task AddContract()
        {
            var contract = new Contract()
            {
                OrderId = _addedOrder.Id, Date = DateTime.Now, DepositPayed = 0, ContractTotalPrice = Price,
                ContractTotalPriceInclVat = PriceInclVat, ProductionProcess = "Подготовка"
            };

           _addedContract = await _contractModel.AddAsync(contract);
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty) o)
            };
            var result = await DialogHost.Show(view, "DialogRoot");
        }
    }
}