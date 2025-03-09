using System.Collections.ObjectModel;
using TODO.Core;
using TODO.MVVM.Model;

namespace TODO.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        private ObservableCollection<TodoItem> _todoItems;
        public ObservableCollection<TodoItem> TodoItems
        {
            get { return _todoItems; }
            set
            {
                _todoItems = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
            AddTodoItem(1, "title1", "description1");
            AddTodoItem(2, "title2", "description2");
        }

        public void AddTodoItem(long id, string title, string description, bool isCompleted = false)
        {
            TodoItems.Add(new TodoItem { Id = id, Title = title, Description = description, IsCompleted = isCompleted });
        }
    }
}
