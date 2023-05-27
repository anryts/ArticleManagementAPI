using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using ArticleAPI.Models.ResponseModels;
using AutoMapper;
using MediatR;

namespace ArticleAPI.Handlers;

public class GetArticleVersionsQuery : IRequest<IEnumerable<GetArticleVersionsResponseModel>>
{
    public Guid ArticleId { get; set; }
}

public class GetArticleVersionsHandler :
    IRequestHandler<GetArticleVersionsQuery, IEnumerable<GetArticleVersionsResponseModel>>
{
    private readonly IArticleVersionRepository _articleVersionRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetArticleVersionsHandler(IArticleRepository articleRepository,
        IArticleVersionRepository articleVersionRepository, 
        IMapper mapper, ICurrentUserService currentUserService)
    {
        _articleRepository = articleRepository;
        _articleVersionRepository = articleVersionRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<GetArticleVersionsResponseModel>> Handle(GetArticleVersionsQuery request,
        CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.ArticleId)
                      ?? throw new Exception("Article not found");

        if (article.AuthorId != _currentUserService.GetCurrentUserId())
            throw new Exception("Access denied");

        var articleVersions = await _articleVersionRepository
            .GetArticleVersionsToArticle(request.ArticleId);
        var result = _mapper
            .Map<IEnumerable<GetArticleVersionsResponseModel>>(articleVersions.OrderByDescending(x => x.UpdatedAt));
        return result;
    }
}