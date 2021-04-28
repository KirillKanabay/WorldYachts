using Autofac;
using WorldYachts.DependencyInjections;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel
{
    public class ViewModelContainer : IViewModelContainer
    {
        private readonly ILifetimeScope _scope;
        public ViewModelContainer(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public TViewModel GetViewModel<TViewModel>() where TViewModel : IBaseViewModel
        {
            return _scope.Resolve<TViewModel>();
        }
    }
}
