using AuthAPI.Data.Repositories.Interfaces;

namespace AuthAPI.Configuration;

public static class RepositoriesRegistration
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        var allProviderTypes = System.Reflection
            .Assembly
            .GetAssembly(typeof(IBaseRepository<>))
            .GetTypes()
            .Where(t => t.Namespace is not null)
            .ToList();

        foreach (var serviceType in allProviderTypes.Where(t => t.IsInterface))
        {
            var implementationType = allProviderTypes
                .FirstOrDefault(c => c.IsClass && serviceType.Name.Substring(1) == c.Name);
            
            if (implementationType is not null)
                services.AddScoped(serviceType, implementationType);
        }

        return services;
    }
}