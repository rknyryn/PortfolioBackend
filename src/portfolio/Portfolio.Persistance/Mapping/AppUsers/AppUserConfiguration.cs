using Core.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Portfolio.Persistance.Mapping.AppUsers;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    #region Methods

    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        AppUser appUser = new()
        {
            Id = Guid.NewGuid(),
            FirstName = "Kaan",
            LastName = "Yarayan",
            UserName = "rknyryn@gmail.com",
            NormalizedUserName = "RKNYRYN@GMAIL.COM",
            Email = "rknyryn@gmail.com",
            NormalizedEmail = "RKNYRYN@GMAIL.COM",
            PhoneNumber = "XXXXXXXXXXXXX",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.Parse("000ec947-d4f7-4785-9a48-d46e2a100001").ToString("D"),
            LockoutEnabled = true,
        };
        appUser.PasswordHash = PassGenerate(appUser);

        builder.HasData(appUser);
    }

    private string PassGenerate(AppUser user)
    {
        var passHash = new PasswordHasher<AppUser>();
        return passHash.HashPassword(user, "Xm2^P,12P0'owB$");
    }

    #endregion Methods
}
