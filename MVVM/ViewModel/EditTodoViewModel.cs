using CommunityToolkit.Mvvm.ComponentModel;
using TODO.MVVM.Model;

namespace TODO.MVVM.ViewModel
{
    public partial class EditTodoViewModel : ObservableObject
    {
        public string[] PriorityList = { "Not Required", "Low", "Normal", "High", "Critical" };
        [ObservableProperty]
        private TodoItem _currentTodo;

        public EditTodoViewModel(TodoItem todoItem)
        {
            CurrentTodo = todoItem;
        }
    }
}
