using PaymentAPI.Services;
using UserAPI.API.Services.Interfaces;
using UserAPI.Services;

namespace PaymentAPI.Configuration.ServicesConfiguration;

public static class ScopedServicesConfiguration
{
    public static IServiceCollection ScopedServicesConfigure (this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        return services;
    }
}