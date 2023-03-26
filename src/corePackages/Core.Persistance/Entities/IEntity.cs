﻿namespace Core.Persistance.Entities;

public class IEntity
{
    #region Properties

    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    #endregion Properties
}

