using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using ArticleAPI.Channels.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArticleAPI.Channels.Handlers;

public class AddArticleToChannelCommandHandler : IRequestHandler<AddArticleToChannelCommand, Unit>
{
    private readonly IChannelRepository _channelRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly ICurrentUserService _currentUserService;

    public AddArticleToChannelCommandHandler(IChannelRepository channelRepository,
        IArticleRepository articleRepository, ICurrentUserService currentUserService)
    {
        _channelRepository = channelRepository;
        _articleRepository = articleRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(AddArticleToChannelCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var channel = await _channelRepository.GetByQueryAsync(q => q
                          .Include(x => x.Author)
                          .FirstOrDefaultAsync(x => x.Id == request.ChannelId, cancellationToken))
                      ?? throw new Exception("Channel not found");

        if (!channel.AuthorId.Equals(currentUserId))
            throw new Exception("You are not the author of this channel");

        if (channel.Articles.Any(x => x.Id == request.ArticleId))
            throw new Exception("Article already added to this channel");
        var article = await _articleRepository.GetByIdAsync(request.ArticleId)
                      ?? throw new Exception("Article not found");
        if (!article.AuthorId.Equals(currentUserId))
            throw new Exception("You are not the author of this article");
        channel.Articles.Add(article);

        await _channelRepository.UpdateAsync(channel);
        return Unit.Value;
    }
}