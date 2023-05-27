using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using ArticleAPI.Channels.Queries;
using ArticleAPI.Models.ResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArticleAPI.Channels.Handlers;

public class GetChannelsForUserQueryHandler : IRequestHandler<GetChannelsForUserQuery,
    IEnumerable<GetChannelsForUserResponseModel>>
{
    private readonly IChannelRepository _channelRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetChannelsForUserQueryHandler(IChannelRepository channelRepository,
        IMapper mapper,
        IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<GetChannelsForUserResponseModel>> Handle(GetChannelsForUserQuery request,
        CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository
                       .GetByQueryAsync(q => q
                           .FirstOrDefaultAsync(user => user.Id.Equals(currentUserId))
                   ?? throw new Exception("User not found"));

        IEnumerable<Channel?> channels;
        if (request.ShowOnlyMyChannels)
        {
            channels = await _channelRepository
                .GetByQueryAsync(q => q
                    .Include(c => c.Articles)
                    .Where(c => c.AuthorId.Equals(currentUserId)));
        }
        else
        {
            channels = user.IsSubscriptionActive switch
            {
                false => await _channelRepository
                    .GetByQueryAsync(q => q
                        .Where(c => !c.IsPrivate)),
                true => await _channelRepository
                    .GetAllAsync()
            };
        }

        var result = _mapper.Map<IEnumerable<GetChannelsForUserResponseModel>>(channels);

        return result;
    }
}