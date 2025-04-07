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
        private HomeViewModel _homeVM;

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private ObservableCollection<TodoItem> _todoItems;

        public MainViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
            HomeVM = new HomeViewModel(this);
            CurrentView = HomeVM;
        }

        [RelayCommand]
        private void HomeView(HomeViewModel parameter)
        {
            CurrentView = HomeVM;
        }

        [RelayCommand]
        private void OpenAddWindow()
        {
            TodoItem newTodoItem = new TodoItemBuilder()
                .SetId(TodoItems.Count + 1)
                .Build();
            EditTodoWindow editWindow = new EditTodoWindow(newTodoItem, this, false);
            editWindow.ShowDialog();
        }
    }
}