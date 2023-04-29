﻿using Core.Persistance.Entities;

namespace Core.Persistance.Paging;

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

//internal class Paginate<TSource, TResult> : IPaginate<TResult>
//    where TSource: BaseEntity
//    where TResult : BaseEntity
//{
//    #region Constructors

//    public Paginate(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
//        int index, int size, int from)
//    {
//        var enumerable = source as TSource[] ?? source.ToArray();

//        if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

//        if (source is IQueryable<TSource> queryable)
//        {
//            Index = index;
//            Size = size;
//            From = from;
//            Count = queryable.Count();
//            Pages = (int)Math.Ceiling(Count / (double)Size);

//            var items = queryable.Skip((Index - From) * Size).Take(Size).ToArray();

//            Items = new List<TResult>(converter(items));
//        }
//        else
//        {
//            Index = index;
//            Size = size;
//            From = from;
//            Count = enumerable.Length;
//            Pages = (int)Math.Ceiling(Count / (double)Size);

//            var items = enumerable.Skip((Index - From) * Size).Take(Size).ToArray();

//            Items = new List<TResult>(converter(items));
//        }
//    }


//    public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
//    {
//        Index = source.Index;
//        Size = source.Size;
//        From = source.From;
//        Count = source.Count;
//        Pages = source.Pages;

//        Items = new List<TResult>(converter(source.Items));
//    }

//    #endregion Constructors

//    #region Properties

//    public int Index { get; }

//    public int Size { get; }

//    public int Count { get; }

//    public int Pages { get; }

//    public int From { get; }

//    public IList<TResult> Items { get; }

//    public bool HasPrevious => Index - From > 0;

//    public bool HasNext => Index - From + 1 < Pages;

//    #endregion Properties
//}
