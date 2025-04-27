using AutoMapper;
using TODO.DTO;
using TODO.Model;

namespace TODO.Mapper;

public class ShareMappingProfile : Profile
{
    public ShareMappingProfile()
    {
        CreateMap<ShareResponse, Shared>()
            .ConstructUsing(src => new Shared(src.Email, new Access()
            {
                Level = src.AccessLevel.AccessLevel
            }));
    }
}