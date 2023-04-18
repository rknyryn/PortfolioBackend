using Porfolio.Application;
using Portfolio.Persistance;
using Portfolio.WebAPI.Configurations;

namespace Portfolio.WebAPI;

public static class WebAPIServiceRegistration
{
    #region Methods

    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices(configuration);
        services.AddPersistenceServices(configuration);
        services.AddSwaggerServices();

        return services;
    }

    #endregion Methods
}
