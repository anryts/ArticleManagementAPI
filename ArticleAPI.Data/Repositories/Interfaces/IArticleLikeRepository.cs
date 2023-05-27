using ArticleAPI.Data.Entities;

namespace ArticleAPI.Data.Repositories.Interfaces;

public interface IArticleLikeRepository : IBaseRepository<ArticleLike>
{
    /// <summary>
    /// Search by composite key
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<ArticleLike?> GetByIdAsync(Guid articleId, Guid userId);
}