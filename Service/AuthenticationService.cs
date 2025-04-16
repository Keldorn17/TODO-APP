using Microsoft.Extensions.Logging;
using TODO.Client;
using TODO.Domain;
using TODO.DTO;
using TODO.Exceptions;

namespace TODO.Service;

public class AuthenticationService(
    TodoClient todoClient,
    OAuthClient oAuthClient,
    OAuthConfig oAuthConfig,
    ILogger<AuthenticationService> log)
    : IAuthenticationService
{
    
    private TaskCompletionSource<bool> _authenticationCompletionSource = new();
    
    public bool IsAuthenticated { get; private set; }

    public async Task Authenticate()
    {
        try
        {
            _authenticationCompletionSource = new TaskCompletionSource<bool>();
            if (!todoClient.IsRefreshTokenAvailable)
            {
                var (code, pkcePair) = oAuthClient.Authenticate();
                await todoClient.Login(new LoginRequest(code, pkcePair.CodeVerifier, oAuthConfig.RedirectUri));
            }
            IsAuthenticated = true;
            _authenticationCompletionSource.SetResult(true);
        }
        catch (TodoClientException ex)
        {
            IsAuthenticated = false;
            log.LogError("Failed to authenticate {message}", ex.Message);
            throw new AuthenticationException(ex.ErrorDetails);
        }
    }

    public Task WaitForAuthentication()
    {
        return _authenticationCompletionSource.Task;
    }
}