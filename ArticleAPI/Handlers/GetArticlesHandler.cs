using ArticleAPI.Data;
using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Filters;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Models.ResponseModels;
using ArticleAPI.Services.Interfaces;
using AutoMapper;
using Common.Enums;
using MassTransit.Initializers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ArticleAPI.Handlers;

public class GetArticlesQuery : IRequest<IEnumerable<GetArticleResponseModel>>
{
    /// <summary>
    /// Can be title or tag 
    /// </summary>
    public string? SearchKey { get; set; }

    public SortBy SortBy { get; set; }
    public bool Ascending { get; set; }
    public int AmountOfArticles { get; set; }
    public int Offset { get; set; }
}

public class GetArticlesHandler : IRequestHandler<GetArticlesQuery, IEnumerable<GetArticleResponseModel>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly AppDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetArticlesHandler(IArticleRepository articleRepository, IMapper mapper,
        ICurrentUserService currentUserService, AppDbContext dbContext)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetArticleResponseModel>> Handle(GetArticlesQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();
        var subscriptionStatus = await _dbContext.Set<User>()
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken: cancellationToken)
            .Select(user => user.IsSubscriptionActive);

        var filter = new GetArticleQueryDo
        {
            IsSubscriptionActive = subscriptionStatus,
            SearchKey = request.SearchKey,
            AmountOfArticles = request.AmountOfArticles,
            Ascending = request.Ascending,
            SortBy = request.SortBy,
            Offset = request.Offset,
        };

        var result = await _articleRepository.QueryAsync(filter);
        var response = _mapper.Map<IEnumerable<GetArticleResponseModel>>(result);

        return response;
    }
}