using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Boat
{
    class BoatManagementViewModel:BaseViewModel
    {
        #region Поля

        private readonly IBoatModel _boatModel;
        private readonly IViewModelContainer _viewModelContainer;

        private ObservableCollection<SelectableBoatViewModel> _boats;

        private Visibility _progressBarVisibility;
        private string _filterText;
        #endregion

        #region Конструкторы
        public BoatManagementViewModel(IBoatModel boatModel, IViewModelContainer viewModelContainer)
        {
            _boatModel = boatModel;
            _viewModelContainer = viewModelContainer;

            _boatModel.BoatModelChanged += GetCollectionMethod;
        }

        #endregion

        #region Свойства

        public ObservableCollection<SelectableBoatViewModel> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter();

                return _boats;
            }
        }
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilteredCollection));
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
        /// <summary>
        /// Получение коллекции предметов из БД
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task GetCollectionMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            _boats = GetSelectableViewModels(await _boatModel.GetAllAsync());
            OnPropertyChanged(nameof(FilteredCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }
        private ObservableCollection<SelectableBoatViewModel> GetSelectableViewModels(IEnumerable<Data.Entities.Boat> items)
        {
            var collection = new ObservableCollection<SelectableBoatViewModel>();
            foreach (var boat in items)
            {
                collection.Add(new SelectableBoatViewModel(boat, _boatModel, _viewModelContainer));
            }

            return collection;
        }
        public ObservableCollection<SelectableBoatViewModel> Filter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                return _boats;
            }

            var filteredCollection = _boats.Where(p =>
                p.Boat.Model.ToLower().Contains(FilterText.ToLower()) ||
                p.Boat.ToString() == FilterText);
            
            return new ObservableCollection<SelectableBoatViewModel>(filteredCollection);
        }

        #endregion

        #region Editor

        private async void ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<BoatEditorViewModel>())
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
