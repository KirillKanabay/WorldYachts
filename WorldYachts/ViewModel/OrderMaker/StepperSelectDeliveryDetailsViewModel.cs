using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MaterialDesignExtensions.Model;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Helpers.Validators;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.OrderMaker
{
    class StepperSelectDeliveryDetailsViewModel : Step, IDataErrorInfo
    {
        private readonly OrderContainerViewModel _orderViewModel;

        public StepperSelectDeliveryDetailsViewModel(OrderContainerViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
        }

        public string DeliveryAddress
        {
            get => _orderViewModel.DeliveryAddress;
            set
            {
                _orderViewModel.DeliveryAddress = value;
                OnPropertyChanged(nameof(DeliveryAddress));
            }
        }

        public string City
        {
            get => _orderViewModel.City;
            set
            {
                _orderViewModel.City = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public bool ButtonIsEnabled => !_errors.Any();
        
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();
        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                string error = columnName switch
                {
                    nameof(City) => new Validation(new NotEmptyFieldValidationRule(City)).Validate(),
                    nameof(DeliveryAddress) => new Validation(new NotEmptyFieldValidationRule(DeliveryAddress))
                        .Validate(),
                    _ => null
                };
                _errors.Remove(columnName);
                if (!string.IsNullOrWhiteSpace(error))
                    _errors.Add(columnName, error);
                OnPropertyChanged(nameof(ButtonIsEnabled));
                return error;
            }
        }
    }
}
