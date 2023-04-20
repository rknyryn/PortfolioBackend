namespace Core.Persistance.Paging;

public interface IPaginate<T>
{
    #region Properties

    int From { get; }
    int Index { get; }
    int Size { get; }
    int Count { get; }
    int Pages { get; }

    IList<T> Items { get; }
    bool HasPrevious { get; }
    bool HasNext { get; }

    #endregion Properties
}
