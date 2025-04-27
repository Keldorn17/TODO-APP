using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using TODO.Client;
using TODO.Domain;
using TODO.Model;
using TODO.Service;
using TODO.View;

namespace TODO.ViewModel;

public partial class DashboardViewModel : AbstractViewModel
{
    private const int DebounceTime = 500;

    public RelayCommand NavigateToPersonalCommand { get; }
    public RelayCommand NavigateToSharedCommand { get; }
    public RelayCommand NavigateToAllCommand { get; }

    private readonly TodoClient _todoClient;
    private readonly IMessenger _messenger;
    private readonly IProfileService _profileService;
    private readonly ILogger<DashboardViewModel> _log;

    private CancellationTokenSource? _searchDebounceCts;

    [ObservableProperty] private INavigationService _navigation;

    [ObservableProperty] private string _searchQuery;

    public string Username => _profileService.GetProfile().Name;
    
    public DashboardViewModel(DashboardNavigationService navigation, TodoClient todoClient, IMessenger messenger,
        IProfileService profileService, ILogger<DashboardViewModel> log)
    {
        Navigation = navigation;
        _todoClient = todoClient;
        _messenger = messenger;
        _profileService = profileService;
        _log = log;
        SearchQuery = string.Empty;
        NavigateToPersonalCommand =
            new RelayCommand(() => { Navigation.NavigateTo<PersonalTodoTabViewModel>(); }, () => true);
        NavigateToSharedCommand =
            new RelayCommand(() => { Navigation.NavigateTo<SharedTodoTabViewModel>(); }, () => true);
        NavigateToAllCommand =
            new RelayCommand(() => { Navigation.NavigateTo<AllTodoTabViewModel>(); }, () => true);
        _messenger.Register<SearchQueryRequestMessage>(this, HandleSearchQueryRequestMessage);
    }

    [RelayCommand]
    private void OpenAddWindow()
    {
        var newTodoItem = new TodoItemBuilder()
            .Build();
        var editViewModel = new EditTodoViewModel(newTodoItem, _todoClient, _messenger, false);
        var editWindow = new EditTodoWindow(editViewModel);
        editWindow.ShowDialog();
    }

    [RelayCommand]
    private void OnWindowLoaded()
    {
        Navigation.NavigateTo<PersonalTodoTabViewModel>();
    }

    [RelayCommand]
    private async Task Logout()
    {
        _log.LogInformation("Logging out");
        await _todoClient.Logout();
        _messenger.Send(new LogoutMessage());
        _log.LogInformation("Logged out");
    }

    [RelayCommand]
    private void OpenProfile()
    {
        _profileService.OpenProfilePage();
    }

    [RelayCommand]
    private void RefreshTodos()
    {
        _messenger.Send(new TodoListChangedMessage());
    }

    private void HandleSearchQueryRequestMessage(object sender, SearchQueryRequestMessage requestMessage)
    {
        _messenger.Send(new SearchQueryMessage(SearchQuery));
    }

    partial void OnSearchQueryChanged(string value)
    {
        _searchDebounceCts?.Cancel();
        _searchDebounceCts = new CancellationTokenSource();
        var token = _searchDebounceCts.Token;

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(DebounceTime, token);
                _messenger.Send(new SearchQueryMessage(value));
            }
            catch (TaskCanceledException)
            {
            }
        }, token);
    }
}