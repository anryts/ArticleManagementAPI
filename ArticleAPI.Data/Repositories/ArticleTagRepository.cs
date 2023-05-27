using ArticleAPI.Data;
using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories;
using ArticleAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Skillup.API.Data.Repositories;

public class ArticleTagRepository : BaseRepository<ArticleTag>, IArticleTagRepository
{
    public ArticleTagRepository(AppDbContext context) : base(context)
    {
    }

    public async Task DeleteByArticleIdAsync(Guid articleId)
    {
        var articleTags = await _dbSet
            .Where(x => x.ArticleId == articleId)
            .ToListAsync();

        _dbSet.RemoveRange(articleTags);
    }

    public async Task CreateByArticleIdAsync(Guid articleId, List<string> tagNames)
    {
        var articleTags = tagNames
            .Select(tagName => new ArticleTag
            {
                ArticleId = articleId, TagName = tagName
            });

        await _dbSet.AddRangeAsync(articleTags);
    }

    public async Task AddAsync(Guid id, List<string> tags)
    {
        await _dbSet
            .AddRangeAsync(tags
                .Select(tagName => new ArticleTag { ArticleId = id, TagName = tagName }));
    }
}