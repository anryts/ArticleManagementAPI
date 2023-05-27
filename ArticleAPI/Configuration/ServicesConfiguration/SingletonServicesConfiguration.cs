using ArticleAPI.Providers;
using ArticleAPI.Providers.Interfaces;
using ArticleAPI.Services;
using ArticleAPI.Services.Interfaces;
using Common.Options;
using SendGrid;

namespace ArticleAPI.Configuration.ServicesConfiguration;

public static class SingletonServicesConfiguration
{
    public static IServiceCollection SingletonServicesConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBackgroundJobService, HangfireService>();

        services.AddSingleton<IFileProvider, AmazonProvider>();

        services.AddSingleton<ISendGridClient>(_ =>
        {
            var apiKey = configuration
                .GetSection(nameof(SendGridOptions))
                .Get<SendGridOptions>()
                .ApiKey;
            return new SendGridClient(apiKey);
        });

        return services;
    }
}