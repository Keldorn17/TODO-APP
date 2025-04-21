using CommunityToolkit.Mvvm.Messaging;
using TODO.Client;
using TODO.Domain;

namespace TODO.ViewModel;

public class AllTodoTabViewModel(TodoClient todoClient, IMessenger messenger)
    : AbstractTodoTabViewModel(todoClient, messenger, QueryMode.All);