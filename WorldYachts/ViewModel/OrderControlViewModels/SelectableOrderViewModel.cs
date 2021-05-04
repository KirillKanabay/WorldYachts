using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Infrastructure;
using WorldYachts.Model;
using WorldYachts.Services;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using Order = WorldYachts.Data.Order;

namespace WorldYachts.ViewModel.OrderControlViewModels
{
    class SelectableOrderViewModel : BaseSelectableViewModel<Order>
    {
        #region Поля

        private AsyncRelayCommand _acceptCommand;
        private AsyncRelayCommand _compliteCommand;
        private AsyncRelayCommand _cancelCommand;
        private AsyncRelayCommand _setOrderStatus;
        private OrderStatus _os;

        private readonly AuthUser _authUser;

        #endregion

        #region Конструкторы

        public SelectableOrderViewModel(Order item, AuthUser authUser) : base(item,null)
        {
            _authUser = authUser;
        }

        #endregion

        #region Свойства

        public Data.Entities.Boat Boat => _item.Boat;
        public List<OrderDetails> OrderDetails => _item.OrderDetails;
        public override BaseEditorViewModel<Order> Editor { get; }

        #endregion

        #region Команды

        public AsyncRelayCommand SetOrderStatus
        {
            get
            {
                return _setOrderStatus ??= new AsyncRelayCommand(SetOrderStatusMethod,
                    (ex) =>
                    {
                        ExecuteRunDialog(new MessageDialogProperty() {Title = "Ошибка", Message = ex.Message});
                    });
            }
        }

        #endregion

        #region Методы

        private async Task SetOrderStatusMethod(object parameter)
        {
            string os = (string) parameter;
            switch (os)
            {
                case "InProcessing":
                    _os = OrderStatus.InProcessing;
                    break;
                case "Accepted":
                    _os = OrderStatus.Accepted;
                    break;
                case "Completed":
                    _os = OrderStatus.Completed;
                    break;
                case "Canceled":
                    _os = OrderStatus.Canceled;
                    break;
            }

            Item.Status = (int) _os;
            //Item.SalesPersonId = (_os == OrderStatus.InProcessing) ? 1 : _.Id;
            await Task.Run(() => new OrderModel().UpdateAsync(Item));
            if (_os == OrderStatus.Accepted)
            {
                var contract = new Contract()
                {
                    OrderId = Item.Id,
                    Date = DateTime.Now,
                    DepositPayed = 0,
                    ContractTotalPrice = Item.CountPrice(),
                    ContractTotalPriceInclVat = Item.CountPriceInclVat(),
                    ProductionProcess = EnumWorker.GetDescription(ProductionProcess.NotStarted),
                };
                await Task.Run(() => new ContractModel().AddAsync(contract));
            }
            OrderManagementViewModel.OnItemChanged?.Invoke();
            MainWindow.SendSnackbarAction?.Invoke(GetStatusOrderSnackbarMessage());
        }

        //public override string ToString()
        //{
        //    return $"id:{Item.Id}\n" +
        //           $"Лодка:{Item.Boat.Model}\n" +
        //           $"Покупатель: {Item.Customer.Name} {Item.Customer.SecondName}\n" +
        //           $"Дата оформления: {Item.Date:d}";
        //}

        private string GetStatusOrderSnackbarMessage()
        {
            string os = "";
            switch (_os)
            {
                case OrderStatus.InProcessing:
                    os = "отправлен в обработку";
                    break;
                case OrderStatus.Accepted:
                    os = "принят";
                    break;
                case OrderStatus.Canceled:
                    os = "отменен";
                    break;
                case OrderStatus.Completed:
                    os  = "выполнен";
                    break;
                default:
                    throw new ArgumentException("Ошибка");
            }

            return $"Заказ #{Item.Id} {os}.";
        }

        protected override void ToggleViewEditorAfterLoaded()
        {
            throw new NotImplementedException();
        }

        protected override BaseViewModel GetEditorViewModel()
        {
            throw new NotImplementedException();
        }

        protected override MessageDialogProperty GetConfirmDeleteDialogProperty()
        {
            return new MessageDialogProperty()
            {
                Title = "Подтверждение удаления",
                Message = "Будет удален следующий заказ:\n\n" + this
            };
        }

        #endregion
    }
}