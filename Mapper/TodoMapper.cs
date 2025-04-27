using AutoMapper;
using TODO.DTO;
using TODO.Model;

namespace TODO.Mapper;

public class TodoMapper
{
    private static readonly Lazy<TodoMapper> LazyInstance = new(() => new TodoMapper());
    public static TodoMapper Instance => LazyInstance.Value;

    private readonly IMapper _mapper;

    private TodoMapper()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<TodoMappingProfile>(); });
        _mapper = config.CreateMapper();
    }

    public TodoItem TodoResponseToTodoItem(TodoResponse response)
    {
        return _mapper.Map<TodoItem>(response);
    }
    
    public List<TodoItem> MapTodos(List<TodoResponse> responses)
    {
        return responses.Select(TodoResponseToTodoItem).ToList();
    }
}