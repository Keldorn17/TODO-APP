using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TODO.Domain;
using TODO.DTO;
using TODO.Exceptions;
using TODO.Provider;
using TODO.Utils;

namespace TODO.Client;

public class TodoClient(
    HttpClient httpClient,
    JsonSerializerOptions serializerOptions,
    AccessTokenProvider tokenProvider,
    TodoConfig todoConfig)
{
    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        var response = await httpClient.PostAsJsonAsync(todoConfig.LoginEndpoint, loginRequest, serializerOptions);
        ValidateResponse(response);
        var loginResponse = (await response.Content.ReadFromJsonAsync<LoginResponse>())!;
        tokenProvider.SaveTokens(loginResponse.AccessToken, loginResponse.ExpiresAt, loginResponse.RefreshToken);
        return loginResponse;
    }

    public async Task Logout()
    {
        var request = new LogoutRequest(tokenProvider.RefreshToken);
        var response = await httpClient.PostAsJsonAsync(todoConfig.LogoutEndpoint, request, serializerOptions);
        ValidateResponse(response);
        tokenProvider.ClearTokens();
    }

    public async Task LogoutAll()
    {
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var response = await httpClient.PostAsync(todoConfig.LogoutAllEndpoint, null);
        ValidateResponse(response);
        tokenProvider.ClearTokens();
    }

    public async Task<TodoListResponse> GetTodos(string? searchText = null, QueryMode? queryMode = null,
        Pageable? pageable = null)
    {
        var mode = queryMode ?? QueryMode.All;
        var page = pageable ?? new Pageable(0, 20);
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var url = $"{todoConfig.TodoEndpoint}{QueryBuilderUtils.BuildQuery(searchText, mode, page)}";
        var response = await httpClient.GetAsync(url);
        ValidateResponse(response);
        var todoListResponse = (await response.Content.ReadFromJsonAsync<TodoListResponse>(serializerOptions))!;
        return todoListResponse;
    }

    public async Task<TodoResponse> GetTodoById(long todoId)
    {
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var response = await httpClient.GetAsync($"{todoConfig.TodoEndpoint}/{todoId}");
        ValidateResponse(response);
        var todoResponse = (await response.Content.ReadFromJsonAsync<TodoResponse>())!;
        return todoResponse;
    }

    public async Task<TodoResponse> CreateTodo(CreateTodoRequest request)
    {
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var response = await httpClient.PostAsJsonAsync(todoConfig.TodoEndpoint, request, serializerOptions);
        ValidateResponse(response);
        var todoResponse = (await response.Content.ReadFromJsonAsync<TodoResponse>())!;
        return todoResponse;
    }

    public async Task<TodoResponse> UpdateTodo(long todoId, UpdateTodoRequest request)
    {
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var response =
            await httpClient.PutAsJsonAsync($"{todoConfig.TodoEndpoint}/{todoId}", request, serializerOptions);
        ValidateResponse(response);
        var todoResponse = (await response.Content.ReadFromJsonAsync<TodoResponse>())!;
        return todoResponse;
    }

    public async Task<TodoResponse> PatchTodo(long todoId, UpdateTodoRequest request)
    {
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var response =
            await httpClient.PatchAsJsonAsync($"{todoConfig.TodoEndpoint}/{todoId}", request, serializerOptions);
        ValidateResponse(response);
        var todoResponse = (await response.Content.ReadFromJsonAsync<TodoResponse>())!;
        return todoResponse;
    }

    public async Task DeleteTodoById(long todoId)
    {
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var response = await httpClient.DeleteAsync($"{todoConfig.TodoEndpoint}/{todoId}");
        ValidateResponse(response);
    }

    public async Task<List<ShareResponse>> GetSharesForTodo(long todoId)
    {
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var response = await httpClient.GetAsync($"{todoConfig.TodoEndpoint}/{todoId}/{todoConfig.ShareEndpoint}");
        ValidateResponse(response);
        var shareResponse = (await response.Content.ReadFromJsonAsync<List<ShareResponse>>())!;
        return shareResponse;
    }

    public async Task CreateShareForTodo(long todoId, TodoShareRequest request)
    {
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var url = $"{todoConfig.TodoEndpoint}/{todoId}/{todoConfig.ShareEndpoint}";
        var response = await httpClient.PostAsJsonAsync(url, request, serializerOptions);
        ValidateResponse(response);
    }

    public async Task DeleteShareForTodo(long todoId, TodoShareDeleteRequest request)
    {
        httpClient.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();
        var endpoint = $"{todoConfig.TodoEndpoint}/{todoId}/{todoConfig.ShareEndpoint}";
        var httpRequest = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri($"{todoConfig.BaseUrl}{endpoint}"),
            Content = new StringContent(JsonSerializer.Serialize(request, serializerOptions), Encoding.UTF8,
                MediaTypeHeaderValue.Parse("application/json"))
        };
        var response = await httpClient.SendAsync(httpRequest);
        ValidateResponse(response);
    }

    private void ValidateResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var errorResponse = response.Content.ReadFromJsonAsync<ErrorResponse>(serializerOptions).Result ??
                            new ErrorResponse();
        throw response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => new UnauthorizedAccessException(),
            HttpStatusCode.Forbidden => new NotFoundOrUnauthorizedException(errorResponse),
            HttpStatusCode.BadRequest => new BadRequestException(errorResponse),
            HttpStatusCode.InternalServerError => new InternalServerErrorException(errorResponse),
            _ => new TodoClientException(errorResponse)
        };
    }

    private async Task<AuthenticationHeaderValue> GetAuthenticationHeader()
    {
        return new AuthenticationHeaderValue("Bearer", await tokenProvider.GetAccessTokenAsync());
    }
}