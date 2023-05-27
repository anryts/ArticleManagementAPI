using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using ArticleAPI.Channels.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArticleAPI.Channels.Handlers;

public class DeleteChannelCommandQuery : IRequestHandler<DeleteChannelCommand, Unit>
{
    private readonly IChannelRepository _channelRepository;

    public DeleteChannelCommandQuery(IChannelRepository channelRepository)
        => _channelRepository = channelRepository;

    public async Task<Unit> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
    {
        var channel = await _channelRepository.GetByQueryAsync(q => q
                          .Include(x => x.Author)
                          .FirstOrDefaultAsync(x => x.Title == request.Title, cancellationToken: cancellationToken))
                      ?? throw new Exception("Channel not found");

        if (channel.AuthorId != request.UserId)
            throw new Exception("You can't delete this channel");

        await _channelRepository.DeleteAsync(channel);
        return Unit.Value;
    }
}