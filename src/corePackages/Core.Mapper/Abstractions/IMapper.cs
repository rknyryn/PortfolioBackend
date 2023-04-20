namespace Core.Abstractions.Mapper;

public interface IMapper
{
    #region Methods

    TDestination Map<TSource, TDestination>(TSource source);

    #endregion Methods
}
