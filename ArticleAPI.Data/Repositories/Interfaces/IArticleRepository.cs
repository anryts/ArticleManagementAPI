using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Filters;

namespace ArticleAPI.Data.Repositories.Interfaces;

public interface IArticleRepository : IBaseRepository<Article>
{
    Task<bool> IsExistAsync(Guid articleId);

    [Obsolete("Better to use this method with predicate to include only data which you need")]
    /// <summary>
    /// Use this method to get article with all included data
    /// </summary>
    /// <param name="id">Article Id</param>
    /// <returns></returns>
    Task<Article?> GetByIdWithIncludedDataAsync(Guid id);

    /// <summary>
    /// Use this method to get article with only interesting for you included data
    /// </summary>
    /// <param name="id">Article Id</param>
    /// <param name="includePredicate">Predicate by which you can take data</param>
    /// <returns></returns>
    Task<Article?> GetByIdWithIncludedDataAsync(Guid id,
        Func<IQueryable<Article>, IQueryable<Article>> includePredicate);

    Task<IEnumerable<Article?>> QueryAsync(GetArticleQueryDo filter);

    Task<IEnumerable<Article>?> GetArticlesByQuery(
        Func<IQueryable<Article>, IQueryable<Article>> queryPredicate);

    /// <summary>
    /// Use this method to find some articles which must be deleted at specific dateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>A collection of Articles</returns>
    Task<List<Article>> GetArticlesToDeleteAsync(DateTime dateTime);
}