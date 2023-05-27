using AuthAPI.Data.Repositories.Interfaces;
using Gateway.Data.Entities;

namespace AuthAPI.Data.Repositories;

public class IdentityUserRepository : BaseRepository<IdentityUser>, IIdentityUserRepository
{
    public IdentityUserRepository(AppDbContext context) : base(context)
    {
    }
}