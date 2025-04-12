using CommunityToolkit.Mvvm.ComponentModel;
using TODO.Core;

namespace TODO.Service;


public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty]
    private AbstractViewMode _currentView;

    private readonly Func<Type, AbstractViewMode> _viewModelFactory;

    public NavigationService(Func<Type, AbstractViewMode> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : AbstractViewMode
    {
        AbstractViewMode viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }
}