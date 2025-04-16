namespace TODO.Domain;

public record TodoConfig(
    string BaseUrl,
    string LoginEndpoint,
    string RefreshEndpoint,
    string TodoEndpoint,
    string ShareEndpoint,
    string LogoutEndpoint,
    string LogoutAllEndpoint
);