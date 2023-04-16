using Core.Security.Entities;
using Core.Security.Jwt.Dtos;
using System.Security.Claims;

namespace Core.Security.Jwt.Abstractions;

public interface ITokenHelper
{
    #region Methods

    Task<AccessToken> CreateTokenAsync(AppUser appUser);
    ClaimsPrincipal ValidateToken(string token);

    #endregion Methods
}
