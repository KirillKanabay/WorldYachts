using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.View.MessageDialogs;
using WorldYachts.ViewModel.BoatManagementViewModels;
using WorldYachts.ViewModel.MessageDialog;

namespace WorldYachts.ViewModel.BoatManagementViewModels
{
    class BoatManagementViewModel:BaseManagementViewModel<Boat>
    {
        #region Конструкторы

        public BoatManagementViewModel()
        {
            SelectableBoatViewModel.OnItemChanged = () =>
            {
                //Убираем лодки которые были удалены
                RemoveItem.Execute(null);
                //Обновляем список лодок
                GetItemsCollection.Execute(null);
            };
        }
        #endregion

        #region Свойства
        public override IDataModel<Boat> ModelItem => new BoatModel();
        public override BaseEditorViewModel<Boat> Editor => new BoatEditorViewModel();

        #endregion
        
        #region Методы

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
            var filteredCollection = ItemsCollection.Where(p =>
                p.Item.Model.ToLower().Contains(filterText.ToLower()) ||
                p.Item.ToString() == filterText);

            var boatsCollection = new ObservableCollection<BaseSelectableViewModel<Boat>>();
            foreach (var selectableBoatViewModel in filteredCollection)
            {
                boatsCollection.Add(selectableBoatViewModel);
            }

            return boatsCollection;
        }
        #endregion

    }
}
