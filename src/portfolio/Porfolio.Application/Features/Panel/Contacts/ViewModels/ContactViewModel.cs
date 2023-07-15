using Core.Persistance.Paging.Abstractions;

namespace Porfolio.Application.Features.Panel.Contacts.ViewModels;

public class ContactViewModel : IViewModel
{
    #region Properties

    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool Active { get; set; }

    #endregion Properties
}
