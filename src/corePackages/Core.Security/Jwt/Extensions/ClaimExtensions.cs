using System.Security.Claims;

namespace Core.Security.Jwt.Extensions;

public static class ClaimExtensions
{
    #region Methods

    public static void AddNameIdentifier(this ICollection<Claim> claims, string id)
    {
        claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
    }

    public static void AddRoles(this ICollection<Claim> claims, IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
    }

    #endregion Methods
}
