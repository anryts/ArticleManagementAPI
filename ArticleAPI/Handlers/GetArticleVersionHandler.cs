using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Models.ResponseModels;
using ArticleAPI.Providers.Interfaces;
using ArticleAPI.Services.Interfaces;
using AutoMapper;
using Common.Extensions;
using MediatR;

namespace ArticleAPI.Handlers;

public class GetArticleVersionQuery : IRequest<GetArticleVersionReposponseModel>
{
    public Guid ArticleVersionId { get; set; }
}

public class GetArticleVersionHandler : IRequestHandler<GetArticleVersionQuery, GetArticleVersionReposponseModel>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleVersionRepository _articleVersionRepository;
    private readonly IMapper _mapper;
    private readonly IFileProvider _fileProvider;
    private readonly ICurrentUserService _currentUserService;

    public GetArticleVersionHandler(IArticleVersionRepository articleVersionRepository,
        IArticleRepository articleRepository,
        IMapper mapper, IFileProvider fileProvider, ICurrentUserService currentUserService)
    {
        _articleVersionRepository = articleVersionRepository;
        _articleRepository = articleRepository;
        _mapper = mapper;
        _fileProvider = fileProvider;
        _currentUserService = currentUserService;
    }

    public async Task<GetArticleVersionReposponseModel> Handle(GetArticleVersionQuery request,
        CancellationToken cancellationToken)
    {
        var articleVersion = await _articleVersionRepository.GetByIdWithIncludedDataAsync(request.ArticleVersionId)
                             ?? throw new Exception("Article not found");

        var article = await _articleRepository.GetByIdAsync(articleVersion.ArticleId);
        if (article.AuthorId != _currentUserService.GetCurrentUserId())
            throw new Exception("Access denied");

        var responseStream = await _fileProvider.GetFileAsync(articleVersion.ContentPath);
        var result = _mapper.Map<GetArticleVersionReposponseModel>(articleVersion);
        result.Content = await responseStream.TransferStreamIntoText();

        return result;
    }
}