using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserAPI.API.Services.Interfaces;

namespace APIGateway.Services;

public class JwtService : IJwtService
{
    private readonly IOptions<JwtOptions> _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options;
    }

    public string CreateJwt(Guid id)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.Value.Token));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: signingCredentials
        );

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);
        return tokenValue;
    }
}