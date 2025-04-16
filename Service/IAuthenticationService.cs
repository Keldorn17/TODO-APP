namespace TODO.Service;

public interface IAuthenticationService
{
    
    bool IsAuthenticated { get; }
    
    Task Authenticate();

    Task WaitForAuthentication();

}