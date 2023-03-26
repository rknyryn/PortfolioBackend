namespace Core.Persistance.Paging;

public class BasePageableModel
{
    #region Properties

    public int Index { get; set; }

    public int Size { get; set; }

    public int Count { get; set; }

    public int Pages { get; set; }

    public bool HasPrevious { get; set; }

    public bool HasNext { get; set; }

    #endregion Properties
}
