namespace Core.Application.Pipelines.Caching;

public interface ICachableRequest
{
    #region Properties

    bool BypassCache { get; }
    string CacheKey { get; }
    TimeSpan? SlidingExpiration { get; }

    #endregion Properties
}
