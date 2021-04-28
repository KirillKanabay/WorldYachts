using System.Collections.Generic;
using System.Collections.ObjectModel;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Models;
using WorldYachts.ViewModel.BaseViewModels;


namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    class PartnersManagementViewModel : BaseViewModel
    {
        #region Конструкторы

        private ObservableCollection<SelectablePartnerViewModel> _partners;

        public PartnersManagementViewModel(IPartnerModel partnerModel, IViewModelContainer viewModelContainer)
        {
        }

        #endregion
        
        #region Методы

        //protected override ObservableCollection<BaseSelectableViewModel<Partner>> GetSelectableViewModels(
        //    IEnumerable<Partner> items)
        //{
        //    var collection = new ObservableCollection<BaseSelectableViewModel<Partner>>();
        //    foreach (var partner in items)
        //    {
        //        collection.Add(new SelectablePartnerViewModel(partner));
        //    }

        //    return collection;
        //}


        /// <summary>
        /// Фильтрация партнеров по id или названию
        /// </summary>
        /// <param name="filterText">Поисковая строка</param>
        /// <returns></returns>
        //protected override ObservableCollection<BaseSelectableViewModel<Partner>> Filter(string filterText)
        //{
        //    var filteredCollection = ItemsCollection.Where(p =>
        //        p.Item.Name.ToLower().Contains(filterText.ToLower()) ||
        //        p.Item.ToString() == filterText);

        //    var partnersCollection = new ObservableCollection<BaseSelectableViewModel<Partner>>();
        //    foreach (var selectablePartnerViewModel in filteredCollection)
        //    {
        //        partnersCollection.Add(selectablePartnerViewModel);
        //    }

        //    return partnersCollection;
        //}

        

        #endregion
    }
}