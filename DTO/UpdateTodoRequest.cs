namespace TODO.DTO;

public record UpdateTodoRequest(
    string? Title = null,
    string? Description = null,
    string? Deadline = null,
    bool? Completed = null,
    long? Parent = null,
    int? Priority = null,
    List<string>? Categories = null
);