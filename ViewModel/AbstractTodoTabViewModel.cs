using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TODO.Client;
using TODO.Domain;
using TODO.Model;
using TODO.View;

namespace TODO.ViewModel;

public abstract partial class AbstractTodoTabViewModel(TodoClient todoClient, IMessenger messenger, QueryMode queryMode)
    : AbstractViewModel
{
    private string _searchQuery = string.Empty;

    [ObservableProperty] 
    private ObservableCollection<TodoItem> _todos = [];

    public override async Task OnNavigatedTo()
    {
        messenger.Register<SearchQuery>(this, HandleSearchQuery);
        messenger.Send<SearchQueryRequest>();
        await QueryTodos();
    }

    public override async Task OnNavigatedFrom()
    {
        messenger.Unregister<SearchQuery>(this);
    }

    [RelayCommand]
    private void OpenEditWindow(TodoItem todoItem)
    {
        EditTodoWindow editWindow = new EditTodoWindow(todoItem, true);
        editWindow.ShowDialog();
    }

    private void HandleSearchQuery(object sender, SearchQuery query)
    {
        _searchQuery = query.Query;
        _ = QueryTodos();
    }

    private async Task QueryTodos()
    {
        // Todo query logic
        await Application.Current.Dispatcher.InvokeAsync(InitializeTodoItems);
    }

    private void InitializeTodoItems()
    {
        try
        {
            Todos.Clear();
            Todos.Add(new TodoItemBuilder()
                .SetId(1)
                .SetTitle("What is Lorem Ipsum?")
                .SetDescription("Lorem Ipsum is simply dummy text of the printing and typesetting industry.")
                .SetPriority(new Priority { Level = 1 })
                .SetDeadline("2026-10-02T14:45:30.123456789+05:30")
                .SetShared(new Shared("Csaba@gmail.com", new Access { Level = 2 }))
                .SetShared(new Shared("Zoli@gmail.com", new Access { Level = 1 }))
                .Build());

            Todos.Add(new TodoItemBuilder()
                .SetId(2)
                .SetTitle("Why do we use it?")
                .SetDescription(
                    "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.")
                .SetIsCompleted(true)
                .SetDeadline("2026-10-02T14:45:30.1234567Z")
                .Build());

            Todos.Add(new TodoItemBuilder()
                .SetId(3)
                .SetTitle("Where does it come from?")
                .SetDescription(
                    "The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32.")
                .SetPriority(new Priority { Level = 2 })
                .Build());

            Todos.Add(new TodoItemBuilder()
                .SetId(4)
                .SetTitle("Where can I get some?")
                .SetDescription(
                    "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable.")
                .SetPriority(new Priority { Level = 3 })
                .SetIsCompleted(true)
                .SetDeadline("2026-10-02T16:45:30.1234568+02:00")
                .Build());

            Todos.Add(new TodoItemBuilder()
                .SetId(5)
                .SetTitle("What is Lorem Ipsum?")
                .SetDescription("Lorem Ipsum is simply dummy text of the printing and typesetting industry.")
                .SetPriority(new Priority { Level = 4 })
                .Build());
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }
}