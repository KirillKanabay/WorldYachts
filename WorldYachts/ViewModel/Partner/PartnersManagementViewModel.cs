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

namespace WorldYachts.ViewModel.Partner
{
    class PartnersManagementViewModel : BaseViewModel
    {
        #region Поля

        private readonly IPartnerModel _partnerModel;
        private readonly IViewModelContainer _viewModelContainer;

        private ObservableCollection<SelectablePartnerViewModel> _partners;

        private Visibility _progressBarVisibility;
        private string _filterText;

        #endregion

        #region Конструкторы

        public PartnersManagementViewModel(IPartnerModel partnerModel, IViewModelContainer viewModelContainer)
        {
            _partnerModel = partnerModel;
            _viewModelContainer = viewModelContainer;

            _partnerModel.PartnerModelChanged += GetCollectionMethod;
        }

        #endregion

        #region Свойства

        public virtual ObservableCollection<SelectablePartnerViewModel> FilteredCollection
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_filterText))
                    return Filter(_filterText);

                return _partners;
            }
        }

        public virtual string FilterText
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

        /// <summary>
        /// Получение коллекции предметов из БД
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task GetCollectionMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            _partners = GetSelectableViewModels(await _partnerModel.GetAllAsync());
            OnPropertyChanged(nameof(FilteredCollection));
            ProgressBarVisibility = Visibility.Collapsed;
        }

        private ObservableCollection<SelectablePartnerViewModel> GetSelectableViewModels(IEnumerable<Data.Entities.Partner> items)
        {
            var collection = new ObservableCollection<SelectablePartnerViewModel>();
            foreach (var accessory in items)
            {
                collection.Add(new SelectablePartnerViewModel(accessory, _partnerModel, _viewModelContainer));
            }

            return collection;
        }

        private ObservableCollection<SelectablePartnerViewModel> Filter(string filterText)
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                return _partners;
            }

            var filteredCollection = _partners.Where(a =>
                a.Partner.Name.ToLower().Contains(filterText.ToLower()) ||
                a.Partner.Id.ToString() == filterText);

            return new ObservableCollection<SelectablePartnerViewModel>(filteredCollection);
        }

        #endregion

        #region Editor

        /// <summary>
        /// Запуск редактора
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteRunEditorDialog(object o)
        {
            var view = new View.MessageDialogs.MessageDialog()
            {
                DataContext = new MessageDialogViewModel(_viewModelContainer.GetViewModel<PartnerEditorViewModel>())
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