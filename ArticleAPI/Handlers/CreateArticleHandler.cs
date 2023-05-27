using ArticleAPI.Models.ResponseModels;
using ArticleAPI.Providers.Interfaces;
using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using AutoMapper;
using Common.Extensions;
using MediatR;

namespace ArticleAPI.Handlers;

public class CreateArticleCommand : IRequest<CreateNewArticleResponseModel>
{
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public List<string> Tags { get; set; } = new ();
}

public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, CreateNewArticleResponseModel>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleVersionRepository _articleVersionRepository;
    private readonly IMapper _mapper;
    private readonly ITagRepository _tagRepository;
    private readonly IArticleTagRepository _articleTagRepository;
    private readonly IFileProvider _fileProvider;
    private readonly ICurrentUserService _currentUserService;
 
    public CreateArticleHandler(IArticleRepository articleRepository,
        IArticleVersionRepository articleVersionRepository,
        IMapper mapper,
        ITagRepository tagRepository,
        IArticleTagRepository articleTagRepository,
        IFileProvider fileProvider, ICurrentUserService currentUserService)
    {
        _articleRepository = articleRepository;
        _articleVersionRepository = articleVersionRepository;
        _mapper = mapper;
        _tagRepository = tagRepository;
        _articleTagRepository = articleTagRepository;
        _fileProvider = fileProvider;
        _currentUserService = currentUserService;
    }

    public async Task<CreateNewArticleResponseModel> Handle(CreateArticleCommand request, CancellationToken token)
    {
        var article = new Article
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            AuthorId = _currentUserService.GetCurrentUserId(),
            ShortIntro = request.Content.GetShortIntroFromContent(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        //Create tags if not exist
        await _tagRepository.CreateByNamesAsync(request.Tags);
        await _tagRepository.SaveChangesAsync();

        //Add tags to article
        await _articleTagRepository.AddAsync(article.Id, request.Tags);
        
        article.ContentPath = await _fileProvider
            .SaveFileAsync(request.Content, $"{article.Id}/{DateTime.Now.Ticks}", "html");

        var articleVersion = _mapper.Map<ArticleVersion>(article);
        articleVersion.Id = Guid.NewGuid();
        articleVersion.UpdatedAt = DateTime.UtcNow;

        await _articleRepository.AddAsync(article);
        await _articleVersionRepository.AddAsync(articleVersion);
        
        return new CreateNewArticleResponseModel
        {
            ArticleId = article.Id,
            ArticleVersionId = articleVersion.Id
        };
    }
}