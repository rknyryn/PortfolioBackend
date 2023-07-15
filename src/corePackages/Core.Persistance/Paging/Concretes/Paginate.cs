using Core.Persistance.Entities;
using Core.Persistance.Paging.Abstractions;

namespace Core.Persistance.Paging.Concretes;

public class Paginate<T> : IPaginate<T> where T : BaseEntity
{
    #region Constructors

    internal Paginate(IEnumerable<T> source, int index, int size, int from)
    {
        var enumerable = source as T[] ?? source.ToArray();

        if (from > index)
            throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

        if (source is IQueryable<T> querable)
        {
            Index = index;
            Size = size;
            From = from;
            Count = querable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);

            Items = querable.Skip((Index - From) * Size).Take(Size).ToList();
        }
        else
        {
            Index = index;
            Size = size;
            From = from;

            Count = enumerable.Length;
            Pages = (int)Math.Ceiling(Count / (double)Size);

            Items = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
        }
    }

    internal Paginate()
    {
        Items = Array.Empty<T>();
    }

    #endregion Constructors

    #region Properties

    public int From { get; set; }
    public int Index { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public int Pages { get; set; }
    public IList<T> Items { get; set; }
    public bool HasPrevious => Index - From > 0;
    public bool HasNext => Index - From + 1 < Pages;

    #endregion Properties
}
