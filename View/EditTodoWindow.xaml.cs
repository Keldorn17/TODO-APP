using TODO.ViewModel;

namespace TODO.View
{
    /// <summary>
    /// Interaction logic for EditTodoWindow.xaml
    /// </summary>
    public partial class EditTodoWindow
    {
        
        public EditTodoWindow(EditTodoViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

    }
}
