using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using ArticleAPI.Providers.Interfaces;
using ArticleAPI.Services.Interfaces;
using MediatR;

namespace ArticleAPI.Handlers;

public class ScheduleArticleDeleteCommand : IRequest
{
    public Article Article { get; set; }
}

public class ScheduleArticleDeleteHandler : IRequestHandler<ScheduleArticleDeleteCommand, Unit>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IBackgroundJobService _backgroundJobService;
    private readonly IFileProvider _fileProvider;
    
    public ScheduleArticleDeleteHandler(IArticleRepository articleRepository,
        IBackgroundJobService backgroundJobService,
        IFileProvider fileProvider)
    {
        _articleRepository = articleRepository;
        _backgroundJobService = backgroundJobService;
        _fileProvider = fileProvider;
    }
    
    public async Task<Unit> Handle(ScheduleArticleDeleteCommand request, CancellationToken cancellationToken)
    {
       _backgroundJobService.CronJob(() => DeleteArticles());
       return await Unit.Task;
    }

    public async Task DeleteArticles()
    {
        var articlesToDelete = await _articleRepository.GetArticlesToDeleteAsync(DateTime.UtcNow);
        List<string> filesToDelete = new();
        
        articlesToDelete
            .ForEach(article => filesToDelete.AddRange(FindPathsToFile(article)));
        
        await _articleRepository.DeleteAsync(articlesToDelete);
        await _articleRepository.SaveChangesAsync();
        await _fileProvider.DeleteFileAsync(filesToDelete);
    }
    
    private IEnumerable<string> FindPathsToFile(ArticleAPI.Data.Entities.Article article)
    {
        var filePaths = new List<string>();
        // you don't need to add file path form article, because, article version already have it
        if(article.ArticleVersions?.Any() ?? false)
            return filePaths;
        
        foreach (var articleVersion in article.ArticleVersions)
        {
            filePaths.Add(articleVersion.ContentPath);
            var articleVersionImagesPaths = articleVersion.ArticleVersionImages?
                .Select(x => x.ImagePath)
                .ToList();
            
            if(articleVersionImagesPaths is not null)
                filePaths.AddRange(articleVersionImagesPaths);
        }
        return filePaths;
    }
}