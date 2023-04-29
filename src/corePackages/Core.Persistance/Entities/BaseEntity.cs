namespace Core.Persistance.Entities;

public abstract class BaseEntity
{
    #region Properties

    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime DeletedDate { get; set; }
    public bool Active { get; set; }
    public bool Deleted { get; set; }
    public bool PermentlyDeleted { get; set; }

    #endregion Properties
}

