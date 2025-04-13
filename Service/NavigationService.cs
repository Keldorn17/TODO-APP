using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using TODO.ViewModel;

namespace TODO.Service;


public partial class NavigationService : ObservableObject, INavigationService
{
    private readonly ILogger<NavigationService> _log;
    
    [ObservableProperty]
    private AbstractViewModel _currentView;

    private readonly Func<Type, AbstractViewModel> _viewModelFactory;

    public NavigationService(Func<Type, AbstractViewModel> viewModelFactory, ILogger<NavigationService> logger)
    {
        _viewModelFactory = viewModelFactory;
        _log = logger;
    }

    public void NavigateTo<TViewModel>() where TViewModel : AbstractViewModel
    {
        _log.LogInformation("Navigating to {ViewModelType}", typeof(TViewModel).Name);
        AbstractViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }
    
}