using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ArticleAPI.Handlers;

public class AddLikeToArticleCommand : IRequest
{
    public bool IsLike { get; set; }
    public Guid UserId { get; set; }
    public Guid ArticleId { get; set; }
}

public class AddLikeToArticleCommandHandler : IRequestHandler<AddLikeToArticleCommand, Unit>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleLikeRepository _articleLikeRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;

    public AddLikeToArticleCommandHandler(IArticleRepository articleRepository,
        IArticleLikeRepository articleLikeRepository, IUserRepository userRepository,
        ICurrentUserService currentUserService, IMemoryCache memoryCache)
    {
        _articleRepository = articleRepository;
        _articleLikeRepository = articleLikeRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _memoryCache = memoryCache;
    }

    public async Task<Unit> Handle(AddLikeToArticleCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(userId)
                   ?? throw new Exception("User not found");
        var article = await _articleRepository.GetByIdWithIncludedDataAsync(request.ArticleId,
                          q => q.Include(x => x.Likes)
                              .Include(x => x.Channel))
                      ?? throw new Exception("Article not found");

        if (article.Channel is not null && article.Channel.IsPrivate && !user.IsSubscriptionActive)
            throw new Exception("Access denied");
        
        if (_memoryCache.TryGetValue($"article:{request.ArticleId}", out _))
            _memoryCache.Remove($"article:{request.ArticleId}");
        
        var existingMark = await _articleLikeRepository.GetByIdAsync(request.ArticleId, request.UserId);
        if (existingMark is not null)
        {
            if (existingMark.IsLike != request.IsLike)
                existingMark.IsLike = request.IsLike;
            else
                existingMark.IsLike = null;
        }

        if (existingMark is null)
        {
            await _articleLikeRepository.AddAsync(new ArticleLike
            {
                IsLike = request.IsLike,
                ArticleId = request.ArticleId,
                AuthorId = request.UserId
            });
        }

        await _articleRepository.SaveChangesAsync();
        await _articleLikeRepository.SaveChangesAsync();

        return Unit.Value;
    }
}