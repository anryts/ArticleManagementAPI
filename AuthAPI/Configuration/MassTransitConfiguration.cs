using Common.DTOs;
using Common.Options;
using MassTransit;
using MassTransit.Transports.Fabric;

namespace AuthAPI.Configuration;

public static class MassTransitConfiguration
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = configuration.GetSection(nameof(RabbitMQOptions)).Get<RabbitMQOptions>();
                cfg.Host(rabbitMqOptions.HostName, h =>
                {
                    h.Username(rabbitMqOptions.UserName);
                    h.Password(rabbitMqOptions.Password);
                });

                // cfg.Publish<UserCreationDto>(p =>
                // {
                //     p.ExchangeType = ExchangeType.FanOut.ToString().ToLower();
                // });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}