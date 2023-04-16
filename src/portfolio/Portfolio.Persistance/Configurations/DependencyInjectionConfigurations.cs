using Core.Persistance.Entities;
using Core.Persistance.Repositories.Abstractions;
using Core.Persistance.Repositories.Concretes;
using Core.Persistance.UnitOfWork.Abstractions;
using Core.Persistance.UnitOfWork.Concretes;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Persistance.Contexts;

namespace Portfolio.Persistance.Configurations;

public static class DependencyInjectionConfigurations
{
    #region Methods

    public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IReadRepository<BaseEntity>), typeof(EfReadRepository<BaseEntity, AppDbContext>));
        services.AddTransient(typeof(IWriteRepository<BaseEntity>), typeof(EfWriteRepository<BaseEntity, AppDbContext>));

        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

        return services;
    }

    #endregion Methods
}
