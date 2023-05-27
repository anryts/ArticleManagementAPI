using ArticleAPI.Data.Entities;
using ArticleAPI.Models.ResponseModels;
using ArticleAPI.Providers.Interfaces;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using AutoMapper;
using Common.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ArticleAPI.Handlers;

public class GetArticleByIdQuery : IRequest<GetArticleByIdResponseModel>
{
    public Guid ArticleId { get; init; }
}

public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, GetArticleByIdResponseModel>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IFileProvider _fileProvider;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMemoryCache _memoryCache;

    public GetArticleByIdHandler(IArticleRepository articleRepository,
        IMapper mapper, IFileProvider fileProvider,
        ICurrentUserService currentUserService, IUserRepository userRepository, IMemoryCache memoryCache)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _fileProvider = fileProvider;
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _memoryCache = memoryCache;
    }

    public async Task<GetArticleByIdResponseModel> Handle(GetArticleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(userId)
                   ?? throw new Exception("User not found");

        if (_memoryCache.TryGetValue($"article:{request.ArticleId}", out GetArticleByIdResponseModel? cachedArticle))
            return cachedArticle;
        
        var article = await _articleRepository.GetByIdWithIncludedDataAsync(request.ArticleId,
                      q => q.Include(x => x.Likes)
                          .Include(x => x.ArticleTags)
                          .Include(x => x.Author)
                          .Include(x => x.Images)
                          .Include(x => x.Channel))
                  ?? throw new Exception("Article not found");

        if (article.Channel is not null && article.Channel.IsPrivate && !user.IsSubscriptionActive)
            throw new Exception("Access denied");

        var result = _mapper.Map<GetArticleByIdResponseModel>(article);
        var responseStream = await _fileProvider.GetFileAsync(article.ContentPath);
        result.Content = await responseStream.TransferStreamIntoText();
        if (article.Images?.Any() ?? false)
            result.ArticleImageUrls = article.Images.Select(x => x.ImagePath).ToList();
        _memoryCache.Set($"article:{request.ArticleId}", result, TimeSpan.FromMinutes(5));
        return result;
    }
}