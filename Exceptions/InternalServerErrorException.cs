using TODO.DTO;

namespace TODO.Exceptions;

public class InternalServerErrorException(ErrorResponse errorResponse) : TodoClientException(errorResponse);