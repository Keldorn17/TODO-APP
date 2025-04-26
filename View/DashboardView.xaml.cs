using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
    
}