using Core.Application.Rules.Factories;
using Core.Application.Rules.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Porfolio.Application.Configurations;

public static class BusinessRulesConfiguration
{
    #region Methods

    public static IServiceCollection AddTransientBusinessRules(this IServiceCollection services, Assembly[] assemblyList)
    {
        services.AddTransient<IBusinessRuleFactory, BusinessRuleFactory>();

        var types = assemblyList.SelectMany(p => p.GetTypes());

        var businessRules = types
            .Where(p => p.GetInterfaces().Any(interfaceType => interfaceType == typeof(IBusinessRule)));

        foreach (Type businessRule in businessRules)
        {   
            services.AddTransient(typeof(IBusinessRule), businessRule);
        }

        return services;
    }

    #endregion Methods
}
