using Core.Security.Entities;
using Core.Security.Jwt.Dtos;

namespace Core.Security.Jwt.Abstractions;

public interface ITokenHelper
{
    #region Methods

    Task<AccessToken> CreateTokenAsync(AppUser appUser);

    #endregion Methods
}
