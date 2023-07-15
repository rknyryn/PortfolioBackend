namespace Core.Abstractions.Mapper;

public interface IMapper
{
    #region Methods

    TDestination Map<TSource, TDestination>(TSource source);
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

    #endregion Methods
}
