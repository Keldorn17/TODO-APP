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
    IProfileService profileService,
    ILogger<AuthenticationService> log)
    : IAuthenticationService
{
    private TaskCompletionSource<bool> _authenticationCompletionSource = new();

    public bool IsAuthenticated { get; private set; }

    public async Task Login()
    {
        await Authenticate(false);
    }

    public async Task SignUp()
    {
        await Authenticate(true);
    }

    public async Task<bool> TryToAuthenticate()
    {
        try
        {
            _authenticationCompletionSource = new TaskCompletionSource<bool>();
            await todoClient.GetTodos(null, null, new Pageable(0, 1));
            IsAuthenticated = true;
            _authenticationCompletionSource.SetResult(true);
        }
        catch (Exception)
        {
            IsAuthenticated = false;
        }

        return IsAuthenticated;
    }

    public Task WaitForAuthentication()
    {
        return _authenticationCompletionSource.Task;
    }

    private async Task Authenticate(bool signUp)
    {
        try
        {
            _authenticationCompletionSource = new TaskCompletionSource<bool>();
            var (code, pkcePair) = oAuthClient.Authenticate(signUp);
            var loginResponse =
                await todoClient.Login(new LoginRequest(code, pkcePair.CodeVerifier, oAuthConfig.RedirectUri));
            profileService.SaveProfile(loginResponse.IdToken!);
            IsAuthenticated = true;
            _authenticationCompletionSource.SetResult(true);
        }
        catch (TodoClientException ex)
        {
            IsAuthenticated = false;
            _authenticationCompletionSource.SetResult(false);
            log.LogError("Failed to login {message}", ex.Message);
            throw new AuthenticationException(ex.ErrorDetails);
        }
    }
}