using Core.Abstractions.Mapper;
using Core.Concretes.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Mapper;

public static class MapperServiceRegistration
{
    #region Methods

    public static IServiceCollection AddMapperServices(this IServiceCollection services)
    {
        services.AddSingleton<IMapper, MapsterProvider>();

        return services;
    }

    #endregion Methods
}
