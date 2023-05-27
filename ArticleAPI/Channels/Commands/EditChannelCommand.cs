using MediatR;

namespace ArticleAPI.Channels.Commands;

public class EditChannelCommand : IRequest
{
    public Guid ChannelId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
}