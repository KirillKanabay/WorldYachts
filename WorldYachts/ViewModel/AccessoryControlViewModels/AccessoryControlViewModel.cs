using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel.AccessoryControlViewModels
{
    public class AccessoryControlViewModel:BaseViewModel
    {
        private readonly IViewModelContainer _viewModelContainer;
        public AccessoryControlViewModel(IViewModelContainer viewModelContainer)
        {
            _viewModelContainer = viewModelContainer;
        }

        //public BaseViewModel PartnerManagementViewModel =>
        //    _viewModelContainer.GetViewModel<PartnersManagementViewModel>();

        public BaseViewModel AccessoryManagementViewModel =>
            _viewModelContainer.GetViewModel<AccessoryManagementViewModel>();
    }
}
