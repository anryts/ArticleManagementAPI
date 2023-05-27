using FluentValidation;
using MediatR;

namespace UserAPI.Configuration;

public static class AdditionalServicesConfiguration
{
    public static IServiceCollection  ConfigureAdditionalServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddMediatR(typeof(Program));
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssemblyContaining<Program>();
        
        return services;
    }
}