using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.View.BoatControlViews.BoatType;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.Boat.BoatType;
using WorldYachts.ViewModel.Boat.Wood;

namespace WorldYachts.ViewModel.Boat
{
    public class BoatControlViewModel:BaseViewModel
    {
        private readonly IViewModelContainer _viewModelContainer;

        public BoatControlViewModel(IViewModelContainer viewModelContainer)
        {
            _viewModelContainer = viewModelContainer;
        }

        public BaseViewModel BoatManagementViewModel => _viewModelContainer.GetViewModel<BoatManagementViewModel>();
        public BaseViewModel BoatTypeManagementViewModel => _viewModelContainer.GetViewModel<BoatTypeManagementViewModel>();
        public BaseViewModel BoatWoodManagementViewModel =>
            _viewModelContainer.GetViewModel<BoatWoodManagementViewModel>();
    }
}
