using UserAPI.MessageHandlers;

namespace UserAPI.Configuration;

public static class MessageHandlersConfiguration
{
    public static IServiceCollection ConfigureMessageHandlers(this IServiceCollection services)
    {
        //services.AddHostedService<UserCreationMessageHandler>();
        //services.AddHostedService<UserCreationConsumer>();
        
        return services;
    }
}