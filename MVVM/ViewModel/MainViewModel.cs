using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODO.MVVM.Model;

namespace TODO.MVVM.ViewModel
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
            HomeVM = new HomeViewModel(TodoItems);
            CurrentView = HomeVM;
        }

        [RelayCommand]
        private void HomeView(HomeViewModel parameter)
        {
            CurrentView = HomeVM;
        }
    }
}