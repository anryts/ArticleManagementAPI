using Microsoft.EntityFrameworkCore;
using UserAPI.Data.Entities;
using UserAPI.Data.Repositories.Interfaces;

namespace UserAPI.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> CheckIfEmailsIsUnique(string email)
    {
        return await _dbSet
            .AllAsync(user => user.Email != email);
    }
    
    public async Task<bool> CheckIfPhoneNumbersIsUnique(string phoneNumber)
    {
        return await _dbSet
            .AllAsync(user => user.PhoneNumber != phoneNumber);
    }
    
    public async Task<IEnumerable<string>> GetUsersEmailsAsync()
    {
        return await _dbSet
            .Where(user => user.IsActivated)
            .Select(user => user.Email)
            .ToListAsync();
    }
}