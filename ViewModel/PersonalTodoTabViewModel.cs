using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using TODO.Client;
using TODO.Domain;

namespace TODO.ViewModel;

public class PersonalTodoTabViewModel(TodoClient todoClient, IMessenger messenger, ILogger<PersonalTodoTabViewModel> log)
    : AbstractTodoTabViewModel(todoClient, messenger, QueryMode.Own, log);