using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;

namespace ArticleAPI.Data.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }
}
