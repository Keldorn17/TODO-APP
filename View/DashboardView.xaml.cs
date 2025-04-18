using System.Windows;
using System.Windows.Controls;
using TODO.Utils;

namespace TODO.View;

public partial class DashboardView
{
    public DashboardView()
    {
        InitializeComponent();
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