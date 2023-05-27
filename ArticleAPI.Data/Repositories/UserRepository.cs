using ArticleAPI.Data.Entities;
using ArticleAPI.Data.Repositories.Interfaces;

namespace ArticleAPI.Data.Repositories;

public class UserRepository : BaseRepository<Entities.User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}