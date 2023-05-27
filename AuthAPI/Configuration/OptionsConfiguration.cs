using Common.Options;

namespace AuthAPI.Configuration;

public static class OptionsConfiguration
{
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StripeOptions>(
            configuration.GetSection(nameof(StripeOptions)));
        
        services.Configure<JwtOptions>(
            configuration.GetSection(nameof(JwtOptions)));
        
        services.Configure<TwilioOptions>(
            configuration.GetSection(nameof(TwilioOptions)));
        
        services.Configure<SendGridOptions>(
            configuration.GetSection(nameof(SendGridOptions)));
        
        services.Configure<FilePaths>(
            configuration.GetSection(nameof(FilePaths)));

        services.Configure<RabbitMQOptions>(
            configuration.GetSection(nameof(RabbitMQOptions)));
        
        return services;
    }
}