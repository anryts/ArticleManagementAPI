using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArticleAPI.Data.Repositories;

public class ArticleVersionRepository : BaseRepository<ArticleVersion>, IArticleVersionRepository
{
    public ArticleVersionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<ArticleVersion?> GetByIdWithIncludedDataAsync(Guid articleVersionId)
    {
        return await _dbSet
            .Include(x => x.ArticleVersionImages)
            .FirstOrDefaultAsync(x => x.Id == articleVersionId);
    }

    public async Task<IEnumerable<ArticleVersion>> GetArticleVersionsToArticle(Guid articleId)
    {
        return await _dbSet
            .Include(x => x.ArticleVersionImages)
            .Where(x => x.ArticleId == articleId)
            .ToListAsync();
    }

    public async Task<ArticleVersion?> LatestArticleVersion(Guid articleId)
    {
        return await _dbSet
            .Include(x => x.ArticleVersionImages)
            .Where(x => x.ArticleId == articleId)
            .OrderByDescending(x => x.UpdatedAt)
            .Take(1).SingleOrDefaultAsync();
    }
}