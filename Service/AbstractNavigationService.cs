using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using TODO.ViewModel;

namespace TODO.Service;

public abstract partial class AbstractNavigationService(
    Func<Type, AbstractViewModel> viewModelFactory,
    ILogger<AbstractNavigationService> logger)
    : ObservableObject, INavigationService
{
    [ObservableProperty] private AbstractViewModel? _currentView;

    public async Task NavigateTo<TViewModel>() where TViewModel : AbstractViewModel
    {
        if (CurrentView != null && CurrentView.GetType() == typeof(TViewModel)) return;
        logger.LogInformation("Navigating to {ViewModelType}", typeof(TViewModel).Name);
        if (CurrentView != null)
        {
            await CurrentView.OnNavigatedFrom();
        }

        AbstractViewModel viewModel = viewModelFactory.Invoke(typeof(TViewModel));
        await viewModel.OnNavigatedTo();
        CurrentView = viewModel;
    }
}