namespace TODO.DTO;

public record TodoListResponse(List<TodoResponse> Content, PageInfoResponse Page);