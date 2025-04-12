using CommunityToolkit.Mvvm.ComponentModel;
using TODO.Core;

namespace TODO.Service;


public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty]
    private AbstractViewModel _currentView;

    private readonly Func<Type, AbstractViewModel> _viewModelFactory;

    public NavigationService(Func<Type, AbstractViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : AbstractViewModel
    {
        AbstractViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }
}