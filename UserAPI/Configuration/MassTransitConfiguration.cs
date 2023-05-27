using Common.DTOs;
using Common.Options;
using MassTransit;
using MassTransit.Transports.Fabric;
using UserAPI.MessageHandlers;

namespace UserAPI.Configuration;

public static class MassTransitConfiguration
{
    public static IServiceCollection MassTransitConfigure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreationConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = configuration.GetSection(nameof(RabbitMQOptions))
                    .Get<RabbitMQOptions>();
                cfg.Host(rabbitMqOptions.HostName, h =>
                {
                    h.Username(rabbitMqOptions.UserName);
                    h.Password(rabbitMqOptions.Password);
                });
                
                cfg.ReceiveEndpoint("userAPI.User", e =>
                {       
                    e.ConfigureConsumer<UserCreationConsumer>(context);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}