using Core.Abstractions.Mapper;
using Mapster;

namespace Core.Concretes.Mapper;

public class MapsterProvider : IMapper
{
    #region Methods

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return source.Adapt<TDestination>();
    }

    #endregion Methods
}
