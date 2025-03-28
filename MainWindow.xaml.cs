using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TODO.MVVM.ViewModel;
using TODO.Utils;

namespace TODO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;
        }

        private void Btn_Exit(object sender, RoutedEventArgs e) => WindowHelper.CloseApp();

        private void Btn_Minimize(object sender, RoutedEventArgs e) => WindowHelper.MinimizeWindow(this);

        private void Btn_Maximize(object sender, RoutedEventArgs e) => WindowHelper.MaximizeRestoreWindow(this);

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => WindowHelper.DragWindow(this);

        private void Btn_ToggleTheme(object sender, RoutedEventArgs e)
        {
            Utils.ThemeManager.ToggleTheme();
            var button = sender as Button;
            if (button != null)
            {
                button.Content = ThemeManager.GetCurrentTheme() == "DarkTheme" ? "Switch to Light Theme" : "Switch to Dark Theme";
            }
        }
    }
}
