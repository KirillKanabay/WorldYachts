using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.BoatManagementViewModels;

namespace WorldYachts.ViewModel.CatalogControlViewModels
{
    public class CatalogControlViewModel : BaseManagementViewModel<Boat>
    {
        #region Поля

        private ObservableCollection<BaseSelectableViewModel<Boat>> _boats;

        private decimal? _defaultPriceFromFilter = 0;
        private decimal? _defaultPriceToFilter = 0;
        private string _defaultTypeFilter = "Любой тип";
        private string _defaultWoodFilter = "Любой тип";
        private string _defaultMastFilter = "Любой";

        private decimal _priceFromFilter = 0;
        private decimal _priceToFilter = 0;
        private string _typeFilter = "Любой тип";
        private string _woodFilter = "Любой тип";
        private string _mastFilter = "Любой";

        private DelegateCommand _setDefaultFilter;

        private readonly IEnumerable<string> _boatTypes = new List<string>()
            {"Любой тип", "Шлюпка", "Парусная лодка", "Галера"};

        private readonly IEnumerable<string> _woodTypes = new List<string>()
            {"Любой тип", "Дуб", "Береза", "Eль", "Cосна", "Лиственница"};

        private readonly IEnumerable<string> _mastTypes = new List<string>()
            {"Любой", "Присутствует", "Отсутствует"};

        private readonly BoatViewModel _boatViewModel;
        #endregion

        #region Конструктор

        public CatalogControlViewModel():base(null, null)
        {
            
        }
        public CatalogControlViewModel(BoatViewModel boatViewModel):base(null, null)
        {
            _boatViewModel = boatViewModel;
        }

        #endregion

        #region Свойства

        public override ObservableCollection<BaseSelectableViewModel<Boat>> FilteredCollection => Filter(_filterText);

        /// <summary>
        /// Фильтр: цена с
        /// </summary>
        public decimal PriceFromFilter
        {
            get => _priceFromFilter;
            set
            {
                _priceFromFilter = value;
                OnPropertyChanged(nameof(PriceFromFilter));
                OnPropertyChanged(nameof(FilteredCollection));
            }
        }

        /// <summary>
        /// Фильтр: цена до
        /// </summary>
        public decimal PriceToFilter
        {
            get => _priceToFilter;
            set
            {
                _priceToFilter = value;
                OnPropertyChanged(nameof(PriceToFilter));
                OnPropertyChanged(nameof(FilteredCollection));
            }
        }

        /// <summary>
        /// Фильтр: тип лодки
        /// </summary>
        public string TypeFilter
        {
            get => _typeFilter;
            set
            {
                _typeFilter = value;
                OnPropertyChanged(nameof(TypeFilter));
                OnPropertyChanged(nameof(FilteredCollection));
            }
        }

        public string WoodFilter
        {
            get => _woodFilter;
            set
            {
                _woodFilter = value;
                OnPropertyChanged(nameof(WoodFilter));
                OnPropertyChanged(nameof(FilteredCollection));
            }
        }

        /// <summary>
        /// Фильтр: наличие лодки
        /// </summary>
        public string MastFilter
        {
            get => _mastFilter;
            set
            {
                _mastFilter = value;
                OnPropertyChanged(nameof(MastFilter));
                OnPropertyChanged(nameof(FilteredCollection));
            }
        }

        public IEnumerable<string> BoatTypes => _boatTypes;
        public IEnumerable<string> WoodTypes => _woodTypes;
        public IEnumerable<string> MastTypes => _mastTypes;

        #endregion

        #region Команды

        public DelegateCommand SetDefaultFilter
        {
            get
            {
                return _setDefaultFilter ??= new DelegateCommand((arg) =>
                {
                    PriceFromFilter = _defaultPriceFromFilter ?? 0;
                    PriceToFilter = _defaultPriceToFilter ?? 0;
                    TypeFilter = _defaultTypeFilter;
                    WoodFilter = _defaultWoodFilter;
                    MastFilter = _defaultMastFilter;
                    OnPropertyChanged(nameof(FilteredCollection));
                });
            }
        }

        #endregion

        #region Методы

        protected override async Task GetCollectionMethod(object parameter)
        {
            ProgressBarVisibility = Visibility.Visible;
            var items = await _dataModel.GetAllAsync();
            
            ItemsCollection = GetSelectableViewModels(items);

            //Установка фильтров
            _defaultPriceFromFilter = ItemsCollection?.Min(b => b.Item.BasePrice) ?? 0;
            _defaultPriceToFilter = ItemsCollection?.Max(b => b.Item.BasePrice) ?? 0;
            SetDefaultFilter?.Execute(null);

            //OnPropertyChanged(nameof(FilteredCollection));
            
            ProgressBarVisibility = Visibility.Collapsed;
        }

        protected override ObservableCollection<BaseSelectableViewModel<Boat>> GetSelectableViewModels(
            IEnumerable<Boat> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Boat>>();

            foreach (var boat in items)
            {
                collection.Add(new SelectableBoatViewModel(boat,_boatViewModel));
            }

            return collection;
        }

        protected override ObservableCollection<BaseSelectableViewModel<Boat>> Filter(string filterText)
        {
            var fc = ItemsCollection.Where(b => true);

            //Фильтрация по поисковой строке
            if (!string.IsNullOrWhiteSpace(filterText))
            {
                fc = fc.Where(p =>
                    p.Item.Model.ToLower().Contains(filterText.ToLower()));
            }

            //Фильтрация по ценам
            fc = fc.Where(i => i.Item.BasePrice >= _priceFromFilter);
            fc = fc.Where(i => i.Item.BasePrice <= _priceToFilter);

            //Фильтрация по типу лодки
            if (_typeFilter != "Любой тип")
            {
                fc = fc.Where(b => b.Item.BoatType.Type == _typeFilter);
            }

            if (_woodFilter != "Любой тип")
            {
                fc = fc.Where(b => b.Item.BoatWood.Wood == _woodFilter);
            }

            //Фильтрация по наличию мачты
            if (_mastFilter != "Любой")
            {
                bool mast = _mastFilter == "Присутствует";
                fc = fc.Where(b => b.Item.Mast == mast);
            }

            return new ObservableCollection<BaseSelectableViewModel<Boat>>(fc);
        }

        #endregion

    }
}