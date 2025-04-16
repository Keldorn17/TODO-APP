namespace TODO.Domain;

public record OAuthConfig(string ClientId, string AuthorizationEndpoint, string RedirectUri);