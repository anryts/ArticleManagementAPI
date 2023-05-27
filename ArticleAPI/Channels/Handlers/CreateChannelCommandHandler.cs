using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using ArticleAPI.Channels.Commands;
using MediatR;

namespace ArticleAPI.Channels.Handlers;

public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, Unit>
{
    private readonly IChannelRepository _channelRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateChannelCommandHandler(IChannelRepository channelRepository,
        ICurrentUserService currentUserService)
    {
        _channelRepository = channelRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
    {
        if (await _channelRepository.IsTitleExistAsync(request.Title))
            throw new Exception("Channel with this title already exist");
        var channel = new Channel
        {
            Title = request.Title,
            Description = request.Description,
            AuthorId = _currentUserService.GetCurrentUserId(),
            IsPrivate = request.IsPrivate
        };
        await _channelRepository.AddAsync(channel);
        return Unit.Value;
    }
}