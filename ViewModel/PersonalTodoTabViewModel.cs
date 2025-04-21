using CommunityToolkit.Mvvm.Messaging;
using TODO.Client;
using TODO.Domain;

namespace TODO.ViewModel;

public class PersonalTodoTabViewModel(TodoClient todoClient, IMessenger messenger)
    : AbstractTodoTabViewModel(todoClient, messenger, QueryMode.Own);