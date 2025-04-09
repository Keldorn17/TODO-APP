using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODO.Model;
using TODO.View;


namespace TODO.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private HomeViewModel _homeVm;

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private ObservableCollection<TodoItem> _todoItems;

        public MainViewModel()
        {
            TodoItems = [];
            HomeVm = new HomeViewModel(this);
            CurrentView = HomeVm;
        }

        [RelayCommand]
        private void HomeView(HomeViewModel parameter)
        {
            CurrentView = HomeVm;
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