using ArticleAPI.Data.Entities;

namespace ArticleAPI.Data.Repositories.Interfaces;

public interface IArticleVersionRepository : IBaseRepository<ArticleVersion>
{
    public Task<ArticleVersion?> GetByIdWithIncludedDataAsync(Guid articleVersionId);
    public Task<IEnumerable<ArticleVersion>> GetArticleVersionsToArticle(Guid articleId);
    public Task<ArticleVersion?> LatestArticleVersion(Guid articleId);
}