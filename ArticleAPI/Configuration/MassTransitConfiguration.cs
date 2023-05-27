using ArticleAPI.MessageHandlers;
using Common.DTOs;
using Common.Options;
using MassTransit;
using MassTransit.Transports.Fabric;

namespace ArticleAPI.Configuration;

public static class MassTransitConfiguration
{
    public static IServiceCollection MassTransitConfigure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserSubscriptionCreatedConsumer>();
            x.AddConsumer<UserCreationConsumer>();
            x.AddConsumer<UserSubscriptionCreatedConsumer>();
            x.AddConsumer<UserSubscriptionCancelledConsumer>();
            x.AddConsumer<UserSubscriptionRenewedConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = configuration.GetSection(nameof(RabbitMQOptions))
                    .Get<RabbitMQOptions>();
                cfg.Host(rabbitMqOptions.HostName, h =>
                {
                    h.Username(rabbitMqOptions.UserName);
                    h.Password(rabbitMqOptions.Password);
                });

                cfg.ReceiveEndpoint("articleAPI.Subscription", e =>
                {
                    e.ConfigureConsumer<UserSubscriptionCreatedConsumer>(context);
                    e.ConfigureConsumer<UserSubscriptionCancelledConsumer>(context);
                    e.ConfigureConsumer<UserSubscriptionRenewedConsumer>(context);
                });
                
                cfg.ReceiveEndpoint("articleAPI.User", e =>
                {
                    e.ConfigureConsumer<UserCreationConsumer>(context);
                });
            });
        });

        return services;
    }
}