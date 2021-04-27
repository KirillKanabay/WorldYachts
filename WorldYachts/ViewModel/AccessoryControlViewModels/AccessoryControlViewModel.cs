using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    public class AccessoryControlViewModel:BaseViewModel
    {
        private readonly ViewModelContainer _viewModelContainer;
        public AccessoryControlViewModel(ViewModelContainer viewModelContainer)
        {
            _viewModelContainer = viewModelContainer;
        }

        //public BaseViewModel PartnerManagementViewModel =>
        //    _viewModelContainer.GetViewModel<PartnersManagementViewModel>();

        public BaseViewModel AccessoryManagementViewModel =>
            _viewModelContainer.GetViewModel<AccessoryManagementViewModel>();
    }
}
