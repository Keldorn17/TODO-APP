namespace TODO.Domain;

public record OAuthConfig(string ClientId, string AuthorizationEndpoint, string RegistrationEndpoint, string RedirectUri);