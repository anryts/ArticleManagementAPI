using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace ArticleAPI.Handlers;

public class DeleteArticleCommand : IRequest<Article>
{
    public Guid ArticleId { get; init; }
    public Guid UserId { get; set; }
}

public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand, Article>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMemoryCache _memoryCache;

    public DeleteArticleHandler(IArticleRepository articleRepository, IMemoryCache memoryCache)
    {
        _articleRepository = articleRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Article> Handle(DeleteArticleCommand request,
        CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue($"article:{request.ArticleId}", out _))
            _memoryCache.Remove($"article:{request.ArticleId}");
        var article = await _articleRepository.GetByIdWithIncludedDataAsync(request.ArticleId)
                      ?? throw new Exception("Article not found");

        if (article.AuthorId != request.UserId)
            throw new Exception("Access denied");

        article.IsDeleted = true;
        article.DeletedAt = DateTime.UtcNow;
        await _articleRepository.UpdateAsync(article);

        return article;
    }
}