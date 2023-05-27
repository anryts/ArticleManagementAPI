using MediatR;

namespace ArticleAPI.Configuration;

public static class AdditionalServicesConfiguration
{
    public static IServiceCollection  ConfigureAdditionalServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddAutoMapper(typeof(Program));
        services.AddMediatR(typeof(Program));
        services.AddMemoryCache(opt => { opt.ExpirationScanFrequency = TimeSpan.FromMinutes(1);});
        services.AddResponseCaching();
        return services;
    }
}