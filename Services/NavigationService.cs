using CommunityToolkit.Mvvm.ComponentModel;

namespace TODO.Services;

public interface INavigationService
{
    Core.ViewModel CurrentView { get; }
    void NavigateTo<T>() where T : Core.ViewModel;

}

public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty]
    private Core.ViewModel _currentView;

    private readonly Func<Type, Core.ViewModel> _viewModelFactory;

    public NavigationService(Func<Type, Core.ViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : Core.ViewModel
    {
        Core.ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }
}