using Microsoft.Extensions.Logging;
using TODO.ViewModel;

namespace TODO.Service;

public class DashboardNavigationService(Func<Type, AbstractViewModel> viewModelFactory,
    ILogger<DashboardNavigationService> logger) : AbstractNavigationService(viewModelFactory, logger);
