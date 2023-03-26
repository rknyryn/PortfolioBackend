using Core.Security.Entities;
using Core.Security.Jwt.Abstractions;
using Core.Security.Jwt.Constants;
using Core.Security.Jwt.Dtos;
using Core.Security.Jwt.Encryption;
using Core.Security.Jwt.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Core.Security.Jwt.Concretes;

public class JwtHelper : ITokenHelper
{
    #region Fields

    private readonly UserManager<AppUser> _userManager;
    private readonly TokenOption _tokenOption;

    #endregion Fields

    #region Constructors

    public JwtHelper(UserManager<AppUser> userManager, IOptions<TokenOption> tokenOption)
    {
        _userManager = userManager;
        _tokenOption = tokenOption.Value;
    }

    #endregion Constructors

    #region Methods

    public async Task<AccessToken> CreateTokenAsync(AppUser appUser)
    {
        await _userManager.ResetAccessFailedCountAsync(appUser);
        //await _userManager.UpdateAsync(appUser);

        DateTime tokenExpiry = DateTime.UtcNow.AddMinutes(_tokenOption.TokenExpires);
        RefreshToken refreshToken = GenerateRefreshToken();

        await _userManager.RemoveAuthenticationTokenAsync(appUser, AuthenticationTokenConstants.LoginProviderName, AuthenticationTokenConstants.RefreshToken);
        await _userManager.SetAuthenticationTokenAsync(appUser, AuthenticationTokenConstants.LoginProviderName, AuthenticationTokenConstants.RefreshToken, refreshToken.Token);

        //await _userManager.UpdateAsync(appUser);

        return new AccessToken
        {
            Token = GenerateToken(appUser, tokenExpiry),
            TokenExpiration = tokenExpiry,
            RefreshToken = refreshToken
        };
    }

    private RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        DateTime refreshTokenExpiry = DateTime.UtcNow.AddHours(_tokenOption.RefresTokenExpires);
        return new RefreshToken
        {
            Token = $"{Convert.ToBase64String(randomNumber)}{SecurityConstants.Refresh_Token_Split_Character}{refreshTokenExpiry}",
            Expiry = refreshTokenExpiry
        };
    }

    private string GenerateToken(AppUser appUser, DateTime tokenExpiry)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOption.SecurityKey);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(GenerateTokenClaims(appUser).Result),
            Expires = tokenExpiry,
            SigningCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private async Task<IEnumerable<Claim>> GenerateTokenClaims(AppUser appUser)
    {
        IEnumerable<string> appUserRoles = await _userManager.GetRolesAsync(appUser);

        List<Claim> claims = new();
        claims.AddNameIdentifier(appUser.Id.ToString());
        claims.AddRoles(appUserRoles);

        return claims;
    }

    #endregion Methods
}
