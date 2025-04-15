namespace TODO.DTO;

public record LoginRequest(string Code, string CodeVerifier, string RedirectUri);