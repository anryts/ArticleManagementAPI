using MediatR;

namespace ArticleAPI.Channels.Commands;

public class CreateChannelCommand : IRequest
{
    public string Title  { get; set; }
    public string Description { get; set; }
    public bool IsPrivate { get; set; }
}