namespace TODO.DTO;

public record LoginResponse(
    string AccessToken,
    long ExpiresAt,
    string RefreshToken,
    long RefreshExpiresAt,
    string TokenType,
    string? IdToken
);