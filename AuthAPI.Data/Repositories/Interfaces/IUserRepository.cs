using Gateway.Data.Entities;

namespace AuthAPI.Data.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IEnumerable<string>> GetUsersEmailsAsync();
    Task<bool> CheckIfPhoneNumbersIsUnique(string phoneNumber);
    Task<bool> CheckIfEmailsIsUnique(string email);
}