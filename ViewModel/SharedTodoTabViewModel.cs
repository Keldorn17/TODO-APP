using CommunityToolkit.Mvvm.Messaging;
using TODO.Client;
using TODO.Domain;

namespace TODO.ViewModel;

public class SharedTodoTabViewModel(TodoClient todoClient, IMessenger messenger)
    : AbstractTodoTabViewModel(todoClient, messenger, QueryMode.Shared);