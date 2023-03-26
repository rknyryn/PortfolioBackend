using Core.Application.Utilities.FileOperations.Abstractions;
using Core.Application.Utilities.FileOperations.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Porfolio.Application.Configurations;

public static class DependencyInjectionConfiguration
{
    #region Methods

    public static IServiceCollection AddApplicationDependencyInjectionServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<IFileOperation, FileOperation>();

        return services;
    }

    #endregion Methods

}
