using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        public EditTodoWindow(TodoItem todoItem)
        {
            InitializeComponent();
            DataContext = new EditTodoViewModel(todoItem);
        }
        private void Btn_Exit(object sender, RoutedEventArgs e) => WindowHelper.CloseWindow(this);

        private void Btn_Minimize(object sender, RoutedEventArgs e) => WindowHelper.MinimizeWindow(this);

        private void Btn_Maximize(object sender, RoutedEventArgs e) => WindowHelper.MaximizeRestoreWindow(this);

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => WindowHelper.DragWindow(this);
    }
}
