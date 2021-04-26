using Autofac;
using Autofac.Core;
using WorldYachts.ViewModel.BaseViewModels;

namespace WorldYachts.ViewModel
{
    public class ViewModelContainer
    {
        private readonly ILifetimeScope _scope;
        public ViewModelContainer(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel
        {
            return _scope.Resolve<TViewModel>();
        }
    }
}
