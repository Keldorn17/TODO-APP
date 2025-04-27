using System.Collections.ObjectModel;
using AutoMapper;
using TODO.DTO;
using TODO.Model;
using TODO.Utils;

namespace TODO.Mapper;

public class TodoMappingProfile : Profile
{
    public TodoMappingProfile()
    {
        CreateMap<TodoResponse, TodoItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.OwnerEmail))
            .ForMember(dest => dest.Deadline,
                opt => opt.MapFrom(src => src.Deadline != null ? DateTimeUtils.DateTimeConverter(src.Deadline) : DateTime.MinValue))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Categories != null
                ? new ObservableCollection<string>(src.Categories)
                : new ObservableCollection<string>()))
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTimeUtils.DateTimeConverter(src.CreatedAt)))
            .ForMember(dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => DateTimeUtils.DateTimeConverter(src.UpdatedAt)))
            .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.Completed))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Shared, opt => opt.Ignore()); // handled separately

        CreateMap<PriorityResponse, Priority>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.PriorityLevel))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<AccessLevelResponse, Access>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.AccessLevel));

        CreateMap<ShareResponse, Shared>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.SharedAccess, opt => opt.MapFrom(src => src.AccessLevel));
    }
}