using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorldYachts.Data;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;


namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class PartnersManagementViewModel : BaseManagementViewModel<Partner>
    {
        #region Конструкторы

        public PartnersManagementViewModel()
        {
        }

        #endregion

        #region Свойства

        public override IDataModel<Partner> ModelItem => new PartnerModel();
        public override BaseEditorViewModel<Partner> Editor => new PartnerEditorViewModel();


        #endregion


        #region Методы

        protected override ObservableCollection<BaseSelectableViewModel<Partner>> GetSelectableViewModels(
            IEnumerable<Partner> items)
        {
            var collection = new ObservableCollection<BaseSelectableViewModel<Partner>>();
            foreach (var partner in items)
            {
                collection.Add(new SelectablePartnerViewModel(partner));
            }

            return collection;
        }


        /// <summary>
        /// Фильтрация партнеров по id или названию
        /// </summary>
        /// <param name="filterText">Поисковая строка</param>
        /// <returns></returns>
        protected override ObservableCollection<BaseSelectableViewModel<Partner>> Filter(string filterText)
        {
            var filteredCollection = ItemsCollection.Where(p =>
                p.Item.Name.ToLower().Contains(filterText.ToLower()) ||
                p.Item.ToString() == filterText);

            var partnersCollection = new ObservableCollection<BaseSelectableViewModel<Partner>>();
            foreach (var selectablePartnerViewModel in filteredCollection)
            {
                partnersCollection.Add(selectablePartnerViewModel);
            }

            return partnersCollection;
        }

        

        #endregion
    }
}