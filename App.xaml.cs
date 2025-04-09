using System.Configuration;
using System.Data;
using System.Windows;

namespace TODO;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        // Attach the handler for unhandled exceptions
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
    }

    // This will handle unhandled exceptions in the app
    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        // Show an error message or log the exception details
        MessageBox.Show($"Unhandled exception: {e.Exception.Message}");
        e.Handled = true; // Prevents the application from crashing
    }
}

