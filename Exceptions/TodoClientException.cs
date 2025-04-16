using TODO.DTO;

namespace TODO.Exceptions;

public class TodoClientException(ErrorResponse errorResponse) : Exception(errorResponse.Details)
{
    public ErrorResponse ErrorDetails { get; init; } = errorResponse;
}