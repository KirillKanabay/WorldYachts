using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;
using WorldYachts.ViewModel.Users.SalesPersons;

namespace WorldYachts.ViewModel.Users.Customers
{
    class CustomerManagementViewModel:BaseViewModel
    {
        #region Поля
        private readonly ICustomerModel _customerModel;
        private readonly IViewModelContainer _viewModelContainer;

        private ObservableCollection<SelectableCustomerViewModel> _customers;

        private Visibility _progressBarVisibility;
        private string _filterText;

        #endregion

        #region Конструкторы

        public CustomerManagementViewModel(ICustomerModel customerModel, IViewModelContainer viewModelContainer)
        {
            _customerModel = customerModel;
            _viewModelContainer = viewModelContainer;

            _customerModel.CustomerModelChanged += GetCollectionMethod;
        }

        #endregion

        #region Свойства

        public ObservableCollection<SelectableCustomerViewModel> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter();

                return _customers;
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                if (_filterText != null)
                {
                    OnPropertyChanged(nameof(FilteredCollection));
                }
            }
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

        #endregion

        #region Команды
        /// <summary>
        /// Команда получения коллекции предметов
        /// </summary>
        public AsyncRelayCommand GetItemsCollection => new AsyncRelayCommand(GetCollectionMethod,
            (ex) => { ExecuteRunDialog(new MessageDialogProperty() { Title = "Ошибка", Message = ex.Message }); });

        /// <summary>
        /// Команда открытия редактора
        /// </summary>
        public DelegateCommand OpenEditorDialog => new DelegateCommand(ExecuteRunEditorDialog);



        #endregion

        #region Методы

        private async Task GetCollectionMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            _customers = GetSelectableViewModels(await _customerModel.GetAllAsync());
            OnPropertyChanged(nameof(FilteredCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }

        private ObservableCollection<SelectableCustomerViewModel> GetSelectableViewModels(IEnumerable<Customer> items)
        {
            var collection = new ObservableCollection<SelectableCustomerViewModel>();
            foreach (var customer in items)
            {
                collection.Add(new SelectableCustomerViewModel(customer, _customerModel, _viewModelContainer));
            }

            return collection;
        }

        private ObservableCollection<SelectableCustomerViewModel> Filter()
        {
            var filteredCollection = _customers.Where(c =>
                c.Customer.FirstName.ToLower().Contains(_filterText.ToLower()) ||
                c.Customer.SecondName.ToLower().Contains(_filterText.ToLower()) ||
                c.Customer.Id.ToString() == _filterText);

            return new ObservableCollection<SelectableCustomerViewModel>(filteredCollection);
        }

        #endregion

        #region Редактор

        /// <summary>
        /// Запуск редактора
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<CustomerEditorViewModel>())
            };

            var result = await DialogHost.Show(view, "RootDialog");
        }

        public async void ExecuteRunDialog(object o)
        {
            var view = new SampleMessageDialog()
            {
                DataContext = new SampleMessageDialogViewModel((MessageDialogProperty)o)
            };
            var result = await DialogHost.Show(view, "DialogRoot");
        }

        #endregion
    }
}
