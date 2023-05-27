using ArticleAPI.Data.Entities;

namespace ArticleAPI.Data.Repositories.Interfaces;

public interface IArticleTagRepository : IBaseRepository<ArticleTag>
{
    Task DeleteByArticleIdAsync(Guid articleId);
    Task CreateByArticleIdAsync(Guid articleId, List<string> tagNames);
    Task AddAsync(Guid id, List<string> tags);
}