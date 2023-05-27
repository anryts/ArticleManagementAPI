using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;

namespace ArticleAPI.Data.Repositories;

public class ImageRepository : BaseRepository<ArticleImage>, IImageRepository
{
    public ImageRepository(AppDbContext context) : base(context)
    {
    }
}