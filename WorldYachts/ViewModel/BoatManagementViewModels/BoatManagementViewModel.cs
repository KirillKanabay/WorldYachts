using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Helpers;
using WorldYachts.Helpers.Commands;
using WorldYachts.Model;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.BoatManagementViewModels
{
    class BoatManagementViewModel:BaseViewModel
    {
        private readonly IDataModel<Boat> _model;
        private readonly BoatEditorViewModel _editor;
        private readonly ObservableCollection<SelectableBoatViewModel> _boats;

        private string _filterText;

        public BoatManagementViewModel()
        {
            _model = new BoatModel();
            _editor = new BoatEditorViewModel();
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
        public ObservableCollection<SelectableBoatViewModel> FilteredCollection => Filter();

        public ObservableCollection<SelectableBoatViewModel> Filter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                return _boats;
            }

            var filteredCollection = _boats.Where(p =>
                p.Item.Model.ToLower().Contains(FilterText.ToLower()) ||
                p.Item.ToString() == FilterText);
            
            return new ObservableCollection<SelectableBoatViewModel>(filteredCollection);
        }

    }
}
