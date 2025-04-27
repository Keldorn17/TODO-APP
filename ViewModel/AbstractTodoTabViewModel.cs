using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Microsoft.Extensions.Logging;
using TODO.Client;
using TODO.Domain;
using TODO.Mapper;
using TODO.Model;
using TODO.View;
using TODO.DTO;
using TODO.Exceptions;

namespace TODO.ViewModel;

public abstract partial class AbstractTodoTabViewModel : AbstractViewModel
{
    private readonly TodoClient _todoClient;
    private readonly IMessenger _messenger;
    private readonly QueryMode _queryMode;
    private readonly ILogger _log;
    private string _searchQuery = string.Empty;

    private readonly SourceList<TodoItem> _todos = new();

    private readonly ReadOnlyObservableCollection<TodoCardViewModel> _todoCards;

    public ReadOnlyObservableCollection<TodoCardViewModel> TodoCards => _todoCards;

    protected AbstractTodoTabViewModel(TodoClient todoClient, IMessenger messenger, QueryMode queryMode,
        ILogger<AbstractTodoTabViewModel> log)
    {
        _todoClient = todoClient;
        _messenger = messenger;
        _queryMode = queryMode;
        _log = log;
        _todos.Connect()
            .Transform(item =>
                new TodoCardViewModel(item, () => OpenEditWindow(item), () => HandleCheckboxChange(item)))
            .Bind(out _todoCards)
            .Subscribe();
    }

    public override Task OnNavigatedTo()
    {
        _messenger.Register<SearchQueryMessage>(this, HandleSearchQueryMessage);
        _messenger.Register<TodoListChangedMessage>(this, HandleTodoListChangedMessage);
        _messenger.Register<LoginMessage>(this, HandleLoginMessage);
        _messenger.Send<SearchQueryRequestMessage>();
        return Task.CompletedTask;
    }

    public override Task OnNavigatedFrom()
    {
        _messenger.Unregister<SearchQueryMessage>(this);
        _messenger.Unregister<TodoListChangedMessage>(this);
        _messenger.Unregister<LoginMessage>(this);
        return Task.CompletedTask;
    }

    [RelayCommand]
    private void OpenEditWindow(TodoItem todoItem)
    {
        var editViewModel = new EditTodoViewModel(todoItem, _todoClient, _messenger, true);
        var editWindow = new EditTodoWindow(editViewModel);
        editWindow.ShowDialog();
    }

    private void HandleCheckboxChange(TodoItem todoItem)
    {
        Application.Current.Dispatcher.Invoke(async () =>
        {
            try
            {
                _log.LogDebug("Setting completed for todo {id} to {completed}", todoItem.Id, todoItem.IsCompleted);
                var request = new UpdateTodoRequest(Completed: todoItem.IsCompleted);
                await _todoClient.PatchTodo(todoItem.Id, request);
                _log.LogDebug("Set completed for todo {id} to {completed}", todoItem.Id, todoItem.IsCompleted);
            }
            catch (TodoClientException)
            {
                _messenger.Send<TodoListChangedMessage>();
                _log.LogError("Failed to update todo with id {id} to completed: {completed}", todoItem.Id,
                    todoItem.IsCompleted);
            }
        });
    }

    private void HandleTodoListChangedMessage(object sender, TodoListChangedMessage todoListChangedMessage)
    {
        _log.LogDebug("Refreshing todo list");
        _ = QueryTodos();
    }

    private void HandleSearchQueryMessage(object sender, SearchQueryMessage queryMessage)
    {
        _searchQuery = queryMessage.Query;
        _log.LogDebug("Handling search query: [{searchQuery}]", _searchQuery);
        _ = QueryTodos();
    }

    private void HandleLoginMessage(object sender, LoginMessage todoLoginMessage)
    {
        _log.LogDebug("Refreshing todos after login");
        _ = QueryTodos();
    }

    private async Task QueryTodos()
    {
        _log.LogDebug("Querying {queryMode} todos", _queryMode.Mode);
        var todos = await _todoClient.GetTodos(_searchQuery, _queryMode, new Pageable(0, 100));
        var todoItems = TodoMapper.Instance.MapTodos(todos.Content);
        _log.LogDebug("Queried {count} {queryMode} todos", todoItems.Count, _queryMode.Mode);
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            _todos.Clear();
            _todos.AddRange(todoItems);
        });
    }
}