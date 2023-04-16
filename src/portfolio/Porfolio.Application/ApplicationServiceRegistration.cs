using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Porfolio.Application.Configurations;

namespace Porfolio.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDependencyInjectionServices(configuration);
            services.AddPipelineServices();

            return services;
        }
    }
}
