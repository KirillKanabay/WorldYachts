using System.Collections.Generic;
using MaterialDesignExtensions.Model;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderMaker
{
    class OrderMakerViewModel : BaseViewModel
    {
        private readonly StepperSelectCustomerViewModel _stepperSelectCustomerViewModel;
        private readonly StepperSelectBoatViewModel _stepperSelectBoatViewModel;
        private readonly StepperSelectAccessoriesViewModel _stepperSelectAccessoriesViewModel;
        private readonly StepperSelectDeliveryDetailsViewModel _stepperSelectDeliveryDetailsViewModel;
        private readonly StepperConfirmOrderViewModel _stepperConfirmOrderViewModel;

        public OrderMakerViewModel(StepperSelectCustomerViewModel stepperSelectCustomerViewModel,
            StepperSelectBoatViewModel stepperSelectBoatViewModel,
            StepperSelectAccessoriesViewModel stepperSelectAccessoriesViewModel,
            StepperSelectDeliveryDetailsViewModel stepperSelectDeliveryDetailsViewModel,
            StepperConfirmOrderViewModel stepperConfirmOrderViewModel)
        {
            _stepperSelectCustomerViewModel = stepperSelectCustomerViewModel;
            _stepperSelectBoatViewModel = stepperSelectBoatViewModel;
            _stepperSelectAccessoriesViewModel = stepperSelectAccessoriesViewModel;
            _stepperSelectDeliveryDetailsViewModel = stepperSelectDeliveryDetailsViewModel;
            _stepperConfirmOrderViewModel = stepperConfirmOrderViewModel;
        }

        public List<IStep> Steps =>
            new List<IStep>()
            {
                new Step()
                {
                    Header = new StepTitleHeader() {FirstLevelTitle = "Клиент"},
                    Content = _stepperSelectCustomerViewModel
                },
                new Step()
                {
                    Header = new StepTitleHeader() {FirstLevelTitle = "Лодка"},
                    Content = _stepperSelectBoatViewModel
                },
                new Step()
                {
                    Header = new StepTitleHeader() {FirstLevelTitle = "Аксессуары"},
                    Content = _stepperSelectAccessoriesViewModel
                },
                new Step()
                {
                    Header = new StepTitleHeader() {FirstLevelTitle = "Детали доставки"},
                    Content = _stepperSelectDeliveryDetailsViewModel
                },
                new Step()
                {
                    Header = new StepTitleHeader() {FirstLevelTitle = "Подтверждение заказа"},
                    Content = _stepperConfirmOrderViewModel
                },
            };
    }
}