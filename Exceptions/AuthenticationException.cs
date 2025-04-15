using TODO.DTO;

namespace TODO.Exceptions;

public class AuthenticationException(ErrorResponse errorResponse) : TodoClientException(errorResponse);