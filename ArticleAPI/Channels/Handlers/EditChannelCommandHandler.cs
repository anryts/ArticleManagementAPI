using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using ArticleAPI.Channels.Commands;
using MediatR;

namespace ArticleAPI.Channels.Handlers;

public class EditChannelCommandHandler : IRequestHandler<EditChannelCommand, Unit>
{
    private readonly IChannelRepository _channelRepository;

    public EditChannelCommandHandler(IChannelRepository channelRepository) 
        => _channelRepository = channelRepository;
    
    public async Task<Unit> Handle(EditChannelCommand request, CancellationToken cancellationToken)
    {
        var channel = await _channelRepository.GetByIdAsync(request.ChannelId)
                      ?? throw new Exception("Channel not found");
        if (channel.AuthorId != request.UserId)
            throw new Exception("You are not author of this channel");

        if (!string.IsNullOrWhiteSpace(request.Title))
            channel.Title = request.Title;
        if (!string.IsNullOrWhiteSpace(request.Description))
            channel.Description = request.Description;

        await _channelRepository.SaveChangesAsync();
        return Unit.Value;
    }
}