using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Filters;
using ArticleAPI.Data.Repositories.Interfaces;
using Common.Enums;
using Common.Extensions;
using Common.Options;
using Microsoft.EntityFrameworkCore;

namespace ArticleAPI.Data.Repositories;

public class ArticleRepository : BaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> IsExistAsync(Guid articleId)
    {
        return await _dbSet
            .AnyAsync(x => x.Id == articleId);
    }

    public async Task<IEnumerable<Article?>> QueryAsync(GetArticleQueryDo filter)
    {
        var articlesQuery = _dbSet
            .AsNoTracking()
            .Include(x => x.ArticleTags)
            .Include(x => x.Likes)
            .Include(x => x.Author)
            .AsNoTracking();

        // just return query 
        if (string.IsNullOrEmpty(filter.SearchKey))
            return await QueryAsync(articlesQuery, filter);
        var pattern = filter.SearchKey.TransferStringIntoPattern('|');

        var findByTitleQueryable = _dbSet
            .AsNoTracking()
            .Include(x => x.ArticleTags)
            .Include(x => x.Likes)
            .Include(x => x.Author)
            .Where(article => article.SearchVector.Matches(EF.Functions.ToTsQuery(pattern)));

        var findByTagsQueryable = _dbSet
            .AsNoTracking()
            .Include(x => x.ArticleTags)
            .Include(x => x.Likes)
            .Include(x => x.Author)
            .Except(findByTitleQueryable) // exclude articles which already found by title  
            .Where(x => x.ArticleTags.Any(
                articleTag => articleTag.SearchVector.Matches(EF.Functions.ToTsQuery(pattern))));

        var result = findByTitleQueryable
            .Union(findByTagsQueryable);

        return await QueryAsync(result, filter);
    }

    [Obsolete("Better to use this method with predicate to include only data which you need")]
    public async Task<Article?> GetByIdWithIncludedDataAsync(Guid articleId)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Images)
            .Include(x => x.ArticleVersions)
            .Include(x => x.ArticleTags)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == articleId);
    }

    public async Task<Article?> GetByIdWithIncludedDataAsync(Guid id,
        Func<IQueryable<Article>, IQueryable<Article>> includePredicate)
    {
        return await includePredicate(_dbSet.AsNoTracking())
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Article>?> GetArticlesByQuery(
        Func<IQueryable<Article>, IQueryable<Article>> queryPredicate)
    {
        return await queryPredicate(_dbSet.AsNoTracking())
            .ToListAsync();
    }

    /// <summary>   
    /// Use this method to find some articles which must be deleted at specific dateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>A collection of Articles</returns>
    public async Task<List<Article>> GetArticlesToDeleteAsync(DateTime dateTime)
    {
        var date = dateTime.Date;

        return await _dbSet
            .AsNoTracking()
            .Include(x => x.ArticleVersions)
            .ThenInclude(x => x.ArticleVersionImages)
            .Include(x => x.Images)
            .Where(x => x.DeletedAt.Value.Date == date && x.IsDeleted)
            .ToListAsync();
    }

    private async Task<List<Article>> QueryAsync(IQueryable<Article> articlesQuery,
        GetArticleQueryDo filter)
    {
        articlesQuery = filter.SortBy switch
        {
            SortBy.UpdatedAt when filter.Ascending => articlesQuery.OrderBy(x => x.UpdatedAt),
            SortBy.UpdatedAt when !filter.Ascending => articlesQuery.OrderByDescending(x => x.UpdatedAt),
            SortBy.Title when filter.Ascending => articlesQuery.OrderBy(x => x.Title),
            SortBy.Title when !filter.Ascending => articlesQuery.OrderByDescending(x => x.Title),
            _ => articlesQuery
        };

        if (!filter.IsSubscriptionActive)
            articlesQuery = articlesQuery.Where(x => (x.Channel != null && !x.Channel.IsPrivate) || x.Channel == null);

        var response = await articlesQuery
            .Take(filter.AmountOfArticles)
            .Skip(filter.Offset)
            .ToListAsync();

        return response;
    }
}