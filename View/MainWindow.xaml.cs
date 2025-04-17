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
    public partial class MainWindow
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
        
        private void Btn_ToggleTheme(object sender, RoutedEventArgs e)
        {
            ThemeManager.ToggleTheme();
            if (sender is Button button)
            {
                button.Content = ThemeManager.GetCurrentTheme() == "DarkTheme" ? "Switch to Light Theme" : "Switch to Dark Theme";
            }
        }
    }
}
