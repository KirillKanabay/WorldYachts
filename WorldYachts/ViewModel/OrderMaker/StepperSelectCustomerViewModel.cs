using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.MessageDialog;
using WorldYachts.ViewModel.OrderControlViewModels;

namespace WorldYachts.ViewModel.OrderMaker
{
    public class StepperSelectCustomerViewModel:Step
    {
        #region Поля

        private readonly OrderContainerViewModel _orderViewModel;
        private readonly ICustomerModel _customerModel;
        private Visibility _progressBarVisibility = Visibility.Collapsed;
        private List<Customer> _customers;
        private int _selectedCustomerIndex;
        #endregion
        public StepperSelectCustomerViewModel(ICustomerModel customerModel, OrderContainerViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
            _customerModel = customerModel;
        }

        #region Свойства

        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged(nameof(ProgressBarVisibility));
            }
        }

        public List<Customer> CustomersCollection
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(CustomersCollection));
            }
        }

        public int SelectedCustomerIndex
        {
            get => _selectedCustomerIndex;
            set
            {
                _selectedCustomerIndex = value;
                OnPropertyChanged(nameof(SelectedCustomerIndex));
            }
        }

        public Customer SelectedCustomer
        {
            get => _orderViewModel.Customer;
            set
            {
                _orderViewModel.Customer = value;
                OnPropertyChanged(nameof(CustomerInfo));
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        public string CustomerInfo => _orderViewModel.Customer?.ToString();
        #endregion
        
        public AsyncRelayCommand LoadedCommand => new AsyncRelayCommand(LoadCollections,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message }); });
        
        #region Методы

        public async Task LoadCollections(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            CustomersCollection = (await _customerModel.GetAllAsync()).ToList();
            OnPropertyChanged(nameof(CustomersCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "DialogRoot");
        }

        public override void Validate()
        {
            HasValidationErrors = _orderViewModel.Customer == null;
        }

        #endregion
    }
}
