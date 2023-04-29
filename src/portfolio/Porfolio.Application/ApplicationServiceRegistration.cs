using Core.Security.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Porfolio.Application.Configurations;

namespace Porfolio.Application;

public static class ApplicationServiceRegistration
{
    #region Methods

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddApplicationDependencyInjectionServices();
        services.AddPipelineServices();
        services.AddAuthenticationServices();
        services.AddTransientBusinessRules(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    #endregion Methods
}
