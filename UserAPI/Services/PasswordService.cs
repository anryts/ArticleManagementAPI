using Microsoft.AspNetCore.Identity;
using UserAPI.API.Services.Interfaces;
using UserAPI.Data.Entities;

namespace UserAPI.Services;

public class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string GenerateHashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    public bool VerifyHashPassword(string password, string hash)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, hash, password);
        return result == PasswordVerificationResult.Success;
    }
}