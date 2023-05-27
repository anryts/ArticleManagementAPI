using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Services.Interfaces;
using ArticleAPI.Providers.Interfaces;
using MediatR;

namespace ArticleAPI.Handlers;

public class UploadImageCommand : IRequest
{
    public Guid ArticleVersionId { get; init; }
    public IFormFileCollection Images { get; init; }
}

public class UploadImageHandler : IRequestHandler<UploadImageCommand, Unit>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleVersionRepository _articleVersionRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IArticleVersionImageRepository _articleVersionImageRepository;
    private readonly ICurrentUserService _currentUserService;

    public UploadImageHandler(IArticleRepository articleRepository, IImageRepository imageRepository,
        IFileProvider fileProvider,
        IArticleVersionRepository articleVersionRepository,
        IArticleVersionImageRepository articleVersionImageRepository, ICurrentUserService currentUserService)
    {
        _articleRepository = articleRepository;
        _imageRepository = imageRepository;
        _fileProvider = fileProvider;
        _articleVersionRepository = articleVersionRepository;
        _articleVersionImageRepository = articleVersionImageRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var articleVersion = await _articleVersionRepository.GetByIdAsync(request.ArticleVersionId)
                             ?? throw new Exception("ArticleVersion not found");

        var article = await _articleRepository.GetByIdWithIncludedDataAsync(articleVersion.ArticleId);

        if (article.AuthorId != _currentUserService.GetCurrentUserId())
            throw new Exception("Access denied");

        foreach (var image in request.Images)
        {
            try
            {
                var articleVersionImage = new ArticleVersionImage
                {
                    Id = Guid.NewGuid(),
                    ArticleVersionId = articleVersion.Id,
                };
                var path = await _fileProvider
                    .SaveFileAsync(image, $"{articleVersionImage.Id}/{DateTime.Now.Ticks}");
                articleVersionImage.ImagePath = path;

                await _articleVersionImageRepository.AddAsync(articleVersionImage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        var latestArticleVersion = await _articleVersionRepository.LatestArticleVersion(articleVersion.ArticleId)
                                   ?? throw new Exception("ArticleVersion not found");
        await _articleVersionRepository.SaveChangesAsync();
        if (articleVersion.Id == latestArticleVersion.Id)
            await CopyArticleVersionImageToArticleImage(
                articleVersion.ArticleVersionImages as List<ArticleVersionImage>, article.Id);

        return Unit.Value;
    }

    private async Task CopyArticleVersionImageToArticleImage(List<ArticleVersionImage> articleVersionImages,
        Guid articleId)
    {
        foreach (var image in articleVersionImages)
        {
            await _imageRepository.AddAsync(new ArticleImage
            {
                Id = Guid.NewGuid(),
                ArticleId = articleId,
                ImagePath = image.ImagePath
            });
        }
    }
}