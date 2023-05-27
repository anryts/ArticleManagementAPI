using ArticleAPI.Data;
using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories;
using ArticleAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Skillup.API.Data.Repositories;

public class ArticleLikeRepository : BaseRepository<ArticleLike>, IArticleLikeRepository
{
    public ArticleLikeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<ArticleLike?> GetByIdAsync(Guid articleId, Guid authorId)
    {
        return await _dbSet
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.ArticleId == articleId && x.AuthorId == authorId);
    }
}