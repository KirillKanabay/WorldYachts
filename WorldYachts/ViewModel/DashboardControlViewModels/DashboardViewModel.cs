
using System.Linq;
using System.Windows;
using WorldYachts.Data;
using WorldYachts.Infrastructure;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.DashboardControlViewModels
{
    class DashboardViewModel:BaseViewModel
    {
        #region Поля
        private IUser _user;
        
        private string _name;
        private string _secondName;
        private string _typeOfUser;
        #endregion

        #region Конструкторы

        public DashboardViewModel()
        {
            _user = AuthUser.User;
            _name = _user.Name;
            _secondName = _user.SecondName;

            switch (AuthUser.TypeOfUser)
            {
                case TypeOfUser.Admin:
                    _typeOfUser = "Администратор WorldYachts";
                    break;
                case TypeOfUser.Customer:
                    _typeOfUser = "Клиент WorldYachts";
                    break;
                case TypeOfUser.SalesPerson:
                    _typeOfUser = "Менеджер WorldYachts";
                    break;
            }
        }

        #endregion

        #region Свойства

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string SecondName
        {
            get => _secondName;
            set
            {
                _secondName = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }

        public string TypeUser
        {
            get => _typeOfUser;
            set
            {
                _typeOfUser = value;
                OnPropertyChanged(nameof(TypeUser));
            }
        }
        
        /// <summary>
        /// Количество денег потраченных покупателем
        /// </summary>
        public decimal SpentSum
        {
            get
            {
                if (AuthUser.TypeOfUser == TypeOfUser.Customer)
                {
                    return new InvoiceModel()
                        .Load()
                        .Where(i => i.Contract.Order.CustomerId == AuthUser.User.Id && i.Settled)
                        .Select(i => i.Sum)
                        .Sum();
                }
                else if (AuthUser.TypeOfUser == TypeOfUser.SalesPerson)
                {
                    return new InvoiceModel()
                        .Load()
                        .Where(i => i.Contract.Order.SalesPersonId == AuthUser.User.Id && i.Settled)
                        .Select(i => i.Sum)
                        .Sum();
                }
                else
                {
                    return new InvoiceModel()
                        .Load()
                        .Where(i => i.Settled)
                        .Select(i=>i.Sum)
                        .Sum();
                }
            }
        }
        
        /// <summary>
        /// Количество контрактов
        /// </summary>
        public int ContractСount
        {
            get
            {
                if (AuthUser.TypeOfUser == TypeOfUser.Customer)
                {
                    return new ContractModel()
                        .Load()
                        .Count(i => i.Order.CustomerId == AuthUser.User.Id);
                }
                else if (AuthUser.TypeOfUser == TypeOfUser.SalesPerson)
                {
                    return new ContractModel()
                        .Load()
                        .Count(i => i.Order.SalesPersonId == AuthUser.User.Id);
                }
                else
                {
                    return new ContractModel().Load().Count();
                }
            }
        }

        /// <summary>
        /// Общая стоимость контрактов
        /// </summary>
        public decimal ContractsPrice
        {
            get
            {
                if (AuthUser.TypeOfUser == TypeOfUser.Customer)
                {
                    return new ContractModel()
                        .Load()
                        .Where(i => i.Order.CustomerId == AuthUser.User.Id)
                        .Select(i=>i.ContractTotalPriceInclVat)
                        .Sum();
                }
                else if (AuthUser.TypeOfUser == TypeOfUser.SalesPerson)
                {
                    return new ContractModel()
                        .Load()
                        .Where(i => i.Order.SalesPersonId == AuthUser.User.Id)
                        .Select(i => i.ContractTotalPriceInclVat)
                        .Sum();
                }
                else
                {
                    return new ContractModel().Load().Select(i => i.ContractTotalPriceInclVat).Sum();
                }
            }
        }
        /// <summary>
        /// Количество оформленных заказов
        /// </summary>
        public int OrderCount
        {
            get
            {
                if (AuthUser.TypeOfUser == TypeOfUser.Customer)
                {
                    return new OrderModel()
                        .Load()
                        .Count(i => i.CustomerId == AuthUser.User.Id);
                }
                else if (AuthUser.TypeOfUser == TypeOfUser.SalesPerson)
                {
                    return new OrderModel()
                        .Load()
                        .Count(i => i.SalesPersonId == AuthUser.User.Id);
                }
                else
                {
                    return new OrderModel().Load().Count();
                }
            }
        }
        /// <summary>
        /// Непогашенная сумма за контракты
        /// </summary>
        public decimal NotPayedDeposit
        {
            get
            {
                return ContractsPrice - SpentSum;
            }
        }
        #endregion


    }
}
