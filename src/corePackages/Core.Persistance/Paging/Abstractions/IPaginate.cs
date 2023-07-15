﻿using Core.Persistance.Entities;

namespace Core.Persistance.Paging.Abstractions;

public interface IPaginate<T> where T : BaseEntity
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