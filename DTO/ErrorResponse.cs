namespace TODO.DTO;

public class ErrorResponse(string type, string title, string details)
{
    private const string UnknownError = "Unknown error";

    public string Type { get; init; } = type;
    public string Title { get; init; } = title;
    public string Details { get; init; } = details;

    public ErrorResponse() : this(UnknownError, UnknownError, UnknownError)
    {
    }
}