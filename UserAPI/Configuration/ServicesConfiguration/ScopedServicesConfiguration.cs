using Microsoft.AspNetCore.Identity;
using UserAPI.API.Services.Interfaces;
using UserAPI.Data.Entities;
using UserAPI.Providers;
using UserAPI.Providers.Interfaces;
using UserAPI.Services;

namespace UserAPI.Configuration.ServicesConfiguration;

public static class ScopedServicesConfiguration
{
    public static IServiceCollection ScopedServicesConfigure (this IServiceCollection services)
    {
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IEmailProvider, SengGridProvider>();
        services.AddScoped<ISmsProvider, TwilioProvider>();
        services.AddScoped<IPasswordHasher<User>>(_ =>
            new PasswordHasher<User>());
        services.AddScoped<IEmailProvider, SengGridProvider>();
        
        return services;
    }
}