using ArticleAPI.Abstraction.Interfaces;
using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using Dapper;
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
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;

    public AddLikeToArticleCommandHandler(IArticleRepository articleRepository,
        IArticleLikeRepository articleLikeRepository, IUserRepository userRepository,
        ICurrentUserService currentUserService, IMemoryCache memoryCache, ISqlConnectionFactory sqlConnectionFactory)
    {
        _articleRepository = articleRepository;
        _articleLikeRepository = articleLikeRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _memoryCache = memoryCache;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Unit> Handle( AddLikeToArticleCommand request,
                                    CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();
        // let's try to use here dapper instead of ef core
        // TODO: add sqlConnectionFactory to DI container
        await using var sqlConnection = _sqlConnectionFactory.CreateConnection();
        var user = await sqlConnection
                       .QueryFirstOrDefaultAsync<User>(
                           @"SELECT * FROM Users WHERE Id = @UserId", 
                           new
                           {
                               request.UserId
                           })
                   ?? throw new Exception("User not found");
        // var user = await _userRepository.GetByIdAsync(userId)
        //            ?? throw new Exception("User not found");
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