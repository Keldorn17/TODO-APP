using CommunityToolkit.Mvvm.ComponentModel;
using TODO.MVVM.Model;

namespace TODO.MVVM.ViewModel
{
    public enum PriorityLevel
    {
        NotRequired,
        Low,
        Normal,
        High,
        Critical
    }
    public partial class EditTodoViewModel : ObservableObject
    {
        public PriorityLevel[] PriorityList { get; } = Enum.GetValues(typeof(PriorityLevel)).Cast<PriorityLevel>().ToArray();

        [ObservableProperty]
        private TodoItem _currentTodo;

        [ObservableProperty]
        private TodoItem _copyTodo;

        public EditTodoViewModel(TodoItem todoItem)
        {
            CurrentTodo = todoItem;
            CopyTodo = new TodoItem
            {
                Id = CurrentTodo.Id,
                Title = CurrentTodo.Title,
                Description = CurrentTodo.Description,
                Owner = CurrentTodo.Owner,
                Deadline = CurrentTodo.Deadline,
                Category = new Category { Name = CurrentTodo.Category.Name },
                CreatedAt = CurrentTodo.CreatedAt,
                UpdatedAt = CurrentTodo.UpdatedAt,
                Parent = CurrentTodo.Parent,
                Priority = new Priority { 
                    Level = CurrentTodo.Priority.Level, 
                    Description = CurrentTodo.Priority.Description, 
                    Name = CurrentTodo.Priority.Name 
                },
                IsCompleted = CurrentTodo.IsCompleted,
                Shared = CurrentTodo.Shared
            };
        }
    }
}
