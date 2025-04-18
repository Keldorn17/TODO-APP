using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TODO.Service;


namespace TODO.ViewModel;

public partial class MainWindowViewModel : AbstractViewModel
{

    [ObservableProperty]
    private INavigationService _navigation;

    public MainWindowViewModel(MainWindowNavigationService navigation)
    {
        Navigation = navigation;
    }

    [RelayCommand]
    private void OnWindowLoaded()
    {
        Navigation.NavigateTo<DashboardViewModel>();
    }
    
}