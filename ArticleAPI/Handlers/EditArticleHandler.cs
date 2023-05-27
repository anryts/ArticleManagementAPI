using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Providers.Interfaces;
using ArticleAPI.Services.Interfaces;
using AutoMapper;
using Common.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace ArticleAPI.Handlers;

public class EditArticleCommand : IRequest<Guid>
{
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public Guid ArticleId { get; init; }
    public List<string> Tags { get; set; } = new();
}

public class EditArticleHandler : IRequestHandler<EditArticleCommand, Guid>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly IArticleVersionRepository _articleVersionRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IImageRepository _imageRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IArticleTagRepository _articleTagRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMemoryCache _memoryCache;

    public EditArticleHandler(IArticleVersionRepository articleVersionRepository,
        IMapper mapper, IArticleRepository articleRepository, IFileProvider fileProvider,
        IImageRepository imageRepository,
        ITagRepository tagRepository,
        IArticleTagRepository articleTagRepository,
        ICurrentUserService currentUserService, IMemoryCache memoryCache)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _articleVersionRepository = articleVersionRepository;
        _fileProvider = fileProvider;
        _imageRepository = imageRepository;
        _tagRepository = tagRepository;
        _articleTagRepository = articleTagRepository;
        _currentUserService = currentUserService;
        _memoryCache = memoryCache;
    }

    public async Task<Guid> Handle(EditArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdWithIncludedDataAsync(request.ArticleId)
                      ?? throw new FileNotFoundException("Article not found");

        if (article.AuthorId != _currentUserService.GetCurrentUserId())
            throw new Exception("You cannot edit this article");

        if (_memoryCache.TryGetValue($"article:{request.ArticleId}", out _))
            _memoryCache.Remove($"article:{request.ArticleId}");
        
        article.Title = request.Title;
        article.ContentPath = await _fileProvider
            .SaveFileAsync(request.Content, $"{article.Id}/{DateTime.Now.Ticks}", "html");

        article.ShortIntro = request.Content.GetShortIntroFromContent();
        article.UpdatedAt = DateTime.UtcNow;

        await _tagRepository.CreateByNamesAsync(request.Tags);
        await _tagRepository.SaveChangesAsync();
        await _articleTagRepository.DeleteByArticleIdAsync(article.Id);
        await _articleTagRepository.CreateByArticleIdAsync(request.ArticleId, request.Tags);

        foreach (var item in article.Images)
        {
            await _imageRepository.DeleteAsync(item);
        }

        var articleVersion = _mapper.Map<ArticleVersion>(article);
        articleVersion.Id = Guid.NewGuid();
        articleVersion.UpdatedAt = DateTime.UtcNow;
        
        await _articleTagRepository.SaveChangesAsync();
        await _articleRepository.UpdateAsync(article);
        await _articleVersionRepository.AddAsync(articleVersion);
        await _imageRepository.SaveChangesAsync();

        return articleVersion.Id;
    }
}