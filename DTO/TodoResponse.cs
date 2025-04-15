namespace TODO.DTO;

public record TodoResponse(
    long Id,
    string? Title,
    string? Description,
    string? Deadline,
    bool Completed,
    string OwnerEmail,
    long? ParentId,
    bool Shared,
    PriorityResponse Priority,
    List<string>? Categories,
    AccessLevelResponse AccessLevel,
    string CreatedAt,
    string UpdatedAt
);