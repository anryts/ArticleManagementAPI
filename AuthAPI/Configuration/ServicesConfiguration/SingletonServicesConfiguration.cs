using APIGateway.Services;
using APIGateway.Services.Interfaces;
using Common.EventBus;
using Common.Options;
using SendGrid;
using Twilio.Clients;

namespace AuthAPI.Configuration.ServicesConfiguration;

public static class SingletonServicesConfiguration
{
    public static IServiceCollection SingletonServicesConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordUpdatedHandlerService, PasswordUpdatedHandlerService>();

        services.AddSingleton<IEventBus, EventBus>();
        
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
        
        return services;
    }
}