using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data.Entities;
using PaymentAPI.Data.Repositories.Interfaces;

namespace PaymentAPI.Data.Repositories;

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
}