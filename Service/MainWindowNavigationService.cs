using Microsoft.Extensions.Logging;
using TODO.ViewModel;

namespace TODO.Service;

public class MainWindowNavigationService(Func<Type, AbstractViewModel> viewModelFactory,
    ILogger<MainWindowNavigationService> logger) : AbstractNavigationService(viewModelFactory, logger);