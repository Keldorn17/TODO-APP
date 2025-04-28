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
    }

    private void CheckBox_Loaded(object sender, RoutedEventArgs e)
    {
        if(ThemeManager.GetCurrentTheme() != "DarkTheme") 
            (sender as CheckBox).IsChecked = true;

    }
}