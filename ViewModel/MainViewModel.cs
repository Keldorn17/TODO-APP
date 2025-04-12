using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODO.Model;
using TODO.Service;
using TODO.View;


namespace TODO.ViewModel
{
    public partial class MainViewModel : AbstractViewModel
    {
        [ObservableProperty]
        private ObservableCollection<TodoItem> _todoItems;

        [ObservableProperty]
        private INavigationService _navigation;

        public RelayCommand NavigateHomeCommand { get; set; }
        public RelayCommand NavigateSharedCommand { get; set; }

        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateHomeCommand = new RelayCommand(() => { Navigation.NavigateTo<HomeViewModel>(); }, () => true);
            NavigateSharedCommand = new RelayCommand(() => { Navigation.NavigateTo<SharedViewModel>(); }, () => true);
            TodoItems = [];
        }

        [RelayCommand]
        private void OpenAddWindow()
        {
            var newTodoItem = new TodoItemBuilder()
                .SetId(TodoItems.Count + 1)
                .Build();
            var editWindow = new EditTodoWindow(newTodoItem, this, false);
            editWindow.ShowDialog();
        }
    }
}