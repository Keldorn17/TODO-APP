using CommunityToolkit.Mvvm.ComponentModel;

namespace TODO.Service;


public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty]
    private Core.AbstractViewMode _currentView;

    private readonly Func<Type, Core.AbstractViewMode> _viewModelFactory;

    public NavigationService(Func<Type, Core.AbstractViewMode> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : Core.AbstractViewMode
    {
        Core.AbstractViewMode viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }
}