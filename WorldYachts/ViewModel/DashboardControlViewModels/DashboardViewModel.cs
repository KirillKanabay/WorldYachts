using WorldYachts.Data;
using WorldYachts.Services;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.DashboardControlViewModels
{
    public class DashboardViewModel:BaseViewModel
    {
        #region Поля
        private readonly IUser _user;

        private string _typeOfUser;

       
        #endregion

        #region Конструкторы

        public DashboardViewModel()
        {
            var authUser = AuthUser.GetInstance();
            _user = authUser.User;
            
            _typeOfUser = AuthUser.GetInstance().Role;
        }

        //#endregion

        //#region Свойства

        public string Name => _user.FirstName;

        public string SecondName => _user.SecondName;

        //public string TypeUser
        //{
        //    get => _typeOfUser;
        //    set
        //    {
        //        _typeOfUser = value;
        //        OnPropertyChanged(nameof(TypeUser));
        //    }
        //}

        ///// <summary>
        ///// Количество денег потраченных покупателем
        ///// </summary>
        ////public decimal SpentSum
        ////{
        ////    get
        ////    {
        ////        if (AuthUser.TypeOfUser == TypeOfUser.Customer)
        ////        {
        ////            return new InvoiceModel()
        ////                .Load()
        ////                .Where(i => i.Contract.Order.CustomerId == AuthUser.User.Id && i.Settled)
        ////                .Select(i => i.Sum)
        ////                .Sum();
        ////        }
        ////        else if (AuthUser.TypeOfUser == TypeOfUser.SalesPerson)
        ////        {
        ////            return new InvoiceModel()
        ////                .Load()
        ////                .Where(i => i.Contract.Order.SalesPersonId == AuthUser.User.Id && i.Settled)
        ////                .Select(i => i.Sum)
        ////                .Sum();
        ////        }
        ////        else
        ////        {
        ////            return new InvoiceModel()
        ////                .Load()
        ////                .Where(i => i.Settled)
        ////                .Select(i=>i.Sum)
        ////                .Sum();
        ////        }
        ////    }
        ////}

        ///// <summary>
        ///// Количество контрактов
        ///// </summary>
        ////public int ContractСount
        //{
        //    get
        //    {
        //        if (AuthUser.TypeOfUser == TypeOfUser.Customer)
        //        {
        //            return new ContractModel()
        //                .Load()
        //                .Count(i => i.Order.CustomerId == AuthUser.User.Id);
        //        }
        //        else if (AuthUser.TypeOfUser == TypeOfUser.SalesPerson)
        //        {
        //            return new ContractModel()
        //                .Load()
        //                .Count(i => i.Order.SalesPersonId == AuthUser.User.Id);
        //        }
        //        else
        //        {
        //            return new ContractModel().Load().Count();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Общая стоимость контрактов
        ///// </summary>
        //public decimal ContractsPrice
        //{
        //    get
        //    {
        //        if (AuthUser.TypeOfUser == TypeOfUser.Customer)
        //        {
        //            return new ContractModel()
        //                .Load()
        //                .Where(i => i.Order.CustomerId == AuthUser.User.Id)
        //                .Select(i=>i.ContractTotalPriceInclVat)
        //                .Sum();
        //        }
        //        else if (AuthUser.TypeOfUser == TypeOfUser.SalesPerson)
        //        {
        //            return new ContractModel()
        //                .Load()
        //                .Where(i => i.Order.SalesPersonId == AuthUser.User.Id)
        //                .Select(i => i.ContractTotalPriceInclVat)
        //                .Sum();
        //        }
        //        else
        //        {
        //            return new ContractModel().Load().Select(i => i.ContractTotalPriceInclVat).Sum();
        //        }
        //    }
        //}
        ///// <summary>
        ///// Количество оформленных заказов
        ///// </summary>
        //public int OrderCount
        //{
        //    get
        //    {
        //        if (AuthUser.TypeOfUser == TypeOfUser.Customer)
        //        {
        //            return new OrderModel()
        //                .Load()
        //                .Count(i => i.CustomerId == AuthUser.User.Id);
        //        }
        //        else if (AuthUser.TypeOfUser == TypeOfUser.SalesPerson)
        //        {
        //            return new OrderModel()
        //                .Load()
        //                .Count(i => i.SalesPersonId == AuthUser.User.Id);
        //        }
        //        else
        //        {
        //            return new OrderModel().Load().Count();
        //        }
        //    }
        //}
        ///// <summary>
        ///// Непогашенная сумма за контракты
        ///// </summary>
        //public decimal NotPayedDeposit
        //{
        //    get
        //    {
        //        return ContractsPrice - SpentSum;
        //    }
        //}

        #endregion


    }
}
