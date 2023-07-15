using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Core.Security.Jwt.Abstractions;
using Core.Security.Jwt.Concretes;
using Core.Mapper;
using Core.FileOperation.Concretes;
using Core.FileOperation.Abstractions;

namespace Porfolio.Application.Configurations;

public static class DependencyInjectionConfiguration
{
    #region Methods

    public static IServiceCollection AddApplicationDependencyInjectionServices(this IServiceCollection services)
    {        
        services.AddMapperServices();
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddSingleton<IFileOperation, FileOperation>();
        services.AddTransient<ITokenHelper, JwtHelper>();

        return services;
    }

    #endregion Methods
}
