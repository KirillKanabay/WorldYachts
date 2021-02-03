using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Model;
using WorldYachts.ViewModel.BoatManagementViewModels;

namespace WorldYachts.ViewModel.CatalogControlViewModels
{
    class CatalogControlViewModel:BaseManagementViewModel<Boat>
    {
        #region Поля

        private ObservableCollection<BaseSelectableViewModel<Boat>> _boats;

        private decimal _priceFromFilter;
        private decimal _priceToFilter;
        private string _typeFilter;
        private bool _mastFilter;
        private bool _isFilter = false;

        private DelegateCommand _filterCommand;
        #endregion

        #region Конструктор

        public CatalogControlViewModel()
        {
            OnItemChanged?.Invoke();
        }

        #endregion

        #region Свойства

        public override ObservableCollection<BaseSelectableViewModel<Boat>> FilteredCollection
        {
            get
            {
                if (_isFilter)
                {
                    _isFilter = false;
                    if (!string.IsNullOrWhiteSpace(FilterText))
                        return Filter(FilterText, FilterProperty());
                    return FilterProperty();
                }
                if (!string.IsNullOrWhiteSpace(FilterText))
                    return Filter(FilterText);

                return ItemsCollection;
            }
        }

        public ObservableCollection<BaseSelectableViewModel<Boat>> Boats
        {
            get => FilteredCollection;
            set
            {
                _boats = value;
                OnPropertyChanged(nameof(Boats));
            }
        }
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
            }
        }
        /// <summary>
        /// Фильтр: наличие лодки
        /// </summary>
        public bool MastFilter
        {
            get => _mastFilter;
            set
            {
                _mastFilter = value;
                OnPropertyChanged(nameof(MastFilter));
            }
        }
        public override IDataModel<Boat> ModelItem => new BoatModel();
        public override BaseEditorViewModel<Boat> Editor => new BoatEditorViewModel();

        #endregion

        #region Команды

        public DelegateCommand FilterCommand
        {
            get
            {
                return _filterCommand ??= new DelegateCommand((arg) =>
                {
                    _isFilter = true;
                    OnItemChanged?.Invoke();
                });
            }
        }

        #endregion

        protected override ObservableCollection<BaseSelectableViewModel<Boat>> GetSelectableViewModels(IEnumerable<Boat> items)
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
            return Filter(filterText, ItemsCollection);
        }

        private ObservableCollection<BaseSelectableViewModel<Boat>> Filter(string filterText,
            ObservableCollection<BaseSelectableViewModel<Boat>> collection)
        {
            var filteredCollection = collection.Where(p =>
                p.Item.Model.ToLower().Contains(filterText.ToLower()));

            if (_isFilter)
            {
                ;
                _isFilter = false;
            }

            var boatsCollection = new ObservableCollection<BaseSelectableViewModel<Boat>>();
            foreach (var selectableBoatViewModel in filteredCollection)
            {
                boatsCollection.Add(selectableBoatViewModel);
            }

            return boatsCollection;
        }

        private ObservableCollection<BaseSelectableViewModel<Boat>> FilterProperty()
        {
            var filteredCollection = ItemsCollection.Where(i => i.Item.BasePrice >= _priceFromFilter)
                .Where(i => i.Item.BasePrice <= _priceToFilter)
                .Where(i => i.Item.Type == TypeFilter)
                .Where(i => i.Item.Mast == MastFilter);

            var boatsCollection = new ObservableCollection<BaseSelectableViewModel<Boat>>();
            foreach (var selectableBoatViewModel in filteredCollection)
            {
                boatsCollection.Add(selectableBoatViewModel);
            }

            return boatsCollection;
        }
    }
}
