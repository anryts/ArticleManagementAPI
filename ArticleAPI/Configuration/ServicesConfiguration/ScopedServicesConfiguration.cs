using ArticleAPI.DailyJobs;
using ArticleAPI.Providers;
using ArticleAPI.Providers.Interfaces;
using ArticleAPI.Services;
using ArticleAPI.Services.Interfaces;
using SendGrid;

namespace ArticleAPI.Configuration.ServicesConfiguration;

public static class ScopedServicesConfiguration
{
    public static IServiceCollection ScopedServicesConfigure (this IServiceCollection services)
    {
        services.AddScoped<IMessageProvider, RabbitMqMessageProvider>();
        services.AddScoped<SendNewArticlesToUsers>();
        services.AddScoped<IEmailProvider, SengGridProvider>();
        services.AddScoped<IMessageProvider, RabbitMqMessageProvider>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}