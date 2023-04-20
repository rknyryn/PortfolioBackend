using Core.Security.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Porfolio.Application.Configurations;

namespace Porfolio.Application;

public static class ApplicationServiceRegistration
{
    #region Methods

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDependencyInjectionServices();
        services.AddPipelineServices();
        services.AddAuthenticationServices();

        return services;
    }

    #endregion Methods
}
