using MediatR;

namespace ArticleAPI.Channels.Commands;

public class DeleteChannelCommand : IRequest
{
    public string Title { get; set; }
    public Guid UserId { get; set; }
}