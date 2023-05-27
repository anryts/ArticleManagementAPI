
using AuthAPI.MessageHandlers;

namespace APIGateway.Configuration;

public static class MessageHandlersConfiguration
{
    public static IServiceCollection ConfigureMessageHandlers(this IServiceCollection services)
    {
        services.AddHostedService<UserCreationMessageHandler>();
        
        return services;
    }
}