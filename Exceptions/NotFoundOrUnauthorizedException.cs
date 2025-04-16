using TODO.DTO;

namespace TODO.Exceptions;

public class NotFoundOrUnauthorizedException(ErrorResponse errorResponse) : TodoClientException(errorResponse);