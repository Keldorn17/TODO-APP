using TODO.DTO;

namespace TODO.Exceptions;

public class InvalidRefreshTokenException(ErrorResponse errorResponse) : TodoClientException(errorResponse);