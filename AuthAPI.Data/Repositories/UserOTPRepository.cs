using AuthAPI.Data.Repositories.Interfaces;
using Gateway.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data.Repositories;

public class UserOTPRepository : BaseRepository<UserOTP>, IUserOTPRepository
{
    public UserOTPRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<UserOTP?> GetByIdAsync(Guid userId, string code)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.Code == code && x.UserId == userId);
    }
}

public interface IUserOTPRepository : IBaseRepository<UserOTP>
{
    Task<UserOTP?> GetByIdAsync(Guid userId, string code);
}