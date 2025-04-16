namespace TODO.DTO;

public record UpdateTodoRequest(
    string? Title,
    string? Description,
    string? Deadline,
    bool Completed,
    long? Parent,
    int Priority,
    List<string> Categories
);