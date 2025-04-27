using AutoMapper;
using TODO.DTO;
using TODO.Model;

namespace TODO.Mapper;

public class ShareMapper
{
    private static readonly Lazy<ShareMapper> LazyInstance = new(() => new ShareMapper());
    public static ShareMapper Instance => LazyInstance.Value;

    private readonly IMapper _mapper;

    private ShareMapper()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<ShareMappingProfile>(); });
        _mapper = config.CreateMapper();
    }

    public Shared SharedResponseToShared(ShareResponse shareResponse)
    {
        return _mapper.Map<Shared>(shareResponse);
    }

    public List<Shared> SharedResponsesToShares(List<ShareResponse> responses)
    {
        return responses.Select(SharedResponseToShared).ToList();
    }

    public ShareResponse SharedToShareResponse(Shared shared)
    {
        return _mapper.Map<ShareResponse>(shared);
    }

    public List<ShareResponse> SharesToShareResponses(List<Shared> shares)
    {
        return shares.Select(SharedToShareResponse).ToList();
    }
    
}