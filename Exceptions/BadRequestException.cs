using TODO.DTO;

namespace TODO.Exceptions;

public class BadRequestException(ErrorResponse errorResponse) : TodoClientException(errorResponse);