using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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

    private readonly IMessenger _messenger;

    private CancellationTokenSource? _searchDebounceCts;

    [ObservableProperty] private INavigationService _navigation;

    [ObservableProperty] private string _searchQuery;


    public DashboardViewModel(DashboardNavigationService navigation, IMessenger messenger)
    {
        Navigation = navigation;
        _messenger = messenger;
        SearchQuery = string.Empty;
        NavigateToPersonalCommand =
            new RelayCommand(() => { Navigation.NavigateTo<PersonalTodoTabViewModel>(); }, () => true);
        NavigateToSharedCommand =
            new RelayCommand(() => { Navigation.NavigateTo<SharedTodoTabViewModel>(); }, () => true);
        NavigateToAllCommand =
            new RelayCommand(() => { Navigation.NavigateTo<AllTodoTabViewModel>(); }, () => true);
        _messenger.Register<SearchQueryRequest>(this, HandleOnSearchQueryRequest);
    }

    [RelayCommand]
    private void OpenAddWindow()
    {
        var newTodoItem = new TodoItemBuilder()
            .Build();
        var editWindow = new EditTodoWindow(newTodoItem, false);
        editWindow.ShowDialog();
    }

    [RelayCommand]
    private void OnWindowLoaded()
    {
        Navigation.NavigateTo<PersonalTodoTabViewModel>();
    }

    private void HandleOnSearchQueryRequest(object sender, SearchQueryRequest request)
    {
        _messenger.Send(new SearchQuery(SearchQuery));
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
                _messenger.Send(new SearchQuery(value));
            }
            catch (TaskCanceledException)
            {
            }
        }, token);
    }
}