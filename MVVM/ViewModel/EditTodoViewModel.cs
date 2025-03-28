using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Core;
using TODO.MVVM.Model;

namespace TODO.MVVM.ViewModel
{
    class EditTodoViewModel : ObservableObject
    {
        public string[] PriorityList = { "Not Required", "Low", "Normal", "High", "Critical" };
        private TodoItem _currentTodo;
        public TodoItem CurrentTodo
        {
            get { return _currentTodo; }
            set
            {
                _currentTodo = value;
                OnPropertyChanged();
            }
        }

        public EditTodoViewModel(TodoItem todoItem)
        {
            CurrentTodo = todoItem;
        }
    }
}
