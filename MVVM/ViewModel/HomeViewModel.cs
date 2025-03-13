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
            AddTodoItem(1, "What is Lorem Ipsum?", "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", true);
            AddTodoItem(2, "Why do we use it?", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.", false, true);
            AddTodoItem(3, "Where does it come from?", "The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32.");
            AddTodoItem(4, "Where can I get some?", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable.", true, true);
        }

        public void AddTodoItem(
            long id,
            string title,
            string description,
            bool priority = false,
            bool isCompleted = false,
            string owner = null,
            string deadline = null,
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
