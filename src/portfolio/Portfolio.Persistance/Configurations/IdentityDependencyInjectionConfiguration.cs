using Core.Security.Entities;
using Core.Security.Jwt.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Persistance.Contexts;

namespace Portfolio.Persistance.Configurations;

public static class IdentityDependencyInjectionConfiguration
{
    #region Methods

    public static IServiceCollection AddIdentityDependencyInjectionServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
        {
            //User Default Validaton
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrstuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@";

            //Password Default Validaton
            options.Password.RequiredLength = SecurityConstants.PASSWORD_MIN_LENGTH;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        return services;
    }

    #endregion Methods
}
