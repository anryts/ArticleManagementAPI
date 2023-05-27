using System.IdentityModel.Tokens.Jwt;
using UserAPI.API.Services.Interfaces;

namespace APIGateway.Services;

public class CurrentUserService : ICurrentUserService
{
    private Guid? _userId;
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        if (_userId.HasValue)
            return _userId.Value;

        _userId = GetUserIdFromClaims();
        return _userId!.Value;
    }
        
    private Guid? GetUserIdFromClaims()
    {
        var claims = _contextAccessor.HttpContext.User.Claims.ToList()
                     ?? throw new Exception("Claim's wasn't founded");
        var userId = claims.FirstOrDefault(x => x.Properties.Values.Any(y => y == JwtRegisteredClaimNames.Sub))
            .Value;
        return new Guid(userId);
    }
}