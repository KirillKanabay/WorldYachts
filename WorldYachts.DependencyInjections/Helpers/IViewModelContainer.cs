namespace WorldYachts.DependencyInjections.Helpers
{
    public interface IViewModelContainer
    {
        TViewModel GetViewModel<TViewModel>() where TViewModel : IBaseViewModel;
    }
}