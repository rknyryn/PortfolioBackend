using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Persistance.Configurations;

namespace Portfolio.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConnectionServices(configuration.GetConnectionString("SQL"));
        services.AddDependencyInjectionServices();
        services.AddIdentityDependencyInjectionServices();

        return services;
    }
}
