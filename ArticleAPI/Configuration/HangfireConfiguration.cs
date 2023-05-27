using Hangfire;
using Hangfire.PostgreSql;

namespace ArticleAPI.Configuration;

public static class HangfireConfiguration
{
    public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHangfire(hangfireConfiguration => hangfireConfiguration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(configuration.GetConnectionString("AppDbContext"), new PostgreSqlStorageOptions
                {
                    SchemaName = "hangfire"
                }));
        services.AddHangfireServer();

        return services;
    }
}