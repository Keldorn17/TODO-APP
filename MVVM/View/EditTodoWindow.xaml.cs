using System.Windows;
using System.Windows.Input;
using TODO.MVVM.Model;
using TODO.MVVM.ViewModel;
using TODO.Utils;

namespace TODO.MVVM.View
{
    /// <summary>
    /// Interaction logic for EditTodoWindow.xaml
    /// </summary>
    public partial class EditTodoWindow : Window
    {
        public EditTodoWindow(TodoItem todoItem, MainViewModel mainViewModel, bool isEditing)
        {
            InitializeComponent();
            DataContext = new EditTodoViewModel(todoItem, this, mainViewModel, isEditing);
        }
        private void Btn_Exit(object sender, RoutedEventArgs e) => WindowHelper.CloseWindow(this);

        private void Btn_Minimize(object sender, RoutedEventArgs e) => WindowHelper.MinimizeWindow(this);

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => WindowHelper.DragWindow(this);

    }
}
