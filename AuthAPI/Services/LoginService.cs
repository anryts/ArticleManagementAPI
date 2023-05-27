using APIGateway.Services.Interfaces;
using AuthAPI.Data.Repositories;
using AuthAPI.Data.Repositories.Interfaces;
using Common.Models.RequestModels;
using Common.Models.ResponseModels;
using Gateway.Data.Entities;
using Microsoft.EntityFrameworkCore;
using UserAPI.API.Services.Interfaces;
using UserAPI.Exthensions;

namespace AuthAPI.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IUserOTPRepository _userOtpRepository;
    private readonly IIdentityUserRepository _userIdentityRepository;

    public LoginService(IJwtService jwtService,
        IUserOTPRepository userOtpRepository,
        IUserRepository userRepository, IIdentityUserRepository userIdentityRepository)
    {
        _jwtService = jwtService;
        _userOtpRepository = userOtpRepository;
        _userRepository = userRepository;
        _userIdentityRepository = userIdentityRepository;
    }


    public async Task<string> GenerateOTPCodeForUser(Guid userId)
    {
        var result = GenerateCodeExtension.GenerateCode(6);

        await _userOtpRepository.AddAsync(new UserOTP
        {
            UserId = userId,
            Code = result,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
        });
        return result;
    }

    public async Task<UserResponseSignInModel> EnterUserVia2FA(Guid userId, string code)
    {
        if (!await Verify2FA(userId, code))
            throw new Exception("Code was incorrect");

        var user = await _userRepository.GetByQueryAsync(q => q.Include(user => user.IdentityUsers)
            .FirstOrDefaultAsync(user => user.Id == userId));
        var tokenValue = _jwtService.CreateJwt(user.Id);
        var refreshToken = new IdentityUser
        {
            RefreshToken = Guid.NewGuid().ToString(),
            ExpireAt = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
        };

        user.IdentityUsers.Add(refreshToken);
        await _userRepository.UpdateAsync(user);

        return new UserResponseSignInModel { JwtToken = tokenValue, RefreshToken = refreshToken.RefreshToken };
    }

    public async Task<RefreshTokenResponseModel> RefreshToken(RefreshTokenModel model)
    {
        var refreshToken = await _userIdentityRepository
                               .GetByQueryAsync(q => q
                                   .FirstOrDefaultAsync(identityUser =>
                                       identityUser.RefreshToken == model.RefreshToken))
                           ?? throw new Exception("Not valid refresh token");
        if (refreshToken.ExpireAt < DateTime.UtcNow)
            throw new Exception("Refresh token was expired");

        await _userIdentityRepository.DeleteAsync(refreshToken);
        var newRefreshToken = new IdentityUser
        {
            RefreshToken = Guid.NewGuid().ToString(),
            ExpireAt = DateTime.UtcNow.AddDays(7),
            UserId = refreshToken.UserId,
        };

        await _userIdentityRepository.AddAsync(newRefreshToken);
        var tokenValue = _jwtService.CreateJwt(refreshToken.UserId);
        return new RefreshTokenResponseModel { AuthToken = tokenValue, RefreshToken = newRefreshToken.RefreshToken };
    }

    public async Task<bool> Verify2FA(Guid userId, string otpCode)
    {
        var otp = await _userOtpRepository.GetByIdAsync(userId, otpCode)
                  ?? throw new Exception("OTP not correct");
        if (otp.ExpiresAt < DateTime.UtcNow)
            throw new Exception("OTP code was expired");
        if (otp.IsUsed)
            throw new Exception("OTP code was used");
        otp.IsUsed = true;

        await _userOtpRepository.UpdateAsync(otp);
        await _userOtpRepository.SaveChangesAsync();
        return true;
    }
}