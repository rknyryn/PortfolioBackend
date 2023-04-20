using Core.CrossCuttingConcern.Exceptions.Exceptions;
using Core.Security.Entities;
using Core.Security.Jwt.Constants;
using Microsoft.AspNetCore.Identity;

namespace Porfolio.Application.Features.Panel.Authentications.Rules;

public class AuthenticationRules
{
    #region Fields

    private readonly UserManager<AppUser> _userManager;

    #endregion Fields

    #region Constructors

    public AuthenticationRules(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    #endregion Constructors

    #region Methods

    public void CheckIfAppUserExists(AppUser appUser)
    {
        if (appUser is null) throw new BusinessException("Kullanıcı bulunamadı.");
    }

    public async Task IsRefreshTokenExistAsync(AppUser appUser, string refreshToken)
    {
        string refreshTokenFromDb =
            await _userManager.GetAuthenticationTokenAsync(appUser, AuthenticationTokenConstants.LoginProviderName, AuthenticationTokenConstants.RefreshToken)
            ?? throw new UnauthorizedAccessException();

        if (!string.Equals(refreshTokenFromDb, refreshToken, StringComparison.Ordinal)) throw new UnauthorizedAccessException();
    }

    #endregion Methods
}
