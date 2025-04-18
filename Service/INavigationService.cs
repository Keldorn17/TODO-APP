using TODO.ViewModel;

namespace TODO.Service
{
    public interface INavigationService
    {
        AbstractViewModel CurrentView { get; }
        Task NavigateTo<T>() where T : AbstractViewModel;
    }
}
