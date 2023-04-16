using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Persistance.Contexts;

namespace Portfolio.Persistance.Configurations;

public static class DatabaseConnectionConfiguration
{
    #region Methods

    public static IServiceCollection AddDatabaseConnectionServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }

    #endregion Methods
}
