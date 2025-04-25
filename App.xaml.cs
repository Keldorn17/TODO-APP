using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TODO.Client;
using TODO.Domain;
using TODO.Provider;
using TODO.Service;
using TODO.View;
using TODO.ViewModel;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace TODO;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private const string ConfigurationFileName = "appsettings.json";
    private const string OAuthConfigName = "OAuthClientConfig";
    private const string TodoConfigName = "TodoClientConfig";

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
            RegisterConfigurations(services, configuration);
            RegisterHttpClients(services);
            RegisterWindows(services);
            RegisterViewModels(services);
            RegisterProviders(services);
            RegisterServices(services);
            RegisterClients(services);

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

    private void RegisterHttpClients(IServiceCollection services)
    {
        Log.Information("Registering HttpClients");
        services.AddHttpClient("AccessTokenProvider", (provider, client) =>
        {
            var config = provider.GetRequiredService<TodoConfig>();
            Log.Debug("Setting BaseUrl for AccessTokenProvider httpclient: {BaseUrl}", config.BaseUrl);
            client.BaseAddress = new Uri(config.BaseUrl);
        });
        services.AddHttpClient("TodoClient", (provider, client) =>
        {
            var config = provider.GetRequiredService<TodoConfig>();
            Log.Debug("Setting BaseUrl for TodoClient httpclient: {BaseUrl}", config.BaseUrl);
            client.BaseAddress = new Uri(config.BaseUrl);
        });
    }

    private void RegisterConfigurations(IServiceCollection services, IConfiguration configuration)
    {
        Log.Information("Registering configurations");
        services.AddSingleton(configuration.GetSection(OAuthConfigName).Get<OAuthConfig>()!);
        services.AddSingleton(configuration.GetSection(TodoConfigName).Get<TodoConfig>()!);
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IncludeFields = true,
        };
        services.AddSingleton(serializerOptions);
    }

    private void RegisterWindows(IServiceCollection services)
    {
        Log.Information("Registering windows");
        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainWindowViewModel>()
        });
    }

    private void RegisterViewModels(IServiceCollection services)
    {
        Log.Information("Registering view models");
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<EditTodoViewModel>();
        services.AddSingleton<WindowControlViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<PersonalTodoTabViewModel>();
        services.AddSingleton<SharedTodoTabViewModel>();
        services.AddSingleton<AllTodoTabViewModel>();
        services.AddSingleton<LoginViewModel>();
    }

    private void RegisterProviders(IServiceCollection services)
    {
        Log.Information("Registering providers");
        services.AddSingleton<RefreshTokenProvider>();
        services.AddSingleton<AccessTokenProvider>(provider =>
        {
            var factory = provider.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient("AccessTokenProvider");

            var options = provider.GetRequiredService<JsonSerializerOptions>();
            var refreshTokenProvider = provider.GetRequiredService<RefreshTokenProvider>();
            var config = provider.GetRequiredService<TodoConfig>();

            return new AccessTokenProvider(client, options, refreshTokenProvider, config);
        });
    }

    private void RegisterServices(IServiceCollection services)
    {
        Log.Information("Registering services");
        services.AddSingleton<MainWindowNavigationService>();
        services.AddSingleton<DashboardNavigationService>();
        services.AddSingleton<Func<Type, AbstractViewModel>>(serviceProvider =>
            viewModelType => (AbstractViewModel)serviceProvider.GetRequiredService(viewModelType));
        services.AddSingleton<ICallbackService, CallbackService>();
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
    }

    private void RegisterClients(IServiceCollection services)
    {
        Log.Information("Registering clients");
        services.AddSingleton<OAuthClient>();
        services.AddSingleton<TodoClient>(provider =>
        {
            var factory = provider.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient("TodoClient");

            var options = provider.GetRequiredService<JsonSerializerOptions>();
            var tokenProvider = provider.GetRequiredService<AccessTokenProvider>();
            var config = provider.GetRequiredService<TodoConfig>();

            return new TodoClient(client, options, tokenProvider, config);
        });
    }

    private void App_DispatcherUnhandledException(object sender,
        System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        Log.Fatal(e.Exception, "Unhandled exception occurred");
        MessageBox.Show($"Unhandled exception: {e.Exception.Message}");
        e.Handled = true;
    }
}