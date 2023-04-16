using Porfolio.Application;
using Portfolio.Persistance;

namespace Portfolio.WebAPI;

public static class WebAPIServiceRegistration
{
    #region Methods

    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices(configuration);
        services.AddPersistenceServices(configuration);

        return services;
    }

    #endregion Methods
}
