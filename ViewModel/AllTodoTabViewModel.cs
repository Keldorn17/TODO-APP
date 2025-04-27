using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using TODO.Client;
using TODO.Domain;

namespace TODO.ViewModel;

public class AllTodoTabViewModel(TodoClient todoClient, IMessenger messenger, ILogger<AllTodoTabViewModel> log)
    : AbstractTodoTabViewModel(todoClient, messenger, QueryMode.All, log);