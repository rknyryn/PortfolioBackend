﻿using Core.Application.Utilities.FileOperations.Abstractions;
using Core.Application.Utilities.FileOperations.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Core.Security.Jwt.Abstractions;
using Core.Security.Jwt.Concretes;
using Core.Security.Configurations;
using Microsoft.Extensions.Configuration;

namespace Porfolio.Application.Configurations;

public static class DependencyInjectionConfiguration
{
    #region Methods

    public static IServiceCollection AddApplicationDependencyInjectionServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationServices(configuration);

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<IFileOperation, FileOperation>();
        services.AddTransient<ITokenHelper, JwtHelper>();

        return services;
    }

    #endregion Methods

}