using Core.Persistance.Entities;

namespace Portfolio.Domain.Entities.Contacts;

public class Contact : BaseEntity
{
    #region Properties

    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    #endregion Properties
}
