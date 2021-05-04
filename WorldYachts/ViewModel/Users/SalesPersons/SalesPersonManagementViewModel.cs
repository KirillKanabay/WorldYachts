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
using WorldYachts.ViewModel.Partner;

namespace WorldYachts.ViewModel.Users.SalesPersons
{
    class SalesPersonManagementViewModel:BaseViewModel
    {

        #region Поля
        private readonly ISalesPersonModel _salesPersonModel;
        private readonly IViewModelContainer _viewModelContainer;

        private ObservableCollection<SelectableSalesPersonViewModel> _salesPersons;

        private Visibility _progressBarVisibility;
        private string _filterText;
        #endregion

        #region Конструкторы

        public SalesPersonManagementViewModel(ISalesPersonModel salesPersonModel, IViewModelContainer viewModelContainer)
        {
            _salesPersonModel = salesPersonModel;
            _viewModelContainer = viewModelContainer;

            _salesPersonModel.SalesPersonModelChanged += GetCollectionMethod;
        }
        #endregion

        #region Свойства

        public ObservableCollection<SelectableSalesPersonViewModel> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter();

                return _salesPersons;
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
            _salesPersons = GetSelectableViewModels(await _salesPersonModel.GetAllAsync());
            OnPropertyChanged(nameof(FilteredCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }

        private ObservableCollection<SelectableSalesPersonViewModel> GetSelectableViewModels(IEnumerable<SalesPerson> items)
        {
            var collection = new ObservableCollection<SelectableSalesPersonViewModel>();
            foreach (var sp in items)
            {
                collection.Add(new SelectableSalesPersonViewModel(sp, _salesPersonModel, _viewModelContainer));
            }

            return collection;
        }

        private ObservableCollection<SelectableSalesPersonViewModel> Filter()
        {
            var filteredCollection = _salesPersons.Where(sp =>
                sp.SalesPerson.FirstName.ToLower().Contains(_filterText.ToLower()) ||
                sp.SalesPerson.SecondName.ToLower().Contains(_filterText.ToLower()) ||
                sp.SalesPerson.Id.ToString() == _filterText);

            return new ObservableCollection<SelectableSalesPersonViewModel>(filteredCollection);
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
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<SalesPersonEditorViewModel>())
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
