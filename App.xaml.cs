using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TODO.Service;
using TODO.View;
using TODO.ViewModel;

namespace TODO;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private const string ConfigurationFileName = "appsettings.json";
    
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        try
        {
            var configuration = LoadConfiguration();
            ConfigureSerilog(configuration);
            Log.Information("Starting application configuration");
            IServiceCollection services = new ServiceCollection();
            // Register Serilog's ILogger
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            // Register other components
            AddWindows(services);
            AddViewModels(services);
            AddServices(services);

            _serviceProvider = services.BuildServiceProvider();

            DispatcherUnhandledException += App_DispatcherUnhandledException;
            Log.Information("Application configuration completed");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application initialization failed");
            MessageBox.Show($"Failed to initialize application: {ex.Message}");
            Shutdown(1);
        }
        
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }
    
    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider.Dispose();
        Log.CloseAndFlush();
        base.OnExit(e);
    }
    
    private IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(ConfigurationFileName)
            .Build();
    }

    private void ConfigureSerilog(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private void AddWindows(IServiceCollection services)
    {
        Log.Information("Adding windows");
        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });
    }
    
    private void AddViewModels(IServiceCollection services)
    {
        Log.Information("Adding view models");
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<EditTodoViewModel>();
        services.AddSingleton<SharedViewModel>();
        services.AddSingleton<LoginViewModel>();
    }
    
    private void AddServices(IServiceCollection services)
    {
        Log.Information("Adding services");
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<Func<Type, AbstractViewModel>>(serviceProvider => viewModelType => (AbstractViewModel)serviceProvider.GetRequiredService(viewModelType));
    }
    
    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        Log.Fatal(e.Exception, "Unhandled exception occurred");
        MessageBox.Show($"Unhandled exception: {e.Exception.Message}");
        e.Handled = true;
    }
}

