using System.Windows;
using System.Windows.Input;
using TODO.Model;
using TODO.Utils;
using TODO.ViewModel;

namespace TODO.View
{
    /// <summary>
    /// Interaction logic for EditTodoWindow.xaml
    /// </summary>
    public partial class EditTodoWindow
    {
        
        public EditTodoWindow(TodoItem todoItem, bool isEditing)
        {
            InitializeComponent();
            DataContext = new EditTodoViewModel(todoItem, this, isEditing);
        }

    }
}
