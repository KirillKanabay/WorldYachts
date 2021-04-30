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
using WorldYachts.ViewModel.Boat.Type;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.Boat.BoatType
{
    public class BoatTypeManagementViewModel:BaseViewModel
    {
        #region Поля

        private readonly IBoatTypeModel _boatTypeModel;
        private readonly IViewModelContainer _viewModelContainer;

        private ObservableCollection<SelectableBoatTypeViewModel> _boatTypes;

        private Visibility _progressBarVisibility;
        private string _filterText;

        #endregion

        #region Конструкторы
        public BoatTypeManagementViewModel(IBoatTypeModel boatTypeModel, IViewModelContainer viewModelContainer)
        {
            _boatTypeModel = boatTypeModel;
            _viewModelContainer = viewModelContainer;

            _boatTypeModel.BoatTypeModelChanged += GetCollectionMethod;
        }

        #endregion

        #region Свойства

        public ObservableCollection<SelectableBoatTypeViewModel> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter();

                return _boatTypes;
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
            _boatTypes = GetSelectableViewModels(await _boatTypeModel.GetAllAsync());
            OnPropertyChanged(nameof(FilteredCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }
        private ObservableCollection<SelectableBoatTypeViewModel> GetSelectableViewModels(IEnumerable<Data.Entities.BoatType> items)
        {
            var collection = new ObservableCollection<SelectableBoatTypeViewModel>();
            foreach (var boatType in items)
            {
                collection.Add(new SelectableBoatTypeViewModel(boatType, _boatTypeModel, _viewModelContainer));
            }

            return collection;
        }
        public ObservableCollection<SelectableBoatTypeViewModel> Filter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                return _boatTypes;
            }

            var filteredCollection = _boatTypes.Where(pt =>
                pt.BoatType.Type.ToLower().Contains(FilterText.ToLower()) ||
                pt.BoatType.Id.ToString() == FilterText);

            return new ObservableCollection<SelectableBoatTypeViewModel>(filteredCollection);
        }

        #endregion

        #region Редактор

        private async void ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<BoatTypeEditorViewModel>())
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
