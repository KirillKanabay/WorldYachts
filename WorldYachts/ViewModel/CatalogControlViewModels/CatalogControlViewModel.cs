using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.BoatManagementViewModels;

namespace WorldYachts.ViewModel.CatalogControlViewModels
{
    class CatalogControlViewModel : BaseManagementViewModel<Boat>
    {
        #region Поля

        private ObservableCollection<BaseSelectableViewModel<Boat>> _boats;

        private decimal _defaultPriceFromFilter = new BoatModel().Load().Min(b => b.BasePrice);
        private decimal _defaultPriceToFilter = new BoatModel().Load().Max(b => b.BasePrice);
        private string _defaultTypeFilter = "Любой тип";
        private string _defaultWoodFilter = "Любой тип";
        private string _defaultMastFilter = "Любой";

        private decimal _priceFromFilter = new BoatModel().Load().Min(b => b.BasePrice);
        private decimal _priceToFilter = new BoatModel().Load().Max(b => b.BasePrice);
        private string _typeFilter = "Любой тип";
        private string _woodFilter = "Любой тип";
        private string _mastFilter = "Любой";

        private DelegateCommand _setDefaultFilter;

        private IEnumerable<string> _boatTypes = new List<string>()
            {"Любой тип", "Шлюпка", "Парусная лодка", "Галера"};

        private IEnumerable<string> _woodTypes = new List<string>()
            {"Любой тип", "Дуб", "Береза", "Eль", "Cосна", "Лиственница"};

        private IEnumerable<string> _mastTypes = new List<string>()
            {"Любой", "Присутствует", "Отсутствует"};

        #endregion

        #region Конструктор

        public CatalogControlViewModel()
        {
            SetDefaultFilter?.Execute(null);
            OnItemChanged?.Invoke();
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
        public override IDataModel<Boat> ModelItem => new BoatModel();
        public override BaseEditorViewModel<Boat> Editor => new BoatEditorViewModel();

        #endregion

        #region Команды

        public DelegateCommand SetDefaultFilter
        {
            get
            {
                return _setDefaultFilter ??= new DelegateCommand((arg) =>
                {
                    PriceFromFilter = _defaultPriceFromFilter;
                    PriceToFilter = _defaultPriceToFilter;
                    TypeFilter = _defaultTypeFilter;
                    WoodFilter = _defaultWoodFilter;
                    MastFilter = _defaultMastFilter;
                    OnPropertyChanged(nameof(FilteredCollection));
                });
            }
        }

        #endregion

        #region Методы

        protected override ObservableCollection<BaseSelectableViewModel<Boat>> GetSelectableViewModels(
            IEnumerable<Boat> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Boat>>();

            foreach (var boat in items)
            {
                collection.Add(new SelectableBoatViewModel(boat));
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
                fc = fc.Where(b => b.Item.Type == _typeFilter);
            }

            if (_woodFilter != "Любой тип")
            {
                fc = fc.Where(b => b.Item.Wood == _woodFilter);
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