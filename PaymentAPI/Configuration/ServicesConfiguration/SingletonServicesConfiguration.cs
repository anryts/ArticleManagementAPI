using Common.EventBus;
using Common.Options;
using Stripe;

namespace PaymentAPI.Configuration.ServicesConfiguration;

public static class SingletonServicesConfiguration
{
    public static IServiceCollection SingletonServicesConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBus, EventBus>();
        
        services.AddSingleton<IStripeClient>(
            new StripeClient(configuration.GetSection(nameof(StripeOptions)).Get<StripeOptions>().SecretKey));
        
        return services;
    }
}