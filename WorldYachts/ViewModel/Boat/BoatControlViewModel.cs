using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.ViewModel.BaseViewModels;

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
    }
}
