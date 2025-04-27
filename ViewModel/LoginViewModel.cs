using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TODO.Domain;
using TODO.Service;

namespace TODO.ViewModel;

public partial class LoginViewModel(IAuthenticationService authenticationService, IMessenger messenger) : AbstractViewModel
{
    [RelayCommand]
    private async Task Login()
    {
        try
        {
            await authenticationService.Login();
            messenger.Send<LoginMessage>();
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to login, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    [RelayCommand]
    private async Task SignUp()
    {
        try
        {
            await authenticationService.SignUp();
            messenger.Send<LoginMessage>();
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to register, please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
}