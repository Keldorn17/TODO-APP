using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TODO.Utils;
using TODO.ViewModel;

namespace TODO.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnMainWindowLoaded;
        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.Navigation.NavigateTo<HomeViewModel>();
            }
        }

        private void Btn_Exit(object sender, RoutedEventArgs e) => WindowHelper.CloseApp();

        private void Btn_Minimize(object sender, RoutedEventArgs e) => WindowHelper.MinimizeWindow(this);

        private void Btn_Maximize(object sender, RoutedEventArgs e) => WindowHelper.MaximizeRestoreWindow(this);

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => WindowHelper.DragWindow(this);

        private void Btn_ToggleTheme(object sender, RoutedEventArgs e)
        {
            Utils.ThemeManager.ToggleTheme();
            if (sender is Button button)
            {
                button.Content = ThemeManager.GetCurrentTheme() == "DarkTheme" ? "Switch to Light Theme" : "Switch to Dark Theme";
            }
        }
    }
}
