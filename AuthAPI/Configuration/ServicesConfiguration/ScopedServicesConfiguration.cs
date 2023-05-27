using APIGateway.Providers.Interfaces;
using APIGateway.Services;
using APIGateway.Services.Interfaces;
using AuthAPI.Providers;
using AuthAPI.Services;
using Gateway.Data.Entities;
using Microsoft.AspNetCore.Identity;
using UserAPI.API.Services.Interfaces;

namespace AuthAPI.Configuration.ServicesConfiguration;

public static class ScopedServicesConfiguration
{
    public static IServiceCollection ScopedServicesConfigure (this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IEmailProvider, SengGridProvider>();
        services.AddScoped<ISmsProvider, TwilioProvider>();
        services.AddScoped<IPasswordHasher<User>>(_ =>
            new PasswordHasher<User>());
        
        return services;
    }
}