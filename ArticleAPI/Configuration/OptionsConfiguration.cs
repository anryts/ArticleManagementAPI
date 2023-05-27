using Common.Options;

namespace ArticleAPI.Configuration;

public static class OptionsConfiguration
{
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AWSCredentials>(
            configuration.GetSection(nameof(AWSCredentials)));

        services.Configure<JwtOptions>(
            configuration.GetSection(nameof(JwtOptions)));

        services.Configure<RabbitMQOptions>(
            configuration.GetSection(nameof(RabbitMQOptions)));

        services.Configure<SendGridOptions>(
            configuration.GetSection(nameof(SendGridOptions)));

        return services;
    }
}