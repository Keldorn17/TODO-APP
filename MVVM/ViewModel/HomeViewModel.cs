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
            AddTodoItem(1, "Alma", "1kg almát kell venni.");
            AddTodoItem(2, "Budipapír", "Elfogyott.");
            AddTodoItem(3, "Hell Coffee", "Kell.");
        }

        public void AddTodoItem(
            long id,
            string title,
            string description,
            bool isCompleted = false,
            string owner = null,
            string deadline = null,
            string priority = null,
            string category = null,
            string createdAt = null,
            string updatedAt = null,
            string parent = null,
            bool shared = false)
        {
            TodoItems.Add(new TodoItem
            {
                Id = id,
                Title = title,
                Description = description,
                IsCompleted = isCompleted,
                Owner = owner,
                Deadline = deadline,
                Priority = priority,
                Category = category,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
                Parent = parent,
                Shared = shared
            });
        }
    }
}
