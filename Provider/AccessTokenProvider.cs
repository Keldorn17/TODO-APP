using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using TODO.Domain;
using TODO.DTO;
using TODO.Exceptions;
using TODO.Utils;

namespace TODO.Provider;

public class AccessTokenProvider(
    HttpClient httpClient,
    JsonSerializerOptions serializerOptions,
    RefreshTokenProvider refreshTokenProvider,
    TodoConfig todoConfig)
{
    private string _accessToken = string.Empty;
    private DateTime _tokenExpiration;

    public bool IsRefreshTokenAvailable => refreshTokenProvider.IsRefreshTokenAvailable();
    
    public async Task<string> GetAccessTokenAsync()
    {
        if (string.IsNullOrEmpty(_accessToken) || DateTime.Now > _tokenExpiration)
        {
            await RefreshTokenAsync();
        }
        return _accessToken;
    }

    public void SaveTokens(string accessToken, long expiresIn, string refreshToken)
    {
        _accessToken = accessToken;
        _tokenExpiration = DateTimeUtils.ToDateTime(expiresIn);
        refreshTokenProvider.SaveRefreshToken(refreshToken);
    }

    public void ClearTokens()
    {
        _accessToken = string.Empty;
        _tokenExpiration = DateTime.MinValue;
        refreshTokenProvider.ClearRefreshToken();
    }
    
    private async Task RefreshTokenAsync()
    {
        CheckForRefreshToken();
        var request = new RefreshTokenRequest(refreshTokenProvider.GetRefreshToken());
        var response = await httpClient.PostAsJsonAsync(todoConfig.RefreshEndpoint, request);
        ValidateResponse(response);
        LoginResponse loginResponse = (await response.Content.ReadFromJsonAsync<LoginResponse>(serializerOptions))!;
        _accessToken = loginResponse.AccessToken;
        refreshTokenProvider.SaveRefreshToken(loginResponse.RefreshToken);
        _tokenExpiration = DateTimeUtils.ToDateTime(loginResponse.RefreshExpiresAt);
    }

    private void CheckForRefreshToken()
    {
        if (!refreshTokenProvider.IsRefreshTokenAvailable())
        {
            throw new InvalidRefreshTokenException(new ErrorResponse()
            {
                Type = "Client Error",
                Title = "Refresh token error",
                Details = "No refresh token available",
            });
        }
    }

    private void ValidateResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var errorResponse = response.Content.ReadFromJsonAsync<ErrorResponse>(serializerOptions).Result
                            ?? new ErrorResponse();
        throw response.StatusCode switch
        {
            HttpStatusCode.BadRequest => new InvalidRefreshTokenException(errorResponse),
            HttpStatusCode.InternalServerError => new InternalServerErrorException(errorResponse),
            _ => new TodoClientException(errorResponse)
        };
    }
    
}