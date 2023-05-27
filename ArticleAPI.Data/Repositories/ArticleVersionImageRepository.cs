using ArticleAPI.Data;
using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories;
using ArticleAPI.Data.Repositories.Interfaces;

namespace Skillup.API.Data.Repositories;

public class ArticleVersionImageRepository : BaseRepository<ArticleVersionImage>, IArticleVersionImageRepository
{
    public ArticleVersionImageRepository(AppDbContext context) : base(context)
    {
    }
}