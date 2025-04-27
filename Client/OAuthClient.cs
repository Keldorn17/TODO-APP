using System.Diagnostics;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using TODO.Domain;
using TODO.Service;
using TODO.Utils;

namespace TODO.Client;

public class OAuthClient(ICallbackService callbackService, OAuthConfig oAuthConfig, ILogger<OAuthClient> log)
{
    public CodePKCE Authenticate(bool signUp)
    {
        log.LogInformation("Attempting to authenticate");
        var pkcePair = PKCEUtils.GeneratePKCEPair();
        Task<string> task = callbackService.ListenForCallback(oAuthConfig.RedirectUri);
        string url = BuildAuthUrl(pkcePair, signUp);
        log.LogInformation("Openning url in default browser: {url}", url);
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        var codePkce = new CodePKCE(task.Result, pkcePair);
        log.LogInformation("Successfully authenticated");
        return codePkce;
    }

    private string BuildAuthUrl(PKCEPair pkcePair, bool signup)
    {
        var scope = WebUtility.UrlEncode("openid profile email offline_access");
        var nonce = Guid.NewGuid().ToString();
        var url = new StringBuilder();
        return url.Append(signup ? oAuthConfig.RegistrationEndpoint : oAuthConfig.AuthorizationEndpoint)
            .Append("?client_id=").Append(oAuthConfig.ClientId)
            .Append("&redirect_uri=").Append(oAuthConfig.RedirectUri)
            .Append("&response_type=code")
            .Append("&code_challenge=").Append(pkcePair.CodeChallenge)
            .Append("&scope=").Append(scope)
            .Append("&nonce=").Append(nonce)
            .Append("&code_challenge_method=S256")
            .ToString();
    }
}