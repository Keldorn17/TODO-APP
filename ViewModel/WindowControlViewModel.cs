using System.Windows;
using CommunityToolkit.Mvvm.Input;

namespace TODO.ViewModel;

public partial class WindowControlViewModel : AbstractViewModel
{
    [RelayCommand]
    private static void Exit() => Application.Current.Shutdown();

    [RelayCommand]
    private static void ExitCurrent() => GetAssociatedWindow()?.Close();

    [RelayCommand]
    private static void Minimize()
    {
        var window = GetAssociatedWindow();
        if (window != null)
        {
            window.WindowState = WindowState.Minimized;
        }
    }

    [RelayCommand]
    private void MaximizeRestore()
    {
        var window = GetAssociatedWindow();
        if (window == null) return;
        
        window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        window.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

        window.WindowState = window.WindowState == WindowState.Maximized 
            ? WindowState.Normal 
            : WindowState.Maximized;
    }

    private static Window? GetAssociatedWindow()
    {
        return Application.Current.Windows.OfType<Window>()
            .FirstOrDefault(w => w.IsActive) ?? Application.Current.MainWindow;
    }
}