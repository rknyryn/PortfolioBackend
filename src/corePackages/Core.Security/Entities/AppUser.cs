using Microsoft.AspNetCore.Identity;

namespace Core.Security.Entities;

public class AppUser : IdentityUser<Guid>
{
    #region Properties

    public string LastName { get; set; }
    public string FirstName { get; set; }

    #endregion Properties
}
