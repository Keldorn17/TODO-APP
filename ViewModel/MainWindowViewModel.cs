using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using TODO.Domain;
using TODO.Service;


namespace TODO.ViewModel;

public partial class MainWindowViewModel : AbstractViewModel
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<MainWindowViewModel> _log;

    [ObservableProperty] private INavigationService _navigation;

    public MainWindowViewModel(MainWindowNavigationService navigation, IAuthenticationService authenticationService,
        IMessenger messenger, ILogger<MainWindowViewModel> log)
    {
        Navigation = navigation;
        _authenticationService = authenticationService;
        _log = log;
        messenger.Register<LoginMessage>(this, HandleLoginMessage);
        messenger.Register<LogoutMessage>(this, HandleLogoutMessage);
    }

    [RelayCommand]
    private async Task OnWindowLoaded()
    {
        if (await _authenticationService.TryToAuthenticate())
        {
            _log.LogInformation("Already authenticated, loading dashboard");
            await Navigation.NavigateTo<DashboardViewModel>();
        }
        else
        {
            _log.LogInformation("Not authenticated, loading login page");
            await Navigation.NavigateTo<LoginViewModel>();
        }
    }

    private void HandleLoginMessage(object? sender, LoginMessage e)
    {
        Navigation.NavigateTo<DashboardViewModel>();
    }

    private void HandleLogoutMessage(object sender, LogoutMessage logoutMessage)
    {
        Navigation.NavigateTo<LoginViewModel>();
    }
}