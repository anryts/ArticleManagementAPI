using MediatR;

namespace ArticleAPI.Channels.Commands;

public class AddArticleToChannelCommand : IRequest
{
    public Guid ArticleId { get; set; }
    public Guid ChannelId { get; set; }
}