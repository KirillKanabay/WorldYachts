using System.Collections.Generic;
using MaterialDesignExtensions.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderMaker
{
    class OrderMakerViewModel:BaseViewModel
    {
        public List<IStep> Steps =>
            new List<IStep>()
            {
                new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Клиент" }, Content = new StepperSelectCustomerViewModel() },
                new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Лодка" }, Content = new StepperSelectBoatViewModel() },
                new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Аксессуары"}, Content = new StepperSelectAccessoriesViewModel() },
                new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Детали доставки" }, Content = new StepperSelectDeliveryDetailsViewModel() },
                new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Подтверждение заказа" }, Content = new StepperConfirmOrderViewModel() },
            };
    }
}
