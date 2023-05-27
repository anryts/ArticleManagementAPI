using Common.Models.RequestModels;
using Common.Models.ResponseModels;

namespace APIGateway.Services.Interfaces
{
    public interface ILoginService
    {
        Task<UserResponseSignInModel> EnterUserVia2FA(Guid userId, string code);
        Task<string> GenerateOTPCodeForUser(Guid userId);
        Task<RefreshTokenResponseModel> RefreshToken(RefreshTokenModel model);
        Task<bool> Verify2FA(Guid userId, string otpCode);
    }
}