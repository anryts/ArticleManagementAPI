using System.IdentityModel.Tokens.Jwt;
using APIGateway.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserAPI.API.Services.Interfaces;

namespace APIGateway.CustomFilters;

public class AuthAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private ICurrentUserService _currentUserService;
    private IPasswordUpdatedHandlerService _passwordUpdatedHandlerService;

    public AuthAttribute()
    {
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();
        _passwordUpdatedHandlerService =
            context.HttpContext.RequestServices.GetRequiredService<IPasswordUpdatedHandlerService>();
        var authToken = context.HttpContext.Request.Headers.Authorization.FirstOrDefault() ??
                        throw new ArgumentNullException();
        var token = authToken.Split(" ")[1];
        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.ReadToken(token) as JwtSecurityToken;
        var userId = _currentUserService.GetCurrentUserId();
        var updatedAt = _passwordUpdatedHandlerService.GetDateTime(userId);
        if (updatedAt > securityToken.ValidFrom)
        {
            context.Result = context.Result = new ContentResult
                { Content = "Token is invalid\nNeed to regenarate jwt", StatusCode = 401 };
        }
    }
}