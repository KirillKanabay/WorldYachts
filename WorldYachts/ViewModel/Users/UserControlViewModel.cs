using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.ViewModel.BaseViewModels;
using WorldYachts.ViewModel.Users.SalesPersons;

namespace WorldYachts.ViewModel.Users
{
    class UserControlViewModel:BaseViewModel
    {
        private readonly IViewModelContainer _viewModelContainer;

        public UserControlViewModel(IViewModelContainer viewModelContainer)
        {
            _viewModelContainer = viewModelContainer;
        }

        public BaseViewModel SalesPersonManagementViewModel =>
            _viewModelContainer.GetViewModel<SalesPersonManagementViewModel>();
    }
}
