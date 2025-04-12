using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TODO.Service;
using TODO.View;
using TODO.ViewModel;

namespace TODO;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<EditTodoViewModel>();
        services.AddSingleton<SharedViewModel>();
        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<Func<Type, Core.AbstractViewMode>>(serviceProvider => viewModelType => (Core.AbstractViewMode)serviceProvider.GetRequiredService(viewModelType));

        _serviceProvider = services.BuildServiceProvider();
        
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show($"Unhandled exception: {e.Exception.Message}");
        e.Handled = true;
    }
}

