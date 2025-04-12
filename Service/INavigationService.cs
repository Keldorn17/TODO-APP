using TODO.ViewModel;

namespace TODO.Service
{
    public interface INavigationService
    {
        AbstractViewModel CurrentView { get; }
        void NavigateTo<T>() where T : AbstractViewModel;
    }
}
