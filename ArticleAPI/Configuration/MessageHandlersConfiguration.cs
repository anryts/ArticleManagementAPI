using ArticleAPI.MessageHandlers;
using Common.Options;

namespace ArticleAPI.Configuration;

public static class MessageHandlersConfiguration
{
    public static IServiceCollection ConfigureMessageHandlers(this IServiceCollection services)
    {
        services.AddHostedService<UserCreationMessageHandler>();
        services.AddHostedService<UserSubscriptionCreated>();

        return services;
    }
}