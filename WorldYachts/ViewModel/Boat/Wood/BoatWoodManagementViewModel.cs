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

namespace WorldYachts.ViewModel.Boat.Wood
{
    public class BoatWoodManagementViewModel:BaseViewModel
    {
        #region Поля

        private readonly IBoatWoodModel _boatWoodModel;
        private readonly IViewModelContainer _viewModelContainer;

        private ObservableCollection<SelectableBoatWoodViewModel> _boatWoods;

        private Visibility _progressBarVisibility;
        private string _filterText;

        #endregion

        #region Конструкторы
        public BoatWoodManagementViewModel(IBoatWoodModel boatWoodModel, IViewModelContainer viewModelContainer)
        {
            _boatWoodModel = boatWoodModel;
            _viewModelContainer = viewModelContainer;

            _boatWoodModel.BoatWoodModelChanged += GetCollectionMethod;
        }

        #endregion

        #region Свойства

        public ObservableCollection<SelectableBoatWoodViewModel> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter();

                return _boatWoods;
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
            _boatWoods = GetSelectableViewModels(await _boatWoodModel.GetAllAsync());
            OnPropertyChanged(nameof(FilteredCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }
        private ObservableCollection<SelectableBoatWoodViewModel> GetSelectableViewModels(IEnumerable<Data.Entities.BoatWood> items)
        {
            var collection = new ObservableCollection<SelectableBoatWoodViewModel>();
            foreach (var boatWood in items)
            {
                collection.Add(new SelectableBoatWoodViewModel(boatWood, _boatWoodModel, _viewModelContainer));
            }

            return collection;
        }
        public ObservableCollection<SelectableBoatWoodViewModel> Filter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                return _boatWoods;
            }

            var filteredCollection = _boatWoods.Where(pt =>
                pt.BoatWood.Wood.ToLower().Contains(FilterText.ToLower()) ||
                pt.BoatWood.Id.ToString() == FilterText);

            return new ObservableCollection<SelectableBoatWoodViewModel>(filteredCollection);
        }

        #endregion

        #region Редактор

        private async void ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<BoatWoodEditorViewModel>())
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
