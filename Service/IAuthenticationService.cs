namespace TODO.Service;

public interface IAuthenticationService
{
    Task Login();

    Task SignUp();

    Task<bool> TryToAuthenticate();
}