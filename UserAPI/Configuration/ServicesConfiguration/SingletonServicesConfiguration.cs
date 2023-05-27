using Common.EventBus;
using Common.Options;
using SendGrid;
using Stripe;
using Twilio.Clients;
using UserAPI.API.Services.Interfaces;
using UserAPI.Providers;
using UserAPI.Providers.Interfaces;
using UserAPI.Services;

namespace UserAPI.Configuration.ServicesConfiguration;

public static class SingletonServicesConfiguration
{
    public static IServiceCollection SingletonServicesConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBackgroundJobService, HangfireService>();

        services.AddSingleton<IFileProvider, AmazonProvider>();

        services.AddSingleton<IEventBus, EventBus>();
        
        services.AddSingleton<IPasswordUpdatedHandlerService, PasswordUpdatedHandlerService>();

        services.AddSingleton<ITwilioRestClient>(
            _ => new TwilioRestClient(
                configuration.GetSection(nameof(TwilioOptions)).Get<TwilioOptions>().AccountSid,
                configuration.GetSection(nameof(TwilioOptions)).Get<TwilioOptions>().AuthToken));
        services.AddSingleton<ISendGridClient>(_ =>
        {
            var apiKey = configuration
                .GetSection(nameof(SendGridOptions))
                .Get<SendGridOptions>()
                .ApiKey;
            return new SendGridClient(apiKey);
        });
        
        services.AddSingleton<IStripeClient>(
            new StripeClient(configuration.GetSection(nameof(StripeOptions)).Get<StripeOptions>().SecretKey));
        
        return services;
    }
}