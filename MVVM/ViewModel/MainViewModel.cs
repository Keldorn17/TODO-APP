using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TODO.Core;
using TODO.MVVM.Model;
using TODO.MVVM.View;

namespace TODO.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand<HomeViewModel> HomeViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        public MainViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();

            HomeVM = new HomeViewModel(TodoItems);
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand<HomeViewModel>(o => 
            {
                CurrentView = HomeVM;
            });
        }
    }
}
