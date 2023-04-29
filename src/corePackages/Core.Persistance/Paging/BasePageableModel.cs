namespace Core.Persistance.Paging;

public class BasePageableModel<T> where T : IViewModel
{
    #region Properties

    public int Index { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public int Pages { get; set; }

    public IList<T> Items { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }

    #endregion Properties
}
