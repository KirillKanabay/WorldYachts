using System.Collections.Generic;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.Accessory
{
    public class FitExpanderViewModel:BaseViewModel
    {
        #region Поля

        private string _name;
        private IEnumerable<SelectableAccessoryFitViewModel> _accessoryFits;

        #endregion

        #region Конструктор

        public FitExpanderViewModel(string name, IEnumerable<SelectableAccessoryFitViewModel> accessoryFits)
        {
            Name = name;
            AccessoryFits = accessoryFits;
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

        public IEnumerable<SelectableAccessoryFitViewModel> AccessoryFits
        {
            get => _accessoryFits;
            set
            {
                _accessoryFits = value;
                OnPropertyChanged(nameof(AccessoryFits));
            }
        }
    }
}
