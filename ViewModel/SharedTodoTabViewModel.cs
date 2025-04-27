using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using TODO.Client;
using TODO.Domain;

namespace TODO.ViewModel;

public class SharedTodoTabViewModel(TodoClient todoClient, IMessenger messenger, ILogger<SharedTodoTabViewModel> log)
    : AbstractTodoTabViewModel(todoClient, messenger, QueryMode.Shared, log);