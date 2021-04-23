using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.ViewModel;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.View.AccessoryControlViews
{
    public class FitExpanderViewModel:BaseViewModel
    {
        #region Поля

        private string _name;
        private IEnumerable<Boat> _boats;

        #endregion

        #region Конструктор

        public FitExpanderViewModel(string name, IEnumerable<Boat> boats)
        {
            Name = name;
            Boats = boats;
        }

        public FitExpanderViewModel()
        {
            
        }
        #endregion

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public IEnumerable<Boat> Boats
        {
            get => _boats;
            set
            {
                _boats = value;
                OnPropertyChanged(nameof(Boats));
            }
        }
    }
}
